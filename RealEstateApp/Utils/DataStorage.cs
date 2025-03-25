using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using RealEstateApp.Models;

namespace RealEstateApp.Utils
{
    public static class DataStorage
    {
        private static readonly string AppDataFolder = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "RealEstateApp");

        private static readonly string FavoritesFile = Path.Combine(AppDataFolder, "favorites.json");
        private static readonly string RecentSearchesFile = Path.Combine(AppDataFolder, "recent_searches.json");

        private static List<PropertyListing> _favorites = new List<PropertyListing>();
        private static List<SearchFilter> _recentSearches = new List<SearchFilter>();

        static DataStorage()
        {
            // Ensure the application data folder exists
            if (!Directory.Exists(AppDataFolder))
            {
                Directory.CreateDirectory(AppDataFolder);
            }

            LoadFavorites();
            LoadRecentSearches();
        }

        #region Favorites

        public static List<PropertyListing> GetFavorites()
        {
            return _favorites?.ToList() ?? new List<PropertyListing>();
        }

        public static bool IsFavorite(string listingId)
        {
            if (string.IsNullOrEmpty(listingId))
                return false;

            return _favorites?.Any(f => f.Id == listingId) ?? false;
        }

        public static void AddToFavorites(PropertyListing listing)
        {
            if (_favorites == null)
            {
                _favorites = new List<PropertyListing>();
            }

            if (!string.IsNullOrEmpty(listing.Id) && !IsFavorite(listing.Id))
            {
                _favorites.Add(listing);
                SaveFavorites();
            }
        }

        public static void RemoveFromFavorites(string listingId)
        {
            if (string.IsNullOrEmpty(listingId))
                return;

            if (_favorites != null && _favorites.Any(f => f.Id == listingId))
            {
                _favorites.RemoveAll(f => f.Id == listingId);
                SaveFavorites();
            }
        }

        private static void LoadFavorites()
        {
            try
            {
                if (File.Exists(FavoritesFile))
                {
                    string json = File.ReadAllText(FavoritesFile);
                    _favorites = JsonConvert.DeserializeObject<List<PropertyListing>>(json) ?? new List<PropertyListing>();
                }
                else
                {
                    _favorites = new List<PropertyListing>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading favorites: {ex.Message}");
                _favorites = new List<PropertyListing>();
            }
        }

        private static void SaveFavorites()
        {
            try
            {
                string json = JsonConvert.SerializeObject(_favorites, Formatting.Indented);
                File.WriteAllText(FavoritesFile, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving favorites: {ex.Message}");
            }
        }

        #endregion

        #region Recent Searches

        public static List<SearchFilter> GetRecentSearches()
        {
            return _recentSearches?.ToList() ?? new List<SearchFilter>();
        }

        public static void AddRecentSearch(SearchFilter filter)
        {
            if (_recentSearches == null)
            {
                _recentSearches = new List<SearchFilter>();
            }

            // Remove any existing similar searches to avoid duplicates
            _recentSearches.RemoveAll(s =>
                s.PropertyType == filter.PropertyType &&
                s.Purpose == filter.Purpose &&
                s.Location == filter.Location);

            // Add at the beginning (most recent)
            _recentSearches.Insert(0, filter);

            // Keep only the last 10 searches
            if (_recentSearches.Count > 10)
            {
                _recentSearches = _recentSearches.Take(10).ToList();
            }

            SaveRecentSearches();
        }

        private static void LoadRecentSearches()
        {
            try
            {
                if (File.Exists(RecentSearchesFile))
                {
                    string json = File.ReadAllText(RecentSearchesFile);
                    _recentSearches = JsonConvert.DeserializeObject<List<SearchFilter>>(json) ?? new List<SearchFilter>();
                }
                else
                {
                    _recentSearches = new List<SearchFilter>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading recent searches: {ex.Message}");
                _recentSearches = new List<SearchFilter>();
            }
        }

        private static void SaveRecentSearches()
        {
            try
            {
                string json = JsonConvert.SerializeObject(_recentSearches, Formatting.Indented);
                File.WriteAllText(RecentSearchesFile, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving recent searches: {ex.Message}");
            }
        }

        #endregion
    }
}
