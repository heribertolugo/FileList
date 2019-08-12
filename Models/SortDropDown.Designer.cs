namespace FileList.Models
{
    partial class SortDropDown
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
            this.ascendingButton = new System.Windows.Forms.Button();
            this.noneButton = new System.Windows.Forms.Button();
            this.descendingButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ascendingButton
            // 
            this.ascendingButton.Image = global::FileList.Properties.Resources.triangle_dkgry_down_16;
            this.ascendingButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ascendingButton.Location = new System.Drawing.Point(3, 3);
            this.ascendingButton.Name = "ascendingButton";
            this.ascendingButton.Size = new System.Drawing.Size(88, 23);
            this.ascendingButton.TabIndex = 0;
            this.ascendingButton.Text = "Ascending";
            this.ascendingButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ascendingButton.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.ascendingButton.UseVisualStyleBackColor = true;
            this.ascendingButton.Click += new System.EventHandler(this.AscendingButton_Click);
            // 
            // noneButton
            // 
            this.noneButton.Image = global::FileList.Properties.Resources.triangle_dkgry_right_16;
            this.noneButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.noneButton.Location = new System.Drawing.Point(3, 32);
            this.noneButton.Name = "noneButton";
            this.noneButton.Size = new System.Drawing.Size(88, 23);
            this.noneButton.TabIndex = 1;
            this.noneButton.Text = "None";
            this.noneButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.noneButton.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.noneButton.UseVisualStyleBackColor = true;
            this.noneButton.Click += new System.EventHandler(this.NoneButton_Click);
            // 
            // descendingButton
            // 
            this.descendingButton.Image = global::FileList.Properties.Resources.triangle_dkgry_up_16;
            this.descendingButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.descendingButton.Location = new System.Drawing.Point(3, 61);
            this.descendingButton.Name = "descendingButton";
            this.descendingButton.Size = new System.Drawing.Size(88, 23);
            this.descendingButton.TabIndex = 2;
            this.descendingButton.Text = "Descending";
            this.descendingButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.descendingButton.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.descendingButton.UseVisualStyleBackColor = true;
            this.descendingButton.Click += new System.EventHandler(this.DescendingButton_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.ascendingButton, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.descendingButton, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.noneButton, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(94, 87);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // SortDropDown
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(96, 88);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "SortDropDown";
            this.Text = "SortDropDown";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ascendingButton;
        private System.Windows.Forms.Button noneButton;
        private System.Windows.Forms.Button descendingButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}