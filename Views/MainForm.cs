//using FileList.Icons;
using FileList.Logic;
using FileList.Models;
using FileList.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

namespace FileList.Views
{
    public partial class MainForm : Form
    {
        private CheckBox previousButton = null;

        public MainForm()
        {
            InitializeComponent();
            this.backgroundWorker1.DoWork += new DoWorkEventHandler(this.BackgroundWorker1_DoWork);
            this.CreateImageLayoutButtons();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Height = 100;
            this.Width = 730;
            this.HideTabPageHeaders(this.viewerTabControl);
            this.textViewerTextBox.BackColor = Color.Black;
            this.textViewerTextBox.ForeColor = Color.Black;
            this.textViewerTextBox.BackColor = Color.White;
        }

        private void CreateImageLayoutButtons()
        {
            TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
            int column = 0;
            tableLayoutPanel.Dock = DockStyle.Fill;
            tableLayoutPanel.RowCount = 1;
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));

            foreach (ImageLayout imageLayout in Enum.GetValues(typeof(ImageLayout)))
            {
                CheckBox layoutButton = new CheckBox();
                layoutButton.Text = Enum.GetName(typeof(ImageLayout), imageLayout);
                layoutButton.Dock = DockStyle.Fill;
                layoutButton.FlatStyle = FlatStyle.Flat;
                layoutButton.Tag = imageLayout;
                layoutButton.Appearance = Appearance.Button;
                layoutButton.TextAlign = ContentAlignment.MiddleCenter;
                layoutButton.FlatAppearance.CheckedBackColor = Color.FromKnownColor(KnownColor.DarkGray);
                layoutButton.Name = this.Name = string.Format("{0}Button", layoutButton.Text);
                layoutButton.Click += new EventHandler(this.ImageLayoutButton_Click);
                ++tableLayoutPanel.ColumnCount;
                tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
                tableLayoutPanel.Controls.Add(layoutButton, column, 0);
                ++column;
            }
            this.imageDisplayStylePanel.Controls.Add(tableLayoutPanel);

