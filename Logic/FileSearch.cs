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
        private IShellDispatch5 _shell;

        public FileSearch(string root, bool searchSubdirectories = false, IShellDispatch5 shell = null)
        {
            this._root = root;
            this._fileEnumerator = this.Search(this._root, searchSubdirectories).GetEnumerator();
            this._shell = shell ?? new ShellClass(); // Models.Win32.Win32Methods.GetIShellDispatch5();
            //this._shell = obj ?? new ShellClass();/*(Shell)*/
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

        private static object shellLock = new object();
        [STAThread]
        private IEnumerable<FileData> Search(string path, bool searchSubdirectories = true)
        {
            IShellDispatch5 shell = this._shell;
            Folder objFolder = null;

            try
            {
                //Console.WriteLine("requesting shell lock @ 47");
                //lock (shellLock)
                //{
                objFolder = shell.NameSpace(path);
                //}
            }
            catch (Exception ex) // file would be inaccessable to shellclass
            {
                //throw;
            }

            if (objFolder == null)
            {
                IEnumerable<FileData> files = System.Linq.Enumerable.Empty<FileData>();
                try
                {
                    // some files/folders inacessable to shell class are picked up by .NET IO
                    // these files are likely link redirects or something. they dont seem to show when you browse explorer  
                    files = this.RegularSearch(path, searchSubdirectories);
                }
                catch (Exception ex2)
                {

                }

                foreach (FileData file in files)
                    yield return file;
                yield break;
            }


            foreach (FolderItem2 folderItem2 in objFolder.Items())
            {
                FolderItem2 item = folderItem2;

                //if (!Extensions.IsSystemObjectAccessable(item.Path))
                //{
                //    Console.WriteLine("{0} inaccessable", item.Path);
                //    continue;
                //}

                FileData? fileData = null;
                if (!item.IsFolder)
                {
                    try
                    {
                        fileData = new FileData(item.Path, this._shell);
                    }
                    catch (Exception ex)
                    {
                        Extensions.WriteToConsole("{0} error", item.Path);
                    }
                    if (fileData.HasValue)
                        yield return fileData.Value;
                    else continue;
                }
                else if (item.Type.Equals("Compressed (zipped) Folder"))
                {
                    try
                    {
                        fileData = new FileData(item.Path, this._shell);
                    }
                    catch (Exception ex)
                    {
                        Extensions.WriteToConsole("{0} error", item.Path);
                    }

                    if (!fileData.HasValue)
                        continue;
                    FileData f = fileData.Value;
                    f.IsZip = true; // = ""; //.IsZip = true;
                    this.AddZipContentsToFileData(item.Path, fileData.Value);
                    yield return fileData.Value;
                }
                else if (searchSubdirectories)
                {
                    foreach (FileData file in this.Search(item.Path))
                    {
                        //FileData file = fileData;
                        yield return file;
                        //file = new FileData();
                    }
                    item = null;
                }
            }
            try
            {
                //Marshal.ReleaseComObject(objFolder);
            }
            catch (Exception ex1)
            {

            }
        }

        private IEnumerable<FileData> RegularSearch(string path, bool searchSubdirectories = true)
        {
            IEnumerable<string> files = Extensions.AccessableFiles(path);

            foreach (string file in files)
            {
                yield return new FileData(file);
            }

            if (searchSubdirectories)
                foreach (string directory in Extensions.AccessableDirectories(path))
                {
                    IEnumerator<FileData> filez = this.Search(directory, searchSubdirectories).GetEnumerator();
                    while (filez.MoveNext())
                        yield return filez.Current;
                }
        }

        private void AddZipContentsToFileData(string path, FileData fileData)
        {
            IShellDispatch5 shell = this._shell;
            Folder objFolder = null;

            try
            {
                //Console.WriteLine("requesting shell lock @ 117");
                //lock (shellLock)
                //{
                objFolder = shell.NameSpace(path);
                //}
            }
            catch (Exception ex)
            {
                throw;
            }

            if (objFolder == null)
            {
                return;
            }

            foreach (FolderItem2 folderItem2 in objFolder.Items())
            {
                string str = folderItem2.IsFolder ? "\\" : string.Empty;
                fileData.ZipContents.Add(new FileData(folderItem2.Path + str, this._shell));
            }

            Marshal.ReleaseComObject(objFolder);
        }
    }
}
