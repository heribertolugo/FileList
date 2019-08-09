namespace FileList.Models
{
    partial class FileListControl
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
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = (System.ComponentModel.IContainer)new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager componentResourceManager = new System.ComponentModel.ComponentResourceManager(typeof(FileListControl));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.fileTypesCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.filterButton = new System.Windows.Forms.Button();
            this.filesTreeViewContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.expandTreeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.collapseTreeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.openFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileLocationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.deleteFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dateModifiedButton = new SortButton();
            this.dateCreatedButton = new SortButton();
            this.sizeSortButton = new SortButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.checkUncheckAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.filesTreeViewContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100f));
            this.tableLayoutPanel1.Controls.Add(this.treeView1, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.fileTypesCheckedListBox, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100f));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(662, 428);
            this.tableLayoutPanel1.TabIndex = 0;
            this.tableLayoutPanel1.SetColumnSpan(this.panel1, 2);
            // 
            // treeView1
            // 
            this.treeView1.CheckBoxes = true;
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.HideSelection = false;
            this.treeView1.Location = new System.Drawing.Point(129, 53);
            this.treeView1.Name = "treeView1";
            this.treeView1.ShowNodeToolTips = true;
            this.treeView1.Size = new System.Drawing.Size(530, 372);
            this.treeView1.TabIndex = 3;
            this.treeView1.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.TreeView1_AfterCheck);
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeView1_AfterSelect);
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TreeView1_NodeMouseClick);
            // 
            // fileTypesCheckedListBox
            // 
            this.fileTypesCheckedListBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.fileTypesCheckedListBox.FormattingEnabled = true;
            this.fileTypesCheckedListBox.Location = new System.Drawing.Point(3, 53);
            this.fileTypesCheckedListBox.MinimumSize = new System.Drawing.Size(120, 372);
            this.fileTypesCheckedListBox.Name = "fileTypesCheckedListBox";
            this.fileTypesCheckedListBox.Size = new System.Drawing.Size(120, 372);
            this.fileTypesCheckedListBox.TabIndex = 2;
            this.fileTypesCheckedListBox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.FileTypesCheckedListBox_ItemCheck);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.filterButton);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.MinimumSize = new System.Drawing.Size(400, 44);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3);
            this.panel1.Size = new System.Drawing.Size(656, 44);
            this.panel1.TabIndex = 0;
            // 
            // panel
            //
            this.panel2.Controls.Add(this.dateModifiedButton);
            this.panel2.Controls.Add(this.dateCreatedButton);
            this.panel2.Controls.Add(this.sizeSortButton);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(46, 16);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(607, 25);
            this.panel2.TabIndex = 2;
            // 
            // label1
            //
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(46, 3);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.label1.Size = new System.Drawing.Size(607, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // filterButton
            //
            this.filterButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.filterButton.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.filterButton.Image = (System.Drawing.Image)Properties.Resources.menu_16;
            this.filterButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.filterButton.Location = new System.Drawing.Point(3, 3);
            this.filterButton.Name = "filterButton";
            this.filterButton.Size = new System.Drawing.Size(43, 38);
            this.filterButton.TabIndex = 0;
            this.filterButton.Text = "Filters";
            this.filterButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.filterButton.UseVisualStyleBackColor = true;
            this.filterButton.Click += new System.EventHandler(this.FilterButton_Click);
            // 
            // filesTreeViewContextMenu
            //
            this.filesTreeViewContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[9]
            {
                (System.Windows.Forms.ToolStripItem) this.checkUncheckAllToolStripMenuItem,
                (System.Windows.Forms.ToolStripItem) this.toolStripSeparator3,
                (System.Windows.Forms.ToolStripItem) this.expandTreeToolStripMenuItem,
                (System.Windows.Forms.ToolStripItem) this.collapseTreeToolStripMenuItem,
                (System.Windows.Forms.ToolStripItem) this.toolStripSeparator2,
                (System.Windows.Forms.ToolStripItem) this.openFileToolStripMenuItem,
                (System.Windows.Forms.ToolStripItem) this.fileLocationToolStripMenuItem,
                (System.Windows.Forms.ToolStripItem) this.toolStripSeparator1,
                (System.Windows.Forms.ToolStripItem) this.deleteFileToolStripMenuItem
            });
            this.filesTreeViewContextMenu.Name = "filesTreeViewContextMenu";
            this.filesTreeViewContextMenu.ShowImageMargin = false;
            this.filesTreeViewContextMenu.Size = new System.Drawing.Size(156, 176);
            // 
            // expandTreeToolStripMenuItem
            //
            this.expandTreeToolStripMenuItem.Name = "expandTreeToolStripMenuItem";
            this.expandTreeToolStripMenuItem.Size = new System.Drawing.Size((int)sbyte.MaxValue, 22);
            this.expandTreeToolStripMenuItem.Text = "Expand All";
            this.expandTreeToolStripMenuItem.Click += new System.EventHandler(this.ExpandTreeToolStripMenuItem_Click);
            // 
            // collapseTreeToolStripMenuItem
            //
            this.collapseTreeToolStripMenuItem.Name = "collapseTreeToolStripMenuItem";
            this.collapseTreeToolStripMenuItem.Size = new System.Drawing.Size((int)sbyte.MaxValue, 22);
            this.collapseTreeToolStripMenuItem.Text = "Collapse All";
            this.collapseTreeToolStripMenuItem.Click += new System.EventHandler(this.CollapseTreeToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            //
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(124, 6);
            // 
            // openFileToolStripMenuItem
            //
            this.openFileToolStripMenuItem.Name = "openFileToolStripMenuItem";
            this.openFileToolStripMenuItem.Size = new System.Drawing.Size((int)sbyte.MaxValue, 22);
            this.openFileToolStripMenuItem.Text = "Open File";
            this.openFileToolStripMenuItem.Click += new System.EventHandler(this.OpenFileToolStripMenuItem_Click);
            // 
            // fileLocationToolStripMenuItem
            //
            this.fileLocationToolStripMenuItem.Name = "fileLocationToolStripMenuItem";
            this.fileLocationToolStripMenuItem.Size = new System.Drawing.Size((int)sbyte.MaxValue, 22);
            this.fileLocationToolStripMenuItem.Text = "Open Location";
            this.fileLocationToolStripMenuItem.Click += new System.EventHandler(this.FileLocationToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            //
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(124, 6);
            // 
            // deleteFileToolStripMenuItem
            //
            this.deleteFileToolStripMenuItem.Name = "deleteFileToolStripMenuItem";
            this.deleteFileToolStripMenuItem.Size = new System.Drawing.Size((int)sbyte.MaxValue, 22);
            this.deleteFileToolStripMenuItem.Text = "Delete";
            this.deleteFileToolStripMenuItem.Click += new System.EventHandler(this.DeleteFileToolStripMenuItem_Click);
            // 
            // dateModifiedButton
            //
            this.dateModifiedButton.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
            this.dateModifiedButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.dateModifiedButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.dateModifiedButton.Location = new System.Drawing.Point(280, 2);
            this.dateModifiedButton.Name = "dateModifiedButton";
            this.dateModifiedButton.Size = new System.Drawing.Size(116, 23);
            this.dateModifiedButton.SortOrder = System.Windows.Forms.SortOrder.None;
            this.dateModifiedButton.TabIndex = 2;
            this.dateModifiedButton.Text = "Date Modified";
            this.dateModifiedButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.dateModifiedButton.UseVisualStyleBackColor = true;
            this.dateModifiedButton.Click += new System.EventHandler(this.DateModifiedButton_Click);
            // 
            // dateCreatedButton
            //
            this.dateCreatedButton.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
            this.dateCreatedButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.dateCreatedButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.dateCreatedButton.Location = new System.Drawing.Point(161, 2);
            this.dateCreatedButton.Name = "dateCreatedButton";
            this.dateCreatedButton.Size = new System.Drawing.Size(113, 23);
            this.dateCreatedButton.SortOrder = System.Windows.Forms.SortOrder.None;
            this.dateCreatedButton.TabIndex = 1;
            this.dateCreatedButton.Text = "Date Created";
            this.dateCreatedButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.dateCreatedButton.UseVisualStyleBackColor = true;
            this.dateCreatedButton.Click += new System.EventHandler(this.DateCreatedButton_Click);
            // 
            // sizeSortButton
            //
            this.sizeSortButton.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
            this.sizeSortButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sizeSortButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.sizeSortButton.Location = new System.Drawing.Point(80, 2);
            this.sizeSortButton.Name = "sizeSortButton";
            this.sizeSortButton.Size = new System.Drawing.Size(75, 23);
            this.sizeSortButton.SortOrder = System.Windows.Forms.SortOrder.None;
            this.sizeSortButton.TabIndex = 0;
            this.sizeSortButton.Text = "Size";
            this.sizeSortButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.sizeSortButton.UseVisualStyleBackColor = true;
            this.sizeSortButton.Click += new System.EventHandler(this.SizeSortButton_Click);
            // 
            // toolStripSeparator3
            //
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(152, 6);
            // 
            // checkUncheckAllToolStripMenuItem
            //
            this.checkUncheckAllToolStripMenuItem.Name = "checkUncheckAllToolStripMenuItem";
            this.checkUncheckAllToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.checkUncheckAllToolStripMenuItem.Text = "Check/Uncheck All";
            this.checkUncheckAllToolStripMenuItem.Click += new System.EventHandler(this.CheckUncheckAllToolStripMenuItem_Click);
            // 
            // FileListControl
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FileListControl";
            this.Size = new System.Drawing.Size(662, 428);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.filesTreeViewContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion


        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button filterButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckedListBox fileTypesCheckedListBox;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Panel panel2;
        private SortButton sizeSortButton;
        private SortButton dateModifiedButton;
        private SortButton dateCreatedButton;
        private System.Windows.Forms.ContextMenuStrip filesTreeViewContextMenu;
        private System.Windows.Forms.ToolStripMenuItem expandTreeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem collapseTreeToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem openFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileLocationToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem deleteFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem checkUncheckAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    }
}
