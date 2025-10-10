using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;


namespace CsharpGalexy.LibraryExtention.Extentions.Struct;





public static class IntExtensions
{
    // ----------------------------------------------------------------------
    // ## عملیات‌های پایه و ریاضی (Basic & Mathematical Operations)
    // ----------------------------------------------------------------------

    /// <summary>
    /// 1. بازگرداندن قدر مطلق عدد صحیح. (abs)
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Abs(this int number) => Math.Abs(number);

    /// <summary>
    /// 13. محاسبه فاکتوریل برای اعداد کوچک (بازگشت long). (factorial)
    /// </summary>
    public static long Factorial(this int n)
    {
        if (n < 0) throw new ArgumentOutOfRangeException(nameof(n), "Factorial is not defined for negative numbers.");
        if (n > 20) throw new OverflowException("Result exceeds the maximum value of a long.");

        long result = 1;
        for (int i = 2; i <= n; i++)
        {
            result *= i;
        }
        return result;
    }

    /// <summary>
    /// 27. عملیات modulo همیشه مثبت. (mod)
    /// </summary>
    public static int Mod(this int a, int n)
    {
        int result = a % n;
        return (result < 0) ? result + n : result;
    }

    /// <summary>
    /// 40. علامت عدد: -1، 0 یا +1. (sign)
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Sign(this int number) => Math.Sign(number);

    /// <summary>
    /// 41. ریشه دوم عدد صحیح (بازگشت int). (sqrtInt)
    /// </summary>
    public static int SqrtInt(this int n)
    {
        if (n < 0) throw new ArgumentOutOfRangeException(nameof(n), "Square root is not defined for negative numbers.");
        return (int)Math.Floor(Math.Sqrt(n));
    }

    // ----------------------------------------------------------------------
    // ## عملیات‌های بررسی و محدودسازی (Checking & Clamping Operations)
    // ----------------------------------------------------------------------

    /// <summary>
    /// 4. بررسی قرارگیری عدد در بازه ]min,max[. (between)
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsBetween(this int number, int min, int max) => number > min && number < max;

    /// <summary>
    /// 9. بستن عدد درون یک بازه مشخص. (clamp)
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Clamp(this int number, int min, int max) => Math.Clamp(number, min, max);

    /// <summary>
    /// 11. چرخاندن عدد درون بازه مشخص (loop-back). (cycle)
    /// </summary>
    public static int Cycle(this int number, int min, int max)
    {
        int range = max - min + 1;
        if (range <= 0) throw new ArgumentException("Max must be greater than or equal to Min.");
        int result = (number - min) % range;
        return (result < 0) ? result + max : result + min;
    }

    /// <summary>
    /// 18. بررسی زوج بودن. (isEven)
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEven(this int number) => (number & 1) == 0;

    /// <summary>
    /// 19. بررسی فرد بودن. (isOdd)
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsOdd(this int number) => (number & 1) != 0;

    /// <summary>
    /// 20. بررسی توان دو بودن. (isPowerOfTwo)
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsPowerOfTwo(this int number) => (number > 0) && ((number & (number - 1)) == 0);

    /// <summary>
    /// 21. آزمایش اول بودن عدد. (isPrime)
    /// </summary>
    public static bool IsPrime(this int number)
    {
        if (number <= 1) return false;
        if (number <= 3) return true;
        if (number % 2 == 0 || number % 3 == 0) return false;

        for (int i = 5; i * i <= number; i = i + 6)
        {
            if (number % i == 0 || number % (i + 2) == 0) return false;
        }
        return true;
    }

    /// <summary>
    /// 49. همان clamp ولی با نام واضح‌تر. (truncateToRange)
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int TruncateToRange(this int number, int min, int max) => number.Clamp(min, max);

    /// <summary>
    /// 50. چرخش عدد درون بازه به صورت modulo (مانند cycle). (wrap)
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Wrap(this int number, int min, int max) => number.Cycle(min, max);

