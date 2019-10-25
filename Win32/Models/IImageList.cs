using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Win32.Models
{
    [Guid("46EB5926-582E-4017-9FDF-E8998DAA0950")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [ComImport]
    public interface IImageList
    {
        [MethodImpl(MethodImplOptions.PreserveSig)]
        int Add(IntPtr hbmImage, IntPtr hbmMask, ref int pi);

        [MethodImpl(MethodImplOptions.PreserveSig)]
        int ReplaceIcon(int i, IntPtr hicon, ref int pi);

        [MethodImpl(MethodImplOptions.PreserveSig)]
        int SetOverlayImage(int iImage, int iOverlay);

        [MethodImpl(MethodImplOptions.PreserveSig)]
        int Replace(int i, IntPtr hbmImage, IntPtr hbmMask);

        [MethodImpl(MethodImplOptions.PreserveSig)]
        int AddMasked(IntPtr hbmImage, int crMask, ref int pi);

        [MethodImpl(MethodImplOptions.PreserveSig)]
        int Draw(ref IMAGELISTDRAWPARAMS pimldp);

        [MethodImpl(MethodImplOptions.PreserveSig)]
        int Remove(int i);

        [MethodImpl(MethodImplOptions.PreserveSig)]
        int GetIcon(int i, int flags, ref IntPtr picon);

        [MethodImpl(MethodImplOptions.PreserveSig)]
        int GetImageInfo(int i, ref IMAGEINFO pImageInfo);

        [MethodImpl(MethodImplOptions.PreserveSig)]
        int Copy(int iDst, IImageList punkSrc, int iSrc, int uFlags);

        [MethodImpl(MethodImplOptions.PreserveSig)]
        int Merge(
          int i1,
          IImageList punk2,
          int i2,
          int dx,
          int dy,
          ref Guid riid,
          ref IntPtr ppv);

        [MethodImpl(MethodImplOptions.PreserveSig)]
        int Clone(ref Guid riid, ref IntPtr ppv);

        [MethodImpl(MethodImplOptions.PreserveSig)]
        int GetImageRect(int i, ref RECT prc);

        [MethodImpl(MethodImplOptions.PreserveSig)]
        int GetIconSize(ref int cx, ref int cy);

        [MethodImpl(MethodImplOptions.PreserveSig)]
        int SetIconSize(int cx, int cy);

        [MethodImpl(MethodImplOptions.PreserveSig)]
        int GetImageCount(ref int pi);

        [MethodImpl(MethodImplOptions.PreserveSig)]
        int SetImageCount(int uNewCount);

        [MethodImpl(MethodImplOptions.PreserveSig)]
        int SetBkColor(int clrBk, ref int pclr);

        [MethodImpl(MethodImplOptions.PreserveSig)]
        int GetBkColor(ref int pclr);

        [MethodImpl(MethodImplOptions.PreserveSig)]
        int BeginDrag(int iTrack, int dxHotspot, int dyHotspot);

        [MethodImpl(MethodImplOptions.PreserveSig)]
        int EndDrag();

        [MethodImpl(MethodImplOptions.PreserveSig)]
        int DragEnter(IntPtr hwndLock, int x, int y);

        [MethodImpl(MethodImplOptions.PreserveSig)]
        int DragLeave(IntPtr hwndLock);

        [MethodImpl(MethodImplOptions.PreserveSig)]
        int DragMove(int x, int y);

        [MethodImpl(MethodImplOptions.PreserveSig)]
        int SetDragCursorImage(
          ref IImageList punk,
          int iDrag,
          int dxHotspot,
          int dyHotspot);

        [MethodImpl(MethodImplOptions.PreserveSig)]
        int DragShowNolock(int fShow);

        [MethodImpl(MethodImplOptions.PreserveSig)]
        int GetDragImage(
          ref POINT ppt,
          ref POINT pptHotspot,
          ref Guid riid,
          ref IntPtr ppv);

        [MethodImpl(MethodImplOptions.PreserveSig)]
        int GetItemFlags(int i, ref int dwFlags);

        [MethodImpl(MethodImplOptions.PreserveSig)]
        int GetOverlayImage(int iOverlay, ref int piIndex);
    }
}
