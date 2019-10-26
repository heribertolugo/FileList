using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FilePreview
{
    public class Previewers
    {
        private IDictionary<string, IPreviewFile> _previewByExtension;
        private IDictionary<FileType, IPreviewFile> _previewByFileType;
        private IDictionary<FileType, IPreviewFile> _previewByFileTypeFlag;
        private IList<IPreviewFile> _filePreviews;

        public Previewers()
        {
            this._previewByExtension = new Dictionary<string, IPreviewFile>();
            this._previewByFileType = new Dictionary<FileType, IPreviewFile>();
            this._previewByFileTypeFlag = new Dictionary<FileType, IPreviewFile>();

            Previewers.GetFilePreviews(ref this._filePreviews);
            Previewers.SetFilePreviewsByExtension(this._filePreviews, this._previewByExtension);
            Previewers.SetFilePreviewsByFileType(this._filePreviews, this._previewByFileType);
            Previewers.SetFilePreviewsByFileTypeFlag(this._filePreviews, this._previewByFileTypeFlag);
        }

        /// <summary>
        /// Gets first IPreviewFile matching the fileExtension specified.
        /// null if no match is found.
        /// </summary>
        /// <param name="fileExtension"></param>
        /// <returns></returns>
        public IPreviewFile GetPreviewer(string fileExtension)
        {
            if (this._previewByExtension.ContainsKey(fileExtension))
                return this._previewByExtension[fileExtension];
            return null;
        }

        /// <summary>
        /// Gets first IPreviewFile matching the FileType specified. If byFlag is true the IPreviewFile must match the Flag value rather than an exact FileType.
        /// null if no match is found.
        /// </summary>
        /// <param name="fileType"></param>
        /// <param name="byFlag"></param>
        /// <returns></returns>
        public IPreviewFile GetPreviewer(FileType fileType, bool byFlag = false)
        {
            if (byFlag && this._previewByFileTypeFlag.ContainsKey(fileType))
                return this._previewByFileTypeFlag[fileType];
            if (!byFlag && this._previewByFileType.ContainsKey(fileType))
                return this._previewByFileType[fileType];
            return null;
        }

        private static void GetFilePreviews(ref IList<IPreviewFile> previews)
        {
            previews = AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes())
                .Where(t => typeof(IPreviewFile).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
                .Select(p => (IPreviewFile)Activator.CreateInstance(p)).ToList();
        }

        private static void SetFilePreviewsByExtension(IEnumerable<IPreviewFile> previews, IDictionary<string, IPreviewFile> target)
        {
            foreach (IPreviewFile preview in previews)
            {
                foreach (string extension in preview.Extensions)
                {
                    if (!target.ContainsKey(extension))
                    {
                        target.Add(extension, preview);
                    }
                }
            }
        }

        private static void SetFilePreviewsByFileType(IEnumerable<IPreviewFile> previews, IDictionary<FileType, IPreviewFile> target)
        {
            foreach (IPreviewFile preview in previews)
            {
                foreach (FileType type in Enum.GetValues(typeof(FileType)))
                {
                    if (type == FileType.Unknown)
                    {
                        if (!target.ContainsKey(type) && preview.FileType == type)
                            target.Add(type, preview);
                    }
                    else
                    {
                        if (!target.ContainsKey(type) && preview.FileType.HasFlag(type))
                        {
                            target.Add(type, preview);
                        }
                    }
                }
            }
        }

        private static void SetFilePreviewsByFileTypeFlag(IEnumerable<IPreviewFile> previews, IDictionary<FileType, IPreviewFile> target)
        {
            foreach (IPreviewFile preview in previews)
            {
                if (!target.ContainsKey(preview.FileType))
                {
                    target.Add(preview.FileType, preview);
                }
            }
        }
    }
}
