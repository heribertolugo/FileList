using System;
using System.Drawing;
using System.Windows.Forms;
using Win32.Constants;
using Win32.Libraries;
using Win32.Models;

namespace Common.Models
{
    public partial class DropForm : Form
    {
        private bool isAeroEnabled;
        private Control _ownerControl;

        public DropForm()
        {
            InitializeComponent();
            this.ShowInTaskbar = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
        }
        public virtual void Show(Control target, DropFormPosition position = DropFormPosition.BottomRight)
        {
            Rectangle screen = target.RectangleToScreen(target.ClientRectangle);
            Form form = target.FindForm();
            this._ownerControl = target;
            form.Move -= new EventHandler(this.Owner_Move);
            form.Move += new EventHandler(this.Owner_Move);
            form.Resize -= new EventHandler(this.TargetForm_Resize);
            form.Resize += new EventHandler(this.TargetForm_Resize);
            this.Location = this.GetNewPosition(position, target); // new Point(screen.X, screen.Bottom);
            this.TopMost = true;
            this.Show();
        }

        public Control TargetControl
        {
            get { return this._ownerControl; }
            private set { }
        }

        private Point GetNewPosition(DropFormPosition position, Control target)
        {
            Rectangle targetRect = target.RectangleToScreen(target.ClientRectangle);

            switch (position)
            {
                case DropFormPosition.BottomRight:
                    return new Point(targetRect.X, targetRect.Bottom);
                case DropFormPosition.BottomLeft:
                    return new Point(targetRect.X - this.Width + target.Width, targetRect.Bottom);
                case DropFormPosition.BottomMiddle:
                    return new Point(targetRect.X - (this.Width / 2) + target.Width, targetRect.Bottom);
                case DropFormPosition.LeftTop:
                    return new Point(targetRect.X - this.Width, targetRect.Bottom - this.Height);
                case DropFormPosition.LeftMiddle:
                    return new Point(targetRect.X - this.Width, targetRect.Top - (this.Height / 2));
                case DropFormPosition.LeftBottom:
                    return new Point(targetRect.X - this.Width, targetRect.Top);
                case DropFormPosition.RightTop:
                    return new Point(targetRect.X + targetRect.Width, targetRect.Bottom - this.Height);
                case DropFormPosition.RightMiddle:
                    return new Point(targetRect.X + targetRect.Width, targetRect.Top - (this.Height / 2));
                case DropFormPosition.RightBottom:
                    return new Point(targetRect.X + targetRect.Width, targetRect.Top);
                case DropFormPosition.TopLeft:
                    return new Point(targetRect.X - this.Width + target.Width, targetRect.Top - this.Height);
                case DropFormPosition.TopMiddle:
                    return new Point(targetRect.X - (this.Width / 2) + target.Width, targetRect.Top - this.Height);
                case DropFormPosition.TopRight:
                    return new Point(targetRect.X, targetRect.Top - this.Height);
                default:
                    break;
            }
            return new Point(targetRect.X, targetRect.Bottom);
        }

        private void Owner_Move(object sender, EventArgs e)
        {
            Rectangle screen = this._ownerControl.RectangleToScreen(this._ownerControl.ClientRectangle);
            this.Location = new Point(screen.X, screen.Bottom);
            this.TopMost = true;
        }
        private void TargetForm_Resize(object sender, EventArgs e)
        {
            Form form = sender as Form;
            switch (form.WindowState)
            {
                case FormWindowState.Normal:
                    this.WindowState = form.WindowState;
                    this.Show(this._ownerControl);
                    break;
                case FormWindowState.Minimized:
                    this.WindowState = form.WindowState;
                    break;
                case FormWindowState.Maximized:
                    this.WindowState = FormWindowState.Normal;
                    this.Show(this._ownerControl);
                    break;
                default:
                    this.WindowState = form.WindowState;
                    break;
            }
        }

        #region Win32 - drop shadow

        private bool CheckAeroEnabled()
        {
            if (Environment.OSVersion.Version.Major < 6)
                return false;
            int pfEnabled = 0;
            dwmapi.DwmIsCompositionEnabled(ref pfEnabled);
            return pfEnabled == HitTestMousePositionCodes.HTCLIENT;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                this.isAeroEnabled = this.CheckAeroEnabled();
                CreateParams createParams = base.CreateParams;
                if (!this.isAeroEnabled)
                    createParams.ClassStyle |= ClassStyles.CS_DROPSHADOW;
                return createParams;
            }
        }
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == MessageCodes.WM_NCPAINT && this.isAeroEnabled)
            {
                int attrValue = HitTestMousePositionCodes.HTCAPTION;
                dwmapi.DwmSetWindowAttribute(this.Handle, HitTestMousePositionCodes.HTCAPTION, ref attrValue, 4);
                MARGINS pMarInset = new MARGINS()
                {
                    bottomHeight = 1,
                    leftWidth = 1,
                    rightWidth = 1,
                    topHeight = 1
                };
                dwmapi.DwmExtendFrameIntoClientArea(this.Handle, ref pMarInset);
            }
            base.WndProc(ref m);
            if (m.Msg != MessageCodes.WM_NCHITTEST || (int)m.Result != HitTestMousePositionCodes.HTCLIENT)
                return;
            m.Result = (IntPtr) HitTestMousePositionCodes.HTCAPTION.Value;
        }
        #endregion
    }

    public enum DropFormPosition
    {
        BottomRight,
        BottomLeft,
        BottomMiddle,
        LeftTop,
        LeftMiddle,
        LeftBottom,
        RightTop,
        RightMiddle,
        RightBottom,
        TopLeft,
        TopMiddle,
        TopRight
    }
}
