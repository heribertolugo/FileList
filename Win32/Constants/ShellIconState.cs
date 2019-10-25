using System;

namespace Win32.Constants
{
    [Flags]
    public enum ShellIconState
    {
        ShellIconStateNormal = 0,
        ShellIconStateLinkOverlay = 32768, // 0x00008000
        ShellIconStateSelected = 65536, // 0x00010000
        ShellIconStateOpen = 2,
        ShellIconAddOverlays = 32, // 0x00000020
    }
}
