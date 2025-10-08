using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace CsharpGalexy.LibraryExtention.Extentions.Province
{
    public class ProvinceModel
    {
        public string ProvinceId { get; set; } = string.Empty;
        public string ProvinceName { get; set; } = string.Empty;
    }

    public class CityModel
    {
        public string CityId { get; set; } = string.Empty;
        public string ProvinceId { get; set; } = string.Empty;
        public string ProvinceName { get; set; } = string.Empty;
        public string CityName { get; set; } = string.Empty;
    }

    public static class ProvinceHelper
    {
        private static Task<List<ProvinceModel>>? _provincesTask;

        private static string JsonFileUrl =>
            "https://raw.githubusercontent.com/CsharpGalexy/ExtentionsTools/main/CsharpGalexy.Data/Iran/Provinces/provinces.json";

        /// <summary>
        /// بارگذاری اولیه و کش کردن داده‌ها
        /// </summary>
        public static Task InitializeAsync()
        {
            if (_provincesTask == null)
            {
                _provincesTask = LoadProvincesFromJsonAsync();
            }

            return _provincesTask;
        }

        public static async Task<List<ProvinceModel>> LoadProvincesFromJsonAsync()
        {
            using var httpClient = new HttpClient();

            try
            {
                var json = await httpClient.GetStringAsync(JsonFileUrl);
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var provinces = JsonSerializer.Deserialize<List<ProvinceModel>>(json, options);
                return provinces ?? new List<ProvinceModel>();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"خطا در بارگذاری JSON از {JsonFileUrl}: {ex.Message}");
            }
        }

        public static async Task<IReadOnlyList<ProvinceModel>> GetAllProvincesAsync()
        {
            if (_provincesTask == null)
                await InitializeAsync();

            return (await _provincesTask!).AsReadOnly();
        }

        public static async Task<ProvinceModel?> GetByProvinceIdAsync(string provinceId)
        {
            var provinces = await GetAllProvincesAsync();
            return provinces.FirstOrDefault(p => p.ProvinceId == provinceId);
        }

        public static async Task<ProvinceModel?> GetByProvinceNameAsync(string provinceName)
        {
            if (string.IsNullOrWhiteSpace(provinceName))
                return null;

            var provinces = await GetAllProvincesAsync();
            return provinces.FirstOrDefault(p =>
                string.Equals(p.ProvinceName, provinceName, StringComparison.OrdinalIgnoreCase));
        }

        public static async Task<bool> ExistsByProvinceIdAsync(string provinceId)
        {
            var provinces = await GetAllProvincesAsync();
            return provinces.Any(p => p.ProvinceId == provinceId);
        }

        public static async Task<bool> ExistsByProvinceNameAsync(string provinceName)
        {
            if (string.IsNullOrWhiteSpace(provinceName))
                return false;

            var provinces = await GetAllProvincesAsync();
            return provinces.Any(p =>
                string.Equals(p.ProvinceName, provinceName, StringComparison.OrdinalIgnoreCase));
        }
    }
}
