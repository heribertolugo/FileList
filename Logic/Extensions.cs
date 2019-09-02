
using System;
using System.Collections.Generic;
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

        public static IEnumerable<string> AccessableDirectories(string path)
        {
            List<string> accessable = new List<string>();
            try
            {
                string[] directories = System.IO.Directory.GetDirectories(path);

                foreach (string directory in directories)
                {
                    if (Extensions.IsSystemObjectAccessable(directory))
                        accessable.Add(directory);
                }
            }
            catch (Exception ex)
            {

            }

            return accessable;
        }
        public static IEnumerable<string> AccessableFiles(string path)
        {
            List<string> accessable = new List<string>();
            try
            {
                string[] files = System.IO.Directory.GetFiles(path);

                foreach (string file in files)
                {
                    if (Extensions.IsSystemObjectAccessable(file))
                        accessable.Add(file);
                }
            }
            catch (Exception ex)
            {

            }

            return accessable;
        }

        public static bool IsSystemObjectAccessable(string path)
        {
            try
            {
                if (System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.GetDirectories(path);
                    System.IO.Directory.GetFiles(path);
                }
                else if (System.IO.File.Exists(path))
                {
                    System.IO.FileStream stream = System.IO.File.Open(path, System.IO.FileMode.Open,
                                                System.IO.FileAccess.Read, System.IO.FileShare.None);
                    stream.Close();
                    //using (System.IO.FileStream reader = new System.IO.FileStream(path, System.IO.FileMode.Open))
                    //{
                    //    byte[] bytes = new byte[1];
                    //    reader.Read(bytes, 0, 1);
                    //}
                }
                else
                {
                    throw new Exception();
                }
                return true;
            }
            catch (Exception ex)
            {
            }

            return false;
        }

        public static IntPtr ToIntPtr(this object target)
        {
            return (IntPtr)GCHandle.Alloc(target);
        }

        public static GCHandle ToGcHandle(this object target)
        {
            return GCHandle.Alloc(target);
        }

        //public static IntPtr ToIntPtr(this GCHandle target)
        //{
        //    return GCHandle.ToIntPtr(target);
        //}

        public static T ToObject<T>(IntPtr ptr)
        {
            try {
                GCHandle gch = GCHandle.FromIntPtr(ptr);
                T t = (T)(gch.Target);
                gch.Free();
                return t;
            }catch(Exception ex)
            {

            }
            return default(T);
        }
    }
}