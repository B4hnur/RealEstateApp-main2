using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HtmlAgilityPack;
using RealEstateApp.Models;

namespace RealEstateApp.Services
{
    public class KubAzScraper
    {
        private readonly WebClientService _webClientService;
        private string _baseUrl = "https://kub.az";
        private readonly CultureInfo _azCulture = new CultureInfo("az-AZ");
        
        /// <summary>
        /// Sayt seçimini dəyişdirmək üçün metod
        /// </summary>
        /// <param name="siteId">Sayt ID: 1 = kub.az, 2 = bina.az, 3 = emlak.az</param>
        public void ChangeSite(int siteId)
        {
            switch(siteId)
            {
                case 1:
                    _baseUrl = "https://kub.az";
                    break;
                case 2:
                    _baseUrl = "https://bina.az";
                    break;
                case 3:
                    _baseUrl = "https://emlak.az";
                    break;
                default:
                    _baseUrl = "https://kub.az";
                    break;
            }
            
            Console.WriteLine($"Sayt dəyişdirildi: {_baseUrl}");
        }
        
        /// <summary>
        /// Hazırkı saytın adını qaytarır
        /// </summary>
        public string GetCurrentSiteName()
        {
            if (_baseUrl.Contains("kub.az"))
                return "kub.az";
            else if (_baseUrl.Contains("bina.az"))
                return "bina.az";
            else if (_baseUrl.Contains("emlak.az"))
                return "emlak.az";
            else
                return _baseUrl;
        }

        public KubAzScraper()
        {
            _webClientService = new WebClientService();
        }

        public async Task<List<PropertyListing>> SearchListingsAsync(SearchFilter filter, int page = 1)
        {
            try
            {
                string searchUrl = BuildSearchUrl(filter, page);
                Console.WriteLine($"Axtarış URL: {searchUrl}");
                
                string html = await _webClientService.GetHtmlAsync(searchUrl);
                Console.WriteLine($"HTML alındı, uzunluq: {html?.Length ?? 0} simvol");
                
                if (string.IsNullOrWhiteSpace(html))
                {
                    Console.WriteLine("Xəbərdarlıq: Boş HTML alındı!");
                    return new List<PropertyListing>();
                }
                
                var listings = ParseListings(html);
                Console.WriteLine($"Çəkilən elanların sayı: {listings.Count}");
                
                return listings;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Xəta baş verdi: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                return new List<PropertyListing>();
            }
        }

