using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FileList.Views
{
    public partial class DeleteFilesDialog : Form
    {
        private static readonly string UiMessage = "";
        public DeleteFilesDialog()
        {
            InitializeComponent();
            this.DialogResult = DialogResult.Abort;
        }

        public DialogResult ShowDialog(IEnumerable<string> paths)
        {

            this.ShowDialog();
            return this.Result;
        }

        private DialogResult Result { get; set; }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.Result = DialogResult.OK;
            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Result = DialogResult.Cancel;
            this.Close();
        }

        private void iconPanel_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(SystemIcons.Warning.ToBitmap(), (sender as Panel).DisplayRectangle);
        }
    }
}