            for (int index = 0; index < tableLayoutPanel.ColumnStyles.Count; ++index)
                tableLayoutPanel.ColumnStyles[index] = new ColumnStyle(SizeType.Percent, 100f / (float)column);
        }

        private void ImageLayoutButton_Click(object sender, EventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox == this.previousButton)
            {
                checkBox.Checked = !this.previousButton.Checked;
            }
            else
            {
                if (this.previousButton != null)
                    this.previousButton.CheckState = CheckState.Unchecked;
                if (checkBox == null)
                    return;
                this.previousButton = checkBox;
                this.imageViewerPanel.BackgroundImageLayout = (ImageLayout)checkBox.Tag;
            }
        }

        private void HideTabPageHeaders(TabControl tabControl)
        {
            tabControl.Appearance = TabAppearance.FlatButtons;
            tabControl.ItemSize = new Size(0, 1);
            tabControl.SizeMode = TabSizeMode.Fixed;
        }

        private void BrowseButton_Click(object sender, EventArgs e)
        {
            if (this.saveFileDialog1.ShowDialog() != DialogResult.OK)
                return;
            this.rootPathTextBox.Text = Path.GetDirectoryName(this.saveFileDialog1.FileName.Replace("Folder.", ""));
            this.Reset();
            if (!this.splitContainer1.Visible)
                return;
            this.Search();
        }

        private void Search()
        {
            UiHelper.Search(this.rootPathTextBox.Text, this.fileListControl1);
        }

        public void Reset()
        {
            UiHelper.ResetPreviews(this.viewerTabControl, this.contentsListView, (Control)this.imageViewerPanel, this.textViewerTextBox, this.documentTabPage, this.contentTabPage, this.imageTabPage, this.previousButton == null ? ImageLayout.None : (ImageLayout)this.previousButton.Tag);
            this.fileListControl1.Clear();
            this.filePropertiesTextBox.Text = string.Empty;
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.rootPathTextBox.Text) || this.rootPathTextBox.Text.Equals(""))
                return;
            if (!Directory.Exists(this.rootPathTextBox.Text) && !File.Exists(this.rootPathTextBox.Text))
            {
                MessageBox.Show("Wouldnt it be nice to go to places that dont exist?", UiHelper.ErrorHeader, MessageBoxButtons.OK, MessageBoxIcon.Question);
                this.searchButton.Enabled = false;
            }
            else
            {
                if (!this.splitContainer1.Visible)
                {
                    this.splitContainer1.Visible = true;
                    this.Height = 829;
                    this.Width = 1350;
                }
                //ConcurrentFileSearch search = new ConcurrentFileSearch(this.rootPathTextBox.Text);
                //search.OnFinishedHandler += Search_OnFinishedHandler;
                //search.Start();
                //return;

                this.ToggleEnabled((Control)this);
                this.Reset();
                this.Search();
                this.ToggleEnabled((Control)this);
            }
        }

        private void Search_OnFinishedHandler(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public void ToggleEnabled(Control control)
        {
            foreach (Control control1 in (ArrangedElementCollection)control.Controls)
                control1.Enabled = !control1.Enabled;
        }

        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            FileSearch search = e.Argument as FileSearch;
            if (search == null)
                return;
            FileData? fileData = null;
            UiHelper.MoveNextFileDataFromMta(search);
            FileData? current;
            while ((current = search.Current).HasValue)
            {
                this.treeIconsImageList.Images.Add(current.Value.Extension, FileToIconConverter.GetFileIcon(current.Value.Extension, FileToIconConverter.IconSize.Small)); //IconManager.FindIconForFilename(current.Value.Extension, false));
                UiHelper.MoveNextFileDataFromMta(search);
            }
        }

        public void SmartImageLayout(Control control, Control buttonContainer)
        {
            if (control.BackgroundImage == null)
                return;
            ImageLayout imageLayout = control.BackgroundImage.Width <= control.Width && control.BackgroundImage.Height <= control.Height ? (control.BackgroundImage.Width > control.Width / 4 && control.BackgroundImage.Height >= control.Height / 4 ? ImageLayout.Zoom : ImageLayout.Center) : ImageLayout.Zoom;
            control.BackgroundImageLayout = imageLayout;
            CheckBox checkBox = ((IEnumerable<Control>)buttonContainer.Controls[0].Controls.Find(string.Format("{0}Button", (object)Enum.GetName(typeof(ImageLayout), (object)imageLayout)), true)).FirstOrDefault<Control>() as CheckBox;
            if (this.previousButton != null)
                this.previousButton.CheckState = CheckState.Unchecked;
            checkBox.CheckState = CheckState.Checked;
            this.previousButton = checkBox;
        }

        private void OpenFileButton_Click(object sender, EventArgs e)
        {
            UiHelper.OpenItem(this.fileListControl1.SelectedPath);
        }

        private void OpenLocationButton_Click(object sender, EventArgs e)
        {
            UiHelper.OpenLocation(this.fileListControl1.SelectedPath);
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            UiHelper.DeleteItem(this.rootPathTextBox.Text, this.fileListControl1);
        }

        private void MoveSelectedButton_Click(object sender, EventArgs e)
        {
            string[] checkedPaths = this.fileListControl1.GetCheckedPaths();
            if (checkedPaths.Length < 1)
            {
                int num = (int)MessageBox.Show("No files selected", UiHelper.ErrorHeader, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                MoveFilesForm moveFilesForm = new MoveFilesForm(checkedPaths);
                this.Enabled = false;
                moveFilesForm.FormClosed += (FormClosedEventHandler)((s, e2) => this.Enabled = true);
                moveFilesForm.Show();
            }
        }

        private void RootPathTextBox_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox == null)
                return;
            this.searchButton.Enabled = !string.IsNullOrEmpty(textBox.Text) && !textBox.Text.Equals(string.Empty);
        }

        private void FileListControl1_OnFileDataSelected(object sender, FileDataSelectedEventArgs e)
        {
            string selectedPath = this.fileListControl1.SelectedPath;
            UiHelper.DisplayPreview(!e.IsRootPath ? e.FileData[0] : new FileData(selectedPath), this.viewerTabControl, this.contentsListView, (Control)this.imageViewerPanel, this.textViewerTextBox, this.documentTabPage, this.contentTabPage, this.imageTabPage, this.previousButton == null ? ImageLayout.None : (ImageLayout)this.previousButton.Tag);
            if (!e.IsRootPath)
            {
                this.filePropertiesTextBox.Text = string.Join(Environment.NewLine, e.FileData[0].ExtendedProperties.Select<KeyValuePair<string, string>, string>((Func<KeyValuePair<string, string>, string>)(k => string.Format("{0}: {1}", (object)k.Key, (object)k.Value))).ToArray<string>());
            }
            else
            {
                string[] strArray = Directory.Exists(selectedPath) ? ((IEnumerable<string>)Directory.GetDirectories(selectedPath)).Select<string, string>((Func<string, string>)(d => Path.GetDirectoryName(d))).ToArray<string>() : new string[0];
                FileDataGroup groupFromSelected = this.fileListControl1.GetFileDataGroupFromSelected();
                this.filePropertiesTextBox.Text = string.Format("Path:{3}{0}Children: {1}{0}{2}{0}{0}Directories:{4}{0}{5}", (object)Environment.NewLine, (object)((IEnumerable<FileData>)groupFromSelected.FileData).Count<FileData>(), (object)string.Join(Environment.NewLine, ((IEnumerable<FileData>)groupFromSelected.FileData).Select<FileData, string>((Func<FileData, string>)(f => f.Name + f.Extension)).ToArray<string>()), (object)selectedPath, (object)strArray.Length, (object)string.Join(Environment.NewLine, strArray));
            }
            this.SmartImageLayout((Control)this.imageViewerPanel, (Control)this.imageDisplayStylePanel);
        }

        private void FileListControl1_OnOpenFileDataClicked(object sender, FileDataSelectedEventArgs e)
        {
            UiHelper.OpenItem(this.fileListControl1.SelectedPath);
        }

        private void FileListControl1_OnOpenLocationClicked(object sender, FileDataSelectedEventArgs e)
        {
            UiHelper.OpenLocation(this.fileListControl1.SelectedPath);
        }

        private void FileListControl1_OnDeleteFileDataClicked(
          object sender,
          FileDataSelectedEventArgs e)
        {
            UiHelper.DeleteItem(this.fileListControl1.SelectedPath, this.fileListControl1);
        }

    }
}
