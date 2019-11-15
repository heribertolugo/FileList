using FileList.Models;
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
    public partial class SearchOptionsForm : Common.Models.DropForm
    {
        private static Color ActiveButtonColor = Color.FromKnownColor(KnownColor.Control);
        private static Color UnactiveButtonColor = Color.FromKnownColor(KnownColor.ControlDark);
        private FileFilterForm fileFilter;
        private WhiteBackListForm whiteBackList;
        public SearchOptionsForm()
        {
            InitializeComponent();
            this.fileFilter = new FileFilterForm() { CloseButtonVisible = false };
            this.whiteBackList = new WhiteBackListForm();

            this.SetChildProperties(this.fileFilter);
            this.SetChildProperties(this.whiteBackList);
        }

        public SearchOption SearchOption
        {
            get { return new SearchOption(
                this.whiteBackList.Values.Select(s => s.StartsWith(".") || s.Equals(string.Empty) ? s : string.Concat(".", s)).ToArray(),
                this.whiteBackList.ListType,
                this.fileFilter.SizeFilter,
                this.fileFilter.CreatedDateFilter,
                this.fileFilter.ModifiedDateFilter
                ); }
            private set { }
        }

        public int Threads { get { return (int)this.threadCountUpDown.Value; } }

        private Func<Common.Models.FileData, bool> GetSearchFilter()
        {
            string[] extensions = this.whiteBackList.Values.Select(s => s.StartsWith(".") ? s : string.Concat(".", s)).ToArray();
            WhiteBackListForm.ListTypeValue listType = this.whiteBackList.ListType;
            SizeFilter sizeFilter = this.fileFilter.SizeFilter;
            DateFilter dateCreatedFilter = this.fileFilter.CreatedDateFilter;
            DateFilter dateModifiedFilter = this.fileFilter.ModifiedDateFilter;


            return (f) =>
            {
                if (extensions.Length > 0)
                    switch (listType)
                    {
                        case WhiteBackListForm.ListTypeValue.WhiteList:
                            if (!extensions.Contains(f.Extension))
                                return false;
                            break;
                        case WhiteBackListForm.ListTypeValue.BlackList:
                            if (extensions.Contains(f.Extension))
                                return false;
                            break;
                    }

                switch (sizeFilter.FilterType)
                {
                    case FilterType.Between:
                        if (f.SizeInKilobytes < Common.Helpers.FileStorageSize.ConvertStorageValueToKb(sizeFilter.Value1, sizeFilter.StorageSize1)
                            || (sizeFilter.Value2.HasValue && sizeFilter.StorageSize2.HasValue
                                && f.SizeInKilobytes > Common.Helpers.FileStorageSize.ConvertStorageValueToKb(sizeFilter.Value2.Value, sizeFilter.StorageSize2.Value))
                        )                
                            return false;
                        break;
                    case FilterType.LessThan:
                        if (f.SizeInKilobytes >= Common.Helpers.FileStorageSize.ConvertStorageValueToKb(sizeFilter.Value1, sizeFilter.StorageSize1))
                            return false;
                        break;
                    case FilterType.GreaterThan:
                        if (f.SizeInKilobytes <= Common.Helpers.FileStorageSize.ConvertStorageValueToKb(sizeFilter.Value1, sizeFilter.StorageSize1))
                            return false;
                        break;
                    case FilterType.Equals:
                        if (f.SizeInKilobytes != Common.Helpers.FileStorageSize.ConvertStorageValueToKb(sizeFilter.Value1, sizeFilter.StorageSize1))
                            return false;
                        break;
                }

                if (f.DateCreated.HasValue && dateCreatedFilter.DateTime1.HasValue)
                    switch (dateCreatedFilter.FilterType)
                    {
                        case FilterType.Between:
                            if (f.DateCreated.Value < dateCreatedFilter.DateTime1.Value
                                || (dateCreatedFilter.DateTime2.HasValue && f.DateCreated.Value > dateCreatedFilter.DateTime2.Value))
                                return false;
                            break;
                        case FilterType.LessThan:
                            if (f.DateCreated.Value >= dateCreatedFilter.DateTime1.Value)
                                return false;
                            break;
                        case FilterType.GreaterThan:
                            if (f.DateCreated.Value <= dateCreatedFilter.DateTime1.Value)
                                return false;
                            break;
                        case FilterType.Equals:
                            if (f.DateCreated.Value.Date != dateCreatedFilter.DateTime1.Value.Date)
                                return false;
                            break;
                    }

                if (f.DateModified.HasValue && dateModifiedFilter.DateTime1.HasValue)
                    switch (dateModifiedFilter.FilterType)
                    {
                        case FilterType.Between:
                            if (f.DateModified.Value < dateModifiedFilter.DateTime1.Value
                                || (dateModifiedFilter.DateTime2.HasValue && f.DateModified.Value > dateModifiedFilter.DateTime2.Value))
                                return false;
                            break;
                        case FilterType.LessThan:
                            if (f.DateModified.Value >= dateModifiedFilter.DateTime1.Value)
                                return false;
                            break;
                        case FilterType.GreaterThan:
                            if (f.DateModified.Value <= dateModifiedFilter.DateTime1.Value)
                                return false;
                            break;
                        case FilterType.Equals:
                            if (f.DateModified.Value.Date != dateModifiedFilter.DateTime1.Value.Date)
                                return false;
                            break;
                    }

                return true;
            };
        }

        private void SearchOptionsForm_Load(object sender, EventArgs e)
        {
            this.fileFilter.Show();
            this.whiteBackList.Show();

            this.ActivateForm(this.fileFilter);
        }

        private void SearchOptionsForm_MdiChildActivate(object sender, EventArgs e)
        {
            this.propertiesButton.BackColor = this.ActiveMdiChild == this.fileFilter ? SearchOptionsForm.ActiveButtonColor : SearchOptionsForm.UnactiveButtonColor;
            this.extensionsButton.BackColor = this.ActiveMdiChild == this.whiteBackList ? SearchOptionsForm.ActiveButtonColor : SearchOptionsForm.UnactiveButtonColor;
            this.propertiesButton.FlatAppearance.BorderSize = this.propertiesButton.BackColor == SearchOptionsForm.ActiveButtonColor ? 0 : 1;
            this.extensionsButton.FlatAppearance.BorderSize = this.extensionsButton.BackColor == SearchOptionsForm.ActiveButtonColor ? 0 : 1;
        }

        private void propertiesButton_Click(object sender, EventArgs e)
        {
            this.ActivateForm(this.fileFilter);
        }

        private void extensionsButton_Click(object sender, EventArgs e)
        {
            this.ActivateForm(this.whiteBackList);
        }

        private void ActivateForm(Form form)
        {
            //form.WindowState = FormWindowState.Maximized;
            form.BringToFront();
            this.ActivateMdiChild(form);
        }

        private void SetChildProperties(Form form)
        {
            form.MdiParent = this;
            form.ControlBox = false;
            form.MaximizeBox = false;
            form.MinimizeBox = false;
            form.ShowIcon = false;
            form.Text = string.Empty;
            form.Dock = DockStyle.Fill;
            form.FormBorderStyle = FormBorderStyle.None;
            form.WindowState = FormWindowState.Normal;
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void threadCountUpDown_ValueChanged(object sender, EventArgs e)
        {

        }
    }

    public struct SearchOption
    {
        private string[] _extensions;
        private WhiteBackListForm.ListTypeValue _listType;
        private SizeFilter _sizeFilter;
        private DateFilter _dateCreatedFilter;
        private DateFilter _dateModifiedFilter;
        public SearchOption(string[] extensions, WhiteBackListForm.ListTypeValue listType, SizeFilter sizeFilter, DateFilter dateCreatedFilter, DateFilter dateModifiedFilter)
        {
            this._extensions = extensions;
            this._listType = listType;
            this._sizeFilter = sizeFilter;
            this._dateCreatedFilter = dateCreatedFilter;
            this._dateModifiedFilter = dateModifiedFilter;
        }

        public bool ValidateFile(Common.Models.FileData fileData)
        {
            Common.Models.FileData f = fileData;

            if (this._extensions.Length > 0)
                switch (this._listType)
                {
                    case WhiteBackListForm.ListTypeValue.WhiteList:
                        if (!this._extensions.Contains(f.Extension))
                            return false;
                        break;
                    case WhiteBackListForm.ListTypeValue.BlackList:
                        if (this._extensions.Contains(f.Extension))
                            return false;
                        break;
                }

            switch (this._sizeFilter.FilterType)
            {
                case FilterType.Between:
                    if (f.SizeInKilobytes < Common.Helpers.FileStorageSize.ConvertStorageValueToKb(this._sizeFilter.Value1, this._sizeFilter.StorageSize1)
                        || (this._sizeFilter.Value2.HasValue && this._sizeFilter.StorageSize2.HasValue
                            && f.SizeInKilobytes > Common.Helpers.FileStorageSize.ConvertStorageValueToKb(this._sizeFilter.Value2.Value, this._sizeFilter.StorageSize2.Value))
                    )
                        return false;
                    break;
                case FilterType.LessThan:
                    if (f.SizeInKilobytes >= Common.Helpers.FileStorageSize.ConvertStorageValueToKb(this._sizeFilter.Value1, this._sizeFilter.StorageSize1))
                        return false;
                    break;
                case FilterType.GreaterThan:
                    if (f.SizeInKilobytes <= Common.Helpers.FileStorageSize.ConvertStorageValueToKb(this._sizeFilter.Value1, this._sizeFilter.StorageSize1))
                        return false;
                    break;
                case FilterType.Equals:
                    if (f.SizeInKilobytes != Common.Helpers.FileStorageSize.ConvertStorageValueToKb(this._sizeFilter.Value1, this._sizeFilter.StorageSize1))
                        return false;
                    break;
            }

            if (f.DateCreated.HasValue && this._dateCreatedFilter.DateTime1.HasValue)
                switch (this._dateCreatedFilter.FilterType)
                {
                    case FilterType.Between:
                        if (f.DateCreated.Value < this._dateCreatedFilter.DateTime1.Value
                            || (this._dateCreatedFilter.DateTime2.HasValue && f.DateCreated.Value > this._dateCreatedFilter.DateTime2.Value))
                            return false;
                        break;
                    case FilterType.LessThan:
                        if (f.DateCreated.Value >= this._dateCreatedFilter.DateTime1.Value)
                            return false;
                        break;
                    case FilterType.GreaterThan:
                        if (f.DateCreated.Value <= this._dateCreatedFilter.DateTime1.Value)
                            return false;
                        break;
                    case FilterType.Equals:
                        if (f.DateCreated.Value.Date != this._dateCreatedFilter.DateTime1.Value.Date)
                            return false;
                        break;
                }

            if (f.DateModified.HasValue && this._dateModifiedFilter.DateTime1.HasValue)
                switch (this._dateModifiedFilter.FilterType)
                {
                    case FilterType.Between:
                        if (f.DateModified.Value < this._dateModifiedFilter.DateTime1.Value
                            || (this._dateModifiedFilter.DateTime2.HasValue && f.DateModified.Value > this._dateModifiedFilter.DateTime2.Value))
                            return false;
                        break;
                    case FilterType.LessThan:
                        if (f.DateModified.Value >= this._dateModifiedFilter.DateTime1.Value)
                            return false;
                        break;
                    case FilterType.GreaterThan:
                        if (f.DateModified.Value <= this._dateModifiedFilter.DateTime1.Value)
                            return false;
                        break;
                    case FilterType.Equals:
                        if (f.DateModified.Value.Date != this._dateModifiedFilter.DateTime1.Value.Date)
                            return false;
                        break;
                }

            return true;
        }
    }
}
