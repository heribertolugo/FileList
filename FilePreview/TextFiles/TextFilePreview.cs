using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FilePreview.TextFiles
{
    public class TextFilePreview : Common.Models.IPreviewFile
    {
        public TextFilePreview()
        {
            this.Viewer = new RichTextBox() { ReadOnly = true };
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

        public bool Load(string path)
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
                    viewer.LoadFile(path, System.IO.Path.GetExtension(path).ToLower().Equals(".rtf") ? RichTextBoxStreamType.RichText : RichTextBoxStreamType.PlainText);
                }
                success = true;
            }
            catch(Exception ex)
            {

            }

            return success;
        }
    }
}
