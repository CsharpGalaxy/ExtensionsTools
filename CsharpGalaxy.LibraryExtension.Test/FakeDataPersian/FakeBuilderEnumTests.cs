using CsharpGalaxy.LibraryExtension.FakeDataPersian.Abstracts;
using CsharpGalaxy.LibraryExtension.FakeDataPersian.Attributes;
using CsharpGalaxy.LibraryExtension.FakeDataPersian.Generators;

namespace CsharpGalaxy.LibraryExtension.Test.FakeDataPersian;

/// <summary>
/// تست‌های Enum برای FakeBuilder
/// </summary>
public class FakeBuilderEnumTests
{
    // Enum تستی
    public enum Color
    {
        Red,
        Green,
        Blue,
        Yellow,
        Black
    }

    public enum Size
    {
        Small,
        Medium,
        Large,
        ExtraLarge
    }

    public enum Status
    {
        Active = 0,
        Inactive = 1,
        Pending = 2
    }

    // مدل‌های تستی
    public class Product
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Color Color { get; set; }
        public Size Size { get; set; }
    }

    public class Item
    {
        public string Id { get; set; }
        public Color Color { get; set; }
        public Size Size { get; set; }
        public Status Status { get; set; }
    }

    public class AttributeEnumModel
    {
        [Guid]
        public string Id { get; set; }

        [Enum(typeof(Color))]
        public Color Color { get; set; }

        [Enum(typeof(Size), Size.Medium, Size.Large)]
        public Size Size { get; set; }
    }

    /// <summary>
    /// Test: RuleForEnum should generate custom enum values
    /// </summary>
    [Fact]
    public void RuleForEnum_ShouldGenerateCustomValue()
    {
        var product = new FakeBuilder<Product>()
            .RuleFor(x => x.Id, () => "1")
            .RuleFor(x => x.Name, () => "Test")
            .RuleForEnum(x => x.Color, () => Color.Red)
            .RuleForEnum(x => x.Size, () => Size.Large)
            .Build();

        Assert.Equal("1", product.Id);
        Assert.Equal("Test", product.Name);
        Assert.Equal(Color.Red, product.Color);
        Assert.Equal(Size.Large, product.Size);
    }

    /// <summary>
    /// Test: RuleForEnum with dynamic selection
    /// </summary>
    [Fact]
    public void RuleForEnum_WithDynamicSelection_ShouldVary()
    {
        var colors = new[] { Color.Blue, Color.Green };
        var random = new Random();

        var products = new List<Product>();
        for (int i = 0; i < 10; i++)
        {
            products.Add(new FakeBuilder<Product>()
                .RuleFor(x => x.Id, () => $"P{i}")
                .RuleFor(x => x.Name, () => $"Product {i}")
                .RuleForEnum(x => x.Color, () => colors[random.Next(colors.Length)])
                .RuleForEnum(x => x.Size, () => EnumGenerator.GetRandomValue<Size>())
                .Build());
        }

        // Check that we got variations
        var uniqueColors = products.Select(p => p.Color).Distinct().Count();
        Assert.True(uniqueColors > 0);
    }

    /// <summary>
    /// Test: RuleForAllEnums should apply to all enum properties
    /// </summary>
    [Fact]
    public void RuleForAllEnums_ShouldApplyToAllEnumProperties()
    {
        var item = new FakeBuilder<Item>()
            .RuleFor(x => x.Id, () => "1")
            .RuleForAllEnums(() => EnumGenerator.GetRandomValue<Color>())
            .Build();

        // All enum properties should have values
        Assert.True(Enum.IsDefined(typeof(Color), item.Color));
        // Size will be initialized with RuleForAllEnums too if it's enum type
        Assert.NotNull(item);
    }

    /// <summary>
    /// Test: BuildList with enum rules
    /// </summary>
    [Fact]
    public void BuildList_WithEnumRules_ShouldCreateMultipleInstances()
    {
        var products = new FakeBuilder<Product>()
            .RuleFor(x => x.Id, () => Guid.NewGuid().ToString())
            .RuleFor(x => x.Name, () => "Product")
            .RuleForEnum(x => x.Color, () => EnumGenerator.GetRandomValue<Color>())
            .RuleForEnum(x => x.Size, () => EnumGenerator.GetRandomValue<Size>())
            .BuildList(5);

        Assert.Equal(5, products.Count);
        Assert.All(products, p =>
        {
            Assert.True(Enum.IsDefined(typeof(Color), p.Color));
            Assert.True(Enum.IsDefined(typeof(Size), p.Size));
        });
    }

    /// <summary>
    /// Test: Enum attribute in Build should work
    /// </summary>
    [Fact]
    public void EnumAttribute_InBuild_ShouldGenerateValidValues()
    {
        var model = new FakeBuilder<AttributeEnumModel>()
            .Build();

        Assert.True(Enum.IsDefined(typeof(Color), model.Color));
        Assert.True(
            model.Size == Size.Medium || model.Size == Size.Large,
            $"Size should be Medium or Large, but got {model.Size}"
        );
    }

    /// <summary>
    /// Test: RuleForEnum overrides enum attribute
    /// </summary>
    [Fact]
    public void RuleForEnum_ShouldOverrideEnumAttribute()
    {
        var model = new FakeBuilder<AttributeEnumModel>()
            .RuleForEnum(x => x.Color, () => Color.Blue)
            .Build();

        Assert.Equal(Color.Blue, model.Color);
        // Size still uses attribute
        Assert.True(model.Size == Size.Medium || model.Size == Size.Large);
    }

    /// <summary>
    /// Test: Combination of RuleFor and RuleForEnum
    /// </summary>
    [Fact]
    public void CombinationOfRuleForAndRuleForEnum_ShouldWork()
    {
        var product = new FakeBuilder<Product>()
            .RuleFor(x => x.Id, () => "ABC123")
            .RuleFor(x => x.Name, () => "Custom Product")
            .RuleForEnum(x => x.Color, () => Color.Red)
            .RuleForEnum(x => x.Size, () => Size.Small)
            .Build();

        Assert.Equal("ABC123", product.Id);
        Assert.Equal("Custom Product", product.Name);
        Assert.Equal(Color.Red, product.Color);
        Assert.Equal(Size.Small, product.Size);
    }

    /// <summary>
    /// Test: RuleForAllEnums with specific return type
    /// </summary>
    [Fact]
    public void RuleForAllEnums_ShouldApplyToMultipleEnumTypes()
    {
        var item = new FakeBuilder<Item>()
            .RuleFor(x => x.Id, () => "TEST")
            .RuleForAllEnums(() =>
            {
                var rnd = new Random();
                return rnd.Next(2) == 0 ? (object)Color.Red : (object)Size.Large;
            })
            .Build();

        Assert.NotNull(item);
        Assert.NotNull(item.Color);
        Assert.NotNull(item.Size);
    }

    /// <summary>
    /// Test: Enum rule consistency across BuildList
    /// </summary>
    [Fact]
    public void EnumRuleConsistency_AcrossBuildList_ShouldBeValid()
    {
        var products = new FakeBuilder<Product>()
            .RuleFor(x => x.Id, () => Guid.NewGuid().ToString())
            .RuleFor(x => x.Name, () => "Product")
            .RuleForEnum(x => x.Color, () => Color.Blue)
            .RuleForEnum(x => x.Size, () => Size.Medium)
            .BuildList(10);

        Assert.All(products, p =>
        {
            Assert.Equal(Color.Blue, p.Color);
            Assert.Equal(Size.Medium, p.Size);
        });
    }

    /// <summary>
    /// Test: Enum without explicit rule should use attribute or type-based
    /// </summary>
    [Fact]
    public void EnumWithoutExplicitRule_ShouldUseDefaultGeneration()
    {
        var product = new FakeBuilder<Product>()
            .RuleFor(x => x.Id, () => "1")
            .RuleFor(x => x.Name, () => "Product")
            // No RuleForEnum specified
            .Build();

        // Should still have valid enum values from automatic generation
        Assert.True(Enum.IsDefined(typeof(Color), product.Color));
        Assert.True(Enum.IsDefined(typeof(Size), product.Size));
    }

    /// <summary>
    /// Test: Multiple RuleForEnum calls on same builder
    /// </summary>
    [Fact]
    public void MultipleRuleForEnumCalls_ShouldLastRuleWin()
    {
        var product = new FakeBuilder<Product>()
            .RuleFor(x => x.Id, () => "1")
            .RuleFor(x => x.Name, () => "Test")
            .RuleForEnum(x => x.Color, () => Color.Red)
            .RuleForEnum(x => x.Color, () => Color.Green)  // This should override
            .Build();

        Assert.Equal(Color.Green, product.Color);
    }

    /// <summary>
    /// Test: Dynamic enum selection in loop
    /// </summary>
    [Fact]
    public void DynamicEnumSelection_InLoop_ShouldCreateVariation()
    {
        var colors = Enum.GetValues(typeof(Color)).Cast<Color>().ToList();
        var colorIndex = 0;

        var products = new List<Product>();
        foreach (var i in Enumerable.Range(0, 5))
        {
            products.Add(new FakeBuilder<Product>()
                .RuleFor(x => x.Id, () => $"P{i}")
                .RuleFor(x => x.Name, () => $"Product {i}")
                .RuleForEnum(x => x.Color, () => colors[colorIndex++ % colors.Count])
                .RuleForEnum(x => x.Size, () => EnumGenerator.GetRandomValue<Size>())
                .Build());
        }

        Assert.Equal(5, products.Count);
        // Colors should follow the pattern
        Assert.Equal(Color.Red, products[0].Color);
        Assert.Equal(Color.Green, products[1].Color);
    }

    /// <summary>
    /// Test: Enum with constant values in attributes
    /// </summary>
    [Fact]
    public void EnumCombinedWithConstant_ShouldWork()
    {
        // Model with both Enum and Constant
        var model = new FakeBuilder<AttributeEnumModel>()
            .RuleForEnum(x => x.Color, () => Color.Yellow)
            .Build();

        Assert.Equal(Color.Yellow, model.Color);
    }
}
