
using System;
using System.Runtime.InteropServices;

namespace Win32.Libraries
{
    public static class uxtheme
    {
        #region uxtheme.dll
        [DllImport("uxtheme.dll", CharSet = CharSet.Unicode)]
        internal extern static int SetWindowTheme(IntPtr hWnd, string pszSubAppName, string pszSubIdList);
        #endregion
    }
}
