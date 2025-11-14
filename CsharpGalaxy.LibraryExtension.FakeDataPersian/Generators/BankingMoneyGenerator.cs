namespace CsharpGalaxy.LibraryExtension.FakeDataPersian.Generators;

public static class BankingMoneyGenerator
{
    private static readonly Random Random = new();

    private static readonly string[] BankNames = new[]
    {
        "بانک ملت", "بانک ملی", "بانک سامان", "بانک توسعه‌صادرات", "بانک توسعه‌تعاون",
        "بانک اقتصاد‌نوین", "بانک دی", "بانک شهر", "بانک صنعت‌و‌معدن", "بانک تجارت",
        "بانک مسکن", "بانک کشاورزی", "بانک رسالت", "بانک سرمایه", "بانک انصار",
        "موسسهٔ اعتباری توسعه", "بانک آینده", "بانک قرض‌الحسنه", "بانک ایران‌زمین", "بانک حکمت"
    };

    /// <summary>
    /// شماره شبا معتبر ایرانی را برمی‌گرداند (۲۶ رقم)
    /// فرمت: IR۰۱۰۰۱۰۰۰۰۰۰۰۰۰۰۰۰۰۰۰۰
    /// </summary>
    public static string Sheba()
    {
        string iban = "IR01";
        for (int i = 0; i < 22; i++)
        {
            iban += Random.Next(0, 10);
        }
        return iban;
    }

    /// <summary>
    /// شماره شبا با فرمت خوش‌ایند را برمی‌گرداند
    /// فرمت: IR01 0010 0000 0000 0000 0000 0
    /// </summary>
    public static string ShebaFormatted()
    {
        string sheba = Sheba();
        return $"{sheba[..4]} {sheba.Substring(4, 4)} {sheba.Substring(8, 4)} {sheba.Substring(12, 4)} {sheba.Substring(16, 4)} {sheba[20..]}";
    }

    /// <summary>
    /// شماره کارت ۱۶ رقمی را برمی‌گرداند
    /// </summary>
    public static string CardNumber()
    {
        string card = string.Concat(Enumerable.Range(0, 16).Select(_ => Random.Next(0, 10)));
        return card;
    }

    /// <summary>
    /// شماره کارت با فرمت مخصوص را برمی‌گرداند
    /// فرمت: ۶۰۳۷-۹۹۹۹-۹۹۹۹-۹۹۹۹
    /// </summary>
    public static string CardNumberFormatted()
    {
        string card = CardNumber();
        return $"{card[..4]}-{card.Substring(4, 4)}-{card.Substring(8, 4)}-{card[12..]}";
    }

    /// <summary>
    /// نام بانک تصادفی را برمی‌گرداند
    /// </summary>
    public static string BankName() => BankNames[Random.Next(BankNames.Length)];

    /// <summary>
    /// شماره حسابی تصادفی را برمی‌گرداند
    /// </summary>
    public static string AccountNumber()
    {
        return string.Concat(Enumerable.Range(0, 16).Select(_ => Random.Next(0, 10)));
    }

    /// <summary>
    /// CVVS کارت را برمی‌گرداند (۳ یا ۴ رقم)
    /// </summary>
    public static string CardCVV2(bool fourDigit = false)
    {
        int digits = fourDigit ? 4 : 3;
        return string.Concat(Enumerable.Range(0, digits).Select(_ => Random.Next(0, 10)));
    }

    /// <summary>
    /// تاریخ انقضای کارت را برمی‌گرداند (MM/YY)
    /// </summary>
    public static string CardExpiryDate()
    {
        int month = Random.Next(1, 13);
        int year = Random.Next(25, 35); // ۲۵ تا ۳۴

        return $"{month:00}/{year:00}";
    }
}
