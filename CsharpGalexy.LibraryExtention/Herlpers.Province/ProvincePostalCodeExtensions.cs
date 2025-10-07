using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;




namespace CsharpGalexy.LibraryExtention.Extentions.Province;
public class ProvincePostalCode
{
    public string ProvinceName { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
}
/// <summary>
/// Helper برای دریافت کد پستی مراکز استان‌های ایران از فایل JSON
/// </summary>
public static class ProvincePostalCodeHelper
{
    private static readonly Lazy<Dictionary<string, string>> _lazyPostalCodes =
        new Lazy<Dictionary<string, string>>(LoadFromJson);

    private static string JsonFilePath =>
        Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Iran/Provinces/province-postal-codes.json");

    private static Dictionary<string, string> LoadFromJson()
    {
        if (!System.IO. File.Exists(JsonFilePath))
            throw new FileNotFoundException($"فایل province-postal-codes.json یافت نشد: {JsonFilePath}");

        var json = System.IO.File.ReadAllText(JsonFilePath);
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var items = JsonSerializer.Deserialize<List<ProvincePostalCode>>(json, options);

        if (items == null)
            throw new InvalidOperationException("خطا در دی‌سریالایز کردن فایل province-postal-codes.json");

        return items
            .ToDictionary(
                item => item.ProvinceName.Trim(),
                item => item.PostalCode,
                StringComparer.OrdinalIgnoreCase
            );
    }

    /// <summary>
    /// دریافت کد پستی استان بر اساس نام استان
    /// </summary>
    /// <param name="provinceName">نام استان (مثلاً "تهران")</param>
    /// <returns>کد پستی (مثلاً "15957") یا null اگر یافت نشد</returns>
    /// <exception cref="ArgumentException">اگر ورودی خالی باشد</exception>
    public static string? GetPostalCode(string provinceName)
    {
        if (string.IsNullOrWhiteSpace(provinceName))
            throw new ArgumentException("نام استان نمی‌تواند خالی باشد.", nameof(provinceName));

        var key = provinceName.Trim();
        return _lazyPostalCodes.Value.TryGetValue(key, out var code) ? code : null;
    }

    /// <summary>
    /// بررسی اینکه آیا استان در لیست پشتیبانی شده است؟
    /// </summary>
    public static bool IsSupportedProvince(string provinceName)
    {
        if (string.IsNullOrWhiteSpace(provinceName)) return false;
        return _lazyPostalCodes.Value.ContainsKey(provinceName.Trim());
    }

    /// <summary>
    /// دریافت لیست تمام استان‌های پشتیبانی شده
    /// </summary>
    public static string[] GetAllProvinceNames() =>
        _lazyPostalCodes.Value.Keys.ToArray();

    /// <summary>
    /// دریافت تمام داده‌ها به صورت لیست
    /// </summary>
    public static IReadOnlyList<(string ProvinceName, string PostalCode)> GetAllPostalCodes()
    {
        return _lazyPostalCodes.Value
            .Select(kvp => (kvp.Key, kvp.Value))
            .ToList()
            .AsReadOnly();
    }
}