namespace FilePreview.MediaFiles
{
    partial class MediaPlayerControl
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
            this.durationLabel = new System.Windows.Forms.Label();
            this.timeLabel = new System.Windows.Forms.Label();
            this.volumeTrackBar = new System.Windows.Forms.TrackBar();
            this.framesTrackBar = new System.Windows.Forms.TrackBar();
            this.viewerPanel = new System.Windows.Forms.Panel();
            this.controlsPanel = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.muteButton = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.playButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.volumeTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.framesTrackBar)).BeginInit();
            this.controlsPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // durationLabel
            // 
            this.durationLabel.AutoSize = true;
            this.durationLabel.Dock = System.Windows.Forms.DockStyle.Right;
            this.durationLabel.Location = new System.Drawing.Point(638, 0);
            this.durationLabel.Name = "durationLabel";
            this.durationLabel.Size = new System.Drawing.Size(49, 13);
            this.durationLabel.TabIndex = 23;
            this.durationLabel.Text = "00:00:00";
            // 
            // timeLabel
            // 
            this.timeLabel.AutoSize = true;
            this.timeLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.timeLabel.Location = new System.Drawing.Point(0, 0);
            this.timeLabel.Name = "timeLabel";
            this.timeLabel.Size = new System.Drawing.Size(49, 13);
            this.timeLabel.TabIndex = 22;
            this.timeLabel.Text = "00:00:00";
            // 
            // volumeTrackBar
            // 
            this.volumeTrackBar.AutoSize = false;
            this.volumeTrackBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.volumeTrackBar.Location = new System.Drawing.Point(0, 15);
            this.volumeTrackBar.Maximum = 100;
            this.volumeTrackBar.Name = "volumeTrackBar";
            this.volumeTrackBar.Size = new System.Drawing.Size(78, 19);
            this.volumeTrackBar.TabIndex = 20;
            this.volumeTrackBar.Value = 5;
            this.volumeTrackBar.Scroll += new System.EventHandler(this.volumeTrackBar_Scroll);
            // 
            // framesTrackBar
            // 
            this.framesTrackBar.AutoSize = false;
            this.framesTrackBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.framesTrackBar.Location = new System.Drawing.Point(0, 15);
            this.framesTrackBar.Maximum = 100;
            this.framesTrackBar.Name = "framesTrackBar";
            this.framesTrackBar.Size = new System.Drawing.Size(687, 19);
            this.framesTrackBar.TabIndex = 19;
            this.framesTrackBar.Scroll += new System.EventHandler(this.framesTrackBar_Scroll);
            // 
            // viewerPanel
            // 
            this.viewerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewerPanel.Location = new System.Drawing.Point(0, 0);
            this.viewerPanel.Name = "viewerPanel";
            this.viewerPanel.Size = new System.Drawing.Size(777, 546);
            this.viewerPanel.TabIndex = 13;
            // 
            // controlsPanel
            // 
            this.controlsPanel.ColumnCount = 3;
            this.controlsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.controlsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.controlsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 84F));
            this.controlsPanel.Controls.Add(this.panel1, 2, 0);
            this.controlsPanel.Controls.Add(this.panel2, 0, 0);
            this.controlsPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.controlsPanel.Location = new System.Drawing.Point(0, 546);
            this.controlsPanel.Name = "controlsPanel";
            this.controlsPanel.RowCount = 1;
            this.controlsPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.controlsPanel.Size = new System.Drawing.Size(777, 40);
            this.controlsPanel.TabIndex = 26;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.muteButton);
            this.panel1.Controls.Add(this.volumeTrackBar);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(696, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(78, 34);
            this.panel1.TabIndex = 28;
            // 
            // muteButton
            // 
            this.muteButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.muteButton.BackgroundImage = global::FilePreview.Properties.Resources.soundButton;
            this.muteButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.muteButton.FlatAppearance.BorderSize = 0;
            this.muteButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.muteButton.Location = new System.Drawing.Point(15, 0);
            this.muteButton.Name = "muteButton";
            this.muteButton.Size = new System.Drawing.Size(49, 18);
            this.muteButton.TabIndex = 14;
            this.muteButton.UseVisualStyleBackColor = true;
            this.muteButton.Click += new System.EventHandler(this.muteButton_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.playButton);
            this.panel2.Controls.Add(this.timeLabel);
            this.panel2.Controls.Add(this.durationLabel);
            this.panel2.Controls.Add(this.framesTrackBar);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(687, 34);
            this.panel2.TabIndex = 29;
            // 
            // playButton
            // 
            this.playButton.BackgroundImage = global::FilePreview.Properties.Resources.pauseButton;
            this.playButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.playButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.playButton.FlatAppearance.BorderSize = 0;
            this.playButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.playButton.Location = new System.Drawing.Point(49, 0);
            this.playButton.Name = "playButton";
            this.playButton.Size = new System.Drawing.Size(589, 15);
            this.playButton.TabIndex = 16;
            this.playButton.UseVisualStyleBackColor = true;
            this.playButton.Click += new System.EventHandler(this.pauseButton_Click);
            // 
            // MediaPlayerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.viewerPanel);
            this.Controls.Add(this.controlsPanel);
            this.Name = "MediaPlayerControl";
            this.Size = new System.Drawing.Size(777, 586);
            ((System.ComponentModel.ISupportInitialize)(this.volumeTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.framesTrackBar)).EndInit();
            this.controlsPanel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label durationLabel;
        private System.Windows.Forms.Label timeLabel;
        private System.Windows.Forms.TrackBar volumeTrackBar;
        private System.Windows.Forms.TrackBar framesTrackBar;
        private System.Windows.Forms.Button playButton;
        private System.Windows.Forms.Button muteButton;
        private System.Windows.Forms.Panel viewerPanel;
        private System.Windows.Forms.TableLayoutPanel controlsPanel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
    }
}
