using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FileList.Views
{
    public partial class WhiteBackListForm : Form
    {
        public WhiteBackListForm()
        {
            InitializeComponent();
        }

        public ListTypeValue ListType { get; private set; }

        public IEnumerable<string> Values
        {
            get
            {
                return this.dataGridView1.Rows.Cast<DataGridViewRow>().Where(r => r.Cells[0].Value != null).Select(r => r.Cells[0].Value.ToString().Trim()).Distinct();
            }
            private set
            {

            }
        }

        private void blackListRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton button = sender as RadioButton;

            if (button.Checked)
                this.ListType = ListTypeValue.BlackList;
        }

        private void whiteListRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton button = sender as RadioButton;

            if (button.Checked)
                this.ListType = ListTypeValue.WhiteList;
        }

        private void noneRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton button = sender as RadioButton;

            if (button.Checked)
                this.ListType = ListTypeValue.None;
        }

        public enum ListTypeValue
        {
            None,
            WhiteList,
            BlackList
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            this.dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void dataGridView1_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void dataGridView1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            this.dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }
    }
}
