using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Common.Extensions
{
    public static class DrawingExtensions
    {
        public static Bitmap ToBitmap(this BitmapSource imgsrc)
        {
            int pixelWidth = imgsrc.PixelWidth;
            int pixelHeight = imgsrc.PixelHeight;
            int stride = pixelWidth * ((imgsrc.Format.BitsPerPixel + 7) / 8);
            IntPtr num = Marshal.AllocHGlobal(pixelHeight * stride);
            imgsrc.CopyPixels(new Int32Rect(0, 0, pixelWidth, pixelHeight), num, pixelHeight * stride, stride);
            return new Bitmap(pixelWidth, pixelHeight, stride, PixelFormat.Format32bppArgb, num);
        }
    }
}
