using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Win32.Constants;
using Win32.Libraries;
using Win32.Models;

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
            dwmapi.DwmIsCompositionEnabled(ref pfEnabled);
            return pfEnabled == MousePositionCodes.HTCLIENT;
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
                int attrValue = MousePositionCodes.HTCAPTION;
                dwmapi.DwmSetWindowAttribute(this.Handle, MousePositionCodes.HTCAPTION, ref attrValue, 4);
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
            if (m.Msg != MessageCodes.WM_NCHITTEST || (int)m.Result != MousePositionCodes.HTCLIENT)
                return;
            m.Result = (IntPtr) MousePositionCodes.HTCAPTION;
        }
        #endregion
    }
}
