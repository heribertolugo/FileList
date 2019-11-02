using Common.Models;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace FileList.Models
{
    public partial class SortDropDown : DropForm
    {
        public event EventHandler<SortSelectedEventArgs> OnSortSelectedHandler;

        public SortDropDown()
        {
            InitializeComponent();
            this.ascendingButton.Tag = System.Windows.Forms.SortOrder.Ascending;
            this.noneButton.Tag = System.Windows.Forms.SortOrder.None;
            this.descendingButton.Tag = System.Windows.Forms.SortOrder.Descending;
        }

        public void HideSorts(SortOrder sorts)
        {
            foreach (Button button in this.tableLayoutPanel1.Controls.Cast<Control>().Where(c => c is Button))
            {
                SortOrder? s = button.Tag as SortOrder?;
                if (!s.HasValue)
                    continue;

                //button.Visible = ((sorts & s.Value) == 0);
                button.Visible = sorts != s.Value;
            }
        }

        public void ClearAllDelegatesOfOnSortSelectedHandler()
        {
            if (OnSortSelectedHandler == null)
                return;
            foreach (Delegate d in OnSortSelectedHandler.GetInvocationList())
                OnSortSelectedHandler -= (EventHandler<SortSelectedEventArgs>)d;
        }

        protected void OnSortSelected(SortSelectedEventArgs args)
        {
            EventHandler<SortSelectedEventArgs> sortSelectedHandler = this.OnSortSelectedHandler;
            if (sortSelectedHandler == null)
                return;
            sortSelectedHandler(this, args);
        }

        private void AscendingButton_Click(object sender, EventArgs e)
        {
            SortOrder sort = (SortOrder)(sender as Button).Tag;
            this.OnSortSelected(new SortSelectedEventArgs(sort));
        }

        private void NoneButton_Click(object sender, EventArgs e)
        {
            SortOrder sort = (SortOrder)(sender as Button).Tag;
            this.OnSortSelected(new SortSelectedEventArgs(sort));
        }

        private void DescendingButton_Click(object sender, EventArgs e)
        {
            SortOrder sort = (SortOrder)(sender as Button).Tag;
            this.OnSortSelected(new SortSelectedEventArgs(sort));
        }
    }
    public class SortSelectedEventArgs : EventArgs
    {
        private SortOrder _sort;
        public SortSelectedEventArgs(SortOrder sort)
        {
            this._sort = sort;
        }

        public SortOrder Sort
        {
            get { return this._sort; }
            private set { }
        }
    }
}
