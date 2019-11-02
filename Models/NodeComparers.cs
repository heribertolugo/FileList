using Common.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace FileList.Models
{

    public class CompareFiledataNodeByName : IComparer<TreeNode>
    {
        private int _compareModifier = 0;
        private SortOrder _sortOrder;

        public CompareFiledataNodeByName(SortOrder sortOrder)
        {
            this._sortOrder = sortOrder;
            switch (sortOrder)
            {
                case SortOrder.None:
                    this._compareModifier = 0;
                    break;
                case SortOrder.Ascending:
                    this._compareModifier = 1;
                    break;
                case SortOrder.Descending:
                    this._compareModifier = -1;
                    break;
            }
        }

        public Filter Filter
        {
            get
            {
                return Filter.Name;
            }
            private set
            {
            }
        }

        public int Compare(TreeNode x, TreeNode y)
        {
            return x.Name.CompareTo(y.Name) * this._compareModifier;
        }
    }
    public class CompareFiledataNodeByDateModified : IComparer<TreeNode>
    {
        private int _compareModifier = 0;
        private SortOrder _sortOrder;

        public CompareFiledataNodeByDateModified(SortOrder sortOrder)
        {
            this._sortOrder = sortOrder;
            switch (sortOrder)
            {
                case SortOrder.None:
                    this._compareModifier = 0;
                    break;
                case SortOrder.Ascending:
                    this._compareModifier = 1;
                    break;
                case SortOrder.Descending:
                    this._compareModifier = -1;
                    break;
            }
        }

        public Filter Filter
        {
            get
            {
                return Filter.DateModified;
            }
            private set
            {
            }
        }

        public int Compare(TreeNode x, TreeNode y)
        {
            FileData? filedata1 = x.Tag as FileData?;
            FileData? filedata2 = y.Tag as FileData?;
            DateTime? dateModified;
            DateTime dateTime1;
            DateTime dateTime2;

            if (filedata1.HasValue)
            {
                if (filedata1.Value.DateModified.HasValue)
                {
                    dateModified = filedata1.Value.DateModified;
                    dateTime1 = dateModified.Value;
                }
                else
                    dateTime1 = DateTime.MinValue;
            }
            else
                dateTime1 = !Directory.Exists(x.Text) ? DateTime.MinValue : new DirectoryInfo(x.Text).LastWriteTime;
            if (filedata2.HasValue)
            {
                dateModified = filedata2.Value.DateModified;
                if (dateModified.HasValue)
                {
                    dateModified = filedata2.Value.DateModified;
                    dateTime2 = dateModified.Value;
                }
                else
                    dateTime2 = DateTime.MinValue;
            }
            else
                dateTime2 = !Directory.Exists(y.Text) ? DateTime.MinValue : new DirectoryInfo(y.Text).LastWriteTime;

            return dateTime1.CompareTo(dateTime2) * this._compareModifier;
        }
    }
    public class CompareFiledataNodeByDateCreated : IComparer<TreeNode>
    {
        private int _compareModifier = 0;
        private SortOrder _sortOrder;

        public CompareFiledataNodeByDateCreated(SortOrder sortOrder)
        {
            this._sortOrder = sortOrder;
            switch (sortOrder)
            {
                case SortOrder.None:
                    this._compareModifier = 0;
                    break;
                case SortOrder.Ascending:
                    this._compareModifier = 1;
                    break;
                case SortOrder.Descending:
                    this._compareModifier = -1;
                    break;
            }
        }

        public Filter Filter
        {
            get
            {
                return Filter.DateCreated;
            }
            private set
            {
            }
        }

        public int Compare(TreeNode x, TreeNode y)
        {
            FileData? filedata1 = x.Tag as FileData?;
            FileData? filedata2 = y.Tag as FileData?;
            DateTime? dateCreated;
            DateTime dateTime1;
            DateTime dateTime2;

            if (filedata1.HasValue)
            {
                if (filedata1.Value.DateCreated.HasValue)
                {
                    dateCreated = filedata1.Value.DateCreated;
                    dateTime1 = dateCreated.Value;
                }
                else
                    dateTime1 = DateTime.MinValue;
            }
            else
                dateTime1 = !Directory.Exists(x.Text) ? DateTime.MinValue : new DirectoryInfo(x.Text).CreationTime;

            if (filedata2.HasValue)
            {
                dateCreated = filedata2.Value.DateCreated;
                if (dateCreated.HasValue)
                {
                    dateCreated = filedata2.Value.DateCreated;
                    dateTime2 = dateCreated.Value;
                }
                else
                    dateTime2 = DateTime.MinValue;
            }
            else
                dateTime2 = !Directory.Exists(y.Text) ? DateTime.MinValue : new DirectoryInfo(y.Text).CreationTime;

            return dateTime1.CompareTo(dateTime2) * this._compareModifier;
        }
    }
    public class CompareFiledataNodeBySize : IComparer<TreeNode>
    {
        private int _compareModifier = 0;
        private SortOrder _sortOrder;

        public CompareFiledataNodeBySize(SortOrder sortOrder)
        {
            this._sortOrder = sortOrder;
            switch (sortOrder)
            {
                case SortOrder.None:
                    this._compareModifier = 0;
                    break;
                case SortOrder.Ascending:
                    this._compareModifier = 1;
                    break;
                case SortOrder.Descending:
                    this._compareModifier = -1;
                    break;
            }
        }

        public Filter Filter
        {
            get
            {
                return Filter.Size;
            }
            private set
            {
            }
        }

        public int Compare(TreeNode x, TreeNode y)
        {
            FileData? filedata1 = x.Tag as FileData?;
            FileData? filedata2 = y.Tag as FileData?;
            float xValue;
            float yValue;

            if (filedata1.HasValue)
            {
                if (filedata1.Value.SizeInKilobytes.HasValue)
                {
                    xValue = filedata1.Value.SizeInKilobytes.Value;
                }
                else
                    xValue = 0;
            }
            else
                xValue = x.Nodes.Cast<TreeNode>().Sum(n =>
                {
                    FileData? fileData = n.Tag as FileData?;
                    if (!fileData.HasValue)
                        return 0;

                    return fileData.Value.SizeInKilobytes.HasValue ? fileData.Value.SizeInKilobytes.Value : 0;
                });

            if (filedata2.HasValue)
            {
                if (filedata2.Value.SizeInKilobytes.HasValue)
                {
                    yValue = filedata2.Value.SizeInKilobytes.Value;
                }
                else
                    yValue = 0;
            }
            else
                yValue = y.Nodes.Cast<TreeNode>().Sum(n =>
                {
                    FileData? fileData = n.Tag as FileData?;
                    if (!fileData.HasValue)
                        return 0;

                    return fileData.Value.SizeInKilobytes.HasValue ? fileData.Value.SizeInKilobytes.Value : 0;
                });

            return xValue.CompareTo(yValue) * this._compareModifier;
        }
    }
    public class MultiCompareFileData : IMultiComparer<TreeNode>, IComparer<TreeNode>, System.Collections.IComparer
    {
        public MultiCompareFileData(IEnumerable<IComparer<TreeNode>> comparers)
        {
            this.Comparers = comparers;
        }

        public IEnumerable<IComparer<TreeNode>> Comparers { get; private set; }

        public int Compare(TreeNode x, TreeNode y)
        {
            int result = 0;
            foreach (IComparer<TreeNode> comparer in this.Comparers)
            {
                int comparison = comparer.Compare(x, y);
                if (comparison != 0)
                {
                    result = comparison;
                    break;
                }
            }
            return result;
        }

        public int Compare(object x, object y)
        {
            if (!(x is TreeNode) || !(y is TreeNode))
                return 0;
            return this.Compare(x as TreeNode, y as TreeNode);
        }

        public void Clear()
        {
            this.Comparers = Enumerable.Empty<IComparer<TreeNode>>();
        }

        public void SetComparers(IEnumerable<IComparer<TreeNode>> comparers)
        {
            this.Clear();
            this.Comparers = comparers;
        }

        public bool AddComparer(IComparer<TreeNode> comparer)
        {
            if (this.Comparers.Any(c => c.Equals(comparer)))
                return false;

            List<IComparer<TreeNode>> comparers = this.Comparers.ToList();
            comparers.Add(comparer);
            this.Comparers = comparers;
            return true;
        }

        public bool Remove(IComparer<TreeNode> comparer)
        {
            List<IComparer<TreeNode>> comparers = this.Comparers.ToList();
            return comparers.Remove(comparer);
        }
    }
}
