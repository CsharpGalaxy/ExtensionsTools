namespace CsharpGalaxy.LibraryExtension.FakeDataPersian.Generators;

using System.Text.RegularExpressions;

public static class IranianMobileGenerator
{
    private static readonly Random Random = new();
    // پیش‌شماره‌های معتبر اپراتورهای ایران
    private static readonly string[] Operators = new[]
    {
    // همراه اول
    "0910", "0911", "0912", "0913", "0914", "0915", "0916", "0917", "0918", "0919",
    // ایرانسل
    "0901", "0902", "0903", "0904", "0905", "0906", "0907", "0908", "0909",
    "0930", "0931", "0932", "0933", "0934", "0935", "0936", "0937", "0938", "0939",
    "0941",
    // رایتل
    "0920", "0921", "0922",
    // تالیا (قدیمی ولی هنوز بعضی شماره‌ها فعالند)
    "0932"
};

    // نگاشت پیش‌شماره به نام اپراتور
    private static readonly Dictionary<string, string> OperatorNames = new()
{
    { "090", "ایرانسل" },
    { "091", "همراه‌اول" },
    { "092", "رایتل" },
    { "093", "ایرانسل/تالیا" },
    { "094", "ایرانسل" }
};


    /// <summary>
    /// شماره موبایل معتبر ایرانی را برمی‌گرداند (فرمت: ۰۹۱۲۳۴۵۶۷۸۹)
    /// </summary>
    public static string Mobile()
    {
        string operatorPrefix = Operators[Random.Next(Operators.Length)];
        int remainingLength = 11 - operatorPrefix.Length; // تعداد رقم‌های باقی‌مانده

        string remaining = string.Concat(
            Enumerable.Range(0, remainingLength).Select(_ => Random.Next(10))
        );

        return operatorPrefix + remaining;
    }

    /// <summary>
    /// شماره موبایل با فرمت مخصوص را برمی‌گرداند (۰۹۱۲-۹۹۹-۹۹۹۹)
    /// </summary>
    public static string MobileFormatted()
    {
        string mobile = Mobile();
        return $"{mobile[..4]}-{mobile.Substring(4, 3)}-{mobile[7..]}";
    }

    /// <summary>
    /// نام اپراتور را برمی‌گرداند
    /// </summary>
    public static string Operator()
    {
        string prefix = Operators[Random.Next(Operators.Length)][..3];
        return OperatorNames.TryGetValue(prefix, out var name) ? name : "نامشخص";
    }

    /// <summary>
    /// بررسی معتبر بودن شماره موبایل ایرانی
    /// الگو: ۰۹[۰-۹]{9}
    /// </summary>
    public static bool IsValidMobile(string mobile)
    {
        if (string.IsNullOrWhiteSpace(mobile))
            return false;

        return Regex.IsMatch(mobile, @"^09\d{9}$");
    }
}
