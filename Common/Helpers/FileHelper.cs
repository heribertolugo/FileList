using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Helpers
{
    public static class FileHelper
    {
        // use FileTypeMimeData
        public static FileType GetFileTypeFromPath(string path)
        {
            string mimeType = Constants.ApplicationUnknown;
            string extension = System.IO.Path.GetExtension(path).ToLower();
            if (extension.Equals(string.Empty) && System.IO.Directory.Exists(path))
                return FileType.Folder;

            Microsoft.Win32.RegistryKey registryKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(extension);
            if (registryKey != null && registryKey.GetValue(Constants.ContentType) != null)
                mimeType = registryKey.GetValue(Constants.ContentType).ToString().ToLowerInvariant();

            if (mimeType.Contains(Constants.ImageWord))
                return FileType.Image;
            if (mimeType.Contains(Constants.TextWord) || extension.Equals(Constants.RichTextExtension))
                return FileType.Text;
            if (mimeType.Contains(Constants.AudioWord) || mimeType.Contains(Constants.VideoWord))
                return FileType.Media;
            if (mimeType.Contains(Constants.ApplicationWord) && !mimeType.Contains(Constants.ZipExtension.Replace(Constants.ExtensionSeparator, string.Empty)))
                return FileType.Application;
            if (mimeType.Contains(Constants.ZipExtension.Replace(Constants.ExtensionSeparator, string.Empty)))
                return FileType.Zip;

            return FileType.Unknown;
        }
    }
}
