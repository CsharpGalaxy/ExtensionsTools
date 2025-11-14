namespace CsharpGalaxy.LibraryExtension.FakeDataPersian.Generators;

public static class InternetCryptoGenerator
{
    private static readonly Random Random = new();

    /// <summary>
    /// آدرس IPV4 تصادفی را برمی‌گرداند
    /// </summary>
    public static string IPv4()
    {
        int a = Random.Next(0, 256);
        int b = Random.Next(0, 256);
        int c = Random.Next(0, 256);
        int d = Random.Next(0, 256);

        return $"{a}.{b}.{c}.{d}";
    }

    /// <summary>
    /// آدرس IPV4 خصوصی را برمی‌گرداند
    /// </summary>
    public static string IPv4Private()
    {
        // ۱۰.x.x.x یا ۱۹۲.۱۶۸.x.x یا ۱۷۲.۱۶.x.x
        int type = Random.Next(0, 3);

        return type switch
        {
            0 => $"10.{Random.Next(0, 256)}.{Random.Next(0, 256)}.{Random.Next(0, 256)}",
            1 => $"192.168.{Random.Next(0, 256)}.{Random.Next(0, 256)}",
            _ => $"172.{Random.Next(16, 32)}.{Random.Next(0, 256)}.{Random.Next(0, 256)}"
        };
    }

    /// <summary>
    /// آدرس MAC را برمی‌گرداند
    /// فرمت: ۰۰:۱A:۲B:۳C:۴D:۵E
    /// </summary>
    public static string MAC()
    {
        var macBytes = new byte[6];
        Random.NextBytes(macBytes);

        return string.Join(":", macBytes.Select(x => x.ToString("X2")));
    }

    /// <summary>
    /// GUID تصادفی را برمی‌گرداند
    /// </summary>
    public static Guid Guid()
    {
        return System.Guid.NewGuid();
    }

    /// <summary>
    /// GUID را با فرمت رشته برمی‌گرداند
    /// </summary>
    public static string GuidString()
    {
        return System.Guid.NewGuid().ToString();
    }

    /// <summary>
    /// GUID بدون هایفن را برمی‌گرداند
    /// </summary>
    public static string GuidStringNoHyphen()
    {
        return System.Guid.NewGuid().ToString("N");
    }

    /// <summary>
    /// Slug URL تصادفی را برمی‌گرداند
    /// </summary>
    public static string Slug()
    {
        string[] words = new[] { "hello", "world", "test", "data", "fake", "generator", "random", "code" };
        var selected = Enumerable.Range(0, Random.Next(2, 5))
            .Select(_ => words[Random.Next(words.Length)])
            .ToList();

        return string.Join("-", selected);
    }

    /// <summary>
    /// Token تصادفی را برمی‌گرداند (۳۲ کاراکتر)
    /// </summary>
    public static string Token()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        return new string(Enumerable.Range(0, 32)
            .Select(_ => chars[Random.Next(chars.Length)])
            .ToArray());
    }

    /// <summary>
    /// URL تصادفی را برمی‌گرداند
    /// </summary>
    public static string Url()
    {
        string domain = Slug();
        return $"https://{domain}.com";
    }
}
