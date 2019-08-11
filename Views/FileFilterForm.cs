using FileList.Logic;
using FileList.Models;
using FileList.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace FileList.Views
{
    public partial class FileFilterForm : Form
    {
        public event EventHandler<FilterSelectedEventArgs> OnFilterSelectedHandler;

        private const int CS_DROPSHADOW = 131072;
        private const int WM_NCPAINT = 133;
        private const int WM_NCHITTEST = 132;
        private const int WM_ACTIVATEAPP = 28;
        private const int HTCAPTION = 2;
        private const int HTCLIENT = 1;

        private static readonly FilterType DefaultUiFilterType = FilterType.None;
        private static readonly StorageSize DefaultUiStorageSize = StorageSize.None;
        private static EnumToUi<FilterType>[] UiFilterTypes = null;
        private static EnumToUi<StorageSize>[] UiStorageSizes = null;
        private bool isAeroEnabled;
        private bool _dataSourcesSet = false;
        private Control _ownerControl;

        public FileFilterForm()
        {
            InitializeComponent();
        }


        #region Public Properties / Methods
        public void Show(Control target)
        {
            Rectangle screen = target.RectangleToScreen(target.ClientRectangle);
            Form form = target.FindForm();
            this._ownerControl = target;
            form.Move -= new EventHandler(this.Owner_Move);
            form.Move += new EventHandler(this.Owner_Move);
            form.Resize -= new EventHandler(this.TargetForm_Resize);
            form.Resize += new EventHandler(this.TargetForm_Resize);
            this.Location = new Point(screen.X, screen.Bottom);
            this.TopMost = true;
            this.Show();
        }

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
            filterSelectedHandler((object)this, args);
        }

        #endregion

        #region Event Handlers

        private void FileFilterForm_Load(object sender, EventArgs e)
        {
            this.SetDataSources();
            this.ShowInTaskbar = false;
        }

        private void TargetForm_Resize(object sender, EventArgs e)
        {
            Form form = sender as Form;
            switch (form.WindowState)
            {
                case FormWindowState.Normal:
                    this.WindowState = form.WindowState;
                    this.Show(this._ownerControl);
                    break;
                case FormWindowState.Minimized:
                    this.WindowState = form.WindowState;
                    break;
                case FormWindowState.Maximized:
                    this.WindowState = FormWindowState.Normal;
                    this.Show(this._ownerControl);
                    break;
                default:
                    this.WindowState = form.WindowState;
                    break;
            }
        }

        private void Owner_Move(object sender, EventArgs e)
        {
            Rectangle screen = this._ownerControl.RectangleToScreen(this._ownerControl.ClientRectangle);
            this.Location = new Point(screen.X, screen.Bottom);
            this.TopMost = true;
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
                FileFilterForm.UiStorageSizes = Enum.GetValues(typeof(StorageSize)).Cast<StorageSize>().Select(s => new EnumToUi<StorageSize>(s)).ToArray();
                //FileFilterForm.DefaultUiStorageSize = StorageSize.None;
            }
            this.sizeFilterComboBox.DataSource = FileFilterForm.UiFilterTypes.Select(n => new EnumToUi<FilterType>(n.Value)).ToArray();
            this.dateModifiedComboBox.DataSource = FileFilterForm.UiFilterTypes.Select(n => new EnumToUi<FilterType>(n.Value)).ToArray();
            this.dateCreatedcomboBox.DataSource = FileFilterForm.UiFilterTypes.Select(n => new EnumToUi<FilterType>(n.Value)).ToArray();
            this.sizeType1ComboBox.DataSource = FileFilterForm.UiStorageSizes.Select(n => new EnumToUi<StorageSize>(n.Value)).ToArray();
            this.sizeType2ComboBox.DataSource = FileFilterForm.UiStorageSizes.Select(n => new EnumToUi<StorageSize>(n.Value)).ToArray();
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

            FilterType selectedFilterType = (FilterType)this.sizeFilterComboBox.SelectedValue;
            StorageSize storageType1 = (StorageSize)this.sizeType1ComboBox.SelectedValue;
            StorageSize? storageType2 = null;
            int sizeValue1 = (int)this.sizeAmount1NumericUpDown.Value;
            int? sizeValue2 = null;

            if (selectedFilterType == FilterType.None || storageType1 == StorageSize.None)
            {
                selectedFilterType = FilterType.None;
                sizeValue1 = 0;
                sizeValue2 = null;
                storageType1 = StorageSize.None;
                storageType2 = null;
            }

            if (selectedFilterType == FilterType.Between)
            {
                this.sizeValue2Panel.Visible = true;
                sizeValue2 = (int?)this.sizeAmount2NumericUpDown.Value;
                storageType2 = (StorageSize?)this.sizeType2ComboBox.SelectedValue;
            }
            else
            {
                this.sizeValue2Panel.Visible = false;
            }

            this.SizeFilter = new SizeFilter(selectedFilterType, sizeValue1, storageType1, sizeValue2, storageType2);
            this.OnFilterSelected(new FilterSelectedEventArgs(Filter.Size, selectedFilterType, 
                                    Misc.ConvertStorageValueToKb((float)sizeValue1, storageType1)
                                    , Misc.ConvertStorageValueToKb(sizeValue2.HasValue ? (float)sizeValue2.Value : 0.0f
                                    , storageType2.HasValue ? storageType2.Value : StorageSize.None)));
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

        #region Win32 - drop shadow
        [DllImport("Gdi32.dll")]
        private static extern IntPtr CreateRoundRectRgn(
          int nLeftRect,
          int nTopRect,
          int nRightRect,
          int nBottomRect,
          int nWidthEllipse,
          int nHeightEllipse);

        [DllImport("dwmapi.dll")]
        public static extern int DwmExtendFrameIntoClientArea(
          IntPtr hWnd,
          ref Win32MARGINS pMarInset);

        [DllImport("dwmapi.dll")]
        public static extern int DwmSetWindowAttribute(
          IntPtr hwnd,
          int attr,
          ref int attrValue,
          int attrSize);

        [DllImport("dwmapi.dll")]
        public static extern int DwmIsCompositionEnabled(ref int pfEnabled);

        protected override CreateParams CreateParams
        {
            get
            {
                this.isAeroEnabled = this.CheckAeroEnabled();
                CreateParams createParams = base.CreateParams;
                if (!this.isAeroEnabled)
                    createParams.ClassStyle |= CS_DROPSHADOW;
                return createParams;
            }
        }

        private bool CheckAeroEnabled()
        {
            if (Environment.OSVersion.Version.Major < 6)
                return false;
            int pfEnabled = 0;
            FileFilterForm.DwmIsCompositionEnabled(ref pfEnabled);
            return pfEnabled == HTCLIENT;
        }
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_NCPAINT && this.isAeroEnabled)
            {
                int attrValue = HTCAPTION;
                FileFilterForm.DwmSetWindowAttribute(this.Handle, HTCAPTION, ref attrValue, 4);
                Win32MARGINS pMarInset = new Win32MARGINS()
                {
                    bottomHeight = 1,
                    leftWidth = 1,
                    rightWidth = 1,
                    topHeight = 1
                };
                FileFilterForm.DwmExtendFrameIntoClientArea(this.Handle, ref pMarInset);
            }
            base.WndProc(ref m);
            if (m.Msg != WM_NCHITTEST || (int)m.Result != HTCLIENT)
                return;
            m.Result = (IntPtr)HTCAPTION;
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
