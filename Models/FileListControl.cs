using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using FileList.Views;
using System.IO;
using FileList.Logic;

namespace FileList.Models
{
    public partial class FileListControl : UserControl
    {
        public event EventHandler<FileDataSelectedEventArgs> OnFileDataSelected;
        public event EventHandler<FileDataSelectedEventArgs> OnOpenFileDataClicked;
        public event EventHandler<FileDataSelectedEventArgs> OnOpenLocationClicked;
        public event EventHandler<FileDataSelectedEventArgs> OnDeleteFileDataClicked;

        private readonly List<TreeNode> TreeDataSource = new List<TreeNode>();
        private bool modifyFileTypesListBoxInternal = false;
        private Dictionary<string, TreeNode> _treeKeys = new Dictionary<string, TreeNode>();
        private List<string> _extensions = new List<string>();
        private FileFilterForm filterForm;
        private FileDataSortStack _sortStack;
        private bool _allCheck;

        private const int WM_HSCROLL = 276;
        private const int SB_LEFT = 6;

        public FileListControl()
        {
            InitializeComponent();
            this.filterForm = new FileFilterForm();
            this.filterForm.VisibleChanged += new EventHandler(this.FilterForm_VisibleChanged);
            this._sortStack = new FileDataSortStack();
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

        public bool FileTypeListSorted
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

        public bool RemoveByPath(string path)
        {
            TreeNode treeNode = this.treeView1.Nodes.ContainsKey(path) ? this.treeView1.Nodes[path] : (TreeNode)null;
            if (treeNode == null)
            {
                foreach (TreeNode node in this.treeView1.Nodes)
                {
                    if (node.Nodes.ContainsKey(path))
                    {
                        treeNode = node.Nodes[path];
                        break;
                    }
                }
            }
            if (!this.TreeDataSource.Remove(treeNode))
                return false;
            treeNode.Remove();
            return true;
        }

        public FileDataGroup GetFileDataGroupFromSelected()
        {
            TreeNode treeNode = this.treeView1.SelectedNode;
            if (treeNode == null)
                return new FileDataGroup(null, Enumerable.Empty<FileData>());
            if (treeNode.Level > 0)
                treeNode = treeNode.Parent;
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

        public void SortTree()
        {
            this.Enabled = false;
            FileListControl.SortTree(this.treeView1, this._sortStack);
            this.ScrollTreeToTop();
            this.Enabled = true;
        }

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
            this.TreeDataSource.Clear();
            this.treeView1.Nodes.Clear();
            this.fileTypesCheckedListBox.Items.Clear();
            this.sizeSortButton.SortOrder = SortOrder.None;
            this.dateCreatedButton.SortOrder = SortOrder.None;
            this.dateModifiedButton.SortOrder = SortOrder.None;
            this.filterForm.Reset();
            this._treeKeys.Clear();
        }

        /// <summary>
        /// Adds FileData to TreeView source, and to FileExtensionsList source with the provided imageKey.
        /// This will not commit the operation to the controls.
        /// </summary>
        /// <param name="fileData"></param>
        /// <param name="imageKey"></param>
        public void AddFileData(FileData fileData, bool commitRequired)
        {
            this.modifyFileTypesListBoxInternal = true;
            TreeNode treeNode;

            if (!this._treeKeys.ContainsKey(fileData.Directory))
            {
                treeNode = new TreeNode(fileData.Directory);
                treeNode.Name = fileData.Directory;
                this.TreeDataSource.Add(treeNode);
            }
            else
                treeNode = this.TreeDataSource.FirstOrDefault(n => n.Name.Equals(fileData.Directory));

            TreeNode node = new TreeNode(fileData.Name + fileData.Extension);
            string fileImageKey = fileData.Extension.Equals(string.Empty) ? UiHelper.NoneFileExtension : fileData.Extension;
            string directoryImageKey = fileData.Directory.ToLowerInvariant().Contains(UiHelper.ZipExtension) ? UiHelper.ZipExtension : UiHelper.DirectoryKey;

            node.Tag = fileData;
            node.ToolTipText = string.Join(Environment.NewLine, fileData.ExtendedProperties.Select(p => string.Format("{0}: {1}", p.Key, p.Value)).ToArray());
            node.ImageKey = fileImageKey;
            node.SelectedImageKey = fileImageKey;
            node.StateImageKey = fileImageKey;
            node.Name = fileData.Name + fileData.Extension;
            treeNode.ImageKey = directoryImageKey;
            treeNode.SelectedImageKey = directoryImageKey;
            treeNode.StateImageKey = directoryImageKey;
            treeNode.Name = fileData.Directory;

            if (!this._treeKeys.ContainsKey(fileData.Directory))
            {
                this._treeKeys.Add(fileData.Directory, treeNode.Clone() as TreeNode);

                if (!commitRequired)
                {
                    this.treeView1.Nodes.Add(this._treeKeys[fileData.Directory]);
                    if (!this._extensions.Contains(fileData.Extension))
                        this.fileTypesCheckedListBox.Items.Add(fileData.Extension, true);
                }
            }
            this._treeKeys[fileData.Directory].ImageKey = directoryImageKey;
            this._treeKeys[fileData.Directory].SelectedImageKey = directoryImageKey;
            this._treeKeys[fileData.Directory].StateImageKey = directoryImageKey;
            this._treeKeys[fileData.Directory].Name = fileData.Directory;
            this._treeKeys[fileData.Directory].Nodes.Add(node.Clone() as TreeNode);
            treeNode.Nodes.Add(node);

            if (!this._extensions.Contains(fileData.Extension))
                this._extensions.Add(fileData.Extension);
            this.countLabel.InvokeIfRequired(c => c.Text = this._treeKeys.Sum(k => k.Value.Nodes.Count).ToString());
            this.modifyFileTypesListBoxInternal = false;
        }

        public void Commit()
        {
            this.modifyFileTypesListBoxInternal = true;
            this.fileTypesCheckedListBox.Items.Clear();
            this.treeView1.Nodes.Clear();
            if (this._treeKeys.Count < 1)
                return;
            this.treeView1.Nodes.AddRange(this._treeKeys.Values.Where(t => !this.treeView1.Nodes.Contains(t)).ToArray<TreeNode>());
            foreach (object extension in this._extensions.Where(x => !this.fileTypesCheckedListBox.Items.Contains(x)).ToArray())
                this.fileTypesCheckedListBox.Items.Add(extension, true);
            this._treeKeys.Clear();
            this._extensions.Clear();
            this.modifyFileTypesListBoxInternal = false;
        }
        #endregion

        #region Event Handlers
        private void FilterForm_VisibleChanged(object sender, EventArgs e)
        {
            if (this.filterForm.Visible)
                return;
            this.SetVisibleNodes();
        }

        private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            EventHandler<FileDataSelectedEventArgs> fileDataSelected = this.OnFileDataSelected;
            if (fileDataSelected == null)
                return;
            this.OnFileDataSelected(this, this.GetEventArgs(fileDataSelected));
        }

