using FileList.Models;

namespace FileList.Views
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.rootPathTextBox = new System.Windows.Forms.TextBox();
            this.browsePanel = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.browseButton = new System.Windows.Forms.Button();
            this.searchButton = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.treeIconsImageList = new System.Windows.Forms.ImageList(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.moveSelectedButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.openLocationButton = new System.Windows.Forms.Button();
            this.openFileButton = new System.Windows.Forms.Button();
            this.splitContainer1 = new Common.Models.GripSplitContainer();
            this.fileListControl1 = new FileList.Models.FileListControl();
            this.viewerPanel = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.filePropertiesTextBox = new System.Windows.Forms.TextBox();
            this.deleteFileContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteSelectedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteCheckedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.browsePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.deleteFileContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // rootPathTextBox
            // 
            this.rootPathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rootPathTextBox.Location = new System.Drawing.Point(12, 33);
            this.rootPathTextBox.Name = "rootPathTextBox";
            this.rootPathTextBox.Size = new System.Drawing.Size(1222, 20);
            this.rootPathTextBox.TabIndex = 0;
            this.rootPathTextBox.TextChanged += new System.EventHandler(this.RootPathTextBox_TextChanged);
            // 
            // browsePanel
            // 
            this.browsePanel.Controls.Add(this.label2);
            this.browsePanel.Controls.Add(this.browseButton);
            this.browsePanel.Controls.Add(this.searchButton);
            this.browsePanel.Controls.Add(this.rootPathTextBox);
            this.browsePanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.browsePanel.Location = new System.Drawing.Point(0, 0);
            this.browsePanel.Name = "browsePanel";
            this.browsePanel.Size = new System.Drawing.Size(1410, 56);
            this.browsePanel.TabIndex = 1;
            this.browsePanel.EnabledChanged += new System.EventHandler(this.browsePanel_EnabledChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(685, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Select a folder from which to copy or move files. Then click search to enumerate " +
    "the files within. Select desired files then click \"Move Selected\".";
            // 
            // browseButton
            // 
            this.browseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.browseButton.Location = new System.Drawing.Point(1240, 30);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(75, 23);
            this.browseButton.TabIndex = 2;
            this.browseButton.Text = "Browse";
            this.toolTip1.SetToolTip(this.browseButton, "Brose for a folder cotaining the files you want to move/copy");
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.BrowseButton_Click);
            // 
            // searchButton
            // 
            this.searchButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.searchButton.Enabled = false;
            this.searchButton.Location = new System.Drawing.Point(1321, 30);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(75, 23);
            this.searchButton.TabIndex = 1;
            this.searchButton.Text = "Search";
            this.toolTip1.SetToolTip(this.searchButton, "Enumerates the files from the selected folder");
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // treeIconsImageList
            // 
            this.treeIconsImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.treeIconsImageList.ImageSize = new System.Drawing.Size(16, 16);
            this.treeIconsImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // moveSelectedButton
            // 
            this.moveSelectedButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.moveSelectedButton.BackColor = System.Drawing.Color.PaleTurquoise;
            this.moveSelectedButton.Location = new System.Drawing.Point(507, 35);
            this.moveSelectedButton.Name = "moveSelectedButton";
            this.moveSelectedButton.Size = new System.Drawing.Size(75, 39);
            this.moveSelectedButton.TabIndex = 6;
            this.moveSelectedButton.Text = "Move Selected";
            this.toolTip1.SetToolTip(this.moveSelectedButton, "Open the file mover/copier");
            this.moveSelectedButton.UseVisualStyleBackColor = false;
            this.moveSelectedButton.Click += new System.EventHandler(this.MoveSelectedButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.deleteButton.Location = new System.Drawing.Point(7, 79);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(75, 23);
            this.deleteButton.TabIndex = 4;
            this.deleteButton.Text = "Delete File";
            this.toolTip1.SetToolTip(this.deleteButton, "Delete the file selected in the tree");
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // openLocationButton
            // 
            this.openLocationButton.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.openLocationButton.Location = new System.Drawing.Point(7, 35);
            this.openLocationButton.Name = "openLocationButton";
            this.openLocationButton.Size = new System.Drawing.Size(75, 39);
            this.openLocationButton.TabIndex = 3;
            this.openLocationButton.Text = "Open File Location";
            this.toolTip1.SetToolTip(this.openLocationButton, "Open the location which contains the file selected in the tree");
            this.openLocationButton.UseVisualStyleBackColor = true;
            this.openLocationButton.Click += new System.EventHandler(this.OpenLocationButton_Click);
            // 
            // openFileButton
            // 
            this.openFileButton.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.openFileButton.Location = new System.Drawing.Point(7, 6);
            this.openFileButton.Name = "openFileButton";
            this.openFileButton.Size = new System.Drawing.Size(75, 23);
            this.openFileButton.TabIndex = 1;
            this.openFileButton.Text = "Open File";
            this.toolTip1.SetToolTip(this.openFileButton, "Opens the file selected in the tree");
            this.openFileButton.UseVisualStyleBackColor = true;
            this.openFileButton.Click += new System.EventHandler(this.OpenFileButton_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 56);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.fileListControl1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.viewerPanel);
            this.splitContainer1.Panel2.Controls.Add(this.panel2);
            this.splitContainer1.Size = new System.Drawing.Size(1410, 734);
            this.splitContainer1.SplitterDistance = 783;
            this.splitContainer1.TabIndex = 2;
            this.splitContainer1.Visible = false;
            // 
            // fileListControl1
            // 
            this.fileListControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fileListControl1.FileTypeListSorted = false;
            this.fileListControl1.Location = new System.Drawing.Point(0, 0);
            this.fileListControl1.Name = "fileListControl1";
            this.fileListControl1.SearcherThreads = 1;
            this.fileListControl1.Size = new System.Drawing.Size(783, 734);
            this.fileListControl1.TabIndex = 0;
            this.fileListControl1.TreeImageList = null;
            this.fileListControl1.OnFileDataSelected += new System.EventHandler<FileList.Models.FileDataSelectedEventArgs>(this.FileListControl1_OnFileDataSelected);
            // 
            // viewerPanel
            // 
            this.viewerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewerPanel.Location = new System.Drawing.Point(0, 105);
            this.viewerPanel.Name = "viewerPanel";
            this.viewerPanel.Padding = new System.Windows.Forms.Padding(3);
            this.viewerPanel.Size = new System.Drawing.Size(623, 629);
            this.viewerPanel.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.moveSelectedButton);
            this.panel2.Controls.Add(this.deleteButton);
            this.panel2.Controls.Add(this.openLocationButton);
            this.panel2.Controls.Add(this.filePropertiesTextBox);
            this.panel2.Controls.Add(this.openFileButton);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(623, 105);
            this.panel2.TabIndex = 1;
            // 
            // filePropertiesTextBox
            // 
            this.filePropertiesTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.filePropertiesTextBox.Location = new System.Drawing.Point(88, 6);
            this.filePropertiesTextBox.Multiline = true;
            this.filePropertiesTextBox.Name = "filePropertiesTextBox";
            this.filePropertiesTextBox.ReadOnly = true;
            this.filePropertiesTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.filePropertiesTextBox.Size = new System.Drawing.Size(369, 96);
            this.filePropertiesTextBox.TabIndex = 2;
            // 
            // deleteFileContextMenu
            // 
            this.deleteFileContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteSelectedToolStripMenuItem,
            this.deleteCheckedToolStripMenuItem});
            this.deleteFileContextMenu.Name = "deleteFileContextMenu";
            this.deleteFileContextMenu.Size = new System.Drawing.Size(181, 70);
            // 
            // deleteSelectedToolStripMenuItem
            // 
            this.deleteSelectedToolStripMenuItem.Name = "deleteSelectedToolStripMenuItem";
            this.deleteSelectedToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.deleteSelectedToolStripMenuItem.Text = "Delete Selected";
            this.deleteSelectedToolStripMenuItem.Click += new System.EventHandler(this.deleteSelectedToolStripMenuItem_Click);
            // 
            // deleteCheckedToolStripMenuItem
            // 
            this.deleteCheckedToolStripMenuItem.Name = "deleteCheckedToolStripMenuItem";
            this.deleteCheckedToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.deleteCheckedToolStripMenuItem.Text = "Delete Checked";
            this.deleteCheckedToolStripMenuItem.Click += new System.EventHandler(this.deleteCheckedToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1410, 790);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.browsePanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "File Thingy";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.browsePanel.ResumeLayout(false);
            this.browsePanel.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.deleteFileContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox rootPathTextBox;
        private System.Windows.Forms.Panel browsePanel;
        private System.Windows.Forms.Button searchButton;
        private Common.Models.GripSplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.Button openLocationButton;
        private System.Windows.Forms.TextBox filePropertiesTextBox;
        private System.Windows.Forms.Button openFileButton;
        private System.Windows.Forms.Panel viewerPanel;
        private System.Windows.Forms.Button deleteButton;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ImageList treeIconsImageList;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button moveSelectedButton;
        private System.Windows.Forms.Label label2;
        private Models.FileListControl fileListControl1;
        private System.Windows.Forms.ContextMenuStrip deleteFileContextMenu;
        private System.Windows.Forms.ToolStripMenuItem deleteSelectedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteCheckedToolStripMenuItem;
    }
}