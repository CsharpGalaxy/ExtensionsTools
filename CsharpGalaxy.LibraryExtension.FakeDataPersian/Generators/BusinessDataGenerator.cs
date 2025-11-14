namespace CsharpGalaxy.LibraryExtension.FakeDataPersian.Generators;

public static class BusinessDataGenerator
{
    private static readonly Random Random = new();

    private static readonly string[] CompanyNames = new[]
    {
        "شرکت فناوری پیشرو", "مؤسسهٔ توسعهٔ دیجیتال", "مرکز نوآوری ایرانی",
        "تیم طراحی خلاق", "شرکت خدمات رایانه‌ای", "توسعه‌دهندگان فارسی",
        "نرم‌افزار ایران", "خدمات ابری ایرانی", "شرکت راهکارهای تجاری",
        "سیستم‌های هوشمند ایرانی"
    };

    private static readonly string[] JobTitles = new[]
    {
        "مدیر عمومی", "مدیر فروش", "مدیر منابع انسانی", "معمار نرم‌افزار",
        "توسعه‌دهندهٔ تمام‌وقت", "طراح رابط کاربری", "متخصص پایگاه‌داده",
        "تست‌کنندهٔ نرم‌افزار", "مشاور تجاری", "کارشناس پشتیبانی"
    };

    private static readonly string[] ProjectStatuses = new[]
    {
        "در حال پیشرفت", "تکمیل‌شده", "معلق", "برای راه‌اندازی آماده",
        "در بررسی", "لغوشده"
    };

    private static readonly string[] PaymentMethods = new[]
    {
        "تراکنش بانکی", "دستورالعمل پرداخت", "چک", "نقد", "کارت اعتباری", "درگاه پرداخت"
    };

    private static readonly string[] InvoiceStatuses = new[]
    {
        "پرداخت‌شده", "معلق", "تاخیر در پرداخت", "لغوشده", "درحال‌بررسی"
    };

    /// <summary>
    /// نام شرکت تصادفی را برمی‌گرداند
    /// </summary>
    public static string CompanyName() => CompanyNames[Random.Next(CompanyNames.Length)];

    /// <summary>
    /// شماره شناسایی ملی (Melli ID) شرکت را برمی‌گرداند
    /// </summary>
    public static string CompanyMelliId()
    {
        return string.Concat(Enumerable.Range(0, 10).Select(_ => Random.Next(0, 10)));
    }

    /// <summary>
    /// شماره ثبت شرکت را برمی‌گرداند
    /// </summary>
    public static string CompanyRegistrationNumber()
    {
        return string.Concat(Enumerable.Range(0, 8).Select(_ => Random.Next(0, 10)));
    }

    /// <summary>
    /// عنوان شغلی تصادفی را برمی‌گرداند
    /// </summary>
    public static string JobTitle() => JobTitles[Random.Next(JobTitles.Length)];

    /// <summary>
    /// شماره قرارداد را برمی‌گرداند
    /// </summary>
    public static string ContractNumber()
    {
        return $"CNT-{DateTime.Now.Year:0000}-{Random.Next(1000, 9999)}";
    }

    /// <summary>
    /// شماره پروژه را برمی‌گرداند
    /// </summary>
    public static string ProjectNumber()
    {
        return $"PRJ-{Random.Next(100000, 999999)}";
    }

    /// <summary>
    /// وضعیت پروژه را برمی‌گرداند
    /// </summary>
    public static string ProjectStatus() => ProjectStatuses[Random.Next(ProjectStatuses.Length)];

    /// <summary>
    /// درصد پیشرفت پروژه را برمی‌گرداند
    /// </summary>
    public static int ProjectProgress() => Random.Next(0, 101);

    /// <summary>
    /// شماره فاکتور را برمی‌گرداند
    /// </summary>
    public static string InvoiceNumber()
    {
        return $"INV-{DateTime.Now.Year}-{Random.Next(10000, 99999)}";
    }

    /// <summary>
    /// وضعیت فاکتور را برمی‌گرداند
    /// </summary>
    public static string InvoiceStatus() => InvoiceStatuses[Random.Next(InvoiceStatuses.Length)];

    /// <summary>
    /// مبلغ تصادفی را برمی‌گرداند
    /// </summary>
    public static decimal Amount(decimal minAmount = 1000m, decimal maxAmount = 1000000m)
    {
        return minAmount + (decimal)(Random.NextDouble() * (double)(maxAmount - minAmount));
    }

    /// <summary>
    /// مبلغ گرد‌شده را برمی‌گرداند
    /// </summary>
    public static decimal RoundedAmount(decimal minAmount = 1000m, decimal maxAmount = 1000000m)
    {
        var amount = Amount(minAmount, maxAmount);
        return Math.Round(amount / 1000) * 1000;
    }

    /// <summary>
    /// روش پرداخت تصادفی را برمی‌گرداند
    /// </summary>
    public static string PaymentMethod() => PaymentMethods[Random.Next(PaymentMethods.Length)];

    /// <summary>
    /// شماره مرجع تراکنش را برمی‌گرداند
    /// </summary>
    public static string TransactionReference()
    {
        return "TXN-" + string.Concat(Enumerable.Range(0, 12).Select(_ => Random.Next(0, 10)));
    }

    /// <summary>
    /// شماره فیش بانکی را برمی‌گرداند
    /// </summary>
    public static string BankSlipNumber()
    {
        return string.Concat(Enumerable.Range(0, 13).Select(_ => Random.Next(0, 10)));
    }

    /// <summary>
    /// شماره سفارش را برمی‌گرداند
    /// </summary>
    public static string OrderNumber()
    {
        return $"ORD-{DateTime.Now:yyyyMMdd}-{Random.Next(1000, 9999)}";
    }

    /// <summary>
    /// کد محصول SKU را برمی‌گرداند
    /// </summary>
    public static string ProductSKU()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Range(0, 8)
            .Select(_ => chars[Random.Next(chars.Length)])
            .ToArray());
    }

    /// <summary>
    /// قیمت واحد را برمی‌گرداند
    /// </summary>
    public static decimal UnitPrice(decimal minPrice = 100m, decimal maxPrice = 100000m)
    {
        return Math.Round(minPrice + (decimal)(Random.NextDouble() * (double)(maxPrice - minPrice)), 2);
    }

    /// <summary>
    /// کمیسیون یا تخفیف را برمی‌گرداند
    /// </summary>
    public static decimal CommissionOrDiscount(decimal minPercent = 0m, decimal maxPercent = 30m)
    {
        return Math.Round(minPercent + (decimal)(Random.NextDouble() * (double)(maxPercent - minPercent)), 2);
    }

    /// <summary>
    /// شماره حساب مشتری را برمی‌گرداند
    /// </summary>
    public static string CustomerAccountNumber()
    {
        return $"ACC-{Random.Next(100000, 999999)}";
    }

    /// <summary>
    /// درجه اعتبار مشتری را برمی‌گرداند
    /// </summary>
    public static string CustomerCreditRating()
    {
        string[] ratings = { "عالی", "خوب", "متوسط", "ضعیف" };
        return ratings[Random.Next(ratings.Length)];
    }

    /// <summary>
    /// حد اعتباری مشتری را برمی‌گرداند
    /// </summary>
    public static decimal CustomerCreditLimit()
    {
        return RoundedAmount(10000m, 10000000m);
    }
}
