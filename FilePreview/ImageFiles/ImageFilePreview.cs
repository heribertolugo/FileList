using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common.Models;

namespace FilePreview.ImageFiles
{
    public class ImageFilePreview : Common.Models.IPreviewFile
    {

        public ImageFilePreview()
        {
            this.Viewer = new ImagePreviewControl();
        }

        public IEnumerable<string> Extensions
        {
            get
            {
                return new string[] { ".gif", ".jpg", ".jpeg", ".png", ".ico", ".bmp" };
            }
        }

        public Control Viewer { get; private set; }

        public FileType FileType
        {
            get { return FileType.Image; }
        }

        public bool LoadFile(string path)
        {
            try
            {
                ImagePreviewControl viewer = this.Viewer as ImagePreviewControl;

                return viewer.SetImage(path);
            }catch(Exception ex)
            {
                return false;
            }
        }

        public bool LoadFile(FileData path)
        {
            return this.LoadFile(path.Path);
        }

        public void Clear()
        {
            (this.Viewer as ImagePreviewControl).Clear();
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

        ~ImageFilePreview()
        {
            this.Dispose(false);
        }
    }
}
