using Microsoft.EntityFrameworkCore;
using RealEstateApp.Data;
using RealEstateApp.Helpers;
using RealEstateApp.Models;
using RealEstateApp.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;

namespace RealEstateApp.Services
{
    public class PropertyService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<PropertyService> _logger;
        private readonly FilterHelper _filterHelper;

        public PropertyService(
            ApplicationDbContext context,
            ILogger<PropertyService> logger)
        {
            _context = context;
            _logger = logger;
            _filterHelper = new FilterHelper();
        }

        public async Task<List<Property>> GetAllPropertiesAsync()
        {
            return await _context.Properties
                .Include(p => p.Images)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
        }

        public async Task<List<Property>> GetFilteredPropertiesAsync(FilterCriteria filter)
        {
            try
            {
                // Start with all properties
                IQueryable<Property> query = _context.Properties.Include(p => p.Images);

                // Apply filter criteria using the FilterHelper
                query = _filterHelper.ApplyFilters(query, filter);

                // Apply sorting
                query = ApplySorting(query, filter.SortBy);

                // Apply pagination
                var totalCount = await query.CountAsync();
                var pageCount = (int)Math.Ceiling(totalCount / (double)filter.PageSize);

                var properties = await query
                    .Skip((filter.Page - 1) * filter.PageSize)
                    .Take(filter.PageSize)
                    .ToListAsync();

                return properties;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error filtering properties");
                throw;
            }
        }

        public async Task<Property?> GetPropertyByIdAsync(int id)
        {
            return await _context.Properties
                .Include(p => p.Images)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Property> CreatePropertyAsync(Property property)
        {
            property.CreatedAt = DateTime.Now;
            property.UpdatedAt = DateTime.Now;

            _context.Properties.Add(property);
            await _context.SaveChangesAsync();

            return property;
        }

        public async Task<bool> UpdatePropertyAsync(Property property)
        {
            var existingProperty = await _context.Properties.FindAsync(property.Id);

            if (existingProperty == null)
                return false;

            property.UpdatedAt = DateTime.Now;

            _context.Entry(existingProperty).CurrentValues.SetValues(property);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeletePropertyAsync(int id)
        {
            var property = await _context.Properties.FindAsync(id);

            if (property == null)
                return false;

            _context.Properties.Remove(property);
            await _context.SaveChangesAsync();

            return true;
        }

        private IQueryable<Property> ApplySorting(IQueryable<Property> query, FilterCriteria.SortOption sortBy)
        {
            switch (sortBy)
            {
                case FilterCriteria.SortOption.PriceLowToHigh:
                    return query.OrderBy(p => p.Price);

                case FilterCriteria.SortOption.PriceHighToLow:
                    return query.OrderByDescending(p => p.Price);

                case FilterCriteria.SortOption.Newest:
                    return query.OrderByDescending(p => p.CreatedAt);

                case FilterCriteria.SortOption.Oldest:
                    return query.OrderBy(p => p.CreatedAt);

                case FilterCriteria.SortOption.MostBedrooms:
                    return query.OrderByDescending(p => p.Bedrooms);

                case FilterCriteria.SortOption.LargestArea:
                    return query.OrderByDescending(p => p.Area);

                default:
                    return query.OrderByDescending(p => p.CreatedAt);
            }
        }
    }
}
