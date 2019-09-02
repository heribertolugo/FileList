using FileList.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
//using System.IO.Packaging;
using System.Threading;
using System.Windows.Forms;

namespace FileList.Logic
{
    public class UiHelper
    {
        public static readonly string ErrorHeader = "1d-10T ERROR";
        public static readonly string ZipExtension = ".zip";
        public static readonly string DirectoryKey = "\\";
        public static readonly string NoneFileExtension = ".none";
        private static BackgroundWorker worker;

        public static void OpenFileSelectedNode(TreeView treeView)
        {
            if (treeView.SelectedNode == null)
                return;
            FileData? tag = treeView.SelectedNode.Tag as FileData?;
            UiHelper.OpenItem(tag.HasValue ? tag.Value.Path : treeView.SelectedNode.Text);
        }

        public static void OpenFileLocationSelectedNode(TreeView treeView)
        {
            if (treeView.SelectedNode == null)
                return;
            FileData? tag = treeView.SelectedNode.Tag as FileData?;
            UiHelper.OpenLocation(tag.HasValue ? tag.Value.Directory : treeView.SelectedNode.Text);
        }

        public static void Search(string path, FileListControl fileListControl)
        {
            if (UiHelper.worker == null)
            {
                UiHelper.worker = new BackgroundWorker();
                UiHelper.worker.DoWork += Worker_DoWork;
                UiHelper.worker.ProgressChanged += Worker_ProgressChanged;
                UiHelper.worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            }

            fileListControl.Clear();

            FileSearchWorkerArgs args = new FileSearchWorkerArgs(path, fileListControl, true);

            UiHelper.worker.RunWorkerAsync(args);
        }

        private static void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private static void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private static void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            FileSearchWorkerArgs args = (FileSearchWorkerArgs)e.Argument;

            //Thread thread = new Thread(() =>
            //{
            //    ConcurrentFileSearch searcher = new ConcurrentFileSearch(args.Path, args);
            //    searcher.Start();
            //});
            //thread.SetApartmentState(ApartmentState.STA);
            //thread.Start();

            ConcurrentFileSearch search = new ConcurrentFileSearch(args.Path, args);
            search.Start();
        }

