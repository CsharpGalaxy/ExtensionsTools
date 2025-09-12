namespace ExtentionLibrary.DateTimes;

using System;
using System.Globalization;


/// <summary>
/// Extension methods for DateTime and DateTimeOffset
/// </summary>
public static class DateTimeExtensions
{
    private static readonly string[] PersianMonths =
    {
        "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور",
        "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند"
    };
    #region Persian (Shamsi) Date Conversions

    /// <summary>
    /// Converts Gregorian date to Persian (Shamsi) date
    /// </summary>
    /// <param name="dateTime">Gregorian DateTime</param>
    /// <returns>Persian date as string (e.g. 1403/02/15)</returns>
    public static string ToShamsiDate(this DateTime dateTime)
    {
        var pc = new PersianCalendar();
        return $"{pc.GetYear(dateTime):0000}/{pc.GetMonth(dateTime):00}/{pc.GetDayOfMonth(dateTime):00}";
    }

    /// <summary>
    /// Converts Gregorian date to Persian date with custom format
    /// </summary>
    /// <param name="dateTime">Gregorian DateTime</param>
    /// <param name="format">Format like "yyyy/MM/dd" or "dd MMMM yyyy"</param>
    /// <returns>Formatted Persian date string</returns>
    public static string ToShamsiDate(this DateTime dateTime, string format)
    {
        var pc = new PersianCalendar();
        int year = pc.GetYear(dateTime);
        int month = pc.GetMonth(dateTime);
        int day = pc.GetDayOfMonth(dateTime);

        // جایگزینی ابتدا نام ماه (MMMM) سپس MM و dd و yyyy
        string result = format
            .Replace("MMMM", PersianMonths[month - 1])
            .Replace("yyyy", year.ToString("0000"))
            .Replace("MM", month.ToString("00"))
            .Replace("dd", day.ToString("00"));

        return result;
    }

    /// <summary>
    /// Gets Persian day of week name
    /// </summary>
    /// <param name="dateTime">Gregorian DateTime</param>
    /// <returns>روز، دوشنبه، ...، شنبه</returns>
    public static string GetPersianDayOfWeek(this DateTime dateTime)
    {
        var days = new[] { "شنبه", "یکشنبه", "دوشنبه", "سه‌شنبه", "چهارشنبه", "پنجشنبه", "جمعه" };
        // تبدیل DayOfWeek به ایندکس ایران
        int index = ((int)dateTime.DayOfWeek + 1) % 7;
        return days[index];
    }

    /// <summary>
    /// Returns the Persian month name of the given date
    /// </summary>
    public static string GetPersianMonth(this DateTime dateTime)
    {
        var pc = new PersianCalendar();
        int month = pc.GetMonth(dateTime);
        return PersianMonths[month - 1];
    }
    #endregion

    #region Date Comparisons

    /// <summary>
    /// Checks if the date is today
    /// </summary>
    public static bool IsToday(this DateTime dateTime)
    {
        return dateTime.Date == DateTime.Today;
    }

    /// <summary>
    /// Checks if the date is yesterday
    /// </summary>
    public static bool IsYesterday(this DateTime dateTime)
    {
        return dateTime.Date == DateTime.Today.AddDays(-1);
    }

    /// <summary>
    /// Checks if the date is tomorrow
    /// </summary>
    public static bool IsTomorrow(this DateTime dateTime)
    {
        return dateTime.Date == DateTime.Today.AddDays(1);
    }

    /// <summary>
    /// Checks if the date is in the future
    /// </summary>
    public static bool IsFuture(this DateTime dateTime)
    {
        return dateTime > DateTime.Now;
    }

    /// <summary>
    /// Checks if the date is in the past
    /// </summary>
    public static bool IsPast(this DateTime dateTime)
    {
        return dateTime < DateTime.Now;
    }

    /// <summary>
    /// Checks if the date is within the current week (Sunday to Saturday)
    /// </summary>
    public static bool IsThisWeek(this DateTime dateTime)
    {
        var today = DateTime.Today;
        var startOfWeek = today.AddDays(-(int)today.DayOfWeek);
        var endOfWeek = startOfWeek.AddDays(6);
        return dateTime.Date >= startOfWeek && dateTime.Date <= endOfWeek;
    }

    /// <summary>
    /// Checks if the date is within the current month
    /// </summary>
    public static bool IsThisMonth(this DateTime dateTime)
    {
        var now = DateTime.Now;
        return dateTime.Year == now.Year && dateTime.Month == now.Month;
    }

    /// <summary>
    /// Checks if the date is within the current year
    /// </summary>
    public static bool IsThisYear(this DateTime dateTime)
    {
        return dateTime.Year == DateTime.Now.Year;
    }

    #endregion

    #region Date Calculations

    /// <summary>
    /// Adds a specified number of business days (excluding weekends)
    /// </summary>
    /// <param name="dateTime">Start date</param>
    /// <param name="days">Number of business days to add (can be negative)</param>
    /// <returns>New date with business days added</returns>
    public static DateTime AddBusinessDays(this DateTime dateTime, int days)
    {
        var sign = Math.Sign(days);
        var unsignedDays = Math.Abs(days);
        var currentDate = dateTime;

        while (unsignedDays > 0)
        {
            currentDate = currentDate.AddDays(sign);
            if (currentDate.DayOfWeek != DayOfWeek.Saturday && currentDate.DayOfWeek != DayOfWeek.Friday) // Assuming Fri/Sat weekend
            {
                unsignedDays--;
            }
        }

        return currentDate;
    }

    /// <summary>
    /// Gets the first day of the month
    /// </summary>
    public static DateTime FirstDayOfMonth(this DateTime dateTime)
    {
        return new DateTime(dateTime.Year, dateTime.Month, 1);
    }

