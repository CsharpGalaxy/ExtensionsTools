namespace CsharpGalaxy.LibraryExtension.FakeDataPersian.Examples;

using Generators;
using Helpers;

/// <summary>
/// نمونه‌های استفاده از کتابخانهٔ تولید داده‌های تصادفی فارسی
/// </summary>
public class UsageExamples
{
    public static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Console.WriteLine("=== نمونه استفاده از PersianFakeData ===\n");

        // مثال ۱: نام‌ها
        Console.WriteLine("📌 نام‌های تصادفی:");
        for (int i = 0; i < 3; i++)
        {
            Console.WriteLine($"  • {PersianNameGenerator.FullName()} - پدر: {PersianNameGenerator.FatherName()}");
        }

        // مثال ۲: موبایل
        Console.WriteLine("\n📌 شماره‌های موبایل:");
        for (int i = 0; i < 3; i++)
        {
            var mobile = IranianMobileGenerator.Mobile();
            var isValid = IranianMobileGenerator.IsValidMobile(mobile);
            Console.WriteLine($"  • {mobile} ({IranianMobileGenerator.Operator()}) - معتبر: {isValid}");
        }

        // مثال ۳: کد ملی
        Console.WriteLine("\n📌 کدهای ملی:");
        for (int i = 0; i < 3; i++)
        {
            var code = IranianNationalCodeGenerator.MelliCode();
            Console.WriteLine($"  • {code} - معتبر: {IranianNationalCodeGenerator.IsValidMelliCode(code)}");
        }

        // مثال ۴: آدرس
        Console.WriteLine("\n📌 آدرس‌های تصادفی:");
        for (int i = 0; i < 2; i++)
        {
            Console.WriteLine($"  • {PersianAddressGenerator.FullAddress()}");
        }

        // مثال ۵: تاریخ
        Console.WriteLine("\n📌 تاریخ و سن:");
        Console.WriteLine($"  • امروز (شمسی): {PersianDateGenerator.ShamsiDate()}");
        Console.WriteLine($"  • تاریخ و ساعت: {PersianDateGenerator.ShamsiDateTime()}");
        Console.WriteLine($"  • سن تصادفی: {PersianDateGenerator.Age(20, 50)} سال");

        // مثال ۶: متن
        Console.WriteLine("\n📌 متن‌های تصادفی:");
        Console.WriteLine($"  • جمله: {PersianTextGenerator.Sentence()}");
        Console.WriteLine($"  • ایمیل: {PersianTextGenerator.Email()}");
        Console.WriteLine($"  • نام‌کاربری: {PersianTextGenerator.Username()}");

        // مثال ۷: بانکی
        Console.WriteLine("\n📌 اطلاعات بانکی:");
        Console.WriteLine($"  • شبا: {BankingMoneyGenerator.ShebaFormatted()}");
        Console.WriteLine($"  • کارت: {BankingMoneyGenerator.CardNumberFormatted()}");
        Console.WriteLine($"  • بانک: {BankingMoneyGenerator.BankName()}");
        Console.WriteLine($"  • CVV: {BankingMoneyGenerator.CardCVV2()}");
        Console.WriteLine($"  • انقضا: {BankingMoneyGenerator.CardExpiryDate()}");

        // مثال ۸: شبکه
        Console.WriteLine("\n📌 اطلاعات شبکه:");
        Console.WriteLine($"  • IPv4: {InternetCryptoGenerator.IPv4()}");
        Console.WriteLine($"  • IPv4 خصوصی: {InternetCryptoGenerator.IPv4Private()}");
        Console.WriteLine($"  • MAC: {InternetCryptoGenerator.MAC()}");
        Console.WriteLine($"  • GUID: {InternetCryptoGenerator.GuidString()}");
        Console.WriteLine($"  • Token: {InternetCryptoGenerator.Token()}");

        // مثال ۹: کمک‌های مجموعه‌ای
        Console.WriteLine("\n📌 کمک‌های مجموعه‌ای:");
        var names = CollectionHelper.RandomList(
            () => PersianNameGenerator.FullName(),
            count: 5
        );
        Console.WriteLine($"  • ۵ نام تصادفی:");
        foreach (var name in names)
        {
            Console.WriteLine($"    - {name}");
        }

        // مثال ۱۰: لیست یکتا
        Console.WriteLine("\n📌 لیست موبایل‌های یکتا:");
        var uniqueMobiles = CollectionHelper.UniqueList(
            () => IranianMobileGenerator.Mobile(),
            count: 3
        );
        foreach (var mobile in uniqueMobiles)
        {
            Console.WriteLine($"  • {mobile}");
        }

        // مثال ۱۱: Shuffle
        Console.WriteLine("\n📌 ترتیب تصادفی:");
        var numbers = new List<int> { 1, 2, 3, 4, 5 };
        var shuffled = numbers.Shuffle();
        Console.WriteLine($"  • اصلی: {string.Join(", ", numbers)}");
        Console.WriteLine($"  • مخلوط: {string.Join(", ", shuffled)}");

        // مثال ۱۲: Batch
        Console.WriteLine("\n📌 تقسیم به دسته‌ها:");
        var users = Enumerable.Range(1, 10)
            .Select(i => $"کاربر {i}")
            .ToList();
        var batches = users.Batch(3);
        for (int i = 0; i < batches.Count; i++)
        {
            Console.WriteLine($"  • دستهٔ {i + 1}: {string.Join(", ", batches[i])}");
        }

        Console.WriteLine("\n✅ تمام مثال‌ها اجرا شد!");
    }
}
