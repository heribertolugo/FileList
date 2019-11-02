using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;

/// <summary>
/// Author: Eugene Sichkar
/// Web site: https://www.codeproject.com/Articles/27148/C-NET-Interface-for-7-Zip-Archive-DLLs
/// </summary>
namespace Common.Models.ZipExtractor
{
    public struct SevenZipFormat
    {
        public static SevenZipFormat None = new SevenZipFormat(new string[] { "" }, new Guid(), new byte[] { });
        public static SevenZipFormat SevenZip = new SevenZipFormat(new string[] { "7z", "7-zip" }, new Guid("23170f69-40c1-278a-1000-000110070000"), new byte[] { 0x37, 0x7A, 0xBC, 0xAF, 0x27, 0x1C });
        public static SevenZipFormat Arj = new SevenZipFormat(new string[] { "arj" }, new Guid("23170f69-40c1-278a-1000-000110040000"), new byte[] { 0x60, 0xEA });
        public static SevenZipFormat BZip2 = new SevenZipFormat(new string[] { "bz2" }, new Guid("23170f69-40c1-278a-1000-000110020000"), new byte[] { 0x42, 0x5A, 0x68 });
        public static SevenZipFormat Cab = new SevenZipFormat(new string[] { "cab" }, new Guid("23170f69-40c1-278a-1000-000110080000"), new byte[] { 0x4D, 0x53, 0x43, 0x46 });
        public static SevenZipFormat Chm = new SevenZipFormat(new string[] { "chm" }, new Guid("23170f69-40c1-278a-1000-000110e90000"), new byte[] { 0x49, 0x54, 0x53, 0x46 });
        public static SevenZipFormat Compound = new SevenZipFormat(new string[] { "" }, new Guid("23170f69-40c1-278a-1000-000110e50000"), new byte[] { });
        public static SevenZipFormat Cpio = new SevenZipFormat(new string[] { "" }, new Guid("23170f69-40c1-278a-1000-000110ed0000"), new byte[] { });
        public static SevenZipFormat Deb = new SevenZipFormat(new string[] { "deb" }, new Guid("23170f69-40c1-278a-1000-000110ec0000"), new byte[] { 0x21, 0x3C, 0x61, 0x72, 0x63, 0x68, 0x3E });
        public static SevenZipFormat GZip = new SevenZipFormat(new string[] { "gz" }, new Guid("23170f69-40c1-278a-1000-000110ef0000"), new byte[] { 0x1f, 0x0b });
        public static SevenZipFormat Iso = new SevenZipFormat(new string[] { "iso" }, new Guid("23170f69-40c1-278a-1000-000110e70000"), new byte[] { 0x43, 0x44, 0x30, 0x30, 0x31 });
        public static SevenZipFormat Lzh = new SevenZipFormat(new string[] { "lzh" }, new Guid("23170f69-40c1-278a-1000-000110060000"), new byte[] { 0x2D, 0x6C, 0x68 });
        public static SevenZipFormat Lzma = new SevenZipFormat(new string[] { "lzma" }, new Guid("23170f69-40c1-278a-1000-0001100a0000"), new byte[] { });
        public static SevenZipFormat Nsis = new SevenZipFormat(new string[] { "" }, new Guid("23170f69-40c1-278a-1000-000110090000"), new byte[] { });
        public static SevenZipFormat Rar = new SevenZipFormat(new string[] { "rar" }, new Guid("23170f69-40c1-278a-1000-000110030000"), new byte[] { 0x52, 0x61, 0x72, 0x21, 0x1A, 0x07, 0x00 });
        public static SevenZipFormat Rar5 = new SevenZipFormat(new string[] { "" }, new Guid("23170f69-40c1-278a-1000-000110CC0000"), new byte[] { 0x52, 0x61, 0x72, 0x21, 0x1A, 0x07, 0x01, 0x00 });
        public static SevenZipFormat Rpm = new SevenZipFormat(new string[] { "rpm" }, new Guid("23170f69-40c1-278a-1000-000110eb0000"), new byte[] { 0xed, 0xab, 0xee, 0xdb });
        public static SevenZipFormat Split = new SevenZipFormat(new string[] { "" }, new Guid("23170f69-40c1-278a-1000-000110ea0000"), new byte[] { });
        public static SevenZipFormat Tar = new SevenZipFormat(new string[] { "tar" }, new Guid("23170f69-40c1-278a-1000-000110ee0000"), new byte[] { 0x75, 0x73, 0x74, 0x61, 0x72 });
        public static SevenZipFormat Wim = new SevenZipFormat(new string[] { "" }, new Guid("23170f69-40c1-278a-1000-000110e60000"), new byte[] { });
        public static SevenZipFormat Lzw = new SevenZipFormat(new string[] { "z" }, new Guid("23170f69-40c1-278a-1000-000110050000"), new byte[] { });
        public static SevenZipFormat Zip = new SevenZipFormat(new string[] { "zip" }, new Guid("23170f69-40c1-278a-1000-000110010000"), new byte[] { 0x50, 0x4b });
        public static SevenZipFormat Udf = new SevenZipFormat(new string[] { "udf" }, new Guid("23170f69-40c1-278a-1000-000110E00000"), new byte[] { });
        public static SevenZipFormat Xar = new SevenZipFormat(new string[] { "xar" }, new Guid("23170f69-40c1-278a-1000-000110E10000"), new byte[] { 0x78, 0x61, 0x72, 0x21 });
        public static SevenZipFormat Mub = new SevenZipFormat(new string[] { "mub" }, new Guid("23170f69-40c1-278a-1000-000110E20000"), new byte[] { });
        public static SevenZipFormat Hfs = new SevenZipFormat(new string[] { "hds" }, new Guid("23170f69-40c1-278a-1000-000110E30000"), new byte[] { });
        public static SevenZipFormat Dmg = new SevenZipFormat(new string[] { "dmg" }, new Guid("23170f69-40c1-278a-1000-000110E40000"), new byte[] { 0x78, 0x01, 0x73, 0x0D, 0x62, 0x62, 0x60 });
        public static SevenZipFormat XZ = new SevenZipFormat(new string[] { "xz" }, new Guid("23170f69-40c1-278a-1000-0001100C0000"), new byte[] { });
        public static SevenZipFormat Mslz = new SevenZipFormat(new string[] { "" }, new Guid("23170f69-40c1-278a-1000-000110D50000"), new byte[] { });
        //public static SevenZipFormat Flv = new SevenZipFormat(new string[]{ "" }, new Guid("23170f69-40c1-278a-1000-000110D60000"), new byte[] {0x46, 0x4C, 0x56});
        public static SevenZipFormat Swf = new SevenZipFormat(new string[] { "swf" }, new Guid("23170f69-40c1-278a-1000-000110D70000"), new byte[] { 0x46, 0x57, 0x53 });
        public static SevenZipFormat PE = new SevenZipFormat(new string[] { "exe", "dll" }, new Guid("23170f69-40c1-278a-1000-000110DD0000"), new byte[] { });
        public static SevenZipFormat Elf = new SevenZipFormat(new string[] { "" }, new Guid("23170f69-40c1-278a-1000-000110DE0000"), new byte[] { });
        public static SevenZipFormat Msi = new SevenZipFormat(new string[] { "msi" }, new Guid(), new byte[] { });
        public static SevenZipFormat Vhd = new SevenZipFormat(new string[] { "vhd" }, new Guid("23170f69-40c1-278a-1000-000110DC0000"), new byte[] { 0x63, 0x6F, 0x6E, 0x65, 0x63, 0x74, 0x69, 0x78 });
        public static SevenZipFormat SquashFS = new SevenZipFormat(new string[] { "" }, new Guid("23170f69-40c1-278a-1000-000110D20000"), new byte[] { });
        public static SevenZipFormat Lzma86 = new SevenZipFormat(new string[] { "" }, new Guid("23170f69-40c1-278a-1000-0001100B0000"), new byte[] { });
        public static SevenZipFormat Ppmd = new SevenZipFormat(new string[] { "" }, new Guid("23170f69-40c1-278a-1000-0001100D0000"), new byte[] { });
        public static SevenZipFormat TE = new SevenZipFormat(new string[] { "" }, new Guid("23170f69-40c1-278a-1000-000110CF0000"), new byte[] { });
        public static SevenZipFormat UEFIc = new SevenZipFormat(new string[] { "" }, new Guid("23170f69-40c1-278a-1000-000110D00000"), new byte[] { });
        public static SevenZipFormat UEFIs = new SevenZipFormat(new string[] { "" }, new Guid("23170f69-40c1-278a-1000-000110D10000"), new byte[] { });
        public static SevenZipFormat CramFS = new SevenZipFormat(new string[] { "" }, new Guid("23170f69-40c1-278a-1000-000110D30000"), new byte[] { });
        public static SevenZipFormat APM = new SevenZipFormat(new string[] { "" }, new Guid("23170f69-40c1-278a-1000-000110D40000"), new byte[] { });
        public static SevenZipFormat Swfc = new SevenZipFormat(new string[] { "" }, new Guid("23170f69-40c1-278a-1000-000110D80000"), new byte[] { });
        public static SevenZipFormat Ntfs = new SevenZipFormat(new string[] { "" }, new Guid("23170f69-40c1-278a-1000-000110D90000"), new byte[] { });
        public static SevenZipFormat Fat = new SevenZipFormat(new string[] { "" }, new Guid("23170f69-40c1-278a-1000-000110DA0000"), new byte[] { });
        public static SevenZipFormat Mbr = new SevenZipFormat(new string[] { "" }, new Guid("23170f69-40c1-278a-1000-000110DB0000"), new byte[] { });
        public static SevenZipFormat MachO = new SevenZipFormat(new string[] { "" }, new Guid("23170f69-40c1-278a-1000-000110DF0000"), new byte[] { });
        private static List<SevenZipFormat> _values;

