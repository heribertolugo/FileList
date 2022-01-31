using Common.Extensions;
using Common.Models;
using FileList.Logic;
using FileList.Models;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

namespace FileList.Views
{
    public partial class MainForm : Form
    {
        private Common.Models.Controls.FolderBrowserDialog dialog;
        private FilePreview.Previewers previewers = null;
        private Views.SearchOptionsForm searchOptionsForm = null;
        private SearchOption searchOptions;
        public MainForm()
        {
            InitializeComponent();
            if (Common.Helpers.IoHelper.IsAdministrator())
                this.Text = string.Format("{0} - Administrator", this.Text);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Height = 100;
            this.Width = 730;
            this.dialog = new Common.Models.Controls.FolderBrowserDialog(this);
            this.searchOptionsForm = new SearchOptionsForm();
            this.searchOptions = this.searchOptionsForm.SearchOption;// default(SearchOption);

            UiHelper.InitiializeFilePreviewers();
        }
        private void BrowseButton_Click(object sender, EventArgs e)
        {
            if (this.dialog.ShowDialog() != DialogResult.OK)
                return;
            this.rootPathTextBox.Text = this.dialog.DirectoryPath;
            this.Reset();
            if (!this.splitContainer1.Visible)
                return;

            this.ToggleEnabled(null);
            this.Search();
        }

        private void Search()
        {
            UiHelper.Search(this.rootPathTextBox.Text, this.fileListControl1, this.ToggleEnabled, this.searchOptions);
        }

