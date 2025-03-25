using System;
using System.Collections.Generic;
using RealEstateApp.Models;

namespace RealEstateApp.Utils
{
    public static class FilterHelper
    {
        public static Dictionary<int, string> GetPropertyTypes()
        {
            return new Dictionary<int, string>
            {
                { (int)PropertyType.Apartment, "Bina ev mənzil" },
                { (int)PropertyType.House, "Həyət evi / Villa, Bağ evi" },
                { (int)PropertyType.Garage, "Qaraj" },
                { (int)PropertyType.Office, "Ofis" },
                { (int)PropertyType.Land, "Torpaq sahəsi" },
                { (int)PropertyType.Commercial, "Obyekt" }
            };
        }

        public static Dictionary<int, string> GetBuildingTypes()
        {
            return new Dictionary<int, string>
            {
                { (int)BuildingType.All, "Hamısı" },
                { (int)BuildingType.New, "Yeni tikili" },
                { (int)BuildingType.Old, "Köhnə tikili" }
            };
        }

        public static Dictionary<int, string> GetPurposeTypes()
        {
            return new Dictionary<int, string>
            {
                { (int)PropertyPurpose.Sale, "Satılır" },
                { (int)PropertyPurpose.Rent, "Kirayə" },
                { (int)PropertyPurpose.DailyRent, "Günlük kirayə" }
            };
        }

        public static Dictionary<int, string> GetOwnerTypes()
        {
            return new Dictionary<int, string>
            {
                { (int)OwnerType.All, "Hamısı" },
                { (int)OwnerType.Owner, "Ancaq mülkiyyətçi" },
                { (int)OwnerType.Agency, "Ancaq vasitəçi" }
            };
        }

        // Yeni əlavə olunan ComboBox initialize funksiyaları
        public static void InitializePropertyTypeComboBox(object comboBox)
        {
            // Konsol applikasiyası olduğundan və ComboBox istifadə edilmədiyindən
            // burada placeholder kimi əlavə edildi
            // Əsas məqsəd kod kompillasiyasında səhvlərin olmamasıdır
            var propertyTypes = GetPropertyTypes();
            // Console versiyası üçün heç bir əməliyyat həyata keçirilmir
        }

        public static void InitializeBuildingTypeComboBox(object comboBox)
        {
            var buildingTypes = GetBuildingTypes();
            // Console versiyası üçün heç bir əməliyyat həyata keçirilmir
        }

        public static void InitializePurposeComboBox(object comboBox)
        {
            var purposeTypes = GetPurposeTypes();
            // Console versiyası üçün heç bir əməliyyat həyata keçirilmir
        }

        public static void InitializeOwnerTypeComboBox(object comboBox)
        {
            var ownerTypes = GetOwnerTypes();
            // Console versiyası üçün heç bir əməliyyat həyata keçirilmir
        }

        // Kontrol element əsaslı filter yaradır (komponentlərdən)
        public static SearchFilter CreateFilterFromControls(
            object entityTypeCombo,
            object buildingTypeCombo,
            object purposeCombo,
            object ownerTypeCombo,
            object locationTextBox,
            object minRoomsNumeric,
            object maxRoomsNumeric,
            object minPriceNumeric,
            object maxPriceNumeric,
            object minAreaNumeric,
            object maxAreaNumeric,
            object minFloorNumeric,
            object maxFloorNumeric,
            object keywordTextBox)
        {
            // Konsol applikasiyası üçün sadəcə boş bir filter qaytarır
            // Bu metod Windows Forms interfeysi ilə işləmək üçündür
            var filter = new SearchFilter();

            // Bu metod konsol applikasiyası üçün uyğunlaşdırılacaq
            // və ya default dəyərlərlə doldurulacaq
            return filter;
        }

        public static SearchFilter CreateFilterFromUserInput(
            PropertyType propertyType,
            BuildingType buildingType,
            PropertyPurpose purpose,
            OwnerType ownerType,
            string location,
            int minRooms,
            int maxRooms,
            decimal minPrice,
            decimal maxPrice,
            double minArea,
            double maxArea,
            int minFloor,
            int maxFloor,
            string keyword)
        {
            var filter = new SearchFilter();

            // Set property type
            filter.PropertyType = propertyType;

            // Set building type
            filter.BuildingType = buildingType;

            // Set purpose
            filter.Purpose = purpose;

            // Set owner type
            filter.OwnerType = ownerType;

            // Location
            if (!string.IsNullOrWhiteSpace(location))
            {
                filter.Location = location.Trim();
            }

            // Rooms
            if (minRooms > 0)
            {
                filter.MinRooms = minRooms;
            }

            if (maxRooms > 0 && maxRooms >= minRooms)
            {
                filter.MaxRooms = maxRooms;
            }

            // Price
            if (minPrice > 0)
            {
                filter.MinPrice = minPrice;
            }

            if (maxPrice > 0 && maxPrice >= minPrice)
            {
                filter.MaxPrice = maxPrice;
            }

            // Area
            if (minArea > 0)
            {
                filter.MinArea = minArea;
            }

            if (maxArea > 0 && maxArea >= minArea)
            {
                filter.MaxArea = maxArea;
            }

            // Floor
            if (minFloor > 0)
            {
                filter.MinFloor = minFloor;
            }

            if (maxFloor > 0 && maxFloor >= minFloor)
            {
                filter.MaxFloor = maxFloor;
            }

            // Keyword
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                filter.Keyword = keyword.Trim();
            }

            return filter;
        }
    }

    public class ComboBoxItem
    {
        public string Text { get; set; } = string.Empty;
        public int Value { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}
