using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FileList.Models.Win32
{

    [Flags]
    internal enum ImageListDrawItemConstants
    {
        ILD_NORMAL = 0,
        ILD_TRANSPARENT = 1,
        ILD_BLEND25 = 2,
        ILD_SELECTED = 4,
        ILD_MASK = 16, // 0x00000010
        ILD_IMAGE = 32, // 0x00000020
        ILD_ROP = 64, // 0x00000040
        ILD_PRESERVEALPHA = 4096, // 0x00001000
        ILD_SCALE = 8192, // 0x00002000
        ILD_DPISCALE = 16384, // 0x00004000
    }

    [Flags]
    internal enum ImageListDrawStateConstants
    {
        ILS_NORMAL = 0,
        ILS_GLOW = 1,
        ILS_SHADOW = 2,
        ILS_SATURATE = 4,
        ILS_ALPHA = 8,
    }

    internal enum SysImageListSize
    {
        largeIcons = 0,
        smallIcons = 1,
        extraLargeIcons = 2,
        jumbo = 4,
    }

    [Flags]
    internal enum ShellIconStateConstants
    {
        ShellIconStateNormal = 0,
        ShellIconStateLinkOverlay = 32768, // 0x00008000
        ShellIconStateSelected = 65536, // 0x00010000
        ShellIconStateOpen = 2,
        ShellIconAddOverlays = 32, // 0x00000020
    }

    [Flags]
    internal enum SHGetFileInfoConstants
    {
        SHGFI_ICON = 256, // 0x00000100
        SHGFI_DISPLAYNAME = 512, // 0x00000200
        SHGFI_TYPENAME = 1024, // 0x00000400
        SHGFI_ATTRIBUTES = 2048, // 0x00000800
        SHGFI_ICONLOCATION = 4096, // 0x00001000
        SHGFI_EXETYPE = 8192, // 0x00002000
        SHGFI_SYSICONINDEX = 16384, // 0x00004000
        SHGFI_LINKOVERLAY = 32768, // 0x00008000
        SHGFI_SELECTED = 65536, // 0x00010000
        SHGFI_ATTR_SPECIFIED = 131072, // 0x00020000
        SHGFI_LARGEICON = 0,
        SHGFI_SMALLICON = 1,
        SHGFI_OPENICON = 2,
        SHGFI_SHELLICONSIZE = 4,
        SHGFI_USEFILEATTRIBUTES = 16, // 0x00000010
        SHGFI_ADDOVERLAYS = 32, // 0x00000020
        SHGFI_OVERLAYINDEX = 64, // 0x00000040
    }

    internal static class Win32Enums
    {
        internal const int CS_DROPSHADOW = 131072;
        internal const int WM_NCPAINT = 133;
        internal const int WM_NCHITTEST = 132;
        internal const int WM_ACTIVATEAPP = 28;
        internal const int HTCAPTION = 2;
        internal const int HTCLIENT = 1;

        internal const uint SHGFI_SYSICONINDEX = 16384;
        internal const uint SHGFI_TYPENAME = 1024;
        internal const uint SHGFI_ICON = 256;
        internal const uint SHGFI_USEFILEATTRIBUTES = 16;
        internal const uint SHGFI_SMALLICON = 1;
        internal const uint SHGFI_LARGEICON = 0;

        internal const int MAX_PATH = 260;
        internal const int FILE_ATTRIBUTE_NORMAL = 128;
        internal const int FILE_ATTRIBUTE_DIRECTORY = 16;
        internal const int FORMAT_MESSAGE_ALLOCATE_BUFFER = 256;
        internal const int FORMAT_MESSAGE_ARGUMENT_ARRAY = 8192;
        internal const int FORMAT_MESSAGE_FROM_HMODULE = 2048;
        internal const int FORMAT_MESSAGE_FROM_STRING = 1024;
        internal const int FORMAT_MESSAGE_FROM_SYSTEM = 4096;
        internal const int FORMAT_MESSAGE_IGNORE_INSERTS = 512;
        internal const int FORMAT_MESSAGE_MAX_WIDTH_MASK = 255;
    }
}
