using Common.Models;
using System.Collections.Generic;
using System.Windows.Forms;

namespace FilePreview.UnknownFiles
{
    public class UnknownPreview : Common.Models.IPreviewFile
    {
        public IEnumerable<string> Extensions { get { return new string[] { string.Empty, "*" }; } }

        public Control Viewer { get { return new Control(); } }

        public FileType FileType
        {
            get { return FileType.Unknown; }
        }

        public bool Load(string path)
        {
            return false;
        }

        public bool Load(FileData path)
        {
            return false;
        }
    }
}
