using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Common.Models
{
    public interface IPreviewFile
    {
        /// <summary>
        /// Loads a file for preview given the provided path. Returns true if load was successful.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        bool Load(string path);
        /// <summary>
        /// Loads a file for preview given the provided path. Returns true if load was successful.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        bool Load(FileData path);
        /// <summary>
        /// List of compatible file extensions
        /// </summary>
        IEnumerable<string> Extensions { get; }
        /// <summary>
        /// Control which is used to preview file
        /// </summary>
        Control Viewer { get; }

        /// <summary>
        /// The system type for the file
        /// </summary>
        FileType FileType { get; }
    }
}
