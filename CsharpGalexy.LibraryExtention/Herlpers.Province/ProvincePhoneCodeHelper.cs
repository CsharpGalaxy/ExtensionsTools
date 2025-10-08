using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace CsharpGalexy.LibraryExtention.Extentions.Province
{
    public class ProvincePhoneCode
    {
        public string ProvinceName { get; set; } = string.Empty;
        public string PhoneCode { get; set; } = string.Empty;
    }

    /// <summary>
    /// Helper برای دریافت پیش‌شماره تلفن مراکز استان‌های ایران از فایل JSON آنلاین
    /// </summary>
    public static class ProvincePhoneCodeHelper
    {
        private static Task<Dictionary<string, string>>? _phoneCodesTask;

        private static string JsonFileUrl =>
            "https://raw.githubusercontent.com/CsharpGalexy/ExtentionsTools/main/CsharpGalexy.LibraryExtention.Data/Iran/Provinces/province-phone-codes.json";

        /// <summary>
        /// بارگذاری اولیه و کش کردن داده‌ها
        /// </summary>
        public static Task InitializeAsync()
        {
            if (_phoneCodesTask == null)
            {
                _phoneCodesTask = LoadFromJsonAsync();
            }

            return _phoneCodesTask;
        }

        public static async Task<Dictionary<string, string>> LoadFromJsonAsync()
        {
            using var httpClient = new HttpClient();

            try
            {
                var json = await httpClient.GetStringAsync(JsonFileUrl);
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var items = JsonSerializer.Deserialize<List<ProvincePhoneCode>>(json, options);

                if (items == null)
                    throw new InvalidOperationException("خطا در دی‌سریالایز کردن فایل province-phone-codes.json");

                return items.ToDictionary(
                    item => item.ProvinceName.Trim(),
                    item => item.PhoneCode,
                    StringComparer.OrdinalIgnoreCase
                );
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"خطا در بارگذاری JSON از {JsonFileUrl}: {ex.Message}");
            }
        }

        public static async Task<Dictionary<string, string>> GetPhoneCodeDictionaryAsync()
        {
            if (_phoneCodesTask == null)
                await InitializeAsync();

            return await _phoneCodesTask!;
        }

        /// <summary>
        /// دریافت پیش‌شماره تلفن استان بر اساس نام استان
        /// </summary>
        public static async Task<string?> GetPhoneCodeAsync(string provinceName)
        {
            if (string.IsNullOrWhiteSpace(provinceName))
                throw new ArgumentException("نام استان نمی‌تواند خالی باشد.", nameof(provinceName));

            var dict = await GetPhoneCodeDictionaryAsync();
            return dict.TryGetValue(provinceName.Trim(), out var code) ? code : null;
        }

        /// <summary>
        /// بررسی اینکه آیا استان در لیست پشتیبانی شده است؟
        /// </summary>
        public static async Task<bool> IsSupportedProvinceAsync(string provinceName)
        {
            if (string.IsNullOrWhiteSpace(provinceName)) return false;

            var dict = await GetPhoneCodeDictionaryAsync();
            return dict.ContainsKey(provinceName.Trim());
        }

        /// <summary>
        /// دریافت لیست تمام استان‌های پشتیبانی شده
        /// </summary>
        public static async Task<string[]> GetAllProvinceNamesAsync()
        {
            var dict = await GetPhoneCodeDictionaryAsync();
            return dict.Keys.ToArray();
        }

        /// <summary>
        /// دریافت تمام داده‌ها به صورت لیست
        /// </summary>
        public static async Task<IReadOnlyList<(string ProvinceName, string PhoneCode)>> GetAllPhoneCodesAsync()
        {
            var dict = await GetPhoneCodeDictionaryAsync();
            return dict.Select(kvp => (kvp.Key, kvp.Value)).ToList().AsReadOnly();
        }
    }
}
