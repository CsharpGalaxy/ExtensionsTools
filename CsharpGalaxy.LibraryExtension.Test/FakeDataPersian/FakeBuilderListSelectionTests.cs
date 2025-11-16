using CsharpGalaxy.LibraryExtension.FakeDataPersian.Abstracts;
using CsharpGalaxy.LibraryExtension.FakeDataPersian.Generators;

namespace CsharpGalaxy.LibraryExtension.Test.FakeDataPersian;

/// <summary>
/// تست‌های RuleForListSelection برای انتخاب رندوم از لیست
/// </summary>
public class FakeBuilderListSelectionTests
{
    // مدل‌های تستی
    public class Product
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public int Stock { get; set; }
    }

    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string Role { get; set; }
        public int Level { get; set; }
    }

    /// <summary>
    /// Test: RuleForListSelection with params should select random value
    /// </summary>
    [Fact]
    public void RuleForListSelection_WithParams_ShouldSelectRandomValue()
    {
        var colors = new[] { "قرمز", "آبی", "سبز", "زرد" };

        var product = new FakeBuilder<Product>()
            .RuleFor(x => x.Id, () => Guid.NewGuid().ToString())
            .RuleFor(x => x.Name, () => "محصول")
            .RuleForListSelection(x => x.Color, colors)
            .Build();

        Assert.NotNull(product.Color);
        Assert.Contains(product.Color, colors);
    }

    /// <summary>
    /// Test: RuleForListSelection with IEnumerable should select random value
    /// </summary>
    [Fact]
    public void RuleForListSelection_WithIEnumerable_ShouldSelectRandomValue()
    {
        var sizes = new List<string> { "کوچک", "متوسط", "بزرگ", "خیلی بزرگ" };

        var product = new FakeBuilder<Product>()
            .RuleFor(x => x.Id, () => Guid.NewGuid().ToString())
            .RuleFor(x => x.Name, () => "محصول")
            .RuleForListSelection(x => x.Size, sizes)
            .Build();

        Assert.NotNull(product.Size);
        Assert.Contains(product.Size, sizes);
    }

    /// <summary>
    /// Test: Multiple RuleForListSelection should all work
    /// </summary>
    [Fact]
    public void MultipleRuleForListSelection_ShouldAllWork()
    {
        var colors = new[] { "قرمز", "آبی", "سبز" };
        var sizes = new[] { "S", "M", "L", "XL" };

        var product = new FakeBuilder<Product>()
            .RuleFor(x => x.Id, () => Guid.NewGuid().ToString())
            .RuleFor(x => x.Name, () => "محصول")
            .RuleForListSelection(x => x.Color, colors)
            .RuleForListSelection(x => x.Size, sizes)
            .Build();

        Assert.Contains(product.Color, colors);
        Assert.Contains(product.Size, sizes);
    }

    /// <summary>
    /// Test: RuleForListSelection with BuildList should distribute values
    /// </summary>
    [Fact]
    public void RuleForListSelection_WithBuildList_ShouldDistributeRandomly()
    {
        var colors = new[] { "قرمز", "آبی", "سبز" };

        var products = new FakeBuilder<Product>()
            .RuleFor(x => x.Id, () => Guid.NewGuid().ToString())
            .RuleFor(x => x.Name, () => "محصول")
            .RuleForListSelection(x => x.Color, colors)
            .BuildList(30);

        Assert.Equal(30, products.Count);
        Assert.All(products, p => Assert.Contains(p.Color, colors));

        // باید حداقل ۲ رنگ مختلف داشته باشیم
        var uniqueColors = products.Select(p => p.Color).Distinct().Count();
        Assert.True(uniqueColors > 1);
    }

    /// <summary>
    /// Test: RuleForListSelection with string values
    /// </summary>
    [Fact]
    public void RuleForListSelection_WithStringValues_ShouldWork()
    {
        var statuses = new[] { "فعال", "غیرفعال", "معلق", "حذف‌شده" };

        var user = new FakeBuilder<User>()
            .RuleFor(x => x.Id, () => Guid.NewGuid().ToString())
            .RuleFor(x => x.Name, () => PersianNameGenerator.FullName())
            .RuleForListSelection(x => x.Status, statuses)
            .Build();

        Assert.Contains(user.Status, statuses);
    }

    /// <summary>
    /// Test: RuleForListSelection with int values
    /// </summary>
    [Fact]
    public void RuleForListSelection_WithIntValues_ShouldWork()
    {
        var levels = new[] { 1, 2, 3, 4, 5 };

        var user = new FakeBuilder<User>()
            .RuleFor(x => x.Id, () => Guid.NewGuid().ToString())
            .RuleFor(x => x.Name, () => "کاربر")
            .RuleFor(x => x.Status, () => "فعال")
            .RuleForListSelection(x => x.Level, levels)
            .Build();

        Assert.Contains(user.Level, levels);
    }

    /// <summary>
    /// Test: RuleForListSelection should override attribute
    /// </summary>
    [Fact]
    public void RuleForListSelection_ShouldOverrideAttribute()
    {
        var roles = new[] { "Admin", "User", "Guest" };

        var user = new FakeBuilder<User>()
            .RuleFor(x => x.Id, () => Guid.NewGuid().ToString())
            .RuleFor(x => x.Name, () => "کاربر")
            .RuleFor(x => x.Status, () => "فعال")
            .RuleForListSelection(x => x.Role, roles)
            .Build();

        Assert.Contains(user.Role, roles);
    }

    /// <summary>
    /// Test: RuleForListSelection with single item should always return that item
    /// </summary>
    [Fact]
    public void RuleForListSelection_WithSingleItem_ShouldAlwaysReturnIt()
    {
        var users = new FakeBuilder<User>()
            .RuleFor(x => x.Id, () => Guid.NewGuid().ToString())
            .RuleFor(x => x.Name, () => "کاربر")
            .RuleForListSelection(x => x.Status, new[] { "فعال" })
            .BuildList(10);

        Assert.All(users, u => Assert.Equal("فعال", u.Status));
    }

    /// <summary>
    /// Test: RuleForListSelection with empty list should throw
    /// </summary>
    [Fact]
    public void RuleForListSelection_WithEmptyList_ShouldThrow()
    {
        var emptyList = new List<string>();

        Assert.Throws<ArgumentException>(() =>
        {
            new FakeBuilder<Product>()
                .RuleFor(x => x.Id, () => Guid.NewGuid().ToString())
                .RuleForListSelection(x => x.Color, emptyList)
                .Build();
        });
    }

    /// <summary>
    /// Test: RuleForListSelection with params and empty array should throw
    /// </summary>
    [Fact]
    public void RuleForListSelection_WithEmptyParams_ShouldThrow()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            new FakeBuilder<Product>()
                .RuleFor(x => x.Id, () => Guid.NewGuid().ToString())
                .RuleForListSelection(x => x.Color)
                .Build();
        });
    }

    /// <summary>
    /// Test: RuleForListSelection with mixed types
    /// </summary>
    [Fact]
    public void RuleForListSelection_WithMixedTypes_ShouldWork()
    {
        var colors = new[] { "قرمز", "آبی", "سبز" };
        var sizes = new[] { "کوچک", "متوسط", "بزرگ" };
        var stocks = new[] { 10, 20, 50, 100 };

        var product = new FakeBuilder<Product>()
            .RuleFor(x => x.Id, () => Guid.NewGuid().ToString())
            .RuleFor(x => x.Name, () => "محصول")
            .RuleForListSelection(x => x.Color, colors)
            .RuleForListSelection(x => x.Size, sizes)
            .RuleForListSelection(x => x.Stock, stocks)
            .Build();

        Assert.Contains(product.Color, colors);
        Assert.Contains(product.Size, sizes);
        Assert.Contains(product.Stock, stocks);
    }

    /// <summary>
    /// Test: RuleForListSelection consistency in list
    /// </summary>
    [Fact]
    public void RuleForListSelection_ConsistencyInList_ShouldHaveVariety()
    {
        var colors = new[] { "قرمز", "آبی", "سبز", "زرد", "مشکی" };

        var products = new FakeBuilder<Product>()
            .RuleFor(x => x.Id, () => Guid.NewGuid().ToString())
            .RuleFor(x => x.Name, () => "محصول")
            .RuleForListSelection(x => x.Color, colors)
            .BuildList(100);

        var uniqueColors = products.Select(p => p.Color).Distinct().Count();
        
        // باید حداقل ۳ رنگ مختلف داشته باشیم
        Assert.True(uniqueColors >= 3, $"Expected at least 3 unique colors, got {uniqueColors}");
    }

    /// <summary>
    /// Test: RuleForListSelection with Persian string values
    /// </summary>
    [Fact]
    public void RuleForListSelection_WithPersianStrings_ShouldWork()
    {
        var persianStatus = new[] { "درحال‌انجام", "تکمیل‌شده", "لغو‌شده", "متوقف" };

        var users = new FakeBuilder<User>()
            .RuleFor(x => x.Id, () => Guid.NewGuid().ToString())
            .RuleFor(x => x.Name, () => PersianNameGenerator.FullName())
            .RuleForListSelection(x => x.Status, persianStatus)
            .BuildList(5);

        Assert.All(users, u => Assert.Contains(u.Status, persianStatus));
    }

    /// <summary>
    /// Test: RuleForListSelection with large list
    /// </summary>
    [Fact]
    public void RuleForListSelection_WithLargeList_ShouldWork()
    {
        var largeList = Enumerable.Range(1, 1000).Select(i => $"Item-{i}").ToList();

        var product = new FakeBuilder<Product>()
            .RuleFor(x => x.Id, () => Guid.NewGuid().ToString())
            .RuleFor(x => x.Name, () => "محصول")
            .RuleForListSelection(x => x.Color, largeList)
            .Build();

        Assert.Contains(product.Color, largeList);
    }

    /// <summary>
    /// Test: Chaining RuleFor with RuleForListSelection
    /// </summary>
    [Fact]
    public void RuleFor_WithRuleForListSelection_ShouldChainCorrectly()
    {
        var colors = new[] { "قرمز", "آبی", "سبز" };
        var sizes = new[] { "S", "M", "L" };

        var product = new FakeBuilder<Product>()
            .RuleFor(x => x.Id, () => Guid.NewGuid().ToString())
            .RuleFor(x => x.Name, () => "محصول خاص")
            .RuleForListSelection(x => x.Color, colors)
            .RuleFor(x => x.Stock, () => 100)
            .RuleForListSelection(x => x.Size, sizes)
            .Build();

        Assert.Equal("محصول خاص", product.Name);
        Assert.Contains(product.Color, colors);
        Assert.Equal(100, product.Stock);
        Assert.Contains(product.Size, sizes);
    }
}
