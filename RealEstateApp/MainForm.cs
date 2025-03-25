using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
using RealEstateApp.Controls;
using RealEstateApp.Models;
using RealEstateApp.Services;
using RealEstateApp.Utils;

namespace RealEstateApp
{
    public partial class MainForm : MetroForm
    {
        private readonly KubAzScraper _scraper;
        private List<PropertyListing> _currentListings;
        private bool _isGridView = false;
        private int _currentPage = 1;
        private int _totalPages = 1;
        private SearchFilter _currentFilter;
        private bool _isLoading = false;

        public MainForm()
        {
            InitializeComponent();
            _scraper = new KubAzScraper();
            _currentListings = new List<PropertyListing>();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Initialize UI
            InitializeFilterControls();

            // Set initial view
            SetListView();

            // Load data
            LoadDefaultListings();
        }

        private void InitializeFilterControls()
        {
            // Entity Type (Property Type)
            FilterHelper.InitializePropertyTypeComboBox(cmbEntityType);

            // Building Type
            FilterHelper.InitializeBuildingTypeComboBox(cmbBuildingType);

            // Purpose (Sale, Rent)
            FilterHelper.InitializePurposeComboBox(cmbPurpose);

            // Owner Type
            FilterHelper.InitializeOwnerTypeComboBox(cmbOwnerType);

            // Set min/max values for numeric inputs
            nudMinRooms.Minimum = 0;
            nudMinRooms.Maximum = 10;
            nudMaxRooms.Minimum = 0;
            nudMaxRooms.Maximum = 10;

            nudMinPrice.Minimum = 0;
            nudMinPrice.Maximum = 10000000;
            nudMinPrice.Increment = 1000;
            nudMaxPrice.Minimum = 0;
            nudMaxPrice.Maximum = 10000000;
            nudMaxPrice.Increment = 1000;

            nudMinArea.Minimum = 0;
            nudMinArea.Maximum = 10000;
            nudMinArea.Increment = 10;
            nudMaxArea.Minimum = 0;
            nudMaxArea.Maximum = 10000;
            nudMaxArea.Increment = 10;

            nudMinFloor.Minimum = 0;
            nudMinFloor.Maximum = 50;
            nudMaxFloor.Minimum = 0;
            nudMaxFloor.Maximum = 50;

            // Add event handlers for controls
            cmbEntityType.SelectedIndexChanged += FilterControl_Changed;
            cmbBuildingType.SelectedIndexChanged += FilterControl_Changed;
            cmbPurpose.SelectedIndexChanged += FilterControl_Changed;
            cmbOwnerType.SelectedIndexChanged += FilterControl_Changed;
        }

        private async void LoadDefaultListings()
        {
            // Show loading indicator
            SetLoadingState(true);

            try
            {
                // Create default filter
                _currentFilter = new SearchFilter
                {
                    EntityType = PropertyType.Apartment,
                    Purpose = PropertyPurpose.Sale
                };

                // Search with default filter
                await SearchWithFilter(_currentFilter);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading listings: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Hide loading indicator
                SetLoadingState(false);
            }
        }

        private async Task SearchWithFilter(SearchFilter filter, int page = 1)
        {
            if (_isLoading)
                return;

            // Show loading indicator
            SetLoadingState(true);

            try
            {
                _currentPage = page;
                _currentFilter = filter;

                // Save the search to recent searches
                DataStorage.AddRecentSearch(filter);

                // Clear current listings panel
                pnlListings.Controls.Clear();

                // Get listings from the scraper
                _currentListings = await _scraper.SearchListingsAsync(filter, page);

                // Display the listings based on current view mode
                if (_isGridView)
                    DisplayGridView();
                else
                    DisplayListView();

                // Update page status
                lblPageInfo.Text = $"Səhifə {_currentPage}";

                // Enable/disable pagination buttons
                btnPrevPage.Enabled = _currentPage > 1;
                btnNextPage.Enabled = _currentListings.Count >= 20; // Assume there are more pages if we have a full page
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Hide loading indicator
                SetLoadingState(false);
            }
        }

        private void DisplayListView()
        {
            pnlListings.Controls.Clear();

            foreach (var listing in _currentListings)
            {
                var item = new PropertyListItem(listing);
                item.ItemClicked += ListingItem_Clicked;
                item.Width = pnlListings.Width - 20;
                pnlListings.Controls.Add(item);
            }

            if (_currentListings.Count == 0)
            {
                var noResultsLabel = new Label
                {
                    Text = "Axtarışınıza uyğun heç bir elan tapılmadı.",
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI", 12F)
                };
                pnlListings.Controls.Add(noResultsLabel);
            }
        }

        private void DisplayGridView()
        {
            pnlListings.Controls.Clear();

            // Calculate how many items we can fit per row based on panel width
            int itemWidth = 250;
            int itemHeight = 300;
            int horizontalSpacing = 10;
            int itemsPerRow = Math.Max(1, (pnlListings.Width - 20) / (itemWidth + horizontalSpacing));

            int currentX = 5;
            int currentY = 5;
            int itemCount = 0;

            foreach (var listing in _currentListings)
            {
                var item = new PropertyGridItem(listing);
                item.ItemClicked += ListingItem_Clicked;
                item.Size = new Size(itemWidth, itemHeight);
                item.Location = new Point(currentX, currentY);
                pnlListings.Controls.Add(item);

                itemCount++;

                if (itemCount % itemsPerRow == 0)
                {
                    // Move to next row
                    currentX = 5;
                    currentY += itemHeight + 10;
                }
                else
                {
                    // Move to next column
                    currentX += itemWidth + horizontalSpacing;
                }
            }

            if (_currentListings.Count == 0)
            {
                var noResultsLabel = new Label
                {
                    Text = "Axtarışınıza uyğun heç bir elan tapılmadı.",
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI", 12F)
                };
                pnlListings.Controls.Add(noResultsLabel);
            }
        }