    /// <summary>
    /// Gets the last day of the month
    /// </summary>
    public static DateTime LastDayOfMonth(this DateTime dateTime)
    {
        return dateTime.FirstDayOfMonth().AddMonths(1).AddDays(-1);
    }

    /// <summary>
    /// Gets the first day of the year
    /// </summary>
    public static DateTime FirstDayOfYear(this DateTime dateTime)
    {
        return new DateTime(dateTime.Year, 1, 1);
    }

    /// <summary>
    /// Gets the last day of the year
    /// </summary>
    public static DateTime LastDayOfYear(this DateTime dateTime)
    {
        return new DateTime(dateTime.Year, 12, 31);
    }

    /// <summary>
    /// Gets number of days in the month
    /// </summary>
    public static int DaysInMonth(this DateTime dateTime)
    {
        return DateTime.DaysInMonth(dateTime.Year, dateTime.Month);
    }

    #endregion

    #region Time Ago / Human Readable

    /// <summary>
    /// Returns a human-readable string for how long ago a date was
    /// e.g. "2 دقیقه پیش", "3 ساعت پیش", "دیروز", "2 هفته پیش"
    /// </summary>
    public static string TimeAgo(this DateTime dateTime)
    {
        var ts = DateTime.Now - dateTime;
        var delta = ts.TotalSeconds;

        if (delta < 60)
        {
            return "هم اکنون";
        }
        if (delta < 120)
        {
            return "1 دقیقه پیش";
        }
        if (delta < 3600)
        {
            return $"{Math.Floor(delta / 60)} دقیقه پیش";
        }
        if (delta < 7200)
        {
            return "1 ساعت پیش";
        }
        if (delta < 86400)
        {
            return $"{Math.Floor(delta / 3600)} ساعت پیش";
        }

        if (delta < 172800)
        {
            return "دیروز";
        }
        if (delta < 604800)
        {
            return $"{Math.Floor(delta / 86400)} روز پیش";
        }

        if (delta < 1209600)
        {
            return "1 هفته پیش";
        }

        if (delta < 2592000)
        {
            return $"{Math.Floor(delta / 604800)} هفته پیش";
        }

        if (delta < 5184000)
        {
            return "1 ماه پیش";
        }

        if (delta < 31536000)
        {
            return $"{Math.Floor(delta / 2592000)} ماه پیش";
        }

        if (delta < 63072000)
        {
            return "1 سال پیش";
        }

        return $"{Math.Floor(delta / 31536000)} سال پیش";
    }

    #endregion

    #region Working with DateTimeOffset

    /// <summary>
    /// Converts DateTimeOffset to Unix timestamp (seconds since epoch)
    /// </summary>
    public static long ToUnixTimestamp(this DateTimeOffset dateTimeOffset)
    {
        return dateTimeOffset.ToUnixTimeSeconds();
    }

    /// <summary>
    /// Converts Unix timestamp to DateTimeOffset
    /// </summary>
    public static DateTimeOffset FromUnixTimestamp(long timestamp)
    {
        return new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero).AddSeconds(timestamp);
    }

    #endregion

    #region Start and End of Day

    /// <summary>
    /// Gets the start of the day (00:00:00)
    /// </summary>
    public static DateTime StartOfDay(this DateTime dateTime)
    {
        return dateTime.Date;
    }

    /// <summary>
    /// Gets the end of the day (23:59:59)
    /// </summary>
    public static DateTime EndOfDay(this DateTime dateTime)
    {
        return dateTime.Date.AddHours(23).AddMinutes(59).AddSeconds(59).AddMilliseconds(999);
    }

    #endregion

    #region Age Calculation

    /// <summary>
    /// Calculates age in years from birth date
    /// </summary>
    public static int Age(this DateTime birthDate)
    {
        var today = DateTime.Today;
        var age = today.Year - birthDate.Year;
        if (birthDate.Date > today.AddYears(-age))
            age--;
        return age;
    }

    #endregion

    #region Weekend & Weekday

    /// <summary>
    /// Checks if the date is a weekend (Friday or Saturday in Iran)
    /// </summary>
    public static bool IsWeekend(this DateTime dateTime)
    {
        return dateTime.DayOfWeek == DayOfWeek.Friday || dateTime.DayOfWeek == DayOfWeek.Saturday;
    }

    /// <summary>
    /// Checks if the date is a weekday (not weekend)
    /// </summary>
    public static bool IsWeekday(this DateTime dateTime)
    {
        return !IsWeekend(dateTime);
    }

    #endregion

    #region Truncate

    /// <summary>
    /// Truncates DateTime to a specified time unit (second, minute, hour, day)
    /// </summary>
    public static DateTime Truncate(this DateTime dateTime, TimeSpan timeSpan)
    {
        if (timeSpan == TimeSpan.Zero) return dateTime; // Or could throw an exception
        return dateTime.AddTicks(-(dateTime.Ticks % timeSpan.Ticks));
    }

    /// <summary>
    /// Truncates DateTime to nearest second
    /// </summary>
    public static DateTime TruncateToSecond(this DateTime dateTime)
    {
        return dateTime.Truncate(TimeSpan.FromSeconds(1));
    }

    /// <summary>
    /// Truncates DateTime to nearest minute
    /// </summary>
    public static DateTime TruncateToMinute(this DateTime dateTime)
    {
        return dateTime.Truncate(TimeSpan.FromMinutes(1));
    }

    /// <summary>
    /// Truncates DateTime to nearest hour
    /// </summary>
    public static DateTime TruncateToHour(this DateTime dateTime)
    {
        return dateTime.Truncate(TimeSpan.FromHours(1));
    }

    #endregion
}
