using Common.Extensions;
using Common.Helpers;
using Common.Models;
using FileList.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Win32.Libraries;

namespace FileList.Logic
{
    internal sealed class ConcurrentFileSearch
    {
        public event EventHandler<ConcurrentFileSearchEventArgs> Finished;
        public event EventHandler<ConcurrentFileSearchEventArgs> Update;

        //private static volatile ConcurrentCollection<string> _directories;
        private string _root;
        private FileListControl _fileListControl;
        private DateTime startTime;
        private CancellationToken _cancelToken;
        private static volatile ConcurrentCollection<FileData> _fileData;
        private static volatile ConcurrentQueue<string> Directories = new ConcurrentQueue<string>("ConcurrentFileSearch Directories");
        private int MaxThreads;

        private static object fileListControlLock = new object();
        private static object locker = new object();

        public ConcurrentFileSearch(string rootPath, FileSearchWorkerArgs args)
        {
            this._root = rootPath;
            if (ConcurrentFileSearch._fileData == null)
                ConcurrentFileSearch._fileData = new ConcurrentCollection<FileData>("ConcurrentFileSearch fileData");
            this._fileListControl = args.FileListControl;
            this._cancelToken = args.CancellationToken;
        }

        public void Start()
        {
            this.MaxThreads = Math.Max(1, this._fileListControl.SearcherThreads);
            this.startTime = DateTime.Now;
            Shell32.ShellClass shell = new Shell32.ShellClass();
            ole32.GetRegisteredInterfaceMarshalPtr<Shell32.IShellDispatch5>(shell); //.ToIntPtr();
            GC.KeepAlive(shell);

            ConcurrentFileSearchMinion searcher = new ConcurrentFileSearchMinion(this._root, this.ConcurrentFileSearch_OnFinishedHandler, this._fileListControl);
            Thread thread = new Thread((object s) =>
            {
                ((ConcurrentFileSearchMinion)s).Start();
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start(searcher);
            this._fileListControl.InvokeIfRequired(f => f.SearcherThreads = 1);
            //thread.Join();
        }
        private void AttachFileData(IEnumerable<FileData> files, FileListControl fileListControl, bool commitRequired)
        {
            IoHelper.WriteToConsole("requesting fileListControlLock");
            lock (fileListControlLock)
            {
                fileListControl.InvokeIfRequired(c =>
                {
                    c.Enabled = false;
                    //c.Clear();

                    FileToIconConverter iconConverter = new FileToIconConverter();
                    if (c.TreeImageList == null)
                        c.TreeImageList = new ImageList();
                    ImageList treeImageList = c.TreeImageList;
                    if (!treeImageList.Images.ContainsKey(UiHelper.DirectoryKey))
                        treeImageList.Images.Add(UiHelper.DirectoryKey, iconConverter.GetImage(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), IconSize.Small).ToBitmap());
                    if (!treeImageList.Images.ContainsKey(UiHelper.ZipExtension))
                        treeImageList.Images.Add(UiHelper.ZipExtension, iconConverter.GetImage(UiHelper.ZipExtension, IconSize.Small).ToBitmap());
                    if (!treeImageList.Images.ContainsKey(UiHelper.NoneFileExtension))
                        treeImageList.Images.Add(UiHelper.NoneFileExtension, iconConverter.GetImage(UiHelper.NoneFileExtension, IconSize.Small).ToBitmap());

                    foreach (FileData file in files)
                    {
                        ImageList.ImageCollection images = treeImageList.Images;
                        if (!images.ContainsKey(file.Extension))
                        {
                            Bitmap icon = iconConverter.GetImage(file.Path, IconSize.Small).ToBitmap();
                            images.Add(file.Extension, icon);
                        }

                        c.AddFileData(file);
                    }
                });
            }
            IoHelper.WriteToConsole("released fileListControlLock");
        }

        private void FinalizeFileListControl(FileListControl fileListControl)
        {
            fileListControl.InvokeIfRequired(c =>
            {
                //c.ExpandTree();
                c.ScrollTreeToTop();
                c.FileTypeListSorted = true;
                c.Enabled = true;
            });
        }

        private void ConcurrentFileSearch_OnFinishedHandler(object sender, ConcurrentFileSearchEventArgs e)
        {
            ConcurrentFileSearchMinion searchSta = (ConcurrentFileSearchMinion)sender;
            searchSta.OnFinishedHandler -= this.ConcurrentFileSearch_OnFinishedHandler;

            if (!this._cancelToken.IsCancellationRequested)
                this.SummonMinions(this.MaxThreads);
            else
                ConcurrentFileSearchMinion.Cancel();

            int bucketCount = ConcurrentFileSearchMinion.ThreadBucketCount();
            this._fileListControl.InvokeIfRequired(c => c.SearcherThreads = bucketCount);
            IoHelper.WriteToConsole("threads {0}", bucketCount);

            if ((ConcurrentFileSearch.Directories.Count == 0 || this._cancelToken.IsCancellationRequested) && ConcurrentFileSearchMinion.ThreadBucketCount() == 0)
            {
                DateTime endTime = DateTime.Now;
                TimeSpan timeSpan = endTime.Subtract(startTime);

                IoHelper.WriteToConsole("processing finished. took {0} hours, {1} minutes, {2} seconds", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
                IoHelper.WriteToConsole("finalizing");
                this.FinalizeFileListControl(this._fileListControl);
                this.OnFinished(e);
                ConcurrentFileSearchMinion.ResetCancelled();
                ConcurrentFileSearch.Directories = new ConcurrentQueue<string>("ConcurrentFileSearch Directories");
                ConcurrentFileSearch._fileData = new ConcurrentCollection<FileData>("ConcurrentFileSearch fileData");
                GC.Collect();
            }
        }

        private void SummonMinions(int maxThreads)
        {
            string directory = null;
            int bucketCount = 0;

            IoHelper.WriteToConsole("{0} directories to process", ConcurrentFileSearch.Directories.Count);

            if ((bucketCount = ConcurrentFileSearchMinion.ThreadBucketCount()) > maxThreads)
                return;

            while ((directory = ConcurrentFileSearch.Directories.Take()) != null)
            {
                ConcurrentFileSearchMinion searcher = new ConcurrentFileSearchMinion(directory, this.ConcurrentFileSearch_OnFinishedHandler, this._fileListControl);
                Thread thread = new Thread((object o) =>
                {
                    //// moved searcher out because ThreadBucket.Add would interfere (thread lock) with while ((directory = ConcurrentFileSearch.Directories.Take()) != null)
                    //string localDir = o?.ToString();
                    //ConcurrentFileSearchSta searcher = new ConcurrentFileSearchSta(localDir, this.ConcurrentFileSearch_OnFinishedHandler);
                    //searcher.Start();
                    ((ConcurrentFileSearchMinion)o).Start();

                });
                thread.SetApartmentState(ApartmentState.STA);
                thread.IsBackground = true;
                thread.Start(searcher);
                //thread.Join();
                this._fileListControl.InvokeIfRequired(f => f.SearcherThreads += 1);

                bucketCount++;

                IoHelper.WriteToConsole("Created thread");
                if (bucketCount >= maxThreads || (bucketCount = ConcurrentFileSearchMinion.ThreadBucketCount()) >= maxThreads)
                //if ((bucketCount = ConcurrentFileSearchMinion.ThreadBucketCount()) >= maxThreads)
                        break;
            }
        }

        private void OnFinished(ConcurrentFileSearchEventArgs args)
        {
            EventHandler<ConcurrentFileSearchEventArgs> handler = this.Finished;
            if (handler == null)
            {
                IoHelper.WriteToConsole("handler null");
                return;
            }
            this.Finished(this, args);
        }

        private void OnUpdate(ConcurrentFileSearchEventArgs args)
        {
            EventHandler<ConcurrentFileSearchEventArgs> handler = this.Update;
            if (handler == null)
                return;
            this.Update(this, args);
        }


        private struct ConcurrentFileSearchMinion
        {
            internal event EventHandler<ConcurrentFileSearchEventArgs> OnFinishedHandler;

            private static ConcurrentCollection<int> ThreadBucket;
            private FileSearch _fileSearch;
            private static int IdTracker;
            private static object IdLock;
            private readonly int ID;
            private string _root;
            private EventHandler<ConcurrentFileSearchEventArgs> _handler;
            private List<FileData> _files;
            private static object StaticLock = new object();
            private static volatile int _bucketCount = 0;
            private static object BucketCountLock = new object();

            static ConcurrentFileSearchMinion()
            {
                lock (StaticLock)
                {
                    if (ConcurrentFileSearchMinion.IdLock != null)
                        return;
                    ConcurrentFileSearchMinion.IdLock = new object();
                    ConcurrentFileSearchMinion.IdTracker = int.MinValue;
                    //ConcurrentFileSearchMinion.ThreadBucket = new ConcurrentCollection<int>("ConcurrentFileSearchSta ThreadBucket");
                    ConcurrentFileSearchMinion.CancelLock = new object();
                    ConcurrentFileSearchMinion._isCancelled = false;
                }
            }
            private FileListControl _fileListControl;
            public ConcurrentFileSearchMinion(string root, EventHandler<ConcurrentFileSearchEventArgs> onFinishedHandler, FileListControl fileListControl)
            {
                System.Threading.Interlocked.Increment(ref _bucketCount);
                this._root = root;
                this._files = new List<FileData>();
                this._fileSearch = null;
                this._handler = onFinishedHandler;
                this.ID = 0;
                this.OnFinishedHandler = null;

                this._fileListControl = fileListControl;
                this.OnFinishedHandler += this._handler;
                this.ID = this.GetNextId();

            }

            [STAThread]
            public void Start()
            {
                if (!ConcurrentFileSearchMinion.IsCancelled())
                {
                    this.AddDirectories(this._root, ConcurrentFileSearch.Directories);
                    this.GetFiles(this._root, this._files);
                }

                this.RmoveFromThreadBucket();
                this.OnFinished(new ConcurrentFileSearchEventArgs(null));
            }

            private void RmoveFromThreadBucket()
            {
                IoHelper.WriteToConsole("removing thread id #{0}", this.ID);
                System.Threading.Interlocked.Decrement(ref _bucketCount);
            }

            public static int ThreadBucketCount()
            {
                return Interlocked.CompareExchange(ref _bucketCount, 0, 0);
            }

            private static bool _isCancelled;
            private static object CancelLock;
            public static void Cancel()
            {
                lock (ConcurrentFileSearchMinion.CancelLock)
                {
                    ConcurrentFileSearchMinion._isCancelled = true;
                }
            }

            public static void ResetCancelled()
            {
                lock (ConcurrentFileSearchMinion.CancelLock)
                {
                    ConcurrentFileSearchMinion._isCancelled = false;
                }
            }

            private static bool IsCancelled()
            {
                lock (ConcurrentFileSearchMinion.CancelLock)
                {
                    return ConcurrentFileSearchMinion._isCancelled;
                }
            }


            private int GetNextId()
            {
                int id = 0;
                IoHelper.WriteToConsole("requesting IdLock");
                lock (ConcurrentFileSearchMinion.IdLock)
                {
                    id = ConcurrentFileSearchMinion.IdTracker;
                    if (ConcurrentFileSearchMinion.IdTracker == int.MaxValue)
                        ConcurrentFileSearchMinion.IdTracker = int.MinValue;
                    ++ConcurrentFileSearchMinion.IdTracker;
                }
                IoHelper.WriteToConsole("IdLock released");

                return id;
            }

            private static object minionLock = new object();
            private void SummonMinions(string root)
            {
                try
                {
                    foreach (string directory in IoHelper.AccessableDirectories(root))
                    {
                        IoHelper.WriteToConsole("created minion");
                        ConcurrentFileSearchMinion search = new ConcurrentFileSearchMinion(directory, this._handler, this._fileListControl); // arguments incorrect, only for ability to compile
                        Thread thread = new Thread((object s) => ((ConcurrentFileSearchMinion)s).Start());
                        thread.SetApartmentState(ApartmentState.STA);
                        thread.Start(search);
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
            }

            private void AddDirectories(string path, ConcurrentQueue<string> directories)
            {
                IoHelper.WriteToConsole("thread id #{0} adding dirs in {1}", this.ID, path);
                directories.AddRange(IoHelper.AccessableDirectories(path));
            }

            private void GetFiles(string root, IList<FileData> files)
            {
                try
                {
                    this._fileSearch = new FileSearch(root, false, ole32.GetIShellDispatch5());

                    // access thread unsafe ConcurrentFileSearchMinion._isCancelled
                    // its ok because its not a big deal to get incorrect value right away
                    // if thread access exception is thrown, it is because value is changing.
                    // we catch the error, forcing us to quit.
                    while (this._fileSearch.GetNext() != null && !ConcurrentFileSearchMinion._isCancelled)
                    {
                        IoHelper.WriteToConsole("thread id #{0} found file {1}", this.ID, this._fileSearch.Current.Value.Path);
                        this.AttachFileData(this._fileSearch.Current.Value, this._fileListControl);
                    }

                    IoHelper.WriteToConsole("this._fileSearch.GetNext() == null");
                }
                catch (Exception ex)
                {
                    string dir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
                    System.IO.File.WriteAllText(dir + "err.txt", ex.Message);
                    IoHelper.WriteToConsole(ex.Message);
                }
            }

            private void CopyHandler()
            {
                if (this.OnFinishedHandler != null)
                {
                    Delegate[] delegates = this.OnFinishedHandler.GetInvocationList();
                    for (int index = 0; index < delegates.Length; index++)
                    {
                        EventHandler<ConcurrentFileSearchEventArgs> subscriber = (EventHandler<ConcurrentFileSearchEventArgs>)delegates[index];
                        if (subscriber != null)
                            this.OnFinishedHandler += subscriber;
                    }
                }
            }

            private void OnFinished(ConcurrentFileSearchEventArgs args)
            {
                EventHandler<ConcurrentFileSearchEventArgs> handler = this.OnFinishedHandler;
                if (handler == null)
                {
                    IoHelper.WriteToConsole("ERROR thread id #{0} no handler", this.ID);
                    return;
                }
                OnFinishedHandler(this, args);
            }

            private void AttachFileData(FileData file, FileListControl fileListControl)
            {
                IoHelper.WriteToConsole("requesting fileListControlLock");

                fileListControl.InvokeIfRequired(c =>
                {
                    c.Enabled = false;
                    //c.Clear();

                    FileToIconConverter iconConverter = new FileToIconConverter();
                    if (c.TreeImageList == null)
                        c.TreeImageList = new ImageList();
                    ImageList treeImageList = c.TreeImageList;
                    if (!treeImageList.Images.ContainsKey(UiHelper.DirectoryKey))
                        treeImageList.Images.Add(UiHelper.DirectoryKey, iconConverter.GetImage(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), IconSize.Small).ToBitmap());
                    if (!treeImageList.Images.ContainsKey(UiHelper.ZipExtension))
                        treeImageList.Images.Add(UiHelper.ZipExtension, iconConverter.GetImage(UiHelper.ZipExtension, IconSize.Small).ToBitmap());
                    if (!treeImageList.Images.ContainsKey(UiHelper.NoneFileExtension))
                        treeImageList.Images.Add(UiHelper.NoneFileExtension, iconConverter.GetImage(UiHelper.NoneFileExtension, IconSize.Small).ToBitmap());

                    ImageList.ImageCollection images = treeImageList.Images;
                    if (!images.ContainsKey(file.Extension))
                    {
                        Bitmap icon = iconConverter.GetImage(file.Path, IconSize.Small).ToBitmap();
                        images.Add(file.Extension, icon);
                    }

                    c.AddFileData(file);

                });

                IoHelper.WriteToConsole("released fileListControlLock");
            }
        }
    }

    public class ConcurrentFileSearchEventArgs : EventArgs
    {
        private IEnumerable<FileData> _files;

        public ConcurrentFileSearchEventArgs(IEnumerable<FileData> files)
        {
            this._files = files;
        }

        public IEnumerable<FileData> Files { get { return this._files; } private set { } }
    }
}
