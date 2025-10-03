
using CsharpGalexy.LibraryExtention.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using NewtonsoftSerializer = Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;

namespace ExtentionLibrary.Strings;
/// <summary>
/// Powerful extension methods for string
/// </summary>
public static class StringExtensions
{
    private static readonly Dictionary<string, string> PersianToEnglish = new()
    {
        {"ض", "q"}, {"ص", "w"}, {"ث", "e"}, {"ق", "r"}, {"ف", "t"}, {"غ", "y"}, {"ع", "u"},
        {"ه", "i"}, {"خ", "o"}, {"ح", "p"}, {"ج", "["}, {"چ", "]"}, {"ش", "a"}, {"س", "s"},
        {"ی", "y"}, {"ب", "f"}, {"ل", "g"}, {"ا", "h"}, {"ت", "j"}, {"ن", "k"}, {"م", "l"},
        {"ک", ";"}, {"گ", "'"}, {"ظ", "z"}, {"ط", "x"}, {"ز", "c"}, {"ر", "v"}, {"ذ", "b"},
        {"د", "n"}, {"پ", "m"}, {"و", ","}, {"َ", "Q"}, {"ُ", "W"}, {"ِ", "E"}, {"ّ", "R"},
        {"ْ", "T"}, {"‌", " "}, {"ئ", "O"}, {"آ", "o"}, {"ء", "P"},
        {" ", " "}, {"-", "-"}, {".", "."}, {",", ","}, {"؟", "?"}
    };
    private static readonly Dictionary<string, string> EnglishToPersian =
        PersianToEnglish
            .GroupBy(kv => kv.Value)
            .ToDictionary(g => g.Key, g => g.First().Key);


    #region Null/Empty/Whitespace Checks

    /// <summary>
    /// Checks if string is null, empty, or whitespace
    /// </summary>
    public static bool IsEmpty(this string value)
    {
        return string.IsNullOrWhiteSpace(value);
    }

    /// <summary>
    /// Checks if string has value (not null, empty, or whitespace)
    /// </summary>
    public static bool HasValue(this string value)
    {
        return !string.IsNullOrWhiteSpace(value);
    }

    /// <summary>
    /// Returns default value if string is empty
    /// </summary>
    public static string OrDefault(this string value, string defaultValue)
    {
        return value.HasValue() ? value : defaultValue;
    }

    /// <summary>
    /// Returns empty string if null
    /// </summary>
    public static string OrEmpty(this string value)
    {
        return value ?? string.Empty;
    }

    #endregion

    #region Cleaning & Formatting
    /// <summary>
    /// Truncates the string to the specified maximum number of characters and appends "..." if truncated.
    /// </summary>
    /// <param name="text">The input string.</param>
    /// <param name="maxLength">Maximum number of characters.</param>
    /// <returns>The truncated string with "..." if it exceeds <paramref name="maxLength"/>.</returns>
    public static string TruncateMore(this string text, int maxLength)
    {
        if (string.IsNullOrEmpty(text) || maxLength <= 0)
            return string.Empty;

        if (text.Length <= maxLength)
            return text;

        return text.Substring(0, maxLength) + "...";
    }

    /// <summary>
    /// Truncates the string to the specified maximum number of words and appends "..." if truncated.
    /// </summary>
    /// <param name="text">The input string.</param>
    /// <param name="maxWords">Maximum number of words.</param>
    /// <returns>The truncated string with "..." if it exceeds <paramref name="maxWords"/> words.</returns>
    public static string TruncateWords(this string text, int maxWords)
    {
        if (string.IsNullOrEmpty(text) || maxWords <= 0)
            return string.Empty;

        var words = text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        if (words.Length <= maxWords)
            return text;

        return string.Join(" ", words.Take(maxWords)) + "...";
    }
    /// <summary>
    /// Removes all whitespace (including newlines, tabs)
    /// </summary>
    public static string RemoveWhitespace(this string value)
    {
        return value?.Replace(" ", "").Replace("\t", "").Replace("\n", "").Replace("\r", "");
    }

    /// <summary>
    /// Removes extra spaces and trims
    /// </summary>
    public static string CleanSpaces(this string value)
    {
        if (value.IsEmpty()) return value;
        return Regex.Replace(value, @"\s+", " ").Trim();
    }

