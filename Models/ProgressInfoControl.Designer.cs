namespace FileList.Models
{
    partial class ProgressInfoControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.progressPanel = new System.Windows.Forms.Panel();
            this.splitContainer1 = new Models.GripSplitContainer();
            this.itemsMovedListBox = new System.Windows.Forms.ListBox();
            this.itemsMovedTextBox = new System.Windows.Forms.TextBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.panel6 = new System.Windows.Forms.Panel();
            this.itemsMovedCountLabel = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.progressPanel.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // progressPanel
            // 
            this.progressPanel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.progressPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.progressPanel.Controls.Add(this.splitContainer1);
            this.progressPanel.Controls.Add(this.progressBar1);
            this.progressPanel.Controls.Add(this.panel6);
            this.progressPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.progressPanel.Location = new System.Drawing.Point(0, 0);
            this.progressPanel.Name = "progressPanel";
            this.progressPanel.Size = new System.Drawing.Size(752, 466);
            this.progressPanel.TabIndex = 2;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 21);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.itemsMovedListBox);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.itemsMovedTextBox);
            this.splitContainer1.Size = new System.Drawing.Size(748, 418);
            this.splitContainer1.SplitterDistance = 248;
            this.splitContainer1.TabIndex = 5;
            // 
            // itemsMovedListBox
            // 
            this.itemsMovedListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.itemsMovedListBox.FormattingEnabled = true;
            this.itemsMovedListBox.Location = new System.Drawing.Point(0, 0);
            this.itemsMovedListBox.Name = "itemsMovedListBox";
            this.itemsMovedListBox.Size = new System.Drawing.Size(248, 418);
            this.itemsMovedListBox.TabIndex = 3;
            // 
            // itemsMovedTextBox
            // 
            this.itemsMovedTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.itemsMovedTextBox.Location = new System.Drawing.Point(0, 0);
            this.itemsMovedTextBox.Multiline = true;
            this.itemsMovedTextBox.Name = "itemsMovedTextBox";
            this.itemsMovedTextBox.ReadOnly = true;
            this.itemsMovedTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.itemsMovedTextBox.Size = new System.Drawing.Size(496, 418);
            this.itemsMovedTextBox.TabIndex = 5;
            this.itemsMovedTextBox.WordWrap = false;
            // 
            // progressBar1
            // 
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar1.Location = new System.Drawing.Point(0, 439);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(748, 23);
            this.progressBar1.TabIndex = 4;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.itemsMovedCountLabel);
            this.panel6.Controls.Add(this.label5);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(748, 21);
            this.panel6.TabIndex = 2;
            // 
            // itemsMovedCountLabel
            // 
            this.itemsMovedCountLabel.AutoSize = true;
            this.itemsMovedCountLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.itemsMovedCountLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.itemsMovedCountLabel.Location = new System.Drawing.Point(82, 0);
            this.itemsMovedCountLabel.Name = "itemsMovedCountLabel";
            this.itemsMovedCountLabel.Size = new System.Drawing.Size(14, 13);
            this.itemsMovedCountLabel.TabIndex = 1;
            this.itemsMovedCountLabel.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Left;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Items moved:";
            // 
            // ProgressInfoControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.progressPanel);
            this.Name = "ProgressInfoControl";
            this.Size = new System.Drawing.Size(752, 466);
            this.progressPanel.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion


        private System.Windows.Forms.Panel progressPanel;
        private Models.GripSplitContainer splitContainer1;
        private System.Windows.Forms.ListBox itemsMovedListBox;
        private System.Windows.Forms.TextBox itemsMovedTextBox;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label itemsMovedCountLabel;
        private System.Windows.Forms.Label label5;
    }
}
