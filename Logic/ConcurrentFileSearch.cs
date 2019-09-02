using FileList.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace FileList.Logic
{
    internal sealed class ConcurrentFileSearch
    {
        public event EventHandler<ConcurrentFileSearchEventArgs> OnFinishedHandler;
        public event EventHandler<ConcurrentFileSearchEventArgs> OnUpdateHandler;

        private static volatile ConcurrentCollection<FileData> _fileData;
        //private static volatile ConcurrentCollection<string> _directories;
        private string _root;
        private FileListControl _fileListControl;
        private bool _commitRequired;

        public ConcurrentFileSearch(string rootPath, FileSearchWorkerArgs args)
        {
            this._root = rootPath;
            if (ConcurrentFileSearch._fileData == null)
                ConcurrentFileSearch._fileData = new ConcurrentCollection<FileData>("ConcurrentFileSearch fileData");
            this._fileListControl = args.FileListControl;
            this._commitRequired = true;// !args.LiveUpdate;
        }

        public void Start()
        {
            //ConcurrentFileSearchSta.Files = ConcurrentFileSearch._fileData;
            Shell32.ShellClass shell = new Shell32.ShellClass();
            //ConcurrentFileSearchSta._shellPtr = 
            Models.Win32.Win32Methods.GetRegisteredInterfaceMarshalPtr<Shell32.IShellDispatch5>(shell); //.ToIntPtr();
            GC.KeepAlive(shell);

            Thread thread = new Thread(() =>
            {
                ConcurrentFileSearchMinion searcher = new ConcurrentFileSearchMinion(this._root, this.ConcurrentFileSearch_OnFinishedHandler);
                searcher.Start();
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }

        private static volatile ConcurrentCollection<string> Directories = new ConcurrentCollection<string>("ConcurrentFileSearch Directories");
        private void ProcessDirecory(string path)
        {

        }

        private static object fileListControlLock = new object();
        private void AttachFileData(IEnumerable<FileData> files, FileListControl fileListControl, bool commitRequired)
        {
            Console.WriteLine("requesting fileListControlLock");
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
                        treeImageList.Images.Add(UiHelper.DirectoryKey, iconConverter.GetImage(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), FileToIconConverter.IconSize.Small).ToBitmap());
                    if (!treeImageList.Images.ContainsKey(UiHelper.ZipExtension))
                        treeImageList.Images.Add(UiHelper.ZipExtension, iconConverter.GetImage(UiHelper.ZipExtension, FileToIconConverter.IconSize.Small).ToBitmap());
                    if (!treeImageList.Images.ContainsKey(UiHelper.NoneFileExtension))
                        treeImageList.Images.Add(UiHelper.NoneFileExtension, iconConverter.GetImage(UiHelper.NoneFileExtension, FileToIconConverter.IconSize.Small).ToBitmap());

                    foreach (FileData file in files)
                    {
                        ImageList.ImageCollection images = treeImageList.Images;
                        if (!images.ContainsKey(file.Extension))
                        {
                            Bitmap icon = iconConverter.GetImage(file.Path, FileToIconConverter.IconSize.Small).ToBitmap();
                            images.Add(file.Extension, icon);
                        }

                        c.AddFileData(file, commitRequired);
                    }
                });
            }
            Console.WriteLine("released fileListControlLock");
        }

        private void FinalizeFileListControl(FileListControl fileListControl, bool commitRequired)
        {
            fileListControl.InvokeIfRequired(c =>
            {
                if (commitRequired)
                    c.Commit();

                c.ExpandTree();
                c.ScrollTreeToTop();
                c.FileTypeListSorted = true;
                c.Enabled = true;
            });
        }

        private static object locker = new object();

        private void ConcurrentFileSearch_OnFinishedHandler(object sender, ConcurrentFileSearchEventArgs e)
        {
            ConcurrentFileSearchMinion searchSta = (ConcurrentFileSearchMinion)sender;
            searchSta.OnFinishedHandler -= this.ConcurrentFileSearch_OnFinishedHandler;
            int maxThreads = 50;

            this.SummonMinions(maxThreads);

            //lock (locker)
            //{
            int bucketCount = ConcurrentFileSearchMinion.ThreadBucketCount(); //ConcurrentFileSearch.Directories.Count; // 
            this._fileListControl.InvokeIfRequired(c => c.scoutCountLabel.Text = bucketCount.ToString());
            Console.WriteLine("threads {0}", bucketCount);

            if (e.Files.Count() > 0)
            {
                this.AttachFileData(e.Files, this._fileListControl, this._commitRequired);
                this.OnUpdate(e);
            }

            if (ConcurrentFileSearch.Directories.Count == 0 && ConcurrentFileSearchMinion.ThreadBucketCount() == 0)
            {
                Console.WriteLine("preparing to finalize");
                this.FinalizeFileListControl(this._fileListControl, this._commitRequired);
                this.OnFinished(e);
                GC.Collect();
            }
        }
        //private void ConcurrentFileSearch_OnFinishedHandler(object sender, ConcurrentFileSearchEventArgs e)
        //{
        //    ConcurrentFileSearchSta searchSta = (ConcurrentFileSearchSta)sender;
        //    searchSta.OnFinishedHandler -= this.ConcurrentFileSearch_OnFinishedHandler;
        //    int maxThreads = 50;

        //    //lock (locker)
        //    //{
        //    int bucketCount = ConcurrentFileSearchSta.ThreadBucketCount(); //ConcurrentFileSearch.Directories.Count; // 
        //    this._fileListControl.InvokeIfRequired(c => c.scoutCountLabel.Text = bucketCount.ToString());
        //    Console.WriteLine("threads {0}", bucketCount);
        //    if (bucketCount == 0)
        //    {
        //        //string[] directories = ConcurrentFileSearch.Directories.TakeAll();
        //        //foreach (string directory in directories)
        //        //{

        //        //    Thread thread = new Thread(() =>
        //        //    {
        //        //        ConcurrentFileSearchSta searcher = new ConcurrentFileSearchSta(directory);
        //        //        searcher.Start(this.ConcurrentFileSearch_OnFinishedHandler);
        //        //    });
        //        //    thread.SetApartmentState(ApartmentState.STA);
        //        //    thread.Start();
        //        //}

        //        string directory = null;
        //        //if (System.Diagnostics.Process.GetCurrentProcess().Threads.Count < 100)
        //        //if (ConcurrentFileSearchSta.ThreadBucketCount() < maxThreads)
        //        while ((directory = ConcurrentFileSearch.Directories.Take()) != null)
        //        {
        //            ConcurrentFileSearchSta searcher = new ConcurrentFileSearchSta(directory, this.ConcurrentFileSearch_OnFinishedHandler);
        //            Thread thread = new Thread((object o) =>
        //                {
        //                    //string localDir = o?.ToString();
        //                    //ConcurrentFileSearchSta searcher = new ConcurrentFileSearchSta(localDir, this.ConcurrentFileSearch_OnFinishedHandler);
        //                    //searcher.Start();
        //                    ((ConcurrentFileSearchSta)o).Start();

        //                });
        //            thread.SetApartmentState(ApartmentState.STA);
        //            thread.IsBackground = true;
        //            thread.Start(searcher);

        //            Console.WriteLine("Created thread");
        //            if ((bucketCount = ConcurrentFileSearchSta.ThreadBucketCount()) > maxThreads)
        //                break;
        //        }
        //        //lock (locker)
        //        //{
        //        if (e.Files.Count() > 0)
        //            this.AttachFileData(e.Files, this._fileListControl, this._commitRequired);
        //        //}
        //        //bucketCount = ConcurrentFileSearchSta.ThreadBucketCount();
        //        //Console.WriteLine("*" + bucketCount);
        //        if (bucketCount == 0 && ConcurrentFileSearch.Directories.Count == 0)
        //        {
        //            this.FinalizeFileListControl(this._fileListControl, this._commitRequired);
        //            this.OnFinished(e);
        //            GC.Collect();
        //        }
        //    }
        //    else// if (e.Files.Count() > 0)
        //    {
        //        //Console.WriteLine("*");
        //        //lock (locker)
        //        //{
        //        if (e.Files.Count() > 0)
        //            this.AttachFileData(e.Files, this._fileListControl, this._commitRequired);
        //        //}
        //        this.OnUpdate(e);
        //    }
        //    //}
        //}

        //private void ConcurrentFileSearch_OnFinishedHandler(object sender, ConcurrentFileSearchEventArgs e)
        //{
        //    int bucketCount = ConcurrentFileSearchSta.ThreadBucketCount();
        //    Console.WriteLine(bucketCount);
        //    if (bucketCount == 0)
        //    {
        //        this.AttachFileData(e.Files, this._fileListControl, this._commitRequired);
        //        this.FinalizeFileListControl(this._fileListControl, this._commitRequired); ;
        //        this.OnFinished(e);
        //    }
        //    else if (e.Files.Count() > 0)
        //    {
        //        this.AttachFileData(e.Files, this._fileListControl, this._commitRequired);
        //        this.OnUpdate(e);
        //    }
        //}

        private void SummonMinions(int maxThreads)
        {
            string directory = null;
            int bucketCount = 0;

            Console.WriteLine("{0} directories to process", ConcurrentFileSearch.Directories.Count);

            if ((bucketCount = ConcurrentFileSearchMinion.ThreadBucketCount()) > maxThreads)
                return;

            while ((directory = ConcurrentFileSearch.Directories.Take()) != null)
            {
                ConcurrentFileSearchMinion searcher = new ConcurrentFileSearchMinion(directory, this.ConcurrentFileSearch_OnFinishedHandler);
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

                Console.WriteLine("Created thread");
                if ((bucketCount = ConcurrentFileSearchMinion.ThreadBucketCount()) > maxThreads)
                    break;
            }
        }

        public void Cancel()
        {
            ConcurrentFileSearchMinion.Cancel();
        }

        private void OnFinished(ConcurrentFileSearchEventArgs args)
        {
            EventHandler<ConcurrentFileSearchEventArgs> handler = this.OnFinishedHandler;
            if (handler == null)
                return;
            this.OnFinishedHandler(this, args);
        }

        private void OnUpdate(ConcurrentFileSearchEventArgs args)
        {
            EventHandler<ConcurrentFileSearchEventArgs> handler = this.OnUpdateHandler;
            if (handler == null)
                return;
            this.OnUpdateHandler(this, args);
        }


        private struct ConcurrentFileSearchMinion
        {
            internal event EventHandler<ConcurrentFileSearchEventArgs> OnFinishedHandler;

            private static ConcurrentCollection<int> ThreadBucket;
            //public static ConcurrentCollection<FileData> Files;
            private FileSearch _fileSearch;
            private static int IdTracker;
            private static object IdLock;
            private readonly int ID;
            private string _root;
            private EventHandler<ConcurrentFileSearchEventArgs> _handler;
            private List<FileData> _files;
            private static object StaticLock = new object();
            //private static IntPtr _shellPtr;
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
                    ConcurrentFileSearchMinion.ThreadBucket = new ConcurrentCollection<int>("ConcurrentFileSearchSta ThreadBucket");
                    ConcurrentFileSearchMinion.CancelLock = new object();
                    ConcurrentFileSearchMinion._isCancelled = false;
                }
            }

            public ConcurrentFileSearchMinion(string root, EventHandler<ConcurrentFileSearchEventArgs> onFinishedHandler)
            {
                this._root = root;
                this._files = new List<FileData>();
                this._fileSearch = null;
                this._handler = onFinishedHandler;
                this.ID = 0;
                this.OnFinishedHandler = null;

                this.OnFinishedHandler += this._handler;
                this.ID = this.GetNextId();
                //ConcurrentFileSearch.Directories.Remove(this._root);
                int bc = 0;
                Console.WriteLine("requesting bucketcount lock @ ConcurrentFileSearchSta");
                ConcurrentFileSearchMinion.ThreadBucket.Add(this.ID, out bc);

                System.Threading.Interlocked.Increment(ref _bucketCount);

                //lock (BucketCountLock)
                //{
                //    ++_bucketCount;
                //}
                Console.WriteLine("released bucketcount lock @ ConcurrentFileSearchSta");
            }

            [STAThread]
            public void Start()
            {
                if (ConcurrentFileSearchMinion.IsCancelled())
                    return;

                //if (ConcurrentFileSearchSta._shellPtr == default(IntPtr))
                //{
                //}
                //this._handler = onFinishedHandler;
                //if (onFinishedHandler != null)
                //    this.OnFinishedHandler += (EventHandler<ConcurrentFileSearchEventArgs>)onFinishedHandler;
                //ConcurrentFileSearchSta.ThreadBucket.Add(this.ID);
                //this.CopyHandler();
                this.GetFiles(this._root, this._files);
                //this.SummonMinions(this._root);
                this.AddDirectories(this._root, ConcurrentFileSearch.Directories);
                FileData[] files = this._files.ToArray();
                this._files.Clear();
                this._files = null;
                this.RmoveFromThreadBucket();
                this.OnFinished(new ConcurrentFileSearchEventArgs(files));
                //GC.Collect();
                //Thread.CurrentThread.Abort();
            }

            private void RmoveFromThreadBucket()
            {
                Console.WriteLine("removing thread id #{0}", this.ID);
                ConcurrentFileSearchMinion.ThreadBucket.Remove(this.ID);
                Console.WriteLine("requesting bucketcount lock @ RmoveFromThreadBucket");
                System.Threading.Interlocked.Decrement(ref _bucketCount);
                //lock (BucketCountLock)
                //{
                //    --_bucketCount;
                //}
                Console.WriteLine("released bucketcount lock @ RmoveFromThreadBucket");
            }

            public static int ThreadBucketCount()
            {
                int c = 0;
                Console.WriteLine("requesting bucketcount lock @ ThreadBucketCount");
                c = Interlocked.CompareExchange(ref _bucketCount, 0, 0);
                //lock (BucketCountLock)
                //{
                //    c = _bucketCount;
                //}
                Console.WriteLine("released bucketcount lock @ ThreadBucketCount with {0}", c);
                return c;
                //return ConcurrentFileSearchSta.ThreadBucket.Count;
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
                Console.WriteLine("requesting IdLock");
                lock (ConcurrentFileSearchMinion.IdLock)
                {
                    id = ConcurrentFileSearchMinion.IdTracker;
                    if (ConcurrentFileSearchMinion.IdTracker == int.MaxValue)
                        ConcurrentFileSearchMinion.IdTracker = int.MinValue;
                    ++ConcurrentFileSearchMinion.IdTracker;
                }
                Console.WriteLine("IdLock released");

                return id;
            }

            private static object minionLock = new object();
            private void SummonMinions(string root)
            {
                try
                {
                    //lock (minionLock)
                    //{
                    foreach (string directory in Directory.GetDirectories(root))
                    {
                        ConcurrentFileSearchMinion search = new ConcurrentFileSearchMinion(directory, this._handler);
                        Thread thread = new Thread(() => search.Start());
                        thread.SetApartmentState(ApartmentState.STA);
                        thread.Start();
                    }
                    //}
                }
                catch (Exception ex)
                {

                }
            }

            private void AddDirectories(string path, ConcurrentCollection<string> directories)
            {
                //foreach (string directory in Extensions.AccessableDirectories(path))
                //{
                //    directories.Add(directory);
                //    Console.WriteLine("thread id #{0} added dir {1}", this.ID, directory);
                //}

                Console.WriteLine("thread id #{0} adding dirs in {1}", this.ID, path);
                directories.AddRange(Extensions.AccessableDirectories(path));
            }

            private void GetFiles(string root, IList<FileData> files)
            {
                this._fileSearch = new FileSearch(root, false, Models.Win32.Win32Methods.GetIShellDispatch5());

                while (this._fileSearch.GetNext() != null)
                {
                    //ConcurrentFileSearchSta.Files.Add(this._fileSearch.Current.Value);
                    Console.WriteLine("thread id #{0} found file {1}", this.ID, this._fileSearch.Current.Value.Path);
                    files.Add(this._fileSearch.Current.Value);
                    //Console.WriteLine(this._fileSearch.Current.Value.Path);
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
                    Console.WriteLine("ERROR thread id #{0} no handler", this.ID);
                    return;
                }
                OnFinishedHandler(this, args);
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
