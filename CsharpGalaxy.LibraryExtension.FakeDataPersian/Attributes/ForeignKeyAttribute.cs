namespace CsharpGalaxy.LibraryExtension.FakeDataPersian.Attributes;

/// <summary>
/// نشان‌دهندهٔ یک کلید خارجی برای روابط بین جداول
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class ForeignKeyAttribute : Attribute
{
    /// <summary>
    /// نام نوعی که این کلید خارجی به آن اشاره می‌کند
    /// </summary>
    public string ReferencedType { get; }

    /// <summary>
    /// آیا این کلید خارجی اختیاری است؟
    /// </summary>
    public bool IsOptional { get; set; }

    /// <summary>
    /// درصد احتمال null بودن کلید خارجی اختیاری
    /// </summary>
    public int NullProbability { get; set; } = 50;

    /// <summary>
    /// Constructor برای تعریف یک کلید خارجی
    /// </summary>
    public ForeignKeyAttribute(string referencedType)
    {
        ReferencedType = referencedType;
    }
}