    /// <summary>
    /// Removes diacritics (e.g. à, ñ) and converts to ASCII
    /// </summary>
    public static string RemoveDiacritics(this string value)
    {
        if (value.IsEmpty()) return value;

        var normalized = value.Normalize(NormalizationForm.FormD);
        var sb = new StringBuilder();

        foreach (var c in normalized)
        {
            var uc = CharUnicodeInfo.GetUnicodeCategory(c);
            if (uc != UnicodeCategory.NonSpacingMark)
                sb.Append(c);
        }

        return sb.ToString().Normalize(NormalizationForm.FormC);
    }

    #endregion

    #region Persian / Finglish / Numbers

    /// <summary>
    /// Converts Persian/Arabic numbers (۰-۹, ٠-٩) to English (0-9)
    /// </summary>
    public static string ToEnglishNumbers(this string value)
    {
        if (value.IsEmpty()) return value;

        var sb = new StringBuilder();
        foreach (char c in value)
        {
            if (c >= '۰' && c <= '۹') // Persian
                sb.Append((char)(c - '۰' + '0'));
            else if (c >= '٠' && c <= '٩') // Arabic
                sb.Append((char)(c - '٠' + '0'));
            else
                sb.Append(c);
        }
        return sb.ToString();
    }

    /// <summary>
    /// Converts English numbers (0-9) to Persian (۰-۹)
    /// </summary>
    public static string ToPersianNumbers(this string value)
    {
        if (value.IsEmpty()) return value;

        return value.Replace("0", "۰").Replace("1", "۱").Replace("2", "۲")
                    .Replace("3", "۳").Replace("4", "۴").Replace("5", "۵")
                    .Replace("6", "۶").Replace("7", "۷").Replace("8", "۸")
                    .Replace("9", "۹");
    }

    public static string ConvertLayout(this string value, KeyboardLayoutDirection direction)
    {
        if (string.IsNullOrEmpty(value)) return value;

        var map = direction == KeyboardLayoutDirection.PersianToEnglish
            ? PersianToEnglish
            : EnglishToPersian;

        var result = new StringBuilder();
        foreach (char c in value)
        {
            var key = c.ToString();
            result.Append(map.ContainsKey(key) ? map[key] : c.ToString());
        }

        return direction == KeyboardLayoutDirection.PersianToEnglish
            ? result.ToString().ToLower()
            : result.ToString();
    }
    #endregion

    #region Validation

    /// <summary>
    /// Checks if string is a valid email address
    /// </summary>
    public static bool IsEmail(this string value)
    {
        if (value.IsEmpty()) return false;

        try
        {
            var addr = new System.Net.Mail.MailAddress(value);
            return addr.Address == value;
        }
        catch
        {
            return false;
        }
    }
    /// <summary>
    /// Checks if the string is a valid URL.
    /// </summary>
    /// <param name="url">The string to validate.</param>
    /// <returns>True if valid URL, otherwise false.</returns>
    public static bool IsValidUrl(this string url)
    {
        if (string.IsNullOrWhiteSpace(url))
            return false;

        return Uri.TryCreate(url, UriKind.Absolute, out Uri uriResult)
               && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
    }
    /// <summary>
    /// Checks if string is a valid Iranian mobile number (09123456789)
    /// </summary>
    public static bool IsIranianMobile(this string value)
    {
        if (value.IsEmpty()) return false;

        var cleaned = value.ToEnglishNumbers().Replace("+98", "0").Replace(" ", "").Replace("-", "");
        return Regex.IsMatch(cleaned, @"^09\d{9}$");
    }

    /// <summary>
    /// Checks if string is a valid Iranian national code (کد ملی)
    /// </summary>
    public static bool IsNationalCode(this string value)
    {
        if (value.IsEmpty() || value.Length != 10 || !value.All(char.IsDigit)) return false;

        var check = int.Parse(value[9].ToString());
        var sum = 0;
        for (int i = 0; i < 9; i++)
            sum += int.Parse(value[i].ToString()) * (10 - i);

        var remainder = sum % 11;
        return (remainder < 2 && check == remainder) || (remainder >= 2 && check == 11 - remainder);
    }

