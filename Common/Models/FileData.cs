using Common.Helpers;
using Shell32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using Win32.Libraries;

namespace Common.Models
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
        private bool _extendedPropertiesSet;
        private object _loadExtendedPropertieslock;

        private static object _filePropertyNamesLock;

        static FileData()
        {
            if (FileData._filePropertyNamesLock == null)
                FileData._filePropertyNamesLock = new object();

            //Extensions.WriteToConsole("requesting filePropertyNames lock @ 36");
            //lock (FileData._filePropertyNamesLock)
            //{
            //    if (FileData.FilePropertyNames == null)
            //    {
            //        FileData.FilePropertyNames = new List<string>(); //  Models.ConcurrentCollection<string>("FileData FilePropertyNames");
            //    }
            //}
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
            this._extendedPropertiesSet = false;

            this._loadExtendedPropertieslock = new object();
            //if (FileData.FilePropertyNames == null || FileData.FilePropertyNames.Count < 1)
            //{
            //    Extensions.WriteToConsole("requesting filePropertyNames lock @ 63");
            //    lock (FileData._filePropertyNamesLock)
            //    {
            //        if (FileData.FilePropertyNames == null)
            //            FileData.FilePropertyNames = new List<string>(); //  Models.ConcurrentCollection<string>("FileData FilePropertyNames 2");
            //        if (FileData.FilePropertyNames.Count < 1)
            //            FileData.LoadFilePropertyNames(System.IO.Path.GetDirectoryName(path));
            //    }
            //}

            //this.LoadExtendedProperties();
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
                if (!this._extendedPropertiesSet)
                    this.LoadExtendedProperties();
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
                if (!this._extendedPropertiesSet)
                    this.LoadExtendedProperties();
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
                if (!this._extendedPropertiesSet)
                    this.LoadExtendedProperties();
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
                if (!this._extendedPropertiesSet)
                    this.LoadExtendedProperties();
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
            string fileSIze = this.ExtendedProperties.FirstOrDefault(p => p.Key.ToUpperInvariant().Equals("SIZE")).Value;
            if (string.IsNullOrEmpty(fileSIze))
            {
                fileSIze = this.ExtendedProperties.Where(p => p.Key.ToUpperInvariant().Equals("LENGTH")).Select(p => p.Value).FirstOrDefault(p => !string.IsNullOrWhiteSpace(p));
                if (!string.IsNullOrEmpty(fileSIze))
                    return FileStorageSize.ConvertStorageValueToKb(fileSIze);
            }

            if (string.IsNullOrEmpty(fileSIze))
                return null;
            return FileStorageSize.ConvertStorageValueToKb(fileSIze);
        }

        private DateTime? GetDateFromExtendedProperties(string propertyName)
        {
            DateTime result;
            if (!DateTime.TryParse(this.ExtendedProperties.FirstOrDefault(p => p.Key.ToUpperInvariant().Equals(propertyName.ToUpperInvariant())).Value, out result))
                return null;
            return result;
        }

        private IShellDispatch5 GetShell()
        {
            try
            {
                Folder folder = this._shell.NameSpace(System.IO.Path.GetDirectoryName(this.Path));
                return this._shell;
            }
            catch (Exception ex) { }

            try
            {
                this._shell = ole32.GetIShellDispatch5();
                Folder folder = this._shell.NameSpace(System.IO.Path.GetDirectoryName(this.Path));
                return this._shell;
            }
            catch (Exception ex) { }

            try
            {
                Shell32.ShellClass shell = new Shell32.ShellClass();
                ole32.GetRegisteredInterfaceMarshalPtr<Shell32.IShellDispatch5>(shell);
                GC.KeepAlive(shell);
                this._shell = ole32.GetIShellDispatch5();
                Folder folder = this._shell.NameSpace(System.IO.Path.GetDirectoryName(this.Path));
                return this._shell;
            }
            catch (Exception ex) { }

            return new ShellClass();
        }

        private void LoadExtendedProperties()
        {
            if (this._extendedPropertiesSet)
                return;

            Thread thread = new Thread((object f) =>
            {
                FileData fileData = (FileData)f;
                if (fileData._extendedPropertiesSet)
                    return;
                lock (fileData._loadExtendedPropertieslock)
                {
                    if (fileData._extendedPropertiesSet || fileData._extendedProperties.Count > 0)
                        return;
                    fileData._extendedPropertiesSet = true;
                    
                    try
                    {

                        if (fileData.GetShell() != null)
                        {
                            Folder folder = fileData._shell.NameSpace(System.IO.Path.GetDirectoryName(fileData.Path));
                            FolderItem name = folder.ParseName(System.IO.Path.GetFileName(fileData.Path));

                            for (int iColumn = -1; iColumn < 1000; iColumn++) //(int)short.MaxValue
                            {
                                string detailsOf = folder.GetDetailsOf(null, iColumn);
                                //IoHelper.WriteToConsole("loading extnded property #{0}", iColumn);
                                if (string.IsNullOrEmpty(detailsOf))
                                    continue;
                                string value = folder.GetDetailsOf(name, iColumn);
                                if (!string.IsNullOrEmpty(value))
                                    fileData._extendedProperties.Add(new KeyValuePair<string, string>(detailsOf, value));
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        IoHelper.WriteToConsole("LoadExtendedProperties exception: {0}", ex.Message);
                        //throw ex;
                    }
                    finally
                    {
                        fileData.LoadExtendedPropertiesLight();
                        IoHelper.WriteToConsole("finished loading extnded properties for {0}", fileData.Path);
                    }
                }
            });

            if (!System.IO.File.Exists(this.Path) && !System.IO.Directory.Exists(this.Path))
                return;
            IoHelper.WriteToConsole("loading extnded properties for {0}", this.Path);

            thread.SetApartmentState(ApartmentState.STA);
            thread.IsBackground = true;
            thread.Start(this);
            //thread.Join();
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
                IoHelper.WriteToConsole("LoadExtendedPropertiesLight exception: {0}", ex.Message);
            }
        }

        private static void LoadFilePropertyNames(string nspace)
        {
            try
            {
                Shell shell1 = new ShellClass();
                Folder folder = shell1.NameSpace(nspace);
                for (int iColumn = -1; iColumn < (int)short.MaxValue; ++iColumn)
                {
                    string detailsOf = folder.GetDetailsOf(null, iColumn);
                    if (string.IsNullOrEmpty(detailsOf))
                        continue;
                    FileData.FilePropertyNames.Add(detailsOf);
                }
                Marshal.ReleaseComObject(folder);
                Marshal.ReleaseComObject(shell1);
            }
            catch (Exception ex)
            {
                IoHelper.WriteToConsole("LoadFilePropertyNames exception: {0}", ex.Message);
            }
        }

        public object Clone()
        {
            FileData f = new FileData(this.Path);



            return new FileData(this.Path);
        }
    }

    public class FileTypeExtendedProperties
    {
        Dictionary<string, HashSet<string>> _properties;
        public FileTypeExtendedProperties()
        {
            this._properties = new Dictionary<string, HashSet<string>>();
        }

        public ISet<string> this[string extension]
        {
            get
            {
                if (!this._properties.ContainsKey(extension))
                    this._properties.Add(extension, new HashSet<string>(StringComparer.OrdinalIgnoreCase));
                return this._properties[extension];
            }
        }
    }

    public class FileExtendedProperties : IEnumerable<FileProperty>, IComparable<FileExtendedProperties>
    {
        private Dictionary<string, string> _properties;

        public FileExtendedProperties(string fileExtension)
        {
            this._properties = new Dictionary<string, string>();
            this.FileExtension = fileExtension;
        }

        public string FileExtension { get; private set; }

        public string this[string propertyName]
        {
            get
            {
                if (this._properties.ContainsKey(propertyName))
                    return this._properties[propertyName];
                return null;
            }
            set
            {
                if (this._properties.ContainsKey(propertyName))
                    this._properties[propertyName] = value;
                else
                    this._properties.Add(propertyName, value);
            }
        }

        public IEnumerator<FileProperty> GetEnumerator()
        {
            return new FileExtensionPropertyEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public int CompareTo(FileExtendedProperties other)
        {
            return this.FileExtension.CompareTo(other.FileExtension);
        }

        public class FileExtensionPropertyEnumerator : IEnumerator<FileProperty>
        {
            private IEnumerator<KeyValuePair<string, string>> enumerator;
            public FileExtensionPropertyEnumerator(FileExtendedProperties properties)
            {
                this.enumerator = properties._properties.GetEnumerator();
            }

            public FileProperty Current
            {
                get
                {
                    return new FileProperty(enumerator.Current.Key, enumerator.Current.Value);
                }
            }

            object IEnumerator.Current
            {
                get { return this.Current; }
            }

            public void Dispose()
            {
                this.enumerator.Dispose();
            }

            public bool MoveNext()
            {
                return this.enumerator.MoveNext();
            }

            public void Reset()
            {
                this.enumerator.Reset();
            }
        }
    }

    public struct FileProperty
    {
        public FileProperty(string name, string value)
        {
            this.Name = name;
            this.Value = value;
        }

        public string Name { get; private set; }
        public string Value { get; private set; }
    }
}
