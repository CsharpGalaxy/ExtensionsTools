using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace CsharpGalexy.LibraryExtention.Helpers.Mony;

using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

public static class CurrencyExtensions
{
    // CultureInfo فارسی (ایران) برای فرمت‌های محلی
    private static readonly CultureInfo PersianCulture = new CultureInfo("fa-IR");
    private const string TOMAN_UNIT = "تومان";
    private const string RIAL_UNIT = "ریال";

    // --- توابع کمکی ---

    /// <summary>
    /// ایجاد NumberFormatInfo سفارشی برای تومان
    /// </summary>
    private static NumberFormatInfo GetTomanFormatInfo(int? decimalDigits = null)
    {
        var nfi = (NumberFormatInfo)PersianCulture.NumberFormat.Clone();
        nfi.CurrencySymbol = TOMAN_UNIT;
        // الگوی قرارگیری واحد: 12,000 تومان
        nfi.CurrencyPositivePattern = 3;
        nfi.CurrencyNegativePattern = 8; // (12,000 تومان)-

        if (decimalDigits.HasValue)
        {
            nfi.CurrencyDecimalDigits = decimalDigits.Value;
        }
        else
        {
            // مدیریت خودکار اعشار
            nfi.CurrencyDecimalDigits = 0;
        }

        return nfi;
    }

    /// <summary>
    /// تبدیل ارقام انگلیسی به فارسی (۰ تا ۹)
    /// </summary>
    private static string ToPersianDigitsInternal(string input)
    {
        if (string.IsNullOrEmpty(input)) return input;

        string result = input;
        for (int i = 0; i <= 9; i++)
        {
            // استفاده از NativeDigits برای اطمینان از صحت تبدیل
            result = result.Replace(i.ToString(), PersianCulture.NumberFormat.NativeDigits[i]);
        }
        return result;
    }

    // --- متدهای نمایش واحد پولی فارسی ---

    /// <summary>
    /// ToRialString(): نمایش مبلغ با واحد ریال و جداکننده هزارگان (مثال: 1,500,000 ریال)
    /// </summary>
    public static string ToRialString(this decimal value)
    {
        var nfi = (NumberFormatInfo)PersianCulture.NumberFormat.Clone();
        nfi.NumberDecimalDigits = 0;
        // استفاده از ToPersianDigits برای اطمینان از نمایش فارسی ارقام
        return ToPersianDigitsInternal(value.ToString("N0", nfi) + " " + RIAL_UNIT);
    }

    /// <summary>
    /// ToTomanString(): تبدیل مبلغ به تومان (تقسیم بر 10) و نمایش با فرمت مناسب (مثال: 150,000 تومان)
    /// </summary>
    public static string ToTomanString(this decimal value)
    {
        var tomanValue = value / 10m;
        // تعیین اعشار: 0 برای اعداد صحیح، 2 برای اعشاری
        int digits = (tomanValue == Math.Truncate(tomanValue)) ? 0 : 2;
        var nfi = GetTomanFormatInfo(digits);

        // N فرمت عددی است، سپس واحد را دستی اضافه می‌کنیم تا مطمئن شویم جداکننده هزارگان اعمال شده است.
        return ToPersianDigitsInternal(tomanValue.ToString("N", nfi).Trim() + " " + TOMAN_UNIT);
    }

    /// <summary>
    /// ToCurrencyString(string unit): نمایش مبلغ با واحد دلخواه (ریال، تومان، دلار...)
    /// </summary>
    public static string ToCurrencyString(this decimal value, string unit)
    {
        var nfi = (NumberFormatInfo)PersianCulture.NumberFormat.Clone();
        nfi.NumberDecimalDigits = 0;

        return ToPersianDigitsInternal(value.ToString("N0", nfi) + " " + unit);
    }

    // --- متدهای قالب‌بندی فارسی/محلی ---

    /// <summary>
    /// ToPersianDigits(): تبدیل ارقام انگلیسی به فارسی برای نمایش بومی‌شده
    /// این متد برای خروجی‌های فرمت‌دهی شده مفید است.
    /// </summary>
    public static string ToPersianDigits(this decimal value, string format = "N0")
    {
        // فرمت کردن با CultureInfo فارسی و سپس تبدیل ارقام به فارسی
        return ToPersianDigitsInternal(value.ToString(format, PersianCulture.NumberFormat));
    }

