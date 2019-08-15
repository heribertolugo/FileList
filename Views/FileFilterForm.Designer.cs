namespace FileList.Views
{
    partial class FileFilterForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.sizeType1ComboBox = new System.Windows.Forms.ComboBox();
            this.sizeAmount1NumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.sizeFilterComboBox = new System.Windows.Forms.ComboBox();
            this.sizeValue2Panel = new System.Windows.Forms.Panel();
            this.sizeType2ComboBox = new System.Windows.Forms.ComboBox();
            this.sizeAmount2NumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dateModifiedValue2Panel = new System.Windows.Forms.Panel();
            this.dateModified2Picker = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dateModified1Picker = new System.Windows.Forms.DateTimePicker();
            this.dateModifiedComboBox = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dateCreatedValue2Panel = new System.Windows.Forms.Panel();
            this.dateCreated2Picker = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.dateCreated1Picker = new System.Windows.Forms.DateTimePicker();
            this.dateCreatedcomboBox = new System.Windows.Forms.ComboBox();
            this.bannerPanel = new System.Windows.Forms.Panel();
            this.closeButton = new System.Windows.Forms.Button();
            this.clearFiltersButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sizeAmount1NumericUpDown)).BeginInit();
            this.sizeValue2Panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sizeAmount2NumericUpDown)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.dateModifiedValue2Panel.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.dateCreatedValue2Panel.SuspendLayout();
            this.bannerPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.sizeType1ComboBox);
            this.groupBox1.Controls.Add(this.sizeAmount1NumericUpDown);
            this.groupBox1.Controls.Add(this.sizeFilterComboBox);
            this.groupBox1.Controls.Add(this.sizeValue2Panel);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 24);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(477, 72);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Size";
            // 
            // sizeType1ComboBox
            // 
            this.sizeType1ComboBox.FormattingEnabled = true;
            this.sizeType1ComboBox.Location = new System.Drawing.Point(186, 29);
            this.sizeType1ComboBox.Name = "sizeType1ComboBox";
            this.sizeType1ComboBox.Size = new System.Drawing.Size(50, 21);
            this.sizeType1ComboBox.TabIndex = 2;
            this.sizeType1ComboBox.SelectionChangeCommitted += new System.EventHandler(this.SizeType1ComboBox_SelectedIndexChanged);
            // 
            // sizeAmount1NumericUpDown
            // 
            this.sizeAmount1NumericUpDown.DecimalPlaces = 2;
            this.sizeAmount1NumericUpDown.Location = new System.Drawing.Point(134, 29);
            this.sizeAmount1NumericUpDown.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.sizeAmount1NumericUpDown.Name = "sizeAmount1NumericUpDown";
            this.sizeAmount1NumericUpDown.Size = new System.Drawing.Size(46, 20);
            this.sizeAmount1NumericUpDown.TabIndex = 1;
            this.sizeAmount1NumericUpDown.ThousandsSeparator = true;
            this.sizeAmount1NumericUpDown.ValueChanged += new System.EventHandler(this.SizeAmount1NumericUpDown_ValueChanged);
            // 
            // sizeFilterComboBox
            // 
            this.sizeFilterComboBox.FormattingEnabled = true;
            this.sizeFilterComboBox.Location = new System.Drawing.Point(6, 29);
            this.sizeFilterComboBox.Name = "sizeFilterComboBox";
            this.sizeFilterComboBox.Size = new System.Drawing.Size(121, 21);
            this.sizeFilterComboBox.TabIndex = 0;
            this.sizeFilterComboBox.SelectionChangeCommitted += new System.EventHandler(this.SizeFilterComboBox_SelectedIndexChanged);
            // 
            // sizeValue2Panel
            // 
            this.sizeValue2Panel.Controls.Add(this.sizeType2ComboBox);
            this.sizeValue2Panel.Controls.Add(this.sizeAmount2NumericUpDown);
            this.sizeValue2Panel.Controls.Add(this.label1);
            this.sizeValue2Panel.Location = new System.Drawing.Point(242, 27);
            this.sizeValue2Panel.Name = "sizeValue2Panel";
            this.sizeValue2Panel.Size = new System.Drawing.Size(155, 26);
            this.sizeValue2Panel.TabIndex = 4;
            this.sizeValue2Panel.Visible = false;
            // 
            // sizeType2ComboBox
            // 
            this.sizeType2ComboBox.FormattingEnabled = true;
            this.sizeType2ComboBox.Location = new System.Drawing.Point(86, 2);
            this.sizeType2ComboBox.Name = "sizeType2ComboBox";
            this.sizeType2ComboBox.Size = new System.Drawing.Size(50, 21);
            this.sizeType2ComboBox.TabIndex = 5;
            this.sizeType2ComboBox.SelectionChangeCommitted += new System.EventHandler(this.SizeType2ComboBox_SelectedIndexChanged);
            // 
            // sizeAmount2NumericUpDown
            // 
            this.sizeAmount2NumericUpDown.DecimalPlaces = 2;
            this.sizeAmount2NumericUpDown.Location = new System.Drawing.Point(34, 3);
            this.sizeAmount2NumericUpDown.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.sizeAmount2NumericUpDown.Name = "sizeAmount2NumericUpDown";
            this.sizeAmount2NumericUpDown.Size = new System.Drawing.Size(46, 20);
            this.sizeAmount2NumericUpDown.TabIndex = 4;
            this.sizeAmount2NumericUpDown.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.sizeAmount2NumericUpDown.ValueChanged += new System.EventHandler(this.SizeAmount2NumericUpDown_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "and";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dateModifiedValue2Panel);
            this.groupBox2.Controls.Add(this.dateModified1Picker);
            this.groupBox2.Controls.Add(this.dateModifiedComboBox);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 96);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(477, 72);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Date Modified";
            // 
            // dateModifiedValue2Panel
            // 
            this.dateModifiedValue2Panel.Controls.Add(this.dateModified2Picker);
            this.dateModifiedValue2Panel.Controls.Add(this.label2);
            this.dateModifiedValue2Panel.Location = new System.Drawing.Point(284, 26);
            this.dateModifiedValue2Panel.Name = "dateModifiedValue2Panel";
            this.dateModifiedValue2Panel.Size = new System.Drawing.Size(190, 28);
            this.dateModifiedValue2Panel.TabIndex = 3;
            this.dateModifiedValue2Panel.Visible = false;
            // 
            // dateModified2Picker
            // 
            this.dateModified2Picker.CustomFormat = "MM/dd/yyyy hh:mm tt";
            this.dateModified2Picker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateModified2Picker.Location = new System.Drawing.Point(34, 4);
            this.dateModified2Picker.Name = "dateModified2Picker";
            this.dateModified2Picker.Size = new System.Drawing.Size(153, 20);
            this.dateModified2Picker.TabIndex = 5;
            this.dateModified2Picker.ValueChanged += new System.EventHandler(this.DateModified2Picker_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(25, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "and";
            // 
            // dateModified1Picker
            // 
            this.dateModified1Picker.CustomFormat = "MM/dd/yyyy hh:mm tt";
            this.dateModified1Picker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateModified1Picker.Location = new System.Drawing.Point(134, 30);
            this.dateModified1Picker.Name = "dateModified1Picker";
            this.dateModified1Picker.Size = new System.Drawing.Size(144, 20);
            this.dateModified1Picker.TabIndex = 2;
            this.dateModified1Picker.ValueChanged += new System.EventHandler(this.DateModified1Picker_ValueChanged);
            // 
            // dateModifiedComboBox
            // 
            this.dateModifiedComboBox.FormattingEnabled = true;
            this.dateModifiedComboBox.Location = new System.Drawing.Point(6, 30);
            this.dateModifiedComboBox.Name = "dateModifiedComboBox";
            this.dateModifiedComboBox.Size = new System.Drawing.Size(121, 21);
            this.dateModifiedComboBox.TabIndex = 1;
            this.dateModifiedComboBox.SelectionChangeCommitted += new System.EventHandler(this.DateModifiedComboBox_SelectedIndexChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dateCreatedValue2Panel);
            this.groupBox3.Controls.Add(this.dateCreated1Picker);
            this.groupBox3.Controls.Add(this.dateCreatedcomboBox);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(0, 168);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(477, 72);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Date Created";
            // 
            // dateCreatedValue2Panel
            // 
            this.dateCreatedValue2Panel.Controls.Add(this.dateCreated2Picker);
            this.dateCreatedValue2Panel.Controls.Add(this.label3);
            this.dateCreatedValue2Panel.Location = new System.Drawing.Point(284, 27);
            this.dateCreatedValue2Panel.Name = "dateCreatedValue2Panel";
            this.dateCreatedValue2Panel.Size = new System.Drawing.Size(190, 28);
            this.dateCreatedValue2Panel.TabIndex = 3;
            this.dateCreatedValue2Panel.Visible = false;
            // 
            // dateCreated2Picker
            // 
            this.dateCreated2Picker.CustomFormat = "MM/dd/yyyy hh:mm tt";
            this.dateCreated2Picker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateCreated2Picker.Location = new System.Drawing.Point(34, 4);
            this.dateCreated2Picker.Name = "dateCreated2Picker";
            this.dateCreated2Picker.Size = new System.Drawing.Size(153, 20);
            this.dateCreated2Picker.TabIndex = 5;
            this.dateCreated2Picker.ValueChanged += new System.EventHandler(this.DateCreated2Picker_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(25, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "and";
            // 
            // dateCreated1Picker
            // 
            this.dateCreated1Picker.CustomFormat = "MM/dd/yyyy hh:mm tt";
            this.dateCreated1Picker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateCreated1Picker.Location = new System.Drawing.Point(134, 30);
            this.dateCreated1Picker.Name = "dateCreated1Picker";
            this.dateCreated1Picker.Size = new System.Drawing.Size(144, 20);
            this.dateCreated1Picker.TabIndex = 2;
            this.dateCreated1Picker.ValueChanged += new System.EventHandler(this.DateCreated1Picker_ValueChanged);
            // 
            // dateCreatedcomboBox
            // 
            this.dateCreatedcomboBox.FormattingEnabled = true;
            this.dateCreatedcomboBox.Location = new System.Drawing.Point(6, 30);
            this.dateCreatedcomboBox.Name = "dateCreatedcomboBox";
            this.dateCreatedcomboBox.Size = new System.Drawing.Size(121, 21);
            this.dateCreatedcomboBox.TabIndex = 1;
            this.dateCreatedcomboBox.SelectionChangeCommitted += new System.EventHandler(this.DateCreatedcomboBox_SelectedIndexChanged);
            // 
            // bannerPanel
            // 
            this.bannerPanel.Controls.Add(this.closeButton);
            this.bannerPanel.Controls.Add(this.clearFiltersButton);
            this.bannerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.bannerPanel.Location = new System.Drawing.Point(0, 0);
            this.bannerPanel.Name = "bannerPanel";
            this.bannerPanel.Size = new System.Drawing.Size(477, 24);
            this.bannerPanel.TabIndex = 3;
            this.bannerPanel.Text = "bannerPanel";
            // 
            // closeButton
            // 
            this.closeButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.closeButton.FlatAppearance.BorderSize = 0;
            this.closeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closeButton.Image = global::FileList.Properties.Resources.closeX_16;
            this.closeButton.Location = new System.Drawing.Point(443, 0);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(34, 24);
            this.closeButton.TabIndex = 1;
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // clearFiltersButton
            // 
            this.clearFiltersButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.clearFiltersButton.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
            this.clearFiltersButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.clearFiltersButton.Location = new System.Drawing.Point(0, 0);
            this.clearFiltersButton.Name = "clearFiltersButton";
            this.clearFiltersButton.Size = new System.Drawing.Size(75, 24);
            this.clearFiltersButton.TabIndex = 0;
            this.clearFiltersButton.Text = "Clear";
            this.clearFiltersButton.UseVisualStyleBackColor = true;
            this.clearFiltersButton.Click += new System.EventHandler(this.ClearFiltersButton_Click);
            // 
            // FileFilterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(477, 245);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.bannerPanel);
            this.Name = "FileFilterForm";
            this.Text = "FileFilterForm";
            this.Load += new System.EventHandler(this.FileFilterForm_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.sizeAmount1NumericUpDown)).EndInit();
            this.sizeValue2Panel.ResumeLayout(false);
            this.sizeValue2Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sizeAmount2NumericUpDown)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.dateModifiedValue2Panel.ResumeLayout(false);
            this.dateModifiedValue2Panel.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.dateCreatedValue2Panel.ResumeLayout(false);
            this.dateCreatedValue2Panel.PerformLayout();
            this.bannerPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ComboBox sizeFilterComboBox;
        private System.Windows.Forms.NumericUpDown sizeAmount1NumericUpDown;
        private System.Windows.Forms.ComboBox sizeType1ComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel sizeValue2Panel;
        private System.Windows.Forms.ComboBox sizeType2ComboBox;
        private System.Windows.Forms.NumericUpDown sizeAmount2NumericUpDown;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox dateModifiedComboBox;
        private System.Windows.Forms.DateTimePicker dateModified1Picker;
        private System.Windows.Forms.Panel dateModifiedValue2Panel;
        private System.Windows.Forms.DateTimePicker dateModified2Picker;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Panel dateCreatedValue2Panel;
        private System.Windows.Forms.DateTimePicker dateCreated2Picker;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dateCreated1Picker;
        private System.Windows.Forms.ComboBox dateCreatedcomboBox;
        private System.Windows.Forms.Panel bannerPanel;
        private System.Windows.Forms.Button clearFiltersButton;
        private System.Windows.Forms.Button closeButton;
    }
}