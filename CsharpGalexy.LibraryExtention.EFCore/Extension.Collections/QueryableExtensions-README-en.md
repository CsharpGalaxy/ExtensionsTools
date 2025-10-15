# QueryableExtensions

This documentation describes the `QueryableExtensions` static class which provides common extension methods and helpers for working with LINQ `IQueryable<T>` sequences, especially when using Entity Framework Core.

## Methods & Helpers

| Method | Parameters | Return Type | Description |
|--------|------------|-------------|-------------|
| WhereIf<T> | IQueryable<T> query, bool condition, Expression<Func<T,bool>> predicate | IQueryable<T> | Applies a Where clause only when `condition` is true |
| OrderByDynamic<T> | IQueryable<T> query, string propertyName, bool descending = false | IQueryable<T> | Orders by a property name provided as string (dynamic ordering) |
| ThenByDynamic<T> | IOrderedQueryable<T> query, string propertyName, bool descending = false | IQueryable<T> | Secondary dynamic ordering for already ordered queries |
| Page<T> | IQueryable<T> query, int page, int size | IQueryable<T> | Simple pagination using Skip/Take |
| TakeRandom<T> | IQueryable<T> query, int count | IQueryable<T> | Returns `count` random items (uses Guid.NewGuid ordering) |
| IsEmpty<T> | IQueryable<T> query | bool | Returns true if the query yields no items |
| ToHashSetAsync<T> | IQueryable<T> query | Task<HashSet<T>> | Materializes query and returns HashSet asynchronously |
| CountAsync<T> | IQueryable<T> query, Expression<Func<T,bool>> predicate = null | Task<int> | Counts items, optional predicate overload |
| AnyAsync<T> | IQueryable<T> query, Expression<Func<T,bool>> predicate | Task<bool> | Async Any() wrapper |
| SelectDistinct<T,TResult> | IQueryable<T> query, Expression<Func<T,TResult>> selector | IQueryable<TResult> | Select + Distinct combined |
| IncludeMultiple<T> | IQueryable<T> query, params string[] navigationProperties | IQueryable<T> | Applies multiple string-based `Include()` calls |
| AsNoTracking<T> | IQueryable<T> query | IQueryable<T> | Shortcut for AsNoTracking() |
| IgnoreQueryFilters<T> | IQueryable<T> query | IQueryable<T> | Shortcut for IgnoreQueryFilters() |
| BatchDeleteAsync<T> | IQueryable<T> query, Expression<Func<T,bool>> predicate | Task<int> | Loads matching items and deletes via DbContext.RemoveRange (EF versions without ExecuteDelete) |
| SearchByKeyword<T> | IQueryable<T> query, string keyword, params Expression<Func<T,string>>[] fields | IQueryable<T> | Builds a combined Contains() expression across multiple string fields |
| ToPagedResultAsync<T> | IQueryable<T> query, int pageIndex, int pageSize | Task<(List<T> Items,int TotalCount)> | Simple paged result returning items and total count |
| FilterByDateRange<T> | IQueryable<T> query, DateTime? from, DateTime? to, Expression<Func<T,DateTime>> selector | IQueryable<T> | Filters by a date range using the provided selector expression |
| WhereIn<T,TValue> | IQueryable<T> query, Expression<Func<T,TValue>> selector, IEnumerable<TValue> values | IQueryable<T> | SQL-like IN filter using Enumerable.Contains |
| ToListSafeAsync<T> | IQueryable<T> query | Task<List<T>> | Safe ToListAsync returning empty list on exception |
| ApplySortingAndPaging<T> | IQueryable<T> query, QueryOptions options | IQueryable<T> | Applies dynamic ordering and pagination from `QueryOptions` |
| GetDbContext<T> (private) | IQueryable<T> query | DbContext | Helper to extract `DbContext` from an `IQueryable<T>` (throws if not possible) |
| ToPagedList overloads | IQueryable<TSource> source, [selector?], int? page=1, int? pageSize=50 | Task<PagedList<TDest>> | Multiple overloads producing `PagedList<TDest>` with pagination metadata |
| Paginate<T> | IQueryable<T> source, int pageIndex, int pageSize | IQueryable<T> | Safe pagination (validates pageIndex/pageSize) |
| ToHashSetAsync overload | IQueryable<T> source, IEqualityComparer<T>? comparer = null | Task<HashSet<T>> | Materializes to HashSet with comparer |
| BatchDeleteAsync (EF7+) | IQueryable<T> source | Task<int> | Uses ExecuteDeleteAsync when available (EF Core 7+)

## Types defined in file

| Type | Description |
|------|-------------|
| QueryOptions | Simple DTO for SortColumn, SortDescending, PageIndex, PageSize |
| PagedResult<T> | Simple paged result model (Items, TotalCount, PageIndex, PageSize, TotalPages) |

## Usage Examples

### Conditional Where
```csharp
var q = dbContext.Users.WhereIf(includeInactive, u => u.IsActive == false);
```

### Dynamic Ordering
```csharp
var ordered = users.OrderByDynamic("LastName", descending: true);
var thenOrdered = ((IOrderedQueryable<User>)ordered).ThenByDynamic("CreatedAt");
```

### Pagination + Sorting from options
```csharp
var options = new QueryOptions { SortColumn = "Name", PageIndex = 2, PageSize = 20 };
var result = dbContext.Products.ApplySortingAndPaging(options).ToList();
```

### Safe materialization
```csharp
var list = await query.ToListSafeAsync();
```

### PagedList with selector
```csharp
var page = await dbContext.Orders.ToPagedList(o => new OrderDto { Id = o.Id, Total = o.Total }, page: 1, pageSize: 25);
Console.WriteLine(page.Pagination.TotalCount);
```

## Important Notes

- Many methods rely on EF Core extension methods (ToListAsync, CountAsync, ExecuteDeleteAsync, Include, AsNoTracking, IgnoreQueryFilters). Ensure EF Core is referenced.
- `WhereIn` compiles to Enumerable.Contains which is translatable by EF Core when used with in-memory or proper providers.
- `TakeRandom` uses Guid.NewGuid ordering — this is not efficient on large datasets and may not be translated to SQL by some providers.
- `GetDbContext` uses internal EF Core infrastructure to obtain DbContext and will throw if IQueryable isn't backed by EF Core provider.
- `BatchDeleteAsync` has two approaches in file: a safe fallback (load & RemoveRange) and an EF7+ optimized ExecuteDeleteAsync implementation.

## Performance & Translation

- Prefer server-side operations that EF Core can translate to SQL. Methods that force client evaluation (e.g., Guid ordering, in-memory projections) should be used with care on large datasets.
- Use `AsNoTracking()` when only reading data to improve performance.

## Dependencies

- Microsoft.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.Infrastructure
- Microsoft.EntityFrameworkCore.Query
- Mapster (for Adapt mapping)
- TeamLibrary.API.Shared.PagedList or the project's PagedList models
- System.Linq.Expressions

## See also

- EF Core docs for Query Types, Translation, and Performance
- Mapster documentation for efficient projection mapping