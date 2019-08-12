using System.Collections.Generic;
using System.Linq;

namespace FileList.Models
{
    public struct FileDataGroup
    {
        private string _parentPath;
        private List<FileData> _fileDatas;

        public FileDataGroup(string parent, IEnumerable<FileData> children)
        {
            this._fileDatas = new List<FileData>();
            this._parentPath = parent;
            this._fileDatas.AddRange(children.ToArray());
        }

        public string ParentPath
        {
            get
            {
                return this._parentPath;
            }
            private set
            {
            }
        }

        public FileData[] FileData
        {
            get
            {
                return this._fileDatas.ToArray();
            }
            private set
            {
            }
        }
    }
}