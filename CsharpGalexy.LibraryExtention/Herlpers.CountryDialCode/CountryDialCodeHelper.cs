

//public static class CountryDialCodeHelper
//{
//    public class CountryDialCode
//    {
//        public string DialCode { get; set; }
//        public string PersianCountryName { get; set; }
//        public string EnglishCountryName { get; set; }
//    }


//    public static string GetPersianCountryByDialCode(string dialCode)
//    {
//        var entry = CountryDialCodes.FirstOrDefault(x => x.DialCode == dialCode);
//        return entry?.PersianCountryName;
//    }

//    public static string GetEnglishCountryByDialCode(string dialCode)
//    {
//        var entry = CountryDialCodes.FirstOrDefault(x => x.DialCode == dialCode);
//        return entry?.EnglishCountryName;
//    }

//    public static string GetDialCodeByPersianCountry(string persianCountryName)
//    {
//        var entry = CountryDialCodes.FirstOrDefault(x => x.PersianCountryName == persianCountryName);
//        return entry?.DialCode;
//    }

//    public static string GetDialCodeByEnglishCountry(string englishCountryName)
//    {
//        var entry = CountryDialCodes.FirstOrDefault(x => x.EnglishCountryName == englishCountryName);
//        return entry?.DialCode;
//    }

//    public static List<CountryDialCode> GetAllCountriesSortedByDialCode()
//    {
//        return CountryDialCodes.OrderBy(x => int.Parse(x.DialCode.Trim('+'))).ToList();
//    }
//}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

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
    /// کلاس کمکی برای مدیریت کدهای تلفن کشورها با بارگذاری از فایل JSON
    /// </summary>
    public static class CountryDialCodeHelper
    {
        // Lazy loading برای بارگذاری تنها یک‌بار و ایمن برای چندنخی
        private static readonly Lazy<List<CountryDialCode>> _lazyCountries =
            new Lazy<List<CountryDialCode>>(LoadFromJson);

        // مسیر فایل JSON (در پوشه‌ی خروجی برنامه)
        private static string JsonFilePath => 
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Iran/Provinces/country-dial-codes.json");

        /// <summary>
        /// بارگذاری داده‌ها از فایل JSON
        /// </summary>
        private static List<CountryDialCode> LoadFromJson()
        {
            if (!File.Exists(JsonFilePath))
                throw new FileNotFoundException(
                    $"فایل country-dial-codes.json یافت نشد. مسیر: {JsonFilePath}");

            try
            {
                var json = File.ReadAllText(JsonFilePath);
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var countries = JsonSerializer.Deserialize<List<CountryDialCode>>(json, options);

                if (countries == null)
                    throw new InvalidOperationException("دی‌سریالایز کردن فایل JSON با شکست مواجه شد.");

                return countries;
            }
            catch (JsonException ex)
            {
                throw new InvalidOperationException(
                    "خطا در پردازش فایل country-dial-codes.json. لطفاً ساختار JSON را بررسی کنید.", ex);
            }
        }

        /// <summary>
        /// بازگرداندن تمام کشورها
        /// </summary>
        public static IReadOnlyList<CountryDialCode> GetAllCountries() =>
            _lazyCountries.Value.AsReadOnly();

        /// <summary>
        /// دریافت نام فارسی کشور بر اساس کد تلفن
        /// </summary>
        public static string? GetPersianCountryByDialCode(string dialCode) =>
            GetAllCountries()
                .FirstOrDefault(c => c.DialCode == dialCode)?
                .PersianCountryName;

        /// <summary>
        /// دریافت نام انگلیسی کشور بر اساس کد تلفن
        /// </summary>
        public static string? GetEnglishCountryByDialCode(string dialCode) =>
            GetAllCountries()
                .FirstOrDefault(c => c.DialCode == dialCode)?
                .EnglishCountryName;

        /// <summary>
        /// دریافت کد تلفن بر اساس نام فارسی کشور (بدون حساسیت به بزرگ/کوچکی)
        /// </summary>
        public static string? GetDialCodeByPersianCountry(string persianCountryName)
        {
            if (string.IsNullOrWhiteSpace(persianCountryName))
                return null;

            return GetAllCountries()
                .FirstOrDefault(c => string.Equals(
                    c.PersianCountryName,
                    persianCountryName,
                    StringComparison.OrdinalIgnoreCase))?
                .DialCode;
        }

        /// <summary>
        /// دریافت کد تلفن بر اساس نام انگلیسی کشور (بدون حساسیت به بزرگ/کوچکی)
        /// </summary>
        public static string? GetDialCodeByEnglishCountry(string englishCountryName)
        {
            if (string.IsNullOrWhiteSpace(englishCountryName))
                return null;

            return GetAllCountries()
                .FirstOrDefault(c => string.Equals(
                    c.EnglishCountryName,
                    englishCountryName,
                    StringComparison.OrdinalIgnoreCase))?
                .DialCode;
        }

        /// <summary>
        /// دریافت تمام کشورها مرتب‌شده بر اساس کد تلفن (عددی)
        /// </summary>
        public static IReadOnlyList<CountryDialCode> GetAllCountriesSortedByDialCode()
        {
            return GetAllCountries()
                .Where(c => !string.IsNullOrEmpty(c.DialCode) && c.DialCode.StartsWith("+"))
                .OrderBy(c =>
                {
                    // حذف '+' و تبدیل به عدد برای مرتب‌سازی صحیح
                    var numericPart = c.DialCode.Substring(1);
                    return long.TryParse(numericPart, out var num) ? num : long.MaxValue;
                })
                .ToList()
                .AsReadOnly();
        }
    }
}