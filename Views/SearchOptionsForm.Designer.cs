namespace FileList.Views
{
    partial class SearchOptionsForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.threadCountUpDown = new System.Windows.Forms.NumericUpDown();
            this.closeButton = new System.Windows.Forms.Button();
            this.extensionsButton = new System.Windows.Forms.Button();
            this.propertiesButton = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.threadCountUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.closeButton);
            this.panel1.Controls.Add(this.extensionsButton);
            this.panel1.Controls.Add(this.propertiesButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.MinimumSize = new System.Drawing.Size(0, 30);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(604, 30);
            this.panel1.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.threadCountUpDown);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(150, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(159, 30);
            this.panel2.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 28);
            this.label2.TabIndex = 6;
            this.label2.Text = "Threads";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // threadCountUpDown
            // 
            this.threadCountUpDown.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.threadCountUpDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.threadCountUpDown.Location = new System.Drawing.Point(49, 9);
            this.threadCountUpDown.Margin = new System.Windows.Forms.Padding(0);
            this.threadCountUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.threadCountUpDown.Name = "threadCountUpDown";
            this.threadCountUpDown.Size = new System.Drawing.Size(100, 16);
            this.threadCountUpDown.TabIndex = 5;
            this.threadCountUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.threadCountUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.threadCountUpDown.ValueChanged += new System.EventHandler(this.threadCountUpDown_ValueChanged);
            // 
            // closeButton
            // 
            this.closeButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.closeButton.Image = global::FileList.Properties.Resources.closeX_16;
            this.closeButton.Location = new System.Drawing.Point(572, 0);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(32, 30);
            this.closeButton.TabIndex = 2;
            this.closeButton.Text = "X";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Visible = false;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // extensionsButton
            // 
            this.extensionsButton.BackColor = System.Drawing.SystemColors.ControlDark;
            this.extensionsButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.extensionsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.extensionsButton.Location = new System.Drawing.Point(75, 0);
            this.extensionsButton.Name = "extensionsButton";
            this.extensionsButton.Size = new System.Drawing.Size(75, 30);
            this.extensionsButton.TabIndex = 1;
            this.extensionsButton.Text = "Extensions";
            this.extensionsButton.UseVisualStyleBackColor = false;
            this.extensionsButton.Click += new System.EventHandler(this.extensionsButton_Click);
            // 
            // propertiesButton
            // 
            this.propertiesButton.BackColor = System.Drawing.SystemColors.ControlDark;
            this.propertiesButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.propertiesButton.FlatAppearance.BorderSize = 0;
            this.propertiesButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.propertiesButton.Location = new System.Drawing.Point(0, 0);
            this.propertiesButton.MinimumSize = new System.Drawing.Size(75, 30);
            this.propertiesButton.Name = "propertiesButton";
            this.propertiesButton.Size = new System.Drawing.Size(75, 30);
            this.propertiesButton.TabIndex = 0;
            this.propertiesButton.Text = "Properties";
            this.propertiesButton.UseVisualStyleBackColor = false;
            this.propertiesButton.Click += new System.EventHandler(this.propertiesButton_Click);
            // 
            // SearchOptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(610, 293);
            this.Controls.Add(this.panel1);
            this.IsMdiContainer = true;
            this.Name = "SearchOptionsForm";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.Text = "SearchOptionsForm";
            this.Load += new System.EventHandler(this.SearchOptionsForm_Load);
            this.MdiChildActivate += new System.EventHandler(this.SearchOptionsForm_MdiChildActivate);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.threadCountUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Button extensionsButton;
        private System.Windows.Forms.Button propertiesButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown threadCountUpDown;
        private System.Windows.Forms.Panel panel2;
    }
}