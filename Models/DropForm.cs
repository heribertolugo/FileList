using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace FileList.Models
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
        public virtual void Show(Control target)
        {
            Rectangle screen = target.RectangleToScreen(target.ClientRectangle);
            Form form = target.FindForm();
            this._ownerControl = target;
            form.Move -= new EventHandler(this.Owner_Move);
            form.Move += new EventHandler(this.Owner_Move);
            form.Resize -= new EventHandler(this.TargetForm_Resize);
            form.Resize += new EventHandler(this.TargetForm_Resize);
            this.Location = new Point(screen.X, screen.Bottom);
            this.TopMost = true;
            this.Show();
        }

        public Control TargetControl
        {
            get { return this._ownerControl; }
            private set { }
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
            Win32.Win32Methods.DwmIsCompositionEnabled(ref pfEnabled);
            return pfEnabled == Win32.Win32Enums.HTCLIENT;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                this.isAeroEnabled = this.CheckAeroEnabled();
                CreateParams createParams = base.CreateParams;
                if (!this.isAeroEnabled)
                    createParams.ClassStyle |= Win32.Win32Enums.CS_DROPSHADOW;
                return createParams;
            }
        }
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == Win32.Win32Enums.WM_NCPAINT && this.isAeroEnabled)
            {
                int attrValue = Win32.Win32Enums.HTCAPTION;
                Win32.Win32Methods.DwmSetWindowAttribute(this.Handle, Win32.Win32Enums.HTCAPTION, ref attrValue, 4);
                Win32MARGINS pMarInset = new Win32MARGINS()
                {
                    bottomHeight = 1,
                    leftWidth = 1,
                    rightWidth = 1,
                    topHeight = 1
                };
                Win32.Win32Methods.DwmExtendFrameIntoClientArea(this.Handle, ref pMarInset);
            }
            base.WndProc(ref m);
            if (m.Msg != Win32.Win32Enums.WM_NCHITTEST || (int)m.Result != Win32.Win32Enums.HTCLIENT)
                return;
            m.Result = (IntPtr) Win32.Win32Enums.HTCAPTION;
        }
        #endregion
    }
}
