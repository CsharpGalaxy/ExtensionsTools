

public static class NullPatternExtentions
{    // ایجاد یک نمونه جدید از نوع T (فقط برای انواعی که کانسترکتور پیش‌فرض دارند)
    public static T Create<T>() where T : new() => new T();

    // اگر obj null بود، یک نمونه جدید از T برمی‌گردونه، در غیر این صورت خود obj
    public static T NothingIfNull<T>(this T obj) where T : new() => obj == null ? new T() : obj;
}

