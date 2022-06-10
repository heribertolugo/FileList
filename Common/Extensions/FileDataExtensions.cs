using Common.Helpers;
using Common.Models;

namespace Common.Extensions
{
    public static class FileDataExtensions
    {
        public static FileType GetFileType(this FileData fileData)
        {
            return FileHelper.GetFileTypeFromPath(fileData.Path);
        }
    }
}
