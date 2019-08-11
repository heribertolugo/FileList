using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace FileList.Models.ImageList
{
    public class SysImageList : IDisposable
    {
        private IntPtr hIml = IntPtr.Zero;
        private SysImageList.IImageList iImageList = (SysImageList.IImageList)null;
        private SysImageListSize size = SysImageListSize.smallIcons;
        private bool disposed = false;
        private const int MAX_PATH = 260;
        private const int FILE_ATTRIBUTE_NORMAL = 128;
        private const int FILE_ATTRIBUTE_DIRECTORY = 16;
        private const int FORMAT_MESSAGE_ALLOCATE_BUFFER = 256;
        private const int FORMAT_MESSAGE_ARGUMENT_ARRAY = 8192;
        private const int FORMAT_MESSAGE_FROM_HMODULE = 2048;
        private const int FORMAT_MESSAGE_FROM_STRING = 1024;
        private const int FORMAT_MESSAGE_FROM_SYSTEM = 4096;
        private const int FORMAT_MESSAGE_IGNORE_INSERTS = 512;
        private const int FORMAT_MESSAGE_MAX_WIDTH_MASK = 255;

        [DllImport("shell32")]
        private static extern IntPtr SHGetFileInfo(
          string pszPath,
          int dwFileAttributes,
          ref SysImageList.SHFILEINFO psfi,
          uint cbFileInfo,
          uint uFlags);

        [DllImport("user32.dll")]
        private static extern int DestroyIcon(IntPtr hIcon);

        [DllImport("kernel32")]
        private static extern int FormatMessage(
          int dwFlags,
          IntPtr lpSource,
          int dwMessageId,
          int dwLanguageId,
          string lpBuffer,
          uint nSize,
          int argumentsLong);

        [DllImport("kernel32")]
        private static extern int GetLastError();

        [DllImport("comctl32")]
        private static extern int ImageList_Draw(
          IntPtr hIml,
          int i,
          IntPtr hdcDst,
          int x,
          int y,
          int fStyle);

        [DllImport("comctl32")]
        private static extern int ImageList_DrawIndirect(ref SysImageList.IMAGELISTDRAWPARAMS pimldp);

        [DllImport("comctl32")]
        private static extern int ImageList_GetIconSize(IntPtr himl, ref int cx, ref int cy);

        [DllImport("comctl32")]
        private static extern IntPtr ImageList_GetIcon(IntPtr himl, int i, int flags);

        [DllImport("shell32.dll", EntryPoint = "#727")]
        private static extern int SHGetImageList(
          int iImageList,
          ref Guid riid,
          ref SysImageList.IImageList ppv);

        [DllImport("shell32.dll", EntryPoint = "#727")]
        private static extern int SHGetImageListHandle(
          int iImageList,
          ref Guid riid,
          ref IntPtr handle);

        public IntPtr Handle
        {
            get
            {
                return this.hIml;
            }
        }

        public SysImageListSize ImageListSize
        {
            get
            {
                return this.size;
            }
            set
            {
                this.size = value;
                this.create();
            }
        }

        public Size Size
        {
            get
            {
                int cx = 0;
                int cy = 0;
                if (this.iImageList == null)
                    SysImageList.ImageList_GetIconSize(this.hIml, ref cx, ref cy);
                else
                    this.iImageList.GetIconSize(ref cx, ref cy);
                return new Size(cx, cy);
            }
        }

        public Icon Icon(int index)
        {
            Icon icon = (Icon)null;
            IntPtr picon = IntPtr.Zero;
            if (this.iImageList == null)
                picon = SysImageList.ImageList_GetIcon(this.hIml, index, 1);
            else
                this.iImageList.GetIcon(index, 1, ref picon);
            if (picon != IntPtr.Zero)
                icon = System.Drawing.Icon.FromHandle(picon);
            return icon;
        }

        public int IconIndex(string fileName)
        {
            return this.IconIndex(fileName, false);
        }

        public int IconIndex(string fileName, bool forceLoadFromDisk)
        {
            return this.IconIndex(fileName, forceLoadFromDisk, ShellIconStateConstants.ShellIconStateNormal);
        }

        public int IconIndex(
          string fileName,
          bool forceLoadFromDisk,
          ShellIconStateConstants iconState)
        {
            SysImageList.SHGetFileInfoConstants fileInfoConstants = SysImageList.SHGetFileInfoConstants.SHGFI_SYSICONINDEX;
            if (this.size == SysImageListSize.smallIcons)
                fileInfoConstants |= SysImageList.SHGetFileInfoConstants.SHGFI_SMALLICON;
            int dwFileAttributes;
            if (!forceLoadFromDisk)
            {
                fileInfoConstants |= SysImageList.SHGetFileInfoConstants.SHGFI_USEFILEATTRIBUTES;
                dwFileAttributes = 128;
            }
            else
                dwFileAttributes = 0;
            SysImageList.SHFILEINFO psfi = new SysImageList.SHFILEINFO();
            uint cbFileInfo = (uint)Marshal.SizeOf(psfi.GetType());
            IntPtr fileInfo = SysImageList.SHGetFileInfo(fileName, dwFileAttributes, ref psfi, cbFileInfo, (uint)(fileInfoConstants | (SysImageList.SHGetFileInfoConstants)iconState));
            if (!fileInfo.Equals((object)IntPtr.Zero))
                return psfi.iIcon;
            Debug.Assert(!fileInfo.Equals((object)IntPtr.Zero), "Failed to get icon index");
            return 0;
        }

        public void DrawImage(IntPtr hdc, int index, int x, int y)
        {
            this.DrawImage(hdc, index, x, y, ImageListDrawItemConstants.ILD_TRANSPARENT);
        }

        public void DrawImage(IntPtr hdc, int index, int x, int y, ImageListDrawItemConstants flags)
        {
            if (this.iImageList == null)
            {
                SysImageList.ImageList_Draw(this.hIml, index, hdc, x, y, (int)flags);
            }
            else
            {
                SysImageList.IMAGELISTDRAWPARAMS pimldp = new SysImageList.IMAGELISTDRAWPARAMS()
                {
                    hdcDst = hdc
                };
                pimldp.cbSize = Marshal.SizeOf(pimldp.GetType());
                pimldp.i = index;
                pimldp.x = x;
                pimldp.y = y;
                pimldp.rgbFg = -1;
                pimldp.fStyle = (int)flags;
                this.iImageList.Draw(ref pimldp);
            }
        }

        public void DrawImage(
          IntPtr hdc,
          int index,
          int x,
          int y,
          ImageListDrawItemConstants flags,
          int cx,
          int cy)
        {
            SysImageList.IMAGELISTDRAWPARAMS pimldp = new SysImageList.IMAGELISTDRAWPARAMS()
            {
                hdcDst = hdc
            };
            pimldp.cbSize = Marshal.SizeOf(pimldp.GetType());
            pimldp.i = index;
            pimldp.x = x;
            pimldp.y = y;
            pimldp.cx = cx;
            pimldp.cy = cy;
            pimldp.fStyle = (int)flags;
            if (this.iImageList == null)
            {
                pimldp.himl = this.hIml;
                SysImageList.ImageList_DrawIndirect(ref pimldp);
            }
            else
                this.iImageList.Draw(ref pimldp);
        }

        public void DrawImage(
          IntPtr hdc,
          int index,
          int x,
          int y,
          ImageListDrawItemConstants flags,
          int cx,
          int cy,
          Color foreColor,
          ImageListDrawStateConstants stateFlags,
          Color saturateColorOrAlpha,
          Color glowOrShadowColor)
        {
            SysImageList.IMAGELISTDRAWPARAMS pimldp = new SysImageList.IMAGELISTDRAWPARAMS()
            {
                hdcDst = hdc
            };
            pimldp.cbSize = Marshal.SizeOf(pimldp.GetType());
            pimldp.i = index;
            pimldp.x = x;
            pimldp.y = y;
            pimldp.cx = cx;
            pimldp.cy = cy;
            pimldp.rgbFg = Color.FromArgb(0, (int)foreColor.R, (int)foreColor.G, (int)foreColor.B).ToArgb();
            Console.WriteLine("{0}", (object)pimldp.rgbFg);
            pimldp.fStyle = (int)flags;
            pimldp.fState = (int)stateFlags;
            if ((stateFlags & ImageListDrawStateConstants.ILS_ALPHA) == ImageListDrawStateConstants.ILS_ALPHA)
                pimldp.Frame = (int)saturateColorOrAlpha.A;
            else if ((stateFlags & ImageListDrawStateConstants.ILS_SATURATE) == ImageListDrawStateConstants.ILS_SATURATE)
            {
                saturateColorOrAlpha = Color.FromArgb(0, (int)saturateColorOrAlpha.R, (int)saturateColorOrAlpha.G, (int)saturateColorOrAlpha.B);
                pimldp.Frame = saturateColorOrAlpha.ToArgb();
            }
            glowOrShadowColor = Color.FromArgb(0, (int)glowOrShadowColor.R, (int)glowOrShadowColor.G, (int)glowOrShadowColor.B);
            pimldp.crEffect = glowOrShadowColor.ToArgb();
            if (this.iImageList == null)
            {
                pimldp.himl = this.hIml;
                SysImageList.ImageList_DrawIndirect(ref pimldp);
            }
            else
                this.iImageList.Draw(ref pimldp);
        }

        private bool isXpOrAbove()
        {
            bool flag = false;
            if (Environment.OSVersion.Version.Major > 5)
                flag = true;
            else if (Environment.OSVersion.Version.Major == 5 && Environment.OSVersion.Version.Minor >= 1)
                flag = true;
            return flag;
        }

        private void create()
        {
            this.hIml = IntPtr.Zero;
            if (this.isXpOrAbove())
            {
                Guid riid = new Guid("46EB5926-582E-4017-9FDF-E8998DAA0950");
                SysImageList.SHGetImageList((int)this.size, ref riid, ref this.iImageList);
                SysImageList.SHGetImageListHandle((int)this.size, ref riid, ref this.hIml);
            }
            else
            {
                SysImageList.SHGetFileInfoConstants fileInfoConstants = SysImageList.SHGetFileInfoConstants.SHGFI_SYSICONINDEX | SysImageList.SHGetFileInfoConstants.SHGFI_USEFILEATTRIBUTES;
                if (this.size == SysImageListSize.smallIcons)
                    fileInfoConstants |= SysImageList.SHGetFileInfoConstants.SHGFI_SMALLICON;
                SysImageList.SHFILEINFO psfi = new SysImageList.SHFILEINFO();
                uint cbFileInfo = (uint)Marshal.SizeOf(psfi.GetType());
                this.hIml = SysImageList.SHGetFileInfo(".txt", 128, ref psfi, cbFileInfo, (uint)fileInfoConstants);
                Debug.Assert(this.hIml != IntPtr.Zero, "Failed to create Image List");
            }
        }

        public SysImageList()
        {
            this.create();
        }

        public SysImageList(SysImageListSize size)
        {
            this.size = size;
            this.create();
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize((object)this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed && disposing)
            {
                if (this.iImageList != null)
                    Marshal.ReleaseComObject((object)this.iImageList);
                this.iImageList = (SysImageList.IImageList)null;
            }
            this.disposed = true;
        }

        ~SysImageList()
        {
            this.Dispose(false);
        }

        [Flags]
        private enum SHGetFileInfoConstants
        {
            SHGFI_ICON = 256, // 0x00000100
            SHGFI_DISPLAYNAME = 512, // 0x00000200
            SHGFI_TYPENAME = 1024, // 0x00000400
            SHGFI_ATTRIBUTES = 2048, // 0x00000800
            SHGFI_ICONLOCATION = 4096, // 0x00001000
            SHGFI_EXETYPE = 8192, // 0x00002000
            SHGFI_SYSICONINDEX = 16384, // 0x00004000
            SHGFI_LINKOVERLAY = 32768, // 0x00008000
            SHGFI_SELECTED = 65536, // 0x00010000
            SHGFI_ATTR_SPECIFIED = 131072, // 0x00020000
            SHGFI_LARGEICON = 0,
            SHGFI_SMALLICON = 1,
            SHGFI_OPENICON = 2,
            SHGFI_SHELLICONSIZE = 4,
            SHGFI_USEFILEATTRIBUTES = 16, // 0x00000010
            SHGFI_ADDOVERLAYS = 32, // 0x00000020
            SHGFI_OVERLAYINDEX = 64, // 0x00000040
        }

        private struct RECT
        {
            private int left;
            private int top;
            private int right;
            private int bottom;
        }

        private struct POINT
        {
            private int x;
            private int y;
        }

        private struct IMAGELISTDRAWPARAMS
        {
            public int cbSize;
            public IntPtr himl;
            public int i;
            public IntPtr hdcDst;
            public int x;
            public int y;
            public int cx;
            public int cy;
            public int xBitmap;
            public int yBitmap;
            public int rgbBk;
            public int rgbFg;
            public int fStyle;
            public int dwRop;
            public int fState;
            public int Frame;
            public int crEffect;
        }

        private struct IMAGEINFO
        {
            public IntPtr hbmImage;
            public IntPtr hbmMask;
            public int Unused1;
            public int Unused2;
            public SysImageList.RECT rcImage;
        }

        private struct SHFILEINFO
        {
            public IntPtr hIcon;
            public int iIcon;
            public int dwAttributes;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szDisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;
        }

        [Guid("46EB5926-582E-4017-9FDF-E8998DAA0950")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [ComImport]
        private interface IImageList
        {
            [MethodImpl(MethodImplOptions.PreserveSig)]
            int Add(IntPtr hbmImage, IntPtr hbmMask, ref int pi);

            [MethodImpl(MethodImplOptions.PreserveSig)]
            int ReplaceIcon(int i, IntPtr hicon, ref int pi);

            [MethodImpl(MethodImplOptions.PreserveSig)]
            int SetOverlayImage(int iImage, int iOverlay);

            [MethodImpl(MethodImplOptions.PreserveSig)]
            int Replace(int i, IntPtr hbmImage, IntPtr hbmMask);

            [MethodImpl(MethodImplOptions.PreserveSig)]
            int AddMasked(IntPtr hbmImage, int crMask, ref int pi);

            [MethodImpl(MethodImplOptions.PreserveSig)]
            int Draw(ref SysImageList.IMAGELISTDRAWPARAMS pimldp);

            [MethodImpl(MethodImplOptions.PreserveSig)]
            int Remove(int i);

            [MethodImpl(MethodImplOptions.PreserveSig)]
            int GetIcon(int i, int flags, ref IntPtr picon);

            [MethodImpl(MethodImplOptions.PreserveSig)]
            int GetImageInfo(int i, ref SysImageList.IMAGEINFO pImageInfo);

            [MethodImpl(MethodImplOptions.PreserveSig)]
            int Copy(int iDst, SysImageList.IImageList punkSrc, int iSrc, int uFlags);

            [MethodImpl(MethodImplOptions.PreserveSig)]
            int Merge(
              int i1,
              SysImageList.IImageList punk2,
              int i2,
              int dx,
              int dy,
              ref Guid riid,
              ref IntPtr ppv);

            [MethodImpl(MethodImplOptions.PreserveSig)]
            int Clone(ref Guid riid, ref IntPtr ppv);

            [MethodImpl(MethodImplOptions.PreserveSig)]
            int GetImageRect(int i, ref SysImageList.RECT prc);

            [MethodImpl(MethodImplOptions.PreserveSig)]
            int GetIconSize(ref int cx, ref int cy);

            [MethodImpl(MethodImplOptions.PreserveSig)]
            int SetIconSize(int cx, int cy);

            [MethodImpl(MethodImplOptions.PreserveSig)]
            int GetImageCount(ref int pi);

            [MethodImpl(MethodImplOptions.PreserveSig)]
            int SetImageCount(int uNewCount);

            [MethodImpl(MethodImplOptions.PreserveSig)]
            int SetBkColor(int clrBk, ref int pclr);

            [MethodImpl(MethodImplOptions.PreserveSig)]
            int GetBkColor(ref int pclr);

            [MethodImpl(MethodImplOptions.PreserveSig)]
            int BeginDrag(int iTrack, int dxHotspot, int dyHotspot);

            [MethodImpl(MethodImplOptions.PreserveSig)]
            int EndDrag();

            [MethodImpl(MethodImplOptions.PreserveSig)]
            int DragEnter(IntPtr hwndLock, int x, int y);

            [MethodImpl(MethodImplOptions.PreserveSig)]
            int DragLeave(IntPtr hwndLock);

            [MethodImpl(MethodImplOptions.PreserveSig)]
            int DragMove(int x, int y);

            [MethodImpl(MethodImplOptions.PreserveSig)]
            int SetDragCursorImage(
              ref SysImageList.IImageList punk,
              int iDrag,
              int dxHotspot,
              int dyHotspot);

            [MethodImpl(MethodImplOptions.PreserveSig)]
            int DragShowNolock(int fShow);

            [MethodImpl(MethodImplOptions.PreserveSig)]
            int GetDragImage(
              ref SysImageList.POINT ppt,
              ref SysImageList.POINT pptHotspot,
              ref Guid riid,
              ref IntPtr ppv);

            [MethodImpl(MethodImplOptions.PreserveSig)]
            int GetItemFlags(int i, ref int dwFlags);

            [MethodImpl(MethodImplOptions.PreserveSig)]
            int GetOverlayImage(int iOverlay, ref int piIndex);
        }
    }

    [Flags]
    public enum ImageListDrawItemConstants
    {
        ILD_NORMAL = 0,
        ILD_TRANSPARENT = 1,
        ILD_BLEND25 = 2,
        ILD_SELECTED = 4,
        ILD_MASK = 16, // 0x00000010
        ILD_IMAGE = 32, // 0x00000020
        ILD_ROP = 64, // 0x00000040
        ILD_PRESERVEALPHA = 4096, // 0x00001000
        ILD_SCALE = 8192, // 0x00002000
        ILD_DPISCALE = 16384, // 0x00004000
    }

    [Flags]
    public enum ImageListDrawStateConstants
    {
        ILS_NORMAL = 0,
        ILS_GLOW = 1,
        ILS_SHADOW = 2,
        ILS_SATURATE = 4,
        ILS_ALPHA = 8,
    }

    public enum SysImageListSize
    {
        largeIcons = 0,
        smallIcons = 1,
        extraLargeIcons = 2,
        jumbo = 4,
    }

    [Flags]
    public enum ShellIconStateConstants
    {
        ShellIconStateNormal = 0,
        ShellIconStateLinkOverlay = 32768, // 0x00008000
        ShellIconStateSelected = 65536, // 0x00010000
        ShellIconStateOpen = 2,
        ShellIconAddOverlays = 32, // 0x00000020
    }
}
