# QueryableExtensions

این مستندات مربوط به کلاس static `QueryableExtensions` است که متدها و ابزارهای رایجی برای کار با `IQueryable<T>` و به‌ویژه در کنار Entity Framework Core فراهم می‌کند.

## متدها و ابزارها

| متد | پارامترها | نوع خروجی | توضیحات |
|-----|-----------|------------|----------|
| WhereIf<T> | IQueryable<T> query, bool condition, Expression<Func<T,bool>> predicate | IQueryable<T> | اعمال Where تنها وقتی که `condition` درست باشد |
| OrderByDynamic<T> | IQueryable<T> query, string propertyName, bool descending = false | IQueryable<T> | مرتب‌سازی داینامیک براساس نام پراپرتی (رشته) |
| ThenByDynamic<T> | IOrderedQueryable<T> query, string propertyName, bool descending = false | IQueryable<T> | مرتب‌سازی ثانویه داینامیک |
| Page<T> | IQueryable<T> query, int page, int size | IQueryable<T> | صفحه‌بندی ساده با Skip/Take |
| TakeRandom<T> | IQueryable<T> query, int count | IQueryable<T> | انتخاب تصادفی `count` آیتم (با OrderBy Guid) |
| IsEmpty<T> | IQueryable<T> query | bool | بررسی سریع خالی بودن نتیجه |
| ToHashSetAsync<T> | IQueryable<T> query | Task<HashSet<T>> | اجرای query و بازگرداندن HashSet به‌صورت async |
| CountAsync<T> | IQueryable<T> query, Expression<Func<T,bool>> predicate = null | Task<int> | شمارش آیتم‌ها با اورلود اختیاری predicate |
| AnyAsync<T> | IQueryable<T> query, Expression<Func<T,bool>> predicate | Task<bool> | اورلود ساده Async Any() |
| SelectDistinct<T,TResult> | IQueryable<T> query, Expression<Func<T,TResult>> selector | IQueryable<TResult> | انتخاب و حذف تکراری‌ها |
| IncludeMultiple<T> | IQueryable<T> query, params string[] navigationProperties | IQueryable<T> | بارگزاری چندین Include بر پایه رشته |
| AsNoTracking<T> | IQueryable<T> query | IQueryable<T> | اورلود کوتاه‌تر AsNoTracking() |
| IgnoreQueryFilters<T> | IQueryable<T> query | IQueryable<T> | اورلود کوتاه‌تر IgnoreQueryFilters() |
| BatchDeleteAsync<T> | IQueryable<T> query, Expression<Func<T,bool>> predicate | Task<int> | حذف گروهی با بارگذاری و RemoveRange (فِال‌بَک برای EF قدیمی) |
| SearchByKeyword<T> | IQueryable<T> query, string keyword, params Expression<Func<T,string>>[] fields | IQueryable<T> | جستجوی داینامیک با Contains روی چند فیلد متنی |
| ToPagedResultAsync<T> | IQueryable<T> query, int pageIndex, int pageSize | Task<(List<T> Items,int TotalCount)> | بازگرداندن آیتم‌ها و تعداد کل برای صفحه‌بندی ساده |
| FilterByDateRange<T> | IQueryable<T> query, DateTime? from, DateTime? to, Expression<Func<T,DateTime>> selector | IQueryable<T> | فیلتر بازه تاریخ بر اساس سلکتور داده شده |
| WhereIn<T,TValue> | IQueryable<T> query, Expression<Func<T,TValue>> selector, IEnumerable<TValue> values | IQueryable<T> | فیلتر IN مشابه SQL با Enumerable.Contains |
| ToListSafeAsync<T> | IQueryable<T> query | Task<List<T>> | ToListAsync ایمن که در صورت خطا لیست خالی برمی‌گرداند |
| ApplySortingAndPaging<T> | IQueryable<T> query, QueryOptions options | IQueryable<T> | اعمال مرتب‌سازی داینامیک و صفحه‌بندی از `QueryOptions` |
| GetDbContext<T> (private) | IQueryable<T> query | DbContext | دریافت DbContext از IQueryable (اگر ممکن نباشد استثنا پرتاب می‌کند) |
| ToPagedList overloads | IQueryable<TSource> source, [selector?], int? page=1, int? pageSize=50 | Task<PagedList<TDest>> | چندین اورلود که `PagedList<TDest>` با metadata صفحه‌بندی برمی‌گردانند |
| Paginate<T> | IQueryable<T> source, int pageIndex, int pageSize | IQueryable<T> | صفحه‌بندی ایمن (مقادیر نامعتبر را تصحیح می‌کند) |
| ToHashSetAsync overload | IQueryable<T> source, IEqualityComparer<T>? comparer = null | Task<HashSet<T>> | materialize به HashSet با comparer |
| BatchDeleteAsync (EF7+) | IQueryable<T> source | Task<int> | استفاده از ExecuteDeleteAsync در EF Core 7+ |

