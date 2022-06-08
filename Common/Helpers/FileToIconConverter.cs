using Common.Models;
using Common.Models.ImageList;
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
using Win32.Constants;
using Win32.Libraries;
using Win32.Models;

namespace Common.Helpers
{

    public class FileToIconConverter : IMultiValueConverter
    {
        private static string _imageFilter = ".jpg,.jpeg,.png,.gif";
        private static string _exeFilter = ".exe,.lnk";
        private static Dictionary<string, BitmapSource> _iconDictionary = new Dictionary<string, BitmapSource>();
        private static SysImageList _imgList = new SysImageList(SysImageListSize.jumbo);
        private static Dictionary<string, BitmapSource> _thumbnailDictionary = new Dictionary<string, BitmapSource>();
        private int _defaultsize;

        public FileToIconConverter()
        {
            this._defaultsize = 48;
        }

        #region Public Properties

        public int DefaultSize
        {
            get
            {
                return this._defaultsize;
            }
            set
            {
                this._defaultsize = value;
            }
        }

        #endregion Public Properties

        #region Public Methods

        public BitmapSource GetImage(string fileName, int iconSize)
        {
            IconSize size = iconSize > 16 ? (iconSize > 32 ? (iconSize > 48 ? (iconSize > 72 ? IconSize.Thumbnail : IconSize.Jumbo) : IconSize.ExtraLarge) : IconSize.Large) : IconSize.Small;
            return this.AddToDictionary(fileName, size);
        }

        public BitmapSource GetImage(string fileName, IconSize iconSize)
        {
            return this.AddToDictionary(fileName, iconSize);
        }

        public Icon GetIcon(string fileName, IconSize iconSize)
        {
            string key = FileToIconConverter.ReturnKey(fileName, iconSize);
            string dummyFile = "aaa" + FileHelper.GetFileExtension(fileName).ToLower();
            if (!key.StartsWith("."))
                dummyFile = fileName;
            return FileToIconConverter.GetFileIcon(dummyFile, iconSize);
        }

        public static bool IsVistaUp()
        {
            return Environment.OSVersion.Version.Major >= 6;
        }

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            int defaultsize = this._defaultsize;
            if (values.Length > 1 && values[1] is double)
                defaultsize = (int)(float)(double)values[1];
            if (values[0] is string)
                return this.GetImage(values[0] as string, defaultsize);
            return this.GetImage(string.Empty, defaultsize);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion Public Methods

        #region Internal Methods

        internal static Icon GetFileIcon(string fileName, IconSize size)
        {
            SHFILEINFO psfi = new SHFILEINFO();
            uint num = ShellIconFlags.SHGFI_SYSICONINDEX; 
            if (fileName.IndexOf(":") == -1)
                num |= ShellIconFlags.SHGFI_USEFILEATTRIBUTES; 
            uint uFlags = size != IconSize.Small ? num | ShellIconFlags.SHGFI_ICON : (uint)((int)num | ShellIconFlags.SHGFI_ICON | ShellIconFlags.SHGFI_SMALLICON);
            shell32.SHGetFileInfo(fileName, ShellIconFlags.SHGFI_LARGEICON, ref psfi, (uint)Marshal.SizeOf(psfi), uFlags);
            return Icon.FromHandle(psfi.hIcon);
        }

        #endregion Internal Methods