        private void FileTypesCheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (this.modifyFileTypesListBoxInternal)
                return;
            FileListControl.SetNodeVisibility(this.treeView1, this.GetFilterPredicate(e), this.TreeDataSource);
            this.ScrollTreeToTop();
            this.treeView1.ExpandAll();
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
            this.OnFileDataSelected(this, this.GetEventArgs(openFileDataClicked));
        }

        private void FileLocationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EventHandler<FileDataSelectedEventArgs> openLocationClicked = this.OnOpenLocationClicked;
            if (openLocationClicked == null)
                return;
            this.OnFileDataSelected(this, this.GetEventArgs(openLocationClicked));
        }

        private void DeleteFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EventHandler<FileDataSelectedEventArgs> deleteFileDataClicked = this.OnDeleteFileDataClicked;
            if (deleteFileDataClicked == null)
                return;
            this.OnFileDataSelected(this, this.GetEventArgs(deleteFileDataClicked));
        }

        private void TreeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            string nodePath = FileListControl.GetNodePath(e.Node);
            if (nodePath.ToLowerInvariant().Contains(UiHelper.ZipExtension) && !Path.GetExtension(nodePath).ToLowerInvariant().Equals(UiHelper.ZipExtension) && e.Node.Checked)
            {
                (sender as TreeView).AfterCheck -= new TreeViewEventHandler(this.TreeView1_AfterCheck);
                MessageBox.Show("Extracting zip files is not supported yet.\nSelect the actual zip to copy or move it", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                e.Node.Checked = false;
                string key = nodePath.Substring(0, nodePath.IndexOf(UiHelper.ZipExtension, StringComparison.OrdinalIgnoreCase) + UiHelper.ZipExtension.Length);
                if (e.Node.TreeView.Nodes.ContainsKey(key))
                    e.Node.TreeView.Nodes[key].Checked = true;
                (sender as TreeView).AfterCheck += new TreeViewEventHandler(this.TreeView1_AfterCheck);
            }
            else
            {
                if (e.Node.Level > 0 || Path.GetExtension(nodePath).Equals(UiHelper.ZipExtension))
                    return;
                foreach (TreeNode node in e.Node.Nodes)
                    node.Checked = e.Node.Checked;
            }
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
            FileListControl.SortTree(this.treeView1, this._sortStack);
            this.treeView1.ExpandAll();
            this.ScrollTreeToTop();
        }

        private void DateCreatedButton_Click(object sender, EventArgs e)
        {
            this._sortStack.Push(Filter.DateCreated, new CompareFiledataNodeByDateCreated((sender as SortButton).SortOrder));
            FileListControl.SortTree(this.treeView1, this._sortStack);
            this.treeView1.ExpandAll();
            this.ScrollTreeToTop();
        }

        private void DateModifiedButton_Click(object sender, EventArgs e)
        {
            this._sortStack.Push(Filter.DateModified, new CompareFiledataNodeByDateModified((sender as SortButton).SortOrder));
            FileListControl.SortTree(this.treeView1, this._sortStack);
            this.treeView1.ExpandAll();
            this.ScrollTreeToTop();
        }

        private void CheckUncheckAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this._allCheck = this.GetCheckedNodes(this.treeView1).Count() <= 1;
            foreach (TreeNode node in this.treeView1.Nodes)
                node.Checked = this._allCheck;
        }


        #endregion

        #region Private Helpers

        private static void ScrollTreeLeft(TreeView tree)
        {
            Win32.Win32Methods.SendMessage(tree.Handle, WM_HSCROLL, SB_LEFT, 0);
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
                List<string> list = this.fileTypesCheckedListBox.CheckedItems.Cast<string>().ToList();
                if (checkedExt != null)
                {
                    string str = this.fileTypesCheckedListBox.Items[checkedExt.Index].ToString();
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
                    && Enum.IsDefined(typeof(StorageSize), this.filterForm.SizeFilter.StorageSize1) && this.filterForm.SizeFilter.StorageSize1 != StorageSize.None)
                {
                    SizeFilter sizeFilter = this.filterForm.SizeFilter;
                    // we'll send these values to CheckIsFileSizeWithinFilter()
                    KeyValuePair<float, StorageSize> fileSize = new KeyValuePair<float, StorageSize>(file.SizeInKilobytes.HasValue ? file.SizeInKilobytes.Value : 0, StorageSize.Kb);
                    FilterType filterType = sizeFilter.FilterType;
                    KeyValuePair<float, StorageSize> filter1 = new KeyValuePair<float, StorageSize>(sizeFilter.Value1, sizeFilter.StorageSize1);
                    KeyValuePair<float, StorageSize>? filter2 = null;

                    // we need a second value and second storage type in order to do a between comparison
                    if (sizeFilter.FilterType == FilterType.Between 
                        && (!sizeFilter.Value2.HasValue || !sizeFilter.StorageSize2.HasValue || sizeFilter.StorageSize2.Value == StorageSize.None))
                    {
                        filterType = FilterType.None;
                    }
                    else if (sizeFilter.FilterType == FilterType.Between
                            && sizeFilter.Value2.HasValue && sizeFilter.StorageSize2.HasValue && sizeFilter.StorageSize2.Value != StorageSize.None)
                    {
                        filter2 = new KeyValuePair<float, StorageSize>(sizeFilter.Value2.Value, sizeFilter.StorageSize2.Value);
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
            FileListControl.SortTree(this.treeView1, this._sortStack);
            FileListControl.SetNodeVisibility(this.treeView1, filterPredicate, this.TreeDataSource);
            this.treeView1.ExpandAll();
            this.treeView1.Refresh();
            this.ScrollTreeToTop();
            this.Enabled = true;
        }

        private IEnumerable<TreeNode> GetCheckedNodes(TreeView treeView)
        {
            List<TreeNode> treeNodeList = new List<TreeNode>();
            foreach (TreeNode node in treeView.Nodes)
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

        private FileDataSelectedEventArgs GetEventArgs(EventHandler<FileDataSelectedEventArgs> handler)
        {
            TreeNode selectedNode = this.treeView1.SelectedNode;
            List<FileData> fileDataList = new List<FileData>();
            string selectedPath;
            if (selectedNode.Level == 0)
            {
                foreach (TreeNode node in selectedNode.Nodes)
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


        private static void SortTree(TreeView treeView, FileDataSortStack sortStack)
        {
            List<IComparer<TreeNode>> comparerList = new List<IComparer<TreeNode>>();
            while (sortStack.MoveNext())
                comparerList.Add(sortStack.CurrentComparer);
            treeView.TreeViewNodeSorter = (System.Collections.IComparer)new MultiCompareFileData(comparerList);
            treeView.Sort();
        }

        [Obsolete("Performance kill. replaced by passing comparer directly to treeview sort method", true)]
        private static void SortTreeSource(List<TreeNode> treeNodes, FileDataSortStack sortStack)
        {
            // get our comparers
            List<IComparer<TreeNode>> comparerList = new List<IComparer<TreeNode>>();
            while (sortStack.MoveNext())
                comparerList.Add(sortStack.CurrentComparer);
            // create a milti compararer
            IMultiComparer<TreeNode> multi = new MultiCompareFileData(comparerList);
            // order chld nodes with multi comparer, then order parent nodes with multi comparer
            IEnumerable<TreeNode> sortedNodes = treeNodes.OrderBy(n =>
            {
                TreeNode[] childNodes = n.Nodes.Cast<TreeNode>().OrderBy((m => m), multi).ToArray();
                n.Nodes.Clear();
                n.Nodes.AddRange(childNodes);
                return n;
            }, multi).ToArray();
            // replace the tree nodes with the sorted nodes
            treeNodes.Clear();
            treeNodes.AddRange(sortedNodes);
        }

        [Obsolete("Performance kill. replaced by passing comparer directly to treeview sort method", true)]
        private static void SortChildNodes(TreeNode node, FileDataSortStack sortStack)
        {
            // order each node in parent by each sort - size, date created, etc.. in whatever order they are in the sort stack
            foreach (TreeNode node1 in node.Nodes.Cast<TreeNode>().ToList()
                .OrderBy((n => n), sortStack[0].Value)
                .ThenBy((n => n), sortStack[1].Value)
                .ThenBy((n => n), sortStack[2].Value)
                .ThenBy((n => n), sortStack[3].Value)
                .ToList())
            {
                node1.Remove();
                node.Nodes.Add(node1);
            }
        }

        private static bool CheckIsFileSizeWithinFilter(
          FilterType filterType,
          KeyValuePair<float, StorageSize> filter1,
          KeyValuePair<float, StorageSize>? filter2,
          KeyValuePair<float, StorageSize> fileSize)
        {
            float kb1 = Misc.ConvertStorageValueToKb(filter1.Key, filter1.Value);
            float kb2 = filter2.HasValue ? Misc.ConvertStorageValueToKb(filter2.Value.Key, filter2.Value.Value) : 0.0f;
            float fileKb = Misc.ConvertStorageValueToKb(fileSize.Key, fileSize.Value);
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

        private static void SetTreeSource(TreeView treeView, List<TreeNode> treeNodes)
        {
        }

        private static string GetNodePath(TreeNode node)
        {
            FileData? tag = node.Tag as FileData?;
            return !tag.HasValue ? node.Text : tag.Value.Path;
        }

        private static void SetNodeVisibility(
          TreeView tree,
          Func<FileData, bool> predicate,
          List<TreeNode> treeDataSource)
        {
            foreach (TreeNode treeNode in treeDataSource)
            {
                if (!tree.Nodes.ContainsKey(treeNode.Name))
                {
                    tree.Nodes.Add(treeNode.Clone() as TreeNode);
                    tree.Nodes[treeNode.Name].Nodes.Clear();
                }
                foreach (TreeNode node in treeNode.Nodes)
                {
                    bool flag = predicate((FileData)node.Tag);
                    if (flag && !tree.Nodes[treeNode.Name].Nodes.ContainsKey(node.Name))
                        tree.Nodes[treeNode.Name].Nodes.Add(node.Clone() as TreeNode);
                    else if (!flag && tree.Nodes[treeNode.Name].Nodes.ContainsKey(node.Name))
                        tree.Nodes[treeNode.Name].Nodes.RemoveByKey(node.Name);
                }
                if (tree.Nodes.ContainsKey(treeNode.Name) && tree.Nodes[treeNode.Name].Nodes.Count < 1)
                    tree.Nodes.RemoveByKey(treeNode.Name);
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
}
