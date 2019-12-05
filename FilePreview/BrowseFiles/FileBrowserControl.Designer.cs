namespace FilePreview.BrowseFiles
{
    partial class FileBrowserControl
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
            this.currentDirectoryTextBox = new System.Windows.Forms.TextBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // currentDirectoryTextBox
            // 
            this.currentDirectoryTextBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.currentDirectoryTextBox.Location = new System.Drawing.Point(0, 0);
            this.currentDirectoryTextBox.Multiline = true;
            this.currentDirectoryTextBox.Name = "currentDirectoryTextBox";
            this.currentDirectoryTextBox.ReadOnly = true;
            this.currentDirectoryTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.currentDirectoryTextBox.Size = new System.Drawing.Size(711, 49);
            this.currentDirectoryTextBox.TabIndex = 0;
            this.currentDirectoryTextBox.WordWrap = false;
            // 
            // listView1
            // 
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(0, 49);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(711, 502);
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // FileBrowserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.currentDirectoryTextBox);
            this.Name = "FileBrowserControl";
            this.Size = new System.Drawing.Size(711, 551);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox currentDirectoryTextBox;
        private System.Windows.Forms.ListView listView1;
    }
}
