using Common.Models;
using FileList.Models;
using System;
using System.Linq;
using System.Windows.Forms;

namespace FileList.Views
{
    public partial class FileFilterForm : DropForm
    {
        public event EventHandler<FilterSelectedEventArgs> OnFilterSelectedHandler;

        private static readonly FilterType DefaultUiFilterType = FilterType.None;
        private static readonly StorageSizeType DefaultUiStorageSize = StorageSizeType.None;
        private static EnumToUi<FilterType>[] UiFilterTypes = null;
        private static EnumToUi<StorageSizeType>[] UiStorageSizes = null;
        private bool _dataSourcesSet = false;

        public FileFilterForm()
        {
            InitializeComponent();
        }


        #region Public Properties / Methods
        public void Reset()
        {
            this.sizeFilterComboBox.SelectedValue = FileFilterForm.DefaultUiFilterType;
            this.dateModifiedComboBox.SelectedValue = FileFilterForm.DefaultUiFilterType;
            this.dateCreatedcomboBox.SelectedValue = FileFilterForm.DefaultUiFilterType;
            this.sizeType1ComboBox.SelectedValue = FileFilterForm.DefaultUiStorageSize;
            this.sizeType2ComboBox.SelectedValue = FileFilterForm.DefaultUiStorageSize;
            this.sizeAmount1NumericUpDown.Value = Decimal.Zero;
            this.sizeAmount2NumericUpDown.Value = Decimal.Zero;
            this.dateCreated1Picker.Value = DateTimePicker.MinimumDateTime;
            this.dateCreated2Picker.Value = DateTimePicker.MinimumDateTime;
            this.dateModified1Picker.Value = DateTimePicker.MinimumDateTime;
            this.dateModified2Picker.Value = DateTimePicker.MinimumDateTime;
        }

        public SizeFilter SizeFilter { get; private set; }

        public DateFilter ModifiedDateFilter { get; set; }

        public DateFilter CreatedDateFilter { get; set; }

        protected void OnFilterSelected(FilterSelectedEventArgs args)
        {
            EventHandler<FilterSelectedEventArgs> filterSelectedHandler = this.OnFilterSelectedHandler;
            if (filterSelectedHandler == null)
                return;
            filterSelectedHandler(this, args);
        }

        public bool CloseButtonVisible
        {
            get { return this.closeButton.Visible; }
            set { this.closeButton.Visible = value; }
        }

        #endregion

        #region Event Handlers

        private void FileFilterForm_Load(object sender, EventArgs e)
        {
            this.SetDataSources();
        }

        private void SizeFilterComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.HandleSizeFilterValueChanged();
        }

