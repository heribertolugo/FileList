using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common.Models;

namespace FilePreview.PdfFiles
{
    public class PdfPreview : Common.Models.IPreviewFile
    {
        public PdfPreview()
        {
            this.Viewer = new PDFView.PDFViewer() { AllowExport = false, AllowImportImages = false };
        }

        public IEnumerable<string> Extensions
        {
            get { return new string[] { ".pdf" }; }
        }

        public Control Viewer { get; private set; }

        public FileType FileType
        {
            get { return FileType.Application; }
        }

        public void Clear()
        {
            (this.Viewer as PDFView.PDFViewer).FileName = string.Empty;
        }

        public void Dispose()
        {
            (this.Viewer as PDFView.PDFViewer).Dispose();
        }

        public bool LoadFile(string path)
        {
            try
            {
                (this.Viewer as PDFView.PDFViewer).FileName = path;
                return true;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            return false;
        }

        public bool LoadFile(FileData path)
        {
            return this.LoadFile(path.Path);
        }
    }
}
