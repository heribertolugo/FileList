
using System;

namespace Win32.Constants
{
    [Flags]
    public enum ImageListDrawItem
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
}
