using System;
using System.Runtime.InteropServices;

namespace Win32.Libraries
{
    public static class comctl32
    {
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
    }
}
