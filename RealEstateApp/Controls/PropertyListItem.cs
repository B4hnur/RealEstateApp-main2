using System;
using System.Drawing;
using System.Windows.Forms;
using RealEstateApp.Models;
using RealEstateApp.Utils;

namespace RealEstateApp.Controls
{
    public class PropertyListItem : UserControl
    {
        private PictureBox pictureBox;
        private Label titleLabel;
        private Label priceLabel;
        private Label detailsLabel;
        private Label locationLabel;
        private Label dateLabel;

        public PropertyListing Listing { get; private set; }

        public event EventHandler<PropertyListing> ItemClicked;

        public PropertyListItem(PropertyListing listing)
        {
            Listing = listing;
            InitializeComponent();
            PopulateData();
        }

        private void InitializeComponent()
        {
            // Configure the control
            this.Size = new Size(800, 120);
            this.BackColor = Color.White;
            this.BorderStyle = BorderStyle.FixedSingle;
            this.Margin = new Padding(3, 5, 3, 5);
            this.Cursor = Cursors.Hand;

            // Main container
            TableLayoutPanel mainPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                Padding = new Padding(5),
                CellBorderStyle = TableLayoutPanelCellBorderStyle.None
            };

            mainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 140F));
            mainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));

            // Image
            pictureBox = new PictureBox
            {
                Size = new Size(120, 110),
                SizeMode = PictureBoxSizeMode.Zoom,
                Dock = DockStyle.Fill,
                Margin = new Padding(5)
            };

            // Info panel
            TableLayoutPanel infoPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 5,
                Padding = new Padding(5)
            };

            infoPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));  // Title
            infoPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));  // Price
            infoPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));  // Details
            infoPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));  // Location
            infoPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));  // Date

            // Labels
            titleLabel = new Label
            {
                AutoSize = false,
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft
            };

            priceLabel = new Label
            {
                AutoSize = false,
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.Green,
                TextAlign = ContentAlignment.MiddleLeft
            };

            detailsLabel = new Label
            {
                AutoSize = false,
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 9F),
                TextAlign = ContentAlignment.MiddleLeft
            };

            locationLabel = new Label
            {
                AutoSize = false,
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 9F),
                TextAlign = ContentAlignment.MiddleLeft
            };

            dateLabel = new Label
            {
                AutoSize = false,
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 8F),
                ForeColor = Color.Gray,
                TextAlign = ContentAlignment.MiddleLeft
            };

            // Add to panels
            infoPanel.Controls.Add(titleLabel, 0, 0);
            infoPanel.Controls.Add(priceLabel, 0, 1);
            infoPanel.Controls.Add(detailsLabel, 0, 2);
            infoPanel.Controls.Add(locationLabel, 0, 3);
            infoPanel.Controls.Add(dateLabel, 0, 4);

            mainPanel.Controls.Add(pictureBox, 0, 0);
            mainPanel.Controls.Add(infoPanel, 1, 0);

            this.Controls.Add(mainPanel);

            // Events
            this.Click += PropertyListItem_Click;
            mainPanel.Click += PropertyListItem_Click;
            pictureBox.Click += PropertyListItem_Click;
            titleLabel.Click += PropertyListItem_Click;
            priceLabel.Click += PropertyListItem_Click;
            detailsLabel.Click += PropertyListItem_Click;
            locationLabel.Click += PropertyListItem_Click;
            dateLabel.Click += PropertyListItem_Click;
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
                details += $"{Listing.Floor}/{Listing.TotalFloors} mərtəbə, ";

            if (Listing.LandArea > 0)
                details += $"{Listing.FormattedLandArea}, ";

            // Remove trailing comma and space
            if (details.EndsWith(", "))
                details = details.Substring(0, details.Length - 2);

            detailsLabel.Text = details;
            locationLabel.Text = Listing.FormattedLocation;
            dateLabel.Text = $"Tarix: {Listing.PublishedDate.ToString("dd.MM.yyyy")}";

            // Load image if available
            if (Listing.ImageUrls.Count > 0)
            {
                ImageLoader.LoadImageIntoPictureBox(Listing.ImageUrls[0], pictureBox);
            }
        }

        private void PropertyListItem_Click(object sender, EventArgs e)
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
