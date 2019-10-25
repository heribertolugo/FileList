using System;
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
            switch (m.Msg)
            {
                case MessageCodes.WM_VSCROLL:
                    this.OnScrolled((Direction)m.WParam.ToInt32());
                    break;
                default:
                    break;
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
            // Explorer Theme
            uxtheme.SetWindowTheme(this.Handle, "explorer", null);
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
