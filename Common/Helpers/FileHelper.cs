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
            string extension = FileHelper.GetFileExtension(path).ToLower();
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
            //try
            //{
                //return System.IO.Path.GetFileName(path); // because System.IO.Path.GetDirectoryName cannot be trusted, i lost faith is built in methods
            //}
            //catch (PathTooLongException ex)
            //{
                if (DirectoryExists(path))
                    return string.Empty;

                char pathSeparator = System.IO.Path.DirectorySeparatorChar;
                int dirSeperatorIndex = path.LastIndexOf(pathSeparator);

                if (dirSeperatorIndex < 0)
                    return path;

                return path.Remove(0, dirSeperatorIndex).Trim(new char[] { pathSeparator });
            //}
        }

        public static string GetFileExtension(string path)
        {
        //    try
        //    {
        //        return System.IO.Path.GetExtension(path); // because System.IO.Path.GetDirectoryName cannot be trusted, i lost faith is built in methods
        //    }
        //    catch(PathTooLongException ex)
        //    {
                if (DirectoryExists(path))
                    return string.Empty;

                string fileName = GetFileName(path);

                if (string.IsNullOrWhiteSpace(fileName))
                    return string.Empty;
                int extensionIndex = fileName.LastIndexOf('.');
                if (extensionIndex < 0)
                    return string.Empty;
                return fileName.Remove(0, fileName.LastIndexOf('.'));
            //}
        }

        public static string GetFileNameWithoutExtension(string path)
        {
            //try
            //{
            //    return System.IO.Path.GetFileNameWithoutExtension(FileHelper.GetFileName(path)); // because System.IO.Path.GetDirectoryName cannot be trusted, i lost faith is built in methods
            //}
            //catch(PathTooLongException ex)
            //{
                if (DirectoryExists(path))
                    return string.Empty;

            string fileName = FileHelper.GetFileName(path);
            if (string.IsNullOrWhiteSpace(fileName))
                return string.Empty;
            string extension = GetFileExtension(fileName);

            if (string.IsNullOrWhiteSpace(extension))
                return fileName;

            return fileName.Substring(0, fileName.LastIndexOf(extension));
            //}
        }

        public static string GetDirectoryName(string path)
        {
            //try
            //{
            //    return System.IO.Path.GetDirectoryName(path); // unfortunately this gives wrong directory if path does not end with /
            //}catch(PathTooLongException ex)
            //{
                char pathSeparator = System.IO.Path.DirectorySeparatorChar;

                if (path.EndsWith(pathSeparator.ToString()))
                    return path.TrimEnd(new char[] { pathSeparator });

                string directoryName = FileExists(path) ? path.Replace(GetFileName(path), "") : path;// GetFileName(path);
                if (directoryName.EndsWith(pathSeparator.ToString()))
                    directoryName = directoryName.TrimEnd(new char[] { pathSeparator });


                return directoryName;
            //}
        }

        public static string GetCurrentDirectoryName(string path)
        {
            char pathSeparator = System.IO.Path.DirectorySeparatorChar;

            string directories = GetDirectoryName(path);

            if (string.IsNullOrWhiteSpace(directories))
                return string.Empty;

            return directories.Substring(directories.LastIndexOf(pathSeparator)).Trim(new char[] { pathSeparator });
        }

        public static bool PathHasExtension(string path)
        {
            //try
            //{
            //    return System.IO.Path.HasExtension(path);  // because System.IO.Path.GetDirectoryName cannot be trusted, i lost faith is built in methods
            //}
            //catch(PathTooLongException ex)
            //{
                if (DirectoryExists(path))
                    return false;

                return path.IndexOf('.', path.LastIndexOf(System.IO.Path.DirectorySeparatorChar)) > -1;
            //}
        }

        //https://stackoverflow.com/a/27111931/6368401
        public static bool DirectoryExists(string path)
        {
            uint attributes = Win32.Libraries.kernal32.GetFileAttributes(path.StartsWith(@"\\?\") ? path : @"\\?\" + path);
            if (attributes != (uint)Win32.Constants.FileAttribute.INVALID_FILE_ATTRIBUTES) //TODO: if(INVALID_FILE_ATTRIBUTES == GetFileAttributes("C:\\MyFile.txt") && GetLastError()==ERROR_FILE_NOT_FOUND)
            {
                return ((FileAttributes)attributes).HasFlag(FileAttributes.Directory);
            }
            else
            {
                return false;
            }
        }

        public static bool FileExists(string path)
        {
            uint attributes = Win32.Libraries.kernal32.GetFileAttributes(path.StartsWith(@"\\?\") ? path : @"\\?\" + path);
            if (attributes != (uint)Win32.Constants.FileAttribute.INVALID_FILE_ATTRIBUTES)
            {
                return !((FileAttributes)attributes).HasFlag(FileAttributes.Directory);
            }
            else
            {
                return false;
            }
        }
    }
}
