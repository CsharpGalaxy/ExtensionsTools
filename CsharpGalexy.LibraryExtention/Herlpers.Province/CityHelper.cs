using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace CsharpGalexy.LibraryExtention.Extentions.Province
{
    public static class CityHelper
    {
        private static Task<List<CityModel>>? _citiesTask;

        private static string JsonFileUrl =>
           "https://raw.githubusercontent.com/CsharpGalexy/ExtentionsTools/main/CsharpGalexy.LibraryExtention.Data/Iran/Provinces/provinces_cities.json";

        /// <summary>
        /// بارگذاری اولیه و کش کردن داده‌ها
        /// </summary>
        public static Task InitializeAsync()
        {
            if (_citiesTask == null)
            {
                _citiesTask = LoadCitiesFromJsonAsync();
            }

            return _citiesTask;
        }

        public static async Task<List<CityModel>> LoadCitiesFromJsonAsync()
        {
            using var httpClient = new HttpClient();

            try
            {
                var json = await httpClient.GetStringAsync(JsonFileUrl);
                var cities = JsonSerializer.Deserialize<List<CityModel>>(json);
                return cities ?? new List<CityModel>();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"خطا در بارگذاری JSON از {JsonFileUrl}: {ex.Message}");
            }
        }

        private static async Task<IReadOnlyList<CityModel>> GetAllCitiesAsync()
        {
            if (_citiesTask == null)
                await InitializeAsync();

            return (await _citiesTask!).AsReadOnly();
        }

        public static async Task<CityModel?> GetByCityIdAsync(string cityId)
        {
            var cities = await GetAllCitiesAsync();
            return cities.FirstOrDefault(c => c.CityId == cityId);
        }

        public static async Task<CityModel?> GetByCityNameAsync(string cityName)
        {
            if (string.IsNullOrWhiteSpace(cityName))
                return null;

            var cities = await GetAllCitiesAsync();
            return cities.FirstOrDefault(c =>
                string.Equals(c.CityName, cityName, StringComparison.OrdinalIgnoreCase));
        }

        public static async Task<IReadOnlyList<CityModel>> GetCitiesByProvinceIdAsync(string provinceId)
        {
            var cities = await GetAllCitiesAsync();
            return cities
                .Where(c => c.ProvinceId == provinceId)
                .ToList()
                .AsReadOnly();
        }

        public static async Task<IReadOnlyList<CityModel>> GetCitiesByProvinceNameAsync(string provinceName)
        {
            if (string.IsNullOrWhiteSpace(provinceName))
                return Array.Empty<CityModel>();

            var cities = await GetAllCitiesAsync();
            return cities
                .Where(c => string.Equals(c.ProvinceName, provinceName, StringComparison.OrdinalIgnoreCase))
                .ToList()
                .AsReadOnly();
        }

        public static async Task<bool> ExistsByCityIdAsync(string cityId)
        {
            var cities = await GetAllCitiesAsync();
            return cities.Any(c => c.CityId == cityId);
        }

        public static async Task<bool> ExistsByCityNameAsync(string cityName)
        {
            if (string.IsNullOrWhiteSpace(cityName))
                return false;

            var cities = await GetAllCitiesAsync();
            return cities.Any(c =>
                string.Equals(c.CityName, cityName, StringComparison.OrdinalIgnoreCase));
        }
    }
}