    ///// <summary>
    ///// Checks if string is numeric (integer or decimal)
    ///// </summary>
    //public static bool IsNumeric(this string value)
    //{

    //    return decimal.TryParse(value, out _);
    //}

    #endregion

    #region Text Processing

    /// <summary>
    /// Truncates string to specified length and adds ellipsis if needed
    /// </summary>
    public static string Truncate(this string value, int maxLength, string suffix = "...")
    {
        if (value.IsEmpty() || value.Length <= maxLength) return value;

        var trimmed = value.Substring(0, maxLength);
        return trimmed + suffix;
    }

    /// <summary>
    /// Converts string to URL-friendly slug (e.g. "Hello World" -> "hello-world")
    /// </summary>
    public static string ToSlug(this string value)
    {
        if (value.IsEmpty()) return value;

        value = value.RemoveDiacritics()
                     .ToLowerInvariant()
                     .CleanSpaces()
                     .Replace(" ", "-")
                     .Replace("--", "-");

        // Remove invalid characters
        value = Regex.Replace(value, @"[^a-z0-9\-]", "");

        return value.Trim('-');
    }

    /// <summary>
    /// Counts words in string
    /// </summary>
    public static int WordCount(this string value)
    {
        if (value.IsEmpty()) return 0;
        return Regex.Matches(value.Trim(), @"\S+").Count;
    }

    /// <summary>
    /// Repeats a string n times
    /// </summary>
    public static string Repeat(this string value, int count)
    {
        if (value.IsEmpty() || count <= 0) return string.Empty;
        return string.Concat(Enumerable.Repeat(value, count));
    }
    /// <summary>
    /// GetCharacterCount O string
    /// </summary>
    public static int GetCharacterCount(string text)
    {
        if (string.IsNullOrEmpty(text))
            return 0;

        return text.Length;
    }
    #endregion

    #region Security & Sanitization

    /// <summary>
    /// Sanitizes HTML input (basic protection)
    /// Note: For production, use libraries like HtmlSanitizer
    /// </summary>
    public static string SanitizeHtml(this string value)
    {
        if (value.IsEmpty()) return value;
        return Regex.Replace(value, @"<script[^>]*>[\s\S]*?</script>", "", RegexOptions.IgnoreCase)
                    .Replace("<", "<")
                    .Replace(">", ">");
    }

    /// <summary>
    /// Masks sensitive data (e.g. phone, ID)
    /// Example: "09123456789" -> "0912******789"
    /// </summary>
    public static string Mask(this string value, int unmaskedStart = 4, int unmaskedEnd = 3)
    {
        if (value.IsEmpty() || value.Length <= unmaskedStart + unmaskedEnd)
            return value?.MaskAll();

        var start = value.Substring(0, unmaskedStart);
        var end = value.Substring(value.Length - unmaskedEnd);
        var masked = "*".Repeat(value.Length - unmaskedStart - unmaskedEnd);
        return start + masked + end;
    }

    /// <summary>
    /// Masks entire string
    /// </summary>
    public static string MaskAll(this string value)
    {
        return value?.Any() == true ? "*".Repeat(value.Length) : value;
    }

    #endregion

    #region Case & Capitalization

    /// <summary>
    /// Converts first letter to uppercase
    /// </summary>
    public static string FirstCharToUpper(this string value)
    {
        if (value.IsEmpty()) return value;
        return char.ToUpper(value[0]) + value.Substring(1).ToLower();
    }

    /// <summary>
    /// Converts to title case (each word capitalized)
    /// </summary>
    public static string ToTitleCase(this string value)
    {
        if (value.IsEmpty()) return value;
        var culture = new CultureInfo("fa-IR");
        return culture.TextInfo.ToTitleCase(value.ToLower());
    }
    public static string ValidateOrGenerateHex24(this string input)
    {
        if (string.IsNullOrWhiteSpace(input) || input.Length != 24 ||
            !Regex.IsMatch(input, "^[a-fA-F0-9]{24}$"))
        {
            // تولید رشته‌ی رندوم هگزادسیمال
            var random = new Random();
            var sb = new StringBuilder();
            const string hexChars = "abcdef0123456789";

            for (int i = 0; i < 24; i++)
            {
                sb.Append(hexChars[random.Next(hexChars.Length)]);
            }

            return sb.ToString(); // خروجی جایگزین معتبر
        }

        return input; // اگر معتبر بود، همون ورودی رو برمی‌گردونه
    }
    public static string CalculateReadTime(this string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return "0:00";

        int textLength = text.Length;
        double rawMinutes = (double)textLength / 200;

        int minutes = (int)rawMinutes;
        int seconds = (int)Math.Round((rawMinutes - minutes) * 60);

        return $"{minutes}:{seconds:D2}";
    }
    #endregion




