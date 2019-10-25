using Common.Helpers;
using FileList.Models;
using FileList.Models.ImageList;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Win32;
using Win32.Constants;
using Win32.Libraries;
using Win32.Models;

namespace FileList.Logic
{

    public class FileToIconConverter : IMultiValueConverter
    {
        private static string imageFilter = ".jpg,.jpeg,.png,.gif";
        private static string exeFilter = ".exe,.lnk";
        private static Dictionary<string, BitmapSource> iconDic = new Dictionary<string, BitmapSource>();
        private static SysImageList _imgList = new SysImageList(SysImageListSize.jumbo);
        private static Dictionary<string, BitmapSource> thumbDic = new Dictionary<string, BitmapSource>();
        private int defaultsize;

        public int DefaultSize
        {
            get
            {
                return this.defaultsize;
            }
            set
            {
                this.defaultsize = value;
            }
        }

        internal static Icon GetFileIcon(string fileName, FileToIconConverter.IconSize size)
        {
            SHFILEINFO psfi = new SHFILEINFO();
            uint num = ShellIconFlags.SHGFI_SYSICONINDEX; 
            if (fileName.IndexOf(":") == -1)
                num |= ShellIconFlags.SHGFI_USEFILEATTRIBUTES; 
            uint uFlags = size != FileToIconConverter.IconSize.Small ? num | ShellIconFlags.SHGFI_ICON : (uint)((int)num | ShellIconFlags.SHGFI_ICON | ShellIconFlags.SHGFI_SMALLICON);
            shell32.SHGetFileInfo(fileName, ShellIconFlags.SHGFI_LARGEICON, ref psfi, (uint)Marshal.SizeOf(psfi), uFlags);
            return Icon.FromHandle(psfi.hIcon);
        }

        private static void copyBitmap(BitmapSource source, WriteableBitmap target, bool dispatcher)
        {
            int width = source.PixelWidth;
            int height = source.PixelHeight;
            int stride = width * ((source.Format.BitsPerPixel + 7) / 8);
            byte[] bits = new byte[height * stride];
            source.CopyPixels(bits, stride, 0);
            source = null;

            if (dispatcher)
            {
                target.Dispatcher.BeginInvoke((Action)(() =>
                {
                    double h = target.Height - (double)height;
                    int w = (double)width > target.Width ? (int)target.Width : width;
                    //int num3 = (double)height > target.Height ? (int)target.Height : height;
                    Int32Rect sourceRect = new Int32Rect(0, (h >= 0.0 ? (int)h : 0) / 2, w, w);
                    try
                    {
                        target.WritePixels(sourceRect, bits, stride, 0);
                    }
                    catch (Exception ex)
                    {
                        Debugger.Break();
                    }
                }), DispatcherPriority.Background, null);
            }
            else
            {
                double h = target.Height - (double)height;
                int w = (double)width > target.Width ? (int)target.Width : width;
                //int num3 = (double)height > target.Height ? (int)target.Height : height;
                Int32Rect sourceRect = new Int32Rect(0, (h >= 0.0 ? (int)h : 0) / 2, w, w);
                try
                {
                    target.WritePixels(sourceRect, (Array)bits, stride, 0);
                }
                catch (Exception ex)
                {
                    Debugger.Break();
                }
            }
        }

        private static System.Drawing.Size getDefaultSize(FileToIconConverter.IconSize size)
        {
            switch (size)
            {
                case FileToIconConverter.IconSize.Large:
                    return new System.Drawing.Size(32, 32);
                case FileToIconConverter.IconSize.ExtraLarge:
                    return new System.Drawing.Size(48, 48);
                case FileToIconConverter.IconSize.Jumbo:
                    return new System.Drawing.Size(256, 256);
                case FileToIconConverter.IconSize.Thumbnail:
                    return new System.Drawing.Size(256, 256);
                default:
                    return new System.Drawing.Size(16, 16);
            }
        }

