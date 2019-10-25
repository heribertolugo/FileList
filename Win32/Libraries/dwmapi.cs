using System;
using System.Runtime.InteropServices;
using Win32.Models;

namespace Win32.Libraries
{
    public static class dwmapi
    {
        #region dwmapi.dll

        [DllImport("dwmapi.dll")]
        internal static extern int DwmExtendFrameIntoClientArea(
          IntPtr hWnd,
          ref MARGINS pMarInset);

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
