namespace CsharpGalaxy.LibraryExtension.FakeDataPersian.Generators;

using System.Text.RegularExpressions;

public static class IranianNationalCodeGenerator
{
    private static readonly Random Random = new();

    /// <summary>
    /// کد ملی معتبر ۱۰ رقمی را برمی‌گرداند
    /// از الگوریتم کنترل‌رقم استفاده می‌کند
    /// </summary>
    public static string MelliCode()
    {
        // ۹ رقم تصادفی اول
        string code = string.Concat(Enumerable.Range(0, 9).Select(_ => Random.Next(10)));

        // محاسبهٔ رقم کنترل
        int sum = 0;
        for (int i = 0; i < 9; i++)
        {
            sum += (int.Parse(code[i].ToString()) * (10 - i));
        }

        int remainder = sum % 11;
        int checkDigit = remainder < 2 ? remainder : 11 - remainder;

        return code + checkDigit;
    }

    /// <summary>
    /// Check if Iranian national code is valid
    /// Pattern: 10 digits
    /// Uses checksum algorithm validation
    /// </summary>
    public static bool IsValidMelliCode(string code)
    {
        if (string.IsNullOrWhiteSpace(code) || !Regex.IsMatch(code, @"^\d{10}$"))
            return false;

        // مورد خاص: همهٔ ارقام یکسان
        if (code.Distinct().Count() == 1)
            return false;

        // محاسبهٔ کنترل‌رقم
        int sum = 0;
        for (int i = 0; i < 9; i++)
        {
            sum += (int.Parse(code[i].ToString()) * (10 - i));
        }

        int remainder = sum % 11;
        int expectedCheck = remainder < 2 ? remainder : 11 - remainder;
        int actualCheck = int.Parse(code[9].ToString());

        return expectedCheck == actualCheck;
    }

    /// <summary>
    /// کد ملی معتبر را بررسی می‌کند و خروجی می‌دهد
    /// </summary>
    public static (bool IsValid, string Message) ValidateMelliCode(string code)
    {
        if (string.IsNullOrWhiteSpace(code))
            return (false, "کد ملی خالی است");

        if (!Regex.IsMatch(code, @"^\d{10}$"))
            return (false, "کد ملی باید ۱۰ رقم داشته باشد");

        if (code.Distinct().Count() == 1)
            return (false, "کد ملی نمی‌تواند همهٔ ارقام یکسان داشته باشد");

        if (!IsValidMelliCode(code))
            return (false, "رقم کنترل کد ملی نامعتبر است");

        return (true, "کد ملی معتبر است");
    }
}
