using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace FileList.Models
{
    public partial class FileListControl : UserControl
    {
        //public event EventHandler<FileDataSelectedEventArgs> OnFileDataSelected;
        //public event EventHandler<FileDataSelectedEventArgs> OnOpenFileDataClicked;
        //public event EventHandler<FileDataSelectedEventArgs> OnOpenLocationClicked;
        //public event EventHandler<FileDataSelectedEventArgs> OnDeleteFileDataClicked;

        private readonly List<TreeNode> TreeDataSource = new List<TreeNode>();
        private bool modifyFileTypesListBoxInternal = false;
        private Dictionary<string, TreeNode> _treeKeys = new Dictionary<string, TreeNode>();
        private List<string> _extensions = new List<string>();
        //private FileFilterForm filterForm;
        //private FileDataSortStack _sortStack;
        private bool _allCheck;

        private const int WM_HSCROLL = 276;
        private const int SB_LEFT = 6;

        public FileListControl()
        {
            InitializeComponent();
            //this.filterForm = new FileFilterForm();
            //this.filterForm.VisibleChanged += new EventHandler(this.FilterForm_VisibleChanged);
            //this._sortStack = new FileDataSortStack();
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

        //public FileDataGroup GetFileDataGroupFromSelected()
        //{
        //    TreeNode treeNode = this.treeView1.SelectedNode;
        //    if (treeNode == null)
        //        return new FileDataGroup((string)null, Enumerable.Empty<FileData>());
        //    if ((uint)treeNode.Level > 0U)
        //        treeNode = treeNode.Parent;
        //    return new FileDataGroup(treeNode.Text, treeNode.Nodes.Cast<TreeNode>().Select<TreeNode, FileData>((Func<TreeNode, FileData>)(n => (FileData)n.Tag)));
        //}

        public string[] GetCheckedPaths()
        {
            return this.GetCheckedNodes(this.treeView1).Where<TreeNode>((Func<TreeNode, bool>)(n =>
            {
                if ((n.Tag as FileData?).HasValue)
                    return true;
                //if (Path.HasExtension(n.Text))
                //    return Path.GetExtension(n.Text).ToLowerInvariant().Equals(UiHelper.ZipExtension);
                return false;
            })).Select<TreeNode, string>((Func<TreeNode, string>)(n =>
            {
                FileData? tag = n.Tag as FileData?;
                return tag.HasValue ? tag.Value.Path : n.Text;
            })).ToArray<string>();
        }

        public FileData[] GetCheckedFileData()
        {
            return this.GetCheckedNodes(this.treeView1).Where<TreeNode>((Func<TreeNode, bool>)(n => (n.Tag as FileData?).HasValue)).Select<TreeNode, FileData>((Func<TreeNode, FileData>)(n => (FileData)n.Tag)).ToArray<FileData>();
        }

        public void SortTree()
        {
            this.Enabled = false;
            //FileListControl.SortTree(this.treeView1, this._sortStack);
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
            //this.filterForm.Reset();
        }

        public void AddFileData(FileData fileData, string imageKey)
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
                treeNode = this.TreeDataSource.FirstOrDefault<TreeNode>((Func<TreeNode, bool>)(n => n.Name.Equals(fileData.Directory)));
            TreeNode node = new TreeNode(fileData.Name + fileData.Extension);
            //string str1 = fileData.Extension.Equals(string.Empty) ? UiHelper.NoneFileExtension : fileData.Extension;
            //node.Tag = (object)fileData;
            //node.ToolTipText = string.Join(Environment.NewLine, fileData.ExtendedProperties.Select<KeyValuePair<string, string>, string>((Func<KeyValuePair<string, string>, string>)(p => string.Format("{0}: {1}", (object)p.Key, (object)p.Value))).ToArray<string>());
            //node.ImageKey = str1;
            //node.SelectedImageKey = str1;
            //node.StateImageKey = str1;
            //node.Name = fileData.Name + fileData.Extension;
            //string str2 = fileData.Directory.ToLowerInvariant().Contains(UiHelper.ZipExtension) ? UiHelper.ZipExtension : UiHelper.DirectoryKey;
            //treeNode.ImageKey = str2;
            //treeNode.SelectedImageKey = str2;
            //treeNode.StateImageKey = str2;
            //treeNode.Name = fileData.Directory;
            //if (!this._treeKeys.ContainsKey(fileData.Directory))
            //    this._treeKeys.Add(fileData.Directory, treeNode.Clone() as TreeNode);
            //this._treeKeys[fileData.Directory].ImageKey = str2;
            //this._treeKeys[fileData.Directory].SelectedImageKey = str2;
            //this._treeKeys[fileData.Directory].StateImageKey = str2;
            //this._treeKeys[fileData.Directory].Name = fileData.Directory;
            //this._treeKeys[fileData.Directory].Nodes.Add(node.Clone() as TreeNode);
            //treeNode.Nodes.Add(node);
            //if (!this._extensions.Contains(fileData.Extension))
            //    this._extensions.Add(fileData.Extension);
            //this.modifyFileTypesListBoxInternal = false;
        }

        public void Commit()
        {
            this.modifyFileTypesListBoxInternal = true;
            this.treeView1.Nodes.AddRange(this._treeKeys.Values.ToArray<TreeNode>());
            foreach (object extension in this._extensions)
                this.fileTypesCheckedListBox.Items.Add(extension, true);
            this._treeKeys.Clear();
            this._extensions.Clear();
            this.modifyFileTypesListBoxInternal = false;
        }
        #endregion

        #region Event Handlers
        private void FilterForm_VisibleChanged(object sender, EventArgs e)
        {
            //if (this.filterForm.Visible)
            //    return;
            //this.SetVisibleNodes();
        }

        private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //EventHandler<FileDataSelectedEventArgs> fileDataSelected = this.OnFileDataSelected;
            //if (fileDataSelected == null)
            //    return;
            //this.OnFileDataSelected((object)this, this.GetEventArgs(fileDataSelected));
        }

        private void FileTypesCheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            //if (this.modifyFileTypesListBoxInternal)
            //    return;
            //FileListControl.SetNodeVisibility(this.treeView1, this.GetFilterPredicate(e), this.TreeDataSource);
            //this.ScrollTreeToTop();
            //this.treeView1.ExpandAll();
        }

        private void TreeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
                return;
            TreeNode node = e.Node;
            if (node == null)
                return;
            this.treeView1.SelectedNode = node;
            this.filesTreeViewContextMenu.Items[this.openFileToolStripMenuItem.Name].Enabled = (uint)node.Level > 0U;
            this.filesTreeViewContextMenu.Show((Control)this.treeView1, e.X, e.Y);
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
            //EventHandler<FileDataSelectedEventArgs> openFileDataClicked = this.OnOpenFileDataClicked;
            //if (openFileDataClicked == null)
            //    return;
            //this.OnFileDataSelected((object)this, this.GetEventArgs(openFileDataClicked));
        }

        private void FileLocationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //EventHandler<FileDataSelectedEventArgs> openLocationClicked = this.OnOpenLocationClicked;
            //if (openLocationClicked == null)
            //    return;
            //this.OnFileDataSelected((object)this, this.GetEventArgs(openLocationClicked));
        }

        private void DeleteFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //EventHandler<FileDataSelectedEventArgs> deleteFileDataClicked = this.OnDeleteFileDataClicked;
            //if (deleteFileDataClicked == null)
            //    return;
            //this.OnFileDataSelected((object)this, this.GetEventArgs(deleteFileDataClicked));
        }

        private void TreeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            //string nodePath = FileListControl.GetNodePath(e.Node);
            //if (nodePath.ToLowerInvariant().Contains(UiHelper.ZipExtension) && !Path.GetExtension(nodePath).ToLowerInvariant().Equals(UiHelper.ZipExtension) && e.Node.Checked)
            //{
            //    (sender as TreeView).AfterCheck -= new TreeViewEventHandler(this.TreeView1_AfterCheck);
            //    int num = (int)MessageBox.Show("Extracting zip files is not supported yet.\nSelect the actual zip to copy or move it", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            //    e.Node.Checked = false;
            //    string key = nodePath.Substring(0, nodePath.IndexOf(UiHelper.ZipExtension, StringComparison.OrdinalIgnoreCase) + UiHelper.ZipExtension.Length);
            //    if (e.Node.TreeView.Nodes.ContainsKey(key))
            //        e.Node.TreeView.Nodes[key].Checked = true;
            //    (sender as TreeView).AfterCheck += new TreeViewEventHandler(this.TreeView1_AfterCheck);
            //}
            //else
            //{
            //    if ((uint)e.Node.Level > 0U || Path.GetExtension(nodePath).Equals(UiHelper.ZipExtension))
            //        return;
            //    foreach (TreeNode node in e.Node.Nodes)
            //        node.Checked = e.Node.Checked;
            //}
        }

        private void FilterButton_Click(object sender, EventArgs e)
        {
            //this.filterForm.Show(sender as Control);
        }

        private void SizeSortButton_Click(object sender, EventArgs e)
        {
            //this._sortStack.Push(FileFilterForm.Filter.Size, (IComparer<TreeNode>)new CompareFiledataNodeBySize((sender as SortButton).SortOrder));
            //FileListControl.SortTree(this.treeView1, this._sortStack);
            //this.treeView1.ExpandAll();
            //this.ScrollTreeToTop();
        }

        private void DateCreatedButton_Click(object sender, EventArgs e)
        {
            //this._sortStack.Push(FileFilterForm.Filter.DateCreated, (IComparer<TreeNode>)new CompareFiledataNodeByDateCreated((sender as SortButton).SortOrder));
            //FileListControl.SortTree(this.treeView1, this._sortStack);
            //this.treeView1.ExpandAll();
            //this.ScrollTreeToTop();
        }

        private void DateModifiedButton_Click(object sender, EventArgs e)
        {
            //this._sortStack.Push(FileFilterForm.Filter.DateModified, (IComparer<TreeNode>)new CompareFiledataNodeByDateModified((sender as SortButton).SortOrder));
            //FileListControl.SortTree(this.treeView1, this._sortStack);
            //this.treeView1.ExpandAll();
            //this.ScrollTreeToTop();
        }

        private void CheckUncheckAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //this._allCheck = this.GetCheckedNodes(this.treeView1).Count<TreeNode>() <= 1;
            //foreach (TreeNode node in this.treeView1.Nodes)
            //    node.Checked = this._allCheck;
        }


        #endregion

        #region Private Helpers

        private static void ScrollTreeLeft(TreeView tree)
        {
            FileListControl.SendMessage(tree.Handle, WM_HSCROLL, SB_LEFT, 0);
        }



        private Func<FileData, bool> GetFilterPredicate(ItemCheckEventArgs checkedExt = null)
        {
            return (Func<FileData, bool>)(f =>
            {
                List<string> list = this.fileTypesCheckedListBox.CheckedItems.Cast<string>().ToList<string>();
                if (checkedExt != null)
                {
                    string str = this.fileTypesCheckedListBox.Items[checkedExt.Index].ToString();
                    if (checkedExt.NewValue == CheckState.Checked && !list.Contains(str))
                        list.Add(str);
                    else if (checkedExt.NewValue == CheckState.Unchecked && list.Contains(str))
                        list.Remove(str);
                }
                if (!list.Contains(f.Extension))
                    return false;
                //if ((uint)this.filterForm.SizeFilter.FilterType > 0U)
                //{
                //    KeyValuePair<float, FileFilterForm.StorageSize> fileSize = new KeyValuePair<float, FileFilterForm.StorageSize>(f.SizeInKilobytes.HasValue ? f.SizeInKilobytes.Value : 0.0f, FileFilterForm.StorageSize.Kb);
                //    KeyValuePair<float, FileFilterForm.StorageSize> filter1;
                //    ref KeyValuePair<float, FileFilterForm.StorageSize> local = ref filter1;
                //    double num1 = (double)this.filterForm.SizeFilter.Value1;
                //    SizeFilter sizeFilter1 = this.filterForm.SizeFilter;
                //    int storageSize1 = (int)sizeFilter1.StorageSize1;
                //    local = new KeyValuePair<float, FileFilterForm.StorageSize>((float)num1, (FileFilterForm.StorageSize)storageSize1);
                //    sizeFilter1 = this.filterForm.SizeFilter;
                //    int? nullable1 = sizeFilter1.Value2;
                //    SizeFilter sizeFilter2;
                //    KeyValuePair<float, FileFilterForm.StorageSize>? nullable2;
                //    if (nullable1.HasValue)
                //    {
                //        FileFilterForm.StorageSize? storageSize2 = this.filterForm.SizeFilter.StorageSize2;
                //        if (!storageSize2.HasValue)
                //        {
                //            nullable1 = this.filterForm.SizeFilter.Value2;
                //            double num2 = (double)nullable1.Value;
                //            sizeFilter2 = this.filterForm.SizeFilter;
                //            storageSize2 = sizeFilter2.StorageSize2;
                //            int num3 = (int)storageSize2.Value;
                //            nullable2 = new KeyValuePair<float, FileFilterForm.StorageSize>?(new KeyValuePair<float, FileFilterForm.StorageSize>((float)num2, (FileFilterForm.StorageSize)num3));
                //            goto label_13;
                //        }
                //    }
                //    nullable2 = new KeyValuePair<float, FileFilterForm.StorageSize>?();
                //label_13:
                //    KeyValuePair<float, FileFilterForm.StorageSize>? filter2 = nullable2;
                //    sizeFilter2 = this.filterForm.SizeFilter;
                //    if (!FileListControl.CheckIsFileSizeWithinFilter(sizeFilter2.FilterType, filter1, filter2, fileSize))
                //        return false;
                //}
                //DateTime? nullable;
                //if ((uint)this.filterForm.CreatedDateFilter.FilterType > 0U && f.DateModified.HasValue)
                //{
                //    int filterType = (int)this.filterForm.CreatedDateFilter.FilterType;
                //    nullable = this.filterForm.CreatedDateFilter.DateTime1;
                //    DateTime minValue1;
                //    if (!nullable.HasValue)
                //    {
                //        minValue1 = DateTime.MinValue;
                //    }
                //    else
                //    {
                //        nullable = this.filterForm.CreatedDateFilter.DateTime1;
                //        minValue1 = nullable.Value;
                //    }
                //    nullable = this.filterForm.CreatedDateFilter.DateTime2;
                //    DateTime minValue2;
                //    if (!nullable.HasValue)
                //    {
                //        minValue2 = DateTime.MinValue;
                //    }
                //    else
                //    {
                //        nullable = this.filterForm.CreatedDateFilter.DateTime2;
                //        minValue2 = nullable.Value;
                //    }
                //    DateTime? filter2 = new DateTime?(minValue2);
                //    nullable = f.DateModified;
                //    DateTime fileDate = nullable.Value;
                //    if (!FileListControl.CheckFileDateWithinFilter((FileFilterForm.FilterType)filterType, minValue1, filter2, fileDate))
                //        return false;
                //}
                //if ((uint)this.filterForm.ModifiedDateFilter.FilterType > 0U)
                //{
                //    nullable = f.DateCreated;
                //    if (nullable.HasValue)
                //    {
                //        int filterType = (int)this.filterForm.ModifiedDateFilter.FilterType;
                //        nullable = this.filterForm.ModifiedDateFilter.DateTime1;
                //        DateTime minValue1;
                //        if (!nullable.HasValue)
                //        {
                //            minValue1 = DateTime.MinValue;
                //        }
                //        else
                //        {
                //            nullable = this.filterForm.ModifiedDateFilter.DateTime1;
                //            minValue1 = nullable.Value;
                //        }
                //        nullable = this.filterForm.ModifiedDateFilter.DateTime2;
                //        DateTime minValue2;
                //        if (!nullable.HasValue)
                //        {
                //            minValue2 = DateTime.MinValue;
                //        }
                //        else
                //        {
                //            nullable = this.filterForm.ModifiedDateFilter.DateTime2;
                //            minValue2 = nullable.Value;
                //        }
                //        DateTime? filter2 = new DateTime?(minValue2);
                //        nullable = f.DateCreated;
                //        DateTime fileDate = nullable.Value;
                //        if (!FileListControl.CheckFileDateWithinFilter((FileFilterForm.FilterType)filterType, minValue1, filter2, fileDate))
                //            return false;
                //    }
                //}
                return true;
            });
        }

        private void SetVisibleNodes()
        {
            this.Enabled = false;
            Func<FileData, bool> filterPredicate = this.GetFilterPredicate((ItemCheckEventArgs)null);
            //FileListControl.SortTree(this.treeView1, this._sortStack);
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
            return (IEnumerable<TreeNode>)treeNodeList;
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
                    enumerator = (IEnumerator<TreeNode>)null;
                    child = (TreeNode)null;
                }
            }
            if (node.Checked)
                yield return node;
        }

        //private FileDataSelectedEventArgs GetEventArgs(
        //  EventHandler<FileDataSelectedEventArgs> handler)
        //{
        //    TreeNode selectedNode = this.treeView1.SelectedNode;
        //    List<FileData> fileDataList = new List<FileData>();
        //    string selectedPath;
        //    if (selectedNode.Level == 0)
        //    {
        //        foreach (TreeNode node in selectedNode.Nodes)
        //            fileDataList.Add((FileData)node.Tag);
        //        selectedPath = selectedNode.Text;
        //    }
        //    else
        //    {
        //        fileDataList.Add((FileData)selectedNode.Tag);
        //        selectedPath = ((FileData)selectedNode.Tag).Path;
        //    }
        //    return new FileDataSelectedEventArgs(fileDataList.ToArray(), selectedPath, selectedNode.Level == 0);
        //}

        //private static void SortTree(TreeView treeView, FileDataSortStack sortStack)
        //{
        //    List<IComparer<TreeNode>> comparerList = new List<IComparer<TreeNode>>();
        //    while (sortStack.MoveNext())
        //        comparerList.Add(sortStack.CurrentComparer);
        //    IMultiComparer<TreeNode> multiComparer = (IMultiComparer<TreeNode>)new MultiCompareFileData((IEnumerable<IComparer<TreeNode>>)comparerList);
        //    treeView.TreeViewNodeSorter = (IComparer)multiComparer;
        //    treeView.Sort();
        //}

        //private static void SortTreeSource(List<TreeNode> treeNodes, FileDataSortStack sortStack)
        //{
        //    List<IComparer<TreeNode>> comparerList = new List<IComparer<TreeNode>>();
        //    while (sortStack.MoveNext())
        //        comparerList.Add(sortStack.CurrentComparer);
        //    IMultiComparer<TreeNode> multi = (IMultiComparer<TreeNode>)new MultiCompareFileData((IEnumerable<IComparer<TreeNode>>)comparerList);
        //    IEnumerable<TreeNode> array1 = (IEnumerable<TreeNode>)treeNodes.OrderBy<TreeNode, TreeNode>((Func<TreeNode, TreeNode>)(n =>
        //    {
        //        TreeNode[] array2 = n.Nodes.Cast<TreeNode>().OrderBy<TreeNode, TreeNode>((Func<TreeNode, TreeNode>)(m => m), (IComparer<TreeNode>)multi).ToArray<TreeNode>();
        //        n.Nodes.Clear();
        //        n.Nodes.AddRange(array2);
        //        return n;
        //    }), (IComparer<TreeNode>)multi).ToArray<TreeNode>();
        //    treeNodes.Clear();
        //    treeNodes.AddRange(array1);
        //}

        //private static void SortChildNodes(TreeNode node, FileDataSortStack sortStack)
        //{
        //    foreach (TreeNode node1 in node.Nodes.Cast<TreeNode>().ToList<TreeNode>().OrderBy<TreeNode, TreeNode>((Func<TreeNode, TreeNode>)(n => n), sortStack[0].Value).ThenBy<TreeNode, TreeNode>((Func<TreeNode, TreeNode>)(n => n), sortStack[1].Value).ThenBy<TreeNode, TreeNode>((Func<TreeNode, TreeNode>)(n => n), sortStack[2].Value).ThenBy<TreeNode, TreeNode>((Func<TreeNode, TreeNode>)(n => n), sortStack[3].Value).ToList<TreeNode>())
        //    {
        //        node1.Remove();
        //        node.Nodes.Add(node1);
        //    }
        //}

        //private static bool CheckIsFileSizeWithinFilter(
        //  FileFilterForm.FilterType filterType,
        //  KeyValuePair<float, FileFilterForm.StorageSize> filter1,
        //  KeyValuePair<float, FileFilterForm.StorageSize>? filter2,
        //  KeyValuePair<float, FileFilterForm.StorageSize> fileSize)
        //{
        //    float kb1 = Misc.ConvertStorageValueToKb(filter1.Key, filter1.Value);
        //    float num = filter2.HasValue ? Misc.ConvertStorageValueToKb(filter2.Value.Key, filter2.Value.Value) : 0.0f;
        //    float kb2 = Misc.ConvertStorageValueToKb(fileSize.Key, fileSize.Value);
        //    switch (filterType)
        //    {
        //        case FileFilterForm.FilterType.Between:
        //            if ((double)kb2 < (double)kb1 || (double)kb2 > (double)num)
        //                return false;
        //            break;
        //        case FileFilterForm.FilterType.LessThan:
        //            if ((double)kb2 >= (double)kb1)
        //                return false;
        //            break;
        //        case FileFilterForm.FilterType.GreaterThan:
        //            if ((double)kb2 <= (double)kb1)
        //                return false;
        //            break;
        //        case FileFilterForm.FilterType.Equals:
        //            if ((double)kb2 != (double)kb1)
        //                return false;
        //            break;
        //        default:
        //            return false;
        //    }
        //    return true;
        //}

        //private static bool CheckFileDateWithinFilter(
        //  FileFilterForm.FilterType filterType,
        //  DateTime filter1,
        //  DateTime? filter2,
        //  DateTime fileDate)
        //{
        //    DateTime dateTime = filter2.HasValue ? filter2.Value : DateTime.MinValue;
        //    switch (filterType)
        //    {
        //        case FileFilterForm.FilterType.Between:
        //            if (fileDate < filter1 || fileDate > dateTime)
        //                return false;
        //            break;
        //        case FileFilterForm.FilterType.LessThan:
        //            if (fileDate >= filter1)
        //                return false;
        //            break;
        //        case FileFilterForm.FilterType.GreaterThan:
        //            if (fileDate <= filter1)
        //                return false;
        //            break;
        //        case FileFilterForm.FilterType.Equals:
        //            if (fileDate != filter1)
        //                return false;
        //            break;
        //        default:
        //            return false;
        //    }
        //    return true;
        //}

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

        #region System32

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);
        #endregion
    }
}
