using System;
using System.Diagnostics;
using System.Drawing;
using FileList.Models;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace FileList.Models.ImageList
{
    public class SysImageList : IDisposable
    {
        private IntPtr hIml = IntPtr.Zero;
        private Win32.IImageList iImageList = null;
        private Win32.SysImageListSize size = Win32.SysImageListSize.smallIcons;
        private bool disposed = false;

        public IntPtr Handle
        {
            get
            {
                return this.hIml;
            }
        }

        internal Win32.SysImageListSize ImageListSize
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
                    Win32.Win32Methods.ImageList_GetIconSize(this.hIml, ref cx, ref cy);
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
                picon = Win32.Win32Methods.ImageList_GetIcon(this.hIml, index, 1);
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
            return this.IconIndex(fileName, forceLoadFromDisk, Win32.ShellIconStateConstants.ShellIconStateNormal);
        }

        internal int IconIndex(
          string fileName,
          bool forceLoadFromDisk,
          Win32.ShellIconStateConstants iconState)
        {
            Win32.SHGetFileInfoConstants fileInfoConstants = Win32.SHGetFileInfoConstants.SHGFI_SYSICONINDEX;
            if (this.size == Win32.SysImageListSize.smallIcons)
                fileInfoConstants |= Win32.SHGetFileInfoConstants.SHGFI_SMALLICON;
            uint dwFileAttributes;
            if (!forceLoadFromDisk)
            {
                fileInfoConstants |= Win32.SHGetFileInfoConstants.SHGFI_USEFILEATTRIBUTES;
                dwFileAttributes = Win32.Win32Enums.FILE_ATTRIBUTE_NORMAL;
            }
            else
                dwFileAttributes = 0;
            Win32.SHFILEINFO psfi = new Win32.SHFILEINFO();
            uint cbFileInfo = (uint)Marshal.SizeOf(psfi.GetType());
            IntPtr fileInfo = Win32.Win32Methods.SHGetFileInfo(fileName, dwFileAttributes, ref psfi, cbFileInfo, (uint)(fileInfoConstants | (Win32.SHGetFileInfoConstants)iconState));
            if (!fileInfo.Equals(IntPtr.Zero))
                return (int)psfi.iIcon;
            Debug.Assert(!fileInfo.Equals(IntPtr.Zero), "Failed to get icon index");
            return 0;
        }

        internal void DrawImage(IntPtr hdc, int index, int x, int y)
        {
            this.DrawImage(hdc, index, x, y, Win32.ImageListDrawItemConstants.ILD_TRANSPARENT);
        }

        internal void DrawImage(IntPtr hdc, int index, int x, int y, Win32.ImageListDrawItemConstants flags)
        {
            if (this.iImageList == null)
            {
                Win32.Win32Methods.ImageList_Draw(this.hIml, index, hdc, x, y, (int)flags);
            }
            else
            {
                Win32.IMAGELISTDRAWPARAMS pimldp = new Win32.IMAGELISTDRAWPARAMS()
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

        internal void DrawImage(
          IntPtr hdc,
          int index,
          int x,
          int y,
          Win32.ImageListDrawItemConstants flags,
          int cx,
          int cy)
        {
            Win32.IMAGELISTDRAWPARAMS pimldp = new Win32.IMAGELISTDRAWPARAMS()
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
                Win32.Win32Methods.ImageList_DrawIndirect(ref pimldp);
            }
            else
                this.iImageList.Draw(ref pimldp);
        }

        internal void DrawImage(
          IntPtr hdc,
          int index,
          int x,
          int y,
          Win32.ImageListDrawItemConstants flags,
          int cx,
          int cy,
          Color foreColor,
          Win32.ImageListDrawStateConstants stateFlags,
          Color saturateColorOrAlpha,
          Color glowOrShadowColor)
        {
            Win32.IMAGELISTDRAWPARAMS pimldp = new Win32.IMAGELISTDRAWPARAMS()
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
            if ((stateFlags & Win32.ImageListDrawStateConstants.ILS_ALPHA) == Win32.ImageListDrawStateConstants.ILS_ALPHA)
                pimldp.Frame = (int)saturateColorOrAlpha.A;
            else if ((stateFlags & Win32.ImageListDrawStateConstants.ILS_SATURATE) == Win32.ImageListDrawStateConstants.ILS_SATURATE)
            {
                saturateColorOrAlpha = Color.FromArgb(0, (int)saturateColorOrAlpha.R, (int)saturateColorOrAlpha.G, (int)saturateColorOrAlpha.B);
                pimldp.Frame = saturateColorOrAlpha.ToArgb();
            }
            glowOrShadowColor = Color.FromArgb(0, (int)glowOrShadowColor.R, (int)glowOrShadowColor.G, (int)glowOrShadowColor.B);
            pimldp.crEffect = glowOrShadowColor.ToArgb();
            if (this.iImageList == null)
            {
                pimldp.himl = this.hIml;
                Win32.Win32Methods.ImageList_DrawIndirect(ref pimldp);
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
                Win32.Win32Methods.SHGetImageList((int)this.size, ref riid, ref this.iImageList);
                Win32.Win32Methods.SHGetImageListHandle((int)this.size, ref riid, ref this.hIml);
            }
            else
            {
                Win32.SHGetFileInfoConstants fileInfoConstants = Win32.SHGetFileInfoConstants.SHGFI_SYSICONINDEX | Win32.SHGetFileInfoConstants.SHGFI_USEFILEATTRIBUTES;
                if (this.size == Win32.SysImageListSize.smallIcons)
                    fileInfoConstants |= Win32.SHGetFileInfoConstants.SHGFI_SMALLICON;
                Win32.SHFILEINFO psfi = new Win32.SHFILEINFO();
                uint cbFileInfo = (uint)Marshal.SizeOf(psfi.GetType());
                this.hIml = Win32.Win32Methods.SHGetFileInfo(".txt", Win32.Win32Enums.FILE_ATTRIBUTE_NORMAL, ref psfi, cbFileInfo, (uint)fileInfoConstants);
                Debug.Assert(this.hIml != IntPtr.Zero, "Failed to create Image List");
            }
        }

        public SysImageList()
        {
            this.create();
        }

        internal SysImageList(Win32.SysImageListSize size)
        {
            this.size = size;
            this.create();
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed && disposing)
            {
                if (this.iImageList != null)
                    Marshal.ReleaseComObject(this.iImageList);
                this.iImageList = null;
            }
            this.disposed = true;
        }

        ~SysImageList()
        {
            this.Dispose(false);
        } 
    }

}
