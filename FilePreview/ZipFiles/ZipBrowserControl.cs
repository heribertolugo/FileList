using Common.Extensions;
using Common.Helpers;
using Common.Models;
using Common.Models.ZipExtractor;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace FilePreview.BrowseFiles
{
    public partial class ZipBrowserControl : UserControl
    {
        private static readonly string UpDirectoryKey = "$Up_One_Directory_Key";
        private List<FileBrowserControlItem> _itemGroups;
        private int _itemGroupIndex;
        private Thread thread;
        private CancellationTokenSource cancellation;
        private ZipFile _zipFile;

        public ZipBrowserControl()
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
                    if (item != null && item.Name.Equals(ZipBrowserControl.UpDirectoryKey))
                    {
                        this._itemGroupIndex--;
                        v.Items.Clear();

                        args.TextBox.InvokeIfRequired(t => t.Text = this._itemGroups[this._itemGroupIndex].Name);

                        foreach (ListViewItem itm in this._itemGroups[this._itemGroupIndex].Items)
                        {
                            if (args.Token.IsCancellationRequested)
                                break;
                            if (v.Items.Contains(itm))
                                v.Items.Remove(itm);
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

                            ListViewItem updir = new ListViewItem("Up Directory", ZipBrowserControl.UpDirectoryKey);
                            updir.Name = ZipBrowserControl.UpDirectoryKey;
                            v.Items.Add(updir);
                            this._itemGroups[this._itemGroupIndex].Items.Add(updir);

                            foreach (ZipFile.ZipFileItem zipItem in ((ZipFile.ZipFileItem)args.ZipItem).Children)
                            {
                                try
                                {
                                    args.Token.ThrowIfCancellationRequested();
                                    string key = ZipBrowserControl.AddImage(zipItem.Path, v.LargeImageList, v.LargeImageList.ImageSize);
                                    ZipBrowserControl.AddItem(v, zipItem, key);
                                    this._itemGroups[this._itemGroupIndex].Items.Add(v.Items[zipItem.Path]);
                                }
                                catch (Exception) { }
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
                        // **** DECOMPRESS AND OPEN?????? ****
                        //new System.Diagnostics.Process()
                        //{
                        //    StartInfo = {
                        //    FileName = "explorer",
                        //    Arguments = ("\"" + item.Name + "\"")
                        //}
                        //}.Start();
                    }
                });

            });

            this.thread.IsBackground = true;
            this.cancellation = new CancellationTokenSource();

            this.thread.Start(new ThreadArgs((ZipFile.ZipFileItem)item.Tag, this.cancellation.Token, this.listView1, this.currentDirectoryTextBox));
        }


        internal void Clear()
        {
            try
            {
                if (this.cancellation != null)
                    this.cancellation.Cancel();
                //this._zipFile.Dispose();
            }catch(Exception ex) { }
            this.listView1.Items.Clear();
            this._itemGroupIndex = 0;
            this._itemGroups.Clear();
        }

        internal bool DisplayBrowsablePreview(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                return false;

            ListView listView = this.listView1;

            if (this._itemGroups.Count <= this._itemGroupIndex)
                this._itemGroups.Add(new FileBrowserControlItem(path));

            if (this.cancellation != null)
                this.cancellation.Cancel();

            if (this._zipFile != null)
                this._zipFile.Dispose();

            this._zipFile = new ZipFile(path);

            this.thread = new Thread((object s) =>
            {
                ThreadArgs args = (ThreadArgs)s;
                ZipFile zip = (ZipFile)args.ZipItem;

                args.View.InvokeIfRequired(v =>
                {
                    args.TextBox.InvokeIfRequired(t => t.Text = zip.Path);

                    if (args.View.LargeImageList == null)
                    {
                        args.View.LargeImageList = new System.Windows.Forms.ImageList();
                        args.View.LargeImageList.ImageSize = new Size(48, 48);
                        args.View.LargeImageList.Images.Add(ZipBrowserControl.UpDirectoryKey, Properties.Resources.upDirectory);
                    }

                    foreach (ZipFile.ZipFileItem zipItem in zip.Contents())
                    {
                        try
                        {
                            args.Token.ThrowIfCancellationRequested();
                            string key = ZipBrowserControl.AddImage(zipItem.Path, v.LargeImageList, v.LargeImageList.ImageSize);
                            ZipBrowserControl.AddItem(v, zipItem, key);
                            this._itemGroups[this._itemGroupIndex].Items.Add(v.Items[zipItem.Path]);
                        }
                        catch (Exception) { }
                    }
                });
            });

            this.thread.IsBackground = true;
            this.cancellation = new CancellationTokenSource();
            
            this.thread.Start(new ThreadArgs(this._zipFile, this.cancellation.Token, listView, this.currentDirectoryTextBox));

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
                key = ZipBrowserControl.GetKey(path);
                // because we are in zip file, we cant pass the folder inside the zip to get image
                // so we pass appdata folder to get a folder image. some files dont have extension, so pass .none as extension
                bitmap1 = fileToIconConverter.GetImage(string.IsNullOrWhiteSpace(System.IO.Path.GetFileName(path)) ? Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) : (path.Contains(".") ? path : ".none"), IconSize.ExtraLarge).ToBitmap(); 

                if (!imageList.Images.ContainsKey(key))
                    imageList.Images.Add(key, bitmap1);
            }

            return key;
        }

        private static void AddItem(ListView listView, ZipFile.ZipFileItem file, string imageKey)
        {
            ListViewItem listViewItem = new ListViewItem(file.Path, imageKey.Equals(string.Empty) ? Constants.NoneFileExtension : imageKey);
            listViewItem.Name = file.Path;
            listViewItem.Tag = file;
            listView.Items.Add(listViewItem);
        }

        private static string GetKey(string fileName)
        {
            return !Path.GetFileName(fileName).Equals(string.Empty) || !fileName.EndsWith(Constants.DirectoryKey) ? Path.GetExtension(fileName) : Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + Constants.DirectoryKey;
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
            public ThreadArgs(object zipItem, CancellationToken token, ListView view, TextBox textBox)
            {
                this.ZipItem = zipItem;
                this.Token = token;
                this.View = view;
                this.TextBox = textBox;
            }

            public object ZipItem { get; private set; }
            public CancellationToken Token { get; private set; }
            public ListView View { get; private set; }
            public TextBox TextBox { get; private set; }
        }
    }
}