        public async Task<PropertyListing> GetListingDetailsAsync(string detailsUrl)
        {
            try
            {
                string fullUrl = detailsUrl.StartsWith("http") ? detailsUrl : $"{_baseUrl}{detailsUrl}";
                string html = await _webClientService.GetHtmlAsync(fullUrl);
                
                return ParseListingDetails(html, detailsUrl);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting listing details: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Axtarış URL-ni qurmaq üçün metod.
        /// Həm kub.az həm də digər daşınmaz əmlak saytları üçün uyğunlaşdırılmışdır
        /// </summary>
        private string BuildSearchUrl(SearchFilter filter, int page)
        {
            // Sayt üçün baza URL təyin edək
            string url = $"{_baseUrl}/search?";
            
            try
            {
                // PropertyType burada entityType/category kimi istifadə edilir
                if (filter.PropertyType.HasValue)
                {
                    string entityTypeParam = "entityType";
                    
                    // Alternativ sayt parametrini yoxlayaq
                    if (_baseUrl.Contains("emlak.az") || _baseUrl.Contains("bina.az"))
                        entityTypeParam = "category";
                        
                    url += $"{entityTypeParam}={Convert.ToInt32(filter.PropertyType.Value)}&";
                }
                
                // Tikili növü
                if (filter.BuildingType.HasValue)
                {
                    // BuildingType = -1 (All) olarsa əlavə edilməməlidir
                    if ((int)filter.BuildingType.Value != -1)
                    {
                        string buildingParam = "buildingType";
                        
                        // Alternativ sayt parametrini yoxlayaq
                        if (_baseUrl.Contains("emlak.az") || _baseUrl.Contains("bina.az"))
                            buildingParam = "building";
                            
                        url += $"{buildingParam}={Convert.ToInt32(filter.BuildingType.Value)}&";
                    }
                }
                
                // Əməliyyat növü (Satılır/Kirayə)
                if (filter.Purpose.HasValue)
                {
                    string purposeParam = "purpose";
                    
                    // Alternativ sayt parametrini yoxlayaq
                    if (_baseUrl.Contains("emlak.az"))
                        purposeParam = "transaction";
                    else if (_baseUrl.Contains("bina.az"))
                        purposeParam = "op";
                        
                    url += $"{purposeParam}={Convert.ToInt32(filter.Purpose.Value)}&";
                }
                
                // Elanın sahibi (Mülkiyyətçi/Vasitəçi)
                if (filter.OwnerType.HasValue)
                {
                    // OwnerType = -1 (All) olarsa əlavə edilməməlidir
                    if ((int)filter.OwnerType.Value != -1)
                    {
                        string ownerParam = "ownerType";
                        
                        // Alternativ sayt parametrini yoxlayaq
                        if (_baseUrl.Contains("emlak.az"))
                            ownerParam = "owner";
                        else if (_baseUrl.Contains("bina.az"))
                            ownerParam = "authorType";
                            
                        url += $"{ownerParam}={Convert.ToInt32(filter.OwnerType.Value)}&";
                    }
                }
                
                // Yerləşmə yeri (Bakı, Xətai və s.)
                if (!string.IsNullOrEmpty(filter.Location))
                {
                    string locationParam = "address";
                    
                    // Alternativ sayt parametrini yoxlayaq
                    if (_baseUrl.Contains("emlak.az") || _baseUrl.Contains("bina.az"))
                        locationParam = "location";
                        
                    url += $"{locationParam}={Uri.EscapeDataString(filter.Location)}&";
                }
                
                // Otaq sayı
                if (filter.MinRooms.HasValue)
                {
                    string roomMinParam = "roomMin";
                    
                    // Alternativ sayt parametrini yoxlayaq
                    if (_baseUrl.Contains("emlak.az") || _baseUrl.Contains("bina.az"))
                        roomMinParam = "rooms_min";
                        
                    url += $"{roomMinParam}={filter.MinRooms.Value}&";
                }
                
                if (filter.MaxRooms.HasValue)
                {
                    string roomMaxParam = "roomMax";
                    
                    // Alternativ sayt parametrini yoxlayaq
                    if (_baseUrl.Contains("emlak.az") || _baseUrl.Contains("bina.az"))
                        roomMaxParam = "rooms_max";
                        
                    url += $"{roomMaxParam}={filter.MaxRooms.Value}&";
                }
                
                // Qiymət
                if (filter.MinPrice.HasValue)
                {
                    string priceMinParam = "priceMin";
                    
                    // Alternativ sayt parametrini yoxlayaq
                    if (_baseUrl.Contains("emlak.az") || _baseUrl.Contains("bina.az"))
                        priceMinParam = "price_min";
                        
                    url += $"{priceMinParam}={filter.MinPrice.Value}&";
                }
                
                if (filter.MaxPrice.HasValue)
                {
                    string priceMaxParam = "priceMax";
                    
                    // Alternativ sayt parametrini yoxlayaq
                    if (_baseUrl.Contains("emlak.az") || _baseUrl.Contains("bina.az"))
                        priceMaxParam = "price_max";
                        
                    url += $"{priceMaxParam}={filter.MaxPrice.Value}&";
                }
                
                // Sahə (m2)
                if (filter.MinArea.HasValue)
                {
                    string areaMinParam = "areaMin";
                    
                    // Alternativ sayt parametrini yoxlayaq
                    if (_baseUrl.Contains("emlak.az") || _baseUrl.Contains("bina.az"))
                        areaMinParam = "area_min";
                        
                    url += $"{areaMinParam}={filter.MinArea.Value}&";
                }
                
                if (filter.MaxArea.HasValue)
                {
                    string areaMaxParam = "areaMax";
                    
                    // Alternativ sayt parametrini yoxlayaq
                    if (_baseUrl.Contains("emlak.az") || _baseUrl.Contains("bina.az"))
                        areaMaxParam = "area_max";
                        
                    url += $"{areaMaxParam}={filter.MaxArea.Value}&";
                }
                
                // Torpaq sahəsi (sot) - kvartal/sot ölçüsü
                if (filter.MinLandArea.HasValue)
                {
                    string landMinParam = "landAreaMin";
                    
                    // Alternativ sayt parametrini yoxlayaq
                    if (_baseUrl.Contains("emlak.az") || _baseUrl.Contains("bina.az"))
                        landMinParam = "land_min";
                        
                    url += $"{landMinParam}={filter.MinLandArea.Value}&";
                }
                
                if (filter.MaxLandArea.HasValue)
                {
                    string landMaxParam = "landAreaMax";
                    
                    // Alternativ sayt parametrini yoxlayaq
                    if (_baseUrl.Contains("emlak.az") || _baseUrl.Contains("bina.az"))
                        landMaxParam = "land_max";
                        
                    url += $"{landMaxParam}={filter.MaxLandArea.Value}&";
                }
                
                // Mərtəbə
                if (filter.MinFloor.HasValue)
                {
                    string floorMinParam = "floorMin";
                    
                    // Alternativ sayt parametrini yoxlayaq
                    if (_baseUrl.Contains("emlak.az") || _baseUrl.Contains("bina.az"))
                        floorMinParam = "floor_min";
                        
                    url += $"{floorMinParam}={filter.MinFloor.Value}&";
                }
                
                if (filter.MaxFloor.HasValue)
                {
                    string floorMaxParam = "floorMax";
                    
                    // Alternativ sayt parametrini yoxlayaq
                    if (_baseUrl.Contains("emlak.az") || _baseUrl.Contains("bina.az"))
                        floorMaxParam = "floor_max";
                        
                    url += $"{floorMaxParam}={filter.MaxFloor.Value}&";
                }
                
                // Açar söz
                if (!string.IsNullOrEmpty(filter.Keyword))
                {
                    string keywordParam = "keyword";
                    
                    // Alternativ sayt parametrini yoxlayaq
                    if (_baseUrl.Contains("emlak.az") || _baseUrl.Contains("bina.az"))
                        keywordParam = "q";
                        
                    url += $"{keywordParam}={Uri.EscapeDataString(filter.Keyword)}&";
                }
                
                // Sıralama
                url += "sort=date_desc&";
                
                // Səhifələmə
                if (page > 1)
                {
                    string pageParam = "page";
                    
                    // Alternativ sayt parametrini yoxlayaq
                    if (_baseUrl.Contains("emlak.az") || _baseUrl.Contains("bina.az"))
                        pageParam = "p";
                        
                    url += $"{pageParam}={page}";
                }
                
                Console.WriteLine($"Hazırlanmış axtarış URL: {url}");
                return url;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"URL hazırlanması zamanı xəta: {ex.Message}");
                return $"{_baseUrl}/search"; // Default URL qaytaraq
            }
        }

        private List<PropertyListing> ParseListings(string html)
        {
            var listings = new List<PropertyListing>();
            var htmlDoc = new HtmlAgilityPack.HtmlDocument();
            htmlDoc.LoadHtml(html);

            // HTML quruluşunu araşdırmaq üçün ilk 2000 simvolu konsola yazaq
            Console.WriteLine("HTML məzmununun bir hissəsi:");
            Console.WriteLine(html.Length > 2000 ? html.Substring(0, 2000) + "..." : html);

            // Yeni seçicilər - Kub.az son versiyasında bu formatlar istifadə edilir
            string[] selectors = {
                "//div[contains(@class, 'ads-list')]/div[contains(@class, 'item')]",
                "//div[contains(@class, 'items-list')]/div[contains(@class, 'item')]",
                "//div[contains(@class, 'items-wrap')]/div[contains(@class, 'item')]",
                "//div[contains(@class, 'item') and contains(@class, 'cursor-p')]",
                // Əgər yuxarıdakılar işləmirsə, daha geniş axtarış aparaq
                "//div[contains(@class, 'item')]"
            };

            HtmlNodeCollection listingNodes = new HtmlNodeCollection(null); // Başlangıçta boş bir koleksiyon oluşturun
            string usedSelector = "";


            foreach (var selector in selectors)
            {
                var nodes = htmlDoc.DocumentNode.SelectNodes(selector);
                if (nodes != null && nodes.Count > 0)
                {
                    listingNodes = nodes;
                    usedSelector = selector;
                    Console.WriteLine($"Seçici işlədi: {selector}, tapılmış elementlər: {nodes.Count}");
                    break;
                }
                else
                {
                    Console.WriteLine($"Seçici işləmədi: {selector}");
                }
            }

            if (listingNodes == null)
            {
                Console.WriteLine("Heç bir elan tapılmadı. HTML quruluşu dəyişmiş ola bilər.");
                
                // Bütün HTML sənədini gəzirək və əmlak elanlarına oxşar hissələri tapmağa çalışaq
                // 1. Link elementləri
                Console.WriteLine("Linkləri axtarıram...");
                var propertyLinks = htmlDoc.DocumentNode.SelectNodes("//a[contains(@href, '/elan/') or contains(@href, '/advert/')]");
                if (propertyLinks != null)
                {
                    Console.WriteLine($"HTML-də {propertyLinks.Count} əmlak elanı linki tapıldı");
                    
                    foreach (var link in propertyLinks.Take(10))
                    {
                        Console.WriteLine($"Link: {link.GetAttributeValue("href", "")} - {link.InnerText.Trim()}");
                        
                        // Bu linklərdən elanlar yaradaq
                        var listing = new PropertyListing();
                        listing.DetailsUrl = link.GetAttributeValue("href", "");
                        
                        if (!listing.DetailsUrl.StartsWith("http"))
                        {
                            listing.DetailsUrl = $"{_baseUrl}{listing.DetailsUrl}";
                        }
                        
                        listing.Id = ExtractIdFromUrl(listing.DetailsUrl);
                        listing.Title = link.InnerText.Trim();
                        
                        // Qiymət və məkan məlumatlarını əldə etmək üçün üst elementləri yoxlayaq
                        var parentNode = link.ParentNode;
                        for (int i = 0; i < 3 && parentNode != null; i++)
                        {
                            // Qiymət axtarışı
                            var priceText = parentNode.InnerText;
                            if (priceText.Contains("AZN") || priceText.Contains("$") || priceText.Contains("€"))
                            {
                                ExtractPriceInfo(priceText, out decimal price, out string currency);
                                listing.Price = price;
                                listing.Currency = currency;
                            }
                            
                            // Şəkil axtarışı
                            var imgNode = parentNode.SelectSingleNode(".//img");
                            if (imgNode != null)
                            {
                                string imgUrl = imgNode.GetAttributeValue("src", "");
                                if (!string.IsNullOrEmpty(imgUrl))
                                {
                                    if (!imgUrl.StartsWith("http"))
                                    {
                                        imgUrl = $"{_baseUrl}{imgUrl}";
                                    }
                                    if (listing.ImageUrls == null)
                                        listing.ImageUrls = new List<string>();
                                    listing.ImageUrls.Add(imgUrl);
                                }
                            }
                            
                            parentNode = parentNode.ParentNode;
                        }
                        
                        // Əgər kifayət qədər məlumat varsa, elanı əlavə edək
                        if (!string.IsNullOrEmpty(listing.Id) && !string.IsNullOrEmpty(listing.Title))
                        {
                            listings.Add(listing);
                        }
                    }
                    
                    if (listings.Count > 0)
                    {
                        Console.WriteLine($"Alternativ metod ilə {listings.Count} elan tapıldı.");
                        return listings;
                    }
                }
                else
                {
                    Console.WriteLine("HTML-də heç bir əmlak elanı linki tapılmadı");
                }
                
                return listings;
            }
            
            Console.WriteLine($"{usedSelector} seçicisi ilə {listingNodes.Count} elan tapıldı.");
            
            foreach (var node in listingNodes)
            {
                try
                {
                    var listing = new PropertyListing();
                    
                    // Extract ID from data attribute or URL
                    var linkNode = node.SelectSingleNode(".//a[contains(@class, 'item-name')]") ?? 
                                   node.SelectSingleNode(".//a[contains(@href, '/elan/')]") ??
                                   node.SelectSingleNode(".//a[contains(@href, '/advert/')]") ??
                                   node.SelectSingleNode(".//a");
                                   
                    if (linkNode != null)
                    {
                        listing.DetailsUrl = linkNode.GetAttributeValue("href", "");
                        if (!listing.DetailsUrl.StartsWith("http"))
                        {
                            listing.DetailsUrl = $"{_baseUrl}{listing.DetailsUrl}";
                        }
                        
                        listing.Id = ExtractIdFromUrl(listing.DetailsUrl);
                        listing.Title = linkNode.InnerText.Trim();
                        Console.WriteLine($"Elan tapıldı: {listing.Title} (ID: {listing.Id})");
                    }
                    else
                    {
                        Console.WriteLine("Link elementi tapılmadı, növbəti elementa keçilir");
                        continue;
                    }
                    
                    // Extract Price - bir neçə müxtəlif CSS sinifi sınayaq
                    var priceNode = node.SelectSingleNode(".//div[contains(@class, 'price')]") ??
                                   node.SelectSingleNode(".//span[contains(@class, 'price')]") ??
                                   node.SelectSingleNode(".//div[contains(@class, 'item-price')]") ??
                                   node.SelectSingleNode(".//div[contains(text(), 'AZN')]") ??
                                   node.SelectSingleNode(".//div[contains(text(), '$')]");
                    
                    if (priceNode == null)
                    {
                        // Qiymət olan nodeları tapmaq üçün hər bir node-un mətnini yoxlayırıq
                        var allNodes = node.SelectNodes(".//*");
                        if (allNodes != null)
                        {
                            foreach (var textNode in allNodes)
                            {
                                string text = textNode.InnerText.Trim();
                                if (text.Contains("AZN") || text.Contains("$") || text.Contains("€"))
                                {
                                    priceNode = textNode;
                                    break;
                                }
                            }
                        }
                    }
                                   
                    if (priceNode != null)
                    {
                        var priceText = priceNode.InnerText.Trim();
                        ExtractPriceInfo(priceText, out decimal price, out string currency);
                        listing.Price = price;
                        listing.Currency = currency;
                        Console.WriteLine($"Qiymət: {listing.Price} {listing.Currency}");
                    }
                    
                    // Extract details - bir neçə müxtəlif CSS sinifi sınayaq
                    var detailsNode = node.SelectSingleNode(".//div[contains(@class, 'details')]") ??
                                     node.SelectSingleNode(".//div[contains(@class, 'item-details')]") ??
                                     node.SelectSingleNode(".//div[contains(@class, 'parameters')]");
                    
                    if (detailsNode == null)
                    {
                        // Detallı məlumatları tapmaq üçün hər bir node-un mətnini yoxlayırıq
                        var allNodes = node.SelectNodes(".//*");
                        if (allNodes != null)
                        {
                            foreach (var textNode in allNodes)
                            {
                                string text = textNode.InnerText.Trim();
                                if (text.Contains("otaq") || text.Contains("m²") || text.Contains("mərtəbə"))
                                {
                                    detailsNode = textNode;
                                    break;
                                }
                            }
                        }
                    }
                                     
                    if (detailsNode != null)
                    {
                        var detailsText = detailsNode.InnerText.Trim();
                        ExtractPropertyDetails(detailsText, listing);
                        Console.WriteLine($"Detallar: {detailsText}");
                    }
                    
                    // Extract location - bir neçə müxtəlif CSS sinifi sınayaq
                    var locationNode = node.SelectSingleNode(".//div[contains(@class, 'location')]") ??
                                      node.SelectSingleNode(".//div[contains(@class, 'item-location')]") ??
                                      node.SelectSingleNode(".//div[contains(@class, 'address')]");
                    
                    if (locationNode == null)
                    {
                        // Ünvan məlumatlarını tapmaq üçün hər bir node-un mətnini yoxlayırıq
                        var allNodes = node.SelectNodes(".//*");
                        if (allNodes != null)
                        {
                            foreach (var textNode in allNodes)
                            {
                                string text = textNode.InnerText.Trim();
                                if (text.Contains("Bakı") || text.Contains("rayon") || 
                                    text.Contains("küç") || text.Contains("pr."))
                                {
                                    locationNode = textNode;
                                    break;
                                }
                            }
                        }
                    }
                                      
                    if (locationNode != null)
                    {
                        listing.Location = locationNode.InnerText.Trim();
                        ExtractLocationDetails(listing.Location, listing);
                        Console.WriteLine($"Ünvan: {listing.Location}");
                    }
                    
                    // Extract image URL - multiple selectors to handle different HTML structures
                    var imgNode = node.SelectSingleNode(".//img") ?? 
                                 node.SelectSingleNode(".//div[contains(@class, 'image')]//img") ??
                                 node.SelectSingleNode(".//div[contains(@class, 'photo')]//img");
                                 
                    if (imgNode != null)
                    {
                        string imgUrl = imgNode.GetAttributeValue("src", "");
                        if (string.IsNullOrEmpty(imgUrl))
                        {
                            imgUrl = imgNode.GetAttributeValue("data-src", "");
                        }
                        
                        if (!string.IsNullOrEmpty(imgUrl))
                        {
                            if (!imgUrl.StartsWith("http"))
                            {
                                imgUrl = $"{_baseUrl}{imgUrl}";
                            }
                            if (listing.ImageUrls == null)
                                listing.ImageUrls = new List<string>();
                            listing.ImageUrls.Add(imgUrl);
                            Console.WriteLine($"Şəkil: {imgUrl}");
                        }
                    }
                    
                    // Evaluate if we have enough info to consider this a valid listing
                    if (!string.IsNullOrEmpty(listing.Id) && !string.IsNullOrEmpty(listing.Title))
                    {
                        listings.Add(listing);
                        Console.WriteLine($"Elan siyahıya əlavə edildi: {listing.Title}");
                    }
                    else
                    {
                        Console.WriteLine($"Kifayət qədər məlumat yoxdur, elan əlavə edilmədi");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Elan emalı zamanı xəta: {ex.Message}");
                    // Skip this listing and continue with the next one
                }
            }
            
            Console.WriteLine($"Toplam {listings.Count} elan emal edildi");
            return listings;
        }

        private PropertyListing ParseListingDetails(string html, string detailsUrl)
        {
            var htmlDoc = new HtmlAgilityPack.HtmlDocument();
            htmlDoc.LoadHtml(html);
            
            // HTML quruluşunu araşdırmaq üçün ilk 2000 simvolu konsola yazaq
            Console.WriteLine("Detallı HTML məzmununun bir hissəsi:");
            Console.WriteLine(html.Length > 2000 ? html.Substring(0, 2000) + "..." : html);
            
            var listing = new PropertyListing
            {
                DetailsUrl = detailsUrl,
                Id = ExtractIdFromUrl(detailsUrl),
                // Default dəyərlər təyin edirik ki, həmişə bir dəyər göstərilsin
                Type = PropertyType.Apartment,
                Purpose = PropertyPurpose.Sale,
                BuildingType = BuildingType.New,
                OwnerType = OwnerType.Owner
            };
            
            // Extract title - multiple selectors
            var titleNode = htmlDoc.DocumentNode.SelectSingleNode("//h1[contains(@class, 'title')]") ??
                           htmlDoc.DocumentNode.SelectSingleNode("//h1[contains(@class, 'item-title')]") ??
                           htmlDoc.DocumentNode.SelectSingleNode("//div[contains(@class, 'title')]//h1");
                           
            if (titleNode != null)
            {
                listing.Title = titleNode.InnerText.Trim();
                Console.WriteLine($"Başlıq: {listing.Title}");
            }
            else
            {
                Console.WriteLine("Başlıq elementləri tapılmadı");
            }
            
            // Extract price - multiple selectors
            var priceNode = htmlDoc.DocumentNode.SelectSingleNode("//div[contains(@class, 'price')]") ??
                           htmlDoc.DocumentNode.SelectSingleNode("//div[contains(@class, 'item-price')]") ??
                           htmlDoc.DocumentNode.SelectSingleNode("//span[contains(@class, 'price')]");
                           
            if (priceNode != null)
            {
                var priceText = priceNode.InnerText.Trim();
                ExtractPriceInfo(priceText, out decimal price, out string currency);
                listing.Price = price;
                listing.Currency = currency;
                Console.WriteLine($"Qiymət: {listing.Price} {listing.Currency}");
            }
            else
            {
                Console.WriteLine("Qiymət elementi tapılmadı");
                
                // Qiymət məlumatlarını bütün HTML-də axtaraq
                var textNodes = htmlDoc.DocumentNode.SelectNodes("//*[contains(text(), 'AZN') or contains(text(), '$')]");
                if (textNodes != null)
                {
                    foreach (var node in textNodes)
                    {
                        var text = node.InnerText.Trim();
                        if (text.Contains("AZN") || text.Contains("$") || text.Contains("€"))
                        {
                            ExtractPriceInfo(text, out decimal price, out string currency);
                            if (price > 0)
                            {
                                listing.Price = price;
                                listing.Currency = currency;
                                Console.WriteLine($"Qiymət alternativ metodla tapıldı: {listing.Price} {listing.Currency}");
                                break;
                            }
                        }
                    }
                }
            }
            
            // Extract description - multiple selectors
            var descNode = htmlDoc.DocumentNode.SelectSingleNode("//div[contains(@class, 'description')]") ??
                          htmlDoc.DocumentNode.SelectSingleNode("//div[contains(@class, 'item-description')]") ??
                          htmlDoc.DocumentNode.SelectSingleNode("//div[contains(@class, 'details-description')]");
                          
            if (descNode != null)
            {
                listing.Description = descNode.InnerText.Trim();
                Console.WriteLine($"Təsvir: {(listing.Description.Length > 100 ? listing.Description.Substring(0, 100) + "..." : listing.Description)}");
            }
            else
            {
                Console.WriteLine("Təsvir elementi tapılmadı");
            }
            
            // Extract property details - multiple approaches
            var detailsNodes = htmlDoc.DocumentNode.SelectNodes("//div[contains(@class, 'property-info')]//div[contains(@class, 'row')]") ??
                              htmlDoc.DocumentNode.SelectNodes("//div[contains(@class, 'details-table')]//tr") ??
                              htmlDoc.DocumentNode.SelectNodes("//table[contains(@class, 'item-parameters')]//tr");
                              
            if (detailsNodes != null)
            {
                Console.WriteLine($"Detal elementləri tapıldı: {detailsNodes.Count}");
                
                foreach (var detailNode in detailsNodes)
                {
                    try
                    {
                        // Try different node selection patterns
                        HtmlNode labelNode = null; // Başlangıçta null atamayın
                        HtmlNode valueNode = null; // Başlangıçta null atamayın

                        // Kullanım sırasında null olup olmadığını kontrol edin
                        if (labelNode != null)
                        {
                            // labelNode ile ilgili işlemleri burada yapın
                        }

                        if (valueNode != null)
                        {
                            // valueNode ile ilgili işlemleri burada yapın
                        }


                        // TR/TD pattern
                        if (detailNode.Name.ToLower() == "tr")
                        {
                            labelNode = detailNode.SelectSingleNode(".//td[1]") ?? 
                                       detailNode.SelectSingleNode(".//th[1]");
                            valueNode = detailNode.SelectSingleNode(".//td[2]") ?? 
                                       detailNode.SelectSingleNode(".//td[last()]");
                        }
                        // DIV/DIV pattern
                        else
                        {
                            labelNode = detailNode.SelectSingleNode(".//div[1]") ?? 
                                       detailNode.SelectSingleNode(".//span[1]");
                            valueNode = detailNode.SelectSingleNode(".//div[2]") ?? 
                                       detailNode.SelectSingleNode(".//span[2]") ?? 
                                       detailNode.SelectSingleNode(".//div[last()]");
                        }
                        
                        if (labelNode != null && valueNode != null)
                        {
                            var label = labelNode.InnerText.Trim().ToLower();
                            var value = valueNode.InnerText.Trim();
                            
                            Console.WriteLine($"Detal: {label} = {value}");
                            SetPropertyDetailsByLabel(listing, label, value);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Detal emalı zamanı xəta: {ex.Message}");
                    }
                }
            }
            else
            {
                Console.WriteLine("Detal elementləri tapılmadı");
                
                // Alternative approach: look for specific text patterns
                var allTextNodes = htmlDoc.DocumentNode.SelectNodes("//*[contains(text(), 'otaq') or contains(text(), 'mərtəbə') or contains(text(), 'm²')]");
                if (allTextNodes != null)
                {
                    Console.WriteLine($"Alternativ metodla {allTextNodes.Count} potensial detal nodları tapıldı");
                    
                    foreach (var node in allTextNodes)
                    {
                        var text = node.InnerText.Trim();
                        ExtractPropertyDetails(text, listing);
                    }
                }
            }
            
            // Extract location - multiple selectors
            var locationNode = htmlDoc.DocumentNode.SelectSingleNode("//div[contains(@class, 'location')]") ??
                              htmlDoc.DocumentNode.SelectSingleNode("//div[contains(@class, 'address')]") ??
                              htmlDoc.DocumentNode.SelectSingleNode("//*[contains(text(), 'Bakı') or contains(text(), 'rayon')]");
                              
            if (locationNode != null)
            {
                listing.Location = locationNode.InnerText.Trim();
                ExtractLocationDetails(listing.Location, listing);
                Console.WriteLine($"Ünvan: {listing.Location}");
            }
            else
            {
                Console.WriteLine("Ünvan elementi tapılmadı");
            }
            
            // Extract images - multiple selectors
            var imageNodes = htmlDoc.DocumentNode.SelectNodes("//div[contains(@class, 'image-gallery')]//img") ??
                             htmlDoc.DocumentNode.SelectNodes("//div[contains(@class, 'carousel')]//img") ??
                             htmlDoc.DocumentNode.SelectNodes("//div[contains(@class, 'gallery')]//img") ??
                             htmlDoc.DocumentNode.SelectNodes("//div[contains(@class, 'slider')]//img");
                             
            if (imageNodes == null)
            {
                // Try a more generic approach
                imageNodes = htmlDoc.DocumentNode.SelectNodes("//img[contains(@src, 'photo') or contains(@src, 'image')]");
            }
            
            if (imageNodes != null)
            {
                Console.WriteLine($"{imageNodes.Count} şəkil tapıldı");
                
                foreach (var imgNode in imageNodes)
                {
                    string imgUrl = imgNode.GetAttributeValue("src", "");
                    if (string.IsNullOrEmpty(imgUrl))
                    {
                        imgUrl = imgNode.GetAttributeValue("data-src", "");
                    }
                    
                    if (!string.IsNullOrEmpty(imgUrl))
                    {
                        if (!imgUrl.StartsWith("http"))
                        {
                            imgUrl = $"{_baseUrl}{imgUrl}";
                        }
                        if (listing.ImageUrls == null)
                            listing.ImageUrls = new List<string>();
                        listing.ImageUrls.Add(imgUrl);
                        Console.WriteLine($"Şəkil əlavə edildi: {imgUrl}");
                    }
                }
            }
            else
            {
                Console.WriteLine("Şəkil elementləri tapılmadı");
            }
            
            // Extract owner information - multiple selectors
            var ownerNameNode = htmlDoc.DocumentNode.SelectSingleNode("//div[contains(@class, 'owner-name')]") ??
                               htmlDoc.DocumentNode.SelectSingleNode("//div[contains(@class, 'author')]") ??
                               htmlDoc.DocumentNode.SelectSingleNode("//div[contains(@class, 'seller')]");
                               
            if (ownerNameNode != null)
            {
                listing.OwnerName = ownerNameNode.InnerText.Trim();
                Console.WriteLine($"Sahibi: {listing.OwnerName}");
            }
            
            var ownerPhoneNode = htmlDoc.DocumentNode.SelectSingleNode("//div[contains(@class, 'owner-phone')]") ??
                                htmlDoc.DocumentNode.SelectSingleNode("//div[contains(@class, 'phone')]") ??
                                htmlDoc.DocumentNode.SelectSingleNode("//a[contains(@href, 'tel:')]");
                                
            if (ownerPhoneNode != null)
            {
                listing.OwnerPhone = ownerPhoneNode.InnerText.Trim();
                Console.WriteLine($"Telefon: {listing.OwnerPhone}");
            }
            
            // Extract owner type
            var ownerTypeNode = htmlDoc.DocumentNode.SelectSingleNode("//div[contains(@class, 'owner-type')]") ??
                               htmlDoc.DocumentNode.SelectSingleNode("//*[contains(text(), 'mülkiyyətçi') or contains(text(), 'sahibi') or contains(text(), 'vasitəçi')]");
                               
            if (ownerTypeNode != null)
            {
                var ownerTypeText = ownerTypeNode.InnerText.Trim().ToLower();
                listing.OwnerType = ownerTypeText.Contains("sahibi") || ownerTypeText.Contains("mülkiyyətçi") ? 
                    OwnerType.Owner : OwnerType.Agency;
                Console.WriteLine($"Sahibi tipi: {listing.OwnerType}");
            }
            
            // Extract date
            var dateNode = htmlDoc.DocumentNode.SelectSingleNode("//div[contains(@class, 'date')]") ??
                          htmlDoc.DocumentNode.SelectSingleNode("//div[contains(@class, 'time')]") ??
                          htmlDoc.DocumentNode.SelectSingleNode("//*[contains(text(), 'tarix') or contains(text(), 'dünən') or contains(text(), 'bugün')]");
                          
            if (dateNode != null)
            {
                var dateText = dateNode.InnerText.Trim();
                listing.PublishedDate = ParseDate(dateText);
                Console.WriteLine($"Tarix: {listing.PublishedDate.ToShortDateString()}");
            }
            
            return listing;
        }

        private string ExtractIdFromUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
                return "";
                
            // Extract ID from URL pattern like /advert/12345 or /elan/12345 or similar
            var match = Regex.Match(url, @"/(?:advert|elan)/(\d+)(?:/|$)");
            if (match.Success)
                return match.Groups[1].Value;
                
            // Try more generic pattern if specific pattern fails
            match = Regex.Match(url, @"/(\d+)(?:/|$)");
            return match.Success ? match.Groups[1].Value : "";
        }

        private void ExtractPriceInfo(string priceText, out decimal price, out string currency)
        {
            price = 0;
            currency = "AZN";
            
            // Remove non-numeric characters, keep decimal point
            var cleanPrice = Regex.Replace(priceText, @"[^\d\.,]", "").Trim();
            
            // Check for currency
            if (priceText.IndexOf("$") >= 0)
                currency = "USD";
            else if (priceText.IndexOf("€") >= 0)
                currency = "EUR";
            
            // Try to parse the price
            if (decimal.TryParse(cleanPrice, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal parsedPrice))
            {
                price = parsedPrice;
            }
        }

        private void ExtractPropertyDetails(string detailsText, PropertyListing listing)
        {
            // Example: "3 otaq, 90 m², 5/12 mərtəbə"
            
            // Extract rooms
            var roomMatch = Regex.Match(detailsText, @"(\d+)\s*otaq");
            if (roomMatch.Success && int.TryParse(roomMatch.Groups[1].Value, out int rooms))
            {
                listing.Rooms = rooms;
            }
            
            // Extract area
            var areaMatch = Regex.Match(detailsText, @"(\d+(?:[.,]\d+)?)\s*m²");
            if (areaMatch.Success && double.TryParse(areaMatch.Groups[1].Value, NumberStyles.Any, CultureInfo.InvariantCulture, out double area))
            {
                listing.Area = area;
            }
            
            // Extract floor information (current/total)
            var floorMatch = Regex.Match(detailsText, @"(\d+)\s*/\s*(\d+)\s*mərtəbə");
            if (floorMatch.Success)
            {
                if (int.TryParse(floorMatch.Groups[1].Value, out int floor))
                {
                    listing.Floor = floor;
                }
                
                if (int.TryParse(floorMatch.Groups[2].Value, out int totalFloors))
                {
                    listing.TotalFloors = totalFloors;
                }
            }
            
            // Extract land area (sot)
            var landMatch = Regex.Match(detailsText, @"(\d+(?:[.,]\d+)?)\s*sot");
            if (landMatch.Success && double.TryParse(landMatch.Groups[1].Value, NumberStyles.Any, CultureInfo.InvariantCulture, out double landArea))
            {
                listing.LandArea = landArea;
            }
            
            // Try to determine property type from details
            if (detailsText.IndexOf("yeni tikili") >= 0)
            {
                listing.BuildingType = BuildingType.New;
            }
            else if (detailsText.IndexOf("köhnə tikili") >= 0)
            {
                listing.BuildingType = BuildingType.Old;
            }
        }

        private void ExtractLocationDetails(string locationText, PropertyListing listing)
        {
            // Example: "Bakı, Nərimanov r."
            var parts = locationText.Split(',').Select(p => p.Trim()).ToArray();
            
            if (parts.Length >= 2)
            {
                listing.City = parts[0];
                listing.District = parts[1];
                
                if (parts.Length >= 3)
                {
                    listing.Address = parts[2];
                }
            }
            else if (parts.Length == 1)
            {
                // Only one part available, assume it's the district
                listing.District = parts[0];
            }
        }

        private DateTime ParseDate(string dateText)
        {
            // Default to current date if parsing fails
            DateTime result = DateTime.Now;
            
            try
            {
                // Try to parse the date
                if (dateText.IndexOf("bugün") >= 0)
                {
                    result = DateTime.Today;
                }
                else if (dateText.IndexOf("dünən") >= 0)
                {
                    result = DateTime.Today.AddDays(-1);
                }
                else if (dateText.IndexOf("ay əvvəl") >= 0)
                {
                    var match = Regex.Match(dateText, @"(\d+)\s*ay");
                    if (match.Success && int.TryParse(match.Groups[1].Value, out int months))
                    {
                        result = DateTime.Today.AddMonths(-months);
                    }
                }
                else if (dateText.IndexOf("il əvvəl") >= 0)
                {
                    var match = Regex.Match(dateText, @"(\d+)\s*il");
                    if (match.Success && int.TryParse(match.Groups[1].Value, out int years))
                    {
                        result = DateTime.Today.AddYears(-years);
                    }
                }
                else
                {
                    // Try to parse as specific date
                    if (DateTime.TryParse(dateText, _azCulture, DateTimeStyles.None, out DateTime parsedDate))
                    {
                        result = parsedDate;
                    }
                }
            }
            catch
            {
                // If anything goes wrong, return current date
            }
            
            return result;
        }

        private void SetPropertyDetailsByLabel(PropertyListing listing, string label, string value)
        {
            switch (label)
            {
                case "elanın növü":
                case "satış növü":
                case "əməliyyat növü":
                case "növü":
                    Console.WriteLine($"Əməliyyat növü tapıldı: {value}");
                    if (value.IndexOf("satılır") >= 0 || value.IndexOf("satış") >= 0)
                        listing.Purpose = PropertyPurpose.Sale;
                    else if (value.IndexOf("günlük kirayə") >= 0)
                        listing.Purpose = PropertyPurpose.DailyRent;
                    else if (value.IndexOf("kirayə") >= 0)
                        listing.Purpose = PropertyPurpose.Rent;
                    break;
                    
                case "əmlak növü":
                case "əmlakın növü":
                case "obyektin tipi":
                case "kateqoriya":
                    Console.WriteLine($"Əmlak növü tapıldı: {value}");
                    if (value.IndexOf("mənzil") >= 0 || value.IndexOf("bina") >= 0)
                        listing.Type = PropertyType.Apartment;
                    else if (value.IndexOf("ev") >= 0 || value.IndexOf("villa") >= 0)
                        listing.Type = PropertyType.House;
                    else if (value.IndexOf("qaraj") >= 0)
                        listing.Type = PropertyType.Garage;
                    else if (value.IndexOf("ofis") >= 0)
                        listing.Type = PropertyType.Office;
                    else if (value.IndexOf("torpaq") >= 0)
                        listing.Type = PropertyType.Land;
                    else if (value.IndexOf("obyekt") >= 0 || value.IndexOf("ticarət") >= 0 || value.IndexOf("kommersi") >= 0)
                        listing.Type = PropertyType.Commercial;
                    break;
                    
                case "tikili növü":
                case "tikili":
                    Console.WriteLine($"Tikili növü tapıldı: {value}");
                    if (value.IndexOf("yeni") >= 0)
                        listing.BuildingType = BuildingType.New;
                    else if (value.IndexOf("köhnə") >= 0 || value.IndexOf("kohne") >= 0 || value.IndexOf("sovet") >= 0)
                        listing.BuildingType = BuildingType.Old;
                    break;
                
                case "sahibi":
                case "satıcı":
                case "sahibkar":
                case "elanın sahibi":
                    Console.WriteLine($"Sahibi tapıldı: {value}");
                    if (value.IndexOf("mülkiyyətçi") >= 0 || value.IndexOf("sahibi") >= 0 || value.IndexOf("sahibkar") >= 0)
                        listing.OwnerType = OwnerType.Owner;
                    else if (value.IndexOf("vasitəçi") >= 0 || value.IndexOf("agentlik") >= 0)
                        listing.OwnerType = OwnerType.Agency;
                    break;
                    
                case "sahə":
                case "ümumi sahə":
                    var areaMatch = Regex.Match(value, @"(\d+(?:[.,]\d+)?)\s*m²");
                    if (areaMatch.Success && double.TryParse(areaMatch.Groups[1].Value, NumberStyles.Any, CultureInfo.InvariantCulture, out double area))
                    {
                        listing.Area = area;
                    }
                    break;
                    
                case "torpaq sahəsi":
                    var landMatch = Regex.Match(value, @"(\d+(?:[.,]\d+)?)\s*sot");
                    if (landMatch.Success && double.TryParse(landMatch.Groups[1].Value, NumberStyles.Any, CultureInfo.InvariantCulture, out double land))
                    {
                        listing.LandArea = land;
                    }
                    break;
                    
                case "otaq sayı":
                    if (int.TryParse(value, out int rooms))
                    {
                        listing.Rooms = rooms;
                    }
                    break;
                    
                case "mərtəbə":
                    var floorMatch = Regex.Match(value, @"(\d+)\s*/\s*(\d+)");
                    if (floorMatch.Success)
                    {
                        if (int.TryParse(floorMatch.Groups[1].Value, out int floor))
                        {
                            listing.Floor = floor;
                        }
                        
                        if (int.TryParse(floorMatch.Groups[2].Value, out int totalFloors))
                        {
                            listing.TotalFloors = totalFloors;
                        }
                    }
                    else if (int.TryParse(value, out int singleFloor))
                    {
                        listing.Floor = singleFloor;
                    }
                    break;
                    
                case "binanın növü":
                    if (value.IndexOf("yeni") >= 0)
                        listing.BuildingType = BuildingType.New;
                    else if (value.IndexOf("köhnə") >= 0)
                        listing.BuildingType = BuildingType.Old;
                    break;
            }
        }
    }
}
