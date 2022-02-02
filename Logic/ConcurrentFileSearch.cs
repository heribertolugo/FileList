using Common.Extensions;
using Common.Helpers;
using Common.Models;
using FileList.Models;
using FileList.Views;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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
        private SearchOption _validator;

        private static object fileListControlLock = new object();
        private static object errFileLock = new object();

        public ConcurrentFileSearch(string rootPath, FileSearchWorkerArgs args)
        {
            this._root = rootPath;
            if (ConcurrentFileSearch._fileData == null)
                ConcurrentFileSearch._fileData = new ConcurrentCollection<FileData>("ConcurrentFileSearch fileData");
            this._fileListControl = args.FileListControl;
            this._cancelToken = args.CancellationToken;
            this._validator = args.Filter;
            this._cancelToken.Register(() => ConcurrentFileSearchMinion.Cancel());
        }

        public void Start()
        {
            this.MaxThreads = Math.Max(1, this._fileListControl.SearcherThreads);
            this.startTime = DateTime.Now;
            Shell32.ShellClass shell = new Shell32.ShellClass();
            ole32.GetRegisteredInterfaceMarshalPtr<Shell32.IShellDispatch5>(shell);
            GC.KeepAlive(shell);

            ConcurrentFileSearch.Directories.Add(this._root);
            this._fileListControl.InvokeIfRequired(f =>
            {
                f.ThreadCounter = 0;
                InitializeFileListControlImageList(f);
            });
            this.SummonMinions(this.MaxThreads);
            //thread.Join();
        }

        private void FinalizeFileListControl(FileListControl fileListControl)
        {
            fileListControl.InvokeIfRequired(c =>
            {
                c.ThreadCounter = 0;
                c.HideNotification();
                //c.ExpandTree();
                c.ScrollTreeToTop();
                //c.FileTypeListSorted = true;
                c.Enabled = true;
            });
        }

        private void ConcurrentFileSearch_OnFinishedHandler(object sender, ConcurrentFileSearchEventArgs e)
        {
            bool minionsSummoned = false;
            int minionCount = System.Threading.Interlocked.Decrement(ref MinionCount);
            ConcurrentFileSearchMinion searchSta = (ConcurrentFileSearchMinion)sender;
            searchSta.OnFinishedHandler -= this.ConcurrentFileSearch_OnFinishedHandler;

            //lock (SummonMinionLock)
            //{
            if (!this._cancelToken.IsCancellationRequested)
                minionsSummoned = this.SummonMinions(this.MaxThreads);
            else
                ConcurrentFileSearchMinion.Cancel();

            //int bucketCount = ConcurrentFileSearchMinion.ThreadBucketCount();
            //this._fileListControl.InvokeIfRequired(c => c.ThreadCounter = bucketCount);
            //minionCount = GetMinionCount();
            IoHelper.WriteToConsole("threads {0} #{1}", minionCount, e.Id);

            if ((ConcurrentFileSearch.Directories.Count == 0 || this._cancelToken.IsCancellationRequested) && GetMinionCount() == 0 && !minionsSummoned) //ConcurrentFileSearchMinion.ThreadBucketCount() == 0
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
            //}
        }

        private int GetMinionCount()
        {
            return Interlocked.CompareExchange(ref MinionCount, MinionCount, 0);
        }

        private static volatile int MinionCount = 0;
        private bool SummonMinions(int maxThreads)
        {
            bool summoned = false;
            string directory = null;
            int bucketCount = 0;
            string message = string.Empty;

            if ((bucketCount = GetMinionCount()) >= maxThreads) //ConcurrentFileSearchMinion.ThreadBucketCount()
                return false;

            message = string.Format("{0} directories to process", ConcurrentFileSearch.Directories.Count);
            IoHelper.WriteToConsole(message);
            //this._fileListControl.InvokeIfRequired(f => f.ShowNotification(message));

            while ((directory = ConcurrentFileSearch.Directories.Take()) != null && !this._cancelToken.IsCancellationRequested)
            {
                System.Threading.Interlocked.Increment(ref MinionCount);
                ConcurrentFileSearchMinion searcher = new ConcurrentFileSearchMinion(directory, this.ConcurrentFileSearch_OnFinishedHandler, this._fileListControl, this._validator);
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
                IoHelper.WriteToConsole("Created thread {0}", searcher.ID);
                //thread.Join(); 
                bucketCount++;
                //this._fileListControl.InvokeIfRequired(f => f.SearcherThreads += 1);
                //this._fileListControl.InvokeIfRequired(f => f.ThreadCounter = bucketCount);
                summoned = true;
                if (bucketCount >= maxThreads || (bucketCount = GetMinionCount()) >= maxThreads)
                    break;
            }

            return summoned;
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

        /// <summary>
        /// Inserts common icons into FileListControl if they do not currently exist.
        /// </summary>
        /// <param name="fileListControl"></param>
        private void InitializeFileListControlImageList(FileListControl fileListControl)
        {
            FileToIconConverter iconConverter = new FileToIconConverter();
            if (fileListControl.TreeImageList == null)
                fileListControl.TreeImageList = new ImageList();
            ImageList treeImageList = fileListControl.TreeImageList;
            if (!treeImageList.Images.ContainsKey(UiHelper.DirectoryKey))
                treeImageList.Images.Add(UiHelper.DirectoryKey, iconConverter.GetImage(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), IconSize.Small).ToBitmap());
            if (!treeImageList.Images.ContainsKey(UiHelper.ZipExtension))
                treeImageList.Images.Add(UiHelper.ZipExtension, iconConverter.GetImage(UiHelper.ZipExtension, IconSize.Small).ToBitmap());
            if (!treeImageList.Images.ContainsKey(UiHelper.NoneFileExtension))
                treeImageList.Images.Add(UiHelper.NoneFileExtension, iconConverter.GetImage(UiHelper.NoneFileExtension, IconSize.Small).ToBitmap());
        }

        private struct ConcurrentFileSearchMinion
        {
            internal event EventHandler<ConcurrentFileSearchEventArgs> OnFinishedHandler;

            private FileSearch _fileSearch;
            private static int IdTracker;
            private static object IdLock;
            public readonly int ID;
            private string _root;
            private EventHandler<ConcurrentFileSearchEventArgs> _handler;
            private static object StaticLock = new object();
            private static volatile int _bucketCount = 0;
            private SearchOption _validator;
            private FileListControl _fileListControl;
            private static bool _isCancelled;
            private static object CancelLock;
            private static object fileListControlLock = new object();

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
            public ConcurrentFileSearchMinion(string root, EventHandler<ConcurrentFileSearchEventArgs> onFinishedHandler, FileListControl fileListControl, SearchOption validator)
            {
                System.Threading.Interlocked.Increment(ref _bucketCount);
                this._root = root;
                this._fileSearch = null;
                this._handler = onFinishedHandler;
                this.ID = 0;
                this.OnFinishedHandler = null;
                this._validator = validator;

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
                    this.GetFiles(this._root);
                }

                this.RmoveFromThreadBucket();
                this.OnFinished(new ConcurrentFileSearchEventArgs(null, this.ID.ToString()));
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

            private void AddDirectories(string path, ConcurrentQueue<string> directories)
            {
                IoHelper.WriteToConsole("thread id #{0} adding dirs in {1}", this.ID, path);
                directories.AddRange(IoHelper.AccessableDirectories(path));
            }

            private void GetFiles(string root)
            {
                try
                {
                    this._fileSearch = new FileSearch(root, false, ole32.GetIShellDispatch5());
                    FileData? fileData = this._fileSearch.GetNext(); // make sure we have files so we don't lock up UI unnecessarily

                    if (!fileData.HasValue)
                        return;
                    IoHelper.WriteToConsole("requesting fileListControlLock");

                    //lock (fileListControlLock)
                    //{
                        int bucketCount = ConcurrentFileSearchMinion.ThreadBucketCount();
                        var that = this;
                        this._fileListControl.InvokeIfRequired(f =>
                        {
                            f.ThreadCounter = bucketCount;
                            // access thread unsafe ConcurrentFileSearchMinion._isCancelled
                            // its ok because its not a big deal to get incorrect value right away
                            // if thread access exception is thrown, it is because value is changing.
                            // we catch the error, forcing us to quit.
                            while (fileData.HasValue && !ConcurrentFileSearchMinion._isCancelled)
                            {
                                FileData currentFile = that._fileSearch.Current.Value;
                                string message = string.Format("thread id #{0} found file {1}", that.ID, currentFile.Path);

                                IoHelper.WriteToConsole(message);
                                f.ShowNotification(message);

                                // check our file filter to see if we want this file
                                if (that._validator.ValidateFile(currentFile))
                                {
                                    that.AttachFileData(currentFile, f);
                                }
                                else
                                {
                                    message = string.Format("skipping filtered file: {0}", currentFile.Path);
                                    IoHelper.WriteToConsole(message);

                                    f.ShowNotification(message);
                                }

                                fileData = that._fileSearch.GetNext();
                            }
                            IoHelper.WriteToConsole("this._fileSearch.GetNext() == null");
                        });
                    //}
                    IoHelper.WriteToConsole("released fileListControlLock");
                }
                catch (Exception ex)
                {
                    string dir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
                    lock (errFileLock)
                    {
                        System.IO.File.WriteAllText(System.IO.Path.Combine(dir, "err.txt"), ex.Message);
                    }
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
            /// <summary>
            /// Finds the icon associated with the file and then attaches the icon and file to the FileListControl
            /// </summary>
            /// <param name="file"></param>
            /// <param name="fileListControl"></param>
            private void AttachFileData(FileData file, FileListControl fileListControl)
            {
                FileToIconConverter iconConverter = new FileToIconConverter();
                ImageList treeImageList = fileListControl.TreeImageList;
                ImageList.ImageCollection images = treeImageList.Images;

                if (!images.ContainsKey(file.Extension))
                {
                    Bitmap icon = iconConverter.GetImage(file.Path, IconSize.Small).ToBitmap();
                    images.Add(file.Extension, icon);
                }

                fileListControl.AddFileData(file);
            }
        }
    }

    public class ConcurrentFileSearchEventArgs : EventArgs
    {
        private IEnumerable<FileData> _files;

        public ConcurrentFileSearchEventArgs(IEnumerable<FileData> files, string id)
        {
            this._files = files;
        }

        public IEnumerable<FileData> Files { get { return this._files; } private set { } }
        public string Id { get; private set; }
    }
}
