﻿using System.Runtime.InteropServices;

namespace Win32.Libraries
{
    public static class shlwapi
    {
        #region shlwapi.dll
        [DllImport("shlwapi.dll")]
        public static extern System.Runtime.InteropServices.ComTypes.IStream SHCreateMemStream(
            byte[] pInit,//
            uint cbInit
        );
        #endregion
    }
}