    /// <summary>
    /// ToLocalizedMoney(CultureInfo culture): فرمت مبلغ بر اساس Culture
    /// </summary>
    public static string ToLocalizedMoney(this decimal value, CultureInfo culture)
    {
        // استفاده از فرمت "C" (Currency) که واحد پول فرهنگ را به کار می‌گیرد.
        return value.ToString("C", culture.NumberFormat);
    }

    /// <summary>
    /// FormatWithCulture(string cultureCode = "fa-IR"): فرمت مبلغ بر اساس Culture Code
    /// </summary>
    public static string FormatWithCulture(this decimal value, string cultureCode = "fa-IR", string format = "N0")
    {
        try
        {
            var culture = new CultureInfo(cultureCode);
            return value.ToString(format, culture.NumberFormat);
        }
        catch (CultureNotFoundException)
        {
            return value.ToString(format); // بازگشت به پیش‌فرض اگر Culture نامعتبر باشد
        }
    }

    // --- متدهای نمایش خلاصه‌شده و خوانا ---

    /// <summary>
    /// ToMoneyWithSuffix()/ToReadablePrice(): نمایش خلاصه‌شده با پسوند عددی مثل "هزار"، "میلیون"
    /// </summary>
    public static string ToMoneyWithSuffix(this decimal value)
    {
        var tomanValue = value / 10m;
        var absValue = Math.Abs(tomanValue);
        var sign = tomanValue < 0 ? "-" : "";

        if (absValue >= 1000000m)
        {
            // میلیون
            var formatted = (absValue / 1000000m).ToString("N1", PersianCulture.NumberFormat);
            return sign + ToPersianDigitsInternal(formatted) + " میلیون " + TOMAN_UNIT;
        }
        else if (absValue >= 1000m)
        {
            // هزار
            var formatted = (absValue / 1000m).ToString("N0", PersianCulture.NumberFormat);
            return sign + ToPersianDigitsInternal(formatted) + " هزار " + TOMAN_UNIT;
        }
        else
        {
            return tomanValue.ToTomanString();
        }
    }

    /// <summary>
    /// ToCompactMoney(): نمایش فشرده با K/M (مثال: 1.5M تومان)
    /// </summary>
    public static string ToCompactMoney(this decimal value)
    {
        var tomanValue = value / 10m;
        var absValue = Math.Abs(tomanValue);
        var sign = tomanValue < 0 ? "-" : "";
        var nfi = PersianCulture.NumberFormat;

        if (absValue >= 1000000m)
        {
            // M (Million)
            var formatted = (absValue / 1000000m).ToString("N1", nfi);
            return sign + formatted.TrimEnd('0').TrimEnd(nfi.NumberDecimalSeparator.ToCharArray()) + "M " + TOMAN_UNIT;
        }
        else if (absValue >= 1000m)
        {
            // K (Kilo)
            var formatted = (absValue / 1000m).ToString("N1", nfi);
            return sign + formatted.TrimEnd('0').TrimEnd(nfi.NumberDecimalSeparator.ToCharArray()) + "K " + TOMAN_UNIT;
        }
        else
        {
            return tomanValue.ToTomanString();
        }
    }

    // --- متدهای حسابداری و حقوقی/عملیات مالی ---

    /// <summary>
    /// ToAccountingFormat(): نمایش مبلغ با فرمت حسابداری (نمایش منفی‌ها داخل پرانتز)
    /// </summary>
    public static string ToAccountingFormat(this decimal value)
    {
        var nfi = (NumberFormatInfo)PersianCulture.NumberFormat.Clone();
        nfi.CurrencyDecimalDigits = 0; // بدون اعشار
        nfi.CurrencySymbol = RIAL_UNIT;

        // الگوی منفی: 0 = ($n)، 1 = -$n، 2 = $-n، 3 = $n-، 4 = (n$)
        // برای فارسی (پرانتز دور عدد): 4
        nfi.CurrencyNegativePattern = 4;

        return value.ToString("C", nfi);
    }

    // نکته: پیاده‌سازی متد ToMoneyWords (تبدیل عدد به حروف) پیچیده است و معمولاً نیاز به یک کتابخانه خارجی دارد.
    // در اینجا یک پیاده‌سازی ساده‌شده ارائه می‌شود (فقط نمونه متد).

