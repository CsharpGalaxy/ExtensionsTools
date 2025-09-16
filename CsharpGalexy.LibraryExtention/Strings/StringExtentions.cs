
using CsharpGalexy.LibraryExtention.Models;
using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

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

        /// <summary>
        /// Checks if string is numeric (integer or decimal)
        /// </summary>
        public static bool IsNumeric(this string value)
        {
        
            return decimal.TryParse(value, out _);
        }

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

        #endregion
    }