    #region 🧠 JSON Serialization Settings (Newtonsoft)

    /// <summary>
    /// تنظیمات پیش‌فرض برای سریالایز کردن JSON با Newtonsoft.Json
    /// - نادیده گرفتن حلقه‌های مرجع (ReferenceLoopHandling.Ignore)
    /// - Escape کردن کاراکترهای HTML در رشته‌ها
    /// - تبدیل نام پراپرتی‌ها به camelCase (مثلاً FirstName → firstName)
    /// </summary>
    private static readonly NewtonsoftSerializer.JsonSerializerSettings JsonSerializerSettings = new NewtonsoftSerializer.JsonSerializerSettings
    {
        ReferenceLoopHandling = NewtonsoftSerializer.ReferenceLoopHandling.Ignore,
        StringEscapeHandling = NewtonsoftSerializer.StringEscapeHandling.EscapeHtml,
        ContractResolver = new CamelCasePropertyNamesContractResolver()
    };

    #endregion

    #region 🔢 توابع عددی و اعتبارسنجی عددی

    /// <summary>
    /// بررسی می‌کند که آیا رشته فقط شامل ارقام است یا خیر.
    /// مثال: "123".IsNumeric() → true | "abc123".IsNumeric() → false
    /// </summary>
    public static bool IsNumeric(this string str) => str.All(char.IsDigit);

    /// <summary>
    /// تبدیل رشته عددی به هگزادسیمال (با طول ثابت 8 کاراکتر).
    /// اگر رشته عددی نباشد، null برمی‌گرداند.
    /// مثال: "255".ToHex() → "000000FF"
    /// </summary>
    public static string ToHex(this string str) => !str.IsNumeric() ? null : $"{int.Parse(str):X8}";

    /// <summary>
    /// تبدیل رشته به عدد اعشاری با تعداد اعشار مشخص.
    /// مثال: "123.456789".ToDecimal(2) → 123.46
    /// </summary>
    public static decimal ToDecimal(this string str, int decimales) => decimal.Round(Convert.ToDecimal(str), decimales);

    /// <summary>
    /// تبدیل رشته به double. در صورت شکست، مقدار پیش‌فرض برگردانده می‌شود.
    /// </summary>
    public static double ToDouble(this string input, double defaultValue = default) => double.TryParse(input, out var value) ? value : defaultValue;

    /// <summary>
    /// تبدیل رشته به long. در صورت شکست، مقدار پیش‌فرض برگردانده می‌شود.
    /// </summary>
    public static long ToLong(this string input, long defaultValue = default) => long.TryParse(input, out var value) ? value : defaultValue;

    /// <summary>
    /// تبدیل رشته به عدد صحیح. در صورت شکست، مقدار پیش‌فرض برگردانده می‌شود.
    /// </summary>
    public static int ToInt(this string input, int defaultValue = default) => int.TryParse(input, out var value) ? value : defaultValue;

    /// <summary>
    /// استخراج فقط ارقام از رشته و تبدیل به int.
    /// مثال: "abc123xyz".CleanAsInt(0) → 123
    /// </summary>
    public static int CleanAsInt(this string input, int defaultValue)
    {
        var strNumber = string.Concat(input.Where(char.IsDigit));
        return strNumber.ToInt(0);
    }

    /// <summary>
    /// استخراج ارقام و کاما (برای اعداد اعشاری) و تبدیل به decimal.
    /// مثال: "Price: 1,234.56 USD".CleanAsDecimal(2) → 1234.56
    /// </summary>
    public static decimal CleanAsDecimal(this string input, int decimales)
    {
        var strNumber = string.Concat(input.Where(x => char.IsDigit(x) || x == ','));
        return strNumber.ToDecimal(decimales);
    }

