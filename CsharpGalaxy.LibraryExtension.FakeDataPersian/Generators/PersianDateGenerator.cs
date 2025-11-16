namespace CsharpGalaxy.LibraryExtension.FakeDataPersian.Generators;

public static class PersianDateGenerator
{
    private static readonly Random Random = new();

    // فهرست نام‌های روز‌های هفته به فارسی
    private static readonly string[] PersianDayNames = 
    {
        "شنبه",    // Saturday
        "یکشنبه",  // Sunday
        "دوشنبه",  // Monday
        "سه‌شنبه",  // Tuesday
        "چهارشنبه", // Wednesday
        "پنج‌شنبه", // Thursday
        "جمعه"     // Friday
    };

    // فهرست نام‌های ماه‌های شمسی به فارسی
    private static readonly string[] PersianMonthNames =
    {
        "فروردین",  // Month 1
        "اردیبهشت", // Month 2
        "خرداد",    // Month 3
        "تیر",     // Month 4
        "مرداد",    // Month 5
        "شهریور",   // Month 6
        "مهر",     // Month 7
        "آبان",    // Month 8
        "آذر",     // Month 9
        "دی",      // Month 10
        "بهمن",    // Month 11
        "اسفند"    // Month 12
    };

    #region تاریخ شمسی ساده

    /// <summary>
    /// تاریخ شمسی تصادفی را برمی‌گرداند (فرمت: ۱۴۰۳/۰۳/۱۴)
    /// </summary>
    public static string ShamsiDate()
    {
        int year = Random.Next(1380, 1410); // ۱۳۸۰ تا ۱۴۰۹
        int month = Random.Next(1, 13);
        int day = Random.Next(1, 31);

        return $"{year:0000}/{month:00}/{day:00}";
    }

    /// <summary>
    /// تاریخ و ساعت شمسی را برمی‌گرداند (فرمت: ۱۴۰۳/۰۳/۱۴ ۱۶:۴۵:۲۲)
    /// </summary>
    public static string ShamsiDateTime()
    {
        string date = ShamsiDate();
        int hour = Random.Next(0, 24);
        int minute = Random.Next(0, 60);
        int second = Random.Next(0, 60);

        return $"{date} {hour:00}:{minute:00}:{second:00}";
    }

    /// <summary>
    /// سن تصادفی در بازهٔ مشخص شده را برمی‌گرداند
    /// </summary>
    public static int Age(int minAge = 18, int maxAge = 60)
    {
        return Random.Next(minAge, maxAge + 1);
    }

    /// <summary>
    /// تاریخ تولد شمسی بر اساس سن را برمی‌گرداند
    /// </summary>
    public static string BirthDate(int age)
    {
        int currentYear = DateTime.Now.Year;
        int shamsiYear = currentYear - age - 621;

        int month = Random.Next(1, 13);
        int day = Random.Next(1, 31);

        return $"{shamsiYear:0000}/{month:00}/{day:00}";
    }

    #endregion

    #region روز و ماه به فارسی

    /// <summary>
    /// نام روز هفته به فارسی برمی‌گرداند (مثل: شنبه، یکشنبه، ...)
    /// </summary>
    public static string GetDayNameFarsi()
    {
        return PersianDayNames[Random.Next(PersianDayNames.Length)];
    }

    /// <summary>
    /// نام روز هفته برای DateTime معین به فارسی برمی‌گرداند
    /// </summary>
    public static string GetDayNameFarsi(DateTime dateTime)
    {
        return PersianDayNames[(int)dateTime.DayOfWeek];
    }

    /// <summary>
    /// نام ماه شمسی به فارسی برمی‌گرداند (مثل: فروردین، اردیبهشت، ...)
    /// </summary>
    public static string GetMonthNameFarsi(int month)
    {
        if (month < 1 || month > 12)
            throw new ArgumentOutOfRangeException(nameof(month), "ماه باید بین ۱ تا ۱۲ باشد");

        return PersianMonthNames[month - 1];
    }

    /// <summary>
    /// نام ماه شمسی تصادفی به فارسی برمی‌گرداند
    /// </summary>
    public static string GetRandomMonthNameFarsi()
    {
        return PersianMonthNames[Random.Next(PersianMonthNames.Length)];
    }

    #endregion

    #region تاریخ شمسی پیشرفته

