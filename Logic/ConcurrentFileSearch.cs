using FileList.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace FileList.Logic
{
    internal sealed class ConcurrentFileSearch
    {
        public event EventHandler OnFinishedHandler;

        private static ConcurrentCollection<FileData> _fileData;
        private string _root;

        public ConcurrentFileSearch(string rootPath)
        {
            this._root = rootPath;
            if (ConcurrentFileSearch._fileData == null)
                ConcurrentFileSearch._fileData = new ConcurrentCollection<FileData>();
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

        private void ConcurrentFileSearch_OnFinishedHandler(object sender, EventArgs e)
        {
            int bucketCount = ConcurrentFileSearchSta.ThreadBucketCount();
            Console.WriteLine(bucketCount);
            if (bucketCount == 0)
                this.OnFinished(EventArgs.Empty);
        }

        public void Cancel()
        {
            ConcurrentFileSearchSta.Cancel();
        }

        private void OnFinished(EventArgs args)
        {
            EventHandler handler = this.OnFinishedHandler;
            if (handler == null)
                return;
            OnFinishedHandler(this, args);
        }


        private sealed class ConcurrentFileSearchSta
        {
            private event EventHandler OnFinishedHandler;

            private static ConcurrentCollection<int> ThreadBucket;
            public static ConcurrentCollection<FileData> Files;

            private static ConcurrentCollection<string> Directories;
            private FileSearch _fileSearch;
            private static int IdTracker;
            private static object IdLock;
            private readonly int ID;
            private string _root;
            private EventHandler _handler;

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
            }

            public void Start(EventHandler onFinishedHandler)
            {
                if (ConcurrentFileSearchSta.IsCancelled())
                    return;
                this._handler = onFinishedHandler;
                if (onFinishedHandler != null)
                    this.OnFinishedHandler += (EventHandler)onFinishedHandler;
                ConcurrentFileSearchSta.ThreadBucket.Add(this.ID);
                //this.CopyHandler();
                this.SummonMinions(this._root);
                this.GetFiles(this._root);
                this.RmoveFromThreadBucket();
                this.OnFinished(EventArgs.Empty);
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

            private void GetFiles(string root)
            {
                this._fileSearch = new FileSearch(root);

                while (this._fileSearch.GetNext() != null)
                {
                    ConcurrentFileSearchSta.Files.Add(this._fileSearch.Current.Value);
                    Console.WriteLine(this._fileSearch.Current.Value.Path);
                }
            }

            private void CopyHandler()
            {
                if (this.OnFinishedHandler != null)
                {
                    Delegate[] delegates = this.OnFinishedHandler.GetInvocationList();
                    for (int index = 0; index < delegates.Length; index++)
                    {
                        EventHandler subscriber = (EventHandler)delegates[index];
                        if (subscriber != null)
                            this.OnFinishedHandler += subscriber;
                    }
                }
            }

            private void OnFinished(EventArgs args)
            {
                EventHandler handler = this.OnFinishedHandler;
                if (handler == null)
                    return;
                OnFinishedHandler(this, args);
            }
        }
    }

    
}
