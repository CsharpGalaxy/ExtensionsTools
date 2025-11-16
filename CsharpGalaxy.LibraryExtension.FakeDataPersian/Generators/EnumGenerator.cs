namespace CsharpGalaxy.LibraryExtension.FakeDataPersian.Generators;

/// <summary>
/// جنریتور برای ایجاد مقادیر تصادفی از enum types
/// </summary>
public class EnumGenerator
{
    private static readonly Random _random = new();

    /// <summary>
    /// یک مقدار تصادفی از enum T را برمی‌گرداند
    /// </summary>
    /// <typeparam name="T">نوع Enum</typeparam>
    /// <returns>یک مقدار تصادفی از enum</returns>
    public static T GetRandomValue<T>() where T : Enum
    {
        var values = Enum.GetValues(typeof(T));
        if (values.Length == 0)
            throw new InvalidOperationException($"Enum type {typeof(T).Name} has no values");

        var randomIndex = _random.Next(values.Length);
        return (T)values.GetValue(randomIndex);
    }

    /// <summary>
    /// یک لیست از مقادیر تصادفی enum را برمی‌گرداند
    /// </summary>
    /// <typeparam name="T">نوع Enum</typeparam>
    /// <param name="count">تعداد مقادیر درخواستی</param>
    /// <returns>لیستی از مقادیر enum</returns>
    public static List<T> GetRandomValues<T>(int count) where T : Enum
    {
        var result = new List<T>();
        for (int i = 0; i < count; i++)
        {
            result.Add(GetRandomValue<T>());
        }
        return result;
    }

    /// <summary>
    /// تمام مقادیر enum را برمی‌گرداند
    /// </summary>
    /// <typeparam name="T">نوع Enum</typeparam>
    /// <returns>آرایه‌ای از تمام مقادیر enum</returns>
    public static T[] GetAllValues<T>() where T : Enum
    {
        return Enum.GetValues(typeof(T)).Cast<T>().ToArray();
    }

    /// <summary>
    /// یک مقدار تصادفی از enum را بر اساس نوع (Type) برمی‌گرداند
    /// </summary>
    /// <param name="enumType">نوع Enum</param>
    /// <returns>یک مقدار تصادفی</returns>
    public static object GetRandomEnumValue(Type enumType)
    {
        if (!enumType.IsEnum)
            throw new ArgumentException($"Type {enumType.Name} is not an enum type");

        var values = Enum.GetValues(enumType);
        if (values.Length == 0)
            throw new InvalidOperationException($"Enum type {enumType.Name} has no values");

        var randomIndex = _random.Next(values.Length);
        return values.GetValue(randomIndex);
    }

    /// <summary>
    /// یک مقدار تصادفی از enum را بر اساس نام enum (string) برمی‌گرداند
    /// </summary>
    /// <param name="enumTypeName">نام کامل نوع Enum</param>
    /// <returns>یک مقدار تصادفی</returns>
    public static object GetRandomEnumValueByName(string enumTypeName)
    {
        var enumType = Type.GetType(enumTypeName) 
            ?? throw new ArgumentException($"Type {enumTypeName} not found");
        
        return GetRandomEnumValue(enumType);
    }
}
