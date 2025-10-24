using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace CsharpGalexy.LibraryExtention.Helpers.Mony
{
    public class CurrencyInfo
    {
        public string CountryName { get; set; } = string.Empty;
        public string CurrencyCode { get; set; } = string.Empty; // مثلاً USD
        public string CurrencyName { get; set; } = string.Empty; // مثلاً دلار آمریکا
        public string Symbol { get; set; } = string.Empty;       // مثلاً $
    }

    /// <summary>
    /// Helper برای دریافت اطلاعات واحد پول کشورها از فایل JSON آنلاین
    /// </summary>
    public static class CurrencyHelper
    {
        private static Task<List<CurrencyInfo>>? _currencyTask;

        private static string JsonFileUrl =>
            "https://raw.githubusercontent.com/CsharpGalexy/ExtensionsTools/main/CsharpGalexy.LibraryExtention.Data/Iran/Provinces/currency-codes.json";

        /// <summary>
        /// بارگذاری اولیه و کش کردن داده‌ها
        /// </summary>
        public static Task InitializeAsync()
        {
            if (_currencyTask == null)
            {
                _currencyTask = LoadFromJsonAsync();
            }

            return _currencyTask;
        }

        public static async Task<List<CurrencyInfo>> LoadFromJsonAsync()
        {
            using var httpClient = new HttpClient();

            try
            {
                var json = await httpClient.GetStringAsync(JsonFileUrl);
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var currencies = JsonSerializer.Deserialize<List<CurrencyInfo>>(json, options);
                return currencies ?? new List<CurrencyInfo>();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"خطا در بارگذاری JSON از {JsonFileUrl}: {ex.Message}");
            }
        }

        public static async Task<IReadOnlyList<CurrencyInfo>> GetAllCurrenciesAsync()
        {
            if (_currencyTask == null)
                await InitializeAsync();

            return (await _currencyTask!).AsReadOnly();
        }

        public static async Task<string?> GetCurrencyCodeByCountryAsync(string countryName)
        {
            if (string.IsNullOrWhiteSpace(countryName)) return null;

            var currencies = await GetAllCurrenciesAsync();
            return currencies.FirstOrDefault(c =>
                string.Equals(c.CountryName, countryName, StringComparison.OrdinalIgnoreCase))?.CurrencyCode;
        }

        public static async Task<string?> GetCurrencyNameByCountryAsync(string countryName)
        {
            if (string.IsNullOrWhiteSpace(countryName)) return null;

            var currencies = await GetAllCurrenciesAsync();
            return currencies.FirstOrDefault(c =>
                string.Equals(c.CountryName, countryName, StringComparison.OrdinalIgnoreCase))?.CurrencyName;
        }

        public static async Task<string?> GetCurrencySymbolByCountryAsync(string countryName)
        {
            if (string.IsNullOrWhiteSpace(countryName)) return null;

            var currencies = await GetAllCurrenciesAsync();
            return currencies.FirstOrDefault(c =>
                string.Equals(c.CountryName, countryName, StringComparison.OrdinalIgnoreCase))?.Symbol;
        }

        public static async Task<string?> GetCountryByCurrencyCodeAsync(string currencyCode)
        {
            if (string.IsNullOrWhiteSpace(currencyCode)) return null;

            var currencies = await GetAllCurrenciesAsync();
            return currencies.FirstOrDefault(c =>
                string.Equals(c.CurrencyCode, currencyCode, StringComparison.OrdinalIgnoreCase))?.CountryName;
        }

        public static async Task<IReadOnlyList<string>> GetCountriesByCurrencyCodeAsync(string currencyCode)
        {
            if (string.IsNullOrWhiteSpace(currencyCode))
                return Array.Empty<string>();

            var currencies = await GetAllCurrenciesAsync();
            return currencies
                .Where(c => string.Equals(c.CurrencyCode, currencyCode, StringComparison.OrdinalIgnoreCase))
                .Select(c => c.CountryName)
                .ToList()
                .AsReadOnly();
        }
    }
}
