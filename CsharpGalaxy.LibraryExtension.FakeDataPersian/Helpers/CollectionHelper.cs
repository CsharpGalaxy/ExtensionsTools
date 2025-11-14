namespace CsharpGalaxy.LibraryExtension.FakeDataPersian.Helpers;

using System.Data;

public static class CollectionHelper
{
    private static readonly Random Random = new();

    /// <summary>
    /// لیست تصادفی از نوع T را برمی‌گرداند
    /// </summary>
    public static List<T> RandomList<T>(Func<T> generator, int count)
    {
        return Enumerable.Range(0, count)
            .Select(_ => generator())
            .ToList();
    }

    /// <summary>
    /// لیست یکتا از نوع T را برمی‌گرداند
    /// </summary>
    public static HashSet<T> UniqueList<T>(Func<T> generator, int count) where T : class
    {
        var hashSet = new HashSet<T>();

        while (hashSet.Count < count)
        {
            var item = generator();
            if (item != null)
                hashSet.Add(item);
        }

        return hashSet;
    }

    /// <summary>
    /// IEnumerable را به DataTable تبدیل می‌کند (برای بایندینگ دیتاگرید)
    /// </summary>
    public static DataTable ToDataTable<T>(this IEnumerable<T> items) where T : class
    {
        var dataTable = new DataTable(typeof(T).Name);

        if (!items.Any())
            return dataTable;

        var properties = typeof(T).GetProperties();

        // افزودن ستون‌ها
        foreach (var property in properties)
        {
            var columnType = property.PropertyType;
            if (columnType.IsGenericType && columnType.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                columnType = columnType.GetGenericArguments()[0];
            }

            dataTable.Columns.Add(property.Name, columnType);
        }

        // افزودن ردیف‌ها
        foreach (var item in items)
        {
            var values = new object[properties.Length];
            for (int i = 0; i < properties.Length; i++)
            {
                var value = properties[i].GetValue(item);
                values[i] = value ?? DBNull.Value;
            }

            dataTable.Rows.Add(values);
        }

        return dataTable;
    }

    /// <summary>
    /// نمونه تصادفی از لیست را برمی‌گرداند
    /// </summary>
    public static T RandomItem<T>(this IEnumerable<T> items)
    {
        var list = items.ToList();
        if (list.Count == 0)
            throw new InvalidOperationException("لیست خالی است");

        return list[Random.Next(list.Count)];
    }

    /// <summary>
    /// تعدادی نمونهٔ تصادفی از لیست را برمی‌گرداند
    /// </summary>
    public static List<T> RandomItems<T>(this IEnumerable<T> items, int count)
    {
        var list = items.ToList();
        if (list.Count == 0)
            throw new InvalidOperationException("لیست خالی است");

        var result = new List<T>();
        for (int i = 0; i < count && i < list.Count; i++)
        {
            result.Add(list[Random.Next(list.Count)]);
        }

        return result;
    }

    /// <summary>
    /// لیست را به صورت تصادفی مخلوط می‌کند
    /// </summary>
    public static List<T> Shuffle<T>(this IEnumerable<T> items)
    {
        var list = items.ToList();

        for (int i = list.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Next(0, i + 1);

            // Swap
            (list[i], list[randomIndex]) = (list[randomIndex], list[i]);
        }

        return list;
    }

    /// <summary>
    /// لیست را به تعدادی دسته تقسیم می‌کند
    /// </summary>
    public static List<List<T>> Batch<T>(this IEnumerable<T> items, int batchSize)
    {
        var list = items.ToList();
        var batches = new List<List<T>>();

        for (int i = 0; i < list.Count; i += batchSize)
        {
            batches.Add(list.Skip(i).Take(batchSize).ToList());
        }

        return batches;
    }
}
