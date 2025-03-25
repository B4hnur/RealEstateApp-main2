using System.ComponentModel.DataAnnotations;

namespace RealEstateApp.Models
{
    public class FilterCriteria
    {
        // Price range
        [Display(Name = "Min Price")]
        public decimal? MinPrice { get; set; }

        [Display(Name = "Max Price")]
        public decimal? MaxPrice { get; set; }

        // Bedrooms range
        [Display(Name = "Min Bedrooms")]
        [Range(0, 20)]
        public int? MinBedrooms { get; set; }

        [Display(Name = "Max Bedrooms")]
        [Range(0, 20)]
        public int? MaxBedrooms { get; set; }

        // Bathrooms range
        [Display(Name = "Min Bathrooms")]
        [Range(0, 10)]
        public int? MinBathrooms { get; set; }

        [Display(Name = "Max Bathrooms")]
        [Range(0, 10)]
        public int? MaxBathrooms { get; set; }

        // Area range
        [Display(Name = "Min Area (sq ft)")]
        [Range(0, 10000)]
        public int? MinArea { get; set; }

        [Display(Name = "Max Area (sq ft)")]
        [Range(0, 10000)]
        public int? MaxArea { get; set; }

        // Property type
        [Display(Name = "Property Type")]
        public string? PropertyType { get; set; }

        // Search term (for title, description, address)
        [Display(Name = "Search")]
        public string? SearchTerm { get; set; }

        // Sort options
        public enum SortOption
        {
            PriceLowToHigh,
            PriceHighToLow,
            Newest,
            Oldest,
            MostBedrooms,
            LargestArea
        }

        [Display(Name = "Sort By")]
        public SortOption SortBy { get; set; } = SortOption.Newest;

        // Page size and current page for pagination
        [Range(1, 100)]
        public int PageSize { get; set; } = 10;

        [Range(1, int.MaxValue)]
        public int Page { get; set; } = 1;
    }
}
