namespace FileList.Views
{
    partial class WhiteBackListForm
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
            this.noneRadioButton = new System.Windows.Forms.RadioButton();
            this.whiteListRadioButton = new System.Windows.Forms.RadioButton();
            this.blackListRadioButton = new System.Windows.Forms.RadioButton();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.Controls.Add(this.noneRadioButton);
            this.panel1.Controls.Add(this.whiteListRadioButton);
            this.panel1.Controls.Add(this.blackListRadioButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3);
            this.panel1.Size = new System.Drawing.Size(800, 29);
            this.panel1.TabIndex = 0;
            // 
            // noneRadioButton
            // 
            this.noneRadioButton.AutoSize = true;
            this.noneRadioButton.Checked = true;
            this.noneRadioButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.noneRadioButton.FlatAppearance.BorderSize = 0;
            this.noneRadioButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.noneRadioButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.noneRadioButton.Location = new System.Drawing.Point(146, 3);
            this.noneRadioButton.Margin = new System.Windows.Forms.Padding(6, 3, 3, 3);
            this.noneRadioButton.MaximumSize = new System.Drawing.Size(72, 17);
            this.noneRadioButton.MinimumSize = new System.Drawing.Size(72, 17);
            this.noneRadioButton.Name = "noneRadioButton";
            this.noneRadioButton.Size = new System.Drawing.Size(72, 17);
            this.noneRadioButton.TabIndex = 2;
            this.noneRadioButton.TabStop = true;
            this.noneRadioButton.Text = "None";
            this.noneRadioButton.UseVisualStyleBackColor = true;
            this.noneRadioButton.CheckedChanged += new System.EventHandler(this.noneRadioButton_CheckedChanged);
            // 
            // whiteListRadioButton
            // 
            this.whiteListRadioButton.AutoSize = true;
            this.whiteListRadioButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.whiteListRadioButton.FlatAppearance.BorderSize = 0;
            this.whiteListRadioButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.whiteListRadioButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.whiteListRadioButton.Location = new System.Drawing.Point(74, 3);
            this.whiteListRadioButton.Margin = new System.Windows.Forms.Padding(6, 3, 3, 3);
            this.whiteListRadioButton.MaximumSize = new System.Drawing.Size(72, 17);
            this.whiteListRadioButton.MinimumSize = new System.Drawing.Size(72, 17);
            this.whiteListRadioButton.Name = "whiteListRadioButton";
            this.whiteListRadioButton.Size = new System.Drawing.Size(72, 17);
            this.whiteListRadioButton.TabIndex = 1;
            this.whiteListRadioButton.Text = "White List";
            this.whiteListRadioButton.UseVisualStyleBackColor = true;
            this.whiteListRadioButton.CheckedChanged += new System.EventHandler(this.whiteListRadioButton_CheckedChanged);
            // 
            // blackListRadioButton
            // 
            this.blackListRadioButton.AutoSize = true;
            this.blackListRadioButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.blackListRadioButton.FlatAppearance.BorderSize = 0;
            this.blackListRadioButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.blackListRadioButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.blackListRadioButton.Location = new System.Drawing.Point(3, 3);
            this.blackListRadioButton.MaximumSize = new System.Drawing.Size(71, 17);
            this.blackListRadioButton.MinimumSize = new System.Drawing.Size(71, 17);
            this.blackListRadioButton.Name = "blackListRadioButton";
            this.blackListRadioButton.Size = new System.Drawing.Size(71, 17);
            this.blackListRadioButton.TabIndex = 0;
            this.blackListRadioButton.Text = "Black List";
            this.blackListRadioButton.UseVisualStyleBackColor = true;
            this.blackListRadioButton.CheckedChanged += new System.EventHandler(this.blackListRadioButton_CheckedChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.ColumnHeadersVisible = false;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Value});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 29);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(800, 421);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
            this.dataGridView1.CurrentCellDirtyStateChanged += new System.EventHandler(this.dataGridView1_CurrentCellDirtyStateChanged);
            this.dataGridView1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dataGridView1_KeyPress);
            // 
            // Value
            // 
            this.Value.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Value.HeaderText = "Value";
            this.Value.Name = "Value";
            // 
            // WhiteBackListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panel1);
            this.Name = "WhiteBackListForm";
            this.Text = "WhiteBackListForm";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton whiteListRadioButton;
        private System.Windows.Forms.RadioButton blackListRadioButton;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value;
        private System.Windows.Forms.RadioButton noneRadioButton;
    }
}