    /// <summary>
    /// استخراج فقط حروف الفبا از رشته.
    /// مثال: "abc123!@#".CleanAsString() → "abc"
    /// </summary>
    public static string CleanAsString(this string input) => string.Concat(input.Where(char.IsLetter));

    /// <summary>
    /// استخراج عدد از رشته (تا اولین کاراکتر غیرعددی یا نقطه غیرمجاز).
    /// مثال: "123.45abc".Val() → 123.45 | "abc123".Val() → 0
    /// </summary>
    public static double Val(this string value)
    {
        var result = string.Empty;
        foreach (char c in value)
        {
            if (char.IsNumber(c) || c.Equals('.') && result.Count(x => x.Equals('.')) == 0)
            {
                result += c;
            }
            else if (!c.Equals(' '))
            {
                return string.IsNullOrEmpty(result) ? 0 : Convert.ToDouble(result);
            }
        }
        return string.IsNullOrEmpty(result) ? 0 : Convert.ToDouble(result);
    }

    #endregion

    #region 📏 توابع مربوط به زیررشته (Substring)

    /// <summary>
    /// گرفتن n کاراکتر از سمت چپ رشته.
    /// مثال: "Hello World".Left(5) → "Hello"
    /// </summary>
    public static string Left(this string str, int len) => str.Substring(0, Math.Min(len, str.Length));

    /// <summary>
    /// گرفتن n کاراکتر از سمت راست رشته.
    /// مثال: "Hello World".Right(5) → "World"
    /// </summary>
    public static string Right(this string str, int len) => str.Substring(str.Length - Math.Min(len, str.Length));

    /// <summary>
    /// حذف n کاراکتر از انتهای رشته.
    /// مثال: "Hello World".Truncate(3) → "Hello Wo"
    /// </summary>
    public static string Truncate(this string str, int len) => str.Length > len ? str.Substring(0, str.Length - len) : str;

    /// <summary>
    /// حذف n کاراکتر از انتهای رشته (همانند Truncate).
    /// </summary>
    public static string TruncFromRight(this string str, int len) => str.Substring(0, str.Length - len);

    /// <summary>
    /// حذف n کاراکتر از ابتدای رشته.
    /// مثال: "Hello World".TruncFromLeft(6) → "World"
    /// </summary>
    public static string TruncFromLeft(this string str, int len) => str.Substring(len, str.Length - len);

    /// <summary>
    /// کپی کردن زیررشته از اندیس start به طول len.
    /// مثال: "Hello World".CopyUntil(6, 5) → "World"
    /// </summary>
    public static string CopyUntil(this string str, int start, int len) => str.Substring(start, Math.Min(len, str.Length - start));

    /// <summary>
    /// کپی کردن زیررشته از اندیس start تا قبل از اولین occurrence کاراکتر داده شده.
    /// ⚠️ اگر کاراکتر پیدا نشود، خطای IndexOutOfRangeException می‌دهد.
    /// </summary>
    public static string CopyUntilChar(this string str, int startindex, char caracter) => str.Substring(startindex, str.IndexOf(caracter) - startindex);

    /// <summary>
    /// کپی کردن زیررشته از بعد از اولین occurrence کاراکتر داده شده تا انتها.
    /// مثال: "key=value".CopyFromChar('=') → "value"
    /// </summary>
    public static string CopyFromChar(this string str, char caracter)
    {
        var from = str.IndexOf(caracter) + 1;
        return from <= str.Length ? str.Substring(from) : string.Empty;
    }

    /// <summary>
    /// جایگزینی کاراکتر در موقعیت مشخص.
    /// مثال: "Hello".ReplaceAt(1, 'a') → "Hallo"
    /// </summary>
    public static string ReplaceAt(this string input, int index, char newChar)
    {
        if (input == null) throw new ArgumentNullException(nameof(input));
        if (index < 0 || index >= input.Length) throw new ArgumentOutOfRangeException(nameof(index));
        char[] chars = input.ToCharArray();
        chars[index] = newChar;
        return new string(chars);
    }

