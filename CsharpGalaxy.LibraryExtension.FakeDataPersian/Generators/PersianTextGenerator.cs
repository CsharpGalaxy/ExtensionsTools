namespace CsharpGalaxy.LibraryExtension.FakeDataPersian.Generators;

public static class PersianTextGenerator
{
    private static readonly Random Random = new();

    private static readonly string[] WordList = new[]
    {
        "درختان", "سبز", "شمال", "زیبایی", "طبیعت", "کوه", "دریا", "باران", "آسمان", "خورشید",
        "ماه", "ستاره", "شب", "روز", "صبح", "عصر", "شام", "پرنده", "گل", "بوته",
        "جنگل", "تنهایی", "خاطره", "یادها", "رویای", "امید", "عشق", "دوستی", "خیر", "برکت"
    };

    private static readonly string[] SentenceTemplates = new[]
    {
        "درختان سبز شمال، زیبایی طبیعت را دوچندان می‌کنند.",
        "کوه‌های بلند و دریای آرام، آرامش روح را می‌یابند.",
        "باران پاییزی، تازگی و شادابی به خیابان‌ها می‌آورد.",
        "آسمان شب، ستاره‌های خاموش را تماشا می‌کند.",
        "صبح زود، پرندگان سرود زندگی می‌خوانند.",
        "شام‌های خاموش، خاطرات را زنده می‌کند.",
        "گل‌های بهاری، رنگین‌کمان امید را نشان می‌دهند.",
        "یادهای قدیم، درختان جنگل را زینت می‌دهند.",
        "عشق و دوستی، بنیاد زندگی خوب است.",
        "برکت خیر، در دل انسان‌های مهربان جای می‌گیرد."
    };

    /// <summary>
    /// جملهٔ تصادفی را برمی‌گرداند
    /// </summary>
    public static string Sentence() => SentenceTemplates[Random.Next(SentenceTemplates.Length)];

    /// <summary>
    /// تعدادی جمله را برمی‌گرداند
    /// </summary>
    public static string Sentences(int count)
    {
        var sentences = Enumerable.Range(0, count)
            .Select(_ => Sentence())
            .ToList();

        return string.Join(" ", sentences);
    }

    /// <summary>
    /// Random word
    /// </summary>
    public static string Word() => WordList[Random.Next(WordList.Length)];

    /// <summary>
    /// Multiple words
    /// </summary>
    public static string Words(int count)
    {
        var words = Enumerable.Range(0, count)
            .Select(_ => Word())
            .ToList();

        return string.Join("، ", words);
    }

    /// <summary>
    /// ایمیل تصادفی را برمی‌گرداند
    /// </summary>
    public static string Email()
    {

        string userName = PersianNameGenerator.UserName();
  

        string domains = "gmail.com";
        return $"{userName}@{domains}";
    }

    /// <summary>
    /// نام‌کاربری تصادفی را برمی‌گرداند
    /// </summary>
    public static string Username()
    {
       return PersianNameGenerator.UserName();

    }

    /// <summary>
    /// نام‌کاربری طولانی‌تر را برمی‌گرداند
    /// </summary>
    public static string UsernameWithLastName()
    {
        string firstName = PersianNameGenerator.FirstName().Replace(" ", "").Replace("‌", "");
        string lastName = PersianNameGenerator.LastName().Replace(" ", "").Replace("‌", "");
        int randomNumber = Random.Next(10, 999);

        return $"{firstName.ToLower()}_{lastName.ToLower()}_{randomNumber}";
    }
}
