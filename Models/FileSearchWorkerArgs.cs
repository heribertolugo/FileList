using System.Threading;

namespace FileList.Models
{
    public struct FileSearchWorkerArgs
    {
        private FileListControl _fileListControl;
        private string _path;
        private bool _liveUpdate;
        private CancellationToken _cancel;

        public FileSearchWorkerArgs(string path, FileListControl fileListControl, bool liveUpdate, CancellationToken token)
        {
            this._path = path;
            this._fileListControl = fileListControl;
            this._liveUpdate = liveUpdate;
            this._cancel = token;
        }

        public string Path { get { return this._path; } private set { } }
        public FileListControl FileListControl { get { return this._fileListControl; } private set { } }
        public bool LiveUpdate { get { return this._liveUpdate; } private set { } }
        public CancellationToken CancellationToken { get { return this._cancel; } private set { } }
    }
}
