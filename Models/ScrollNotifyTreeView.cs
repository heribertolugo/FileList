using FileList.Models.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace FileList.Models
{
    public class ScrollNotifyTreeView : System.Windows.Forms.TreeView
    {
        public event EventHandler<ScrollNotifyTreeViewEventArgs> Scrolled;

        public ScrollNotifyTreeView() : base()
        {
            //this.Scrollable
        }

        public new bool Scrollable
        {
            get;set;
        }

        public bool VerticalScroll
        {
            get;set;
        }

        public bool HorizontalScroll
        {
            get;set;
        }

        protected override void DefWndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case Win32Messages.WM_VSCROLL:
                    this.OnScrolled((Direction)m.WParam.ToInt32());
                    break;
                default:
                    break;
            }
            base.DefWndProc(ref m);
        }

        public void SetScrollInfo()
        {

        }

        protected void OnScrolled(Direction direction)
        {
            EventHandler<ScrollNotifyTreeViewEventArgs> handler = Scrolled;

            if (handler != null)
                handler(this, new ScrollNotifyTreeViewEventArgs(direction));
        }

        #region Explorer Theme
        [DllImport("uxtheme.dll", CharSet = CharSet.Unicode)]
        private extern static int SetWindowTheme(IntPtr hWnd, string pszSubAppName,
                                            string pszSubIdList);

        protected override void CreateHandle()
        {
            base.CreateHandle();
            SetWindowTheme(this.Handle, "explorer", null);
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

    public struct SCROLLINFO
    {
        uint cbSize;
        uint fMask;
        int nMin;
        int nMax;
        uint nPage;
        int nPos;
        int nTrackPos;
    }
    
}
