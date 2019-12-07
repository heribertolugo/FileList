using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Common.Extensions
{
    public static class TreeViewExtensions
    {
        public static bool IsInVisibleScope(this System.Windows.Forms.TreeNode node)
        {
            if (node == null)
                return false;
            return node.TreeView.ClientRectangle.Contains(new Rectangle(node.Bounds.Location, new System.Drawing.Size(1, 1)));
        }

        public static bool IsFullyVisible(this System.Windows.Forms.TreeNode node)
        {
            if (node == null)
                return false;
            return node.TreeView.ClientRectangle.Contains(node.Bounds);
        }

        public static IEnumerable<TreeNode> SortChildNodes(this TreeNode parent, IComparer<TreeNode> comparer)
        {
            if (parent is null || parent.Nodes.Count < 2)
                return parent == null ? Enumerable.Empty<TreeNode>() : parent.Nodes.Cast<TreeNode>();

            TreeNode[] nodes = parent.Nodes.Cast<TreeNode>().OrderBy(n => n,comparer).ToArray();
            parent.Nodes.Clear();
            parent.Nodes.AddRange(nodes);

            return nodes;
        }

        public static int GetNodeCount(this TreeView treeView)
        {
            if (treeView.Nodes.Count < 1)
                return 0;

            int count = 0;
            TreeNode node = treeView.Nodes[0];

            while (node != null)
            {
                count++;

                if (node.IsExpanded)
                {
                    node = node.Nodes[0];
                    continue;
                }

                TreeNode tempNode = node;
                node = node.NextNode;

                if (node == null && tempNode.Parent != null)
                {
                    node = tempNode.Parent.NextNode;
                }
            }

            return count;
        }
    }
}
