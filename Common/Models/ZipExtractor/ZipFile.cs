using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Common.Models.ZipExtractor
{
    public class ZipFile : IDisposable
    {
        private const string sevenZipDll = "7z.dll";
        private SevenZipLoader sevenZipFormat;
        private IInArchive Archive;
        private InStreamWrapper ArchiveStream;
        private uint itemCount;
        private Dictionary<string, ZipFileItem> _items;
        private string _path;
        private ulong CheckPos = 32 * 1024;

        public ZipFile(string path)
        {
            this._items = new Dictionary<string, ZipFileItem>();
            this._path = path;

        }

        public string Path { get { return this._path; } }

        public IEnumerable<ZipFileItem> Contents()
        {
            if (this._items.Count < 1)
            {
                this.OpenStream();
                uint counter = 0;

                while (counter < this.itemCount)
                {
                    bool isFolder = this.GetProperty<bool>(this.Archive, ItemPropId.kpidIsFolder, counter);
                    string path = this.GetProperty<string>(this.Archive, ItemPropId.kpidPath, counter);
                    if (string.IsNullOrEmpty(path))
                    {
                        counter++;
                        continue;
                    }
                    string fullPath = System.IO.Path.Combine(this._path, path);
                    string root = fullPath.Substring(0, fullPath.TrimEnd('\\').LastIndexOf('\\'));
                    bool isRoot = root.Equals(this._path);
                    ZipFileItem newItem = null;

                    if (string.IsNullOrWhiteSpace(path))
                        path = "\\";

                    if (!root.EndsWith("\\"))
                        root += '\\';

                    if (isFolder)
                    {
                        if (!fullPath.EndsWith("\\"))
                            fullPath += '\\';

                        foreach (ZipFileItem itm in this.EnsureParents(path))
                            yield return itm;

                        if (!this._items.ContainsKey(root))
                            this._items.Add(root, new ZipFileItem(root, counter));
                        if (!this._items.ContainsKey(fullPath))
                            this._items.Add(fullPath, new ZipFileItem(fullPath, counter));
                        //else
                        //    this._items[fullPath] = new ZipFileItem(fullPath, counter);
                        newItem = this._items[fullPath];

                        if (!this._items[root].Children.Contains(newItem))
                            this._items[root].Children.Add(newItem);
                    }
                    else
                    {
                        foreach (ZipFileItem itm in this.EnsureParents(path))
                            yield return itm;

                        if (!this._items.ContainsKey(root))
                            this._items.Add(root, new ZipFileItem(root, 0));

                        newItem = new ZipFileItem(fullPath, counter);
                        this._items[root].Children.Add(newItem);
                    }

                    this.SetProperties(newItem, this.Archive, counter);
                    newItem.Parent = this._items[root];

                    counter++;

                    if (!this._items[root].Children.Contains(newItem))
                        this._items[root].Children.Add(newItem);

                    Console.WriteLine(fullPath);

                    if (isRoot)
                        yield return newItem;
                }

                if (Archive != null)
                    Archive.Close();
                if (sevenZipFormat != null)
                    sevenZipFormat.Dispose();
                if (ArchiveStream != null)
                    ArchiveStream.Dispose();
            }
            else
            {
                foreach (ZipFileItem item in this._items[this._path + "\\"].Children)
                    yield return item;
            }
        }

        private IEnumerable<ZipFileItem> EnsureParents(string path)
        {
            IList<string> parents = path.Split('\\').ToList();
            parents.Insert(0, "\\");

            string currentParent = this._path;
            int count = 0;

            foreach (string parent in parents)
            {
                string previousParent = currentParent;
                currentParent += parent;

                if (!currentParent.EndsWith("\\"))
                    currentParent += '\\';

                if (parent.Contains("."))
                    break;

                if (!this._items.ContainsKey(currentParent))
                {
                    this._items.Add(currentParent, new ZipFileItem(currentParent, 0));

                    if (count == 1)
                        yield return this._items[currentParent];
                }

                if (this._items.ContainsKey(previousParent) && this._items[previousParent].Children.FirstOrDefault(z => z.Path.Equals(currentParent)) == null)
                {
                    this._items[previousParent].Children.Add(this._items[currentParent]);
                    this._items[currentParent].Parent = this._items[previousParent];
                }

                count++;
            }
        }

        public IEnumerable<ZipFileItem> Contents(ZipFileItem zipItem)
        {
            if (this._items.ContainsKey(zipItem.Path))
            {
                foreach (ZipFileItem child in zipItem.Children)
                    yield return child;
            }
        }

        private void SetProperties(ZipFileItem item, IInArchive archive, uint index)
        {
            foreach (ItemPropId propId in Enum.GetValues(typeof(ItemPropId)))
                item.AddProperty(propId, this.GetProperty<object>(archive, propId, index));
        }

        private T GetProperty<T>(IInArchive archive, ItemPropId propId, uint index)
        {
            try
            {
                PropVariant Name = new PropVariant();
                archive.GetProperty(index, propId, ref Name);
                T value = (T)Name.GetObject();
                Name.Clear();
                return value;
            }
            catch (Exception ex) { }

            return default(T);
        }

        private InStreamWrapper OpenStream()
        {
            if (this.ArchiveStream == null)
            {
                this.sevenZipFormat = new SevenZipLoader(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), sevenZipDll));
                this.Archive = sevenZipFormat.CreateInArchive(this.GetFormatFromExtension(System.IO.Path.GetExtension(this._path)).Guid);
                if (Archive == null)
                    return null;
                this.ArchiveStream = new InStreamWrapper(File.OpenRead(this._path));

                if (this.Archive.Open(ArchiveStream, ref CheckPos, null) != 0)
                    return null;

                this.itemCount = Archive.GetNumberOfItems();
            }

            return this.ArchiveStream;
        }

        private SevenZipFormat GetFormatFromExtension(string extension)
        {
            //KnownSevenZipFormat format;

            //if (!Enum.TryParse(extension.Replace(".7-", "seven").Replace(".7", "seven").Replace(".", ""), true, out format))
            //    format = KnownSevenZipFormat.SevenZip;

            //return format;
            //extension;
            return SevenZipFormat.Values.FirstOrDefault(v => v.Extensions.Contains(extension.Trim(new char[] { '.' })));
        }


        #region IDisposable
        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    
                }

                this._disposed = true;

            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~ZipFile()
        {
            this.Dispose(false);
        }
        #endregion

        #region ZipItem
        public sealed class ZipFileItem : IEquatable<ZipFileItem>
        {
            private Dictionary<ItemPropId, object> properties;
            string _fullPath;
            uint _index;
            private IList<ZipFileItem> _children;
            public ZipFileItem(string fullPath, uint index)
            {
                this.properties = new Dictionary<ItemPropId, object>();
                this._fullPath = fullPath;
                this._index = index;
                this._children = new List<ZipFileItem>();

                this.Name = this._fullPath.Substring(this._fullPath.TrimEnd('\\').LastIndexOf("\\") + 1);
            }

            public void AddProperty(ItemPropId propId, object value)
            {
                if (!this.properties.ContainsKey(propId))
                    this.properties.Add(propId, value);
                else
                    this.properties[propId] = value;
            }

            public string Path { get { return this._fullPath; } }
            public string Name
            {
                get { return this.properties[ItemPropId.kpidPath] as string; }
                private set
                {
                    if (!this.properties.ContainsKey(ItemPropId.kpidPath))
                        this.properties.Add(ItemPropId.kpidPath, value);
                    else
                        this.properties[ItemPropId.kpidPath] = value;
                }
            }
            public bool? IsFolder { get { return this.properties[ItemPropId.kpidIsFolder] as bool?; } }
            public bool? IsEncrypted { get { return this.properties[ItemPropId.kpidEncrypted] as bool?; } }
            public ulong? Size { get { return this.properties[ItemPropId.kpidSize] as ulong?; } }
            public ulong? PackedSize { get { return this.properties[ItemPropId.kpidPackedSize] as ulong?; } }
            public DateTime? CreationTime { get { return this.properties[ItemPropId.kpidCreationTime] as DateTime?; } }
            public DateTime? LastWriteTime { get { return this.properties[ItemPropId.kpidLastWriteTime] as DateTime?; } }
            public DateTime? LastAccessTime { get { return this.properties[ItemPropId.kpidLastAccessTime] as DateTime?; } }
            public uint? Crc { get { return this.properties[ItemPropId.kpidCRC] as uint?; } }
            public uint? Attributes { get { return this.properties[ItemPropId.kpidAttributes] as uint?; } }
            public string Comment { get { return this.properties[ItemPropId.kpidComment] as string; } }
            public string HostOS { get { return this.properties[ItemPropId.kpidHostOS] as string; } }
            public string Method { get { return this.properties[ItemPropId.kpidMethod] as string; } }

            public bool? IsSplitBefore { get { return this.properties[ItemPropId.kpidSplitBefore] as bool?; } }
            public bool? IsSplitAfter { get { return this.properties[ItemPropId.kpidSplitAfter] as bool?; } }

            public uint FileIndex { get { return this._index; } }

            public ZipFileItem Parent { get; set; }

            public IList<ZipFileItem> Children { get { return this._children; } private set { } }

            public FileData ToFileData()
            {
                try
                {
                    List<KeyValuePair<string, string>> extendedProperties = new List<KeyValuePair<string, string>>();
                    object fileDataObject = new FileData(this.properties[ItemPropId.kpidPath].ToString());
                    Type type = typeof(FileData);
                    FileData fileData;

                    System.Reflection.FieldInfo p = type.GetField("_extendedProperties", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);

                    extendedProperties.AddRange(this.properties.Select(kvp => new KeyValuePair<string, string>(Helpers.EnumHelpers.GetFriendly(kvp.Key).Replace("kpid", "").Trim(), kvp.Value?.ToString())));

                    p.SetValue(fileDataObject, extendedProperties);

                    fileData = (FileData)fileDataObject;

                    foreach (ZipFileItem item in this.Children)
                        fileData.ZipContents.Add(item.ToFileData());

                    return fileData;
                }
                catch (Exception ex)
                {
                    return default(FileData);
                }
            }

            public override string ToString()
            {
                return this._fullPath;
            }

            public bool Equals(ZipFileItem other)
            {
                return string.Equals(this._fullPath, other._fullPath);
            }
        }
        #endregion

        public static implicit operator ZipFileItem(ZipFile z)
        {
            if (z._items.Count > 0 && z._items.ContainsKey("\\"))
                return z._items["\\"];
            return default(ZipFile);
        }
    }

}
