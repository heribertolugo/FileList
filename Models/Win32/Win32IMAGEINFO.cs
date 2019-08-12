using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FileList.Models.Win32
{
    internal struct IMAGEINFO
    {
        public IntPtr hbmImage;
        public IntPtr hbmMask;
        public int Unused1;
        public int Unused2;
        public RECT rcImage;
    }
}
