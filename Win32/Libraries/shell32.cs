using System;
using System.Runtime.InteropServices;
using Win32.Models;

namespace Win32.Libraries
{
    public static class shell32
    {
        #region shell32.dll

        [DllImport("shell32.dll")]
        internal static extern IntPtr SHGetFileInfo(
          string pszPath,
          uint dwFileAttributes,
          ref SHFILEINFO psfi,
          uint cbSizeFileInfo,
          uint uFlags);

        [DllImport("shell32.dll", EntryPoint = "#727")]
        internal static extern int SHGetImageList(
        int iImageList,
        ref Guid riid,
        ref IImageList ppv);

        [DllImport("shell32.dll", EntryPoint = "#727")]
        internal static extern int SHGetImageListHandle(
          int iImageList,
          ref Guid riid,
          ref IntPtr handle);
        #endregion
    }
}
