namespace FilePreview.ImageFiles
{
    partial class ImagePreviewControl
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
            this.imageDisplayStylePanel = new System.Windows.Forms.Panel();
            this.imageViewerPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // imageDisplayStylePanel
            // 
            this.imageDisplayStylePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imageDisplayStylePanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.imageDisplayStylePanel.Location = new System.Drawing.Point(0, 0);
            this.imageDisplayStylePanel.Margin = new System.Windows.Forms.Padding(0);
            this.imageDisplayStylePanel.Name = "imageDisplayStylePanel";
            this.imageDisplayStylePanel.Padding = new System.Windows.Forms.Padding(3);
            this.imageDisplayStylePanel.Size = new System.Drawing.Size(670, 41);
            this.imageDisplayStylePanel.TabIndex = 4;
            // 
            // imageViewerPanel
            // 
            this.imageViewerPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imageViewerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageViewerPanel.Location = new System.Drawing.Point(0, 41);
            this.imageViewerPanel.Name = "imageViewerPanel";
            this.imageViewerPanel.Size = new System.Drawing.Size(670, 414);
            this.imageViewerPanel.TabIndex = 5;
            // 
            // ImagePreviewControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.imageViewerPanel);
            this.Controls.Add(this.imageDisplayStylePanel);
            this.Name = "ImagePreviewControl";
            this.Size = new System.Drawing.Size(670, 455);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel imageDisplayStylePanel;
        private System.Windows.Forms.Panel imageViewerPanel;
    }
}
