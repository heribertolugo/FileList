﻿using FileList.Logic;
using FileList.Views;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Win32.Constants;
using Win32.Libraries;
using Common.Extensions;
using Common.Models;

namespace FileList.Models
{
    public partial class FileListControl : UserControl
    {
        public event EventHandler<FileDataSelectedEventArgs> OnFileDataSelected;
        public event EventHandler<FileDataSelectedEventArgs> OnOpenFileDataClicked;
        public event EventHandler<FileDataSelectedEventArgs> OnOpenLocationClicked;
        public event EventHandler<FileDataSelectedEventArgs> OnDeleteFileDataClicked;

        private enum IsParentNode
        {
            DontKnow,
            Yes,
            No
        }

        ///// <summary>
        ///// comparer we use to sort treenodes
        ///// </summary>
        //private IMultiComparer<TreeNode> treeNodeComparer;
        ///// <summary>
        ///// contains the nodes in our treeview. we remove and add from here to change node visibility in treeview.
        ///// </summary>
        //private readonly List<TreeNode> TreeDataSource;
        /// <summary>
        /// contains all the files found in form of treenodes, sorted by our custom sort (treeNodeComparer).
        /// </summary>
        private SortedSet<TreeNode> SortedNodes;
        /// <summary>
        /// contains all the files found in form of treenodes. key is the path for the parent node. used for quick lookup of nodes by path.
        /// </summary>
        private readonly Dictionary<string, TreeNode> _treeKeys;
        /// <summary>
        /// trigger nodes must be same instance as nodes in TreeView
        /// </summary>
        private readonly Dictionary<string, ChildNodeTriggers> ChildNodeTriggers;
        /// <summary>
        /// trigger nodes must be same instance as nodes in TreeView
        /// </summary>
        private TreeNode topTrigger;
        /// <summary>
        /// trigger nodes must be same instance as nodes in TreeView
        /// </summary>
        private TreeNode bottomTrigger;
        /// <summary>
        /// Keeps track of our selected node for peristing the selection during virtual scrolling
        /// </summary>
        private TreeNode lastSelectedNode;
        /// <summary>
        /// contains all file extensions found in all files found in search
        /// </summary>
        private readonly SortedSet<string> _extensions;
        /// <summary>
        /// for to allow user to sort treeview
        /// </summary>
        private FileFilterForm filterForm;
        /// <summary>
        /// allows tracking of sort order
        /// </summary>
        private FileDataSortStack _sortStack;
        /// <summary>
        /// allows disabling of event handlers when controls are modified programmatically
        /// </summary>
        private bool modifyFileTypesListBoxInternal = false;
        /// <summary>
        /// keeps track if all nodes were selected to be checked
        /// </summary>
        private bool _allCheck;
        /// <summary>
        /// keeps trtack of how many files we've found
        /// </summary>
        private int _fileCount = 0;
        private ToolTip _treeViewToolTip = new ToolTip();

        private const int WM_HSCROLL = 276;
        private const int SB_LEFT = 6;

        public FileListControl()
        {
            InitializeComponent();
            this.filterForm = new FileFilterForm();
            this.filterForm.VisibleChanged += new EventHandler(this.FilterForm_VisibleChanged);
            this._sortStack = new FileDataSortStack();
            this._extensions = new SortedSet<string>();
            this._treeKeys = new Dictionary<string, TreeNode>();
            this._sortStack.Push(Filter.Name, new CompareFiledataNodeByName(SortOrder.Ascending));
            MultiCompareFileData multi = new MultiCompareFileData(this._sortStack); 
            this.SortedNodes = new SortedSet<TreeNode>(multi);
            this.treeView1.TreeViewNodeSorter = (System.Collections.IComparer)multi;
            this.ChildNodeTriggers = new Dictionary<string, ChildNodeTriggers>();
            this.FileTypeListSorted = true;
        }

        #region Public


        public System.Windows.Forms.ImageList TreeImageList
        {
            get
            {
                return this.treeView1.ImageList;
            }
            set
            {
                this.treeView1.ImageList = value;
            }
        }

        private bool FileTypeListSorted
        {
            get
            {
                return this.fileTypesCheckedListBox.Sorted;
            }
            set
            {
                this.fileTypesCheckedListBox.Sorted = value;
            }
        }

        public FileData? SelectedFile
        {
            get
            {
                return this.treeView1.SelectedNode?.Tag as FileData?;
            }
            private set
            {
            }
        }

        public string SelectedText
        {
            get
            {
                return this.treeView1.SelectedNode?.Text;
            }
            private set
            {
            }
        }

        public string SelectedPath
        {
            get
            {
                return this.SelectedFile.HasValue ? this.SelectedFile.Value.Path : this.SelectedText;
            }
            private set
            {
            }
        }

        public int SearcherThreads
        {
            get
            {
                return (int)this.scoutCountLabel.Value;
            }
            set
            {
                this.scoutCountLabel.Value = value;
            }
        }

        public bool RemoveByPath(string path)
        {
            // if path doesnt exist, then this should be a child node. 
            // get directory from path, then try delete node using that.
            // if that fails, search all nodes for path
            TreeNode treeNode = this.treeView1.Nodes.ContainsKey(path) ? this.treeView1.Nodes[path] : null;
            string parentName = path;
            string childName = null;
            bool removed = false;

            if (treeNode == null)
            {
                parentName = System.IO.Path.GetDirectoryName(path);
                TreeNode parentNode = this.treeView1.Nodes[System.IO.Path.GetDirectoryName(path)];
                childName = System.IO.Path.GetFileName(path);

                if (parentNode.Nodes.ContainsKey(childName))
                {
                    treeNode = parentNode.Nodes[childName];
                    this._treeKeys[parentName].Nodes.RemoveByKey(childName);
                    removed = true;
                }
                else
                    foreach (TreeNode node in this.treeView1.Nodes)
                    {
                        if (node.Nodes.ContainsKey(path))
                        {
                            treeNode = node.Nodes[path];
                            this._treeKeys[node.Name].Nodes.RemoveByKey(treeNode.Name);
                            removed = true;
                            break;
                        }
                    }
            }

            if (treeNode != null)
                treeNode.Remove();
            return removed;
        }

        public FileDataGroup GetFileDataGroupFromSelected()
        {
            TreeNode treeNode = this.treeView1.SelectedNode;
            if (treeNode == null)
                return new FileDataGroup(null, Enumerable.Empty<FileData>());
            if (treeNode.Level > 0)
                treeNode = this._treeKeys[treeNode.Parent.Name];
            else treeNode = this._treeKeys[treeNode.Name];
            return new FileDataGroup(treeNode.Text, treeNode.Nodes.Cast<TreeNode>().Select(n => (FileData)n.Tag));
        }

        public string[] GetCheckedPaths()
        {
            return this.GetCheckedNodes(this.treeView1).Where(n =>
            {
                if ((n.Tag as FileData?).HasValue)
                    return true;
                if (Path.HasExtension(n.Text))
                    return Path.GetExtension(n.Text).ToLowerInvariant().Equals(UiHelper.ZipExtension);
                return false;
            }).Select(n =>
            {
                FileData? tag = n.Tag as FileData?;
                return tag.HasValue ? tag.Value.Path : n.Text;
            }).ToArray();
        }