        public void Reset()
        {
            this.viewerPanel.Controls.Clear();
            this.fileListControl1.Clear();
            this.filePropertiesTextBox.Text = string.Empty;
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.rootPathTextBox.Text) || this.rootPathTextBox.Text.Equals(string.Empty))
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
                    if (this.searchOptionsForm.Visible)
                        this.searchOptionsForm.Hide();
                }

                this.ToggleEnabled(null);
                this.Reset();
                this.Search();
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            UiHelper.CancelSearch();
        }

        public void ToggleEnabled(ConcurrentFileSearchEventArgs fileSearchEventArgs)
        {
            this.InvokeIfRequired(c => { 
                Control control = (Control)c;
                foreach (Control control1 in (ArrangedElementCollection)control.Controls)
                    control1.Enabled = !control1.Enabled;

                if (this.searchButton.Text.ToUpper().Equals("SEARCH"))
                {
                    this.searchButton.Text = "Cancel";
                    this.searchButton.Click -= SearchButton_Click;
                    this.searchButton.Click += CancelButton_Click;
                }
                else
                {
                    this.searchButton.Text = "Search";
                    this.searchButton.Click -= CancelButton_Click;
                    this.searchButton.Click += SearchButton_Click;
                }
            });
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
            Control button = sender as Control;
            bool? menuVisible = button.Tag as bool?;
            //UiHelper.DeleteItem(this.fileListControl1.SelectedPath, this.fileListControl1);
            if (!menuVisible.HasValue || !menuVisible.Value)
            {
                this.deleteFileContextMenu.Show(button, new System.Drawing.Point(0, button.Height));
                button.Tag = true;
            }else
                button.Tag = false;
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
                this.browsePanel.EnabledChanged -= this.browsePanel_EnabledChanged;
                this.Enabled = false;
                moveFilesForm.FormClosed += (FormClosedEventHandler)((s, e2) => 
                    { 
                        this.Enabled = true;
                        this.browsePanel.EnabledChanged += this.browsePanel_EnabledChanged; 
                        (s as Form).Dispose(); 
                    }); 
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

            if (previewers == null)
                previewers = new FilePreview.Previewers();

            UiHelper.DisplayPreview(!e.IsRootPath ? e.FileData[0] : new FileData(selectedPath), previewers, this.viewerPanel);

            if (!e.IsRootPath)
            {
                this.filePropertiesTextBox.Text = string.Join(Environment.NewLine, e.FileData[0].ExtendedProperties.Select(k => string.Format("{0}: {1}", k.Key, k.Value)).ToArray());
            }
            else
            {
                string[] directories = Directory.Exists(selectedPath) ? Directory.GetDirectories(selectedPath).Select(d => {
                    //Path.GetDirectoryName(d)
                    int lastDirSeparator = d.TrimEnd('\\').LastIndexOf('\\');
                    return d.Substring(lastDirSeparator+1);
                    }).ToArray() : new string[0];
                FileDataGroup groupFromSelected = this.fileListControl1.GetFileDataGroupFromSelected();
                this.filePropertiesTextBox.Text = string.Format("Path:{3}{0}Children: {1}{0}{2}{0}{0}Directories:{4}{0}{5}"
                    , Environment.NewLine
                    , groupFromSelected.FileData.Count()
                    , string.Join
                        (Environment.NewLine
                        , groupFromSelected.FileData.Select(f => f.Name + f.Extension).ToArray()
                        )
                    , selectedPath, directories.Length, string.Join(Environment.NewLine, directories));
            }
        }

        private void FileListControl1_OnOpenFileDataClicked(object sender, FileDataSelectedEventArgs e)
        {
            UiHelper.OpenItem(this.fileListControl1.SelectedPath);
        }

        private void FileListControl1_OnOpenLocationClicked(object sender, FileDataSelectedEventArgs e)
        {
            UiHelper.OpenLocation(this.fileListControl1.SelectedPath);
        }

        private void FileListControl1_OnDeleteFileDataClicked(object sender, FileDataSelectedEventArgs e)
        {
            UiHelper.DeleteItems(e.FileData.Select(f => f.Path), this.fileListControl1);
        }

        private void browsePanel_EnabledChanged(object sender, EventArgs e)
        {
            Panel panel = sender as Panel;

            panel.EnabledChanged -= browsePanel_EnabledChanged;
            panel.Enabled = true;

            foreach (Control control in panel.Controls)
                control.Enabled = !control.Enabled;

            panel.Controls["searchButton"].Enabled = true;
            panel.EnabledChanged += browsePanel_EnabledChanged;
        }

        private void deleteSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.deleteButton.Tag = false;
            UiHelper.DeleteSelected(this.fileListControl1);
        }

        private void deleteCheckedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.deleteButton.Tag = false;
            UiHelper.DeleteChecked(this.fileListControl1);
        }
        
        private void ProcessSearchOptionForm()
        {
            this.fileListControl1.scoutCountLabel.Value = this.searchOptionsForm.Threads;
            this.searchOptions = this.searchOptionsForm.SearchOption;
            this.searchOptionsForm.Hide();
        }

        private void searchOptionsButton_Click(object sender, EventArgs e)
        {
            this.BeginSearchOptionsFormListening = false;

            if (this.searchOptionsForm.Visible)
            {
                this.ProcessSearchOptionForm();
            }
            else
            {
                Win32.MouseListenerLL.MouseAction += new EventHandler<Win32.MouseListenerEventArgs>(SearchOptionsFormlistener);
                Win32.MouseListenerLL.Start();
                this.searchOptionsForm.Show(sender as Control, DropFormPosition.BottomLeft);
            }
        }

        private bool BeginSearchOptionsFormListening;
        private void SearchOptionsFormlistener(object sender, Win32.MouseListenerEventArgs e)
        {
            // if we have already hovered the form with mouse, close form as soon as mouse leaves
            // or if we click anywhere outside form, close form
            if ((this.BeginSearchOptionsFormListening && !this.searchOptionsForm.RectangleToScreen(this.searchOptionsForm.DisplayRectangle).Contains(e.Location))
                || (e.ButtonClick != Win32.MouseButtonClick.None && !this.searchOptionsForm.RectangleToScreen(this.searchOptionsForm.DisplayRectangle).Contains(e.Location)))
            {
                Win32.MouseListenerLL.Stop();
                this.ProcessSearchOptionForm();
            }
            // when we enter the form, allow form closing when mouse leaves form
            else if (this.searchOptionsForm.RectangleToScreen(this.searchOptionsForm.DisplayRectangle).Contains(e.Location))
            {
                this.BeginSearchOptionsFormListening = true;
            }
        }
    }
}
