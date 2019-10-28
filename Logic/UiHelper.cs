using Common.Extensions;
using Common.Helpers;
using Common.Models;
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
        private static CancellationTokenSource cancellationToken;
        private static Action<ConcurrentFileSearchEventArgs> _searchFinishedCallback;

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

        public static void Search(string path, FileListControl fileListControl, Action<ConcurrentFileSearchEventArgs> searchFinishedCallback)
        {
            if (UiHelper.worker == null)
            {
                UiHelper.worker = new BackgroundWorker();
                UiHelper.worker.DoWork += Worker_DoWork;
                UiHelper.worker.ProgressChanged += Worker_ProgressChanged;
                UiHelper.worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
                UiHelper.worker.WorkerSupportsCancellation = true;
            }

            fileListControl.Clear();

            UiHelper.cancellationToken = new CancellationTokenSource();
            UiHelper._searchFinishedCallback = searchFinishedCallback;

            FileSearchWorkerArgs args = new FileSearchWorkerArgs(path, fileListControl, true, UiHelper.cancellationToken.Token);

            UiHelper.worker.RunWorkerAsync(args);
        }

        public static void CancelSearch()
        {
            if (UiHelper.worker != null && UiHelper.cancellationToken != null && !UiHelper.cancellationToken.IsCancellationRequested)
            {
                UiHelper.cancellationToken.Cancel();
            }
        }

        private static void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private static void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            throw new NotImplementedException();
        }
        private static void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            FileSearchWorkerArgs args = (FileSearchWorkerArgs)e.Argument;

            ConcurrentFileSearch search = new ConcurrentFileSearch(args.Path, args);
            search.Finished += Search_Finished;
            search.Start();
        }

        private static void Search_Finished(object sender, ConcurrentFileSearchEventArgs e)
        {
            // sender as ConcurrentFileSearch.Dispose()
            UiHelper.cancellationToken.Dispose();
            UiHelper._searchFinishedCallback(e);
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

        public static bool DisplayPreview(FileData fileData, FilePreview.Previewers previewers, Control control)
        {
            FileType fileType = fileData.GetFileType();
            IPreviewFile previewFile = previewers.GetPreviewer(fileType == FileType.Application? FileType.Unknown : fileType);

            if ((control.Tag as IPreviewFile) != null)
                (control.Tag as IPreviewFile).Clear();

            control.Controls.Clear();
            if (previewFile == null)
                return false;
            control.Controls.Add(previewFile.Viewer);
            previewFile.Viewer.Dock = DockStyle.Fill;
            control.Tag = previewFile;
            return previewFile.LoadFile(fileData);
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
            throw new NotImplementedException();
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
