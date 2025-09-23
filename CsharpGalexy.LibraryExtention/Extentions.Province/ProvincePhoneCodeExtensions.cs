using System.Globalization;

/// <summary>
/// Extension Method برای دریافت پیش‌شماره تلفن استان‌های ایران
/// فقط بر اساس نام استان — بدون شهرهای فرعی
/// مثال: "تهران".GetProvincePhoneCode() → "021"
/// </summary>
public static class ProvincePhoneCodeExtensions
{
    // 🗺️ دیکشنری پیش‌شماره استان‌های ایران — فقط مراکز استان
    private static readonly Dictionary<string, string> ProvincePhoneCodes =
        new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "تهران", "021" },
            { "اصفهان", "031" },
            { "خراسان رضوی", "051" },
            { "فارس", "071" },
            { "آذربایجان شرقی", "041" },
            { "خوزستان", "061" },
            { "البرز", "026" },
            { "قم", "025" },
            { "یزد", "035" },
            { "گیلان", "013" },
            { "آذربایجان غربی", "044" },
            { "سیستان و بلوچستان", "054" },
            { "کردستان", "087" },
            { "خراسان شمالی", "058" },
            { "مازندران", "011" },
            { "هرمزگان", "076" },
            { "لرستان", "066" },
            { "قزوین", "028" },
            { "همدان", "081" },
            { "کهگیلویه و بویراحمد", "083" },
            { "اردبیل", "045" },
            { "خراسان جنوبی", "056" },
            { "زنجان", "024" },
            { "سمنان", "023" },
            { "ایلام", "084" },
            { "گلستان", "017" },
            { "بوشهر", "077" },
            { "مرکزی", "086" },
            { "چهارمحال و بختیاری", "038" }
        };

    /// <summary>
    /// دریافت پیش‌شماره تلفن استان بر اساس نام استان
    /// </summary>
    /// <param name="provinceName">نام استان — مثال: "تهران"</param>
    /// <param name="culture">فرهنگ (اختیاری — پیش‌فرض: فارسی)</param>
    /// <returns>پیش‌شماره استان (مثلاً "021") یا null اگر استان یافت نشد</returns>
    /// <exception cref="ArgumentException">اگر ورودی خالی باشد</exception>
    public static string? GetProvincePhoneCode(this string provinceName, CultureInfo? culture = null)
    {
        if (string.IsNullOrWhiteSpace(provinceName))
            throw new ArgumentException("نام استان نمی‌تواند خالی باشد.", nameof(provinceName));

        var key = provinceName.Trim();

        return ProvincePhoneCodes.TryGetValue(key, out var code) ? code : null;
    }

    /// <summary>
    /// [اختیاری] بررسی اینکه آیا استان در لیست پشتیبانی شده است؟
    /// </summary>
    public static bool IsSupportedProvince(this string provinceName)
    {
        if (string.IsNullOrWhiteSpace(provinceName)) return false;
        return ProvincePhoneCodes.ContainsKey(provinceName.Trim());
    }

    /// <summary>
    /// [اختیاری] دریافت لیست تمام استان‌های پشتیبانی شده
    /// </summary>
    public static string[] GetAllProvinces()
    {
        return ProvincePhoneCodes.Keys.ToArray();
    }
}