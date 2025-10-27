using CsharpGalexy.LibraryExtention.Helpers.Collections;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CsharpGalexy.LibraryExtention.Tests
{
    public class QueryableExtensionsTests
    {
        private DbContextOptions<TestDbContext> CreateNewContextOptions()
        {
            return new DbContextOptionsBuilder<TestDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()) // هر تست یک دیتابیس جداگانه
                .Options;
        }

        private async Task<List<TestEntity>> SeedDataAsync(TestDbContext context)
        {
            var data = new List<TestEntity>
            {
                new() { Id = 1, Name = "Alpha", CreatedAt = new DateTime(2023, 1, 1), Category = "A" },
                new() { Id = 2, Name = "Beta", CreatedAt = new DateTime(2023, 2, 1), Category = "B" },
                new() { Id = 3, Name = "Gamma", CreatedAt = new DateTime(2023, 3, 1), Category = "A" },
                new() { Id = 4, Name = "Delta", CreatedAt = new DateTime(2023, 4, 1), Category = "C" }
            };
            context.Entities.AddRange(data);
            await context.SaveChangesAsync();
            return data;
        }

        [Fact]
        public async Task WhereIf_ConditionTrue_ShouldApplyFilter()
        {
            using var context = new TestDbContext(CreateNewContextOptions());
            await SeedDataAsync(context);

            var result = await context.Entities
                .WhereIf(true, x => x.Category == "A")
                .ToListAsync();

            Assert.Equal(2, result.Count);
            Assert.All(result, x => Assert.Equal("A", x.Category));
        }

        [Fact]
        public async Task WhereIf_ConditionFalse_ShouldNotApplyFilter()
        {
            using var context = new TestDbContext(CreateNewContextOptions());
            await SeedDataAsync(context);

            var result = await context.Entities
                .WhereIf(false, x => x.Category == "A")
                .ToListAsync();

            Assert.Equal(4, result.Count);
        }

        [Fact]
        public async Task OrderByDynamic_Descending_ShouldSortCorrectly()
        {
            using var context = new TestDbContext(CreateNewContextOptions());
            await SeedDataAsync(context);

            var result = await context.Entities
                .OrderByDynamic("Name", descending: true)
                .ToListAsync();

            Assert.Equal(new[] { "Gamma", "Delta", "Beta", "Alpha" }, result.Select(x => x.Name));
        }

        [Fact]
        public async Task Page_ShouldReturnCorrectPage()
        {
            using var context = new TestDbContext(CreateNewContextOptions());
            await SeedDataAsync(context);

            var result = await context.Entities
                .OrderBy(x => x.Id)
                .Page(page: 2, size: 2)
                .ToListAsync();

            Assert.Equal(2, result.Count);
            Assert.Equal(3, result[0].Id);
            Assert.Equal(4, result[1].Id);
        }

        [Fact]
        public async Task IsEmpty_EmptyQuery_ReturnsTrue()
        {
            using var context = new TestDbContext(CreateNewContextOptions());
            // بدون داده

            var result = context.Entities.Where(x => x.Id > 100).IsEmpty();
            Assert.True(result);
        }

        [Fact]
        public async Task ToHashSetAsync_ShouldReturnHashSet()
        {
            using var context = new TestDbContext(CreateNewContextOptions());
            await SeedDataAsync(context);

            var result = await context.Entities.ToHashSetAsync();
            Assert.IsType<HashSet<TestEntity>>(result);
            Assert.Equal(4, result.Count);
        }

        [Fact]
        public async Task CountAsync_WithPredicate_ShouldCountCorrectly()
        {
            using var context = new TestDbContext(CreateNewContextOptions());
            await SeedDataAsync(context);

            var count = await context.Entities.CountAsync(x => x.Category == "A");
            Assert.Equal(2, count);
        }

        [Fact]
        public async Task SearchByKeyword_ShouldMatchAnyField()
        {
            using var context = new TestDbContext(CreateNewContextOptions());
            await SeedDataAsync(context);

            var result = await context.Entities
                .SearchByKeyword("mm", x => x.Name)
                .ToListAsync();

            Assert.Single(result);
            Assert.Equal("Gamma", result[0].Name);
        }

        [Fact]
        public async Task FilterByDateRange_ShouldFilterCorrectly()
        {
            using var context = new TestDbContext(CreateNewContextOptions());
            await SeedDataAsync(context);

            var from = new DateTime(2023, 2, 1);
            var to = new DateTime(2023, 3, 15);

            var result = await context.Entities
                .FilterByDateRange(from, to, x => x.CreatedAt)
                .ToListAsync();

            Assert.Equal(2, result.Count);
            Assert.Contains(result, x => x.Id == 2);
            Assert.Contains(result, x => x.Id == 3);
        }
        [Fact]
        public async Task Paginate_ShouldReturnCorrectPage()
        {
            using var context = new TestDbContext(CreateNewContextOptions());
            await SeedDataAsync(context);

            var result = await context.Entities
                .OrderBy(x => x.Id)
                .Paginate(pageIndex: 2, pageSize: 2)
                .ToListAsync();

            Assert.Equal(2, result.Count);
            Assert.Equal(3, result[0].Id);
            Assert.Equal(4, result[1].Id);
        }
        [Fact]
        public async Task IncludeMultiple_ShouldIncludeNavigationProperties()
        {
            using var context = new TestDbContext(CreateNewContextOptions());

            var entity = new TestEntity
            {
                Name = "Alpha",
                Tags = new List<Tag> { new Tag { Label = "Important" } }
            };

            context.Entities.Add(entity);
            await context.SaveChangesAsync();

            var result = await context.Entities
                .IncludeMultiple(nameof(TestEntity.Tags))
                .FirstOrDefaultAsync(x => x.Name == "Alpha");

            Assert.NotNull(result);
            Assert.NotNull(result.Tags);
            Assert.Single(result.Tags);
            Assert.Equal("Important", result.Tags[0].Label);
        }
        [Fact]
        public async Task AnyAsync_ShouldReturnTrueIfMatchExists()
        {
            using var context = new TestDbContext(CreateNewContextOptions());
            await SeedDataAsync(context);

            var exists = await context.Entities.AnyAsync(x => x.Category == "C");

            Assert.True(exists);
        }
        [Fact]
        public async Task SelectDistinct_ShouldReturnUniqueValues()
        {
            using var context = new TestDbContext(CreateNewContextOptions());
            await SeedDataAsync(context);

            var distinctCategories = await context.Entities
                .SelectDistinct(x => x.Category)
                .ToListAsync();

            Assert.Equal(3, distinctCategories.Count); // A, B, C
        }
        [Fact]
        public async Task ThenByDynamic_ShouldApplySecondarySorting()
        {
            using var context = new TestDbContext(CreateNewContextOptions());
            await SeedDataAsync(context);

            var result = await context.Entities.AsQueryable().OrderByDescending(_=>_.Id)
                .ThenByDynamic("Name", true)
                .ToListAsync();

            var expected = new[] { "Gamma", "Alpha", "Beta", "Delta" };
            Assert.Equal(expected, result.Select(x => x.Name));
        }

        [Fact]
        public async Task WhereIn_ShouldFilterByValues()
        {
            using var context = new TestDbContext(CreateNewContextOptions());
            await SeedDataAsync(context);

            var categories = new[] { "A", "C" };
            var result = await context.Entities
                .WhereIn(x => x.Category, categories)
                .ToListAsync();

            Assert.Equal(3, result.Count);
            Assert.All(result, x => Assert.Contains(x.Category, categories));
        }

        [Fact]
        public async Task ApplySortingAndPaging_ShouldWork()
        {
            using var context = new TestDbContext(CreateNewContextOptions());
            await SeedDataAsync(context);

            var options = new QueryOptions
            {
                SortColumn = "Name",
                SortDescending = false,
                PageIndex = 1,
                PageSize = 2
            };

            var result = await context.Entities
                .ApplySortingAndPaging(options)
                .ToListAsync();

            Assert.Equal(2, result.Count);
            Assert.Equal("Alpha", result[0].Name);
            Assert.Equal("Beta", result[1].Name);
        }
        [Fact]
        public async Task ToPagedList_TDest_ShouldReturnCorrectMetadata()
        {
            using var context = new TestDbContext(CreateNewContextOptions());
            await SeedDataAsync(context);

            var paged = await context.Entities.ToPagedList(page: 2, pageSize: 2);

            Assert.Equal(2, paged.List.Count);
            Assert.Equal(4, paged.Pagination.TotalCount);
            Assert.Equal(2, paged.Pagination.PageNumber);
            Assert.Equal(2, paged.Pagination.PageSize);
            Assert.Equal(2, paged.Pagination.TotalPages);
        }
        [Fact]
        public async Task ToPagedList_Adapted_ShouldReturnAdaptedListWithMetadata()
        {
            using var context = new TestDbContext(CreateNewContextOptions());
            await SeedDataAsync(context);

            var paged = await context.Entities
                .ToPagedList<TestEntity, TestEntity>(page: 2, pageSize: 2);

            Assert.Equal(2, paged.List.Count);
            Assert.Equal("Gamma", paged.List[0].Name);
            Assert.Equal("Delta", paged.List[1].Name);
            Assert.Equal(4, paged.Pagination.TotalCount);
            Assert.Equal(2, paged.Pagination.TotalPages);
        }


        [Fact]
        public async Task ToPagedList_WithSelector_ShouldProjectAndPaginateCorrectly()
        {
            using var context = new TestDbContext(CreateNewContextOptions());
            await SeedDataAsync(context);

            var paged = await context.Entities
                .ToPagedList(x => new { x.Name }, page: 1, pageSize: 2);

            Assert.Equal(2, paged.List.Count);
            Assert.Equal("Alpha", paged.List[0].Name);
            Assert.Equal("Beta", paged.List[1].Name);
            Assert.Equal(4, paged.Pagination.TotalCount);
            Assert.Equal(2, paged.Pagination.TotalPages);
        }

        [Fact]
        public async Task ToPagedList_ShouldReturnCorrectMetadata()
        {
            using var context = new TestDbContext(CreateNewContextOptions());
            await SeedDataAsync(context);

            var paged = await context.Entities.ToPagedList(_ => new TestEntity2
            {
                Name = _.Name
            }, page: 2, pageSize: 2);

            Assert.Equal(2, paged.List.Count);
            Assert.Equal(4, paged.Pagination.TotalCount);
            Assert.Equal(2, paged.Pagination.PageNumber);
            Assert.Equal(2, paged.Pagination.PageSize);
            Assert.Equal(2, paged.Pagination.TotalPages);
        }
    }

    // DbContext آزمایشی
    public class TestDbContext : DbContext
    {
        public TestDbContext(DbContextOptions<TestDbContext> options) : base(options) { }
        public DbSet<TestEntity> Entities => Set<TestEntity>();
    }
}
public class TestEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public string Category { get; set; } = string.Empty;
    public List<Tag> Tags { get; set; } = new();
}
public class TestEntity2
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public string Category { get; set; } = string.Empty;

}
public class Tag
{
    public int Id { get; set; }
    public string Label { get; set; } = "";
}