    /// <summary>
    /// ToMoneyWords(): تبدیل عدد به حروف فارسی (نیاز به کتابخانه خارجی یا پیاده‌سازی کامل)
    /// </summary>
    public static string ToMoneyWords(this decimal value, string currencyUnit = RIAL_UNIT)
    {
        // در پروژه‌های واقعی از یک Nuget Package مانند NumberToWord یا تبدیلگرهای کامل فارسی استفاده کنید.
        if (value == 0) return "صفر " + currencyUnit;
        if (value == 1000000m) return "یک میلیون " + currencyUnit;

        // شبیه‌سازی خروجی
        return $"[نیاز به کتابخانه خارجی] {value.ToRialString()} به حروف";
    }

    /// <summary>
    /// ToColoredMoney(): نمایش رشته رنگی برای مثبت/منفی (نیاز به فریمورک UI)
    /// </summary>
    public static string ToColoredMoney(this decimal value)
    {
        // در محیط‌های Console/Web/WPF/WinForms باید منطق رنگ اعمال شود.
        // در اینجا فقط یک پیش‌نمایش متنی می‌دهیم.
        if (value < 0)
        {
            return $"[قرمز] {value.ToTomanString()}"; // بدهی
        }
        else
        {
            return $"[سبز] {value.ToTomanString()}"; // درآمد
        }
    }

    /// <summary>
    /// RoundToNearest(int step): گرد کردن مبلغ به نزدیک‌ترین عدد مشخص
    /// </summary>
    public static decimal RoundToNearest(this decimal value, int step = 100)
    {
        if (step <= 0) return value;
        // گرد کردن به نزدیک‌ترین ضریب step
        return Math.Round(value / step, MidpointRounding.AwayFromZero) * step;
    }

    /// <summary>
    /// ApplyDiscount(decimal percent): محاسبه مبلغ بعد از اعمال تخفیف درصدی
    /// </summary>
    public static decimal ApplyDiscount(this decimal value, decimal percent)
    {
        if (percent < 0) percent = 0;
        if (percent > 100) percent = 100;
        return value * (1m - (percent / 100m));
    }

    /// <summary>
    /// AddTax(decimal percent): افزودن مالیات بر مبلغ
    /// </summary>
    public static decimal AddTax(this decimal value, decimal percent)
    {
        if (percent < 0) percent = 0;
        return value * (1m + (percent / 100m));
    }

    /// <summary>
    /// ConvertToCurrency(decimal rate): تبدیل مبلغ به ارز دیگر بر اساس نرخ تبدیل داده‌شده
    /// </summary>
    public static decimal ConvertToCurrency(this decimal value, decimal rate)
    {
        if (rate <= 0) return 0m;
        return value * rate;
    }

    /// <summary>
    /// IsAffordable(decimal balance): بررسی اینکه آیا موجودی کاربر برای پرداخت مبلغ کافی است یا نه
    /// </summary>
    public static bool IsAffordable(this decimal value, decimal balance)
    {
        // value در اینجا هزینه است و balance موجودی کاربر.
        return balance >= value;
    }

    /// <summary>
    /// ToPercentageOf(decimal total): محاسبه درصد مبلغ نسبت به کل
    /// </summary>
    public static decimal ToPercentageOf(this decimal value, decimal total)
    {
        if (total == 0m) return 0m;
        // بازگشت درصد به صورت عدد (نه رشته)
        return (value / total) * 100m;
    }

    /// <summary>
    /// NormalizeCurrency(): حذف صفرهای اضافی یا تبدیل مبالغ بزرگ به واحدهای بالاتر (از ریال به تومان)
    /// </summary>
    public static string NormalizeCurrency(this decimal value)
    {
        // در این پیاده‌سازی، آن را به واحد 'تومان' تبدیل می‌کند چون واحد رایج‌تر است.
        return value.ToTomanString();
    }

    // --- متدهای قبلی (برای تکمیل درخواست) ---

