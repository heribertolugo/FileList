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

        private static ConcurrentCollection<FileData> _fileData;
        private string _root;
        private FileListControl _fileListControl;
        private bool _commitRequired;

        public ConcurrentFileSearch(string rootPath, FileSearchWorkerArgs args)
        {
            this._root = rootPath;
            if (ConcurrentFileSearch._fileData == null)
                ConcurrentFileSearch._fileData = new ConcurrentCollection<FileData>();
            this._fileListControl = args.FileListControl;
            this._commitRequired = !args.LiveUpdate;
        }

        public void Start()
        {
            ConcurrentFileSearchSta searcher = new ConcurrentFileSearchSta(this._root);
            ConcurrentFileSearchSta.Files = ConcurrentFileSearch._fileData;

            Thread thread = new Thread(() =>
            {
                searcher.Start(this.ConcurrentFileSearch_OnFinishedHandler);
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }

        private void AttachFileData(IEnumerable<FileData> files, FileListControl fileListControl, bool commitRequired)
        {
            fileListControl.InvokeIfRequired(c =>
            {
                c.Enabled = false;
                c.Clear();

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

        private void ConcurrentFileSearch_OnFinishedHandler(object sender, ConcurrentFileSearchEventArgs e)
        {
            int bucketCount = ConcurrentFileSearchSta.ThreadBucketCount();
            //Console.WriteLine(bucketCount);
            if (bucketCount == 0)
            {
                this.AttachFileData(e.Files, this._fileListControl, this._commitRequired);
                this.FinalizeFileListControl(this._fileListControl, this._commitRequired); ;
                this.OnFinished(e);
            }
            else if (e.Files.Count() > 0)
            {
                this.AttachFileData(e.Files, this._fileListControl, this._commitRequired);
                this.OnUpdate(e);
            }
        }

        public void Cancel()
        {
            ConcurrentFileSearchSta.Cancel();
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


        private sealed class ConcurrentFileSearchSta
        {
            private event EventHandler<ConcurrentFileSearchEventArgs> OnFinishedHandler;

            private static ConcurrentCollection<int> ThreadBucket;
            public static ConcurrentCollection<FileData> Files;
            private FileSearch _fileSearch;
            private static int IdTracker;
            private static object IdLock;
            private readonly int ID;
            private string _root;
            private EventHandler<ConcurrentFileSearchEventArgs> _handler;
            private List<FileData> _files;

            static ConcurrentFileSearchSta()
            {
                ConcurrentFileSearchSta.IdLock = new object();
                ConcurrentFileSearchSta.IdTracker = int.MinValue;
                ConcurrentFileSearchSta.ThreadBucket = new ConcurrentCollection<int>();
                ConcurrentFileSearchSta.CancelLock = new object();
                ConcurrentFileSearchSta._isCancelled = false;
            }

            public ConcurrentFileSearchSta(string root)
            {
                this.ID = this.GetNextId();
                this._root = root;
                this._files = new List<FileData>();
            }

            public void Start(EventHandler<ConcurrentFileSearchEventArgs> onFinishedHandler)
            {
                if (ConcurrentFileSearchSta.IsCancelled())
                    return;
                this._handler = onFinishedHandler;
                if (onFinishedHandler != null)
                    this.OnFinishedHandler += (EventHandler<ConcurrentFileSearchEventArgs>)onFinishedHandler;
                ConcurrentFileSearchSta.ThreadBucket.Add(this.ID);
                //this.CopyHandler();
                this.SummonMinions(this._root);
                this.GetFiles(this._root, this._files);
                this.RmoveFromThreadBucket();
                this.OnFinished(new ConcurrentFileSearchEventArgs(this._files));
            }

            private void RmoveFromThreadBucket()
            {
                ConcurrentFileSearchSta.ThreadBucket.Remove(this.ID);
            }

            public static int ThreadBucketCount()
            {
                return ConcurrentFileSearchSta.ThreadBucket.Count;
            }

            private static bool _isCancelled;
            private static object CancelLock;
            public static void Cancel()
            {
                lock (ConcurrentFileSearchSta.CancelLock)
                {
                    ConcurrentFileSearchSta._isCancelled = true;
                }
            }

            private static bool IsCancelled()
            {
                lock (ConcurrentFileSearchSta.CancelLock)
                {
                    return ConcurrentFileSearchSta._isCancelled;
                }
            }


            private int GetNextId()
            {
                int id = 0;
                lock (ConcurrentFileSearchSta.IdLock)
                {
                    id = ConcurrentFileSearchSta.IdTracker;
                    if (ConcurrentFileSearchSta.IdTracker == int.MaxValue)
                        ConcurrentFileSearchSta.IdTracker = int.MinValue;
                    ++ConcurrentFileSearchSta.IdTracker;
                }

                return id;
            }

            private void SummonMinions(string root)
            {
                foreach (string directory in Directory.GetDirectories(root))
                {
                    ConcurrentFileSearchSta search = new ConcurrentFileSearchSta(directory);
                    Thread thread = new Thread(() => search.Start(this._handler));
                    thread.SetApartmentState(ApartmentState.STA);
                    thread.Start();
                }
            }

            private void GetFiles(string root, IList<FileData> files)
            {
                this._fileSearch = new FileSearch(root);

                while (this._fileSearch.GetNext() != null)
                {
                    //ConcurrentFileSearchSta.Files.Add(this._fileSearch.Current.Value);
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
                    return;
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
