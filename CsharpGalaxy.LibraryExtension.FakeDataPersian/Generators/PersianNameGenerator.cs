namespace CsharpGalaxy.LibraryExtension.FakeDataPersian.Generators;

public static class PersianNameGenerator
{
    private static readonly Random Random = new();

    private static readonly string[] FirstNames = new[]
    {
        "زهرا", "علی", "کیان", "سارا", "محمد", "فاطمه", "رضا", "لیا", "حسن", "مریم",
        "بهرام", "الهام", "علیرضا", "نیکی", "سیاوش", "دلارام", "مهران", "پریا", "سیاوش", "شیما",
        "امیر", "نسرین", "فرهاد", "کارن", "بابک", "ناهید", "مسعود", "شیرین", "داریوش", "آزاده"
    };
    private static readonly string[] UserNames = new[]
{
    "alex", "sara", "mohammad", "fatemeh", "reza", "maryam",
    "nima", "shirin", "dariush", "azadeh", "kian", "paria",
    "armin", "samira", "farhad", "babak", "nahid", "siavash",
    "nikki", "elham", "mehran", "zahra", "amir", "hossein"
};


    private static readonly string[] LastNames = new[]
    {
        "رضوی", "نجفی", "کاظمی", "حسینی", "علوی", "صفوی", "صادقی", "مرادی", "احمدی", "علیزاده",
        "محمودی", "فردی", "کریمی", "سالمی", "نظری", "فضلی", "عزیزی", "شریفی", "حسنی", "شاهی",
        "رفیعی", "سعیدی", "طاهری", "اسدی", "عباسی", "رضایی", "موسوی", "حسن‌پور", "یوسفی", "باقری"
    };

    private static readonly string[] FatherNames = new[]
    {
        "غلام‌رضا", "محمدعلی", "غلام‌حسن", "احمدعلی", "علی‌اکبر", "محمدحسن", "حسن", "علی", "محمد", "رضا",
        "اسماعیل", "ابراهیم", "اسحاق", "یوسف", "محمود", "داریوش", "آرشام", "بهزاد", "کیومرث", "هوشیار"
    };

    /// <summary>
    /// نام اول را برمی‌گرداند
    /// </summary>
    public static string FirstName() => FirstNames[Random.Next(FirstNames.Length)];

    /// <summary>
    /// نام خانوادگی را برمی‌گرداند
    /// </summary>
    public static string LastName() => LastNames[Random.Next(LastNames.Length)];
    public static string UserName() => UserNames[Random.Next(UserNames.Length)] + Random.Next(1000, 9999999);

    /// <summary>
    /// نام و نام خانوادگی را برمی‌گرداند
    /// </summary>
    public static string FullName() => $"{FirstName()} {LastName()}";

    /// <summary>
    /// نام پدر را برمی‌گرداند (برای فرم‌های اداری)
    /// </summary>
    public static string FatherName() => FatherNames[Random.Next(FatherNames.Length)];
}
