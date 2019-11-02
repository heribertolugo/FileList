using FileList.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace FileList.Models
{
    public class SortButton : Button
    {
        private static readonly Dictionary<SortOrder, Image> SortOrderImage;
        private static SortDropDown SortDropDown;
        private SortOrder _sortOrder;

        static SortButton()
        {
            SortButton.SortOrderImage = new Dictionary<SortOrder, Image>()
            {
                { SortOrder.None, (Image)Resources.triangle_dkgry_right_16 }
                ,{ SortOrder.Ascending, (Image)Resources.triangle_dkgry_down_16 }
                ,{ SortOrder.Descending, (Image)Resources.triangle_dkgry_up_16 }
            };

            SortButton.SortDropDown = new SortDropDown();
        }

        public SortButton()
        {
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            this.FlatAppearance.BorderColor = Color.DarkGray;
            this.FlatStyle = FlatStyle.Flat;
            this.ImageAlign = ContentAlignment.MiddleRight;
            this.TextAlign = ContentAlignment.MiddleLeft;
            this.UseVisualStyleBackColor = true;
            this.Image = SortButton.SortOrderImage[this.SortOrder];
        }

        protected override void InitLayout()
        {
            base.InitLayout();
        }

        public SortOrder SortOrder
        {
            get
            {
                return this._sortOrder;
            }
            set
            {
                this._sortOrder = value;
                this.Image = SortButton.SortOrderImage[this._sortOrder];
            }
        }


        protected override void OnClick(EventArgs e)
        {
            bool wasVisible = SortButton.SortDropDown.Visible;
            // close and clear in case another sort button was pressed
            SortButton.SortDropDown.Hide();
            SortButton.SortDropDown.ClearAllDelegatesOfOnSortSelectedHandler();
            SortButton.SortDropDown.OnSortSelectedHandler += this.SortDropDown_OnSortSelectedHandler;
            // did the same sort button get clicked while the drop down was open?
            if (wasVisible && SortButton.SortDropDown.TargetControl == this)
                return;
            // hide sort buttons that match our current sort mode
            SortButton.SortDropDown.HideSorts(this.SortOrder);

            // if the sort image was clicked, show the drop down. otherwise, change sort by next sort in order
            if (!this.ShowDropDown(MousePosition))
            {
                int num = (int)(this.SortOrder + 1); // get the next sort
                if (!Enum.IsDefined(typeof(SortOrder), num)) // num > Enum.GetValues(typeof(SortOrder)).Length - 1)
                    num = 0;
                this.SortOrder = (SortOrder)num;
                SortButton.SortDropDown.ClearAllDelegatesOfOnSortSelectedHandler();
                base.OnClick(e);
                return;
            }
        }

        private void SortDropDown_OnSortSelectedHandler(object sender, SortSelectedEventArgs e)
        {
            this.SortOrder = e.Sort;
            SortButton.SortDropDown.Hide();
            SortButton.SortDropDown.ClearAllDelegatesOfOnSortSelectedHandler();
            base.OnClick(e);
        }

        private bool ShowDropDown(Point hitPoint)
        {
            ContentAlignment leftFlags = ContentAlignment.MiddleLeft | ContentAlignment.BottomLeft | ContentAlignment.TopLeft;
            ContentAlignment middleFlags = ContentAlignment.BottomCenter | ContentAlignment.MiddleCenter | ContentAlignment.TopCenter;
            ContentAlignment rightFlags = ContentAlignment.BottomRight | ContentAlignment.MiddleRight | ContentAlignment.TopRight;
            Rectangle imageRect = new Rectangle();
            Padding hitPadding = new Padding(5);
            bool isHit = false;
            // figure out where our image is, created a padded rectangle around it, and check if our cords are within
            if ((leftFlags & this.ImageAlign) != 0)
            {
                imageRect = new Rectangle(0 + this.Padding.Left  - hitPadding.Left, 0, this.Image.Width + hitPadding.Right, this.Height);
            }else if ((rightFlags & this.ImageAlign) != 0)
            {
                imageRect = new Rectangle(this.Width - this.Image.Width - hitPadding.Left, 0, this.Image.Width + hitPadding.Right, this.Height);
            }
            else if ((middleFlags & this.ImageAlign) != 0)
            {
                imageRect = new Rectangle((this.Width / 2) - (this.Image.Width / 2) - hitPadding.Left, 0, this.Image.Width + hitPadding.Right, this.Height);
            }

            Rectangle rect = this.RectangleToScreen(imageRect);
            isHit = rect.Contains(hitPoint);

            if (isHit)
                SortButton.SortDropDown.Show(this);
            return isHit;
        }
    }
}
