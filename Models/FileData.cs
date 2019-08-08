﻿using FileList.Logic;
using Shell32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileList
{
    public struct FileData
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

        public FileData(string path)
        {
            this._name = (string)null;
            this._extension = (string)null;
            this._diectory = (string)null;
            this._extendedProperties = new List<KeyValuePair<string, string>>();
            this._path = path;
            this._isZip = false;
            this._zipContents = new List<FileData>();
            this._sizeInKilobytes = new float?();
            this._isSizeSet = false;
            this._dateCreated = new DateTime?();
            this._isDateCreatedSet = false;
            this._dateModified = new DateTime?();
            this._isDateModifiedSet = false;
            if (FileData.FilePropertyNames == null || FileData.FilePropertyNames.Count < 1)
            {
                FileData.FilePropertyNames = new List<string>();
                FileData.LoadFilePropertyNames(System.IO.Path.GetDirectoryName(path));
            }
            this.LoadExtendedProperties();
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
                return this._extension;
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
                return (IEnumerable<KeyValuePair<string, string>>)this._extendedProperties;
            }
            private set
            {
            }
        }

        public IList<FileData> ZipContents
        {
            get
            {
                return (IList<FileData>)this._zipContents;
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
                    this._dateCreated = this.GetDateFromExtendedProperties("date created");
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
                    this._dateModified = this.GetDateFromExtendedProperties("date modified");
                return this._dateModified;
            }
            private set
            {
            }
        }

        private float? GetSizeInKiloBytes()
        {
            KeyValuePair<string, string> keyValuePair = this.ExtendedProperties.FirstOrDefault<KeyValuePair<string, string>>((Func<KeyValuePair<string, string>, bool>)(p => p.Key.ToLowerInvariant().Equals("size")));
            if (keyValuePair.Equals((object)new KeyValuePair<string, string>()))
                return new float?();
            return new float?(Misc.ConvertStorageValueToKb(keyValuePair.Value));
        }

        private DateTime? GetDateFromExtendedProperties(string propertyName)
        {
            DateTime result;
            if (!DateTime.TryParse(this.ExtendedProperties.FirstOrDefault<KeyValuePair<string, string>>((Func<KeyValuePair<string, string>, bool>)(p => p.Key.ToLowerInvariant().Equals(propertyName.ToLowerInvariant()))).Value, out result))
                return new DateTime?();
            return new DateTime?(result);
        }

        private void LoadExtendedProperties()
        {
            try
            {
                Folder folder = ((IShellDispatch6)new ShellClass()).NameSpace((object)System.IO.Path.GetDirectoryName(this.Path));
                FolderItem name = folder.ParseName(System.IO.Path.GetFileName(this.Path));
                for (int iColumn = 0; iColumn < FileData.FilePropertyNames.Count; ++iColumn)
                    this._extendedProperties.Add(new KeyValuePair<string, string>(FileData.FilePropertyNames[iColumn], folder.GetDetailsOf((object)name, iColumn)));
            }
            catch (Exception ex)
            {
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
                this._extendedProperties.Add(new KeyValuePair<string, string>("Length", string.Format("{0} bytes", (object)fileInfo.Length.ToString())));
                this._extendedProperties.Add(new KeyValuePair<string, string>("Last Accessed Time", fileInfo.LastAccessTime.ToString()));
            }
            catch (Exception ex)
            {
            }
        }

        private static void LoadFilePropertyNames(string nspace)
        {
            Folder folder = ((IShellDispatch6)new ShellClass()).NameSpace((object)nspace);
            for (int iColumn = 0; iColumn < (int)short.MaxValue; ++iColumn)
            {
                string detailsOf = folder.GetDetailsOf((object)null, iColumn);
                if (string.IsNullOrEmpty(detailsOf))
                    break;
                FileData.FilePropertyNames.Add(detailsOf);
            }
        }
    }
}
