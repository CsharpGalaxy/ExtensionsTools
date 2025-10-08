using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace YourNamespace.Helpers
{
    /// <summary>
    /// مدل نماینده‌ی یک کشور با کد تلفن
    /// </summary>
    public class CountryDialCode
    {
        public string DialCode { get; set; } = string.Empty;
        public string PersianCountryName { get; set; } = string.Empty;
        public string EnglishCountryName { get; set; } = string.Empty;
    }

    /// <summary>
    /// کلاس کمکی برای مدیریت کدهای تلفن کشورها با بارگذاری از فایل JSON آنلاین
    /// </summary>
    public static class CountryDialCodeHelper
    {
        private static Task<List<CountryDialCode>>? _countriesTask;

        private static string JsonFileUrl =>
            "https://raw.githubusercontent.com/CsharpGalexy/ExtentionsTools/main/CsharpGalexy.Data/Iran/Provinces/country-dial-codes.json";

        /// <summary>
        /// بارگذاری اولیه و کش کردن داده‌ها
        /// </summary>
        public static Task InitializeAsync()
        {
            if (_countriesTask == null)
            {
                _countriesTask = LoadFromJsonAsync();
            }

            return _countriesTask;
        }

        public static async Task<List<CountryDialCode>> LoadFromJsonAsync()
        {
            using var httpClient = new HttpClient();

            try
            {
                var json = await httpClient.GetStringAsync(JsonFileUrl);
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var countries = JsonSerializer.Deserialize<List<CountryDialCode>>(json, options);
                return countries ?? new List<CountryDialCode>();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"خطا در بارگذاری JSON از {JsonFileUrl}: {ex.Message}");
            }
        }

        public static async Task<IReadOnlyList<CountryDialCode>> GetAllCountriesAsync()
        {
            if (_countriesTask == null)
                await InitializeAsync();

            return (await _countriesTask!).AsReadOnly();
        }

        public static async Task<string?> GetPersianCountryByDialCodeAsync(string dialCode)
        {
            var countries = await GetAllCountriesAsync();
            return countries.FirstOrDefault(c => c.DialCode == dialCode)?.PersianCountryName;
        }

        public static async Task<string?> GetEnglishCountryByDialCodeAsync(string dialCode)
        {
            var countries = await GetAllCountriesAsync();
            return countries.FirstOrDefault(c => c.DialCode == dialCode)?.EnglishCountryName;
        }

        public static async Task<string?> GetDialCodeByPersianCountryAsync(string persianCountryName)
        {
            if (string.IsNullOrWhiteSpace(persianCountryName))
                return null;

            var countries = await GetAllCountriesAsync();
            return countries.FirstOrDefault(c =>
                string.Equals(c.PersianCountryName, persianCountryName, StringComparison.OrdinalIgnoreCase))?.DialCode;
        }

        public static async Task<string?> GetDialCodeByEnglishCountryAsync(string englishCountryName)
        {
            if (string.IsNullOrWhiteSpace(englishCountryName))
                return null;

            var countries = await GetAllCountriesAsync();
            return countries.FirstOrDefault(c =>
                string.Equals(c.EnglishCountryName, englishCountryName, StringComparison.OrdinalIgnoreCase))?.DialCode;
        }

        public static async Task<IReadOnlyList<CountryDialCode>> GetAllCountriesSortedByDialCodeAsync()
        {
            var countries = await GetAllCountriesAsync();

            return countries
                .Where(c => !string.IsNullOrEmpty(c.DialCode) && c.DialCode.StartsWith("+"))
                .OrderBy(c =>
                {
                    var numericPart = c.DialCode.Substring(1);
                    return long.TryParse(numericPart, out var num) ? num : long.MaxValue;
                })
                .ToList()
                .AsReadOnly();
        }
    }
}
