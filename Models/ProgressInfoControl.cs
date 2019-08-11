using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FileList.Models
{
    public partial class ProgressInfoControl : UserControl
    {
        private int _count;
        private int _goal;

        public ProgressInfoControl()
        {
            InitializeComponent();
            this._count = 0;
            this.itemsMovedTextBox.BackColor = Color.White;
            this.itemsMovedTextBox.ForeColor = Color.FromArgb(87, 166, 74);
            this.itemsMovedTextBox.BackColor = Color.Black;
        }

        public int Step
        {
            get
            {
                return this.progressBar1.Step;
            }
            set
            {
                this.progressBar1.Step = value;
            }
        }

        public int Goal
        {
            get
            {
                return this._goal;
            }
            set
            {
                this._goal = value;
                if (value <= 0)
                    return;
                this.progressBar1.Step = 100 / value;
            }
        }

        public int AddItem(object item, string message = null)
        {
            if (item != null)
                this.itemsMovedListBox.Items.Add(item);
            if (message != null)
                this.itemsMovedTextBox.Text = string.Format("{2}{0}{1}", message ?? this.itemsMovedListBox.ToString(), Environment.NewLine, this.itemsMovedTextBox.Text);
            ++this._count;
            this.itemsMovedCountLabel.Text = this._count.ToString();
            this.progressBar1.PerformStep();
            return this._count;
        }

        public void PushMessage(string message)
        {
            this.itemsMovedTextBox.Text = string.Format("{2}{0}{1}", message ?? this.itemsMovedListBox.ToString(), Environment.NewLine, this.itemsMovedTextBox.Text);
        }
    }
}
