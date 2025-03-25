using System;
using System.Drawing;
using System.Windows.Forms;
using RealEstateApp.Models;
using RealEstateApp.Utils;

namespace RealEstateApp.Controls
{
    public class PropertyGridItem : UserControl
    {
        private PictureBox pictureBox;
        private Label titleLabel;
        private Label priceLabel;
        private Label detailsLabel;
        private Label locationLabel;

        public PropertyListing Listing { get; private set; }

        public event EventHandler<PropertyListing> ItemClicked;

        public PropertyGridItem(PropertyListing listing)
        {
            Listing = listing;
            InitializeComponent();
            PopulateData();
        }

        private void InitializeComponent()
        {
            // Configure the control
            this.Size = new Size(250, 300);
            this.BackColor = Color.White;
            this.BorderStyle = BorderStyle.FixedSingle;
            this.Margin = new Padding(5);
            this.Cursor = Cursors.Hand;

            // Main container
            TableLayoutPanel mainPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2,
                Padding = new Padding(5),
                CellBorderStyle = TableLayoutPanelCellBorderStyle.None
            };

            mainPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 60F));  // Image
            mainPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 40F));  // Info

            // Image
            pictureBox = new PictureBox
            {
                Dock = DockStyle.Fill,
                SizeMode = PictureBoxSizeMode.Zoom,
                Margin = new Padding(5)
            };

            // Info panel
            TableLayoutPanel infoPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 4,
                Padding = new Padding(3)
            };

            infoPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));  // Title
            infoPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));  // Price
            infoPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));  // Details
            infoPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));  // Location

            // Labels
            titleLabel = new Label
            {
                AutoSize = false,
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft
            };

            priceLabel = new Label
            {
                AutoSize = false,
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.Green,
                TextAlign = ContentAlignment.MiddleLeft
            };

            detailsLabel = new Label
            {
                AutoSize = false,
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 8F),
                TextAlign = ContentAlignment.MiddleLeft
            };

            locationLabel = new Label
            {
                AutoSize = false,
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 8F),
                TextAlign = ContentAlignment.MiddleLeft
            };

            // Add to panels
            infoPanel.Controls.Add(titleLabel, 0, 0);
            infoPanel.Controls.Add(priceLabel, 0, 1);
            infoPanel.Controls.Add(detailsLabel, 0, 2);
            infoPanel.Controls.Add(locationLabel, 0, 3);

            mainPanel.Controls.Add(pictureBox, 0, 0);
            mainPanel.Controls.Add(infoPanel, 0, 1);

            this.Controls.Add(mainPanel);

            // Events
            this.Click += PropertyGridItem_Click;
            mainPanel.Click += PropertyGridItem_Click;
            pictureBox.Click += PropertyGridItem_Click;
            titleLabel.Click += PropertyGridItem_Click;
            priceLabel.Click += PropertyGridItem_Click;
            detailsLabel.Click += PropertyGridItem_Click;
            locationLabel.Click += PropertyGridItem_Click;
        }

        private void PopulateData()
        {
            if (Listing == null)
                return;

            titleLabel.Text = Listing.Title;
            priceLabel.Text = Listing.FormattedPrice;

            string details = "";
            if (Listing.Rooms > 0)
                details += $"{Listing.Rooms} otaq, ";

            if (Listing.Area > 0)
                details += $"{Listing.FormattedArea}, ";

            if (Listing.Floor > 0 && Listing.TotalFloors > 0)
                details += $"{Listing.Floor}/{Listing.TotalFloors}, ";

            // Remove trailing comma and space
            if (details.EndsWith(", "))
                details = details.Substring(0, details.Length - 2);

            detailsLabel.Text = details;
            locationLabel.Text = Listing.FormattedLocation;

            // Load image if available
            if (Listing.ImageUrls.Count > 0)
            {
                ImageLoader.LoadImageIntoPictureBox(Listing.ImageUrls[0], pictureBox);
            }
        }

        private void PropertyGridItem_Click(object sender, EventArgs e)
        {
            ItemClicked?.Invoke(this, Listing);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Add a border
            using (Pen pen = new Pen(Color.LightGray, 1))
            {
                e.Graphics.DrawRectangle(pen, 0, 0, Width - 1, Height - 1);
            }
        }
    }
}
