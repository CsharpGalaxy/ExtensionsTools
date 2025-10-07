using System;
using System.Collections.Generic;
using System.Text.Json;




public class ProvinceCapital
{
    public string ProvinceId { get; set; } = string.Empty;
    public string ProvinceName { get; set; } = string.Empty;
    public string Capital { get; set; } = string.Empty;
}


public static class ProvinceCapitalHelper
{
    private static readonly Lazy<List<ProvinceCapital>> _lazyProvinces =
        new Lazy<List<ProvinceCapital>>(LoadFromJson);

    private static string JsonFilePath =>
        Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Iran/Provinces/province-capitals.json");

    private static List<ProvinceCapital> LoadFromJson()
    {
        if (!File.Exists(JsonFilePath))
            throw new FileNotFoundException($"فایل province-capitals.json یافت نشد: {JsonFilePath}");

        var json = File.ReadAllText(JsonFilePath);
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var provinces = JsonSerializer.Deserialize<List<ProvinceCapital>>(json, options);

        if (provinces == null)
            throw new InvalidOperationException("خطا در دی‌سریالایز کردن فایل province-capitals.json");

        return provinces;
    }

    /// <summary>
    /// بازگرداندن تمام استان‌ها و پایتخت‌ها
    /// </summary>
    public static IReadOnlyList<ProvinceCapital> GetAllProvinces() =>
        _lazyProvinces.Value.AsReadOnly();

    /// <summary>
    /// دریافت پایتخت بر اساس شناسه استان (ProvinceId)
    /// </summary>
    public static string? GetCapitalByProvinceId(string provinceId) =>
        GetAllProvinces()
            .FirstOrDefault(p => p.ProvinceId == provinceId)?
            .Capital;

    /// <summary>
    /// دریافت پایتخت بر اساس نام استان (بدون حساسیت به بزرگ/کوچکی)
    /// </summary>
    public static string? GetCapitalByProvinceName(string provinceName)
    {
        if (string.IsNullOrWhiteSpace(provinceName))
            return null;

        return GetAllProvinces()
            .FirstOrDefault(p => string.Equals(
                p.ProvinceName,
                provinceName,
                StringComparison.OrdinalIgnoreCase))?
            .Capital;
    }

    /// <summary>
    /// دریافت نام استان بر اساس پایتخت (بدون حساسیت)
    /// </summary>
    public static string? GetProvinceNameByCapital(string capitalName)
    {
        if (string.IsNullOrWhiteSpace(capitalName))
            return null;

        return GetAllProvinces()
            .FirstOrDefault(p => string.Equals(
                p.Capital,
                capitalName,
                StringComparison.OrdinalIgnoreCase))?
            .ProvinceName;
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
        GetAllProvinces().Any(p => string.Equals(p.ProvinceName, provinceName, StringComparison.OrdinalIgnoreCase));
}
