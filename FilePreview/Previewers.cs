using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FilePreview
{
    public class Previewers
    {
        private static IDictionary<string, IPreviewFile> _previewByExtension;
        private static IDictionary<FileType, IPreviewFile> _previewByFileType;
        private static IDictionary<FileType, IPreviewFile> _previewByFileTypeFlag;
        private static IList<IPreviewFile> _filePreviews;
        private static object locker = new object();

        static Previewers()
        {
            lock (locker)
            {
                if (Previewers._previewByExtension != null)
                    return;
                Previewers._previewByExtension = new Dictionary<string, IPreviewFile>();
                if (Previewers._previewByFileType != null)
                    return;
                Previewers._previewByFileType = new Dictionary<FileType, IPreviewFile>();
                if (Previewers._previewByFileTypeFlag != null)
                    return;
                Previewers._previewByFileTypeFlag = new Dictionary<FileType, IPreviewFile>();

                Previewers.GetFilePreviews(ref Previewers._filePreviews);
                Previewers.SetFilePreviewsByExtension(Previewers._filePreviews, Previewers._previewByExtension);
                Previewers.SetFilePreviewsByFileType(Previewers._filePreviews, Previewers._previewByFileType);
                Previewers.SetFilePreviewsByFileTypeFlag(Previewers._filePreviews, Previewers._previewByFileTypeFlag);
            }
        }

        public Previewers()
        {
            //this._previewByExtension = new Dictionary<string, IPreviewFile>();
            //this._previewByFileType = new Dictionary<FileType, IPreviewFile>();
            //this._previewByFileTypeFlag = new Dictionary<FileType, IPreviewFile>();

            //Previewers.GetFilePreviews(ref this._filePreviews);
            //Previewers.SetFilePreviewsByExtension(this._filePreviews, this._previewByExtension);
            //Previewers.SetFilePreviewsByFileType(this._filePreviews, this._previewByFileType);
            //Previewers.SetFilePreviewsByFileTypeFlag(this._filePreviews, this._previewByFileTypeFlag);
        }

        public static void ForceInit()
        {
            IPreviewFile previewFile = new Previewers().GetPreviewer(" ");
        }

        /// <summary>
        /// Gets first IPreviewFile matching the fileExtension specified.
        /// null if no match is found.
        /// </summary>
        /// <param name="fileExtension"></param>
        /// <returns></returns>
        public IPreviewFile GetPreviewer(string fileExtension)
        {
            if (Previewers._previewByExtension.ContainsKey(fileExtension))
                return Previewers._previewByExtension[fileExtension];
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
            if (byFlag && Previewers._previewByFileTypeFlag.ContainsKey(fileType))
                return Previewers._previewByFileTypeFlag[fileType];
            if (!byFlag && Previewers._previewByFileType.ContainsKey(fileType))
                return Previewers._previewByFileType[fileType];
            return null;
        }

        public IEnumerable<IPreviewFile> GetPreviewrs()
        {
            return Previewers._filePreviews;
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
