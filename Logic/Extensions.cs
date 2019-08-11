
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media.Imaging;

namespace FileList
{
    public static class Extensions
    {
        public static void InvokeIfRequired(this ISynchronizeInvoke obj, MethodInvoker action)
        {
            if (obj.InvokeRequired)
            {
                object[] args = new object[0];
                obj.Invoke((Delegate)action, args);
            }
            else
                action();
        }

        public static void InvokeIfRequired<T>(
          this T obj,
          Extensions.InvokeIfRequiredDelegate<T> action)
          where T : ISynchronizeInvoke
        {
            if (obj.InvokeRequired)
                obj.Invoke((Delegate)action, new object[1]
                {
          (object) obj
                });
            else
                action(obj);
        }

        public static Bitmap ToBitmap(this BitmapSource imgsrc)
        {
            int pixelWidth = imgsrc.PixelWidth;
            int pixelHeight = imgsrc.PixelHeight;
            int stride = pixelWidth * ((imgsrc.Format.BitsPerPixel + 7) / 8);
            IntPtr num = Marshal.AllocHGlobal(pixelHeight * stride);
            imgsrc.CopyPixels(new Int32Rect(0, 0, pixelWidth, pixelHeight), num, pixelHeight * stride, stride);
            return new Bitmap(pixelWidth, pixelHeight, stride, PixelFormat.Format32bppArgb, num);
        }

        public delegate void InvokeIfRequiredDelegate<T>(T obj) where T : ISynchronizeInvoke;
    }
}