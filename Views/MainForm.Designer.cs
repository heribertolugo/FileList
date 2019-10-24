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
            this.splitContainer1 = new GripSplitContainer();
            this.fileListControl1 = new FileList.Models.FileListControl();
            this.viewerTabControl = new System.Windows.Forms.TabControl();
            this.imageTabPage = new System.Windows.Forms.TabPage();
            this.imageViewerPanel = new System.Windows.Forms.Panel();
            this.imageDisplayStylePanel = new System.Windows.Forms.Panel();
            this.documentTabPage = new System.Windows.Forms.TabPage();
            this.textViewerTextBox = new System.Windows.Forms.RichTextBox();
            this.contentTabPage = new System.Windows.Forms.TabPage();
            this.contentsListView = new System.Windows.Forms.ListView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.moveSelectedButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.openLocationButton = new System.Windows.Forms.Button();
            this.filePropertiesTextBox = new System.Windows.Forms.TextBox();
            this.openFileButton = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.treeIconsImageList = new System.Windows.Forms.ImageList(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.browsePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.viewerTabControl.SuspendLayout();
            this.imageTabPage.SuspendLayout();
            this.documentTabPage.SuspendLayout();
            this.contentTabPage.SuspendLayout();
            this.panel2.SuspendLayout();
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
            this.splitContainer1.Panel2.Controls.Add(this.viewerTabControl);
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
            this.fileListControl1.Size = new System.Drawing.Size(783, 734);
            this.fileListControl1.TabIndex = 0;
            this.fileListControl1.TreeImageList = null;
            this.fileListControl1.OnFileDataSelected += new System.EventHandler<FileList.Models.FileDataSelectedEventArgs>(this.FileListControl1_OnFileDataSelected);
            // 
            // viewerTabControl
            // 
            this.viewerTabControl.Controls.Add(this.imageTabPage);
            this.viewerTabControl.Controls.Add(this.documentTabPage);
            this.viewerTabControl.Controls.Add(this.contentTabPage);
            this.viewerTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewerTabControl.Location = new System.Drawing.Point(0, 105);
            this.viewerTabControl.Name = "viewerTabControl";
            this.viewerTabControl.SelectedIndex = 0;
            this.viewerTabControl.Size = new System.Drawing.Size(623, 629);
            this.viewerTabControl.TabIndex = 3;
            // 
            // imageTabPage
            // 
            this.imageTabPage.Controls.Add(this.imageViewerPanel);
            this.imageTabPage.Controls.Add(this.imageDisplayStylePanel);
            this.imageTabPage.Location = new System.Drawing.Point(4, 22);
            this.imageTabPage.Name = "imageTabPage";
            this.imageTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.imageTabPage.Size = new System.Drawing.Size(615, 603);
            this.imageTabPage.TabIndex = 0;
            this.imageTabPage.Text = "Image";
            this.imageTabPage.UseVisualStyleBackColor = true;
            // 
            // imageViewerPanel
            // 
            this.imageViewerPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imageViewerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageViewerPanel.Location = new System.Drawing.Point(3, 44);
            this.imageViewerPanel.Name = "imageViewerPanel";
            this.imageViewerPanel.Size = new System.Drawing.Size(609, 556);
            this.imageViewerPanel.TabIndex = 2;
            // 
            // imageDisplayStylePanel
            // 
            this.imageDisplayStylePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imageDisplayStylePanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.imageDisplayStylePanel.Location = new System.Drawing.Point(3, 3);
            this.imageDisplayStylePanel.Margin = new System.Windows.Forms.Padding(0);
            this.imageDisplayStylePanel.Name = "imageDisplayStylePanel";
            this.imageDisplayStylePanel.Padding = new System.Windows.Forms.Padding(3);
            this.imageDisplayStylePanel.Size = new System.Drawing.Size(609, 41);
            this.imageDisplayStylePanel.TabIndex = 3;
            // 
            // documentTabPage
            // 
            this.documentTabPage.Controls.Add(this.textViewerTextBox);
            this.documentTabPage.Location = new System.Drawing.Point(4, 22);
            this.documentTabPage.Name = "documentTabPage";
            this.documentTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.documentTabPage.Size = new System.Drawing.Size(615, 603);
            this.documentTabPage.TabIndex = 1;
            this.documentTabPage.Text = "Text";
            this.documentTabPage.UseVisualStyleBackColor = true;
            // 
            // textViewerTextBox
            // 
            this.textViewerTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textViewerTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textViewerTextBox.Location = new System.Drawing.Point(3, 3);
            this.textViewerTextBox.Name = "textViewerTextBox";
            this.textViewerTextBox.ReadOnly = true;
            this.textViewerTextBox.Size = new System.Drawing.Size(609, 597);
            this.textViewerTextBox.TabIndex = 0;
            this.textViewerTextBox.Text = "";
            // 
            // contentTabPage
            // 
            this.contentTabPage.Controls.Add(this.contentsListView);
            this.contentTabPage.Location = new System.Drawing.Point(4, 22);
            this.contentTabPage.Name = "contentTabPage";
            this.contentTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.contentTabPage.Size = new System.Drawing.Size(615, 603);
            this.contentTabPage.TabIndex = 2;
            this.contentTabPage.Text = "Content";
            this.contentTabPage.UseVisualStyleBackColor = true;
            // 
            // contentsListView
            // 
            this.contentsListView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.contentsListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentsListView.Location = new System.Drawing.Point(3, 3);
            this.contentsListView.Name = "contentsListView";
            this.contentsListView.Size = new System.Drawing.Size(609, 597);
            this.contentsListView.TabIndex = 0;
            this.contentsListView.UseCompatibleStateImageBehavior = false;
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
            // saveFileDialog1
            // 
            this.saveFileDialog1.AddExtension = false;
            this.saveFileDialog1.FileName = "Folder.";
            this.saveFileDialog1.OverwritePrompt = false;
            this.saveFileDialog1.Title = "Select Folder";
            // 
            // treeIconsImageList
            // 
            this.treeIconsImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.treeIconsImageList.ImageSize = new System.Drawing.Size(16, 16);
            this.treeIconsImageList.TransparentColor = System.Drawing.Color.Transparent;
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
            this.viewerTabControl.ResumeLayout(false);
            this.imageTabPage.ResumeLayout(false);
            this.documentTabPage.ResumeLayout(false);
            this.contentTabPage.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox rootPathTextBox;
        private System.Windows.Forms.Panel browsePanel;
        private System.Windows.Forms.Button searchButton;
        private GripSplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.Panel imageViewerPanel;
        private System.Windows.Forms.Button openLocationButton;
        private System.Windows.Forms.TextBox filePropertiesTextBox;
        private System.Windows.Forms.Button openFileButton;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.TabControl viewerTabControl;
        private System.Windows.Forms.TabPage imageTabPage;
        private System.Windows.Forms.TabPage documentTabPage;
        private System.Windows.Forms.RichTextBox textViewerTextBox;
        private System.Windows.Forms.Button deleteButton;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ImageList treeIconsImageList;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TabPage contentTabPage;
        private System.Windows.Forms.ListView contentsListView;
        private System.Windows.Forms.Button moveSelectedButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel imageDisplayStylePanel;
        private Models.FileListControl fileListControl1;
    }
}