    /// <summary>
    /// شی PersianDate ساده برای نمایندگی تاریخ شمسی
    /// </summary>
    public class PersianDateTime
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }
        public int Second { get; set; }

        public PersianDateTime()
        {
        }

        public PersianDateTime(int year, int month, int day, int hour = 0, int minute = 0, int second = 0)
        {
            Year = year;
            Month = month;
            Day = day;
            Hour = hour;
            Minute = minute;
            Second = second;
        }

        public PersianDateTime(DateTime gregorianDate)
        {
            ConvertFromGregorian(gregorianDate);
        }

        public PersianDateTime(string shamsiDateString)
        {
            // فرمت: ۱۴۰۰/۰۱/۰۱
            var parts = shamsiDateString.Split('/');
            if (parts.Length >= 3)
            {
                Year = int.Parse(parts[0]);
                Month = int.Parse(parts[1]);
                Day = int.Parse(parts[2]);
            }
        }

        private void ConvertFromGregorian(DateTime gregorianDate)
        {
            int gy = gregorianDate.Year;
            int gm = gregorianDate.Month;
            int gd = gregorianDate.Day;

            int g_d_n = 365 * gy + ((gy + 3) / 4) - ((gy + 99) / 100) + ((gy + 399) / 400);
            for (int i = 0; i < gm; ++i)
                g_d_n += DateTime.DaysInMonth(gy, i + 1);

            int j_d_n = g_d_n - 79;
            int j_np = j_d_n / 12053;
            j_d_n %= 12053;

            int jy = 979 + 2820 * j_np + 33 * (j_d_n / 1029983);
            j_d_n %= 1029983;

            if (j_d_n >= 366)
            {
                jy += (j_d_n - 1) / 365;
                j_d_n = (j_d_n - 1) % 365;
            }

            int jm = 1;
            int jd = 1;

            if (j_d_n < 186)
            {
                jm = 1 + j_d_n / 31;
                jd = 1 + (j_d_n % 31);
            }
            else
            {
                jm = 7 + (j_d_n - 186) / 30;
                jd = 1 + ((j_d_n - 186) % 30);
            }

            Year = jy;
            Month = jm;
            Day = jd;
            Hour = gregorianDate.Hour;
            Minute = gregorianDate.Minute;
            Second = gregorianDate.Second;
        }

        public DateTime ToDateTime()
        {
            int jy = Year;
            int jm = Month;
            int jd = Day;

            int jy2 = jy + 1474;
            if (jm > 10)
                jy2 += 1;

            int j_d_n = 365 * jy + (30 * jm) - (jm / 7) * 6 - 30 + (jy2 / 2820) * 1029983 + ((jy2 % 2820) / 4) * 1461 + ((jy2 % 2820) % 4) / 1 * 365 + (jd - 1) + 79;

            int gy = 400 * (j_d_n / 146097);
            j_d_n %= 146097;
            int flag = 1;

            if (j_d_n >= 36525)
            {
                j_d_n -= 1;
                gy += 100 * (j_d_n / 36524);
                j_d_n %= 36524;
                if (j_d_n >= 365)
                    j_d_n += 1;
                flag = 0;
            }

            gy += 4 * (j_d_n / 1461);
            j_d_n %= 1461;

            if (flag == 1 && j_d_n >= 366)
            {
                j_d_n -= 1;
                gy += j_d_n / 365;
                j_d_n = j_d_n % 365;
            }

            int gm = 0;
            int[] gd_days = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            if (IsLeapYear(gy))
                gd_days[1] = 29;

            for (int i = 0; i < 12; i++)
            {
                int v = gd_days[i];
                if (j_d_n < v)
                    break;
                j_d_n -= v;
                gm++;
            }

            return new DateTime(gy, gm + 1, j_d_n + 1, Hour, Minute, Second);
        }

        private bool IsLeapYear(int year)
        {
            return (year % 400 == 0) || (year % 100 != 0 && year % 4 == 0);
        }

        public override string ToString()
        {
            return $"{Year:0000}/{Month:00}/{Day:00}";
        }
    }

    /// <summary>
    /// تاریخ شمسی تصادفی به صورت PersianDateTime برمی‌گرداند
    /// </summary>
    public static PersianDateTime GetRandomPersianDateTime()
    {
        var gregorianDate = new DateTime(Random.Next(1960, 2025), Random.Next(1, 13), Random.Next(1, 28));
        return new PersianDateTime(gregorianDate);
    }

    /// <summary>
    /// تاریخ شمسی امروز را برمی‌گرداند
    /// </summary>
    public static PersianDateTime GetTodayPersian()
    {
        return new PersianDateTime(DateTime.Now);
    }

    /// <summary>
    /// تاریخ شمسی دیروز را برمی‌گرداند
    /// </summary>
    public static PersianDateTime GetYesterdayPersian()
    {
        return new PersianDateTime(DateTime.Now.AddDays(-1));
    }

    /// <summary>
    /// تاریخ شمسی فردا را برمی‌گرداند
    /// </summary>
    public static PersianDateTime GetTomorrowPersian()
    {
        return new PersianDateTime(DateTime.Now.AddDays(1));
    }

    /// <summary>
    /// سال شمسی تصادفی برمی‌گرداند
    /// </summary>
    public static int GetRandomShamsiYear(int minYear = 1380, int maxYear = 1410)
    {
        return Random.Next(minYear, maxYear + 1);
    }

    /// <summary>
    /// ماه شمسی تصادفی برمی‌گرداند (۱-۱۲)
    /// </summary>
    public static int GetRandomShamsiMonth()
    {
        return Random.Next(1, 13);
    }

    /// <summary>
    /// روز شمسی تصادفی برمی‌گرداند (۱-۳۰)
    /// </summary>
    public static int GetRandomShamsiDay()
    {
        return Random.Next(1, 31);
    }

    /// <summary>
    /// تعداد روزهای یک ماه شمسی را برمی‌گرداند
    /// </summary>
    public static int GetDaysInMonth(int year, int month)
    {
        if (month < 1 || month > 12)
            throw new ArgumentOutOfRangeException(nameof(month), "ماه باید بین ۱ تا ۱۲ باشد");

        // شش ماه اول: ۳۱ روز، پنج ماه دوم: ۳۰ روز، ماه آخر: ۲۹ یا ۳۰ روز
        if (month <= 6) return 31;
        if (month <= 11) return 30;
        
        return IsLeapYear(year) ? 30 : 29;
    }

    /// <summary>
    /// تعداد کل روزهای یک سال شمسی را برمی‌گرداند
    /// </summary>
    public static int GetDaysInYear(int year)
    {
        return IsLeapYear(year) ? 366 : 365;
    }

    /// <summary>
    /// بررسی می‌کند که آیا سال شمسی کبیسه است یا نه
    /// </summary>
    public static bool IsLeapYear(int shamsiYear)
    {
        // الگوریتم کبیسه‌سال شمسی
        return ((shamsiYear + 1474) % 2820 + 474 + 38) / 128 == ((shamsiYear + 1474) % 2820 + 474) / 128;
    }

    /// <summary>
    /// روز سال را برمی‌گرداند (۱-۳۶۶)
    /// </summary>
    public static int GetDayOfYear(int year, int month, int day)
    {
        if (month < 1 || month > 12)
            throw new ArgumentOutOfRangeException(nameof(month), "ماه باید بین ۱ تا ۱۲ باشد");

        int dayOfYear = 0;
        for (int m = 1; m < month; m++)
        {
            dayOfYear += GetDaysInMonth(year, m);
        }
        dayOfYear += day;

        return dayOfYear;
    }

    /// <summary>
    /// هفتهٔ سال را برمی‌گرداند (۱-۵۳)
    /// </summary>
    public static int GetWeekOfYear(int year, int month, int day)
    {
        int dayOfYear = GetDayOfYear(year, month, day);
        return (dayOfYear - 1) / 7 + 1;
    }

    /// <summary>
    /// تفاوت روزها بین دو تاریخ شمسی را برمی‌گرداند
    /// </summary>
    public static int GetDaysBetween(string shamsiDate1, string shamsiDate2)
    {
        var pd1 = new PersianDateTime(shamsiDate1);
        var pd2 = new PersianDateTime(shamsiDate2);
        return Math.Abs((pd2.ToDateTime() - pd1.ToDateTime()).Days);
    }

    /// <summary>
    /// تبدیل تاریخ میلادی به شمسی برمی‌گرداند
    /// </summary>
    public static PersianDateTime ConvertToShamsi(DateTime gregorianDate)
    {
        return new PersianDateTime(gregorianDate);
    }

    /// <summary>
    /// تبدیل تاریخ شمسی به میلادی برمی‌گرداند
    /// </summary>
    public static DateTime ConvertToGregorian(string shamsiDate)
    {
        var pd = new PersianDateTime(shamsiDate);
        return pd.ToDateTime();
    }

    /// <summary>
    /// تبدیل تاریخ شمسی (اجزاء جداگانه) به میلادی برمی‌گرداند
    /// </summary>
    public static DateTime ConvertToGregorian(int shamsiYear, int shamsiMonth, int shamsiDay)
    {
        string shamsiDateString = $"{shamsiYear:0000}/{shamsiMonth:00}/{shamsiDay:00}";
        return ConvertToGregorian(shamsiDateString);
    }

    /// <summary>
    /// تاریخ شمسی بین دو تاریخ را برمی‌گرداند
    /// </summary>
    public static PersianDateTime GetRandomDateBetween(string startShamsiDate, string endShamsiDate)
    {
        var start = new PersianDateTime(startShamsiDate).ToDateTime();
        var end = new PersianDateTime(endShamsiDate).ToDateTime();
        
        TimeSpan timeSpan = end - start;
        int randomDays = Random.Next((int)timeSpan.TotalDays);
        
        return new PersianDateTime(start.AddDays(randomDays));
    }

    /// <summary>
    /// تاریخ شمسی تولد بر اساس سن را برمی‌گرداند (PersianDateTime)
    /// </summary>
    public static PersianDateTime GetBirthDatePersian(int age)
    {
        var birthDate = DateTime.Now.AddYears(-age);
        return new PersianDateTime(birthDate);
    }

    #endregion
}