        private static Bitmap resizeImage(Bitmap imgToResize, System.Drawing.Size size, int spacing)
        {
            int width1 = imgToResize.Width;
            int height1 = imgToResize.Height;
            float num1 = (float)size.Width / (float)width1;
            float num2 = (float)size.Height / (float)height1;
            float num3 = (double)num2 >= (double)num1 ? num1 : num2;
            int width2 = (int)((double)width1 * (double)num3 - (double)(spacing * 4));
            int height2 = (int)((double)height1 * (double)num3 - (double)(spacing * 4));
            int x = (size.Width - width2) / 2;
            int y = (size.Height - height2) / 2;
            Bitmap bitmap = new Bitmap(size.Width, size.Height);
            Graphics graphics = Graphics.FromImage((Image)bitmap);
            graphics.InterpolationMode = InterpolationMode.High;
            graphics.DrawLines(Pens.Silver, new PointF[3]
            {
        new PointF((float) (x - spacing), (float) (y + height2 + spacing)),
        new PointF((float) (x - spacing), (float) (y - spacing)),
        new PointF((float) (x + width2 + spacing), (float) (y - spacing))
            });
            graphics.DrawLines(Pens.Gray, new PointF[3]
            {
        new PointF((float) (x + width2 + spacing), (float) (y - spacing)),
        new PointF((float) (x + width2 + spacing), (float) (y + height2 + spacing)),
        new PointF((float) (x - spacing), (float) (y + height2 + spacing))
            });
            graphics.DrawImage((Image)imgToResize, x, y, width2, height2);
            graphics.Dispose();
            return bitmap;
        }

        private static Bitmap resizeJumbo(Bitmap imgToResize, System.Drawing.Size size, int spacing)
        {
            int width1 = imgToResize.Width;
            int height1 = imgToResize.Height;
            float num1 = 0.0f;
            float num2 = (float)size.Width / (float)width1;
            float num3 = (float)size.Height / (float)height1;
            num1 = (double)num3 >= (double)num2 ? num2 : num3;
            int width2 = 80;
            int height2 = 80;
            int x = (size.Width - width2) / 2;
            int y = (size.Height - height2) / 2;
            Bitmap bitmap = new Bitmap(size.Width, size.Height);
            Graphics graphics = Graphics.FromImage((Image)bitmap);
            graphics.InterpolationMode = InterpolationMode.High;
            graphics.DrawLines(Pens.Silver, new PointF[3]
            {
        new PointF((float) spacing, (float) (size.Height - spacing)),
        new PointF((float) spacing, (float) spacing),
        new PointF((float) (size.Width - spacing), (float) spacing)
            });
            graphics.DrawLines(Pens.Gray, new PointF[3]
            {
        new PointF((float) (size.Width - spacing), (float) spacing),
        new PointF((float) (size.Width - spacing), (float) (size.Height - spacing)),
        new PointF((float) spacing, (float) (size.Height - spacing))
            });
            graphics.DrawImage((Image)imgToResize, x, y, width2, height2);
            graphics.Dispose();
            return bitmap;
        }

