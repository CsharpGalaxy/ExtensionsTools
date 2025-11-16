namespace CsharpGalaxy.LibraryExtension.FakeDataPersian.Attributes;

/// <summary>
/// برای تولید تاریخ شمسی تصادفی با PersianDateTime استفاده می‌شود
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class PersianDateAttribute : Attribute
{
    public PersianDateAttribute()
    {
    }
}

/// <summary>
/// برای تولید نام روز هفته به فارسی استفاده می‌شود (مثل: شنبه، یکشنبه، ...)
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class PersianDayNameAttribute : Attribute
{
    public PersianDayNameAttribute()
    {
    }
}

/// <summary>
/// برای تولید نام ماه شمسی به فارسی استفاده می‌شود (مثل: فروردین، اردیبهشت، ...)
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class PersianMonthNameAttribute : Attribute
{
    public PersianMonthNameAttribute()
    {
    }
}

/// <summary>
/// برای تولید سال شمسی استفاده می‌شود
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class PersianYearAttribute : Attribute
{
    public int MinYear { get; set; } = 1380;
    public int MaxYear { get; set; } = 1410;

    public PersianYearAttribute()
    {
    }

    public PersianYearAttribute(int minYear, int maxYear)
    {
        MinYear = minYear;
        MaxYear = maxYear;
    }
}

/// <summary>
/// برای تولید تاریخ شمسی در بازهٔ مشخص استفاده می‌شود
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class PersianDateRangeAttribute : Attribute
{
    /// <summary>
    /// تاریخ شروع (فرمت: ۱۴۰۰/۰۱/۰۱)
    /// </summary>
    public string StartDate { get; set; }

    /// <summary>
    /// تاریخ پایان (فرمت: ۱۴۱۰/۱۲/۲۹)
    /// </summary>
    public string EndDate { get; set; }

    public PersianDateRangeAttribute(string startDate, string endDate)
    {
        StartDate = startDate;
        EndDate = endDate;
    }
}
