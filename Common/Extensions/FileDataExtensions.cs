using Common.Models;

namespace Common.Extensions
{
    public static class FileDataExtensions
    {
        public static FileType GetFileType(this FileData fileData)
        {
            string fileName = fileData.Path;
            string str = Constants.ApplicationUnknown;
            string lower = System.IO.Path.GetExtension(fileName).ToLower();
            if (lower.Equals(string.Empty) && System.IO.Directory.Exists(fileName))
            {
                str = string.Empty;
            }
            else
            {
                Microsoft.Win32.RegistryKey registryKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(lower);
                if (registryKey != null && registryKey.GetValue(Constants.ContentType) != null)
                    str = registryKey.GetValue(Constants.ContentType).ToString().ToLowerInvariant();
            }
            if (str.Contains(Constants.ImageWord))
                return FileType.Image;
            if (str.Contains(Constants.TextWord) || lower.Equals(Constants.RichTextExtension))
                return FileType.Text;
            if (str.Contains(Constants.AudioWord) || str.Contains(Constants.VideoWord))
                return FileType.Media;
            if (str.Contains(Constants.ApplicationWord) && !str.Contains(Constants.ZipExtension.Replace(Constants.ExtensionSeparator, string.Empty)))
                return FileType.Application;
            if (str.Contains(Constants.ZipExtension.Replace(Constants.ExtensionSeparator, string.Empty)))
                return FileType.Zip;
            if (str.Equals(string.Empty))
                return FileType.Folder;
            return FileType.Unknown;
        }
    }
}
