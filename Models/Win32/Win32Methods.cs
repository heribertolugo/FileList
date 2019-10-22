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

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        internal static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, int wParam, object lParam);

        [DllImport("user32.dll")]
        internal static extern int DestroyIcon(IntPtr hIcon);

        [DllImport("user32.dll", EntryPoint = "GetWindowLong")]
        private static extern IntPtr GetWindowLongPtr32(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", EntryPoint = "GetWindowLongPtr")]
        private static extern IntPtr GetWindowLongPtr64(IntPtr hWnd, int nIndex);

        // This static method is required because Win32 does not support
        // GetWindowLongPtr directly
        internal static IntPtr GetWindowLongPtr(IntPtr hWnd, int nIndex)
        {
            if (IntPtr.Size == 8)
                return GetWindowLongPtr64(hWnd, nIndex);
            else
                return GetWindowLongPtr32(hWnd, nIndex);
        }
        #endregion

        #region kernal32.dll
        [DllImport("kernel32.dll")]
        internal static extern bool AttachConsole(int dwProcessId);

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

        #region ole32.dll
        [DllImport("ole32.dll", PreserveSig = false)]
        public static extern void CoMarshalInterThreadInterfaceInStream(
            [In] ref Guid riid, //, MarshalAs(UnmanagedType.LPStruct)
            [MarshalAs(UnmanagedType.IUnknown)] object pUnk,
            out IntPtr ppStm);

        [DllImport("ole32.dll", PreserveSig = false)]
        public static extern void CoGetInterfaceAndReleaseStream(
            IntPtr pStm,
            [In] ref Guid riid, //, MarshalAs(UnmanagedType.LPStruct)
            [MarshalAs(UnmanagedType.IUnknown)] out object ppv);

        [DllImport("ole32.dll")]
        public static extern long CoUnmarshalInterface(
            System.Runtime.InteropServices.ComTypes.IStream pStm,
            [In] ref Guid riid, //, MarshalAs(UnmanagedType.LPStruct)
            [MarshalAs(UnmanagedType.IUnknown)] out object ppv
        );

        /// <summary>
        /// Writes into a stream the data required to initialize a proxy object in some client process.
        /// </summary>
        /// <remarks>
        /// The CoMarshalInterface function marshals the interface referred to by riid on the object whose IUnknown implementation is pointed to by pUnk. To do so, the CoMarshalInterface function performs the following tasks:
        /// <list type="number">
        /// <item><description>
        /// Queries the object for a pointer to the IMarshal interface. If the object does not implement IMarshal, meaning that it relies on COM to provide marshaling support, CoMarshalInterface gets a pointer to COM's default implementation of IMarshal.
        /// </description></item>
        /// <item><description>
        /// Gets the CLSID of the object's proxy by calling IMarshal::GetUnmarshalClass, using whichever IMarshal interface pointer has been returned.
        /// </description></item>
        /// <item><description>
        /// Writes the CLSID of the proxy to the stream to be used for marshaling.
        /// </description></item>
        /// <item><description>
        /// Marshals the interface pointer by calling IMarshal::MarshalInterface.
        /// </description></item>
        /// </list>
        /// The COM library in the client process calls the CoUnmarshalInterface function to extract the data and initialize the proxy.Before calling CoUnmarshalInterface, seek back to the original position in the stream.
        /// If you are implementing existing COM interfaces or defining your own interfaces using the Microsoft Interface Definition Language(MIDL), the MIDL-generated proxies and stubs call CoMarshalInterface for you.If you are writing your own proxies and stubs, your proxy code and stub code should each call CoMarshalInterface to correctly marshal interface pointers. Calling IMarshal directly from your proxy and stub code is not recommended.
        /// If you are writing your own implementation of IMarshal, and your proxy needs access to a private object, you can include an interface pointer to that object as part of the data you write to the stream.In such situations, if you want to use COM's default marshaling implementation when passing the interface pointer, you can call CoMarshalInterface on the object to do so.
        /// </remarks>
        /// <param name="pStm">A pointer to the stream to be used during marshaling. See IStream.</param>
        /// <param name="riid">A reference to the identifier of the interface to be marshaled. This interface must be derived from the IUnknown interface.</param>
        /// <param name="pUnk">A pointer to the interface to be marshaled. This interface must be derived from the IUnknown interface.</param>
        /// <param name="dwDestContext">The destination context where the specified interface is to be unmarshaled. The possible values come from the enumeration MSHCTX. Currently, unmarshaling can occur in another apartment of the current process (MSHCTX_INPROC), in another process on the same computer as the current process (MSHCTX_LOCAL), or in a process on a different computer (MSHCTX_DIFFERENTMACHINE).</param>
        /// <param name="pvDestContext">This parameter is reserved and must be NULL.</param>
        /// <param name="mshlflags">The flags that specify whether the data to be marshaled is to be transmitted back to the client process (the typical case) or written to a global table, where it can be retrieved by multiple clients. The possibles values come from the MSHLFLAGS enumeration.</param>
        /// <returns>
        /// This function can return the standard return values E_FAIL, E_OUTOFMEMORY, and E_UNEXPECTED, the stream-access error values returned by IStream, as well as the following values.
        /// <list type="table">
        /// <listheader>
        /// <term>Return code</term> <term>Description</term>
        /// </listheader>
        /// <item><term>S_OK</term> <term>The HRESULT was marshaled successfully.</term></item>
        /// <item><term>CO_E_NOTINITIALIZED</term> <term>The CoInitialize or OleInitialize function was not called on the current thread before this function was called.</term></item>
        /// </list>
        /// </returns>
        [DllImport("ole32.dll")]
        public static extern long CoMarshalInterface(
            System.Runtime.InteropServices.ComTypes.IStream pStm,
            [In] ref Guid riid,
            [MarshalAs(UnmanagedType.IUnknown)] object pUnk,
            MSHCTX dwDestContext,
            IntPtr pvDestContext,
            MSHLFLAGS mshlflags
        );

        private static object locker = new object();
        //private static object localObject;
        public static Shell32.IShellDispatch5 GetIShellDispatch5()
        {
            Extensions.WriteToConsole("requesting IShellDispatch5 lock @ 199");
            lock (locker)
            {
                object localObject = null;
                Guid shellId = Marshal.GenerateGuidForType(typeof(Shell32.IShellDispatch5));
                try
                {
                    IntPtr ptr = mystream.ToIntPtr();

                    mystream.Seek(0, 0, ptr);
                    HRESULT o = Win32Methods.CoUnmarshalInterface(mystream, ref shellId, out localObject);
                }
                catch (Exception ex)
                {
                    int c = 0;
                    c++;
                    throw;
                }
                return (Shell32.IShellDispatch5)localObject;
            }
        }

        private static System.Runtime.InteropServices.ComTypes.IStream mystream;
        public static System.Runtime.InteropServices.ComTypes.IStream GetRegisteredInterfaceMarshalPtr<T>(object obj)
        {
            Extensions.WriteToConsole("requesting IShellDispatch5 lock @ 222");
            lock (locker)
            {
                Guid guid = Marshal.GenerateGuidForType(typeof(T));
                mystream = Win32Methods.SHCreateMemStream(null, 0);

                IntPtr ptr = mystream.ToIntPtr();
                mystream.Seek(0, 0, ptr);

                HRESULT result = Win32Methods.CoMarshalInterface(mystream, ref guid, obj, Win32.MSHCTX.MSHCTX_INPROC, new IntPtr(), Win32.MSHLFLAGS.MSHLFLAGS_NORMAL);
                return mystream;
            }
        }
        #endregion

        #region shlwapi.dll
        [DllImport("shlwapi.dll")] 
        public static extern System.Runtime.InteropServices.ComTypes.IStream SHCreateMemStream(
            object pInit,
            uint cbInit
        );
        #endregion

        #region uxtheme.dll
        [DllImport("uxtheme.dll", CharSet = CharSet.Unicode)]
        internal extern static int SetWindowTheme(IntPtr hWnd, string pszSubAppName, string pszSubIdList);
        #endregion
    }
}
