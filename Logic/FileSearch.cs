using Shell32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace FileList.Logic
{
    public class FileSearch
    {
        private string _root;
        private IEnumerator _fileEnumerator;
        private FileData? _current;

        public FileSearch(string root, bool searchSubdirectories = false)
        {
            this._root = root;
            this._fileEnumerator = this.Search(this._root, searchSubdirectories).GetEnumerator();
        }

        public FileData? GetNext()
        {
            this._current = this._fileEnumerator.MoveNext() ? new FileData?((FileData)this._fileEnumerator.Current) : new FileData?();
            return this._current;
        }

        public FileData? Current
        {
            get
            {
                return this._current;
            }
        }

        private IEnumerable<FileData> Search(string path, bool searchSubdirectories = true)
        {
            Shell shell = new ShellClass();
            Folder objFolder = null;

            try
            {
                objFolder = shell.NameSpace(path);
            }
            catch(Exception ex)
            {
                
            }

            if (objFolder == null)
            {
                //Marshal.ReleaseComObject(objFolder);
                Marshal.ReleaseComObject(shell);
                //yield return new FileData(path);
                yield break;
            }

            foreach (FolderItem2 folderItem2 in objFolder.Items())
            {
                FolderItem2 item = folderItem2;
                if (!item.IsFolder)
                    yield return new FileData(item.Path);
                else if (item.Type.Equals("Compressed (zipped) Folder"))
                {
                    FileData fileData = new FileData(item.Path);
                    fileData.IsZip = true;
                    this.AddZipContentsToFileData(item.Path, fileData);
                    yield return fileData;
                }
                else if (searchSubdirectories)
                {
                    foreach (FileData fileData in this.Search(item.Path))
                    {
                        FileData file = fileData;
                        yield return file;
                        file = new FileData();
                    }
                    item = null;
                }
            }
            Marshal.ReleaseComObject(objFolder);
            Marshal.ReleaseComObject(shell);
            GC.Collect(0, GCCollectionMode.Forced);
        }

        private void AddZipContentsToFileData(string path, FileData fileData)
        {
            Shell shell = new ShellClass();
            Folder objFolder = null;

            try
            {
                objFolder = (Folder)shell.NameSpace(path);
            }
            catch (Exception ex)
            {

            }

            if (objFolder == null)
            {
                //Marshal.ReleaseComObject(objFolder);
                Marshal.ReleaseComObject(shell);
                //fileData.ZipContents.Add(new FileData(path));
                return;
            }

            foreach (FolderItem2 folderItem2 in objFolder.Items())
            {
                string str = folderItem2.IsFolder ? "\\" : string.Empty;
                fileData.ZipContents.Add(new FileData(folderItem2.Path + str));
            }

            Marshal.ReleaseComObject(objFolder);
            Marshal.ReleaseComObject(shell);
        }
    }
}
