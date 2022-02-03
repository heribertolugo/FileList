using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Win32.Constants;
using Win32.Libraries;
using Common.Extensions;

namespace FileList.Models
{
    public class ScrollNotifyTreeView : System.Windows.Forms.TreeView
    {
        public event EventHandler<ScrollNotifyTreeViewEventArgs> Scrolled;
        public event EventHandler<NeedToolTipEventArgs> NeedToolTip;

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

        #region Overrides
        protected override void DefWndProc(ref Message m)
        {
            MessageCodes msg = m.Msg;

            if (msg == MessageCodes.WM_VSCROLL)
            {
                this.OnScrolled((Direction)m.WParam.ToInt32());
            }

            base.DefWndProc(ref m);
        }

        ///// <summary>
        ///// Could not get this to work. the tooltip only displays first line of text unless a breakpoint is used to pause execution
        ///// </summary>
        //protected override void WndProc(ref Message m)
        //{
        //    if (m.Msg == Win32.Constants.MessageCodes.WM_NOTIFY)
        //    {
        //        MessageCodes msg = m.Msg;
        //        NMHDR hdr = (NMHDR)m.GetLParam(typeof(NMHDR));
        //        int TTM_SETMAXTIPWIDTH = (Win32.Constants.MessageCodes.WM_USER + 24);
        //        int TTM_ADDTOOLA = (Win32.Constants.MessageCodes.WM_USER + 4);
        //        int TTM_ADDTOOLW = (Win32.Constants.MessageCodes.WM_USER + 50);
        //        int TTN_FIRST = -520;
        //        int TTN_GETDISPINFOA = TTN_FIRST - 0;
        //        int TTN_GETDISPINFOW = TTN_FIRST - 10;
        //        int TTN_GETDISPINFO = -530;
        //        if (hdr.code == -530)
        //        {
        //            Point pos = this.PointToClient(Control.MousePosition);
        //            TreeNode node = this.GetNodeAt(pos);
        //            if (node != null && NeedToolTip != null)
        //            {
        //                NeedToolTipEventArgs e = new NeedToolTipEventArgs(node);
        //                NeedToolTip(this, e);
        //                //TOOLINFO tt = (TOOLINFO)m.GetLParam(typeof(TOOLINFO));
        //                TOOLTIPTEXT tt = (TOOLTIPTEXT)m.GetLParam(typeof(TOOLTIPTEXT));

        //                tt.hinst = IntPtr.Zero;
        //                tt.lpszText = e.ToolTipText;
        //                System.Threading.Thread.Sleep(200);
        //                Marshal.StructureToPtr(tt, m.LParam, false);
        //                m.Result = (IntPtr)1;
        //                return;
        //            }
        //        }
        //    }
        //    base.WndProc(ref m);
        //}

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

        protected override void OnHandleCreated(EventArgs e)
        {
            user32.SendMessage(this.Handle,
                (uint)TreeViewMessages.TVM_SETEXTENDEDSTYLE,
                (int)TreeViewExtendedStyles.TVS_EX_DOUBLEBUFFER,
                (IntPtr)TreeViewExtendedStyles.TVS_EX_DOUBLEBUFFER);
            base.OnHandleCreated(e);
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
        #endregion

        #region Win32
        [StructLayout(LayoutKind.Sequential)]
        public struct NMHDR
        {
            public IntPtr hwndFrom;
            public IntPtr idFrom;
            public int code;
        }
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class TOOLTIPTEXT
        {
            public NMHDR hdr;
            public string lpszText;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szText;
            public IntPtr hinst;
            public int uFlags;
        }

        struct TOOLINFO
        {
            public int cbSize;
            public int uFlags;
            public IntPtr hwnd;
            public IntPtr uId;
            public Win32.RECT rect;
            public IntPtr hinst;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string lpszText;
            public IntPtr lParam;
        }

        struct NMTTDISPINFO
        {
            public NMHDR hdr;
            public string lpszText;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szText;
            public IntPtr hinst;
            public uint uFlags;
            public Int64 lParam;
        }
        #endregion
    }

    public class ScrollNotifyTreeViewEventArgs : EventArgs
    {
        public ScrollNotifyTreeViewEventArgs(Direction direction)
        {
            this.Direction = direction;
        }

        public Direction Direction { get; private set; }
    }

    public class NeedToolTipEventArgs : EventArgs
    {
        private TreeNode _node;
        private string _text;
        internal NeedToolTipEventArgs(TreeNode node)
        {
            this._node = node;
            this._text = "";
        }
        public TreeNode Node
        {
            get { return this._node; }
        }
        public string ToolTipText
        {
            get { return this._text; }
            set
            {
                //if (value.Length >= 80) //throw new ArgumentException("Tooltip text too long");
                //    value = value.Substring(0, 80);
                this._text = value;
            }
        }
    }
}
