namespace CsharpGalexy.LibraryExtention.Helpers.Collections;

using CsharpGalexy.LibraryExtention.EFCore.Models.PagedList;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TeamLibrary.API.Shared.PagedList;


public static class QueryableExtensions
{
    // ✅ اعمال شرط Where فقط در صورت برقرار بودن شرط بولی
    public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, bool>> predicate)
        => condition ? query.Where(predicate) : query;

    // ✅ مرتب‌سازی داینامیک بر اساس نام پراپرتی
    public static IQueryable<T> OrderByDynamic<T>(this IQueryable<T> query, string propertyName, bool descending = false)
    {
        if (string.IsNullOrEmpty(propertyName)) return query;
        var param = Expression.Parameter(typeof(T), "x");
        var property = Expression.PropertyOrField(param, propertyName);
        var lambda = Expression.Lambda(property, param);
        string methodName = descending ? "OrderByDescending" : "OrderBy";
        var result = Expression.Call(typeof(Queryable), methodName,
            new Type[] { typeof(T), property.Type },
            query.Expression, Expression.Quote(lambda));
        return query.Provider.CreateQuery<T>(result);
    }

    // ✅ مرتب‌سازی ثانویه داینامیک
    public static IQueryable<T> ThenByDynamic<T>(this IOrderedQueryable<T> query, string propertyName, bool descending = false)
    {
        if (string.IsNullOrEmpty(propertyName)) return query;
        var param = Expression.Parameter(typeof(T), "x");
        var property = Expression.PropertyOrField(param, propertyName);
        var lambda = Expression.Lambda(property, param);
        string methodName = descending ? "ThenByDescending" : "ThenBy";
        var result = Expression.Call(typeof(Queryable), methodName,
            new Type[] { typeof(T), property.Type },
            query.Expression, Expression.Quote(lambda));
        return query.Provider.CreateQuery<T>(result);
    }

    // ✅ صفحه‌بندی ساده (Skip / Take)
    public static IQueryable<T> Page<T>(this IQueryable<T> query, int page, int size)
        => query.Skip((page - 1) * size).Take(size);

    // ✅ انتخاب تصادفی بدون لود کل مجموعه
    public static IQueryable<T> TakeRandom<T>(this IQueryable<T> query, int count)
        => query.OrderBy(x => Guid.NewGuid()).Take(count);

    // ✅ بررسی سریع خالی بودن IQueryable
    public static bool IsEmpty<T>(this IQueryable<T> query)
        => !query.Any();

    // ✅ تبدیل به HashSet بدون لیست واسط
    public static async Task<HashSet<T>> ToHashSetAsync<T>(this IQueryable<T> query)
        => new HashSet<T>(await query.ToListAsync());

    // ✅ CountAsync سفارشی
    public static async Task<int> CountAsync<T>(this IQueryable<T> query, Expression<Func<T, bool>> predicate = null)
        => predicate == null ? await EntityFrameworkQueryableExtensions.CountAsync(query)
                             : await EntityFrameworkQueryableExtensions.CountAsync(query.Where(predicate));

    // ✅ AnyAsync اورلود ساده‌شده
    public static Task<bool> AnyAsync<T>(this IQueryable<T> query, Expression<Func<T, bool>> predicate)
        => EntityFrameworkQueryableExtensions.AnyAsync(query, predicate);

    // ✅ انتخاب مقادیر یکتا
    public static IQueryable<TResult> SelectDistinct<T, TResult>(this IQueryable<T> query, Expression<Func<T, TResult>> selector)
        => query.Select(selector).Distinct();

    // ✅ LeftJoin قابل ترجمه به SQL
    //public static IQueryable<TResult> LeftJoin<TOuter, TInner, TKey, TResult>(
    //    this IQueryable<TOuter> outer,
    //    IQueryable<TInner> inner,
    //    Expression<Func<TOuter, TKey>> outerKeySelector,
    //    Expression<Func<TInner, TKey>> innerKeySelector,
    //    Expression<Func<TOuter, TInner, TResult>> resultSelector)
    //{
    //    return outer.GroupJoin(inner, outerKeySelector, innerKeySelector, (o, inners) => new { o, inners })
    //                .SelectMany(x => x.inners.DefaultIfEmpty(), (x, i) => resultSelector.Invoke(x.o, i));
    //}

    // ✅ بارگزاری چندین Include یکجا
    public static IQueryable<T> IncludeMultiple<T>(this IQueryable<T> query, params string[] navigationProperties)
        where T : class
    {
        foreach (var nav in navigationProperties)
            query = query.Include(nav);
        return query;
    }

    // ✅ اورلود کوتاه‌تر برای AsNoTracking
    public static IQueryable<T> AsNoTracking<T>(this IQueryable<T> query)
        where T : class
        => EntityFrameworkQueryableExtensions.AsNoTracking(query);

    // ✅ حذف موقتی فیلترهای سراسری EF
    public static IQueryable<T> IgnoreQueryFilters<T>(this IQueryable<T> query)
        where T : class
        => EntityFrameworkQueryableExtensions.IgnoreQueryFilters(query);

    // ✅ حذف گروهی بدون لود موجودیت‌ها
    public static async Task<int> BatchDeleteAsync<T>(this IQueryable<T> query, Expression<Func<T, bool>> predicate)
        where T : class
    {
        var items = await query.Where(predicate).ToListAsync();
        query.GetDbContext().RemoveRange(items);
        return await query.GetDbContext().SaveChangesAsync();
    }

    // ✅ جستجوی داینامیک در چند فیلد متنی
    public static IQueryable<T> SearchByKeyword<T>(this IQueryable<T> query, string keyword, params Expression<Func<T, string>>[] fields)
    {
        if (string.IsNullOrWhiteSpace(keyword) || fields == null || fields.Length == 0)
            return query;

        var parameter = Expression.Parameter(typeof(T), "x");
        Expression? combined = null;

        foreach (var field in fields)
        {
            var body = Expression.Call(field.Body, nameof(string.Contains), Type.EmptyTypes,
                                       Expression.Constant(keyword, typeof(string)));
            combined = combined == null ? body : Expression.OrElse(combined, body);
        }

        return query.Where(Expression.Lambda<Func<T, bool>>(combined!, parameter));
    }

    // ✅ صفحه‌بندی async با مدل خروجی
    public static async Task<(List<T> Items, int TotalCount)> ToPagedResultAsync<T>(this IQueryable<T> query, int pageIndex, int pageSize)
    {
        var total = await query.CountAsync();
        var items = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
        return (items, total);
    }

    // ✅ فیلتر بازه تاریخی
    public static IQueryable<T> FilterByDateRange<T>(this IQueryable<T> query, DateTime? from, DateTime? to, Expression<Func<T, DateTime>> selector)
    {
        if (from.HasValue)
            query = query.Where(Expression.Lambda<Func<T, bool>>(Expression.GreaterThanOrEqual(selector.Body, Expression.Constant(from.Value)), selector.Parameters));
        if (to.HasValue)
            query = query.Where(Expression.Lambda<Func<T, bool>>(Expression.LessThanOrEqual(selector.Body, Expression.Constant(to.Value)), selector.Parameters));
        return query;
    }

    // ✅ فیلتر IN مشابه SQL
    public static IQueryable<T> WhereIn<T, TValue>(this IQueryable<T> query, Expression<Func<T, TValue>> selector, IEnumerable<TValue> values)
    {
        if (values == null || !values.Any()) return query;
        var parameter = selector.Parameters.Single();
        var body = Expression.Call(
            typeof(Enumerable),
            nameof(Enumerable.Contains),
            new[] { typeof(TValue) },
            Expression.Constant(values),
            selector.Body
        );
        var predicate = Expression.Lambda<Func<T, bool>>(body, parameter);
        return query.Where(predicate);
    }

    // ✅ نسخه ایمن ToListAsync (در صورت خطا لیست خالی)
    public static async Task<List<T>> ToListSafeAsync<T>(this IQueryable<T> query)
    {
        try
        {
            return await query.ToListAsync();
        }
        catch
        {
            return new List<T>();
        }
    }

    // ✅ اعمال مرتب‌سازی و صفحه‌بندی با مدل تنظیمات
    public static IQueryable<T> ApplySortingAndPaging<T>(this IQueryable<T> query, QueryOptions options)
    {
        if (!string.IsNullOrWhiteSpace(options.SortColumn))
            query = query.OrderByDynamic(options.SortColumn, options.SortDescending);

        return query.Page(options.PageIndex, options.PageSize);
    }

    // 🔧 Helper – دریافت DbContext از IQueryable
    private static DbContext GetDbContext<T>(this IQueryable<T> query) where T : class
    {
        if (query is IInfrastructure<IServiceProvider> infrastructure)
        {
            var dependencies = infrastructure.Instance.GetService(typeof(ICurrentDbContext)) as ICurrentDbContext;
            return dependencies?.Context!;
        }
        throw new InvalidOperationException("Cannot retrieve DbContext from IQueryable.");
    }

    public static async Task<PagedList<TDest>> ToPagedList<TDest>
    (this IQueryable<TDest> source, int? page = 1, int? pageSize = 50)
    {
        page ??= 1;
        pageSize ??= 50;

        int count = await source.CountAsync();

        var paginationMetadata = new PagedListInfo
        {
            PageNumber = page.Value,
            TotalCount = count,
            PageSize = pageSize
        };

        int totalPages = (int)Math.Ceiling(count / (double)paginationMetadata!.PageSize);

        paginationMetadata.TotalPages = totalPages;

        var data = await source
            .Skip((page.Value - 1) * paginationMetadata.PageSize.Value)
            .Take(paginationMetadata.PageSize.Value).ToListAsync();

        return new PagedList<TDest>
        {
            List = data,
            Pagination = paginationMetadata
        };
    }


    public static async Task<PagedList<TDest>> ToPagedList<TSource, TDest>
    (this IQueryable<TSource> source, Expression<Func<TSource, TDest>> selector,
        int? page = 1, int? pageSize = 50)
    {
        page ??= 1;
        pageSize ??= 50;

        var count = await source.CountAsync();

        var paginationMetadata = new PagedListInfo
        {
            PageNumber = page.Value,
            TotalCount = count,
            PageSize = pageSize
        };

        var totalPages = (int)Math.Ceiling(count / (double)paginationMetadata!.PageSize);

        paginationMetadata.TotalPages = totalPages;

        var data = await source
            .Select(selector)
            .Skip((page.Value - 1) * paginationMetadata.PageSize.Value)
            .Take(paginationMetadata.PageSize.Value).ToListAsync();

        return new PagedList<TDest>
        {
            List = data.Adapt<List<TDest>>(),
            Pagination = paginationMetadata
        };
    }


    public static async Task<PagedList<TDest>> ToPagedList<TSource, TDest>
    (this IQueryable<TSource> source,
        int? page = 1, int? pageSize = 50)
    {
        page ??= 1;
        pageSize ??= 50;

        var count = await source.CountAsync();

        var paginationMetadata = new PagedListInfo
        {
            PageNumber = page.Value,
            TotalCount = count,
            PageSize = pageSize
        };

        var totalPages = (int)Math.Ceiling(count / (double)paginationMetadata!.PageSize);

        paginationMetadata.TotalPages = totalPages;

        var data = await source
            .Skip((page.Value - 1) * paginationMetadata.PageSize.Value)
            .Take(paginationMetadata.PageSize.Value).ToListAsync();

        return new PagedList<TDest>
        {
            List = data.Adapt<List<TDest>>(),
            Pagination = paginationMetadata
        };
    }




    // 4. Paginate (یا Page)
    public static IQueryable<T> Paginate<T>(this IQueryable<T> source, int pageIndex, int pageSize)
    {
        if (pageIndex < 1) pageIndex = 1;
        if (pageSize < 1) pageSize = 10;
        return source.Skip((pageIndex - 1) * pageSize).Take(pageSize);
    }


    // 7. ToHashSetAsync
    public static async Task<HashSet<T>> ToHashSetAsync<T>(this IQueryable<T> source, IEqualityComparer<T>? comparer = null)
    {
        var list = await source.ToListAsync();
        return new HashSet<T>(list, comparer ?? EqualityComparer<T>.Default);
    }

    // 8. CountAsync (برای وضوح بیشتر – در واقع فقط wrapper است)
    public static Task<int> CountAsync<T>(this IQueryable<T> source)
    {
        return source.CountAsync();
    }




    // 16. BatchDeleteAsync – نیاز به EF Core 7+ یا Z.EntityFramework.Extensions
    // ⚠️ EF Core خالص از حذف گروهی پشتیبانی نمی‌کند (تا نسخه 8 هم ندارد)
    // راه‌حل: استفاده از ExecuteDeleteAsync (EF Core 7+)
    public static async Task<int> BatchDeleteAsync<T>(this IQueryable<T> source) where T : class
    {
        // EF Core 7+ فقط
        return await source.ExecuteDeleteAsync();
    }

  



}
// 📦 مدل تنظیمات برای مرتب‌سازی و صفحه‌بندی
public class QueryOptions
    {
        public string SortColumn { get; set; } = "";
        public bool SortDescending { get; set; }
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

public class PagedResult<T>
{
    public List<T> Items { get; set; } = new();
    public int TotalCount { get; set; }
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
}

