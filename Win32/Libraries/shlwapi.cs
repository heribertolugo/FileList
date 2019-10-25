
using System;
using System.Runtime.InteropServices;

namespace Win32.Libraries
{
    public static class shlwapi
    {
        #region shlwapi.dll
        [DllImport("shlwapi.dll")]
        public static extern System.Runtime.InteropServices.ComTypes.IStream SHCreateMemStream(
            object pInit,
            uint cbInit
        );
        #endregion
    }
}
