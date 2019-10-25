using System;
using System.Runtime.InteropServices;

namespace Win32.Libraries
{
    public static class Gdi32
    {
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
    }
}
