using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;




namespace CsharpGalexy.LibraryExtention.Extentions.Province;
public class ProvincePhoneCode
{
    public string ProvinceName { get; set; } = string.Empty;
    public string PhoneCode { get; set; } = string.Empty;
}
/// <summary>
/// Helper برای دریافت پیش‌شماره تلفن مراکز استان‌های ایران از فایل JSON
/// </summary>
public static class ProvincePhoneCodeHelper
{
    private static readonly Lazy<Dictionary<string, string>> _lazyPhoneCodes =
        new Lazy<Dictionary<string, string>>(LoadFromJson);

    private static string JsonFilePath =>
        Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Iran/Provinces/province-phone-codes.json");

    private static Dictionary<string, string> LoadFromJson()
    {
        if (!System.IO.File.Exists(JsonFilePath))
            throw new FileNotFoundException($"فایل province-phone-codes.json یافت نشد: {JsonFilePath}");

        var json = System.IO.File.ReadAllText(JsonFilePath);
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var items = JsonSerializer.Deserialize<List<ProvincePhoneCode>>(json, options);

        if (items == null)
            throw new InvalidOperationException("خطا در دی‌سریالایز کردن فایل province-phone-codes.json");

        // تبدیل لیست به دیکشنری با مقایسه‌ی بدون حساسیت به بزرگ/کوچکی
        return items
            .ToDictionary(
                item => item.ProvinceName.Trim(),
                item => item.PhoneCode,
                StringComparer.OrdinalIgnoreCase
            );
    }

    /// <summary>
    /// دریافت پیش‌شماره تلفن استان بر اساس نام استان
    /// </summary>
    /// <param name="provinceName">نام استان (مثلاً "تهران")</param>
    /// <returns>پیش‌شماره (مثلاً "021") یا null اگر یافت نشد</returns>
    /// <exception cref="ArgumentException">اگر ورودی خالی باشد</exception>
    public static string? GetPhoneCode(string provinceName)
    {
        if (string.IsNullOrWhiteSpace(provinceName))
            throw new ArgumentException("نام استان نمی‌تواند خالی باشد.", nameof(provinceName));

        var key = provinceName.Trim();
        return _lazyPhoneCodes.Value.TryGetValue(key, out var code) ? code : null;
    }

    /// <summary>
    /// بررسی اینکه آیا استان در لیست پشتیبانی شده است؟
    /// </summary>
    public static bool IsSupportedProvince(string provinceName)
    {
        if (string.IsNullOrWhiteSpace(provinceName)) return false;
        return _lazyPhoneCodes.Value.ContainsKey(provinceName.Trim());
    }

    /// <summary>
    /// دریافت لیست تمام استان‌های پشتیبانی شده
    /// </summary>
    public static string[] GetAllProvinceNames() =>
        _lazyPhoneCodes.Value.Keys.ToArray();

    /// <summary>
    /// دریافت تمام داده‌ها به صورت لیست
    /// </summary>
    public static IReadOnlyList<(string ProvinceName, string PhoneCode)> GetAllPhoneCodes()
    {
        return _lazyPhoneCodes.Value
            .Select(kvp => (kvp.Key, kvp.Value))
            .ToList()
            .AsReadOnly();
    }
}