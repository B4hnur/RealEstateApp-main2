using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using RestSharp;
using System.Threading;

namespace RealEstateApp.Services
{
    public class WebClientService
    {
        private readonly RestClient _client;

        public WebClientService()
        {
            var options = new RestClientOptions
            {
                MaxTimeout = 30000, // Timeout-u 30 saniyəyə artırırıq
                UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36",
                FollowRedirects = true
            };
            _client = new RestClient(options);
        }

        public async Task<string> GetHtmlAsync(string url)
        {
            try
            {
                Console.WriteLine($"URL-ə sorğu göndərilir: {url}");

                var request = new RestRequest(url);

                // Browser kimi davranırıq, tipik HTTP başlıqlarını əlavə edirik
                request.AddHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
                request.AddHeader("Accept-Language", "en-US,en;q=0.5");
                request.AddHeader("Referer", "https://kub.az/");
                request.AddHeader("Cache-Control", "no-cache");
                request.AddHeader("Pragma", "no-cache");
                request.AddHeader("DNT", "1");

                // Bəzən serverlərin bot kimi qəbul etməməsi üçün bir neçə saniyə gözləyirik
                await Task.Delay(2000);

                var response = await _client.ExecuteGetAsync(request);

                Console.WriteLine($"Server cavabı: Status: {response.StatusCode}, Uzunluq: {response.Content?.Length ?? 0}");

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    if (string.IsNullOrWhiteSpace(response.Content))
                    {
                        Console.WriteLine("XƏBƏRDARLIQ: Server boş cavab göndərdi!");
                    }
                    return response.Content ?? string.Empty;
                }

                // Alternativ metod: HttpClient-dən istifadə edirik
                if (response.StatusCode != HttpStatusCode.OK || string.IsNullOrWhiteSpace(response.Content))
                {
                    Console.WriteLine("RestClient uğursuz oldu, HttpClient ilə sınayırıq...");

                    using (var httpClient = new HttpClient())
                    {
                        httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36");

                        var httpResponse = await httpClient.GetAsync(url);
                        httpResponse.EnsureSuccessStatusCode();

                        var htmlContent = await httpResponse.Content.ReadAsStringAsync();
                        return htmlContent;
                    }
                }

                throw new Exception($"HTML əldə edilə bilmədi. Status kodu: {response.StatusCode}, Xəta: {response.ErrorMessage}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"HTML əldə edilərkən xəta baş verdi: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                return string.Empty;
            }
        }
        


        private static readonly HttpClient _httpClient = new HttpClient();

        public async Task<byte[]> DownloadImageAsync(string imageUrl)
        {
            try
            {
                var response = await _httpClient.GetAsync(imageUrl);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsByteArrayAsync();
                }

                Console.WriteLine($"Error downloading image: {response.StatusCode}");
                return Array.Empty<byte>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error downloading image: {ex.Message}");
                return Array.Empty<byte>();
            }
        }
    }
}
