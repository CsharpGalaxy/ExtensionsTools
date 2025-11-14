namespace CsharpGalaxy.LibraryExtension.FakeDataPersian.Generators;

public static class PersianDateGenerator
{
    private static readonly Random Random = new();

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
        int currentYear = DateTime.Now.Year; // تبدیل میلادی به شمسی ساده (حدودی)
        int shamsiYear = currentYear - age - 621;

        int month = Random.Next(1, 13);
        int day = Random.Next(1, 31);

        return $"{shamsiYear:0000}/{month:00}/{day:00}";
    }
}