    #endregion

    #region 🔄 توابع تبدیل نوع و پارس کردن

    /// <summary>
    /// تبدیل رشته به مقدار بولین.
    /// در صورت خطا، false برگردانده می‌شود.
    /// </summary>
    public static bool ToBoolean(this string str)
    {
        try
        {
            return Convert.ToBoolean(str);
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// بررسی می‌کند که آیا رشته "false" یا "0" نیست (معادل true منطقی).
    /// مثال: "true".IsTrue() → true | "0".IsTrue() → false
    /// </summary>
    public static bool IsTrue(this string input) => !string.Equals(input, "false", StringComparison.OrdinalIgnoreCase) && input != "0";

    /// <summary>
    /// تبدیل شیء به آرایه بایت UTF-8 با استفاده از System.Text.Json.
    /// در صورت خطا، null برمی‌گرداند.
    /// </summary>
    public static byte[] ToUtf8Bytes(this object value, JsonSerializerOptions options = null)
    {
        if (value == null) return null;
        try
        {
            return JsonSerializer.SerializeToUtf8Bytes(value, options);
        }
        catch
        {
            return null;
        }
    }

    #endregion

    #region 📦 JSON Serialization/Deserialization (System.Text.Json)

    /// <summary>
    /// تبدیل شیء به رشته JSON با استفاده از System.Text.Json.
    /// در صورت خطا، null برمی‌گرداند.
    /// </summary>
    public static string ToJson(this object value, JsonSerializerOptions options = null)
    {
        if (value == null) return null;
        try
        {
            return JsonSerializer.Serialize(value, options ?? new JsonSerializerOptions());
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// تبدیل رشته JSON به شیء از نوع T با استفاده از System.Text.Json.
    /// در صورت خطا، مقدار پیش‌فرض T برگردانده می‌شود.
    /// </summary>
    public static T ParseTo<T>(this string str, JsonSerializerOptions options = null)
    {
        if (string.IsNullOrEmpty(str)) return default(T);
        try
        {
            return JsonSerializer.Deserialize<T>(str, options);
        }
        catch
        {
            return default(T);
        }
    }

    #endregion

    #region 📦 JSON Serialization/Deserialization (Newtonsoft.Json)

    /// <summary>
    /// تبدیل شیء به رشته JSON با استفاده از Newtonsoft.Json و تنظیمات پیش‌فرض.
    /// در صورت خطا، null برمی‌گرداند.
    /// </summary>
    public static string ToJson(this object value)
    {
        if (value == null) return null;
        try
        {
            return NewtonsoftSerializer.JsonConvert.SerializeObject(value);
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// تبدیل شیء به رشته JSON با استفاده از Newtonsoft.Json و تنظیمات دلخواه.
    /// اگر تنظیمات داده نشود، از تنظیمات پیش‌فرض کلاس استفاده می‌کند.
    /// </summary>
    public static string ToJson(this object value, NewtonsoftSerializer.JsonSerializerSettings settings)
    {
        if (value == null) return null;
        var opSettings = settings ?? JsonSerializerSettings;
        try
        {
            return NewtonsoftSerializer.JsonConvert.SerializeObject(value, opSettings);
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// تبدیل رشته JSON به شیء از نوع T با استفاده از Newtonsoft.Json.
    /// در صورت خطا، مقدار پیش‌فرض T برگردانده می‌شود و خطا در کنسول چاپ می‌شود.
    /// </summary>
    public static T ParseTo<T>(this string str)
    {
        if (string.IsNullOrEmpty(str)) return default(T);
        try
        {
            return NewtonsoftSerializer.JsonConvert.DeserializeObject<T>(str);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ParseTo Error: {ex.Message}");
            return default(T);
        }
    }

    /// <summary>
    /// اعتبارسنجی ساختار JSON (آیا رشته یک JSON معتبر است؟)
    /// با استفاده از Newtonsoft.Json.JToken.Parse
    /// </summary>
    public static bool IsValidJson(this string text)
    {
        if (string.IsNullOrWhiteSpace(text)) return false;
        text = text.Trim();
        if ((text.StartsWith("{") && text.EndsWith("}")) || // For object
            (text.StartsWith("[") && text.EndsWith("]")))   // For array
        {
            try
            {
                var obj = NewtonsoftSerializer.Linq.JToken.Parse(text);
                return true;
            }
            catch (NewtonsoftSerializer.JsonReaderException) { return false; }
            catch { return false; }
        }
        return false;
    }

    #endregion

    #region String && Array

    /// <summary>
    /// جستجوی باینری در ArrayList (فقط برای لیست‌های مرتب شده!)
    /// ⚠️ اگر لیست null باشد یا المان پیدا نشود، false برمی‌گرداند.
    /// </summary>
    public static bool ArraySearch(this ArrayList lista, string value)
    {
        if (lista == null) return false;
        var index = lista.BinarySearch(value);
        return index >= 0;
    }

    /// <summary>
    /// احاطه کردن مقدار با علامت نقل قول تکی.
    /// مثال: "Hello".Apostrophe() → "'Hello'"
    /// </summary>
    public static string Apostrophe<T>(this T data) => $"'{data}'";

    /// <summary>
    /// بررسی می‌کند که آیا مقدار x در بین مقادیر args وجود دارد یا خیر.
    /// مثال: "net".In("dot", "net", "languages") → true
    /// </summary>
    public static bool In<T>(this T x, params T[] args) => args.Contains(x);

    /// <summary>
    /// تولید رشته‌ای شامل n فاصله (Space).
    /// مثال: Space(4) → "    "
    /// </summary>
    public static string Space(int count) => new string(' ', count);

    /// <summary>
    /// تبدیل رشته به Base64 (با کدگذاری UTF-8).
    /// </summary>
    public static string EncodeToBase64(this string plainText) => Convert.ToBase64String(Encoding.UTF8.GetBytes(plainText));

    /// <summary>
    /// تبدیل رشته Base64 به رشته اصلی (با کدگذاری UTF-8).
    /// </summary>
    public static string DecodeFromBase64(this string base64EncodedData) => Encoding.UTF8.GetString(Convert.FromBase64String(base64EncodedData));

    /// <summary>
    /// تبدیل آرایه بایت به رشته Base64.
    /// </summary>
    public static string ToBase64String(this byte[] bytes) => Convert.ToBase64String(bytes);

    /// <summary>
    /// تبدیل رشته به آرایه بایت با کدگذاری UTF-8.
    /// </summary>
    public static byte[] GetBytes(this string str) => Encoding.UTF8.GetBytes(str);

    /// <summary>
    /// شمارش تعداد کلمات در رشته (کلمه = دنباله‌ای از کاراکترهای غیرفضا).
    /// </summary>
    public static int WordsCount(this string input) => Regex.Matches(input ?? "", @"[^\s]+").Count;

    /// <summary>
    /// جایگزینی رشته با استفاده از Regex و با Optionهای خاص (مثلاً IgnoreCase).
    /// </summary>
    public static string Replace(this string input, string word, string with, RegexOptions caseOption) => Regex.Replace(input ?? "", word, with, caseOption);

    /// <summary>
    /// تلاش برای تبدیل رشته به int. در صورت شکست، مقدار پیش‌فرض برگردانده می‌شود.
    /// </summary>
    public static int TryParse(this string input, int defaultValue) => int.TryParse(input, out var value) ? value : defaultValue;

    #endregion

    /// <summary>
    /// تبدیل تاریخ شمسی به میلادی
    /// جداکننده می‌تواند / یا - باشد
    /// </summary>
    /// <param name="shamsiDate">تاریخ شمسی به صورت رشته (مثال: "1404/7/3" یا "1404-07-03")</param>
    /// <returns>DateTime میلادی</returns>
    public static DateTime ToGregorianDate(this string shamsiDate)
    {
        if (string.IsNullOrWhiteSpace(shamsiDate))
            throw new ArgumentNullException(nameof(shamsiDate));

        // قبول هر دو جداکننده / و -
        var parts = shamsiDate.Split(new[] { '/', '-' }, StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length != 3)
            throw new FormatException("فرمت تاریخ شمسی باید yyyy/MM/dd یا yyyy-MM-dd باشد.");

        // parse سال، ماه، روز
        int year = int.Parse(parts[0]);
        int month = int.Parse(parts[1]);
        int day = int.Parse(parts[2]);

        var persianCalendar = new PersianCalendar();
        return persianCalendar.ToDateTime(year, month, day, 0, 0, 0, 0);
    }

    /// <summary>
    /// تبدیل تاریخ شمسی به میلادی با ساعت و دقیقه
    /// </summary>
    public static DateTime ToGregorianDate(this string shamsiDate, int hour, int minute, int second = 0)
    {
        if (string.IsNullOrWhiteSpace(shamsiDate))
            throw new ArgumentNullException(nameof(shamsiDate));

        var parts = shamsiDate.Split(new[] { '/', '-' }, StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length != 3)
            throw new FormatException("فرمت تاریخ شمسی باید yyyy/MM/dd یا yyyy-MM-dd باشد.");

        int year = int.Parse(parts[0]);
        int month = int.Parse(parts[1]);
        int day = int.Parse(parts[2]);

        var persianCalendar = new PersianCalendar();
        return persianCalendar.ToDateTime(year, month, day, hour, minute, second, 0);
    }
  
    /// <summary>/// <summary>
    /// Reverses the characters in the input string.
    /// </summary>
    /// <param name="source">The input string.</param>
    /// <returns>The reversed string. If null or empty, returns the original value.</returns>
    public static string Reverse(this string source) =>
        string.IsNullOrEmpty(source) ? source : new string(source.Reverse().ToArray());

    /// Checks whether the input string is a valid GUID.
    /// </summary>
    /// <param name="source">The input string.</param>
    /// <returns>True if the string is a valid GUID; otherwise, false.</returns>
    public static bool IsGuid(this string source) =>
        !string.IsNullOrWhiteSpace(source) && Guid.TryParse(source, out _);



    /// <summary>
    /// Trims whitespace from both the beginning and end of the string.
    /// </summary>
    /// <param name="source">The input string.</param>
    /// <returns>The trimmed string, or null if the input is null.</returns>
    public static string TrimAll(this string source) => source?.Trim();

    /// <summary>
    /// Trims whitespace from the beginning of the string.
    /// </summary>
    /// <param name="source">The input string.</param>
    /// <returns>The trimmed string, or null if the input is null.</returns>
    public static string TrimStartAll(this string source) => source?.TrimStart();

    /// <summary>
    /// Trims whitespace from the end of the string.
    /// </summary>
    /// <param name="source">The input string.</param>
    /// <returns>The trimmed string, or null if the input is null.</returns>
    public static string TrimEndAll(this string source) => source?.TrimEnd();

    /// <summary>
    /// Compares two strings for equality, ignoring case sensitivity.
    /// </summary>
    /// <param name="source">The first string.</param>
    /// <param name="other">The second string to compare.</param>
    /// <returns>True if the strings are equal ignoring case; otherwise, false.</returns>
    public static bool EqualsIgnoreCase(this string source, string other) =>
        string.Equals(source, other, StringComparison.OrdinalIgnoreCase);

    /// <summary>
    /// Compares two strings for equality using invariant culture, ignoring case sensitivity.
    /// </summary>
    /// <param name="source">The first string.</param>
    /// <param name="other">The second string to compare.</param>
    /// <returns>True if the strings are equal using invariant culture and ignoring case; otherwise, false.</returns>
    public static bool EqualsInvariant(this string source, string other) =>
        string.Equals(source, other, StringComparison.InvariantCultureIgnoreCase);


    /// <summary>
    /// Determines whether the string is null or empty.
    /// </summary>
    /// <param name="value">The input string.</param>
    /// <returns>True if the string is null or empty; otherwise, false.</returns>
    public static bool IsNullOrEmptyEx(this string value) => string.IsNullOrEmpty(value);

    /// <summary>
    /// Determines whether the string is null, empty, or consists only of white-space characters.
    /// </summary>
    /// <param name="value">The input string.</param>
    /// <returns>True if the string is null, empty, or whitespace; otherwise, false.</returns>
    public static bool IsNullOrWhiteSpaceEx(this string value) => string.IsNullOrWhiteSpace(value);

}

