using System;
using System.Collections.Generic;

namespace RealEstateApp.Models
{
    public class PropertyListing
    {
        public string Id { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Currency { get; set; } = "AZN";
        public PropertyType Type { get; set; }
        public PropertyPurpose Purpose { get; set; }
        public int Rooms { get; set; }
        public double Area { get; set; }
        public double LandArea { get; set; }
        public int Floor { get; set; }
        public int TotalFloors { get; set; }
        public BuildingType BuildingType { get; set; }
        public string Location { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string District { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public OwnerType OwnerType { get; set; }
        public string OwnerName { get; set; } = string.Empty;
        public string OwnerPhone { get; set; } = string.Empty;
        public DateTime PublishedDate { get; set; } = DateTime.Now;
        public List<string> ImageUrls { get; set; }
        public string DetailsUrl { get; set; } = string.Empty;

        public PropertyListing()
        {
            ImageUrls = new List<string>();
        }

        public string FormattedPrice
        {
            get { return $"{Price:N0} {Currency}"; }
        }

        public string FormattedArea
        {
            get { return $"{Area:N1} m²"; }
        }

        public string FormattedLandArea
        {
            get
            {
                if (LandArea > 0)
                    return $"{LandArea:N1} sot";
                return string.Empty;
            }
        }

        public string FormattedLocation
        {
            get
            {
                if (!string.IsNullOrEmpty(District) && !string.IsNullOrEmpty(City))
                    return $"{City}, {District}";
                if (!string.IsNullOrEmpty(District))
                    return District;
                if (!string.IsNullOrEmpty(City))
                    return City;
                return Location;
            }
        }
    }

    public enum PropertyType
    {
        Apartment = 0,
        House = 1,
        Garage = 2,
        Office = 3,
        Land = 4,
        Commercial = 5
    }

    public enum PropertyPurpose
    {
        Sale = 0,
        Rent = 1,
        DailyRent = 2
    }

    public enum BuildingType
    {
        All = -1,
        New = 0,
        Old = 1
    }

    public enum OwnerType
    {
        All = -1,
        Owner = 0,
        Agency = 1
    }
}
