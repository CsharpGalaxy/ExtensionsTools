namespace CsharpGalaxy.LibraryExtension.FakeDataPersian.Attributes;

/// <summary>
/// برای تعریف یک مقدار ثابت برای یک property
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class ConstantAttribute : Attribute
{
    /// <summary>
    /// مقدار ثابتی که برای property تنظیم می‌شود
    /// </summary>
    public object Value { get; }

    /// <summary>
    /// Constructor برای تعریف یک مقدار ثابت
    /// </summary>
    public ConstantAttribute(object value)
    {
        Value = value;
    }
}
