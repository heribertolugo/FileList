using Common.Extensions;
using Common.Helpers;
using Common.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace FilePreview.BrowseFiles
{
    public class FileBrowserPreview : Common.Models.IPreviewFile
    {
        public FileBrowserPreview()
        {
            this.Viewer = new System.Windows.Forms.ListView();
        }

        public IEnumerable<string> Extensions
        {
            get { return new string[] { "\\", string.Empty }; }
        }

        public Control Viewer { get; private set; }

        public FileType FileType
        {
            get { return FileType.Browsable | FileType.Folder | FileType.Zip; }
        }

        public bool Load(string path)
        {
            try
            {
                return FileBrowserPreview.DisplayBrowsablePreview(string.IsNullOrWhiteSpace(path) ? null : (FileData?)(new FileData(path)), this.Viewer as ListView);
            } catch (Exception) { }
            return false;
        }

        public bool Load(FileData path)
        {
            try
            {
               return FileBrowserPreview.DisplayBrowsablePreview((FileData?)path, this.Viewer as ListView);
            } catch(Exception ex) { }
            return false;
        }


        private static bool DisplayBrowsablePreview(FileData? fileData, ListView listView)
        {            
            listView.Items.Clear();

            if (!fileData.HasValue)
                return false;

            string path = fileData.Value.Path;
            
            if (listView.LargeImageList == null)
                listView.LargeImageList = new System.Windows.Forms.ImageList();
            else
                listView.LargeImageList.Images.Clear();
            listView.LargeImageList.ImageSize = new Size(48, 48);

            if (Directory.Exists(path))
            {
                foreach (string file in Directory.GetFiles(path))
                {
                    try
                    {
                        string key = FileBrowserPreview.AddImage(file, listView.LargeImageList, listView.LargeImageList.ImageSize);  
                        FileBrowserPreview.AddItem(listView, file, key);
                    }
                    catch (Exception) { }
                }
                foreach (string directory in Directory.GetDirectories(path))
                {
                    try
                    {
                        string key = FileBrowserPreview.AddImage(directory + Constants.DirectoryKey, listView.LargeImageList, listView.LargeImageList.ImageSize); 
                        FileBrowserPreview.AddItem(listView, directory + Constants.DirectoryKey, key);
                    }
                    catch (Exception) { }
                }
            }
            else
            {
                foreach (FileData zipContent in fileData.Value.ZipContents)
                {
                    try
                    {
                        string key = FileBrowserPreview.AddImage(zipContent.Path, listView.LargeImageList, listView.LargeImageList.ImageSize); 
                        FileBrowserPreview.AddItem(listView, zipContent.Path, key);
                    }
                    catch (Exception) { }
                }
            }

            return true;
        }

        private static string AddImage(string path, ImageList imageList, Size imageSize)
        {
            Bitmap bitmap1;
            string key = null;
            try
            {
                Bitmap bitmap2 = new Bitmap(path);
                bitmap1 = new Bitmap(bitmap2, imageSize);
                bitmap2.Dispose();

                if (!imageList.Images.ContainsKey(path))
                    imageList.Images.Add(path, bitmap1);
                
                key = path;
            }
            catch (ArgumentException ex)
            {
                FileToIconConverter fileToIconConverter = new FileToIconConverter();
                key = FileBrowserPreview.GetKey(path);
                bitmap1 = fileToIconConverter.GetImage(path, IconSize.ExtraLarge).ToBitmap();

                if (!imageList.Images.ContainsKey(key))
                    imageList.Images.Add(key, bitmap1);
            }

            return key;
        }

        private static void AddItem(ListView listView, string fileName, string imageKey)
        {
            ListViewItem listViewItem = new ListViewItem(fileName, imageKey.Equals(string.Empty) ? Constants.NoneFileExtension : imageKey);
            listView.Items.Add(listViewItem);
        }

        private static string GetKey(string fileName)
        {
            return !Path.GetFileName(fileName).Equals(string.Empty) || !fileName.EndsWith(Constants.DirectoryKey) ? Path.GetExtension(fileName) : Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + Constants.DirectoryKey;
        }
    }
}