    // ----------------------------------------------------------------------
    // ## عملیات‌های بیت و باینری (Bitwise & Binary Operations)
    // ----------------------------------------------------------------------

    /// <summary>
    /// 5. شمارش تعداد بیت‌های 1 در فرم دوّمی. (bitCount)
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int BitCount(this int number) => BitOperations.PopCount((uint)number);

    /// <summary>
    /// 8. بالاترین توان 2 که ≥ عدد ورودی است. (ceilToPowerOfTwo)
    /// </summary>
    public static int CeilToPowerOfTwo(this int n)
    {
        if (n <= 0) return 1;
        return (int)BitOperations.RoundUpToPowerOf2((uint)n);
    }

    /// <summary>
    /// 15. فاصله هامینگ دو عدد صحیح. (hammingDistance)
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int HammingDistance(this int a, int b) => BitOperations.PopCount((uint)(a ^ b));

    /// <summary>
    /// 23. محاسبه لوگاریتم پایه 2 عدد صحیح. (log2)
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Log2(this int number)
    {
        if (number <= 0) throw new ArgumentOutOfRangeException(nameof(number), "Log2 is not defined for non-positive numbers.");
        return BitOperations.Log2((uint)number);
    }

    /// <summary>
    /// 24. محاسبه لگاریتم پایه 10 عدد صحیح. (log10)
    /// </summary>
    public static int Log10(this int number)
    {
        if (number <= 0) throw new ArgumentOutOfRangeException(nameof(number), "Log10 is not defined for non-positive numbers.");
        return (int)Math.Floor(Math.Log10(number));
    }

    ///// <summary>
    ///// 33. معکوس کردن ترتیب 32 بیت عدد. (reverseBits)
    ///// </summary>
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static int ReverseBits(this int number) => (int)BitOperations.ReverseByteOrder((uint)number);

    /// <summary>
    /// 35. چرخش بیت‌ها به چپ. (rotateLeft)
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int RotateLeft(this int value, int offset) => (int)BitOperations.RotateLeft((uint)value, offset);

    /// <summary>
    /// 36. چرخش بیت‌ها به راست. (rotateRight)
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int RotateRight(this int value, int offset) => (int)BitOperations.RotateRight((uint)value, offset);

    // ----------------------------------------------------------------------
    // ## عملیات‌های Byte و Endianness
    // ----------------------------------------------------------------------

    /// <summary>
    /// 16. تجزیه int به 4 بایت Big-Endian. (intToBytesBE)
    /// </summary>
    public static byte[] ToBytesBE(this int value)
    {
        byte[] bytes = BitConverter.GetBytes(value);
        if (BitConverter.IsLittleEndian)
        {
            Array.Reverse(bytes);
        }
        return bytes;
    }

    /// <summary>
    /// 17. تجزیه int به 4 بایت Little-Endian. (intToBytesLE)
    /// </summary>
    public static byte[] ToBytesLE(this int value)
    {
        byte[] bytes = BitConverter.GetBytes(value);
        if (!BitConverter.IsLittleEndian)
        {
            Array.Reverse(bytes);
        }
        return bytes;
    }

    // ----------------------------------------------------------------------
    // ## عملیات‌های تبدیل رشته‌ای و رقم (String & Digit Conversion)
    // ----------------------------------------------------------------------

    /// <summary>
    /// 34. معکوس کردن رقم‌های عدد (123 ← 321). (reverseDigits)
    /// </summary>
    public static int ReverseDigits(this int number)
    {
        int sign = Math.Sign(number);
        number = number.Abs();
        long reversed = 0;

        while (number > 0)
        {
            reversed = reversed * 10 + (number % 10);
            number /= 10;
        }

        if (reversed > int.MaxValue) throw new OverflowException("Reversed number exceeds the maximum value of an int.");

        return (int)reversed * sign;
    }

    /// <summary>
    /// 45. رشته باینری با طول ثابت 32 کاراکتر. (toBinaryStringPadded)
    /// </summary>
    public static string ToBinaryStringPadded(this int number)
    {
        return Convert.ToString(number, 2).PadLeft(32, '0');
    }