        static SevenZipFormat()
        {
            if (SevenZipFormat._values == null)
                SevenZipFormat._values = new List<SevenZipFormat>();
        }


        private string[] _extensions;
        private Guid _guid;
        private byte[] _bytes;
        private bool _isSet;

        private SevenZipFormat(string[] extensions, Guid guid, byte[] bytes)
        {
            this._extensions = extensions;
            this._guid = guid;
            this._bytes = bytes;
            this._isSet = true;

            if (SevenZipFormat._values == null)
                SevenZipFormat._values = new List<SevenZipFormat>();
            SevenZipFormat._values.Add(this);
        }

        public Guid Guid
        {
            get
            {
                if (this._isSet)
                    return this._guid;
                else
                    return SevenZipFormat.None.Guid;
            }
            private set { }
        }
        public string[] Extensions
        {
            get
            {
                if (this._isSet)
                    return this._extensions;
                else
                    return SevenZipFormat.None.Extensions;
            }
            private set { }
        }
        public byte[] Bytes
        {
            get
            {
                if (this._isSet)
                    return this._bytes;
                else
                    return SevenZipFormat.None.Bytes;
            }
            private set { }
        }
        //public static implicit operator byte(Digit d) => d.digit;
        public static SevenZipFormat[] Values
        {
            get { return SevenZipFormat._values.ToArray(); }
            private set { }
        }

