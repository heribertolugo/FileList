using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FileList.Models
{
    public struct FileSearchWorkerArgs
    {
        private FileListControl _fileListControl;
        private string _path;
        private bool _liveUpdate;

        public FileSearchWorkerArgs(string path, FileListControl fileListControl, bool liveUpdate)
        {
            this._path = path;
            this._fileListControl = fileListControl;
            this._liveUpdate = liveUpdate;
        }

        public string Path { get { return this._path; } private set { } }
        public FileListControl FileListControl { get { return this._fileListControl; } private set { } }
        public bool LiveUpdate { get { return this._liveUpdate; } private set { } }
    }
}
