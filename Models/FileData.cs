using FileList.Logic;
using Shell32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace FileList
{
    public struct FileData : ICloneable
    {
        private static List<string> FilePropertyNames;
        private string _path;
        private string _name;
        private string _extension;
        private string _diectory;
        private readonly List<KeyValuePair<string, string>> _extendedProperties;
        private List<FileData> _zipContents;
        private bool _isZip;
        private float? _sizeInKilobytes;
        private bool _isSizeSet;
        private DateTime? _dateCreated;
        private bool _isDateCreatedSet;
        private DateTime? _dateModified;
        private bool _isDateModifiedSet;
        private IShellDispatch5 _shell;

        private static object _filePropertyNamesLock;

        static FileData()
        {
            if (FileData._filePropertyNamesLock == null)
                FileData._filePropertyNamesLock = new object();

            Console.WriteLine("requesting filePropertyNames lock @ 36");
            lock (FileData._filePropertyNamesLock)
            {
                if (FileData.FilePropertyNames == null)
                {
                    FileData.FilePropertyNames = new List<string>(); //  Models.ConcurrentCollection<string>("FileData FilePropertyNames");
                }
            }
        }

        public FileData(string path, IShellDispatch5 shell = null)
        {
            this._name = null;
            this._extension = null;
            this._diectory = null;
            this._extendedProperties = new List<KeyValuePair<string, string>>();
            this._path = path;
            this._isZip = false;
            this._zipContents = new List<FileData>();
            this._sizeInKilobytes = null;
            this._isSizeSet = false;
            this._dateCreated = null;
            this._isDateCreatedSet = false;
            this._dateModified = null;
            this._isDateModifiedSet = false;
            this._shell = shell; // ?? new ShellClass();

            if (FileData.FilePropertyNames == null || FileData.FilePropertyNames.Count < 1)
            {
                Console.WriteLine("requesting filePropertyNames lock @ 63");
                lock (FileData._filePropertyNamesLock)
                {
                    if (FileData.FilePropertyNames == null)
                        FileData.FilePropertyNames = new List<string>(); //  Models.ConcurrentCollection<string>("FileData FilePropertyNames 2");
                    if (FileData.FilePropertyNames.Count < 1)
                        FileData.LoadFilePropertyNames(System.IO.Path.GetDirectoryName(path));
                }
            }

            this.LoadExtendedProperties();
            //GC.Collect();
        }

        public string Path
        {
            get
            {
                return this._path;
            }
            private set
            {
            }
        }

        public string Name
        {
            get
            {
                if (string.IsNullOrEmpty(this.Path) || this.Path.Trim() == string.Empty)
                    return string.Empty;
                if (this._name == null)
                    this._name = System.IO.Path.GetFileNameWithoutExtension(this.Path);
                return this._name;
            }
            private set
            {
            }
        }

        public string Extension
        {
            get
            {
                if (string.IsNullOrEmpty(this.Path) || this.Path.Trim() == string.Empty)
                    return string.Empty;
                if (this._extension == null)
                    this._extension = System.IO.Path.GetExtension(this.Path);
                return this._extension.ToLowerInvariant();
            }
            private set
            {
            }
        }

        public string Directory
        {
            get
            {
                if (string.IsNullOrEmpty(this.Path) || this.Path.Trim() == string.Empty)
                    return string.Empty;
                if (this._diectory == null)
                    this._diectory = System.IO.Path.GetDirectoryName(this.Path) + "\\";
                return this._diectory;
            }
            private set
            {
            }
        }

        public bool IsZip
        {
            get
            {
                return this._isZip || this.ZipContents.Count > 0;
            }
            set
            {
                this._isZip = value;
            }
        }

        public IEnumerable<KeyValuePair<string, string>> ExtendedProperties
        {
            get
            {
                return this._extendedProperties;
            }
            private set
            {
            }
        }

        public IList<FileData> ZipContents
        {
            get
            {
                return this._zipContents;
            }
            private set
            {
            }
        }

        public float? SizeInKilobytes
        {
            get
            {
                if (!this._isSizeSet)
                {
                    this._sizeInKilobytes = this.GetSizeInKiloBytes();
                    this._isSizeSet = true;
                }
                return this._sizeInKilobytes;
            }
            private set
            {
            }
        }

        public DateTime? DateCreated
        {
            get
            {
                if (!this._isDateCreatedSet)
                    this._dateCreated = this.GetDateFromExtendedProperties("date created") ?? this.GetDateFromExtendedProperties("Creation Time");
                return this._dateCreated;
            }
            private set
            {
            }
        }

        public DateTime? DateModified
        {
            get
            {
                if (!this._isDateModifiedSet)
                    this._dateModified = this.GetDateFromExtendedProperties("date modified") ?? this.GetDateFromExtendedProperties("Last Write Time");
                return this._dateModified;
            }
            private set
            {
            }
        }

        private float? GetSizeInKiloBytes()
        {
            string fileSIze = this.ExtendedProperties.FirstOrDefault(p => p.Key.ToLowerInvariant().Equals("size")).Value;
            if (string.IsNullOrEmpty(fileSIze))
            {
                fileSIze = this.ExtendedProperties.Where(p => p.Key.ToLowerInvariant().Equals("Length")).Select(p => p.Value).FirstOrDefault();
                if (!string.IsNullOrEmpty(fileSIze))
                    return Misc.ConvertStorageValueToKb(fileSIze);
            }

            if (string.IsNullOrEmpty(fileSIze))
                return null;
            return Misc.ConvertStorageValueToKb(fileSIze);
        }

        private DateTime? GetDateFromExtendedProperties(string propertyName)
        {
            DateTime result;
            if (!DateTime.TryParse(this.ExtendedProperties.FirstOrDefault(p => p.Key.ToLowerInvariant().Equals(propertyName.ToLowerInvariant())).Value, out result))
                return null;
            return result;
        }

        private void LoadExtendedProperties()
        {
            try
            {


                //Folder objFolder = null;

                //try
                //{
                //    objFolder = (Folder)shell.NameSpace(path);
                //}
                //catch (Exception ex)
                //{

                //}

                //if (objFolder == null)
                //{
                //    //Marshal.ReleaseComObject(objFolder);
                //    fileData.ZipContents.Add(new FileData(path));
                //    return;
                //}
                //Shell shell = new ShellClass();
                if (this._shell != null)
                {
                    Folder folder = this._shell.NameSpace(System.IO.Path.GetDirectoryName(this.Path));
                    FolderItem name = folder.ParseName(System.IO.Path.GetFileName(this.Path));
                    for (int iColumn = 0; iColumn < FileData.FilePropertyNames.Count; ++iColumn)
                        this._extendedProperties.Add(new KeyValuePair<string, string>(FileData.FilePropertyNames[iColumn], folder.GetDetailsOf(name, iColumn)));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("LoadExtendedProperties exception: {0}", ex.Message);
            }
            finally
            {
                this.LoadExtendedPropertiesLight();
            }
        }

        private void LoadExtendedPropertiesLight()
        {
            try
            {
                FileInfo fileInfo = new FileInfo(this.Path);
                this._extendedProperties.Add(new KeyValuePair<string, string>("Creation Time", fileInfo.CreationTime.ToString()));
                this._extendedProperties.Add(new KeyValuePair<string, string>("Last Write Time", fileInfo.LastWriteTime.ToString()));
                this._extendedProperties.Add(new KeyValuePair<string, string>("Length", string.Format("{0} bytes", fileInfo.Length.ToString())));
                this._extendedProperties.Add(new KeyValuePair<string, string>("Last Accessed Time", fileInfo.LastAccessTime.ToString()));
            }
            catch (Exception ex)
            {
                Console.WriteLine("LoadExtendedPropertiesLight exception: {0}", ex.Message);
            }
        }

        private static void LoadFilePropertyNames(string nspace)
        {
            try
            {
                Shell shell1 = new ShellClass();
                Folder folder = shell1.NameSpace(nspace);
                for (int iColumn = 0; iColumn < (int)short.MaxValue; ++iColumn)
                {
                    string detailsOf = folder.GetDetailsOf(null, iColumn);
                    if (string.IsNullOrEmpty(detailsOf))
                        break;
                    FileData.FilePropertyNames.Add(detailsOf);
                }
                Marshal.ReleaseComObject(folder);
                Marshal.ReleaseComObject(shell1);
            }
            catch (Exception ex)
            {
                Console.WriteLine("LoadFilePropertyNames exception: {0}", ex.Message);
            }
        }

        public object Clone()
        {
            FileData f = new FileData(this.Path);



            return new FileData(this.Path);
        }
    }
}
