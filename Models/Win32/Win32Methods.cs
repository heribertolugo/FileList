using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace FileList.Models.Win32
{
    public static partial class Win32Methods
    {
        #region user32.dll
        [DllImport("user32.dll")]
        internal static extern int SetWindowText(IntPtr hWnd, string text);

        [DllImport("user32.dll")]
        internal static extern IntPtr FindWindowEx(
          IntPtr hwndParent,
          IntPtr hwndChildAfter,
          string lpszClass,
          string lpszWindow);

        [DllImport("User32.dll")]
        internal static extern int SendMessage(IntPtr hWnd, int uMsg, int wParam, string lParam);

        [DllImport("user32.dll")]
        internal static extern int DestroyIcon(IntPtr hIcon);
        #endregion

        #region kernal32.dll
        [DllImport("kernel32")]
        internal static extern int FormatMessage(
          int dwFlags,
          IntPtr lpSource,
          int dwMessageId,
          int dwLanguageId,
          string lpBuffer,
          uint nSize,
          int argumentsLong);

        [DllImport("kernel32")]
        internal static extern int GetLastError();
        #endregion

        #region comctl32.dll
        [DllImport("comctl32")]
        internal static extern int ImageList_Draw(
          IntPtr hIml,
          int i,
          IntPtr hdcDst,
          int x,
          int y,
          int fStyle);

        [DllImport("comctl32")]
        internal static extern int ImageList_DrawIndirect(ref IMAGELISTDRAWPARAMS pimldp);

        [DllImport("comctl32")]
        internal static extern int ImageList_GetIconSize(IntPtr himl, ref int cx, ref int cy);

        [DllImport("comctl32")]
        internal static extern IntPtr ImageList_GetIcon(IntPtr himl, int i, int flags);

        #endregion


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


        #region Gdi32.dll

        [DllImport("Gdi32.dll")]
        internal static extern bool DeleteObject(IntPtr hObject);

        [DllImport("Gdi32.dll")]
        internal static extern IntPtr CreateRoundRectRgn(
          int nLeftRect,
          int nTopRect,
          int nRightRect,
          int nBottomRect,
          int nWidthEllipse,
          int nHeightEllipse);
        #endregion

        #region dwmapi.dll

        [DllImport("dwmapi.dll")]
        internal static extern int DwmExtendFrameIntoClientArea(
          IntPtr hWnd,
          ref Models.Win32MARGINS pMarInset);

        [DllImport("dwmapi.dll")]
        internal static extern int DwmSetWindowAttribute(
          IntPtr hwnd,
          int attr,
          ref int attrValue,
          int attrSize);

        [DllImport("dwmapi.dll")]
        internal static extern int DwmIsCompositionEnabled(ref int pfEnabled);
        #endregion
    }
}
