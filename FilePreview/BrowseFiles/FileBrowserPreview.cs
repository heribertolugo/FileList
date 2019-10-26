using Common.Extensions;
using Common.Helpers;
using Common.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
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
            return FileBrowserPreview.DisplayBrowsablePreview(string.IsNullOrWhiteSpace(path) ? null : (FileData?)(new FileData(path)), this.Viewer as ListView);
        }

        public bool Load(FileData path)
        {
            return FileBrowserPreview.DisplayBrowsablePreview((FileData?)path, this.Viewer as ListView);
        }

        private static bool DisplayBrowsablePreview(FileData? fileData, ListView listView)
        {
            FileToIconConverter fileToIconConverter = new FileToIconConverter();
            if (!fileData.HasValue)
            {
                listView.Items.Clear();
            }
            else
            {
                try
                {
                    string path = fileData.Value.Path;
                    Dictionary<string, bool> dictionary = new Dictionary<string, bool>();
                    if (Directory.Exists(path))
                    {
                        foreach (string file in Directory.GetFiles(path))
                            dictionary.Add(file, true);
                        foreach (string directory in Directory.GetDirectories(path))
                            dictionary.Add(directory + Constants.DirectoryKey, false);
                    }
                    else
                    {
                        foreach (FileData zipContent in fileData.Value.ZipContents)
                        {
                            try
                            {
                                dictionary.Add(zipContent.Path, true);
                            }
                            catch (Exception ex)
                            {
                            }
                        }
                    }
                    listView.Items.Clear();
                    if (listView.LargeImageList == null)
                        listView.LargeImageList = new ImageList();
                    Size size = new Size();
                    foreach (KeyValuePair<string, bool> keyValuePair in dictionary)
                    {
                        string key = !Path.GetFileName(keyValuePair.Key).Equals(string.Empty) || !keyValuePair.Key.EndsWith(Constants.DirectoryKey) ? Path.GetExtension(keyValuePair.Key) : Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + Constants.DirectoryKey;
                        ListViewItem listViewItem = new ListViewItem(keyValuePair.Key, key.Equals(string.Empty) ? Constants.NoneFileExtension : key);
                        listView.Items.Add(listViewItem);
                        Bitmap bitmap1;
                        try
                        {
                            Size newSize = size.Equals((object)new Size()) ? new Size(48, 48) : size;
                            Bitmap bitmap2 = new Bitmap(keyValuePair.Key);
                            bitmap1 = new Bitmap((Image)bitmap2, newSize);
                            bitmap2.Dispose();
                            key = keyValuePair.Key;
                            listViewItem.ImageKey = key;
                        }
                        catch (ArgumentException ex)
                        {
                            bitmap1 = fileToIconConverter.GetImage(key.Equals(string.Empty) ? keyValuePair.Key : key, IconSize.ExtraLarge).ToBitmap();
                        }
                        MemoryStream memoryStream = new MemoryStream();
                        bitmap1.Save((Stream)memoryStream, ImageFormat.Bmp);
                        Convert.ToBase64String(memoryStream.ToArray());
                        if (!listView.LargeImageList.Images.ContainsKey(key))
                            listView.LargeImageList.Images.Add(key.Equals(string.Empty) ? Constants.NoneFileExtension : key, (Image)bitmap1);
                        size = bitmap1.Size;
                    }
                    listView.LargeImageList.ImageSize = size;
                }
                catch (Exception ex)
                {
                    listView.Items.Clear();
                    return false;
                }
            }

            return true;
        }
    }
}
