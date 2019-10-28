using Common.Extensions;
using Common.Helpers;
using Common.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace FilePreview.BrowseFiles
{
    public class FileBrowserPreview : Common.Models.IPreviewFile
    {

        public FileBrowserPreview()
        {
            this.Viewer = new FileBrowserControl();
        }


        public IEnumerable<string> Extensions
        {
            get { return new string[] { "\\", string.Empty }; }
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
                return (this.Viewer as FileBrowserControl).DisplayBrowsablePreview(string.IsNullOrWhiteSpace(path) ? null : (FileData?)(new FileData(path)));
            }
            catch (Exception) { }
            return false;
        }

        public bool LoadFile(FileData path)
        {
            try
            {
                this.Clear();
                return (this.Viewer as FileBrowserControl).DisplayBrowsablePreview((FileData?)path);
            }
            catch (Exception ex) { }
            return false;
        }

        public void Clear()
        {
            (this.Viewer as FileBrowserControl).Clear();
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

        ~FileBrowserPreview()
        {
            this.Dispose(false);
        }
    }
}
