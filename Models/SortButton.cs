using FileList.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FileList.Models
{
    public class SortButton : Button
    {
        private static readonly Dictionary<SortOrder, Image> SortOrderImage;
        private SortOrder _sortOrder;

        static SortButton()
        {
            SortButton.SortOrderImage = new Dictionary<SortOrder, Image>()
            {
                { SortOrder.None, (Image)Resources.triangle_dkgry_right_16 }
                ,{ SortOrder.Ascending, (Image)Resources.triangle_dkgry_up_16 }
                ,{ SortOrder.Descending, (Image)Resources.triangle_dkgry_down_16 }
            };
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
            int num = (int)(this.SortOrder + 1);
            if (!Enum.IsDefined(typeof(SortOrder), num)) // num > Enum.GetValues(typeof(SortOrder)).Length - 1)
                num = 0;
            this.SortOrder = (SortOrder)num;
            base.OnClick(e);
        }
    }
}