        private void ListingItem_Clicked(object sender, PropertyListing listing)
        {
            // Show details form
            DetailsForm detailsForm = new DetailsForm(listing, _scraper);
            detailsForm.ShowDialog();

            // Refresh if needed (e.g., if the user added/removed the listing from favorites)
            if (_isGridView)
                DisplayGridView();
            else
                DisplayListView();
        }

        private void SetListView()
        {
            _isGridView = false;
            btnGridView.Enabled = true;
            btnListView.Enabled = false;

            if (_currentListings.Count > 0)
                DisplayListView();
        }

        private void SetGridView()
        {
            _isGridView = true;
            btnGridView.Enabled = false;
            btnListView.Enabled = true;

            if (_currentListings.Count > 0)
                DisplayGridView();
        }

        private void SetLoadingState(bool isLoading)
        {
            _isLoading = isLoading;

            // Show/hide loading indicator
            progressBar.Visible = isLoading;

            // Enable/disable UI controls
            pnlFilters.Enabled = !isLoading;
            btnSearch.Enabled = !isLoading;
            btnPrevPage.Enabled = !isLoading && _currentPage > 1;
            btnNextPage.Enabled = !isLoading && _currentListings.Count >= 20;
            btnGridView.Enabled = !isLoading && !_isGridView;
            btnListView.Enabled = !isLoading && _isGridView;
            btnFavorites.Enabled = !isLoading;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchFilter filter = FilterHelper.CreateFilterFromControls(
                cmbEntityType,
                cmbBuildingType,
                cmbPurpose,
                cmbOwnerType,
                txtLocation,
                nudMinRooms,
                nudMaxRooms,
                nudMinPrice,
                nudMaxPrice,
                nudMinArea,
                nudMaxArea,
                nudMinFloor,
                nudMaxFloor,
                txtKeyword
            );

            SearchWithFilter(filter);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            // Reset all filter controls
            cmbEntityType.SelectedIndex = 0;
            cmbBuildingType.SelectedIndex = 0;
            cmbPurpose.SelectedIndex = 0;
            cmbOwnerType.SelectedIndex = 0;

            txtLocation.Text = "";

            nudMinRooms.Value = 0;
            nudMaxRooms.Value = 0;

            nudMinPrice.Value = 0;
            nudMaxPrice.Value = 0;

            nudMinArea.Value = 0;
            nudMaxArea.Value = 0;

            nudMinFloor.Value = 0;
            nudMaxFloor.Value = 0;

            txtKeyword.Text = "";
        }

        private void btnPrevPage_Click(object sender, EventArgs e)
        {
            if (_currentPage > 1)
            {
                SearchWithFilter(_currentFilter, _currentPage - 1);
            }
        }

        private void btnNextPage_Click(object sender, EventArgs e)
        {
            SearchWithFilter(_currentFilter, _currentPage + 1);
        }

        private void btnGridView_Click(object sender, EventArgs e)
        {
            SetGridView();
        }

        private void btnListView_Click(object sender, EventArgs e)
        {
            SetListView();
        }

        private void btnFavorites_Click(object sender, EventArgs e)
        {
            // Display favorites
            _currentListings = DataStorage.GetFavorites();

            if (_isGridView)
                DisplayGridView();
            else
                DisplayListView();

            lblResultsInfo.Text = $"Seçilmişlər ({_currentListings.Count})";
        }

        private void FilterControl_Changed(object sender, EventArgs e)
        {
            // Auto-update UI based on entity type selection
            if (sender == cmbEntityType)
            {
                UpdateUIForEntityType();
            }
        }

        private void UpdateUIForEntityType()
        {
            if (cmbEntityType.SelectedItem is ComboBoxItem entityTypeItem)
            {
                PropertyType entityType = (PropertyType)entityTypeItem.Value;

                // Enable/disable controls based on entity type
                switch (entityType)
                {
                    case PropertyType.Apartment:
                        // For apartments, enable room fields, floor fields, building type
                        pnlRooms.Enabled = true;
                        pnlFloor.Enabled = true;
                        pnlArea.Enabled = true;
                        cmbBuildingType.Enabled = true;
                        break;

                    case PropertyType.House:
                        // For houses, enable room fields, area, disable floors and building type
                        pnlRooms.Enabled = true;
                        pnlFloor.Enabled = false;
                        pnlArea.Enabled = true;
                        cmbBuildingType.Enabled = false;
                        break;

                    case PropertyType.Garage:
                        // For garages, disable rooms, enable area, disable floors and building type
                        pnlRooms.Enabled = false;
                        pnlFloor.Enabled = false;
                        pnlArea.Enabled = true;
                        cmbBuildingType.Enabled = false;
                        break;

                    case PropertyType.Office:
                        // For offices, disable rooms, enable area and floors, disable building type
                        pnlRooms.Enabled = false;
                        pnlFloor.Enabled = true;
                        pnlArea.Enabled = true;
                        cmbBuildingType.Enabled = false;
                        break;

                    case PropertyType.Land:
                        // For land, disable rooms, floors, building type, enable area
                        pnlRooms.Enabled = false;
                        pnlFloor.Enabled = false;
                        pnlArea.Enabled = true;
                        cmbBuildingType.Enabled = false;
                        break;

                    case PropertyType.Commercial:
                        // For commercial, disable rooms, enable area and floors, disable building type
                        pnlRooms.Enabled = false;
                        pnlFloor.Enabled = true;
                        pnlArea.Enabled = true;
                        cmbBuildingType.Enabled = false;
                        break;
                }
            }
        }

        private void pnlListings_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pnlPurpose_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