        public FileData[] GetCheckedFileData()
        {
            return this.GetCheckedNodes(this.treeView1).Where(n => (n.Tag as FileData?).HasValue).Select(n => (FileData)n.Tag).ToArray();
        }

        //public void SortTree()
        //{
        //    this.Enabled = false;
        //    this.SortTree(this.treeView1, this._sortStack);
        //    this.ScrollTreeToTop();
        //    this.Enabled = true;
        //}

        public void ExpandTree()
        {
            this.treeView1.ExpandAll();
        }

        public void ScrollTreeToTop()
        {
            if (this.treeView1.Nodes.Count > 0)
                this.treeView1.Nodes[0].EnsureVisible();
            FileListControl.ScrollTreeLeft(this.treeView1);
        }

        public void Clear()
        {
            //this.TreeDataSource.Clear();
            this.SortedNodes.Clear();
            this.ChildNodeTriggers.Clear();
            this._extensions.Clear();
            this._sortStack.Clear();
            this.treeView1.Nodes.Clear();
            this.fileTypesCheckedListBox.Items.Clear();
            this.sizeSortButton.SortOrder = SortOrder.None;
            this.dateCreatedButton.SortOrder = SortOrder.None;
            this.dateModifiedButton.SortOrder = SortOrder.None;
            this.filterForm.Reset();
            this._treeKeys.Clear();
            this._fileCount = 0;
            this._sortStack.Push(Filter.Name, new CompareFiledataNodeByName(SortOrder.Ascending));
        }



        /// <summary>
        /// Adds FileData to TreeView source, and to FileExtensionsList source with the provided imageKey.
        /// </summary>
        /// <param name="fileData"></param>
        /// <param name="imageKey"></param>
        public void AddFileData(FileData fileData)
        {
            //this.treeView1.BeginUpdate();
            this.modifyFileTypesListBoxInternal = true;
            TreeNode parentNode;
            string fileImageKey = fileData.Extension.Equals(string.Empty) ? UiHelper.NoneFileExtension : fileData.Extension;
            string directoryImageKey = fileData.Directory.ToLowerInvariant().Contains(UiHelper.ZipExtension) ? UiHelper.ZipExtension : UiHelper.DirectoryKey;

            if (this._treeKeys.ContainsKey(fileData.Directory))
            {
                parentNode = this._treeKeys[fileData.Directory];
            }
            else
            {
                parentNode = new TreeNode(fileData.Directory);
                parentNode.Name = fileData.Directory;
                parentNode.ImageKey = directoryImageKey;
                parentNode.SelectedImageKey = directoryImageKey;
                parentNode.StateImageKey = directoryImageKey;
                parentNode.Name = fileData.Directory;

                // use the same node instance for our master lists
                this._treeKeys.Add(fileData.Directory, parentNode);
                
                if (!this.SortedNodes.Add(parentNode))
                {
                    System.Diagnostics.Debugger.Break();
                }

                // this new item found needs to be inserted somewhere before last node currently in tree because of sort order
                if (this.SortedNodes.IndexOf(parentNode) < (this.treeView1.VisibleCount * 2))
                {
                    this.treeView1.Nodes.Add((TreeNode)parentNode.Clone());

                    if (this.treeView1.Nodes.Count == (this.treeView1.VisibleCount * 2))
                        this.SetTriggerNodes();
                }

                // we added too many items to tree. remove
                if (this.treeView1.Nodes.Count > (this.treeView1.VisibleCount * 2))
                {
                    this.treeView1.Nodes.RemoveAt(this.treeView1.Nodes.Count - 1);
                    this.SetTriggerNodes();
                }
            }

            if (!this._extensions.Contains(fileData.Extension))
            {
                this._extensions.Add(fileData.Extension);
                this.fileTypesCheckedListBox.Items.Add(new FileExtensionCount(fileData.Extension, 1), true);
            }
            else
            {
                this.fileTypesCheckedListBox.Items.Cast<FileExtensionCount>().FirstOrDefault(f => f.Extension.Equals(fileData.Extension)).IncrementCount();
            }

            TreeNode node = new TreeNode(fileData.Name + fileData.Extension);
            node.Tag = fileData;
            //node.ToolTipText = string.Join(Environment.NewLine, fileData.ExtendedProperties.Select(p => string.Format("{0}: {1}", p.Key, p.Value)).ToArray());
            node.ImageKey = fileImageKey;
            node.SelectedImageKey = fileImageKey;
            node.StateImageKey = fileImageKey;
            node.Name = fileData.Name + fileData.Extension;

            parentNode.Nodes.Add(node);

            // we need to add a node to treeview to enable node expanding in treeview
            if (this.treeView1.Nodes.ContainsKey(parentNode.Name) && (this.treeView1.Nodes[parentNode.Name].Nodes.Count < 1 || this.ShouldNodeVisible(node, IsParentNode.No)))
                this.treeView1.Nodes[parentNode.Name].Nodes.Add((TreeNode)node.Clone());

            ++this._fileCount;
            this.countLabel.Text = this._fileCount.ToString();

            //this.treeView1.EndUpdate();
            this.modifyFileTypesListBoxInternal = false;
        }

        public void ShowNotification(string message)
        {
            this.notificationLabel.Text = message;
            this.notificationLabel.Visible = true;
        }

        public void HideNotification()
        {
            this.notificationLabel.Visible = false;
        }

        #endregion

        #region Event Handlers
        private void FilterForm_VisibleChanged(object sender, EventArgs e)
        {
            if (this.filterForm.Visible)
                return;
            this.SetVisibleNodes();

            List<string> filters = new List<string>();
            string sizeDescription = this.filterForm.SizeFilter.ToString();
            string modifiedDateDescription = this.filterForm.ModifiedDateFilter.ToString();
            string createdDateDescription = this.filterForm.CreatedDateFilter.ToString();

            if (!string.IsNullOrEmpty(sizeDescription))
                filters.Add($"size {this.filterForm.SizeFilter.ToString()}");
            if (!string.IsNullOrEmpty(modifiedDateDescription))
                filters.Add($"modified {modifiedDateDescription}");
            if (!string.IsNullOrEmpty(createdDateDescription))
                filters.Add($"created {createdDateDescription}");

            string filterDescription = string.Join(" ,and ", filters);

            if (!string.IsNullOrEmpty(filterDescription))
                this.filterSummaryLabel.Text = string.Concat("Where ", filterDescription);
            else
                this.filterSummaryLabel.Text = string.Empty;
        }

