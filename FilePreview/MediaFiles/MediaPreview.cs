using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common.Models;
using Declarations;
using Declarations.Media;
using Declarations.Players;

namespace FilePreview.MediaFiles
{
    public class MediaPreview : Common.Models.IPreviewFile
    {
        public MediaPreview()
        {
            this.Viewer = new MediaPlayerControl();
        }

        public IEnumerable<string> Extensions
        {
            get { return new string[] { ".mp3", ".mp4", ".avi", ".flv", ".mpeg", ".aif", ".cda", ".mid", ".midi", ".mpa", ".ogg", ".wav", ".wma", ".wpl", ".3g2", ".3gp", ".h264", ".m4v", ".mkv", ".mov", ".rm", ".swf", ".vob", ".wmv" }; }
        }

        public Control Viewer
        {
            get; private set;
        }

        public FileType FileType
        {
            get { return FileType.Media; }
        }

        public bool LoadFile(string path)
        {
            (this.Viewer as MediaPlayerControl).Play(path);
            return true;
        }

        public bool LoadFile(FileData path)
        {
            (this.Viewer as MediaPlayerControl).Play(path.Path);
            return true;
        }

        public void Clear()
        {
            (this.Viewer as MediaPlayerControl).Reset();
        }

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    this.Viewer.Dispose();
                }

                this._disposed = true;
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~MediaPreview()
        {
            this.Dispose(false);
        }
    }
}
