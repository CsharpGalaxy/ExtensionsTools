using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace CsharpGalexy.LibraryExtention.Extentions.Province;

public class ProvinceModel
{
    public string ProvinceName { get; set; } = string.Empty;
    public string ProvinceId { get; set; } = string.Empty;
}
public class CityModel
{
    public string CityId { get; set; } = string.Empty;
    public string ProvinceName { get; set; } = string.Empty;
    public string CityName { get; set; } = string.Empty;
    public string ProvinceId { get; set; } = string.Empty;
}
public static class ProvinceHelper
{
    private static readonly Lazy<List<ProvinceModel>> _lazyProvinces =
        new Lazy<List<ProvinceModel>>(LoadProvincesFromJson);

    private static string JsonFilePath =>
        Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Iran/Provinces/provinces.json");

    private static List<ProvinceModel> LoadProvincesFromJson()
    {
        if (!System.IO. File.Exists(JsonFilePath))
            throw new FileNotFoundException($"فایل provinces.json یافت نشد: {JsonFilePath}");

        var json = System.IO.File.ReadAllText(JsonFilePath);
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var provinces = JsonSerializer.Deserialize<List<ProvinceModel>>(json, options);

        if (provinces == null)
            throw new InvalidOperationException("خطا در دی‌سریالایز کردن فایل provinces.json");

        return provinces;
    }

    /// <summary>
    /// بازگرداندن تمام استان‌ها (بارگذاری تنها یک‌بار در اولین استفاده)
    /// </summary>
    public static IReadOnlyList<ProvinceModel> GetAllProvinces() =>
        _lazyProvinces.Value.AsReadOnly();

    /// <summary>
    /// جستجوی استان بر اساس شناسه (ProvinceId)
    /// </summary>
    public static ProvinceModel? GetByProvinceId(string provinceId) =>
        GetAllProvinces().FirstOrDefault(p => p.ProvinceId == provinceId);

    /// <summary>
    /// جستجوی استان بر اساس نام (بدون حساسیت به بزرگ/کوچکی حروف)
    /// </summary>
    public static ProvinceModel? GetByProvinceName(string provinceName)
    {
        if (string.IsNullOrWhiteSpace(provinceName))
            return null;

        return GetAllProvinces().FirstOrDefault(p =>
            string.Equals(p.ProvinceName, provinceName, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// بررسی وجود استان بر اساس شناسه
    /// </summary>
    public static bool ExistsByProvinceId(string provinceId) =>
        GetAllProvinces().Any(p => p.ProvinceId == provinceId);

    /// <summary>
    /// بررسی وجود استان بر اساس نام
    /// </summary>
    public static bool ExistsByProvinceName(string provinceName) =>
        !string.IsNullOrWhiteSpace(provinceName) &&
        GetAllProvinces().Any(p =>
            string.Equals(p.ProvinceName, provinceName, StringComparison.OrdinalIgnoreCase));
}