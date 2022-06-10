using Common.Extensions;
using Common.Helpers;
using Common.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace FilePreview.BrowseFiles
{
    public partial class FileBrowserControl : UserControl
    {
        private static readonly string UpDirectoryKey = "$Up_One_Directory_Key";
        private List<FileBrowserControlItem> _itemGroups;
        private int _itemGroupIndex;
        private Thread thread;
        private CancellationTokenSource cancellation;

        public FileBrowserControl()
        {
            InitializeComponent();
            this._itemGroups = new List<FileBrowserControlItem>();
            this.listView1.MouseDoubleClick += FileBrowserPreview_MouseDoubleClick;
        }

        private void FileBrowserPreview_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListView view = this.listView1;
            ListViewHitTestInfo info = view.HitTest(e.X, e.Y);
            ListViewItem item = info.Item;

            if (this.cancellation != null)
                this.cancellation.Cancel();

            this.thread = new Thread((object s) =>
            {
                ThreadArgs args = (ThreadArgs)s;

                args.View.InvokeIfRequired(v =>
                {
                    if (item != null && item.Name.Equals(FileBrowserControl.UpDirectoryKey))
                    {
                        this._itemGroupIndex--;
                        v.Items.Clear();

                        args.TextBox.InvokeIfRequired(t => t.Text = this._itemGroups[this._itemGroupIndex].Name);

                        foreach (ListViewItem itm in this._itemGroups[this._itemGroupIndex].Items)
                        {
                            if (args.Token.IsCancellationRequested)
                                break;
                            v.Items.Add(itm);
                        }

                        this._itemGroups.RemoveRange(this._itemGroupIndex + 1, this._itemGroups.Count - (this._itemGroupIndex + 1));
                    }
                    else if (item != null && item.Name.EndsWith("\\"))
                    {
                        this._itemGroupIndex++;
                        v.Items.Clear();

                        if (this._itemGroupIndex >= this._itemGroups.Count)
                        {
                            this._itemGroups.Add(new FileBrowserControlItem(item.Name));

                            args.TextBox.InvokeIfRequired(t => t.Text = this._itemGroups[this._itemGroupIndex].Name);

                            ListViewItem updir = new ListViewItem("Up Directory", FileBrowserControl.UpDirectoryKey);
                            updir.Name = FileBrowserControl.UpDirectoryKey;
                            v.Items.Add(updir);
                            this._itemGroups[this._itemGroupIndex].Items.Add(updir);

                            foreach (string file in System.IO.Directory.EnumerateFiles(item.Name))
                            {
                                try
                                {
                                    if (args.Token.IsCancellationRequested)
                                        break;
                                    string key = FileBrowserControl.AddImage(file, v.LargeImageList, v.LargeImageList.ImageSize);
                                    FileBrowserControl.AddItem(v, file, key, FileHelper.GetFileName(file));
                                    this._itemGroups[this._itemGroupIndex].Items.Add(v.Items[file]);
                                }
                                catch (Exception ex) { }
                            }

                            foreach (string directory in System.IO.Directory.EnumerateDirectories(item.Name))
                            {
                                try
                                {
                                    if (args.Token.IsCancellationRequested)
                                        break;
                                    string currentDirectory = FileHelper.GetCurrentDirectoryName(directory);
                                    string key = FileBrowserControl.AddImage(directory + Constants.DirectoryKey, v.LargeImageList, v.LargeImageList.ImageSize);
                                    FileBrowserControl.AddItem(v, directory + Constants.DirectoryKey, key, currentDirectory);
                                    this._itemGroups[this._itemGroupIndex].Items.Add(v.Items[directory + Constants.DirectoryKey]);
                                }
                                catch (Exception ex) { }
                            }

                        }
                        else
                        {
                            args.TextBox.InvokeIfRequired(t => t.Text = this._itemGroups[this._itemGroupIndex].Name);

                            foreach (ListViewItem itm in this._itemGroups[this._itemGroupIndex].Items)
                            {
                                if (args.Token.IsCancellationRequested)
                                    break;
                                v.Items.Add(itm);
                            }
                        }
                    }
                    else
                    {
                        if (args.Token.IsCancellationRequested)
                            return;
                        view.SelectedItems.Clear();
                        try
                        {
                            new System.Diagnostics.Process()
                            {
                                StartInfo = {
                                    FileName = "explorer",
                                    Arguments = ("\"" + System.IO.Path.Combine(this.currentDirectoryTextBox.Text, item.Name) + "\"")
                                }
                            }.Start();
                        }catch(System.ComponentModel.Win32Exception win32Ex)
                        {
                            MessageBox.Show($"Could not start {item.Name} because:\n{win32Ex.Message}", "Explorer Process Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                });

            });

            this.thread.IsBackground = true;
            this.cancellation = new CancellationTokenSource();

            this.thread.Start(new ThreadArgs("", this.cancellation.Token, this.listView1, this.currentDirectoryTextBox));
        }


        internal void Clear()
        {
            try
            {
                if (this.cancellation != null)
                    this.cancellation.Cancel();
            }
            catch (Exception ex) { }
            this.listView1.Items.Clear();
            this._itemGroupIndex = 0;
            this._itemGroups.Clear();
        }

        internal bool DisplayBrowsablePreview(FileData? fileData)
        {
            if (!fileData.HasValue)
                return false;

            string path = fileData.Value.Path;
            ListView listView = this.listView1;

            if (this._itemGroups.Count <= this._itemGroupIndex)
                this._itemGroups.Add(new FileBrowserControlItem(path));

            if (this.cancellation != null)
                this.cancellation.Cancel();

            this.thread = new Thread((object s) =>
            {
                ThreadArgs args = (ThreadArgs)s;

                args.View.InvokeIfRequired(v =>
                {
                    args.TextBox.InvokeIfRequired(t => t.Text = args.Path);

                    this.DisplayBrowsablePreview(args);

                    foreach (FileData zipContent in fileData.Value.ZipContents)
                    {
                        try
                        {
                            args.Token.ThrowIfCancellationRequested();
                            string key = FileBrowserControl.AddImage(zipContent.Path, v.LargeImageList, v.LargeImageList.ImageSize);
                            string fileName = FileHelper.GetFileName(zipContent.Path);
                            string itemName = string.IsNullOrWhiteSpace(fileName) ? FileHelper.GetDirectoryName(zipContent.Path) : fileName;
                            FileBrowserControl.AddItem(v, itemName, key);
                            this._itemGroups[this._itemGroupIndex].Items.Add(v.Items[zipContent.Path]);
                        }
                        catch (Exception) { }
                    }
                });
            });

            this.thread.IsBackground = true;
            this.cancellation = new CancellationTokenSource();

            this.thread.Start(new ThreadArgs(path, this.cancellation.Token, listView, this.currentDirectoryTextBox));

            return true;
        }


        private void DisplayBrowsablePreview(ThreadArgs args)
        {
            if (this._itemGroups.Count <= this._itemGroupIndex)
                this._itemGroups.Add(new FileBrowserControlItem(args.Path));

            if (args.View.LargeImageList == null)
            {
                args.View.LargeImageList = new System.Windows.Forms.ImageList();
                args.View.LargeImageList.ImageSize = new Size(48, 48);
                args.View.LargeImageList.Images.Add(FileBrowserControl.UpDirectoryKey, Properties.Resources.upDirectory);
            }
            //else
            //    listView.LargeImageList.Images.Clear();

            if (Directory.Exists(args.Path))
            {
                foreach (string file in Directory.GetFiles(args.Path))
                {
                    try
                    {
                        args.Token.ThrowIfCancellationRequested();
                        string key = FileBrowserControl.AddImage(file, args.View.LargeImageList, args.View.LargeImageList.ImageSize);
                        FileBrowserControl.AddItem(args.View, file, key, FileHelper.GetFileName(file));
                        this._itemGroups[this._itemGroupIndex].Items.Add(args.View.Items[file]);
                    }
                    catch (Exception) { }
                }
                foreach (string directory in Directory.GetDirectories(args.Path))
                {
                    try
                    {
                        args.Token.ThrowIfCancellationRequested();
                        string currentDirectory = FileHelper.GetCurrentDirectoryName(directory);
                        string key = FileBrowserControl.AddImage(directory + Constants.DirectoryKey, args.View.LargeImageList, args.View.LargeImageList.ImageSize);
                        FileBrowserControl.AddItem(args.View, directory + Constants.DirectoryKey, key, currentDirectory);
                        this._itemGroups[this._itemGroupIndex].Items.Add(args.View.Items[directory + Constants.DirectoryKey]);
                    }
                    catch (Exception) { }
                }
            }
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
                key = FileBrowserControl.GetKey(path);
                bitmap1 = fileToIconConverter.GetImage(path, IconSize.ExtraLarge).ToBitmap();

                if (!imageList.Images.ContainsKey(key))
                    imageList.Images.Add(key, bitmap1);
            }

            return key;
        }

        private static void AddItem(ListView listView, string fileName, string imageKey, string text = null)
        {
            ListViewItem listViewItem = new ListViewItem(fileName, imageKey.Equals(string.Empty) ? Constants.NoneFileExtension : imageKey);
            listViewItem.Name = fileName;
            if (text != null)
                listViewItem.Text = text;
            listView.Items.Add(listViewItem);
        }

        private static string GetKey(string fileName)
        {
            return !FileHelper.GetFileName(fileName).Equals(string.Empty) || !fileName.EndsWith(Constants.DirectoryKey) ? FileHelper.GetFileExtension(fileName) : Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + Constants.DirectoryKey;
        }

        private class FileBrowserControlItem
        {
            public FileBrowserControlItem(string name)
            {
                this.Name = name;
                this.Items = new List<ListViewItem>();
            }

            public FileBrowserControlItem(string name, List<ListViewItem> items)
            {
                this.Name = name;
                this.Items = items;
            }


            public string Name { get; set; }
            public List<ListViewItem> Items { get; set; }
        }

        private struct ThreadArgs
        {
            public ThreadArgs(string path, CancellationToken token, ListView view, TextBox textBox)
            {
                this.Path = path;
                this.Token = token;
                this.View = view;
                this.TextBox = textBox;
            }

            public string Path { get; private set; }
            public CancellationToken Token { get; private set; }
            public ListView View { get; private set; }
            public TextBox TextBox { get; private set; }
        }
    }
}