        public static void DeleteItem(string path, FileListControl fileListControl)
        {
            if (MessageBox.Show("You are about to delete this item and its children.\nDo you want to delete?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
                return;
            if (Directory.Exists(path))
                Directory.Delete(path, true);
            if (File.Exists(path))
                File.Delete(path);
            fileListControl.RemoveByPath(path);
        }

        public static void OpenItem(string path)
        {
            new Process()
            {
                StartInfo = {
          FileName = "explorer",
          Arguments = ("\"" + path + "\"")
        }
            }.Start();
        }

        public static void OpenLocation(string path)
        {
            if (File.Exists(path))
                UiHelper.OpenItem(path.Replace(Path.GetFileName(path), string.Empty));
            else
                UiHelper.OpenItem(path);
        }

        public static void MoveNextFileDataFromMta(FileSearch search)
        {
            if (Thread.CurrentThread.GetApartmentState() == ApartmentState.STA)
            {
                UiHelper.MoveNextFileData(search);
            }
            else
            {
                Thread thread = new Thread(new ParameterizedThreadStart(UiHelper.MoveNextFileData));
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start(search);
                thread.Join();
            }
        }

        public static void MoveNextFileData(object search)
        {
            (search as FileSearch)?.GetNext();
        }

        public static void DisplayPreview(
          FileData fileData,
          TabControl tabControl,
          ListView contentsListView,
          Control imageViewerPanel,
          RichTextBox textViewerTextBox,
          TabPage documentTabPage,
          TabPage contentTabPage,
          TabPage imageTabPage,
          ImageLayout imageLayout)
        {
            string path = fileData.Path;
            switch (UiHelper.GetFileType(path))
            {
                case FileType.Unknown:
                    UiHelper.DisplayBrowsablePreview(new FileData?(), contentsListView);
                    UiHelper.DisplayImagePreview((string)null, imageViewerPanel, ImageLayout.None);
                    UiHelper.DisplayTextPreview((string)null, textViewerTextBox);
                    UiHelper.DisplayApplicationPreview(path, textViewerTextBox);
                    tabControl.SelectedTab = documentTabPage;
                    break;
                case FileType.Text:
                    UiHelper.DisplayBrowsablePreview(new FileData?(), contentsListView);
                    UiHelper.DisplayImagePreview((string)null, imageViewerPanel, ImageLayout.None);
                    UiHelper.DisplayTextPreview(path, textViewerTextBox);
                    tabControl.SelectedTab = documentTabPage;
                    break;
                case FileType.Media:
                    UiHelper.DisplayBrowsablePreview(new FileData?(), contentsListView);
                    UiHelper.DisplayImagePreview((string)null, imageViewerPanel, ImageLayout.None);
                    UiHelper.DisplayTextPreview((string)null, textViewerTextBox);
                    UiHelper.DisplayApplicationPreview(path, (TextBoxBase)textViewerTextBox);
                    tabControl.SelectedTab = documentTabPage;
                    break;
                case FileType.Browsable:
                case FileType.Folder:
                case FileType.Zip:
                    UiHelper.DisplayBrowsablePreview(new FileData?(fileData), contentsListView);
                    UiHelper.DisplayImagePreview((string)null, imageViewerPanel, ImageLayout.None);
                    UiHelper.DisplayTextPreview((string)null, textViewerTextBox);
                    tabControl.SelectedTab = contentTabPage;
                    break;
                case FileType.Application:
                    UiHelper.DisplayBrowsablePreview(new FileData?(), contentsListView);
                    UiHelper.DisplayImagePreview((string)null, imageViewerPanel, ImageLayout.None);
                    UiHelper.DisplayTextPreview((string)null, textViewerTextBox);
                    UiHelper.DisplayApplicationPreview(path, (TextBoxBase)textViewerTextBox);
                    tabControl.SelectedTab = documentTabPage;
                    break;
                case FileType.Image:
                    UiHelper.DisplayBrowsablePreview(new FileData?(), contentsListView);
                    UiHelper.DisplayImagePreview(path, imageViewerPanel, imageLayout);
                    UiHelper.DisplayTextPreview((string)null, textViewerTextBox);
                    tabControl.SelectedTab = imageTabPage;
                    break;
            }
        }

        public static void ResetPreviews(
          TabControl tabControl,
          ListView contentsListView,
          Control imageViewerPanel,
          RichTextBox textViewerTextBox,
          TabPage documentTabPage,
          TabPage contentTabPage,
          TabPage imageTabPage,
          ImageLayout imageLayout)
        {
            UiHelper.DisplayBrowsablePreview(new FileData?(), contentsListView);
            UiHelper.DisplayImagePreview((string)null, imageViewerPanel, ImageLayout.None);
            UiHelper.DisplayTextPreview((string)null, textViewerTextBox);
            UiHelper.DisplayApplicationPreview((string)null, (TextBoxBase)textViewerTextBox);
            tabControl.SelectedTab = documentTabPage;
        }

        private static void DisplayApplicationPreview(string path, TextBoxBase textBox)
        {
            if (path == null)
            {
                textBox.Text = (string)null;
            }
            else
            {
                try
                {
                    textBox.Text = File.ReadAllText(path);
                }
                catch (Exception ex)
                {
                    textBox.Text = (string)null;
                }
            }
        }

        private static void DisplayImagePreview(string path, Control control, ImageLayout imageLayout)
        {
            if (path == null)
            {
                control.BackgroundImage = null;
            }
            else
            {
                try
                {
                    control.BackgroundImage = new Bitmap(path);
                    control.BackgroundImageLayout = imageLayout;
                }
                catch (Exception ex)
                {
                    control.BackgroundImage = null;
                }
            }
        }

        private static void DisplayTextPreview(string path, RichTextBox textBox)
        {
            if (path == null)
            {
                textBox.Text = (string)null;
            }
            else
            {
                try
                {
                    textBox.LoadFile(path, Path.GetExtension(path).ToLower().Equals(".rtf") ? RichTextBoxStreamType.RichText : RichTextBoxStreamType.PlainText);
                }
                catch (Exception ex)
                {
                    textBox.Text = (string)null;
                }
            }
        }

        private static void DisplayBrowsablePreview(FileData? fileData, ListView listView)
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
                            dictionary.Add(directory + "\\", false);
                    }
                    else
                    {
                        try
                        {
                            foreach (FileData zipContent in fileData.Value.ZipContents)
                                dictionary.Add(zipContent.Path, true);
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                    listView.Items.Clear();
                    if (listView.LargeImageList == null)
                        listView.LargeImageList = new ImageList();
                    Size size = new Size();
                    foreach (KeyValuePair<string, bool> keyValuePair in dictionary)
                    {
                        string key = !Path.GetFileName(keyValuePair.Key).Equals(string.Empty) || !keyValuePair.Key.EndsWith(UiHelper.DirectoryKey) ? Path.GetExtension(keyValuePair.Key) : Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + UiHelper.DirectoryKey;
                        ListViewItem listViewItem = new ListViewItem(keyValuePair.Key, key.Equals(string.Empty) ? UiHelper.NoneFileExtension : key);
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
                            bitmap1 = fileToIconConverter.GetImage(key.Equals(string.Empty) ? keyValuePair.Key : key, FileToIconConverter.IconSize.ExtraLarge).ToBitmap();
                        }
                        MemoryStream memoryStream = new MemoryStream();
                        bitmap1.Save((Stream)memoryStream, ImageFormat.Bmp);
                        Convert.ToBase64String(memoryStream.ToArray());
                        if (!listView.LargeImageList.Images.ContainsKey(key))
                            listView.LargeImageList.Images.Add(key.Equals(string.Empty) ? UiHelper.NoneFileExtension : key, (Image)bitmap1);
                        size = bitmap1.Size;
                    }
                    listView.LargeImageList.ImageSize = size;
                }
                catch (Exception ex)
                {
                    listView.Items.Clear();
                }
            }
        }

        public static IEnumerable<string> GetZipContents(string zipPath)
        {
            List<string> stringList = new List<string>();
            //using (Package package = Package.Open(zipPath, FileMode.Open, FileAccess.Read))
            //{
            //    foreach (PackagePart part in package.GetParts())
            //    {
            //        if (part.Uri.IsFile)
            //            stringList.Add(Path.GetFileName(part.Uri.LocalPath));
            //        else
            //            stringList.Add(Path.GetDirectoryName(part.Uri.LocalPath));
            //    }
            //}
            return stringList;
        }

        public static FileType GetFileType(string fileName)
        {
            string str = "application/unknown";
            string lower = Path.GetExtension(fileName).ToLower();
            if (lower.Equals(string.Empty) && Directory.Exists(fileName))
            {
                str = string.Empty;
            }
            else
            {
                RegistryKey registryKey = Registry.ClassesRoot.OpenSubKey(lower);
                if (registryKey != null && registryKey.GetValue("Content Type") != null)
                    str = registryKey.GetValue("Content Type").ToString();
            }
            if (str.ToLowerInvariant().Contains("image"))
                return FileType.Image;
            if (str.ToLowerInvariant().Contains("text") || lower.Equals(".rtf"))
                return FileType.Text;
            if (str.ToLowerInvariant().Contains("audio") || str.ToLowerInvariant().Contains("video"))
                return FileType.Media;
            if (str.ToLowerInvariant().Contains("application") && !str.ToLowerInvariant().Contains(UiHelper.ZipExtension.Replace(".", string.Empty)))
                return FileType.Application;
            if (str.ToLowerInvariant().Equals(string.Empty) || str.ToLowerInvariant().Contains(UiHelper.ZipExtension.Replace(".", string.Empty)))
                return str.ToLowerInvariant().Contains(UiHelper.ZipExtension.Replace(".", string.Empty)) ? FileType.Zip : FileType.Folder;
            return FileType.Unknown;
        }

        public static string GetNodePath(TreeNode node)
        {
            FileData? tag = node.Tag as FileData?;
            return !tag.HasValue ? node.Text : tag.Value.Path;
        }
    }
}
