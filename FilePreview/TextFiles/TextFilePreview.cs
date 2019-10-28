using Common.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace FilePreview.TextFiles
{
    public class TextFilePreview : Common.Models.IPreviewFile
    {
        public TextFilePreview()
        {
            this.Viewer = new RichTextBox() { ReadOnly = true };
            this.Viewer.BackColor = Color.Black;
            this.Viewer.ForeColor = Color.Black;
            this.Viewer.BackColor = Color.White;
        }

        public IEnumerable<string> Extensions
        {
            get
            {
                return new string[] { ".txt", ".rtf" };
            }
        }

        public Common.Models.FileType FileType
        {
            get { return Common.Models.FileType.Text; }
        }

        public System.Windows.Forms.Control Viewer
        {
            get;
            private set;
        }

        public bool LoadFile(string path)
        {
            bool success = false;
            try
            {
                RichTextBox viewer = this.Viewer as RichTextBox;

                if (path == null)
                {
                    viewer.Text = null;
                }
                else
                {
                    viewer.Clear();
                    viewer.LoadFile(path, System.IO.Path.GetExtension(path).ToLower().Equals(Common.Models.Constants.ZipExtension) ? RichTextBoxStreamType.RichText : RichTextBoxStreamType.PlainText);
                }
                success = true;
            }
            catch(Exception ex)
            {

            }

            return success;
        }

        public bool LoadFile(FileData path)
        {
            return this.LoadFile(path.Path);
        }

        public void Clear()
        {
            (this.Viewer as RichTextBox).Clear();
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

        ~TextFilePreview()
        {
            this.Dispose(false);
        }
    }
}