        private static void CopyBitmap(BitmapSource source, WriteableBitmap target, bool dispatcher)
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
                Int32Rect sourceRect = new Int32Rect(0, (h >= 0.0 ? (int)h : 0) / 2, w, w);
                try
                {
                    target.WritePixels(sourceRect, bits, stride, 0);
                }
                catch (Exception ex)
                {
                    Debugger.Break();
                }
            }
        }

        private static System.Drawing.Size GetDefaultSize(IconSize size)
        {
            switch (size)
            {
                case IconSize.Large:
                    return new System.Drawing.Size(32, 32);
                case IconSize.ExtraLarge:
                    return new System.Drawing.Size(48, 48);
                case IconSize.Jumbo:
                    return new System.Drawing.Size(256, 256);
                case IconSize.Thumbnail:
                    return new System.Drawing.Size(256, 256);
                default:
                    return new System.Drawing.Size(16, 16);
            }
        }

        private static Bitmap ResizeImage(Bitmap imgToResize, System.Drawing.Size size, int spacing)
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

        private static Bitmap ResizeJumbo(Bitmap imgToResize, System.Drawing.Size size, int spacing)
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

        private static BitmapSource LoadBitmap(Bitmap source)
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

        private static bool IsImage(string fileName)
        {
            string lower = FileHelper.GetFileExtension(fileName).ToLower();
            if (lower == string.Empty)
                return false;
            return FileToIconConverter._imageFilter.IndexOf(lower) != -1 && File.Exists(fileName);
        }

        private static bool IsExecutable(string fileName)
        {
            string lower = FileHelper.GetFileExtension(fileName).ToLower();
            if (lower == string.Empty)
                return false;
            return FileToIconConverter._exeFilter.IndexOf(lower) != -1 && File.Exists(fileName);
        }

        private static bool IsFolder(string path)
        {
            return path.EndsWith(Constants.DirectoryKey) || Directory.Exists(path);
        }

        private static string ReturnKey(string fileName, IconSize size)
        {
            string lower = FileHelper.GetFileExtension(fileName).ToLower();
            if (FileToIconConverter.IsExecutable(fileName))
                lower = fileName.ToLower();
            if (FileToIconConverter.IsImage(fileName) && size == IconSize.Thumbnail)
                lower = fileName.ToLower();
            if (FileToIconConverter.IsFolder(fileName))
                lower = fileName.ToLower();
            switch (size)
            {
                case IconSize.Small:
                    lower += "+S";
                    break;
                case IconSize.Large:
                    lower += "+L";
                    break;
                case IconSize.ExtraLarge:
                    lower += "+XL";
                    break;
                case IconSize.Jumbo:
                    lower += "+J";
                    break;
                case IconSize.Thumbnail:
                    lower += FileToIconConverter.IsImage(fileName) ? "+T" : "+J";
                    break;
            }
            return lower;
        }

        private Bitmap LoadJumbo(string lookup)
        {
            FileToIconConverter._imgList.ImageListSize = FileToIconConverter.IsVistaUp() ? SysImageListSize.jumbo : SysImageListSize.extraLargeIcons;
            Icon icon = FileToIconConverter._imgList.Icon(FileToIconConverter._imgList.IconIndex(lookup, FileToIconConverter.isFolder(lookup)));
            Bitmap imgToResize = icon.ToBitmap();
            icon.Dispose();
            Color color = Color.FromArgb(0, 0, 0, 0);
            if (imgToResize.Width < 256)
                imgToResize = FileToIconConverter.ResizeImage(imgToResize, new System.Drawing.Size(256, 256), 0);
            else if (imgToResize.GetPixel(100, 100) == color && imgToResize.GetPixel(200, 200) == color && imgToResize.GetPixel(200, 200) == color)
            {
                FileToIconConverter._imgList.ImageListSize = SysImageListSize.largeIcons;
                imgToResize = FileToIconConverter.ResizeJumbo(FileToIconConverter._imgList.Icon(FileToIconConverter._imgList.IconIndex(lookup)).ToBitmap(), new System.Drawing.Size(200, 200), 5);
            }
            return imgToResize;
        }

        public void ClearInstanceCache()
        {
            FileToIconConverter._thumbnailDictionary.Clear();
        }

        private void PollIconCallback(object state)
        {
            FileToIconConverter.ThumbnailInfo thumbnailInfo = state as FileToIconConverter.ThumbnailInfo;
            string fullPath = thumbnailInfo.fullPath;
            WriteableBitmap bitmap1 = thumbnailInfo.bitmap;
            IconSize iconsize = thumbnailInfo.iconsize;
            Bitmap bitmap2 = FileToIconConverter.GetFileIcon(fullPath, iconsize).ToBitmap();
            Bitmap source1 = iconsize != IconSize.Jumbo && iconsize != IconSize.Thumbnail ? FileToIconConverter.ResizeImage(bitmap2, FileToIconConverter.GetDefaultSize(iconsize), 0) : FileToIconConverter.ResizeJumbo(bitmap2, FileToIconConverter.GetDefaultSize(iconsize), 5);
            BitmapSource source2 = FileToIconConverter.LoadBitmap(source1);
            bitmap2.Dispose();
            source1.Dispose();
            FileToIconConverter.CopyBitmap(source2, bitmap1, true);
        }

        private void PollThumbnailCallback(object state)
        {
            FileToIconConverter.ThumbnailInfo thumbnailInfo = state as FileToIconConverter.ThumbnailInfo;
            string fullPath = thumbnailInfo.fullPath;
            WriteableBitmap bitmap = thumbnailInfo.bitmap;
            IconSize iconsize = thumbnailInfo.iconsize;
            try
            {
                Bitmap imgToResize = new Bitmap(fullPath);
                Bitmap source1 = FileToIconConverter.ResizeImage(imgToResize, FileToIconConverter.GetDefaultSize(iconsize), 5);
                BitmapSource source2 = FileToIconConverter.LoadBitmap(source1);
                imgToResize.Dispose();
                source1.Dispose();
                FileToIconConverter.CopyBitmap(source2, bitmap, true);
            }
            catch
            {
            }
        }

        private BitmapSource AddToDictionary(string fileName, IconSize size)
        {
            string key = FileToIconConverter.ReturnKey(fileName, size);
            if (size == IconSize.Thumbnail || FileToIconConverter.IsExecutable(fileName))
            {
                if (!FileToIconConverter._thumbnailDictionary.ContainsKey(key))
                {
                    IoHelper.WriteToConsole("requesting thumbDic lock");
                    lock (FileToIconConverter._thumbnailDictionary)
                        FileToIconConverter._thumbnailDictionary.Add(key, this.GetImageInternal(fileName, size));
                    IoHelper.WriteToConsole("thumbDic lock released");
                }
                return FileToIconConverter._thumbnailDictionary[key];
            }
            if (!FileToIconConverter._iconDictionary.ContainsKey(key))
            {
                IoHelper.WriteToConsole("requesting iconDic lock");
                lock (FileToIconConverter._iconDictionary)
                    FileToIconConverter._iconDictionary.Add(key, this.GetImageInternal(fileName, size));
                IoHelper.WriteToConsole("iconDic lock released");
            }
            return FileToIconConverter._iconDictionary[key];
        }

        private BitmapSource GetImageInternal(string fileName, IconSize size)
        {
            string key = FileToIconConverter.ReturnKey(fileName, size);
            string dummyFile = "aaa" + FileHelper.GetFileExtension(fileName).ToLower();
            WriteableBitmap writeableBitmap = null;

            if (!key.StartsWith("."))
                dummyFile = fileName;
            if (FileToIconConverter.IsExecutable(fileName))
            {
                writeableBitmap = new WriteableBitmap(this.AddToDictionary("aaa.exe", size));
                ThreadPool.QueueUserWorkItem(new WaitCallback(this.PollIconCallback), new FileToIconConverter.ThumbnailInfo(writeableBitmap, fileName, size));
                return writeableBitmap;
            }
            switch (size)
            {
                case IconSize.ExtraLarge:
                    FileToIconConverter._imgList.ImageListSize = SysImageListSize.extraLargeIcons;
                    return FileToIconConverter.LoadBitmap(FileToIconConverter._imgList.Icon(FileToIconConverter._imgList.IconIndex(dummyFile, FileToIconConverter.isFolder(fileName))).ToBitmap());
                case IconSize.Jumbo:
                    return FileToIconConverter.LoadBitmap(this.LoadJumbo(dummyFile));
                case IconSize.Thumbnail:
                    if (!FileToIconConverter.IsImage(fileName))
                        return this.GetImageInternal(dummyFile, IconSize.Jumbo);
                    writeableBitmap = new WriteableBitmap(this.AddToDictionary(fileName, IconSize.Jumbo));
                    ThreadPool.QueueUserWorkItem(new WaitCallback(this.PollThumbnailCallback), new FileToIconConverter.ThumbnailInfo(writeableBitmap, fileName, size));
                    return writeableBitmap;
                default:
                    return FileToIconConverter.LoadBitmap(FileToIconConverter.GetFileIcon(dummyFile, size).ToBitmap());
            }
        }

        private class ThumbnailInfo
        {
            public IconSize iconsize;
            public WriteableBitmap bitmap;
            public string fullPath;

            public ThumbnailInfo(WriteableBitmap b, string path, IconSize size)
            {
                this.bitmap = b;
                this.fullPath = path;
                this.iconsize = size;
            }
        }
    }
}