        private void SizeAmount1NumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            this.HandleSizeFilterValueChanged();
        }

        private void SizeType1ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.HandleSizeFilterValueChanged();
        }

        private void SizeAmount2NumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            this.HandleSizeFilterValueChanged();
        }

        private void SizeType2ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.HandleSizeFilterValueChanged();
        }

        private void DateModifiedComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!this._dataSourcesSet)
                return;
            this.HandleDateFilterValueChanged(Filter.DateModified, (FilterType)this.dateModifiedComboBox.SelectedValue, this.dateModified1Picker, this.dateModifiedValue2Panel);
        }

        private void DateModified1Picker_ValueChanged(object sender, EventArgs e)
        {
            if (!this._dataSourcesSet)
                return;
            this.HandleDateFilterValueChanged(Filter.DateModified, (FilterType)this.dateModifiedComboBox.SelectedValue, this.dateModified1Picker, this.dateModifiedValue2Panel);
        }

        private void DateModified2Picker_ValueChanged(object sender, EventArgs e)
        {
            if (!this._dataSourcesSet)
                return;
            this.HandleDateFilterValueChanged(Filter.DateModified, (FilterType)this.dateModifiedComboBox.SelectedValue, this.dateModified1Picker, this.dateModifiedValue2Panel);
        }

        private void DateCreatedcomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!this._dataSourcesSet)
                return;
            this.HandleDateFilterValueChanged(Filter.DateCreated, (FilterType)this.dateCreatedcomboBox.SelectedValue, this.dateCreated1Picker, this.dateCreatedValue2Panel);
        }

        private void DateCreated1Picker_ValueChanged(object sender, EventArgs e)
        {
            if (!this._dataSourcesSet)
                return;
            this.HandleDateFilterValueChanged(Filter.DateCreated, (FilterType)this.dateCreatedcomboBox.SelectedValue, this.dateCreated1Picker, this.dateCreatedValue2Panel);
        }

        private void DateCreated2Picker_ValueChanged(object sender, EventArgs e)
        {
            if (!this._dataSourcesSet)
                return;
            this.HandleDateFilterValueChanged(Filter.DateCreated, (FilterType)this.dateCreatedcomboBox.SelectedValue, this.dateCreated1Picker, this.dateCreatedValue2Panel);
        }

        private void ClearFiltersButton_Click(object sender, EventArgs e)
        {
            this.Reset();
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        #endregion

        #region Helpers

        private void SetDataSources()
        {
            if (FileFilterForm.UiFilterTypes == null)
            {
                FileFilterForm.UiFilterTypes = Enum.GetValues(typeof(FilterType)).Cast<FilterType>().Select(f => new EnumToUi<FilterType>(f)).ToArray();
                //FileFilterForm.DefaultUiFilterType = FilterType.None;
            }
            if (FileFilterForm.UiStorageSizes == null)
            {
                FileFilterForm.UiStorageSizes = Enum.GetValues(typeof(StorageSizeType)).Cast<StorageSizeType>().Select(s => new EnumToUi<StorageSizeType>(s)).ToArray();
                //FileFilterForm.DefaultUiStorageSize = StorageSize.None;
            }
            this.sizeFilterComboBox.DataSource = FileFilterForm.UiFilterTypes.Select(n => new EnumToUi<FilterType>(n.Value)).ToArray();
            this.dateModifiedComboBox.DataSource = FileFilterForm.UiFilterTypes.Select(n => new EnumToUi<FilterType>(n.Value)).ToArray();
            this.dateCreatedcomboBox.DataSource = FileFilterForm.UiFilterTypes.Select(n => new EnumToUi<FilterType>(n.Value)).ToArray();
            this.sizeType1ComboBox.DataSource = FileFilterForm.UiStorageSizes.Select(n => new EnumToUi<StorageSizeType>(n.Value)).ToArray();
            this.sizeType2ComboBox.DataSource = FileFilterForm.UiStorageSizes.Select(n => new EnumToUi<StorageSizeType>(n.Value)).ToArray();
            this.sizeFilterComboBox.ValueMember = "Value";
            this.sizeFilterComboBox.DisplayMember = "Friendly";
            this.dateModifiedComboBox.ValueMember = "Value";
            this.dateModifiedComboBox.DisplayMember = "Friendly";
            this.dateCreatedcomboBox.ValueMember = "Value";
            this.dateCreatedcomboBox.DisplayMember = "Friendly";
            this.sizeType1ComboBox.ValueMember = "Value";
            this.sizeType1ComboBox.DisplayMember = "Friendly";
            this.sizeType2ComboBox.ValueMember = "Value";
            this.sizeType2ComboBox.DisplayMember = "Friendly";
            this.Reset();
            this._dataSourcesSet = true;
        }
        private void HandleSizeFilterValueChanged()
        {
            if (!this._dataSourcesSet)
                return;

            FilterType filterType = (FilterType)this.sizeFilterComboBox.SelectedValue;
            StorageSizeType storageType1 = (StorageSizeType)this.sizeType1ComboBox.SelectedValue;
            StorageSizeType? storageType2 = (StorageSizeType)this.sizeType2ComboBox.SelectedValue;
            float sizeValue1 = (float)this.sizeAmount1NumericUpDown.Value;
            float? sizeValue2 = (float)this.sizeAmount2NumericUpDown.Value;
            Panel value2Panel = this.sizeValue2Panel;

            value2Panel.Visible = (filterType == FilterType.Between);

            if (filterType == FilterType.None || storageType1 == StorageSizeType.None)
            {
                filterType = FilterType.None;
                sizeValue1 = 0;
                sizeValue2 = null;
                storageType1 = StorageSizeType.None;
                storageType2 = null;
            }else if (filterType != FilterType.Between)
            {
                sizeValue2 = null;
                storageType2 = null;
            }

            this.SizeFilter = new SizeFilter(filterType, sizeValue1, storageType1, sizeValue2, storageType2);
            this.OnFilterSelected(new FilterSelectedEventArgs(Filter.Size, filterType,
                                    Common.Helpers.FileStorageSize.ConvertStorageValueToKb(sizeValue1, storageType1)
                                    , Common.Helpers.FileStorageSize.ConvertStorageValueToKb(sizeValue2.HasValue ? sizeValue2.Value : 0.0f
                                    , storageType2.HasValue ? storageType2.Value : StorageSizeType.None)));
        }

        private void HandleDateFilterValueChanged(Filter filter, FilterType filterType, DateTimePicker dateTime1, Panel dateTime2Panel)
        {
            DateTime? dateTime1_1 = new DateTime?(dateTime1.Value);
            DateTime? dateTime2 = new DateTime?((dateTime2Panel.Controls.Cast<Control>().FirstOrDefault(c => c.GetType().Equals(typeof(DateTimePicker))) as DateTimePicker).Value);
            dateTime2Panel.Visible = filterType == FilterType.Between;
            dateTime2 = dateTime2Panel.Visible ? dateTime2 : null;
            DateFilter dateFilter = new DateFilter(filterType, dateTime1_1, dateTime2);
            FilterSelectedEventArgs args = new FilterSelectedEventArgs(filter, dateFilter.FilterType, dateFilter.DateTime1, dateFilter.DateTime2);

            if (filter == Filter.DateCreated)
            {
                this.CreatedDateFilter = dateFilter;
            }
            else
            {
                if (filter != Filter.DateModified)
                    throw new ArgumentException("Invalid filter");
                this.ModifiedDateFilter = dateFilter;
            }
            this.OnFilterSelected(args);
        }
        #endregion
    }

    public class FilterSelectedEventArgs : EventArgs
    {
        public FilterSelectedEventArgs(Filter filter, FilterType filterType, object value1, object value2)
        {
            this.Filter = filter;
            this.FilterType = filterType;
            this.Value1 = value1;
            this.Value2 = value2;
        }

        public Filter Filter { get; private set; }

        public FilterType FilterType { get; private set; }

        public object Value1 { get; private set; }

        public object Value2 { get; private set; }
    }
}
