using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpGalexy.LibraryExtention.Extentions.Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System;
using System.Runtime.CompilerServices;

/// <summary>
/// مجموعه متدهای گسترشی برای نوع bool.
/// </summary>
public static class BoolExtensions
{
    // ----------------------------------------------------------------------
    // ## عملیات‌های پایه و معکوس (Basic & Inverse Operations)
    // ----------------------------------------------------------------------

    /// <summary>
    /// 7. نگation تکی. (not)
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Not(this bool value) => !value;

    /// <summary>
    /// 16. جابجایی true↔false. (toggle)
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Toggle(this bool value) => !value;

    /// <summary>
    /// 30. همان not با نام آشناتر. (negate)
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Negate(this bool value) => !value;

    // ----------------------------------------------------------------------
    // ## عملیات‌های مقایسه دو boolean
    // ----------------------------------------------------------------------

    /// <summary>
    /// 3. XOR دوتایی. (xor)
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Xor(this bool a, bool b) => a ^ b;

    /// <summary>
    /// 4. نتیجه معکوس AND. (nand)
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Nand(this bool a, bool b) => !(a && b);

    /// <summary>
    /// 5. نتیجه معکوس OR. (nor)
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Nor(this bool a, bool b) => !(a || b);

    /// <summary>
    /// 6. نتیجه معکوس XOR. (xnor)
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Xnor(this bool a, bool b) => a == b;

    /// <summary>
    /// 8. A → B (implication). (implies)
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Implies(this bool a, bool b) => !a || b; // معادل !A OR B

    /// <summary>
    /// 9. برابری منطقی A ↔ B. (iff)
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Iff(this bool a, bool b) => a == b;

    /// <summary>
    /// 28. مقایسه دو boolean (redundant but for readability). (equals)
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Equals(this bool a, bool b) => a == b;

    /// <summary>
    /// 29. مقایسه با خروجی -1,0,1 (false < true). (compare)
    /// </summary>
    public static int Compare(this bool a, bool b)
    {
        // false به 0 و true به 1 تبدیل می شود
        return a.ToInt().CompareTo(b.ToInt());
    }

    // ----------------------------------------------------------------------
    // ## عملیات‌های بررسی (Checking Operations)
    // ----------------------------------------------------------------------

    /// <summary>
    /// 26. بررسی == true (خوانایی بالاتر). (isTrue)
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsTrue(this bool value) => value == true;

    /// <summary>
    /// 27. بررسی == false. (isFalse)
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsFalse(this bool value) => value == false;

    // ----------------------------------------------------------------------
    // ## عملیات‌های تبدیل (Conversion Operations)
    // ----------------------------------------------------------------------

    /// <summary>
    /// 17. تبدیل true→1، false→0. (toInt)
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int ToInt(this bool value) => value ? 1 : 0;

    /// <summary>
    /// 19. خروجی "Y"/"N". (toStringYN)
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string ToStringYN(this bool value) => value ? "Y" : "N";

    /// <summary>
    /// 20. خروجی "1"/"0". (toString10)
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string ToString10(this bool value) => value ? "1" : "0";

    /// <summary>
    /// 21. خروجی "ON"/"OFF". (toStringOnOff)
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string ToStringOnOff(this bool value) => value ? "ON" : "OFF";

    /// <summary>
    /// 22. خروجی "Yes"/"No". (toStringYesNo)
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string ToStringYesNo(this bool value) => value ? "Yes" : "No";
}
/// <summary>
/// کلاسی کمکی برای عملیات‌های منطقی (Boolean) روی مجموعه‌ها، تبدیل و تولید تصادفی.
/// </summary>
public static class BoolHelper
{
    private static readonly Random Rng = new Random();

    // ----------------------------------------------------------------------
    // ## عملیات‌های مجموعه (Collection Operations)
    // ----------------------------------------------------------------------

    /// <summary>
    /// 1. AND منطقی چند boolean. (and)
    /// </summary>
    public static bool And(params bool[] values)
    {
        if (values == null || values.Length == 0) return true; // AND روی مجموعه خالی معمولاً true است (Identity)
        return values.All(v => v);
    }

    /// <summary>
    /// 2. OR منطقی چند boolean. (or)
    /// </summary>
    public static bool Or(params bool[] values)
    {
        if (values == null || values.Length == 0) return false; // OR روی مجموعه خالی معمولاً false است (Identity)
        return values.Any(v => v);
    }

    /// <summary>
    /// 10. آیا تمام عناصر آرایه true است؟ (allTrue)
    /// </summary>
    public static bool AllTrue(IEnumerable<bool> values)
    {
        if (values == null) return false;
        return values.All(v => v);
    }

    /// <summary>
    /// 11. آیا تمام عناصر false است؟ (allFalse)
    /// </summary>
    public static bool AllFalse(IEnumerable<bool> values)
    {
        if (values == null) return false;
        return values.All(v => !v);
    }

    /// <summary>
    /// 12. حداقل یک true وجود دارد؟ (anyTrue)
    /// </summary>
    public static bool AnyTrue(IEnumerable<bool> values)
    {
        if (values == null) return false;
        return values.Any(v => v);
    }

    /// <summary>
    /// 13. حداقل یک false وجود دارد؟ (anyFalse)
    /// </summary>
    public static bool AnyFalse(IEnumerable<bool> values)
    {
        if (values == null) return false;
        return values.Any(v => !v);
    }

    /// <summary>
    /// 14. تعداد true در مجموعه. (countTrue)
    /// </summary>
    public static int CountTrue(IEnumerable<bool> values)
    {
        if (values == null) return 0;
        return values.Count(v => v);
    }

    /// <summary>
    /// 15. تعداد false در مجموعه. (countFalse)
    /// </summary>
    public static int CountFalse(IEnumerable<bool> values)
    {
        if (values == null) return 0;
        return values.Count(v => !v);
    }

    // ----------------------------------------------------------------------
    // ## عملیات‌های پارس و تولید تصادفی (Parsing & Random Generation)
    // ----------------------------------------------------------------------

    /// <summary>
    /// 18. تبدیل ۰→false، غیر۰→true. (fromInt)
    /// </summary>
    public static bool FromInt(int value) => value != 0;

    /// <summary>
    /// 23. پارس رشته‌های 1/0/true/false/yes/no بدون حساس به حروف. (parseLenient)
    /// </summary>
    public static bool ParseLenient(string s)
    {
        if (string.IsNullOrWhiteSpace(s)) return false;

        string lower = s.Trim().ToLowerInvariant();

        return lower == "true" || lower == "1" || lower == "yes" || lower == "y" || lower == "on";
    }

    /// <summary>
    /// 24. boolean تصادفی. (random)
    /// </summary>
    public static bool Random() => Rng.Next(2) == 1;

    /// <summary>
    /// 25. true با احتمال داده‌شده ۰…۱. (randomWithProbability)
    /// </summary>
    public static bool RandomWithProbability(double probability)
    {
        if (probability <= 0.0) return false;
        if (probability >= 1.0) return true;

        return Rng.NextDouble() < probability;
    }
}