    /// <summary>
    /// 47. رشته هگز با طول ثابت 8 کاراکتر. (toHexStringPadded)
    /// </summary>
    public static string ToHexStringPadded(this int number)
    {
        return number.ToString("X8");
    }

    /// <summary>
    /// 48. رشته هشت‌تایی عدد. (toOctalString)
    /// </summary>
    public static string ToOctalString(this int number)
    {
        return Convert.ToString(number, 8);
    }
}
public static class IntHelper
{


    private static readonly Random Rng = new Random();

    // ----------------------------------------------------------------------
    // ## عملیات‌های دو عدد/آرایه (Two-Number/Array Operations)
    // ----------------------------------------------------------------------

    /// <summary>
    /// 2. جمع دو عدد با چسبندگی به بازه MIN…MAX (overflow ندارد). (addClamped)
    /// </summary>
    public static int AddClamped(int a, int b)
    {
        try { return checked(a + b); }
        catch (OverflowException)
        {
            return (a > 0 && b > 0) ? int.MaxValue : int.MinValue;
        }
    }

    /// <summary>
    /// 12. تقسیم به سمت بالا و بازگشت سقف کسر. (divideRoundUp)
    /// </summary>
    public static int DivideRoundUp(int dividend, int divisor)
    {
        if (divisor == 0) throw new DivideByZeroException();
        if (divisor < 0) return DivideRoundUp(-dividend, -divisor);

        return (dividend + divisor - 1) / divisor;
    }

