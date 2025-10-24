namespace CsharpGalexy.LibraryExtention.Helpers.Collections;



public static class ListHelper
{
    // 1. isNullOrEmpty
    public static bool IsNullOrEmpty<T>(this IList<T> list) => list == null || list.Count == 0;

    // 2. isNotEmpty
    public static bool IsNotEmpty<T>(this IList<T> list) => !IsNullOrEmpty(list);

    // 3. sizeOf
    public static int SizeOf<T>(this IList<T> list) => list?.Count ?? 0;

    // 4. getFirst
    public static T GetFirst<T>(this IList<T> list) => list.IsNullOrEmpty() ? default(T) : list[0];

    // 5. getLast
    public static T GetLast<T>(this IList<T> list) => list.IsNullOrEmpty() ? default(T) : list[list.Count - 1];

    // 6. getOrElse
    public static T GetOrElse<T>(this IList<T> list, int index, T defaultValue = default(T))
    {
        if (list != null && index >= 0 && index < list.Count)
            return list[index];
        return defaultValue;
    }

    // 7. getRandom
    private static readonly Random _random = new Random();
    public static T GetRandom<T>(this IList<T> list)
    {
        if (list.IsNullOrEmpty()) return default(T);
        lock (_random) // برای thread-safety
            return list[_random.Next(list.Count)];
    }

    // 8. setSafe (فقط برای List<T> چون نیاز به Add دارد)
    public static void SetSafe<T>(this List<T> list, int index, T value)
    {
        if (list == null) throw new ArgumentNullException(nameof(list));
        while (list.Count <= index)
            list.Add(default(T));
        list[index] = value;
    }

    // 9. addIfAbsent
    public static bool AddIfAbsent<T>(this List<T> list, T item)
    {
        if (list == null) throw new ArgumentNullException(nameof(list));
        if (!list.Contains(item))
        {
            list.Add(item);
            return true;
        }
        return false;
    }

    // 10. addAllIfAbsent
    public static void AddAllIfAbsent<T>(this List<T> list, IEnumerable<T> items)
    {
        if (list == null || items == null) return;
        foreach (var item in items)
            list.AddIfAbsent(item);
    }

    // 11. removeFirst
    public static T RemoveFirst<T>(this List<T> list)
    {
        if (list.IsNullOrEmpty()) return default(T);
        var first = list[0];
        list.RemoveAt(0);
        return first;
    }

    // 12. removeLast
    public static T RemoveLast<T>(this List<T> list)
    {
        if (list.IsNullOrEmpty()) return default(T);
        var last = list[list.Count - 1];
        list.RemoveAt(list.Count - 1);
        return last;
    }

    // 13. removeNulls
    public static void RemoveNulls<T>(this List<T> list) where T : class
    {
        if (list == null) return;
        list.RemoveAll(item => item == null);
    }

    // 14. removeDuplicates
    public static void RemoveDuplicates<T>(this List<T> list)
    {
        if (list == null) return;
        var seen = new HashSet<T>();
        list.RemoveAll(item => !seen.Add(item));
    }

    // 15. removeByIndex
    public static void RemoveByIndex<T>(this List<T> list, params int[] indices)
    {
        if (list == null || indices == null || indices.Length == 0) return;
        var sortedIndices = indices.Where(i => i >= 0 && i < list.Count).OrderByDescending(i => i).Distinct();
        foreach (var idx in sortedIndices)
            list.RemoveAt(idx);
    }

    // 16. retainAll
    public static void RetainAll<T>(this List<T> list, IEnumerable<T> toKeep)
    {
        if (list == null || toKeep == null) return;
        var set = new HashSet<T>(toKeep);
        list.RemoveAll(item => !set.Contains(item));
    }

    // 17. swap
    public static void Swap<T>(this IList<T> list, int i, int j)
    {
        if (list == null || i < 0 || j < 0 || i >= list.Count || j >= list.Count) return;
        (list[i], list[j]) = (list[j], list[i]);
    }

    // 18. reverse
    public static void Reverse<T>(this IList<T> list)
    {
        if (list.IsNullOrEmpty()) return;
        var n = list.Count;
        for (int i = 0; i < n / 2; i++)
            list.Swap(i, n - 1 - i);
    }

