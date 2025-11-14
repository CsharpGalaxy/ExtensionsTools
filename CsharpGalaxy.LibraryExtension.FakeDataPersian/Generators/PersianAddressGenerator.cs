namespace CsharpGalaxy.LibraryExtension.FakeDataPersian.Generators;

public static class PersianAddressGenerator
{
    private static readonly Random Random = new();

    private static readonly string[] Cities = new[]
    {
        "تهران", "مشهد", "اصفهان", "شیراز", "کرج", "قم", "اهواز", "تبریز", "کیش", "کرمانشاه",
        "اردبیل", "یزد", "بندرعباس", "رشت", "هرمزگان", "سنندج", "بیرجند", "زاهدان", "ساری", "بابل",
        "قائم‌شهر", "نوشهر", "چالوس", "گرگان", "گیلان", "لاهیجان", "ملایر", "همدان", "سمنان", "آمل"
    };

    private static readonly string[] Provinces = new[]
    {
        "تهران", "خراسان‌رضوی", "اصفهان", "فارس", "البرز", "قم", "خوزستان", "اردبیل", "یزد", "کرمانشاه",
        "آذربایجان‌شرقی", "هرمزگان", "کردستان", "سیستان‌وبلوچستان", "مازندران", "گیلان", "مرکزی", "لرستان", "خراسان‌شمالی", "خراسان‌جنوبی"
    };

    private static readonly string[] StreetNames = new[]
    {
        "خیابان فرهنگ", "خیابان انقلاب", "خیابان کاوه", "خیابان ولیعصر", "خیابان صادقیه", "خیابان جمهوری",
        "خیابان پاسداران", "خیابان ستارخان", "خیابان آزادی", "خیابان شهید‌قندی"
    };

    private static readonly string[] StreetTypes = new[]
    {
        "کوچه", "بزرگراه", "بولوار", "راهخانه", "جادهٔ"
    };

    /// <summary>
    /// نام شهر تصادفی را برمی‌گرداند
    /// </summary>
    public static string City() => Cities[Random.Next(Cities.Length)];

    /// <summary>
    /// نام استان را برمی‌گرداند
    /// </summary>
    public static string Province() => Provinces[Random.Next(Provinces.Length)];

    /// <summary>
    /// نام خیابان را برمی‌گرداند
    /// </summary>
    public static string StreetName() => StreetNames[Random.Next(StreetNames.Length)];

    /// <summary>
    /// نوع کوچه را برمی‌گرداند
    /// </summary>
    public static string StreetType() => StreetTypes[Random.Next(StreetTypes.Length)];

    /// <summary>
    /// آدرس کامل تصادفی را برمی‌گرداند
    /// </summary>
    public static string FullAddress()
    {
        string province = Province();
        string city = City();
        string street = StreetName();
        string streetType = StreetType();
        int houseNumber = Random.Next(1, 500);
        int unitNumber = Random.Next(1, 20);

        return $"{province}، {city}، {street}، {streetType} {houseNumber}، واحد {unitNumber}";
    }

    /// <summary>
    /// آدرس کوتاه تر را برمی‌گرداند
    /// </summary>
    public static string ShortAddress()
    {
        string city = City();
        string street = StreetName();
        return $"{city}، {street}";
    }
}
