using System;
using System.Diagnostics;
using System.Drawing;
using FileList.Models;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Win32.Models;
using Win32.Constants;
using Win32.Libraries;
using Common.Helpers;

namespace FileList.Models.ImageList
{
    public class SysImageList : IDisposable
    {
        private IntPtr hIml = IntPtr.Zero;
        private IImageList iImageList = null;
        private SysImageListSize size = SysImageListSize.smallIcons;
        private bool disposed = false;

        public IntPtr Handle
        {
            get
            {
                return this.hIml;
            }
        }

        internal SysImageListSize ImageListSize
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
                    comctl32.ImageList_GetIconSize(this.hIml, ref cx, ref cy);
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
                picon = comctl32.ImageList_GetIcon(this.hIml, index, 1);
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
            return this.IconIndex(fileName, forceLoadFromDisk, ShellIconState.ShellIconStateNormal);
        }

        internal int IconIndex(
          string fileName,
          bool forceLoadFromDisk,
          ShellIconState iconState)
        {
            SHGetFileInfo fileInfoConstants = SHGetFileInfo.SHGFI_SYSICONINDEX;
            if (this.size == SysImageListSize.smallIcons)
                fileInfoConstants |= SHGetFileInfo.SHGFI_SMALLICON;
            uint dwFileAttributes;
            if (!forceLoadFromDisk)
            {
                fileInfoConstants |= SHGetFileInfo.SHGFI_USEFILEATTRIBUTES;
                dwFileAttributes = FileAttribute.FILE_ATTRIBUTE_NORMAL;
            }
            else
                dwFileAttributes = 0;
            SHFILEINFO psfi = new SHFILEINFO();
            uint cbFileInfo = (uint)Marshal.SizeOf(psfi.GetType());
            IntPtr fileInfo = shell32.SHGetFileInfo(fileName, dwFileAttributes, ref psfi, cbFileInfo, (uint)(fileInfoConstants | (SHGetFileInfo)iconState));
            if (!fileInfo.Equals(IntPtr.Zero))
                return (int)psfi.iIcon;
            Debug.Assert(!fileInfo.Equals(IntPtr.Zero), "Failed to get icon index");
            return 0;
        }

        internal void DrawImage(IntPtr hdc, int index, int x, int y)
        {
            this.DrawImage(hdc, index, x, y, ImageListDrawItem.ILD_TRANSPARENT);
        }

        internal void DrawImage(IntPtr hdc, int index, int x, int y, ImageListDrawItem flags)
        {
            if (this.iImageList == null)
            {
                comctl32.ImageList_Draw(this.hIml, index, hdc, x, y, (int)flags);
            }
            else
            {
                IMAGELISTDRAWPARAMS pimldp = new IMAGELISTDRAWPARAMS()
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
          ImageListDrawItem flags,
          int cx,
          int cy)
        {
            IMAGELISTDRAWPARAMS pimldp = new IMAGELISTDRAWPARAMS()
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
                comctl32.ImageList_DrawIndirect(ref pimldp);
            }
            else
                this.iImageList.Draw(ref pimldp);
        }

        internal void DrawImage(
          IntPtr hdc,
          int index,
          int x,
          int y,
          ImageListDrawItem flags,
          int cx,
          int cy,
          Color foreColor,
          ImageListDrawState stateFlags,
          Color saturateColorOrAlpha,
          Color glowOrShadowColor)
        {
            IMAGELISTDRAWPARAMS pimldp = new IMAGELISTDRAWPARAMS()
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
            IoHelper.WriteToConsole("{0}", (object)pimldp.rgbFg);
            pimldp.fStyle = (int)flags;
            pimldp.fState = (int)stateFlags;
            if ((stateFlags & ImageListDrawState.ILS_ALPHA) == ImageListDrawState.ILS_ALPHA)
                pimldp.Frame = (int)saturateColorOrAlpha.A;
            else if ((stateFlags & ImageListDrawState.ILS_SATURATE) == ImageListDrawState.ILS_SATURATE)
            {
                saturateColorOrAlpha = Color.FromArgb(0, (int)saturateColorOrAlpha.R, (int)saturateColorOrAlpha.G, (int)saturateColorOrAlpha.B);
                pimldp.Frame = saturateColorOrAlpha.ToArgb();
            }
            glowOrShadowColor = Color.FromArgb(0, (int)glowOrShadowColor.R, (int)glowOrShadowColor.G, (int)glowOrShadowColor.B);
            pimldp.crEffect = glowOrShadowColor.ToArgb();
            if (this.iImageList == null)
            {
                pimldp.himl = this.hIml;
                comctl32.ImageList_DrawIndirect(ref pimldp);
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
                shell32.SHGetImageList((int)this.size, ref riid, ref this.iImageList);
                shell32.SHGetImageListHandle((int)this.size, ref riid, ref this.hIml);
            }
            else
            {
                SHGetFileInfo fileInfoConstants = SHGetFileInfo.SHGFI_SYSICONINDEX | SHGetFileInfo.SHGFI_USEFILEATTRIBUTES;
                if (this.size == SysImageListSize.smallIcons)
                    fileInfoConstants |= SHGetFileInfo.SHGFI_SMALLICON;
                SHFILEINFO psfi = new SHFILEINFO();
                uint cbFileInfo = (uint)Marshal.SizeOf(psfi.GetType());
                this.hIml = shell32.SHGetFileInfo(".txt", FileAttribute.FILE_ATTRIBUTE_NORMAL, ref psfi, cbFileInfo, (uint)fileInfoConstants);
                Debug.Assert(this.hIml != IntPtr.Zero, "Failed to create Image List");
            }
        }

        public SysImageList()
        {
            this.create();
        }

        internal SysImageList(SysImageListSize size)
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
