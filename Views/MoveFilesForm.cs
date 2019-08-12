using FileList.Logic;
using FileList.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

namespace FileList.Views
{
    public partial class MoveFilesForm : Form
    {
        private string[] FilePaths;

        public MoveFilesForm()
        {
            InitializeComponent();
        }

        public MoveFilesForm(string[] filePaths)
      : this()
        {
            this.itemsToMoveCountLabel.Text = this.BuildTreeByDirectoryStructure(filePaths).ToString();
            this.FilePaths = filePaths;
            this.backgroundWorker1.DoWork += new DoWorkEventHandler(this.BackgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.BackgroundWorker1_RunWorkerCompleted);
        }

        private void BackgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            IEnumerable<Exception> result = e.Result as IEnumerable<Exception>;
            if (result.Count() > 0)
                Notepad.ShowMessage(string.Join(string.Format("{0}{0}", Environment.NewLine), result.Select(x => string.Format("{0}{1}{2}", x.Message, Environment.NewLine, x.InnerException == null ? string.Empty : x.InnerException.Message)).ToArray()), "Exceptions");
            this.ToggleUiEnabled();
        }

        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Mover mover = e.Argument as Mover;
            mover.Start();
            e.Result = mover.Exceptions;
        }

        private int BuildTreeByDirectoryStructure(string[] paths)
        {
            int totalItemsInTree = 0;
            this.treeView1.Nodes.Clear();
            foreach (string path in paths)
            {
                string[] filesAndDirectories = path.Split(new char[1] { '\\' });
                StringBuilder stringBuilder = new StringBuilder();
                TreeNode treeNode = null;
                foreach (string fileOrDirectory in filesAndDirectories)
                {
                    if (treeNode == null)
                    {
                        if (!this.treeView1.Nodes.ContainsKey(fileOrDirectory))
                        {
                            this.treeView1.Nodes.Add(fileOrDirectory, fileOrDirectory);
                            ++totalItemsInTree;
                        }
                        treeNode = this.treeView1.Nodes[fileOrDirectory];
                    }
                    else
                    {
                        if (!treeNode.Nodes.ContainsKey(fileOrDirectory))
                        {
                            treeNode.Nodes.Add(fileOrDirectory, fileOrDirectory);
                            ++totalItemsInTree;
                        }
                        treeNode = treeNode.Nodes[fileOrDirectory];
                    }
                }
            }
            this.treeView1.ExpandAll();
            return totalItemsInTree;
        }

        private int BuildTreeByFiles(string[] paths)
        {
            int totalItemsInTree = 0;
            this.treeView1.Nodes.Clear();
            foreach (string path in paths)
            {
                if (File.Exists(path))
                {
                    this.treeView1.Nodes.Add(path, path);
                    ++totalItemsInTree;
                }
            }
            this.treeView1.ExpandAll();
            return totalItemsInTree;
        }

        private void MoveFilesForm_Load(object sender, EventArgs e)
        {
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.retainDirectoryCheckBox.Checked)
                this.itemsToMoveCountLabel.Text = this.BuildTreeByDirectoryStructure(this.FilePaths).ToString();
            else
                this.itemsToMoveCountLabel.Text = this.BuildTreeByFiles(this.FilePaths).ToString();
        }

        private void BrowseButton_Click(object sender, EventArgs e)
        {
            if (this.saveFileDialog1.ShowDialog() != DialogResult.OK)
                return;
            this.destinationTextBox.Text = this.saveFileDialog1.FileName.Replace(".Folder", "");
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            if (this.backgroundWorker1.IsBusy)
                return;
            this.backgroundWorker1.CancelAsync();
        }

        private void MoveFilesButton_Click(object sender, EventArgs e)
        {
            this.ToggleUiEnabled();
            this.backgroundWorker1.RunWorkerAsync(new Mover(this.FilePaths, this.destinationTextBox.Text, this.retainDirectoryCheckBox.Checked, this.copyItemsCheckBox.Checked, this.overwriteCheckBox.Checked, this.progressInfoControl1, int.Parse(this.maxErrorsTextBox.Text)));
        }

        private void ToggleUiEnabled()
        {
            foreach (Control control in this.Controls)
                control.Enabled = !control.Enabled;
            this.progressInfoControl1.Enabled = true;
        }

        private void DestinationTextBox_TextChanged(object sender, EventArgs e)
        {
            this.moveFilesButton.Enabled = Directory.Exists(this.destinationTextBox.Text);
        }

        private void OverwriteCheckBox_CheckedChanged(object sender, EventArgs e)
        {
        }
    }
}
