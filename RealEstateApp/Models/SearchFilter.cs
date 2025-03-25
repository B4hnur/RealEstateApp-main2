using System;

namespace RealEstateApp.Models
{
    public class SearchFilter
    {
        public Nullable<PropertyType> PropertyType { get; set; }
        public Nullable<BuildingType> BuildingType { get; set; }
        public Nullable<PropertyPurpose> Purpose { get; set; }
        public Nullable<OwnerType> OwnerType { get; set; }
        public string Location { get; set; } = string.Empty;
        public Nullable<int> MinRooms { get; set; }
        public Nullable<int> MaxRooms { get; set; }
        public Nullable<decimal> MinPrice { get; set; }
        public Nullable<decimal> MaxPrice { get; set; }
        public Nullable<double> MinArea { get; set; }
        public Nullable<double> MaxArea { get; set; }
        public Nullable<double> MinLandArea { get; set; }
        public Nullable<double> MaxLandArea { get; set; }
        public Nullable<int> MinFloor { get; set; }
        public Nullable<int> MaxFloor { get; set; }
        public string Keyword { get; set; } = string.Empty;

        // Windows Forms applikasiyasında istifadə olunmuş EntityType property
        // Console versiyasında istifadə edilmir, lakin kompilyasiya üçün lazımdır
        public Nullable<PropertyType> EntityType
        {
            get { return PropertyType; }
            set { PropertyType = value; }
        }

        public SearchFilter()
        {
            // Default constructor
        }

        public bool Matches(PropertyListing listing)
        {
            // Check all filter criteria
            if (PropertyType.HasValue && listing.Type != PropertyType.Value)
                return false;

            if (BuildingType.HasValue && BuildingType.Value != Models.BuildingType.All &&
                listing.BuildingType != BuildingType.Value)
                return false;

            if (Purpose.HasValue && listing.Purpose != Purpose.Value)
                return false;

            if (OwnerType.HasValue && OwnerType.Value != Models.OwnerType.All &&
                listing.OwnerType != OwnerType.Value)
                return false;

            if (!string.IsNullOrEmpty(Location) &&
                !listing.FormattedLocation.ToLower().Contains(Location.ToLower()))
                return false;

            if (MinRooms.HasValue && listing.Rooms < MinRooms.Value)
                return false;

            if (MaxRooms.HasValue && listing.Rooms > MaxRooms.Value)
                return false;

            if (MinPrice.HasValue && listing.Price < MinPrice.Value)
                return false;

            if (MaxPrice.HasValue && listing.Price > MaxPrice.Value)
                return false;

            if (MinArea.HasValue && listing.Area < MinArea.Value)
                return false;

            if (MaxArea.HasValue && listing.Area > MaxArea.Value)
                return false;

            if (MinLandArea.HasValue && listing.LandArea < MinLandArea.Value)
                return false;

            if (MaxLandArea.HasValue && listing.LandArea > MaxLandArea.Value)
                return false;

            if (MinFloor.HasValue && listing.Floor < MinFloor.Value)
                return false;

            if (MaxFloor.HasValue && listing.Floor > MaxFloor.Value)
                return false;

            if (!string.IsNullOrEmpty(Keyword) &&
                !((listing.Title != null && listing.Title.ToLower().Contains(Keyword.ToLower())) ||
                  (listing.Description != null && listing.Description.ToLower().Contains(Keyword.ToLower()))))
                return false;

            return true;
        }
    }
}
