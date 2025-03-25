using RealEstateApp.Models;
using System.Linq;
using System.Linq.Expressions;

namespace RealEstateApp.Helpers
{
    public class FilterHelper
    {
        public IQueryable<Property> ApplyFilters(IQueryable<Property> query, FilterCriteria filter)
        {
            // Apply price range filter
            if (filter.MinPrice.HasValue)
            {
                query = query.Where(p => p.Price >= filter.MinPrice.Value);
            }

            if (filter.MaxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= filter.MaxPrice.Value);
            }

            // Apply bedrooms filter
            if (filter.MinBedrooms.HasValue)
            {
                query = query.Where(p => p.Bedrooms >= filter.MinBedrooms.Value);
            }

            if (filter.MaxBedrooms.HasValue)
            {
                query = query.Where(p => p.Bedrooms <= filter.MaxBedrooms.Value);
            }

            // Apply bathrooms filter
            if (filter.MinBathrooms.HasValue)
            {
                query = query.Where(p => p.Bathrooms >= filter.MinBathrooms.Value);
            }

            if (filter.MaxBathrooms.HasValue)
            {
                query = query.Where(p => p.Bathrooms <= filter.MaxBathrooms.Value);
            }

            // Apply area filter
            if (filter.MinArea.HasValue)
            {
                query = query.Where(p => p.Area >= filter.MinArea.Value);
            }

            if (filter.MaxArea.HasValue)
            {
                query = query.Where(p => p.Area <= filter.MaxArea.Value);
            }

            // Apply property type filter
            if (!string.IsNullOrWhiteSpace(filter.PropertyType))
            {
                query = query.Where(p => p.PropertyType == filter.PropertyType);
            }

            // Apply search term filter (on title, description, and address)
            if (!string.IsNullOrWhiteSpace(filter.SearchTerm))
            {
                var searchTerm = filter.SearchTerm.ToLower();
                query = query.Where(p =>
                    p.Title.ToLower().Contains(searchTerm) ||
                    (p.Description != null && p.Description.ToLower().Contains(searchTerm)) ||
                    p.Address.ToLower().Contains(searchTerm));
            }

            return query;
        }
    }
}
