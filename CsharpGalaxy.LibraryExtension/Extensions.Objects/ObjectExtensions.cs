using CsharpGalexy.LibraryExtention.Models;

using System;
using System.Collections;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;


namespace CsharpGalaxy.LibraryExtension.Extensions.Object;

public static class ObjectExtensions
{
    /// <summary>
    /// بررسی می‌کند که آبجکت نال باشد.
    /// </summary>
    public static bool IsNull(this object obj)
        => obj is null;

    /// <summary>
    /// بررسی می‌کند که آبجکت نال نباشد.
    /// </summary>
    public static bool IsNotNull(this object obj)
        => obj is not null;

    /// <summary>
    /// بررسی می‌کند که آبجکت و تمام مسیرهای انتخابی نال نباشند.
    /// </summary>
    public static bool IsSafe<T>(this T obj, Func<T, object> selector)
    {
        if (obj == null) return false;

        try
        {
            var value = selector(obj);
            return value != null;
        }
        catch (NullReferenceException)
        {
            return false;
        }
    }

    public static TResult SafeGet<TSource, TResult>(
      this TSource source,
      Func<TSource, TResult> selector)
      where TSource : class
    {
        return source == null ? default : selector(source);
    }
}
