namespace FileList.Views
{
    partial class MoveFilesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MoveFilesForm));
            this.label2 = new System.Windows.Forms.Label();
            this.destinationTextBox = new System.Windows.Forms.TextBox();
            this.browseButton = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.panel5 = new System.Windows.Forms.Panel();
            this.itemsToMoveCountLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.overwriteCheckBox = new System.Windows.Forms.CheckBox();
            this.maxErrorsTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.copyItemsCheckBox = new System.Windows.Forms.CheckBox();
            this.retainDirectoryCheckBox = new System.Windows.Forms.CheckBox();
            this.moveFilesButton = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cancelButton = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.splitContainer2 = new Common.Models.GripSplitContainer();
            this.progressInfoControl1 = new FileList.Models.ProgressInfoControl();
            this.panel3.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Destination";
            // 
            // destinationTextBox
            // 
            this.destinationTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.destinationTextBox.Location = new System.Drawing.Point(6, 72);
            this.destinationTextBox.Name = "destinationTextBox";
            this.destinationTextBox.Size = new System.Drawing.Size(782, 20);
            this.destinationTextBox.TabIndex = 3;
            this.destinationTextBox.TextChanged += new System.EventHandler(this.DestinationTextBox_TextChanged);
            // 
            // browseButton
            // 
            this.browseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.browseButton.Location = new System.Drawing.Point(711, 99);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(77, 23);
            this.browseButton.TabIndex = 4;
            this.browseButton.Text = "Browse";
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.BrowseButton_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.treeView1);
            this.panel3.Controls.Add(this.panel5);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(266, 267);
            this.panel3.TabIndex = 0;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(0, 21);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(266, 246);
            this.treeView1.TabIndex = 1;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.itemsToMoveCountLabel);
            this.panel5.Controls.Add(this.label3);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(266, 21);
            this.panel5.TabIndex = 2;
            // 
            // itemsToMoveCountLabel
            // 
            this.itemsToMoveCountLabel.AutoSize = true;
            this.itemsToMoveCountLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.itemsToMoveCountLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.itemsToMoveCountLabel.Location = new System.Drawing.Point(90, 0);
            this.itemsToMoveCountLabel.Name = "itemsToMoveCountLabel";
            this.itemsToMoveCountLabel.Size = new System.Drawing.Size(14, 13);
            this.itemsToMoveCountLabel.TabIndex = 1;
            this.itemsToMoveCountLabel.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Left;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Items to move:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.overwriteCheckBox);
            this.panel1.Controls.Add(this.maxErrorsTextBox);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.copyItemsCheckBox);
            this.panel1.Controls.Add(this.retainDirectoryCheckBox);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.browseButton);
            this.panel1.Controls.Add(this.destinationTextBox);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 133);
            this.panel1.TabIndex = 6;
            // 
            // overwriteCheckBox
            // 
            this.overwriteCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.overwriteCheckBox.AutoSize = true;
            this.overwriteCheckBox.Location = new System.Drawing.Point(319, 102);
            this.overwriteCheckBox.Name = "overwriteCheckBox";
            this.overwriteCheckBox.Size = new System.Drawing.Size(108, 17);
            this.overwriteCheckBox.TabIndex = 10;
            this.overwriteCheckBox.Text = "Overwrite if exists";
            this.overwriteCheckBox.UseVisualStyleBackColor = true;
            this.overwriteCheckBox.CheckedChanged += new System.EventHandler(this.OverwriteCheckBox_CheckedChanged);
            // 
            // maxErrorsTextBox
            // 
            this.maxErrorsTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.maxErrorsTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.maxErrorsTextBox.Location = new System.Drawing.Point(75, 102);
            this.maxErrorsTextBox.Name = "maxErrorsTextBox";
            this.maxErrorsTextBox.Size = new System.Drawing.Size(29, 20);
            this.maxErrorsTextBox.TabIndex = 9;
            this.maxErrorsTextBox.Text = "10";
            this.maxErrorsTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(104, 103);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Errors";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 103);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Cancel after";
            // 
            // copyItemsCheckBox
            // 
            this.copyItemsCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.copyItemsCheckBox.AutoSize = true;
            this.copyItemsCheckBox.Checked = true;
            this.copyItemsCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.copyItemsCheckBox.Location = new System.Drawing.Point(433, 102);
            this.copyItemsCheckBox.Name = "copyItemsCheckBox";
            this.copyItemsCheckBox.Size = new System.Drawing.Size(131, 17);
            this.copyItemsCheckBox.TabIndex = 6;
            this.copyItemsCheckBox.Text = "Just Copy, Dont Move";
            this.copyItemsCheckBox.UseVisualStyleBackColor = true;
            // 
            // retainDirectoryCheckBox
            // 
            this.retainDirectoryCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.retainDirectoryCheckBox.AutoSize = true;
            this.retainDirectoryCheckBox.Checked = true;
            this.retainDirectoryCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.retainDirectoryCheckBox.Location = new System.Drawing.Point(567, 102);
            this.retainDirectoryCheckBox.Name = "retainDirectoryCheckBox";
            this.retainDirectoryCheckBox.Size = new System.Drawing.Size(138, 17);
            this.retainDirectoryCheckBox.TabIndex = 5;
            this.retainDirectoryCheckBox.Text = "Keep directory structure";
            this.retainDirectoryCheckBox.UseVisualStyleBackColor = true;
            this.retainDirectoryCheckBox.CheckedChanged += new System.EventHandler(this.CheckBox1_CheckedChanged);
            // 
            // moveFilesButton
            // 
            this.moveFilesButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.moveFilesButton.Enabled = false;
            this.moveFilesButton.Location = new System.Drawing.Point(711, 15);
            this.moveFilesButton.Name = "moveFilesButton";
            this.moveFilesButton.Size = new System.Drawing.Size(77, 23);
            this.moveFilesButton.TabIndex = 5;
            this.moveFilesButton.Text = "Move/Copy";
            this.moveFilesButton.UseVisualStyleBackColor = true;
            this.moveFilesButton.Click += new System.EventHandler(this.MoveFilesButton_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.cancelButton);
            this.panel2.Controls.Add(this.moveFilesButton);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 400);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(800, 50);
            this.panel2.TabIndex = 7;
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.Enabled = false;
            this.cancelButton.Location = new System.Drawing.Point(612, 15);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 0;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 133);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.panel3);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.progressInfoControl1);
            this.splitContainer2.Size = new System.Drawing.Size(800, 267);
            this.splitContainer2.SplitterDistance = 266;
            this.splitContainer2.TabIndex = 8;
            // 
            // progressInfoControl1
            // 
            this.progressInfoControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.progressInfoControl1.Goal = 0;
            this.progressInfoControl1.Location = new System.Drawing.Point(0, 0);
            this.progressInfoControl1.Name = "progressInfoControl1";
            this.progressInfoControl1.Size = new System.Drawing.Size(530, 267);
            this.progressInfoControl1.Step = 10;
            this.progressInfoControl1.TabIndex = 0;
            // 
            // MoveFilesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.splitContainer2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MoveFilesForm";
            this.Text = "Move Files";
            this.Load += new System.EventHandler(this.MoveFilesForm_Load);
            this.panel3.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion


        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox destinationTextBox;
        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label itemsToMoveCountLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox retainDirectoryCheckBox;
        private System.Windows.Forms.Button moveFilesButton;
        private System.Windows.Forms.CheckBox copyItemsCheckBox;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.TextBox maxErrorsTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private Common.Models.GripSplitContainer splitContainer2;
        private System.Windows.Forms.CheckBox overwriteCheckBox;
        private Models.ProgressInfoControl progressInfoControl1;
    }
}