## انواع تعریف‌شده در فایل

| نوع | توضیحات |
|-----|----------|
| QueryOptions | DTO ساده برای SortColumn, SortDescending, PageIndex, PageSize |
| PagedResult<T> | مدل صفحه‌بندی ساده (Items, TotalCount, PageIndex, PageSize, TotalPages) |

## مثال‌های استفاده

### Where شرطی
```csharp
var q = dbContext.Users.WhereIf(includeInactive, u => u.IsActive == false);
```

### مرتب‌سازی داینامیک
```csharp
var ordered = users.OrderByDynamic("LastName", descending: true);
var thenOrdered = ((IOrderedQueryable<User>)ordered).ThenByDynamic("CreatedAt");
```

### صفحه‌بندی و مرتب‌سازی بر اساس تنظیمات
```csharp
var options = new QueryOptions { SortColumn = "Name", PageIndex = 2, PageSize = 20 };
var result = dbContext.Products.ApplySortingAndPaging(options).ToList();
```

### اجرای ایمن
```csharp
var list = await query.ToListSafeAsync();
```

### PagedList با selector
```csharp
var page = await dbContext.Orders.ToPagedList(o => new OrderDto { Id = o.Id, Total = o.Total }, page: 1, pageSize: 25);
Console.WriteLine(page.Pagination.TotalCount);
```

## نکات مهم

- بسیاری از متدها به متدهای EF Core وابسته‌اند (ToListAsync، CountAsync، ExecuteDeleteAsync، Include، AsNoTracking، IgnoreQueryFilters). مطمئن شوید EF Core را ارجاع داده‌اید.
- `WhereIn` به Enumerable.Contains تکیه دارد که توسط EF Core در شرایط مناسب ترجمه می‌شود.
- `TakeRandom` از OrderBy Guid استفاده می‌کند — ناکارا برای داده‌های بزرگ و ممکن است توسط بعضی providerها به SQL ترجمه نشود.
- `GetDbContext` از زیرساخت داخلی EF Core برای به‌دست‌آوردن DbContext استفاده می‌کند و اگر IQueryable از EF Core پشتیبانی نکند خطا پرتاب می‌کند.
- `BatchDeleteAsync` در فایل دو رویکرد دارد: فِال‌بَک ایمن (بارگذاری و حذف) و اجرای بهینه ExecuteDeleteAsync برای EF7+

## کارایی و ترجمه

- عملیات سمت سرور که توسط EF Core به SQL ترجمه می‌شوند را ترجیح دهید. متدهایی که به ارزیابی در سمت کلاینت منجر می‌شوند (مثل OrderBy Guid یا projectionهای in-memory) را با احتیاط روی مجموعه‌های بزرگ استفاده کنید.
- هنگام خواندن فقط، از `AsNoTracking()` استفاده کنید تا کارایی بهتر شود.

## وابستگی‌ها

- Microsoft.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.Infrastructure
- Microsoft.EntityFrameworkCore.Query
- Mapster (برای Adapt/Map)
- TeamLibrary.API.Shared.PagedList یا مدل‌های PagedList پروژه
- System.Linq.Expressions

## منابع

- مستندات EF Core درباره Query Translation و Performance
- مستندات Mapster برای نگاشت مؤثر