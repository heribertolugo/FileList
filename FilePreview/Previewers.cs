using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Models;

namespace FilePreview
{
    public class Previewers
    {
        public IPreviewFile GetPreviewer(string fileExtension)
        {
            return null;
        }
        public IPreviewFile GetPreviewer(FileType fileType)
        {
            return null;
        }
    }
}
