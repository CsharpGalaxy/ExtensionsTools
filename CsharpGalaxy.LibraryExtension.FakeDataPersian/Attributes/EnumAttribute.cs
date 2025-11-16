namespace CsharpGalaxy.LibraryExtension.FakeDataPersian.Attributes;

/// <summary>
/// Attribute برای تولید مقادیر تصادفی از enum
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class EnumAttribute : Attribute
{
    /// <summary>
    /// نوع Enum
    /// </summary>
    public Type EnumType { get; }

    /// <summary>
    /// مقدار‌های خاصی را محدود می‌کند
    /// null به معنی استفاده از تمام مقادیر است
    /// </summary>
    public object[] AllowedValues { get; set; }

    /// <summary>
    /// سازنده Enum Attribute
    /// </summary>
    /// <param name="enumType">نوع Enum</param>
    public EnumAttribute(Type enumType)
    {
        if (!enumType.IsEnum)
            throw new ArgumentException($"Type {enumType.Name} is not an enum type");

        EnumType = enumType;
        AllowedValues = null;
    }

    /// <summary>
    /// سازنده Enum Attribute با مقادیر محدود
    /// </summary>
    /// <param name="enumType">نوع Enum</param>
    /// <param name="allowedValues">مقادیر مجاز</param>
    public EnumAttribute(Type enumType, params object[] allowedValues)
    {
        if (!enumType.IsEnum)
            throw new ArgumentException($"Type {enumType.Name} is not an enum type");

        EnumType = enumType;
        AllowedValues = allowedValues;
    }
}