    /// <summary>
    /// 14. بزرگ‌ترین مقسوم‌علیه مشترک دو عدد. (gcd)
    /// </summary>
    public static int Gcd(int a, int b)
    {
        a = Math.Abs(a);
        b = Math.Abs(b);
        while (b != 0)
        {
            int temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }

    /// <summary>
    /// 22. کوچک‌ترین مضرب مشترک دو عدد. (lcm)
    /// </summary>
    public static long Lcm(int a, int b)
    {
        if (a == 0 || b == 0) return 0;
        return Math.Abs((long)a * b) / Gcd(a, b);
    }

    /// <summary>
    /// 28. ضرب دو عدد با چسبندگی به بازه MIN…MAX. (multiplyClamped)
    /// </summary>
    public static int MultiplyClamped(int a, int b)
    {
        try { return checked(a * b); }
        catch (OverflowException)
        {
            return ((a > 0 && b > 0) || (a < 0 && b < 0)) ? int.MaxValue : int.MinValue;
        }
    }

    /// <summary>
    /// 32. توان عدد صحیح (بازگشت long). (pow)
    /// </summary>
    public static long Pow(int @base, int exponent)
    {
        if (exponent < 0) return 0;
        return (long)Math.Pow(@base, exponent);
    }

    /// <summary>
    /// 42. تفریق دو عدد با چسبندگی به بازه MIN…MAX. (subtractClamped)
    /// </summary>
    public static int SubtractClamped(int a, int b)
    {
        try { return checked(a - b); }
        catch (OverflowException)
        {
            return (b < 0) ? int.MaxValue : int.MinValue;
        }
    }

    /// <summary>
    /// 44. جایگزینی مقادیر دو متغیر int بدون متغیر سوم. (swap)
    /// </summary>
    public static void Swap(ref int a, ref int b)
    {
        a = a ^ b;
        b = a ^ b;
        a = a ^ b;
    }

    // ----------------------------------------------------------------------
    // ## عملیات‌های Safe Arithmetic (محاسبات ایمن)
    // ----------------------------------------------------------------------

    /// <summary>
    /// 37. جمع با throw exception در صورت overflow. (safeAdd)
    /// </summary>
    public static int SafeAdd(int a, int b) => checked(a + b);

    /// <summary>
    /// 38. ضرب با throw exception در صورت overflow. (safeMultiply)
    /// </summary>
    public static int SafeMultiply(int a, int b) => checked(a * b);

    /// <summary>
    /// 39. تفریق با throw exception در صورت overflow. (safeSubtract)
    /// </summary>
    public static int SafeSubtract(int a, int b) => checked(a - b);

    // ----------------------------------------------------------------------
    // ## عملیات‌های آرایه (Array Operations)
    // ----------------------------------------------------------------------

    /// <summary>
    /// 3. میانگین چند int بدون overflow. (average)
    /// </summary>
    public static int Average(params int[] numbers)
    {
        if (numbers == null || numbers.Length == 0) return 0;
        long sum = 0;
        foreach (var num in numbers) sum += num;
        return (int)(sum / numbers.Length);
    }

    /// <summary>
    /// 25. بیشترین مقدار بین چند int. (max)
    /// </summary>
    public static int Max(params int[] numbers)
    {
        if (numbers == null || numbers.Length == 0) throw new ArgumentException("Array cannot be empty.", nameof(numbers));
        return numbers.Max();
    }

    /// <summary>
    /// 26. کمترین مقدار بین چند int. (min)
    /// </summary>
    public static int Min(params int[] numbers)
    {
        if (numbers == null || numbers.Length == 0) throw new ArgumentException("Array cannot be empty.", nameof(numbers));
        return numbers.Min();
    }

    /// <summary>
    /// 43. جمع تمام اعضای آرایه int بدون overflow. (sumArray)
    /// </summary>
    public static long SumArray(params int[] numbers)
    {
        if (numbers == null) return 0;
        long sum = 0;
        foreach (var num in numbers) sum += num;
        return sum;
    }

    /// <summary>
    /// 46. تبدیل آرایه int به رشته جداشده با کاما. (toCommaSeparatedString)
    /// </summary>
    public static string ToCommaSeparatedString(params int[] numbers)
    {
        if (numbers == null) return string.Empty;
        return string.Join(", ", numbers);
    }

    // ----------------------------------------------------------------------
    // ## عملیات‌های Byte/Endianness عمومی
    // ----------------------------------------------------------------------

    /// <summary>
    /// 6. تبدیل 4 بایت به int به ترتیب Big-Endian. (bytesToIntBE)
    /// </summary>
    public static int BytesToIntBE(byte[] bytes)
    {
        if (bytes.Length < 4) throw new ArgumentException("Byte array must contain at least 4 elements.");
        if (BitConverter.IsLittleEndian)
        {
            Array.Reverse(bytes, 0, 4);
        }
        return BitConverter.ToInt32(bytes, 0);
    }

    /// <summary>
    /// 7. تبدیل 4 بایت به int به ترتیب Little-Endian. (bytesToIntLE)
    /// </summary>
    public static int BytesToIntLE(byte[] bytes)
    {
        if (bytes.Length < 4) throw new ArgumentException("Byte array must contain at least 4 elements.");
        if (!BitConverter.IsLittleEndian)
        {
            Array.Reverse(bytes, 0, 4);
        }
        return BitConverter.ToInt32(bytes, 0);
    }

    // ----------------------------------------------------------------------
    // ## عملیات‌های پارس و تصادفی (Parsing & Random)
    // ----------------------------------------------------------------------

    /// <summary>
    /// 29. تولید int تصادفی در بازه مشخص. (nextRandomInt)
    /// </summary>
    public static int NextRandomInt(int min, int max)
    {
        return Rng.Next(min, max + 1);
    }

    /// <summary>
    /// 30. پارس رشته به int یا بازگشت مقدار پیش‌فرض در صورت خطا. (parseIntOrDefault)
    /// </summary>
    public static int ParseIntOrDefault(string s, int defaultValue = 0)
    {
        return int.TryParse(s, out int result) ? result : defaultValue;
    }

    /// <summary>
    /// 31. پارس رشته به Integer یا null در صورت خطا. (parseIntOrNull)
    /// </summary>
    public static int? ParseIntOrNull(string s)
    {
        return int.TryParse(s, out int result) ? (int?)result : null;
    }
}