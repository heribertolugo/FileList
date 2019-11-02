using Common.Models;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace FilePreview.BrowseFiles
{
    public class ZipBrowserPreview : Common.Models.IPreviewFile
    {
        private List<string> _extensions;
        public ZipBrowserPreview()
        {
            this.Viewer = new ZipBrowserControl();
            this._extensions = new List<string>(Common.Models.ZipExtractor.SevenZipFormat.ZipExtensions);
        }


        public IEnumerable<string> Extensions
        {
            get { return this._extensions; }
        }

        public Control Viewer { get; private set; }

        public FileType FileType
        {
            get { return FileType.Browsable | FileType.Folder | FileType.Zip; }
        }

        public bool LoadFile(string path)
        {
            try
            {
                this.Clear();
                return (this.Viewer as ZipBrowserControl).DisplayBrowsablePreview(path);
            }
            catch (Exception ex) { }
            return false;
        }

        public bool LoadFile(FileData path)
        {
            try
            {
                this.Clear();
                return (this.Viewer as ZipBrowserControl).DisplayBrowsablePreview(path.Path);
            }
            catch (Exception ex) { }
            return false;
        }

        public void Clear()
        {
            (this.Viewer as ZipBrowserControl).Clear();
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

        ~ZipBrowserPreview()
        {
            this.Dispose(false);
        }
    }
}
