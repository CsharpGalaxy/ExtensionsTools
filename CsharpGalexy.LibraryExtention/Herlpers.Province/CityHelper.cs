

namespace CsharpGalexy.LibraryExtention.Extentions.Province;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;


    public static class CityHelper
    {
        private static readonly Lazy<List<CityModel>> _lazyCities =
            new Lazy<List<CityModel>>(LoadCitiesFromJson);

        private static string JsonFilePath =>
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Iran/Provinces/provinces_cities.json");

        private static List<CityModel> LoadCitiesFromJson()
        {
            if (!File.Exists(JsonFilePath))
                throw new FileNotFoundException($"فایل cities.json یافت نشد: {JsonFilePath}");

            var json = File.ReadAllText(JsonFilePath);
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var cities = JsonSerializer.Deserialize<List<CityModel>>(json, options);
            if (cities == null)
                throw new InvalidOperationException("خطا در دی‌سریالایز کردن فایل cities.json");

            return cities;
        }

        /// <summary>
        /// بازگرداندن تمام شهرها
        /// </summary>
        public static IReadOnlyList<CityModel> GetAllCities() =>
            _lazyCities.Value.AsReadOnly();

        /// <summary>
        /// جستجوی شهر بر اساس شناسه شهر (cityId)
        /// </summary>
        public static CityModel? GetByCityId(string cityId) =>
            GetAllCities().FirstOrDefault(c => c.CityId == cityId);

        /// <summary>
        /// جستجوی شهر بر اساس نام شهر (بدون حساسیت به بزرگ/کوچکی)
        /// </summary>
        public static CityModel? GetByCityName(string cityName)
        {
            if (string.IsNullOrWhiteSpace(cityName))
                return null;

            return GetAllCities().FirstOrDefault(c =>
                string.Equals(c.CityName, cityName, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// دریافت تمام شهرهای یک استان بر اساس شناسه استان (provinceId)
        /// </summary>
        public static IReadOnlyList<CityModel> GetCitiesByProvinceId(string provinceId) =>
            GetAllCities()
                .Where(c => c.ProvinceId == provinceId)
                .ToList()
                .AsReadOnly();

        /// <summary>
        /// دریافت تمام شهرهای یک استان بر اساس نام استان (بدون حساسیت به بزرگ/کوچکی)
        /// </summary>
        public static IReadOnlyList<CityModel> GetCitiesByProvinceName(string provinceName)
        {
            if (string.IsNullOrWhiteSpace(provinceName))
                return Array.Empty<CityModel>();

            return GetAllCities()
                .Where(c => string.Equals(c.ProvinceName, provinceName, StringComparison.OrdinalIgnoreCase))
                .ToList()
                .AsReadOnly();
        }

        /// <summary>
        /// بررسی وجود شهر بر اساس شناسه
        /// </summary>
        public static bool ExistsByCityId(string cityId) =>
            GetAllCities().Any(c => c.CityId == cityId);

        /// <summary>
        /// بررسی وجود شهر بر اساس نام
        /// </summary>
        public static bool ExistsByCityName(string cityName) =>
            !string.IsNullOrWhiteSpace(cityName) &&
            GetAllCities().Any(c =>
                string.Equals(c.CityName, cityName, StringComparison.OrdinalIgnoreCase));
    }
