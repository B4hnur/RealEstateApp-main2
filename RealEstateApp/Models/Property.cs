using Newtonsoft.Json;
using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RealEstateApp.Models
{
    public class Property
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;

        [StringLength(1000)]
        public string? Description { get; set; }

        [Required]
        [Range(1, double.MaxValue)]
        [Display(Name = "Price")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required]
        [StringLength(200)]
        public string Address { get; set; } = string.Empty;

        [Range(0, 50)]
        public int? Bedrooms { get; set; }

        [Range(0, 20)]
        public int? Bathrooms { get; set; }

        [Range(1, 10000)]
        [Display(Name = "Area (sq ft)")]
        public int? Area { get; set; }

        [Display(Name = "Year Built")]
        [Range(1800, 2100)]
        public int? YearBuilt { get; set; }

        [Display(Name = "Property Type")]
        [Required]
        public string PropertyType { get; set; } = "Apartment";

        [JsonIgnore]
        public virtual ICollection<PropertyImage> Images { get; set; } = new List<PropertyImage>();

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