    // استفاده مجدد از متدهای قبلی برای جلوگیری از تکرار کد:
    public static string ToCurrency(this decimal value) => value.ToCurrencyString(RIAL_UNIT);
    public static string ToToman(this decimal value) => value.ToTomanString();
    public static string ToRial(this decimal value) => value.ToRialString();
    public static string ToPrice(this decimal value) => value.ToTomanString(); // ساده شده = تومان
    public static string ToFormattedPrice(this decimal value) => value.ToTomanString(); // پیشرفته = تومان
    public static string ToReadablePrice(this decimal value) => value.ToMoneyWithSuffix();
    public static string ToCurrencySymbol(this decimal value) => value.ToTomanString(); // افزودن واحد تومان
    public static string ToZeroIfNegative(this decimal value) => (value < 0 ? 0m : value).ToTomanString();
    public static string ToFreeIfZero(this decimal value) => value == 0m ? "رایگان" : value.ToTomanString();

    /// <summary>
    /// ToDiscountedPrice(decimal originalPrice): نمایش درصد تخفیف یا قیمت بعد از تخفیف
    /// </summary>
    public static string ToDiscountedPrice(this decimal discountedPrice, decimal originalPrice)
    {
        if (discountedPrice >= originalPrice || originalPrice == 0)
        {
            return discountedPrice.ToTomanString();
        }

        var percent = discountedPrice.ToPercentageOf(originalPrice);
        var discountPercentage = (100m - percent);

        return $"{discountedPrice.ToTomanString()} ({discountPercentage:N0}٪ تخفیف)";
    }

    public static string ToMoneyWords(this decimal rialValue)
    {
        var toman = (long)(rialValue / 10);
        // ساده‌سازی: فقط برای اعداد کوچک
        if (toman == 0) return "صفر تومان";
        if (toman == 1) return "یک تومان";
        if (toman < 1000)
            return $"{NumberToWords(toman)} تومان";
        return $"{NumberToWords(toman)} تومان"; // پیاده‌سازی کامل NumberToWords در پایین
    }

    private static string NumberToWords(long number)
    {
        if (number == 0) return "صفر";
        if (number < 0) return "منفی " + NumberToWords(Math.Abs(number));

        string[] units = { "", "یک", "دو", "سه", "چهار", "پنج", "شش", "هفت", "هشت", "نه" };
        string[] teens = { "ده", "یازده", "دوازده", "سیزده", "چهارده", "پانزده", "شانزده", "هفده", "هجده", "نوزده" };
        string[] tens = { "", "", "بیست", "سی", "چهل", "پنجاه", "شصت", "هفتاد", "هشتاد", "نود" };
        string[] hundreds = { "", "یکصد", "دویست", "سیصد", "چهارصد", "پانصد", "ششصد", "هفتصد", "هشتصد", "نهصد" };

        if (number < 10) return units[number];
        if (number < 20) return teens[number - 10];
        if (number < 100) return tens[number / 10] + (number % 10 != 0 ? " و " + units[number % 10] : "");
        if (number < 1000) return hundreds[number / 100] + (number % 100 != 0 ? " و " + NumberToWords(number % 100) : "");

        // پشتیبانی از هزار و میلیون (ساده‌شده)
        if (number < 1_000_000)
            return NumberToWords(number / 1000) + " هزار" + (number % 1000 != 0 ? " و " + NumberToWords(number % 1000) : "");
        if (number < 1_000_000_000)
            return NumberToWords(number / 1_000_000) + " میلیون" + (number % 1_000_000 != 0 ? " و " + NumberToWords(number % 1_000_000) : "");

        return number.ToString(); // fallback
    }
    public static string ToPersianDigits(this string input)
    {
        var persianNumbers = new[] { "۰", "۱", "۲", "۳", "۴", "۵", "۶", "۷", "۸", "۹" };
        var result = new StringBuilder(input);
        for (int i = 0; i < 10; i++)
            result.Replace(i.ToString(), persianNumbers[i]);
        return result.ToString();
    }

    // Overload برای decimal
    public static string ToPersianDigits(this decimal value)
        => value.ToString("N0").ToPersianDigits(); public static string ToMoneyWithSuffix(this decimal rialValue)
    {
        var toman = rialValue / 10;
        if (toman >= 1_000_000_000) return $"{toman / 1_000_000_000:0.##} میلیارد تومان";
        if (toman >= 1_000_000) return $"{toman / 1_000_000:0.##} میلیون تومان";
        if (toman >= 1_000) return $"{toman / 1_000:0.##} هزار تومان";
        return $"{toman:0} تومان";
    }
}