        public static IEnumerable<string> ZipExtensions
        {
            get
            {
                foreach (SevenZipFormat item in SevenZipFormat._values)
                    foreach (string extension in item.Extensions)
                        if (!string.IsNullOrWhiteSpace(extension))
                            yield return string.Concat('.', extension);
            }
        }
    }

    //public enum KnownSevenZipFormat
    //{
    //    SevenZip,
    //    Arj,
    //    BZip2,
    //    Cab,
    //    Chm,
    //    Compound,
    //    Cpio,
    //    Deb,
    //    GZip,
    //    Iso,
    //    Lzh,
    //    Lzma,
    //    Nsis,
    //    Rar,
    //    Rar5,
    //    Rpm,
    //    Split,
    //    Tar,
    //    Wim,
    //    Lzw,
    //    Z,
    //    Zip,
    //    Udf,
    //    Xar,
    //    Mub,
    //    Hfs,
    //    Dmg,
    //    XZ,
    //    Mslz,
    //    //Flv,
    //    Swf,
    //    PE,
    //    Elf,
    //    Msi,
    //    Vhd,
    //    SquashFS,
    //    Lzma86,
    //    Ppmd,
    //    TE,
    //    UEFIc,
    //    UEFIs,
    //    CramFS,
    //    APM,
    //    Swfc,
    //    Ntfs,
    //    Fat,
    //    Mbr,
    //    MachO
    //}

    public class SevenZipLoader : IDisposable
    {
        #region Win32 API

        private const string Kernel32Dll = "kernel32.dll";

        private sealed class SafeLibraryHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
            public SafeLibraryHandle() : base(true) { }

            [SuppressUnmanagedCodeSecurity]
            [DllImport(Kernel32Dll)]
            [return: MarshalAs(UnmanagedType.Bool)]
            private static extern bool FreeLibrary(IntPtr hModule);

            /// <summary>Release library handle</summary>
            /// <returns>true if the handle was released</returns>
            [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
            protected override bool ReleaseHandle()
            {
                return FreeLibrary(handle);
            }
        }

        [DllImport(Kernel32Dll, CharSet = CharSet.Auto, SetLastError = true)]
        private static extern SafeLibraryHandle LoadLibrary(
          [MarshalAs(UnmanagedType.LPTStr)] string lpFileName);

        [DllImport(Kernel32Dll, CharSet = CharSet.Ansi, SetLastError = true)]
        private static extern IntPtr GetProcAddress(
          SafeLibraryHandle hModule,
          [MarshalAs(UnmanagedType.LPStr)] string procName);

        #endregion

        private SafeLibraryHandle LibHandle;

        public SevenZipLoader(string sevenZipLibPath)
        {
            LibHandle = LoadLibrary(sevenZipLibPath);
            if (LibHandle.IsInvalid)
                throw new Win32Exception();

            IntPtr FunctionPtr = GetProcAddress(LibHandle, "GetHandlerProperty");
            // Not valid dll
            if (FunctionPtr == IntPtr.Zero)
            {
                LibHandle.Close();
                throw new ArgumentException();
            }
        }

