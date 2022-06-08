using Common.Models;
using System;
using System.Collections.Generic;
using System.IO;
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

        public static string GetFileName(string path)
        {
            try
            {
                return System.IO.Path.GetFileName(path);
            }
            catch (PathTooLongException ex)
            {
                char pathSeparator = System.IO.Path.DirectorySeparatorChar;
                int dirSeperatorIndex = path.LastIndexOf(pathSeparator);

                return path.Remove(0, dirSeperatorIndex).Trim(new char[] { ' ', pathSeparator });
            }
        }

        public static string GetFileExtension(string path)
        {
            try
            {
                return System.IO.Path.GetExtension(path);
            }catch(PathTooLongException ex)
            {
                string fileName = FileHelper.GetFileName(path);

                return fileName.Remove(0, fileName.LastIndexOf('.'));
            }
        }

        public static string GetFileNameWithoutExtension(string path)
        {
            try
            {
                return System.IO.Path.GetFileNameWithoutExtension(FileHelper.GetFileName(path));
            }catch(PathTooLongException ex)
            {
                string fileName = FileHelper.GetFileName(path);
                int extensionIndex = fileName.LastIndexOf('.');

                return fileName.Substring(0, extensionIndex);
            }
        }

        public static string GetDirectoryName(string path)
        {
            try
            {
                return System.IO.Path.GetDirectoryName(path);
            }catch(PathTooLongException ex)
            {
                return path.Replace(FileHelper.GetFileName(path), "");
            }
        }

        public static string GetCurrentDirectoryName(string path)
        {
            char pathSeparator = System.IO.Path.DirectorySeparatorChar;

            return FileHelper.GetFileName(path.Replace(FileHelper.GetFileName(path), "").Trim(new char[] { ' ', pathSeparator })) + pathSeparator;
        }

        public static bool PathHasExtension(string path)
        {
            try
            {
                return System.IO.Path.HasExtension(path);
            }catch(PathTooLongException ex)
            {
                string fileName = FileHelper.GetFileName(path);
                string extension = fileName.Remove(0, fileName.LastIndexOf('.'));

                return !string.IsNullOrWhiteSpace(extension.Trim(new char[] { '.' }));
            }
        }
    }
}
