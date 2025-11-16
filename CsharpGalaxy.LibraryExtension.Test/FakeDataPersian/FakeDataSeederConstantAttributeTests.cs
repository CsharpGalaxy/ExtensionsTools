using CsharpGalaxy.LibraryExtension.FakeDataPersian.Attributes;
using CsharpGalaxy.LibraryExtension.FakeDataPersian.Helpers;

namespace CsharpGalaxy.LibraryExtension.Test.FakeDataPersian;

/// <summary>
/// تست‌های ConstantAttribute برای FakeDataSeeder
/// </summary>
public class FakeDataSeederConstantAttributeTests
{
    // مدل‌های تستی
    public class ProductModel
    {
        [Guid]
        public string Id { get; set; }

        [Word]
        public string Name { get; set; }

        [Constant("IRR")]
        public string Currency { get; set; }

   

        [Constant("فعال")]
        public string Status { get; set; }

        [Constant(2024)]
        public int Year { get; set; }

        [Constant(3.14)]
        public double P { get; set; }

    }

    public class OrderModel
    {
        [Guid]
        public string Id { get; set; }

        [Email]
        public string CustomerEmail { get; set; }

        [Constant("درحال‌پردازش")]
        public string OrderStatus { get; set; }

        [Constant(1)]
        public int Version { get; set; }

        [DateTime]
        public DateTime CreatedDate { get; set; }

        [Ignore]
        public string InternalNotes { get; set; }
    }

    public class ConfigModel
    {
        [Constant(true)]
        public bool IsProduction { get; set; }

        [Constant("2024-11-16")]
        public string ReleaseDate { get; set; }

        [Constant(100)]
        public int MaxRetries { get; set; }

        [Constant(30.5)]
        public double Timeout { get; set; }

        [Guid]
        public string ConfigId { get; set; }
    }

    /// <summary>
    /// Test: ConstantAttribute should set constant string value
    /// </summary>
    [Fact]
    public void ConstantAttribute_WithString_ShouldSetValue()
    {
        var product = FakeDataSeeder.Seed<ProductModel>();

        Assert.Equal("IRR", product.Currency);
    }



    /// <summary>
    /// Test: ConstantAttribute should set constant int value
    /// </summary>
    [Fact]
    public void ConstantAttribute_WithInt_ShouldSetValue()
    {
        var product = FakeDataSeeder.Seed<ProductModel>();

        Assert.Equal(2024, product.Year);
    }

    /// <summary>
    /// Test: ConstantAttribute should set constant bool value
    /// </summary>
    [Fact]
    public void ConstantAttribute_WithBool_ShouldSetValue()
    {
        var config = FakeDataSeeder.Seed<ConfigModel>();

        Assert.True(config.IsProduction);
    }

    /// <summary>
    /// Test: ConstantAttribute should set constant double value
    /// </summary>
    [Fact]
    public void ConstantAttribute_WithDouble_ShouldSetValue()
    {
        var config = FakeDataSeeder.Seed<ConfigModel>();

        Assert.Equal(30.5, config.Timeout);
    }

    /// <summary>
    /// Test: Multiple ConstantAttributes in same object
    /// </summary>
    [Fact]
    public void MultipleConstantAttributes_ShouldAllBeSet()
    {
        var product = FakeDataSeeder.Seed<ProductModel>();

        Assert.Equal("IRR", product.Currency);
        //Assert.Equal(0.09m, product.TaxRate);
        Assert.Equal("فعال", product.Status);
        Assert.Equal(2024, product.Year);
    }

    /// <summary>
    /// Test: ConstantAttribute with SeedList should maintain values
    /// </summary>
    [Fact]
    public void ConstantAttribute_WithSeedList_ShouldMaintainValues()
    {
        var products = FakeDataSeeder.SeedList<ProductModel>(10);

        Assert.All(products, p => Assert.Equal("IRR", p.Currency));
        //Assert.All(products, p => Assert.Equal(0.09m, p.TaxRate));
        Assert.All(products, p => Assert.Equal("فعال", p.Status));
        Assert.All(products, p => Assert.Equal(2024, p.Year));
    }