        private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node == this.treeView1.SelectedNode)
                this.lastSelectedNode = e.Node;
            EventHandler<FileDataSelectedEventArgs> fileDataSelected = this.OnFileDataSelected;
            if (fileDataSelected == null)
                return;
            fileDataSelected(this, this.GetEventArgs());
        }

        private void FileTypesCheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (this.modifyFileTypesListBoxInternal)
                return;

            Func<FileData, bool> filterPredicate = this.GetFilterPredicate(e);
            TreeNode[] nodes = this.SortedNodes.Where(n => n.Nodes.Cast<TreeNode>().Any(c => filterPredicate((FileData)c.Tag))).Take(this.treeView1.VisibleCount*2)
                .Select(n => 
                {
                    TreeNode clone = (TreeNode)n.Clone();
                    clone.Nodes.Add((TreeNode)n.Nodes[0].Clone());
                    return clone;
                }).ToArray();
            this.treeView1.BeginUpdate();
            this.treeView1.Nodes.Clear();
            this.treeView1.Nodes.AddRange(nodes);
            this.treeView1.EndUpdate();
            this.ScrollTreeToTop();
            this.SetTriggerNodes();
        }

        private void TreeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
                return;
            TreeNode node = e.Node;
            if (node == null)
                return;
            this.treeView1.SelectedNode = node;
            this.filesTreeViewContextMenu.Items[this.openFileToolStripMenuItem.Name].Enabled = node.Level > 0;
            this.filesTreeViewContextMenu.Show(this.treeView1, e.X, e.Y);
        }

        private void TreeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            (sender as TreeView).AfterCheck -= new TreeViewEventHandler(this.TreeView1_AfterCheck);
            string nodePath = FileListControl.GetNodePath(e.Node);
            if (nodePath.ToLowerInvariant().Contains(UiHelper.ZipExtension) && !Path.GetExtension(nodePath).ToLowerInvariant().Equals(UiHelper.ZipExtension) && e.Node.Checked)
            {
                MessageBox.Show("Extracting zip files is not supported yet.\nSelect the actual zip to copy or move it", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                e.Node.Checked = false;
                string key = nodePath.Substring(0, nodePath.IndexOf(UiHelper.ZipExtension, StringComparison.OrdinalIgnoreCase) + UiHelper.ZipExtension.Length);
                // dont set the zip to checked for user.. let him do that
                //if (e.Node.TreeView.Nodes.ContainsKey(key))
                //    e.Node.TreeView.Nodes[key].Checked = true;
                //if (this._treeKeys.ContainsKey(key))
                //    this._treeKeys[key].Checked = true;
            }
            else
            {
                if (Path.GetExtension(nodePath).Equals(UiHelper.ZipExtension))
                {
                    (sender as TreeView).AfterCheck += new TreeViewEventHandler(this.TreeView1_AfterCheck);
                    return;
                }
                if (e.Node.Level > 0)
                {
                    this._treeKeys[e.Node.Parent.Name].Nodes[e.Node.Name].Checked = e.Node.Checked; 
                    // we should uncheck parent here if child is being unchecked
                    // if all childs checked, check parent
                    if (e.Node.Checked)
                    {
                        bool allChecked = true;
                        foreach (TreeNode node in this._treeKeys[e.Node.Parent.Name].Nodes)
                        {
                            if (!node.Checked)
                                allChecked = false;
                        }
                        this._treeKeys[e.Node.Parent.Name].Checked = allChecked;
                        e.Node.Parent.Checked = allChecked;
                    }
                    else
                    {
                        this._treeKeys[e.Node.Parent.Name].Checked = false;
                        e.Node.Parent.Checked = false;
                    }
                }
                else
                {
                    this._treeKeys[e.Node.Name].Checked = e.Node.Checked;
                    foreach (TreeNode node in this._treeKeys[e.Node.Name].Nodes)
                    {
                        node.Checked = e.Node.Checked;
                        if (e.Node.Nodes.ContainsKey(node.Name))
                            e.Node.Nodes[node.Name].Checked = e.Node.Checked;
                    }
                }
            }
            (sender as TreeView).AfterCheck += new TreeViewEventHandler(this.TreeView1_AfterCheck);
        }

        private void FilterButton_Click(object sender, EventArgs e)
        {
            if (this.filterForm.Visible)
                this.filterForm.Hide();
            else
                this.filterForm.Show(sender as Control);
        }

        private void SizeSortButton_Click(object sender, EventArgs e)
        {
            this._sortStack.Push(Filter.Size, new CompareFiledataNodeBySize((sender as SortButton).SortOrder));
            this.SortTree(this.treeView1, this._sortStack);
            //this.treeView1.ExpandAll();
            this.ScrollTreeToTop();
        }

        private void DateCreatedButton_Click(object sender, EventArgs e)
        {
            this._sortStack.Push(Filter.DateCreated, new CompareFiledataNodeByDateCreated((sender as SortButton).SortOrder));
            this.SortTree(this.treeView1, this._sortStack);
            //this.treeView1.ExpandAll();
            this.ScrollTreeToTop();
        }

        private void DateModifiedButton_Click(object sender, EventArgs e)
        {
            this._sortStack.Push(Filter.DateModified, new CompareFiledataNodeByDateModified((sender as SortButton).SortOrder));
            this.SortTree(this.treeView1, this._sortStack);
            //this.treeView1.ExpandAll();
            this.ScrollTreeToTop();
        }


        #region TreeView ContextMenu

        private void CheckUncheckAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this._allCheck = this.GetCheckedNodes(this.treeView1).Count() <= 1;
            foreach (TreeNode node in this.treeView1.Nodes)
                node.Checked = this._allCheck;
        }

        private void ExpandTreeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.treeView1.ExpandAll();
        }

        private void CollapseTreeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.treeView1.CollapseAll();
        }

        private void OpenFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EventHandler<FileDataSelectedEventArgs> openFileDataClicked = this.OnOpenFileDataClicked;
            if (openFileDataClicked == null)
                return;
            openFileDataClicked(this, this.GetEventArgs());
        }

        private void FileLocationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EventHandler<FileDataSelectedEventArgs> openLocationClicked = this.OnOpenLocationClicked;
            if (openLocationClicked == null)
                return;
            openLocationClicked(this, this.GetEventArgs());
        }

        private void DeleteFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EventHandler<FileDataSelectedEventArgs> deleteFileDataClicked = this.OnDeleteFileDataClicked;
            if (deleteFileDataClicked == null)
                return;
            deleteFileDataClicked(this, this.GetEventArgs());
        }
        #endregion

        #region Extensions ListBox ContextMenu
        private void UncheckAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.fileTypesCheckedListBox.ItemCheck -= FileTypesCheckedListBox_ItemCheck;

            for (int itemIndex = 0; itemIndex < this.fileTypesCheckedListBox.Items.Count; itemIndex++)
            {
                if (itemIndex == this.fileTypesCheckedListBox.Items.Count-1)
                    this.fileTypesCheckedListBox.ItemCheck += FileTypesCheckedListBox_ItemCheck;

                this.fileTypesCheckedListBox.SetItemChecked(itemIndex, false);
            }
        }

        private void CheckAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.fileTypesCheckedListBox.ItemCheck -= FileTypesCheckedListBox_ItemCheck;

            for (int itemIndex = 0; itemIndex < this.fileTypesCheckedListBox.Items.Count; itemIndex++)
            {
                if (itemIndex == this.fileTypesCheckedListBox.Items.Count - 1)
                    this.fileTypesCheckedListBox.ItemCheck += FileTypesCheckedListBox_ItemCheck;

                this.fileTypesCheckedListBox.SetItemChecked(itemIndex, true);
            }
        }

        private void UncheckOthersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.fileTypesCheckedListBox.ItemCheck -= FileTypesCheckedListBox_ItemCheck;

            for (int itemIndex = 0; itemIndex < this.fileTypesCheckedListBox.Items.Count; itemIndex++)
            {
                if (itemIndex == this.fileTypesCheckedListBox.Items.Count - 1)
                    this.fileTypesCheckedListBox.ItemCheck += FileTypesCheckedListBox_ItemCheck;

                if (itemIndex != this.fileTypesCheckedListBox.SelectedIndex)
                    this.fileTypesCheckedListBox.SetItemChecked(itemIndex, false);
            }
        }

        private void CheckOthersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.fileTypesCheckedListBox.ItemCheck -= FileTypesCheckedListBox_ItemCheck;

            for (int itemIndex = 0; itemIndex < this.fileTypesCheckedListBox.Items.Count; itemIndex++)
            {
                if (itemIndex == this.fileTypesCheckedListBox.Items.Count - 1)
                    this.fileTypesCheckedListBox.ItemCheck += FileTypesCheckedListBox_ItemCheck;

                if (itemIndex != this.fileTypesCheckedListBox.SelectedIndex)
                    this.fileTypesCheckedListBox.SetItemChecked(itemIndex, true);
            }
        }
        #endregion

        #region TreeNode ToolTips
        /// <summary>
        /// Could not get this to work correctly. see ScrollNotifyTreeView comment in WndProc(ref Message m)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView1_NodeNeedToolTip(object sender, NeedToolTipEventArgs e)
        {
            if (e.Node.Tag is FileData)
            {
                FileData fileData = (FileData)e.Node.Tag;
                e.ToolTipText = string.Join(Environment.NewLine, fileData.ExtendedProperties.Select(p => string.Format("{0}: {1}", p.Key, p.Value)).ToArray());
            }
        }

        private void treeView1_NodeMouseHover(object sender, TreeNodeMouseHoverEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(e.Node.ToolTipText) && e.Node.Tag is FileData)
            {
                FileData fileData = (FileData)e.Node.Tag;
                System.Drawing.Point treeScreenPoint = RectangleToScreen(e.Node.TreeView.DisplayRectangle).Location;

                this._treeViewToolTip.IsBalloon = true;
                // expand fileData.ExtendedProperties to list, to prevent possible enumeration changed error
                this._treeViewToolTip.Show(string.Join(Environment.NewLine, fileData.ExtendedProperties.ToList().Select(p => string.Format("{0}: {1}", p.Key, p.Value)).ToArray())
                    , this
                    , new System.Drawing.Point(MousePosition.X - treeScreenPoint.X + 5, MousePosition.Y - treeScreenPoint.Y + 5));
            }
            else
            {
                this._treeViewToolTip.Hide(this);
            }
        }

        private void treeView1_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.treeView1.GetNodeAt(e.Location) == null || !(this.treeView1.GetNodeAt(e.Location).Tag is FileData))
                this._treeViewToolTip.Hide(this);
        }

        private void treeView1_MouseLeave(object sender, EventArgs e)
        {
            this._treeViewToolTip.Hide(this);
        }
        #endregion

        #endregion

        #region Private Helpers

        /// <summary>
        /// When treeview is programatically scrolled, it tends to also horizontally scroll right.
        /// we will force to scroll back left
        /// </summary>
        /// <param name="tree"></param>
        private static void ScrollTreeLeft(TreeView tree)
        {
            user32.SendMessage(tree.Handle, (uint)(int)MessageCodes.WM_HSCROLL, HorizontalScrollBarCommands.SB_LEFT, 0);
        }

        /// <summary>
        /// Gets predicate used for determining if a FileData should be filtered out,
        /// taking into consideration file extension filter, file size, file created date and file modified data
        /// </summary>
        /// <param name="checkedExt"></param>
        /// <returns></returns>
        private Func<FileData, bool> GetFilterPredicate(ItemCheckEventArgs checkedExt = null)
        {
            return (file =>
            {
                // check for file extensions filtered out
                List<string> list = this.fileTypesCheckedListBox.CheckedItems.Cast<FileExtensionCount>().Select(f => f.Extension).ToList();
                if (checkedExt != null)
                {
                    string str = (this.fileTypesCheckedListBox.Items[checkedExt.Index] as FileExtensionCount).Extension;
                    if (checkedExt.NewValue == CheckState.Checked && !list.Contains(str))
                        list.Add(str);
                    else if (checkedExt.NewValue == CheckState.Unchecked && list.Contains(str))
                        list.Remove(str);
                }
                if (!list.Contains(file.Extension))
                    return false;

                // check that file is within size filter, if size filter is used
                // validate the data then send to CheckIsFileSizeWithinFilter()
                if (Enum.IsDefined(typeof(FilterType), this.filterForm.SizeFilter.FilterType) && this.filterForm.SizeFilter.FilterType != FilterType.None
                    && Enum.IsDefined(typeof(StorageSizeType), this.filterForm.SizeFilter.StorageSize1) && this.filterForm.SizeFilter.StorageSize1 != StorageSizeType.None)
                {
                    SizeFilter sizeFilter = this.filterForm.SizeFilter;
                    // we'll send these values to CheckIsFileSizeWithinFilter()
                    KeyValuePair<float, StorageSizeType> fileSize = new KeyValuePair<float, StorageSizeType>(file.SizeInKilobytes.HasValue ? file.SizeInKilobytes.Value : 0, StorageSizeType.Kb);
                    FilterType filterType = sizeFilter.FilterType;
                    KeyValuePair<float, StorageSizeType> filter1 = new KeyValuePair<float, StorageSizeType>(sizeFilter.Value1, sizeFilter.StorageSize1);
                    KeyValuePair<float, StorageSizeType>? filter2 = null;

                    // we need a second value and second storage type in order to do a between comparison
                    if (sizeFilter.FilterType == FilterType.Between
                        && (!sizeFilter.Value2.HasValue || !sizeFilter.StorageSize2.HasValue || sizeFilter.StorageSize2.Value == StorageSizeType.None))
                    {
                        filterType = FilterType.None;
                    }
                    else if (sizeFilter.FilterType == FilterType.Between
                            && sizeFilter.Value2.HasValue && sizeFilter.StorageSize2.HasValue && sizeFilter.StorageSize2.Value != StorageSizeType.None)
                    {
                        filter2 = new KeyValuePair<float, StorageSizeType>(sizeFilter.Value2.Value, sizeFilter.StorageSize2.Value);
                    }

                    if (filterType != FilterType.None)
                    {
                        if (!FileListControl.CheckIsFileSizeWithinFilter(filterType, filter1, filter2, fileSize))
                            return false;
                    }
                }

                // check that file is within date created filter, if date created filter is used
                // validate the data then send to CheckFileDateWithinFilter()
                if (Enum.IsDefined(typeof(FilterType), this.filterForm.CreatedDateFilter.FilterType) && this.filterForm.CreatedDateFilter.FilterType != FilterType.None
                    && this.filterForm.CreatedDateFilter.DateTime1.HasValue && file.DateModified.HasValue)
                {
                    DateFilter createDateFilter = this.filterForm.CreatedDateFilter;
                    FilterType filterType = createDateFilter.FilterType;
                    if (createDateFilter.FilterType == FilterType.Between && !createDateFilter.DateTime2.HasValue)
                        filterType = FilterType.None;

                    if (filterType != FilterType.None)
                    {
                        if (!FileListControl.CheckFileDateWithinFilter(createDateFilter.FilterType, createDateFilter.DateTime1.Value
                            , createDateFilter.DateTime2, file.DateModified.Value))
                            return false;
                    }
                }

                // check that file is within date modified filter, if date modified filter is used
                // validate the data then send to CheckFileDateWithinFilter()
                if (Enum.IsDefined(typeof(FilterType), this.filterForm.ModifiedDateFilter.FilterType) && this.filterForm.ModifiedDateFilter.FilterType != FilterType.None
                    && this.filterForm.ModifiedDateFilter.DateTime1.HasValue && file.DateModified.HasValue)
                {
                    DateFilter modifiedDateFilter = this.filterForm.ModifiedDateFilter;
                    FilterType filterType = modifiedDateFilter.FilterType;
                    if (modifiedDateFilter.FilterType == FilterType.Between && !modifiedDateFilter.DateTime2.HasValue)
                        filterType = FilterType.None;

                    if (filterType != FilterType.None)
                    {
                        if (!FileListControl.CheckFileDateWithinFilter(modifiedDateFilter.FilterType, modifiedDateFilter.DateTime1.Value
                            , modifiedDateFilter.DateTime2, file.DateModified.Value))
                            return false;
                    }
                }

                return true;
            });
        }

        private void SetVisibleNodes()
        {
            this.Enabled = false;
            Func<FileData, bool> filterPredicate = this.GetFilterPredicate(null);
            //this.SortTree(this.treeView1, this._sortStack);
            //this.SetNodeVisibility(this.treeView1, this.SortedNodes);
            this.treeView1.Nodes.Clear();
            this.treeView1.Nodes.AddRange(this.SortedNodes.Where(n => n.Nodes.Cast<TreeNode>().Any(c => filterPredicate((FileData)c.Tag))).Select(n => {
                TreeNode clone = (TreeNode)n.Clone();
                clone.Nodes.Add((TreeNode)n.Nodes[0].Clone());
                return clone;
                }).Take(this.treeView1.VisibleCount * 2).ToArray());
            this.ScrollTreeToTop();
            this.SetTriggerNodes();
            this.Enabled = true;
        }

        private IEnumerable<TreeNode> GetCheckedNodes(TreeView treeView)
        {
            List<TreeNode> treeNodeList = new List<TreeNode>();

            foreach (TreeNode node in this.SortedNodes)
                treeNodeList.AddRange(this.GetCheckedNodes(node));
            return treeNodeList;
        }

        private IEnumerable<TreeNode> GetCheckedNodes(TreeNode node)
        {
            if (node.Nodes.Count > 0)
            {
                foreach (TreeNode node1 in node.Nodes)
                {
                    TreeNode child = node1;
                    IEnumerator<TreeNode> enumerator = this.GetCheckedNodes(child).GetEnumerator();
                    while (enumerator.MoveNext())
                        yield return enumerator.Current;
                    enumerator = null;
                    child = null;
                }
            }
            if (node.Checked)
                yield return node;
        }

        private FileDataSelectedEventArgs GetEventArgs()
        {
            TreeNode selectedNode = this.treeView1.SelectedNode;
            List<FileData> fileDataList = new List<FileData>();
            string selectedPath;
            if (selectedNode.Level == 0)
            {
                foreach (TreeNode node in this._treeKeys[selectedNode.Name].Nodes)
                    fileDataList.Add((FileData)node.Tag);
                selectedPath = selectedNode.Text;
            }
            else
            {
                fileDataList.Add((FileData)selectedNode.Tag);
                selectedPath = ((FileData)selectedNode.Tag).Path;
            }
            return new FileDataSelectedEventArgs(fileDataList.ToArray(), selectedPath, selectedNode.Level == 0);
        }

        private void SortTree(TreeView treeView, FileDataSortStack sortStack)
        {
            //List<IComparer<TreeNode>> comparerList = new List<IComparer<TreeNode>>();
            //while (sortStack.MoveNext())
            //    comparerList.Add(sortStack.CurrentComparer);
            MultiCompareFileData comparer = new MultiCompareFileData(sortStack);

            //this._treeKeys.Values.AsParallel().ForAll(n => n.SortChildNodes(comparer));
            // this hacky sort seems to have the best performance.. sigh.........
            IEnumerable<TreeNode> temp = this.SortedNodes.ToList();
            this.SortedNodes = new SortedSet<TreeNode>( comparer);
            foreach (TreeNode node in temp)
                this.SortedNodes.Add(node);
            this.treeView1.Nodes.Clear();
            this.treeView1.TreeViewNodeSorter = comparer; // we shouldnt need sort anymore. we can depend on our SortedSet
            this.treeView1.Nodes.AddRange(this.SortedNodes.Take(this.treeView1.VisibleCount*2).Select(n => 
                {
                    TreeNode clone = (TreeNode)n.Clone();
                    clone.Nodes.Add((TreeNode)n.Nodes[0].Clone());
                    return clone;
                    }).ToArray());

            this.SetTriggerNodes();
        }

        #region Filters
        private static bool CheckIsFileSizeWithinFilter(
          FilterType filterType,
          KeyValuePair<float, StorageSizeType> filter1,
          KeyValuePair<float, StorageSizeType>? filter2,
          KeyValuePair<float, StorageSizeType> fileSize)
        {
            float kb1 = Common.Helpers.FileStorageSize.ConvertStorageValueToKb(filter1.Key, filter1.Value);
            float kb2 = filter2.HasValue ? Common.Helpers.FileStorageSize.ConvertStorageValueToKb(filter2.Value.Key, filter2.Value.Value) : 0.0f;
            float fileKb = Common.Helpers.FileStorageSize.ConvertStorageValueToKb(fileSize.Key, fileSize.Value);
            switch (filterType)
            {
                case FilterType.Between:
                    if (fileKb < kb1 || fileKb > kb2)
                        return false;
                    break;
                case FilterType.LessThan:
                    if (fileKb >= kb1)
                        return false;
                    break;
                case FilterType.GreaterThan:
                    if (fileKb <= kb1)
                        return false;
                    break;
                case FilterType.Equals:
                    if (fileKb != kb1)
                        return false;
                    break;
                default:
                    return false;
            }
            return true;
        }

        private static bool CheckFileDateWithinFilter(
          FilterType filterType,
          DateTime filter1,
          DateTime? filter2,
          DateTime fileDate)
        {
            DateTime dateTime = filter2.HasValue ? filter2.Value : DateTime.MinValue;
            switch (filterType)
            {
                case FilterType.Between:
                    if (fileDate < filter1 || fileDate > dateTime)
                        return false;
                    break;
                case FilterType.LessThan:
                    if (fileDate >= filter1)
                        return false;
                    break;
                case FilterType.GreaterThan:
                    if (fileDate <= filter1)
                        return false;
                    break;
                case FilterType.Equals:
                    if (fileDate != filter1)
                        return false;
                    break;
                default:
                    return false;
            }
            return true;
        }
        #endregion

        private static string GetNodePath(TreeNode node)
        {
            FileData? tag = node.Tag as FileData?;
            return !tag.HasValue ? node.Text : tag.Value.Path;
        }

        private bool IsNodeWithinBounds(TreeNode node, TreeNode parentNode = null)
        {
            if ((parentNode == null && this.treeView1.Nodes.Count < 1) || (parentNode != null && parentNode.Nodes.Count < 1))
                return true;
            TreeNode topNode = parentNode == null ? this.treeView1.Nodes[0] : parentNode.Nodes[0];
            TreeNode bottomNode = parentNode == null ? this.treeView1.Nodes[this.treeView1.Nodes.Count - 1] : parentNode.Nodes[parentNode.Nodes.Count - 1];
            int topNodeIndex = -1;
            int bottomNodeIndex = int.MaxValue;
            int nodeIndex = -1;
            int currentIndex = -1;

            foreach (TreeNode sortedNode in this.SortedNodes)
            {
                currentIndex++;

                if (topNodeIndex == -1 && sortedNode.Name.Equals(node.Name))
                    return false;

                if (sortedNode.Name.Equals(topNode.Name))
                {
                    topNodeIndex = currentIndex;
                    continue;
                }
                if (sortedNode.Name.Equals(bottomNode.Name))
                {
                    bottomNodeIndex = currentIndex;
                    break;
                }
                if (sortedNode.Name.Equals(node.Name))
                {
                    nodeIndex = currentIndex;
                    continue;
                }
            }

            if (nodeIndex < topNodeIndex || nodeIndex > bottomNodeIndex)
                return false;
            return true;
        }

        /// <summary>
        /// checks whether a node is within virtual scroll bounds and filters.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="isParent"></param>
        /// <returns></returns>
        private bool ShouldNodeVisible(TreeNode node, IsParentNode isParent, ItemCheckEventArgs itemCheckEventArgs = null)
        {
            Func<FileData, bool> filterPredicate = this.GetFilterPredicate(itemCheckEventArgs);
            string parentNodeName = System.IO.Path.GetDirectoryName(node.Name);
            int maxNodes = this.treeView1.VisibleCount * 2;

            switch (isParent)
            {
                case IsParentNode.Yes:

                    if (this.treeView1.Nodes.Count >= maxNodes)
                        return false;

                    // *** virtual scroll check *** \\
                    if (!this.IsNodeWithinBounds(node))
                        return false;
                    break;
                case IsParentNode.No:
                    if (!node.Parent.IsExpanded)
                        return false;
                    TreeNode parentNode = this._treeKeys[parentNodeName];

                    if (parentNode.Nodes.Count >= maxNodes)
                        return false;

                    // *** virtual scroll check *** \\
                    if (!this.IsNodeWithinBounds(node, parentNode))
                        return false;
                    // *** filters check *** \\
                    return filterPredicate((FileData)node.Tag);
                case IsParentNode.DontKnow:
                default:
                    isParent = this._treeKeys.ContainsKey(parentNodeName) ? IsParentNode.Yes : IsParentNode.No;
                    return this.ShouldNodeVisible(node, isParent);
            }

            return true;
        }

        /// <summary>
        /// Adds clone of node if it should be visible.
        /// return whether node was added
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private bool AddPrentNode(TreeNode node)
        {
            if (this.ShouldNodeVisible(node, IsParentNode.Yes))
            {
                this.treeView1.Nodes.Add((TreeNode)node.Clone());
                return true;
            }
            return false;
        }

        /// <summary>
        /// Determines if child should be visible and if so, adds clone to the parent.
        /// if parent is not in treeview, clone of parent is added to treeview
        /// returns whether child node was added
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="child"></param>
        /// <returns></returns>
        private bool AddChildNode(TreeNode parent, TreeNode child)
        {
            if (this.ShouldNodeVisible(parent, IsParentNode.Yes) && this.ShouldNodeVisible(child, IsParentNode.No))
            {
                if (!this.treeView1.Nodes.ContainsKey(parent.Name))
                    this.treeView1.Nodes.Add((TreeNode)parent.Clone());

                this.treeView1.Nodes[parent.Name].Nodes.Add((TreeNode)child.Clone());
                return true;
            }

            return false;
        }

        private void SetNodeVisibility(TreeView tree, IEnumerable<TreeNode> treeDataSource)
        {
            foreach (TreeNode parentNode in treeDataSource)
            {
                if (!this.ShouldNodeVisible(parentNode, IsParentNode.Yes))
                    continue;
                if (!tree.Nodes.ContainsKey(parentNode.Name))
                {
                    tree.Nodes.Add((TreeNode)parentNode.Clone());
                    tree.Nodes[parentNode.Name].Nodes.Clear();
                }
                foreach (TreeNode node in parentNode.Nodes)
                {
                    if (!tree.Nodes[parentNode.Name].IsExpanded)
                    {
                        // make sure we keep 1 child node to enable parent node expanding in UI
                        if (tree.Nodes[parentNode.Name].Nodes.Count < 1)
                            tree.Nodes[parentNode.Name].Nodes.Add((TreeNode)node.Clone());
                        
                        break;
                    }
                    bool flag = this.ShouldNodeVisible(node, IsParentNode.No);

                    if (flag && !tree.Nodes[parentNode.Name].Nodes.ContainsKey(node.Name))
                        tree.Nodes[parentNode.Name].Nodes.Add((TreeNode)node.Clone());
                    else if (!flag && tree.Nodes[parentNode.Name].Nodes.ContainsKey(node.Name))
                        tree.Nodes[parentNode.Name].Nodes.RemoveByKey(node.Name);
                }
                if (tree.Nodes.ContainsKey(parentNode.Name) && tree.Nodes[parentNode.Name].Nodes.Count < 1)
                    tree.Nodes.RemoveByKey(parentNode.Name);
            }
            tree.Refresh();
        }

        private static void SetNodeVisibility(
          TreeView tree,
          string fileType,
          bool visible,
          List<TreeNode> treeDataSource)
        {
            foreach (TreeNode treeNode in treeDataSource)
            {
                if (visible && !tree.Nodes.ContainsKey(treeNode.Name))
                {
                    tree.Nodes.Add(treeNode.Clone() as TreeNode);
                    tree.Nodes[treeNode.Name].Nodes.Clear();
                }
                foreach (TreeNode node in treeNode.Nodes)
                {
                    if (node.ImageKey.Equals(fileType))
                    {
                        if (visible && !tree.Nodes[treeNode.Name].Nodes.ContainsKey(node.Name))
                            tree.Nodes[treeNode.Name].Nodes.Add(node.Clone() as TreeNode);
                        else if (!visible && tree.Nodes[treeNode.Name].Nodes.ContainsKey(node.Name))
                            tree.Nodes[treeNode.Name].Nodes.RemoveByKey(node.Name);
                    }
                }
                if (tree.Nodes.ContainsKey(treeNode.Name) && tree.Nodes[treeNode.Name].Nodes.Count < 1)
                    tree.Nodes.RemoveByKey(treeNode.Name);
            }
        }
        #endregion

        #region Virtual Scroll
        private void treeView1_Scrolled(object sender, ScrollNotifyTreeViewEventArgs e)
        {
            TreeNode parentNode = this.treeView1.TopNode.Parent;

            // we have an expanded parent node. need to check child triggers
            if (parentNode != null && this.ChildNodeTriggers.ContainsKey(parentNode.Name))
            {
                ChildNodeTriggers triggers = this.ChildNodeTriggers[parentNode.Name];

                if (e.Direction == Direction.Up && triggers.Top.IsInVisibleScope())
                {
                    this.FillChildTopReserve(parentNode);
                    FileListControl.ScrollTreeLeft(this.treeView1);
                }
                else if (e.Direction == Direction.Down && triggers.Bottom.IsInVisibleScope())
                {
                    this.FillChildBottomReserve(parentNode);
                    FileListControl.ScrollTreeLeft(this.treeView1);
                }
            }
            else
            {
                if (this.topTrigger != null && e.Direction == Direction.Up && this.topTrigger.IsInVisibleScope())
                {
                    this.FillTopReserve();
                    FileListControl.ScrollTreeLeft(this.treeView1);
                }
                else if (this.bottomTrigger != null && e.Direction == Direction.Down && this.bottomTrigger.IsInVisibleScope())
                {
                    this.FillBottomReserve();
                    FileListControl.ScrollTreeLeft(this.treeView1);
                }
            }
        }

        private void FillTopReserve()
        {
            TreeNode topNode = this.treeView1.Nodes[0];
            int bufferCount = this.treeView1.VisibleCount / 2;
            Func<FileData, bool> filterPredicate = this.GetFilterPredicate(null);
            //TreeNode[] nodes = this.SortedNodes.TakeWhile(n => n.Nodes.Cast<TreeNode>().Any(c => filterPredicate((FileData)c.Tag)) && !n.Name.Equals(topNode.Name)).TakeLast(bufferCount).ToArray();
            TreeNode[] nodes = this.SortedNodes.TakeWhile(n => !n.Name.Equals(topNode.Name)).Where(n => n.Nodes.Cast<TreeNode>().Any(c => filterPredicate((FileData)c.Tag))).TakeLast(bufferCount).ToArray();
            TreeNode bottomNode = this.treeView1.GetBottomVisibleNode(); 
            bool scrollBarWasVisible = this.treeView1.HorizontalScrollVisible();

            if (nodes == null || nodes.Length == 0)
                return;

            this.treeView1.BeginUpdate();

            for (int node = nodes.Length - 1; node > -1; node--)
            {
                this.treeView1.Nodes.Insert(0, (TreeNode)nodes[node].Clone());
                this.treeView1.Nodes[0].Checked = nodes[node].Checked;
                if (this.treeView1.Nodes.Count > (this.treeView1.VisibleCount * 2))
                {
                    if (this.lastSelectedNode!= null && this.treeView1.Nodes[this.treeView1.Nodes.Count - 1].Name == this.lastSelectedNode.Name)
                        this.treeView1.SelectedNode = null;
                    this.treeView1.Nodes.RemoveAt(this.treeView1.Nodes.Count - 1);
                }
            }

            this.SetTriggerNodes();
            this.treeView1.EndUpdate();
            // set our scroll point to 1 item below where we were.  the virtual scroll will change our current position
            // so we need to change it back.
            bottomNode = this.treeView1.Nodes[Math.Min(this.treeView1.Nodes.Count - 1, topNode.Index + this.treeView1.VisibleCount)];
            if (!scrollBarWasVisible && this.treeView1.HorizontalScrollVisible())
                this.treeView1.Nodes[this.treeView1.Nodes.Find(bottomNode.Name, false)[0].Index - 2].EnsureVisible();
            else
                this.treeView1.Nodes.Find(bottomNode.Name, false)[0].EnsureVisible();

            if (this.lastSelectedNode != null && this.treeView1.Nodes.ContainsKey(this.lastSelectedNode.Name))
                this.treeView1.SelectedNode = this.treeView1.Nodes[this.lastSelectedNode.Name];
        }

        private void FillBottomReserve()
        {
            TreeNode topNode = this.treeView1.TopNode;
            TreeNode bottomNode = this.treeView1.Nodes[this.treeView1.Nodes.Count - 1];
            int bufferCount = this.treeView1.VisibleCount / 2;
            Func<FileData, bool> filterPredicate = this.GetFilterPredicate(null);
            //TreeNode[] nodes = this.SortedNodes.SkipWhile(n => !n.Nodes.Cast<TreeNode>().Any(c => filterPredicate((FileData)c.Tag)) || !n.Name.Equals(bottomNode.Name)).Skip(1).Take(bufferCount).ToArray();
            TreeNode[] nodes = this.SortedNodes.SkipWhile(n => !n.Name.Equals(bottomNode.Name)).Skip(1).Where(n => n.Nodes.Cast<TreeNode>().Any(c => filterPredicate((FileData)c.Tag))).Take(bufferCount).ToArray();
            bool scrollBarWasVisible = this.treeView1.HorizontalScrollVisible();

            if (nodes == null || nodes.Length == 0)
                return;
            this.treeView1.BeginUpdate();

            for (int node = nodes.Length - 1; node > -1; node--)
            {
                this.treeView1.Nodes.Insert(this.treeView1.Nodes.Count - 1, (TreeNode)nodes[node].Clone());
                this.treeView1.Nodes[this.treeView1.Nodes.Count - 1].Checked = nodes[node].Checked;
                if (this.treeView1.Nodes.Count > (this.treeView1.VisibleCount * 2))
                {
                    if (this.lastSelectedNode != null && this.treeView1.Nodes[0].Name == this.lastSelectedNode.Name)
                        this.treeView1.SelectedNode = null;
                    this.treeView1.Nodes.RemoveAt(0);
                }
            }

            this.SetTriggerNodes();
            this.treeView1.EndUpdate();
            // set our scroll point to 1 item below where we were.  the virtual scroll will change our current position
            // so we need to change it back.
            topNode = this.treeView1.Nodes[Math.Max(0, bottomNode.Index - this.treeView1.VisibleCount)];
            if (!scrollBarWasVisible && this.treeView1.HorizontalScrollVisible()) // our field of view shifted
                this.treeView1.Nodes[this.treeView1.Nodes.Find(topNode.Name, false)[0].Index + 1].EnsureVisible();
            else
                this.treeView1.Nodes.Find(topNode.Name, false)[0].EnsureVisible();

            if (this.lastSelectedNode != null && this.treeView1.Nodes.ContainsKey(this.lastSelectedNode.Name))
                this.treeView1.SelectedNode = this.treeView1.Nodes[this.lastSelectedNode.Name];
        }

        private void SetTriggerNodes()
        {
            if (this.treeView1.Nodes.Count < (this.treeView1.VisibleCount))
                return;
            int baseReserveCount = this.treeView1.Nodes.Count - this.treeView1.VisibleCount;
            int topTriggerIndex = (int)Math.Ceiling(baseReserveCount / 2d);
            int bottomTriggerIndex = (int)Math.Floor(baseReserveCount / 2d);

            this.topTrigger = this.treeView1.Nodes[1];
            this.bottomTrigger = this.treeView1.Nodes[this.treeView1.Nodes.Count - 2];
        }

        private void FillChildTopReserve(TreeNode parent)
        {
            TreeNode topNode = parent.Nodes[0];
            int bufferCount = this.treeView1.VisibleCount / 2;
            Func<FileData, bool> filterPredicate = this.GetFilterPredicate(null);
            int nodeIndex = this._treeKeys.Values.FirstOrDefault(n => n.Name.Equals(parent.Name)).Nodes.Cast<TreeNode>().ToList().IndexOf(topNode);
            TreeNode[] nodes = this._treeKeys[parent.Name].SortChildNodes(new MultiCompareFileData(this._sortStack))
                .TakeWhile(n => !n.Name.Equals(topNode.Name)).Where(n => filterPredicate((FileData)n.Tag)).TakeLast(bufferCount).Select(n => (TreeNode)n.Clone()).ToArray();
            bool scrollBarWasVisible = this.treeView1.HorizontalScrollVisible();
            TreeNode bottomNode = parent.Nodes[parent.Nodes.Count - 1];  

            if (nodes == null || nodes.Length == 0)
                return;
            this.treeView1.BeginUpdate();

            for (int node = nodes.Length - 1; node > -1; node--)
            {
                parent.Nodes.Insert(0, nodes[node]);
                parent.Nodes[0].Checked = nodes[node].Checked;
                if (parent.Nodes.Count > (this.treeView1.VisibleCount * 2))
                {
                    if (this.lastSelectedNode != null && parent.Nodes[parent.Nodes.Count - 1].Name == this.lastSelectedNode.Name)
                        this.treeView1.SelectedNode = null;
                    parent.Nodes.RemoveAt(parent.Nodes.Count - 1);
                }
            }

            this.SetChildTriggerNodes(parent);
            this.treeView1.EndUpdate(); 
            bottomNode = parent.Nodes[Math.Min(parent.Nodes.Count - 1, topNode.Index + this.treeView1.VisibleCount)];
            // set our scroll point to 1 item below where we were.  the virtual scroll will change our current position
            // so we need to change it back.
            if (!scrollBarWasVisible && this.treeView1.HorizontalScrollVisible())
                parent.Nodes[parent.Nodes.Find(bottomNode.Name, false)[0].Index - 2].EnsureVisible();
            else
                parent.Nodes.Find(bottomNode.Name, false)[0].EnsureVisible();

            if (this.lastSelectedNode != null && this.treeView1.Nodes.ContainsKey(this.lastSelectedNode.Name))
                this.treeView1.SelectedNode = this.treeView1.Nodes[this.lastSelectedNode.Name];
        }

        private void FillChildBottomReserve(TreeNode parent)
        {
            TreeNode topNode = parent.Nodes[0];
            TreeNode bottomNode = parent.Nodes[parent.Nodes.Count - 1];
            int bufferCount = this.treeView1.VisibleCount / 2;
            Func<FileData, bool> filterPredicate = this.GetFilterPredicate(null);
            TreeNode[] nodes = this._treeKeys[parent.Name].SortChildNodes(new MultiCompareFileData(this._sortStack))
                .SkipWhile(n => !n.Name.Equals(bottomNode.Name)).Skip(1).Where(n => filterPredicate((FileData)n.Tag)).Take(bufferCount).Select(n => (TreeNode)n.Clone()).ToArray(); 
            bool scrollBarWasVisible = this.treeView1.HorizontalScrollVisible();

            if (nodes == null || nodes.Length == 0)
                return;

            this.treeView1.BeginUpdate();

            for (int node = nodes.Length - 1; node > -1; node--)
            {
                parent.Nodes.Insert(parent.Nodes.Count - 1, nodes[node]);
                parent.Nodes[parent.Nodes.Count - 1].Checked = nodes[node].Checked;
                if (parent.Nodes.Count > (this.treeView1.VisibleCount * 2))
                {
                    if (this.lastSelectedNode != null && parent.Nodes[0].Name == this.lastSelectedNode.Name)
                        this.treeView1.SelectedNode = null;
                    parent.Nodes.RemoveAt(0);
                }
            }

            this.SetChildTriggerNodes(parent);
            this.treeView1.EndUpdate();
            topNode = parent.Nodes[Math.Max(0, bottomNode.Index - this.treeView1.VisibleCount)];
            // set our scroll point to 1 item below where we were.  the virtual scroll will change our current position
            // so we need to change it back.
            if (!scrollBarWasVisible && this.treeView1.HorizontalScrollVisible()) // our field of view shifted
                parent.Nodes[parent.Nodes.Find(topNode.Name, false)[0].Index + 1].EnsureVisible();
            else
                parent.Nodes.Find(topNode.Name, false)[0].EnsureVisible();

            if (this.lastSelectedNode != null && this.treeView1.Nodes.ContainsKey(this.lastSelectedNode.Name))
                this.treeView1.SelectedNode = this.treeView1.Nodes[this.lastSelectedNode.Name];
        }

        private void SetChildTriggerNodes(TreeNode parent)
        {
            int baseReserveCount = parent.Nodes.Count - this.treeView1.VisibleCount;
            if (baseReserveCount < 0 || parent.Nodes.Count < this.treeView1.VisibleCount)
                return;
            int topTriggerIndex = (int)Math.Ceiling(baseReserveCount / 2d);
            int bottomTriggerIndex = (int)Math.Floor(baseReserveCount / 2d);

            TreeNode top = parent.Nodes[1];
            TreeNode bottom = parent.Nodes[parent.Nodes.Count - 1];

            if (this._treeKeys[parent.Name].Name.Equals(bottom.Name))
                bottom = null;

            if (this.ChildNodeTriggers.ContainsKey(parent.Name))
                this.ChildNodeTriggers[parent.Name] = new ChildNodeTriggers(top, bottom);
            else
                this.ChildNodeTriggers.Add(parent.Name, new ChildNodeTriggers(top, bottom));
        }

        private void treeView1_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            TreeNode parentNode = e.Node;
            TreeNode bastardNode = parentNode.Nodes[0];
            parentNode.Nodes.Clear();
            parentNode.Nodes.Add(bastardNode);
        }

        private void treeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            TreeNode parentNode = e.Node;
            Func<FileData, bool> filterPredicate = this.GetFilterPredicate(null);
            parentNode.Nodes.Clear();
            this._treeKeys[parentNode.Name].SortChildNodes(new MultiCompareFileData(this._sortStack));
            parentNode.Nodes.AddRange(this._treeKeys[parentNode.Name].Nodes.Cast<TreeNode>().Where(n => filterPredicate((FileData)n.Tag)).Take(this.treeView1.VisibleCount * 2).Select(n => (TreeNode)n.Clone()).ToArray());

            this.SetChildTriggerNodes(parentNode);
        }
        #endregion // end virtual scroll

        
    }

    public class ChildNodeTriggers
    {
        public ChildNodeTriggers() { }

        public ChildNodeTriggers(TreeNode top, TreeNode bottom)
        {
            this.Top = top;
            this.Bottom = bottom;
        }

        public TreeNode Top { get; set; }
        public TreeNode Bottom { get; set; }
    }

    public class FileDataSelectedEventArgs : EventArgs
    {
        public FileDataSelectedEventArgs(FileData[] fileData, string selectedPath, bool isRootPath)
        {
            this.FileData = fileData;
            this.SelectedPath = selectedPath;
            this.IsRootPath = isRootPath;
        }

        public bool IsRootPath { get; private set; }

        public string SelectedPath { get; private set; }

        public FileData[] FileData { get; private set; }
    }

    public class FileExtensionCount
    {
        private volatile int _count;
        public FileExtensionCount(string extension, int initialCount = 0)
        {
            this.Extension = extension;
            this._count = initialCount;
        }

        public string Extension { get; private set; }

        public int Count
        {
            get
            {
                return System.Threading.Interlocked.CompareExchange(ref this._count, 0, 0);
            }
            set
            {
                int count = this._count;
                System.Threading.Interlocked.CompareExchange(ref this._count, count + value, count);
            }
        }

        public void IncrementCount()
        {
            System.Threading.Interlocked.Increment(ref this._count);
        }

        public override string ToString()
        {
            return $"{this.Extension} ({this.Count})";
        }
    }
}
