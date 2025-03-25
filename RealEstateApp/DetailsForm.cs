using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
using RealEstateApp.Models;
using RealEstateApp.Services;
using RealEstateApp.Utils;

namespace RealEstateApp
{
    public partial class DetailsForm : MetroForm
    {
        private readonly PropertyListing _listing;
        private readonly KubAzScraper _scraper;
        private List<PictureBox> _imagePictureBoxes;
        private int _currentImageIndex = 0;
        private bool _isLoading = false;
        private PropertyListing _detailedListing;

        public DetailsForm(PropertyListing listing, KubAzScraper scraper)
        {
            InitializeComponent();
            _listing = listing;
            _scraper = scraper;
            _imagePictureBoxes = new List<PictureBox>();
        }

        private void DetailsForm_Load(object sender, EventArgs e)
        {
            // Set window title
            this.Text = _listing.Title;

            // Display initial data
            DisplayBasicInfo();

            // Load detailed information
            LoadDetailedInfo();

            // Update favorite button state
            UpdateFavoriteButtonState();
        }

        private void DisplayBasicInfo()
        {
            lblTitle.Text = _listing.Title;
            lblPrice.Text = _listing.FormattedPrice;
            lblLocation.Text = _listing.FormattedLocation;

            string details = "";
            if (_listing.Rooms > 0)
                details += $"Otaq sayı: {_listing.Rooms}\n";

            if (_listing.Area > 0)
                details += $"Sahə: {_listing.FormattedArea}\n";

            if (_listing.Floor > 0)
                details += $"Mərtəbə: {_listing.Floor}";

            if (_listing.TotalFloors > 0)
                details += $"/{_listing.TotalFloors}\n";
            else if (_listing.Floor > 0)
                details += "\n";

            if (_listing.LandArea > 0)
                details += $"Torpaq sahəsi: {_listing.FormattedLandArea}\n";

            switch (_listing.Type)
            {
                case PropertyType.Apartment:
                    details += "Növü: Mənzil\n";
                    break;
                case PropertyType.House:
                    details += "Növü: Ev / Villa\n";
                    break;
                case PropertyType.Garage:
                    details += "Növü: Qaraj\n";
                    break;
                case PropertyType.Office:
                    details += "Növü: Ofis\n";
                    break;
                case PropertyType.Land:
                    details += "Növü: Torpaq sahəsi\n";
                    break;
                case PropertyType.Commercial:
                    details += "Növü: Kommersiya obyekti\n";
                    break;
            }

            switch (_listing.Purpose)
            {
                case PropertyPurpose.Sale:
                    details += "Elan növü: Satılır\n";
                    break;
                case PropertyPurpose.Rent:
                    details += "Elan növü: Kirayə verilir\n";
                    break;
                case PropertyPurpose.DailyRent:
                    details += "Elan növü: Günlük kirayə\n";
                    break;
            }

            switch (_listing.BuildingType)
            {
                case BuildingType.New:
                    details += "Tikili növü: Yeni tikili\n";
                    break;
                case BuildingType.Old:
                    details += "Tikili növü: Köhnə tikili\n";
                    break;
            }

            switch (_listing.OwnerType)
            {
                case OwnerType.Owner:
                    details += "Satıcı: Mülkiyyətçi\n";
                    break;
                case OwnerType.Agency:
                    details += "Satıcı: Vasitəçi\n";
                    break;
            }

            details += $"Tarix: {_listing.PublishedDate.ToString("dd.MM.yyyy")}";

            txtDetails.Text = details;
            txtDescription.Text = _listing.Description ?? "Yüklənir...";

            // Load first image if available
            if (_listing.ImageUrls?.Count > 0)
            {
                ImageLoader.LoadImageIntoPictureBox(_listing.ImageUrls[0], pictureBoxMain);
            }

            // Setup thumbnail images
            SetupThumbnails();
        }

        private void SetupThumbnails()
        {
            pnlThumbnails.Controls.Clear();
            _imagePictureBoxes.Clear();

            int thumbnailSize = 80;
            int spacing = 5;
            int currentX = 5;

            if (_listing.ImageUrls == null)
                return;

            for (int i = 0; i < _listing.ImageUrls.Count; i++)
            {
                PictureBox thumbnail = new PictureBox
                {
                    Size = new Size(thumbnailSize, thumbnailSize),
                    Location = new Point(currentX, 5),
                    SizeMode = PictureBoxSizeMode.Zoom,
                    BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle,
                    Tag = i,
                    Cursor = Cursors.Hand
                };

                thumbnail.Click += Thumbnail_Click;

                if (i == 0)
                {
                    thumbnail.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
                }

                ImageLoader.LoadImageIntoPictureBox(_listing.ImageUrls[i], thumbnail);

                pnlThumbnails.Controls.Add(thumbnail);
                _imagePictureBoxes.Add(thumbnail);

                currentX += thumbnailSize + spacing;
            }
        }