        private static BitmapSource loadBitmap(Bitmap source)
        {
            IntPtr hbitmap = source.GetHbitmap();
            try
            {
                return System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(hbitmap, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            }
            finally
            {
               Gdi32.DeleteObject(hbitmap);
            }
        }

        private static bool isImage(string fileName)
        {
            string lower = Path.GetExtension(fileName).ToLower();
            if (lower == "")
                return false;
            return FileToIconConverter.imageFilter.IndexOf(lower) != -1 && File.Exists(fileName);
        }

        private static bool isExecutable(string fileName)
        {
            string lower = Path.GetExtension(fileName).ToLower();
            if (lower == "")
                return false;
            return FileToIconConverter.exeFilter.IndexOf(lower) != -1 && File.Exists(fileName);
        }

        private static bool isFolder(string path)
        {
            return path.EndsWith("\\") || Directory.Exists(path);
        }

        private static string returnKey(string fileName, FileToIconConverter.IconSize size)
        {
            string lower = Path.GetExtension(fileName).ToLower();
            if (FileToIconConverter.isExecutable(fileName))
                lower = fileName.ToLower();
            if (FileToIconConverter.isImage(fileName) && size == FileToIconConverter.IconSize.Thumbnail)
                lower = fileName.ToLower();
            if (FileToIconConverter.isFolder(fileName))
                lower = fileName.ToLower();
            switch (size)
            {
                case FileToIconConverter.IconSize.Small:
                    lower += "+S";
                    break;
                case FileToIconConverter.IconSize.Large:
                    lower += "+L";
                    break;
                case FileToIconConverter.IconSize.ExtraLarge:
                    lower += "+XL";
                    break;
                case FileToIconConverter.IconSize.Jumbo:
                    lower += "+J";
                    break;
                case FileToIconConverter.IconSize.Thumbnail:
                    lower += FileToIconConverter.isImage(fileName) ? "+T" : "+J";
                    break;
            }
            return lower;
        }

        private Bitmap loadJumbo(string lookup)
        {
            FileToIconConverter._imgList.ImageListSize = FileToIconConverter.isVistaUp() ? SysImageListSize.jumbo : SysImageListSize.extraLargeIcons;
            Icon icon = FileToIconConverter._imgList.Icon(FileToIconConverter._imgList.IconIndex(lookup, FileToIconConverter.isFolder(lookup)));
            Bitmap imgToResize = icon.ToBitmap();
            icon.Dispose();
            Color color = Color.FromArgb(0, 0, 0, 0);
            if (imgToResize.Width < 256)
                imgToResize = FileToIconConverter.resizeImage(imgToResize, new System.Drawing.Size(256, 256), 0);
            else if (imgToResize.GetPixel(100, 100) == color && imgToResize.GetPixel(200, 200) == color && imgToResize.GetPixel(200, 200) == color)
            {
                FileToIconConverter._imgList.ImageListSize = SysImageListSize.largeIcons;
                imgToResize = FileToIconConverter.resizeJumbo(FileToIconConverter._imgList.Icon(FileToIconConverter._imgList.IconIndex(lookup)).ToBitmap(), new System.Drawing.Size(200, 200), 5);
            }
            return imgToResize;
        }

        public void ClearInstanceCache()
        {
            FileToIconConverter.thumbDic.Clear();
        }

        private void PollIconCallback(object state)
        {
            FileToIconConverter.thumbnailInfo thumbnailInfo = state as FileToIconConverter.thumbnailInfo;
            string fullPath = thumbnailInfo.fullPath;
            WriteableBitmap bitmap1 = thumbnailInfo.bitmap;
            FileToIconConverter.IconSize iconsize = thumbnailInfo.iconsize;
            Bitmap bitmap2 = FileToIconConverter.GetFileIcon(fullPath, iconsize).ToBitmap();
            Bitmap source1 = iconsize != FileToIconConverter.IconSize.Jumbo && iconsize != FileToIconConverter.IconSize.Thumbnail ? FileToIconConverter.resizeImage(bitmap2, FileToIconConverter.getDefaultSize(iconsize), 0) : FileToIconConverter.resizeJumbo(bitmap2, FileToIconConverter.getDefaultSize(iconsize), 5);
            BitmapSource source2 = FileToIconConverter.loadBitmap(source1);
            bitmap2.Dispose();
            source1.Dispose();
            FileToIconConverter.copyBitmap(source2, bitmap1, true);
        }

        private void PollThumbnailCallback(object state)
        {
            FileToIconConverter.thumbnailInfo thumbnailInfo = state as FileToIconConverter.thumbnailInfo;
            string fullPath = thumbnailInfo.fullPath;
            WriteableBitmap bitmap = thumbnailInfo.bitmap;
            FileToIconConverter.IconSize iconsize = thumbnailInfo.iconsize;
            try
            {
                Bitmap imgToResize = new Bitmap(fullPath);
                Bitmap source1 = FileToIconConverter.resizeImage(imgToResize, FileToIconConverter.getDefaultSize(iconsize), 5);
                BitmapSource source2 = FileToIconConverter.loadBitmap(source1);
                imgToResize.Dispose();
                source1.Dispose();
                FileToIconConverter.copyBitmap(source2, bitmap, true);
            }
            catch
            {
            }
        }

        private BitmapSource addToDic(string fileName, FileToIconConverter.IconSize size)
        {
            string key = FileToIconConverter.returnKey(fileName, size);
            if (size == FileToIconConverter.IconSize.Thumbnail || FileToIconConverter.isExecutable(fileName))
            {
                if (!FileToIconConverter.thumbDic.ContainsKey(key))
                {
                    IoHelper.WriteToConsole("requesting thumbDic lock");
                    lock (FileToIconConverter.thumbDic)
                        FileToIconConverter.thumbDic.Add(key, this.getImage(fileName, size));
                    IoHelper.WriteToConsole("thumbDic lock released");
                }
                return FileToIconConverter.thumbDic[key];
            }
            if (!FileToIconConverter.iconDic.ContainsKey(key))
            {
                IoHelper.WriteToConsole("requesting iconDic lock");
                lock (FileToIconConverter.iconDic)
                    FileToIconConverter.iconDic.Add(key, this.getImage(fileName, size));
                IoHelper.WriteToConsole("iconDic lock released");
            }
            return FileToIconConverter.iconDic[key];
        }

        public BitmapSource GetImage(string fileName, int iconSize)
        {
            FileToIconConverter.IconSize size = iconSize > 16 ? (iconSize > 32 ? (iconSize > 48 ? (iconSize > 72 ? FileToIconConverter.IconSize.Thumbnail : FileToIconConverter.IconSize.Jumbo) : FileToIconConverter.IconSize.ExtraLarge) : FileToIconConverter.IconSize.Large) : FileToIconConverter.IconSize.Small;
            return this.addToDic(fileName, size);
        }

        public BitmapSource GetImage(string fileName, FileToIconConverter.IconSize iconSize)
        {
            return this.addToDic(fileName, iconSize);
        }

        public Icon GetIcon(string fileName, FileToIconConverter.IconSize iconSize)
        {
            string str = FileToIconConverter.returnKey(fileName, iconSize);
            string fileName1 = "aaa" + Path.GetExtension(fileName).ToLower();
            if (!str.StartsWith("."))
                fileName1 = fileName;
            return FileToIconConverter.GetFileIcon(fileName1, iconSize);
        }

        public static bool isVistaUp()
        {
            return Environment.OSVersion.Version.Major >= 6;
        }

        private BitmapSource getImage(string fileName, FileToIconConverter.IconSize size)
        {
            string str1 = FileToIconConverter.returnKey(fileName, size);
            string str2 = "aaa" + Path.GetExtension(fileName).ToLower();
            if (!str1.StartsWith("."))
                str2 = fileName;
            if (FileToIconConverter.isExecutable(fileName))
            {
                WriteableBitmap b = new WriteableBitmap(this.addToDic("aaa.exe", size));
                ThreadPool.QueueUserWorkItem(new WaitCallback(this.PollIconCallback), (object)new FileToIconConverter.thumbnailInfo(b, fileName, size));
                return (BitmapSource)b;
            }
            switch (size)
            {
                case FileToIconConverter.IconSize.ExtraLarge:
                    FileToIconConverter._imgList.ImageListSize = SysImageListSize.extraLargeIcons;
                    return FileToIconConverter.loadBitmap(FileToIconConverter._imgList.Icon(FileToIconConverter._imgList.IconIndex(str2, FileToIconConverter.isFolder(fileName))).ToBitmap());
                case FileToIconConverter.IconSize.Jumbo:
                    return FileToIconConverter.loadBitmap(this.loadJumbo(str2));
                case FileToIconConverter.IconSize.Thumbnail:
                    if (!FileToIconConverter.isImage(fileName))
                        return this.getImage(str2, FileToIconConverter.IconSize.Jumbo);
                    WriteableBitmap b1 = new WriteableBitmap(this.addToDic(fileName, FileToIconConverter.IconSize.Jumbo));
                    ThreadPool.QueueUserWorkItem(new WaitCallback(this.PollThumbnailCallback), (object)new FileToIconConverter.thumbnailInfo(b1, fileName, size));
                    return (BitmapSource)b1;
                default:
                    return FileToIconConverter.loadBitmap(FileToIconConverter.GetFileIcon(str2, size).ToBitmap());
            }
        }

        public FileToIconConverter()
        {
            this.defaultsize = 48;
        }

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            int defaultsize = this.defaultsize;
            if (values.Length > 1 && values[1] is double)
                defaultsize = (int)(float)(double)values[1];
            if (values[0] is string)
                return (object)this.GetImage(values[0] as string, defaultsize);
            return (object)this.GetImage("", defaultsize);
        }

        public object[] ConvertBack(
          object value,
          Type[] targetTypes,
          object parameter,
          CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public enum IconSize
        {
            Small,
            Large,
            ExtraLarge,
            Jumbo,
            Thumbnail,
        }

        private class thumbnailInfo
        {
            public FileToIconConverter.IconSize iconsize;
            public WriteableBitmap bitmap;
            public string fullPath;

            public thumbnailInfo(WriteableBitmap b, string path, FileToIconConverter.IconSize size)
            {
                this.bitmap = b;
                this.fullPath = path;
                this.iconsize = size;
            }
        }
    }
}
