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
        private static readonly string UiMessage = "You are about to delete {0} files/folders listed below. Are you sure?";
        public DeleteFilesDialog()
        {
            InitializeComponent();
            this.DialogResult = DialogResult.Abort;
        }

        private new void Show() { }

        public DialogResult ShowDialog(IEnumerable<string> paths)
        {
            foreach(string path in paths.ToArray())
                this.AddPathsToListBox(path);

            this.messageLabel.Text = string.Format(DeleteFilesDialog.UiMessage, this.filesListBox.Items.Count);
            this.StartPosition = FormStartPosition.CenterParent;
            this.ShowDialog();
            return this.Result;
        }

        private void AddPathsToListBox(string path)
        {
            this.filesListBox.Items.Add(path);
            
            if (System.IO.Directory.Exists(path))
            {
                foreach (string file in System.IO.Directory.EnumerateFiles(path))
                    this.filesListBox.Items.Add(file);
                foreach (string directory in System.IO.Directory.EnumerateDirectories(path))
                    AddPathsToListBox(directory);
            }
        }

        private DialogResult Result { get; set; }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.Result = DialogResult.OK;
            this.DeletePaths(this.filesListBox.Items.Cast<string>());
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

        private void DeleteFilesDialog_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose(true);
        }

        private void DeletePaths(IEnumerable<string> paths)
        {
            foreach (string path in paths)
            {
                if (System.IO.Directory.Exists(path))
                    System.IO.Directory.Delete(path, true);
                if (System.IO.File.Exists(path))
                    System.IO.File.Delete(path);
            }
        }
    }
}