        private void Thumbnail_Click(object sender, EventArgs e)
        {
            if (sender is PictureBox clickedThumbnail && clickedThumbnail.Tag is int index)
            {
                _currentImageIndex = index;
                ShowCurrentImage();
            }
        }

        private void ShowCurrentImage()
        {
            if (_listing.ImageUrls == null || _listing.ImageUrls.Count == 0)
                return;

            if (_currentImageIndex < 0)
                _currentImageIndex = _listing.ImageUrls.Count - 1;

            if (_currentImageIndex >= _listing.ImageUrls.Count)
                _currentImageIndex = 0;

            // Load current image
            ImageLoader.LoadImageIntoPictureBox(_listing.ImageUrls[_currentImageIndex], pictureBoxMain);

            // Update thumbnail selection
            for (int i = 0; i < _imagePictureBoxes.Count; i++)
            {
                _imagePictureBoxes[i].BorderStyle = (i == _currentImageIndex) ?
                    System.Windows.Forms.BorderStyle.Fixed3D : System.Windows.Forms.BorderStyle.FixedSingle;
            }

            // Update image counter
            lblImageCounter.Text = $"{_currentImageIndex + 1} / {_listing.ImageUrls.Count}";
        }

        private async void LoadDetailedInfo()
        {
            if (_isLoading || string.IsNullOrEmpty(_listing.DetailsUrl))
                return;

            _isLoading = true;
            lblStatus.Text = "Ətraflı məlumat yüklənir...";
            lblStatus.Visible = true;

            try
            {
                // Fetch detailed listing information
                _detailedListing = await _scraper.GetListingDetailsAsync(_listing.DetailsUrl);

                if (_detailedListing != null)
                {
                    // Update description and details with more complete information
                    if (!string.IsNullOrEmpty(_detailedListing.Description))
                    {
                        txtDescription.Text = _detailedListing.Description;
                    }

                    // Update owner contact information if available
                    string contactInfo = "";
                    if (!string.IsNullOrEmpty(_detailedListing.OwnerName))
                        contactInfo += $"Ad: {_detailedListing.OwnerName}\n";

                    if (!string.IsNullOrEmpty(_detailedListing.OwnerPhone))
                        contactInfo += $"Telefon: {_detailedListing.OwnerPhone}\n";

                    txtContact.Text = contactInfo;

                    // Add any additional images that weren't in the initial listing
                    if (_detailedListing.ImageUrls != null && _listing.ImageUrls != null &&
                        _detailedListing.ImageUrls.Count > _listing.ImageUrls.Count)
                    {
                        bool hasNewImages = false;
                        foreach (var imgUrl in _detailedListing.ImageUrls)
                        {
                            if (imgUrl != null && !_listing.ImageUrls.Contains(imgUrl))
                            {
                                _listing.ImageUrls.Add(imgUrl);
                                hasNewImages = true;
                            }
                        }

                        if (hasNewImages)
                        {
                            SetupThumbnails();
                            ShowCurrentImage();
                        }
                    }
                }

                lblStatus.Visible = false;
            }
            catch (Exception ex)
            {
                lblStatus.Text = $"Xəta: {ex.Message}";
            }
            finally
            {
                _isLoading = false;
            }
        }

        private void UpdateFavoriteButtonState()
        {
            bool isFavorite = DataStorage.IsFavorite(_listing.Id);

            if (isFavorite)
            {
                btnFavorite.Text = "Seçilmişlərdən sil";
                btnFavorite.BackColor = Color.LightPink;
            }
            else
            {
                btnFavorite.Text = "Seçilmişlərə əlavə et";
                btnFavorite.BackColor = Color.LightGreen;
            }
        }

        private void btnPrevImage_Click(object sender, EventArgs e)
        {
            _currentImageIndex--;
            ShowCurrentImage();
        }

        private void btnNextImage_Click(object sender, EventArgs e)
        {
            _currentImageIndex++;
            ShowCurrentImage();
        }

        private void btnFavorite_Click(object sender, EventArgs e)
        {
            bool isFavorite = DataStorage.IsFavorite(_listing.Id);

            if (isFavorite)
            {
                DataStorage.RemoveFromFavorites(_listing.Id);
            }
            else
            {
                DataStorage.AddToFavorites(_listing);
            }

            UpdateFavoriteButtonState();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DetailsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Clean up resources
            ImageLoader.CancelPendingImageLoads();
        }
    }
}
