using System;
using System.Drawing;
using System.Windows.Forms;
using Win32.Constants;
using Win32.Libraries;

namespace FileList.Models
{
    public class ScrollNotifyTreeView : System.Windows.Forms.TreeView
    {
        public event EventHandler<ScrollNotifyTreeViewEventArgs> Scrolled;

        public ScrollNotifyTreeView() : base()
        {
            this.HideSelection = false;
            this.DrawMode = TreeViewDrawMode.OwnerDrawText;
        }

        public bool VerticleScrollVisible()
        {
            long style = user32.GetWindowLongPtr(this.Handle, GwlCodes.GWL_STYLE).ToInt64();
            return ((style & WindowStyles.WS_VSCROLL) != 0);
        }

        public bool HorizontalScrollVisible()
        {
            long style = user32.GetWindowLongPtr(this.Handle, GwlCodes.GWL_STYLE).ToInt64();
            return ((style & WindowStyles.WS_HSCROLL) != 0);
        }

        public TreeNode GetBottomVisibleNode()
        {
            TreeNode currentNode = this.TopNode;
            TreeNode tempNode = currentNode;
            int counter = this.VisibleCount;

            while (tempNode != null)
            {

                tempNode = tempNode.NextVisibleNode;
                counter--;

                if (counter < 0)
                    break;

                if (tempNode != null)
                    currentNode = tempNode;
            }

            return currentNode;
        }

        protected override void DefWndProc(ref Message m)
        {
            MessageCodes msg = m.Msg;

            if (msg == MessageCodes.WM_VSCROLL)
            {
                this.OnScrolled((Direction)m.WParam.ToInt32());
            }
         
            base.DefWndProc(ref m);
        }

        protected void OnScrolled(Direction direction)
        {
            EventHandler<ScrollNotifyTreeViewEventArgs> handler = Scrolled;

            if (handler != null)
                handler(this, new ScrollNotifyTreeViewEventArgs(direction));
        }

        protected override void CreateHandle()
        {
            base.CreateHandle();
            //// Explorer Theme
            uxtheme.SetWindowTheme(this.Handle, "explorer", null);
        }

        protected override void OnDrawNode(DrawTreeNodeEventArgs e)
        {
            TreeNodeStates treeState = e.State;   

            if (e.Node == e.Node.TreeView.SelectedNode || (e.State & TreeNodeStates.Hot) == TreeNodeStates.Hot)
            {
                Font font = e.Node.NodeFont ?? e.Node.TreeView.Font;
                Rectangle rect = e.Bounds;
                rect.Offset(0, 1);
                Brush brush = SystemBrushes.Highlight; // e.Node.TreeView.Focused ? SystemBrushes.Highlight : Brushes.Gray;
                e.Graphics.FillRectangle(brush, e.Bounds);
                TextRenderer.DrawText(e.Graphics, e.Node.Text, font, rect, SystemColors.HighlightText, TextFormatFlags.GlyphOverhangPadding);
            }
            else
            {
                e.DrawDefault = true;
            }
        }
    }

    public class ScrollNotifyTreeViewEventArgs : EventArgs
    {
        public ScrollNotifyTreeViewEventArgs(Direction direction)
        {
            this.Direction = direction;
        }

        public Direction Direction { get; private set; }
    }

}
