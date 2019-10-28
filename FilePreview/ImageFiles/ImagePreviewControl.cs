using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FilePreview.ImageFiles
{
    public partial class ImagePreviewControl : UserControl
    {
        private CheckBox previousButton = null;

        public ImagePreviewControl()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            this.CreateImageLayoutButtons();
            base.OnLoad(e);
        }

        internal bool SetImage(string path)
        {
            bool success = ImagePreviewControl.DisplayImagePreview(path, this.imageViewerPanel, ImageLayout.None);
            this.SmartImageLayout(this.imageViewerPanel, this.imageDisplayStylePanel);

            return success;
        }

        public void Clear()
        {
            this.imageViewerPanel.BackgroundImage = null;
        }

        private static bool DisplayImagePreview(string path, Control control, ImageLayout imageLayout)
        {
            if (path == null)
            {
                control.BackgroundImage = null;
                return true;
            }
            else
            {
                try
                {
                    control.BackgroundImage = new Bitmap(path);
                    control.BackgroundImageLayout = imageLayout;
                    return true;
                }
                catch (Exception ex)
                {
                    control.BackgroundImage = null;
                    return false;
                }
            }
        }

        private void SmartImageLayout(Control control, Control buttonContainer)
        {
            if (control.BackgroundImage == null)
                return;
            ImageLayout imageLayout = control.BackgroundImage.Width <= control.Width && control.BackgroundImage.Height <= control.Height ? (control.BackgroundImage.Width > control.Width / 4 && control.BackgroundImage.Height >= control.Height / 4 ? ImageLayout.Zoom : ImageLayout.Center) : ImageLayout.Zoom;
            control.BackgroundImageLayout = imageLayout;
            CheckBox checkBox = buttonContainer.Controls[0].Controls.Find(string.Format("{0}Button", Enum.GetName(typeof(ImageLayout), imageLayout)), true).FirstOrDefault() as CheckBox;
            if (this.previousButton != null)
                this.previousButton.CheckState = CheckState.Unchecked;
            checkBox.CheckState = CheckState.Checked;
            this.previousButton = checkBox;
        }

        private void ImageLayoutButton_Click(object sender, EventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox == this.previousButton)
            {
                checkBox.Checked = !this.previousButton.Checked;
            }
            else
            {
                if (this.previousButton != null)
                    this.previousButton.CheckState = CheckState.Unchecked;
                if (checkBox == null)
                    return;
                this.previousButton = checkBox;
                this.imageViewerPanel.BackgroundImageLayout = (ImageLayout)checkBox.Tag;
            }
        }

        private void CreateImageLayoutButtons()
        {
            TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
            int column = 0;
            tableLayoutPanel.Dock = DockStyle.Fill;
            tableLayoutPanel.RowCount = 1;
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));

            foreach (ImageLayout imageLayout in Enum.GetValues(typeof(ImageLayout)))
            {
                CheckBox layoutButton = new CheckBox();
                layoutButton.Text = Enum.GetName(typeof(ImageLayout), imageLayout);
                layoutButton.Dock = DockStyle.Fill;
                layoutButton.FlatStyle = FlatStyle.Flat;
                layoutButton.Tag = imageLayout;
                layoutButton.Appearance = Appearance.Button;
                layoutButton.TextAlign = ContentAlignment.MiddleCenter;
                layoutButton.FlatAppearance.CheckedBackColor = Color.FromKnownColor(KnownColor.DarkGray);
                layoutButton.Name = this.Name = string.Format("{0}Button", layoutButton.Text);
                layoutButton.Click += new EventHandler(this.ImageLayoutButton_Click);
                ++tableLayoutPanel.ColumnCount;
                tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
                tableLayoutPanel.Controls.Add(layoutButton, column, 0);
                ++column;
            }
            this.imageDisplayStylePanel.Controls.Add(tableLayoutPanel);

            for (int index = 0; index < tableLayoutPanel.ColumnStyles.Count; ++index)
                tableLayoutPanel.ColumnStyles[index] = new ColumnStyle(SizeType.Percent, 100f / (float)column);
        }
    }
}