    // 19. shuffle
    public static void Shuffle<T>(this IList<T> list)
    {
        if (list.IsNullOrEmpty()) return;
        lock (_random)
        {
            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = _random.Next(i + 1);
                list.Swap(i, j);
            }
        }
    }

    // 20. rotateLeft
    public static void RotateLeft<T>(this IList<T> list, int n)
    {
        if (list.IsNullOrEmpty()) return;
        n = ((n % list.Count) + list.Count) % list.Count;
        if (n == 0) return;
        var temp = new T[n];
        for (int i = 0; i < n; i++)
            temp[i] = list[i];
        for (int i = n; i < list.Count; i++)
            list[i - n] = list[i];
        for (int i = 0; i < n; i++)
            list[list.Count - n + i] = temp[i];
    }

    // 21. rotateRight
    public static void RotateRight<T>(this IList<T> list, int n)
    {
        if (list.IsNullOrEmpty()) return;
        n = ((n % list.Count) + list.Count) % list.Count;
        if (n == 0) return;
        list.RotateLeft(list.Count - n);
    }

    // 22. sortNatural
    public static void SortNatural<T>(this IList<T> list) where T : IComparable<T>
    {
        if (list is List<T> concreteList)
            concreteList.Sort();
        else
            throw new NotSupportedException("SortNatural only supports List<T>.");
    }

    // 23. sortReverse
    public static void SortReverse<T>(this IList<T> list) where T : IComparable<T>
    {
        if (list is List<T> concreteList)
            concreteList.Sort((x, y) => y.CompareTo(x));
        else
            throw new NotSupportedException("SortReverse only supports List<T>.");
    }

    // 24. sortBy
    public static void SortBy<T>(this IList<T> list, Comparison<T> comparison)
    {
        if (list is List<T> concreteList)
            concreteList.Sort(comparison);
        else
            throw new NotSupportedException("SortBy only supports List<T>.");
    }

    // 25. min
    public static T Min<T>(this IEnumerable<T> source) where T : IComparable<T>
        => source?.Any() == true ? source.Min() : default(T);

    // 26. max
    public static T Max<T>(this IEnumerable<T> source) where T : IComparable<T>
        => source?.Any() == true ? source.Max() : default(T);

    // 27-29. sum
    public static int SumInt(this IEnumerable<int> source) => source?.Sum() ?? 0;
    public static long SumLong(this IEnumerable<long> source) => source?.Sum() ?? 0L;
    public static double SumDouble(this IEnumerable<double> source) => source?.Sum() ?? 0.0;

    // 30. average
    public static double Average(this IEnumerable<double> source) => source?.Any() == true ? source.Average() : 0.0;

    // 31. count
    public static int Count<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        => source?.Count(predicate) ?? 0;

    // 32. countDistinct
    public static int CountDistinct<T>(this IEnumerable<T> source)
        => source?.Distinct().Count() ?? 0;

    // 33. filter
    public static List<T> Filter<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        => source?.Where(predicate).ToList() ?? new List<T>();

    // 34. filterNot
    public static List<T> FilterNot<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        => source?.Where(x => !predicate(x)).ToList() ?? new List<T>();

    // 35. filterNotNull
    public static List<T> FilterNotNull<T>(this IEnumerable<T> source) where T : class
        => source?.Where(x => x != null).ToList() ?? new List<T>();

    // 36. map
    public static List<TResult> Map<T, TResult>(this IEnumerable<T> source, Func<T, TResult> selector)
        => source?.Select(selector).ToList() ?? new List<TResult>();

    // 37. mapIndexed
    public static List<TResult> MapIndexed<T, TResult>(this IEnumerable<T> source, Func<int, T, TResult> selector)
    {
        if (source == null) return new List<TResult>();
        var result = new List<TResult>();
        int index = 0;
        foreach (var item in source)
            result.Add(selector(index++, item));
        return result;
    }

    // 38. mapNotNull
    public static List<TResult> MapNotNull<T, TResult>(this IEnumerable<T> source, Func<T, TResult> selector)
        where TResult : class
        => source?.Select(selector).Where(x => x != null).ToList() ?? new List<TResult>();

    // 39. flatMap
    public static List<TResult> FlatMap<T, TResult>(this IEnumerable<T> source, Func<T, IEnumerable<TResult>> selector)
        => source?.SelectMany(selector).ToList() ?? new List<TResult>();

    // 40. distinct
    public static List<T> Distinct<T>(this IEnumerable<T> source)
        => source?.Distinct().ToList() ?? new List<T>();

    // 41. distinctBy
    public static List<T> DistinctBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> keySelector)
        => source?.GroupBy(keySelector).Select(g => g.First()).ToList() ?? new List<T>();

    // 42-47. take/drop
    public static List<T> Take<T>(this IEnumerable<T> source, int count) => source?.Take(count).ToList() ?? new List<T>();
    //public static List<T> TakeLast<T>(this IEnumerable<T> source, int count) => source?.Reverse().Take(count).Reverse().ToList() ?? new List<T>();
    public static List<T> TakeWhile<T>(this IEnumerable<T> source, Func<T, bool> predicate) => source?.TakeWhile(predicate).ToList() ?? new List<T>();
    public static List<T> Drop<T>(this IEnumerable<T> source, int count) => source?.Skip(count).ToList() ?? new List<T>();
    public static List<T> DropLast<T>(this IEnumerable<T> source, int count) => source?.Take(Math.Max(0, source.Count() - count)).ToList() ?? new List<T>();
    public static List<T> DropWhile<T>(this IEnumerable<T> source, Func<T, bool> predicate) => source?.SkipWhile(predicate).ToList() ?? new List<T>();

    // 48. slice
    public static List<T> Slice<T>(this IList<T> source, int start, int? end = null)
    {
        if (source == null) return new List<T>();
        end ??= source.Count;
        if (start < 0) start = Math.Max(0, source.Count + start);
        if (end < 0) end = Math.Max(0, source.Count + end.Value);
        if (start >= source.Count || start >= end) return new List<T>();
        var count = Math.Min(end.Value - start, source.Count - start);
        return source.Skip(start).Take(count).ToList();
    }

    public static List<List<T>> Chunked<T>(this IEnumerable<T> source, int size)
    {
        if (source == null || size <= 0)
            return new List<List<T>>();

        return source
            .Select((item, index) => new { item, index })
            .GroupBy(x => x.index / size)
            .Select(g => g.Select(x => x.item).ToList())
            .ToList();
    }


    // 50. windowed
    public static List<List<T>> Windowed<T>(this IEnumerable<T> source, int size, int step = 1)
    {
        if (source == null || size <= 0 || step <= 0) return new List<List<T>>();
        var list = source.ToList();
        var result = new List<List<T>>();
        for (int i = 0; i <= list.Count - size; i += step)
            result.Add(list.GetRange(i, size));
        return result;
    }

    // 51. partition
    public static (List<T> True, List<T> False) Partition<T>(this IEnumerable<T> source, Func<T, bool> predicate)
    {
        var trueList = new List<T>();
        var falseList = new List<T>();
        if (source != null)
        {
            foreach (var item in source)
            {
                if (predicate(item))
                    trueList.Add(item);
                else
                    falseList.Add(item);
            }
        }
        return (trueList, falseList);
    }

    // 52. groupBy
    //public static Dictionary<TKey, List<T>> GroupBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> keySelector)
    //    => source?.GroupBy(keySelector).ToDictionary(g => g.Key, g => g.ToList()) ?? new Dictionary<TKey, List<T>>();

    //// 53. groupByCounting
    //public static Dictionary<TKey, int> GroupByCounting<T, TKey>(this IEnumerable<T> source, Func<T, TKey> keySelector)
    //    => source?.GroupBy(keySelector).ToDictionary(g => g.Key, g => g.Count()) ?? new Dictionary<TKey, int>();

    // 54. zip
    public static List<(T1, T2)> Zip<T1, T2>(this IEnumerable<T1> first, IEnumerable<T2> second)
        => first?.Zip(second, (a, b) => (a, b)).ToList() ?? new List<(T1, T2)>();

    // 55. zipWithNext
    public static List<(T, T)> ZipWithNext<T>(this IEnumerable<T> source)
    {
        if (source == null) return new List<(T, T)>();
        var list = source.ToList();
        var result = new List<(T, T)>();
        for (int i = 0; i < list.Count - 1; i++)
            result.Add((list[i], list[i + 1]));
        return result;
    }

    // 56. unzip
    public static (List<T1>, List<T2>) Unzip<T1, T2>(this IEnumerable<(T1, T2)> source)
    {
        var list1 = new List<T1>();
        var list2 = new List<T2>();
        if (source != null)
        {
            foreach (var (a, b) in source)
            {
                list1.Add(a);
                list2.Add(b);
            }
        }
        return (list1, list2);
    }

    // 57-59. find
    //public static T FindFirst<T>(this IEnumerable<T> source, Func<T, bool> predicate) => source?.FirstOrDefault(predicate);
    //public static T FindLast<T>(this IEnumerable<T> source, Func<T, bool> predicate) => source?.Reverse().FirstOrDefault(predicate);
    public static List<T> FindAll<T>(this IEnumerable<T> source, Func<T, bool> predicate) => source?.Where(predicate).ToList() ?? new List<T>();

    // 60-62. match
    public static bool AnyMatch<T>(this IEnumerable<T> source, Func<T, bool> predicate) => source?.Any(predicate) == true;
    public static bool AllMatch<T>(this IEnumerable<T> source, Func<T, bool> predicate) => source?.All(predicate) == true;
    public static bool NoneMatch<T>(this IEnumerable<T> source, Func<T, bool> predicate) => !AnyMatch(source, predicate);

    // 63-65. contains
    public static bool Contains<T>(this IEnumerable<T> source, T item) => source?.Contains(item) == true;
    public static bool ContainsAll<T>(this IEnumerable<T> source, IEnumerable<T> others) => others?.All(item => source.Contains(item)) == true;
    public static bool ContainsAny<T>(this IEnumerable<T> source, IEnumerable<T> others) => others?.Any(item => source.Contains(item)) == true;

    // 66-69. indexOf
    public static int IndexOf<T>(this IList<T> source, T item) => source?.IndexOf(item) ?? -1;
    public static int LastIndexOf<T>(this IList<T> source, T item) => source?.LastIndexOf(item) ?? -1;
    public static int IndexOfFirst<T>(this IList<T> source, Func<T, bool> predicate)
    {
        if (source == null) return -1;
        for (int i = 0; i < source.Count; i++)
            if (predicate(source[i]))
                return i;
        return -1;
    }
    public static int IndexOfLast<T>(this IList<T> source, Func<T, bool> predicate)
    {
        if (source == null) return -1;
        for (int i = source.Count - 1; i >= 0; i--)
            if (predicate(source[i]))
                return i;
        return -1;
    }

    // 70. subList
    public static List<T> SubList<T>(this IList<T> source, int start, int end)
        => source?.Skip(start).Take(end - start).ToList() ?? new List<T>();

    // 71. merge
    public static List<T> Merge<T>(params IEnumerable<T>[] lists)
        => lists?.SelectMany(x => x ?? Enumerable.Empty<T>()).ToList() ?? new List<T>();

    // 72-75. set operations
    public static List<T> Intersect<T>(this IEnumerable<T> first, IEnumerable<T> second)
        => first?.Intersect(second).ToList() ?? new List<T>();

    public static List<T> Union<T>(this IEnumerable<T> first, IEnumerable<T> second)
        => first?.Union(second).ToList() ?? new List<T>();

    public static List<T> Subtract<T>(this IEnumerable<T> first, IEnumerable<T> second)
        => first?.Except(second).ToList() ?? new List<T>();

    public static List<T> SymmetricDiff<T>(this IEnumerable<T> first, IEnumerable<T> second)
        => first.Subtract(second).Concat(second.Subtract(first)).ToList();

    // 76-79. conversions
    public static T[] ToArray<T>(this IEnumerable<T> source) => source?.ToArray() ?? new T[0];
    public static HashSet<T> ToSet<T>(this IEnumerable<T> source) => source != null ? new HashSet<T>(source) : new HashSet<T>();
    public static Dictionary<TKey, T> ToMap<T, TKey>(this IEnumerable<T> source, Func<T, TKey> keySelector)
        => source?.ToDictionary(keySelector) ?? new Dictionary<TKey, T>();

    public static IReadOnlyList<T> ToImmutableList<T>(this IEnumerable<T> source)
        => source?.ToList().AsReadOnly() ?? (IReadOnlyList<T>)new List<T>().AsReadOnly();

    // 80. synchronize
    public static IList<T> Synchronize<T>(this IList<T> list)
        => list != null ? System.Collections.ArrayList.Synchronized(new System.Collections.ArrayList(list.Cast<object>().ToArray())).Cast<T>().ToList() : null;

    // ⚠️ نکته: برای thread-safety بهتر است از ConcurrentBag یا lock استفاده کنید.
    // ولی برای سازگاری با IList<T>، این روش ساده‌تر است.
}
