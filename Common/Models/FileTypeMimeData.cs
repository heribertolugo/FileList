using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Models
{
    public class FileTypeMimeData
    {
        // create a static dictionary which will act as a cache. key is file type

        // create a method which allows user to populate cache
        // populating cache would consist of reading reg keys under HKEY_ROOT.
        // need to have a flag of "found extensions" set to false as you start reading reg.
        // when the first reg key is found which starts with a period, set the flag to true
        // continue reading reg keys until a key is found NOT starting with a period

        // create a method which allows user to clear cache

        // create a method which allows the user to add file extensions to a file type &/or a mime type
        public FileTypeMimeData(string mimeType, string perceivedType = null)
        {

        }

        public FileType FileType { get; }
        public string MimeType { get; }
        public IList<string> Extensions { get; }
        public string Type { get; }
        public string SubType { get; }
        public string PerceivedType { get; }
    }
}