        ~SevenZipLoader()
        {
            Dispose(false);
        }

        protected void Dispose(bool disposing)
        {
            if ((LibHandle != null) && !LibHandle.IsClosed)
                LibHandle.Close();
            LibHandle = null;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IInArchive CreateInArchive(Guid classId)
        {
            if (LibHandle == null)
                throw new ObjectDisposedException("SevenZipFormat");

            CreateObjectDelegate CreateObject =
              (CreateObjectDelegate)Marshal.GetDelegateForFunctionPointer(
              GetProcAddress(LibHandle, "CreateObject"), typeof(CreateObjectDelegate));

            if (CreateObject != null)
            {
                object Result;
                Guid InterfaceId = typeof(IInArchive).GUID;
                CreateObject(ref classId, ref InterfaceId, out Result);
                return Result as IInArchive;
            }

            return null;
        }

        //private static Dictionary<KnownSevenZipFormat, Guid> FFormatClassMap;
        //private static Dictionary<KnownSevenZipFormat, Guid> FormatClassMap
        //{
        //    get
        //    {
        //        if (FFormatClassMap == null)
        //        {
        //            FFormatClassMap = new Dictionary<KnownSevenZipFormat, Guid>();
        //            FFormatClassMap.Add(KnownSevenZipFormat.SevenZip, new Guid("23170f69-40c1-278a-1000-000110070000"));
        //            FFormatClassMap.Add(KnownSevenZipFormat.Arj, new Guid("23170f69-40c1-278a-1000-000110040000"));
        //            FFormatClassMap.Add(KnownSevenZipFormat.BZip2, new Guid("23170f69-40c1-278a-1000-000110020000"));
        //            FFormatClassMap.Add(KnownSevenZipFormat.Cab, new Guid("23170f69-40c1-278a-1000-000110080000"));
        //            FFormatClassMap.Add(KnownSevenZipFormat.Chm, new Guid("23170f69-40c1-278a-1000-000110e90000"));
        //            FFormatClassMap.Add(KnownSevenZipFormat.Compound, new Guid("23170f69-40c1-278a-1000-000110e50000"));
        //            FFormatClassMap.Add(KnownSevenZipFormat.Cpio, new Guid("23170f69-40c1-278a-1000-000110ed0000"));
        //            FFormatClassMap.Add(KnownSevenZipFormat.Deb, new Guid("23170f69-40c1-278a-1000-000110ec0000"));
        //            FFormatClassMap.Add(KnownSevenZipFormat.GZip, new Guid("23170f69-40c1-278a-1000-000110ef0000"));
        //            FFormatClassMap.Add(KnownSevenZipFormat.Iso, new Guid("23170f69-40c1-278a-1000-000110e70000"));
        //            FFormatClassMap.Add(KnownSevenZipFormat.Lzh, new Guid("23170f69-40c1-278a-1000-000110060000"));
        //            FFormatClassMap.Add(KnownSevenZipFormat.Lzma, new Guid("23170f69-40c1-278a-1000-0001100a0000"));
        //            FFormatClassMap.Add(KnownSevenZipFormat.Nsis, new Guid("23170f69-40c1-278a-1000-000110090000"));
        //            FFormatClassMap.Add(KnownSevenZipFormat.Rar, new Guid("23170f69-40c1-278a-1000-000110030000"));
        //            FFormatClassMap.Add(KnownSevenZipFormat.Rpm, new Guid("23170f69-40c1-278a-1000-000110eb0000"));
        //            FFormatClassMap.Add(KnownSevenZipFormat.Split, new Guid("23170f69-40c1-278a-1000-000110ea0000"));
        //            FFormatClassMap.Add(KnownSevenZipFormat.Tar, new Guid("23170f69-40c1-278a-1000-000110ee0000"));
        //            FFormatClassMap.Add(KnownSevenZipFormat.Wim, new Guid("23170f69-40c1-278a-1000-000110e60000"));
        //            FFormatClassMap.Add(KnownSevenZipFormat.Z, new Guid("23170f69-40c1-278a-1000-000110050000"));
        //            FFormatClassMap.Add(KnownSevenZipFormat.Zip, new Guid("23170f69-40c1-278a-1000-000110010000"));
        //        }
        //        return FFormatClassMap;
        //    }
        //}

        //public static Guid GetClassIdFromKnownFormat(SevenZipFormat format)
        //{
        //    //Guid Result;
        //    //if (FormatClassMap.TryGetValue(format, out Result))
        //    //    return Result;
        //    return Guid.Empty;
        //}
    }
}