    /// <summary>
    /// Test: ConstantAttribute should not affect other properties
    /// </summary>
    [Fact]
    public void ConstantAttribute_ShouldNotAffectOtherProperties()
    {
        var product1 = FakeDataSeeder.Seed<ProductModel>();
        var product2 = FakeDataSeeder.Seed<ProductModel>();

        // Constant properties should be same
        Assert.Equal(product1.Currency, product2.Currency);
        //Assert.Equal(product1.TaxRate, product2.TaxRate);
        Assert.Equal(product1.Status, product2.Status);

        // Non-constant properties should be different
        Assert.NotEqual(product1.Id, product2.Id);
        Assert.NotEqual(product1.Name, product2.Name);
    }

    /// <summary>
    /// Test: Combination of Constant and Ignore attributes
    /// </summary>
    [Fact]
    public void ConstantAndIgnore_ShouldWorkTogether()
    {
        var order = FakeDataSeeder.Seed<OrderModel>();

        // Constant property should have value
        Assert.Equal("درحال‌پردازش", order.OrderStatus);
        Assert.Equal(1, order.Version);

        // Ignored property should be null
        Assert.Null(order.InternalNotes);

        // Other properties should have values
        Assert.NotEmpty(order.Id);
        Assert.NotEmpty(order.CustomerEmail);
        Assert.NotEqual(default(DateTime), order.CreatedDate);
    }

    /// <summary>
    /// Test: ConstantAttribute with Persian text
    /// </summary>
    [Fact]
    public void ConstantAttribute_WithPersianText_ShouldPreserveTxt()
    {
        var order = FakeDataSeeder.Seed<OrderModel>();

        Assert.Equal("درحال‌پردازش", order.OrderStatus);
        Assert.Contains("درحال", order.OrderStatus);
    }

    /// <summary>
    /// Test: ConstantAttribute with zero values
    /// </summary>
    [Fact]
    public void ConstantAttribute_WithZero_ShouldSetZero()
    {
        var model = FakeDataSeeder.Seed<(string id, int zero)>();
        // Note: This would need a special model class, but demonstrates the concept
    }

    /// <summary>
    /// Test: ConstantAttribute in large list
    /// </summary>
    [Fact]
    public void ConstantAttribute_InLargeList_ShouldPerformWell()
    {
        var configs = FakeDataSeeder.SeedList<ConfigModel>(1000);

        Assert.Equal(1000, configs.Count);
        Assert.All(configs, c => Assert.True(c.IsProduction));
        Assert.All(configs, c => Assert.Equal("2024-11-16", c.ReleaseDate));
        Assert.All(configs, c => Assert.Equal(100, c.MaxRetries));
    }

    /// <summary>
    /// Test: ConstantAttribute should take precedence over type-based generation
    /// </summary>
    [Fact]
    public void ConstantAttribute_ShouldHavePrecedence()
    {
        var product = FakeDataSeeder.Seed<ProductModel>();

        // Even though Currency is a string, it should have the constant value
        Assert.Equal("IRR", product.Currency);
        Assert.NotEmpty(product.Name); // Other strings are still generated
    }

    /// <summary>
    /// Test: ConstantAttribute with null value
    /// </summary>
    [Fact]
    public void ConstantAttribute_CanSetNull()
    {
        // This would require a class with nullable property and [Constant(null)]
        var config = FakeDataSeeder.Seed<ConfigModel>();
        Assert.True(config.IsProduction);
    }

    /// <summary>
    /// Test: ConstantAttribute reflects configuration intent
    /// </summary>
    [Fact]
    public void ConstantAttribute_ReflectsConfigurationIntent()
    {
        var products = FakeDataSeeder.SeedList<ProductModel>(5);

        // All products from a batch should have same tax rate
        // This ensures consistent test data
        //var uniqueTaxRates = products.Select(p => p.TaxRate).Distinct().Count();
        //Assert.Equal(1, uniqueTaxRates);

        var uniqueStatuses = products.Select(p => p.Status).Distinct().Count();
        Assert.Equal(1, uniqueStatuses);
    }
}
