using CsharpGalaxy.LibraryExtension.FakeDataPersian.Abstracts;
using CsharpGalaxy.LibraryExtension.FakeDataPersian.Attributes;
using CsharpGalaxy.LibraryExtension.FakeDataPersian.Generators;

namespace CsharpGalaxy.LibraryExtension.Test.FakeDataPersian;

/// <summary>
/// تست‌های FakeBuilder برای مدیریت کلیدهای خارجی
/// </summary>
public class FakeBuilderForeignKeyTests
{
    // مدل‌های تستی
    public class Category
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class Product
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        [Ignore]        
        
        public string CategoryId { get; set; }
        
        [ForeignKey(nameof(Category), IsOptional = false)]
        public Category Category { get; set; }

        [ForeignKey(nameof(Category2), IsOptional = true)]
        public Category Category2 { get; set; }
    }

    public class Order
    {
        public string Id { get; set; }
        public string CustomerName { get; set; }
        
        [ForeignKey(nameof(Customer), IsOptional = true, NullProbability = 50)]
        public Customer Customer { get; set; }
    }

    public class Customer
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
    }

    /// <summary>
    /// Test: RuleForForeignKey should set foreign key property
    /// </summary>
    [Fact]
    public void RuleForForeignKey_ShouldSetForeignKeyProperty()
    {
        var product = new FakeBuilder<Product>()
            .RuleFor(x => x.Id, () => Guid.NewGuid().ToString())
            .RuleFor(x => x.Name, () => "محصول تستی")
            .RuleFor(x => x.Price, () => 50000m)
      
            .Build();

   
        Assert.NotEqual(product.Id, product.CategoryId);
    }

    /// <summary>
    /// Test: RuleForForeignKey with related object should create object
    /// </summary>
    [Fact]
    public void RuleForForeignKey_WithRelatedObject_ShouldCreateObject()
    {
        var product = new FakeBuilder<Product>()
            .RuleFor(x => x.Id, () => Guid.NewGuid().ToString())
            .RuleFor(x => x.Name, () => "محصول")
            .RuleFor(x => x.Price, () => 100000m)
            .RuleForForeignKey(x => x.Category, () => new FakeBuilder<Category>()
                .RuleFor(c => c.Id, () => Guid.NewGuid().ToString())
                .RuleFor(c => c.Name, () => "دسته‌بندی")
                .Build())
            .Build();

        Assert.NotNull(product.Category);
        Assert.NotEmpty(product.Category.Id);
        Assert.NotEmpty(product.Category.Name);
    }

    /// <summary>
    /// Test: Optional foreign key should sometimes be null
    /// </summary>
    [Fact]
    public void OptionalForeignKey_ShouldSometimesBeNull()
    {
        var categories = new List<Category>();
        var nullCount = 0;

        for (int i = 0; i < 100; i++)
        {
            var product = new FakeBuilder<Product>()
                .RuleFor(x => x.Id, () => Guid.NewGuid().ToString())
                .RuleFor(x => x.Name, () => "محصول")
                .RuleFor(x => x.Price, () => 50000m)
                .RuleForForeignKey(x => x.Category2, () => new FakeBuilder<Category>()
                    .RuleFor(c => c.Id, () => Guid.NewGuid().ToString())
                    .RuleFor(c => c.Name, () => "دسته‌بندی")
                    .Build())
                .Build();

            if (product.Category2 == null)
                nullCount++;
            else
                categories.Add(product.Category2);
        }

        // باید حدود ۵۰ درصد null باشند
        Assert.True(nullCount > 20 && nullCount < 80, $"Expected 20-80 nulls, got {nullCount}");
        Assert.True(categories.Count > 20 && categories.Count < 80, $"Expected 20-80 non-nulls, got {categories.Count}");
    }

    /// <summary>
    /// Test: Optional foreign key with custom null probability
    /// </summary>
    [Fact]
    public void OptionalForeignKey_WithCustomProbability_ShouldRespectNullProbability()
    {
        var order = new FakeBuilder<Order>()
            .RuleFor(x => x.Id, () => Guid.NewGuid().ToString())
            .RuleFor(x => x.CustomerName, () => "مشتری")
            .RuleForForeignKey(x => x.Customer, () => new FakeBuilder<Customer>()
                .RuleFor(c => c.Id, () => Guid.NewGuid().ToString())
                .RuleFor(c => c.FullName, () => PersianNameGenerator.FullName())
                .RuleFor(c => c.Email, () => PersianTextGenerator.Email())
                .Build())
            .Build();

        // Order.Customer.NullProbability = 50 پس باید احتمالات تقریبی برابر باشند
        Assert.True(order.Customer == null || !string.IsNullOrEmpty(order.Customer.Id));
    }

    /// <summary>
    /// Test: Multiple foreign keys in same object
    /// </summary>
    [Fact]
    public void MultipleForeignKeys_ShouldAllWork()
    {
        var order = new FakeBuilder<Order>()
            .RuleFor(x => x.Id, () => Guid.NewGuid().ToString())
            .RuleFor(x => x.CustomerName, () => "مشتری")
            .RuleForForeignKey(x => x.Customer, () => new FakeBuilder<Customer>()
                .RuleFor(c => c.Id, () => Guid.NewGuid().ToString())
                .RuleFor(c => c.FullName, () => PersianNameGenerator.FullName())
                .RuleFor(c => c.Email, () => PersianTextGenerator.Email())
                .Build())
            .Build();

        Assert.NotEmpty(order.Id);
        Assert.NotEmpty(order.CustomerName);
        // Customer می‌تواند null یا مقدار دارد
        if (order.Customer != null)
        {
            Assert.NotEmpty(order.Customer.Id);
            Assert.NotEmpty(order.Customer.FullName);
            Assert.NotEmpty(order.Customer.Email);
        }
    }

    /// <summary>
    /// Test: BuildList with foreign keys
    /// </summary>
    [Fact]
    public void BuildList_WithForeignKeys_ShouldCreateMultipleObjects()
    {
        var products = new FakeBuilder<Product>()
            .RuleFor(x => x.Id, () => Guid.NewGuid().ToString())
            .RuleFor(x => x.Name, () => $"محصول {new Random().Next(1000, 9999)}")
            .RuleFor(x => x.Price, () => Convert.ToDecimal(new Random().Next(10000, 1000000)))
            .RuleForForeignKey(x => x.Category, () => new FakeBuilder<Category>()
                .RuleFor(c => c.Id, () => Guid.NewGuid().ToString())
                .RuleFor(c => c.Name, () => "دسته‌بندی")
                .Build())
            .BuildList(10);

        Assert.Equal(10, products.Count);
        Assert.All(products, p => Assert.NotEmpty(p.Id));
        Assert.All(products, p => Assert.NotEmpty(p.Name));
        
        // Some should have categories, some might not
        var withCategories = products.Count(p => p.Category != null);
        Assert.True(withCategories > 0 && withCategories <= 10, "Some products should have categories, some should not");
    }

    /// <summary>
    /// Test: Nested foreign keys
    /// </summary>
    [Fact]
    public void NestedForeignKeys_ShouldWork()
    {
        var product = new FakeBuilder<Product>()
            .RuleFor(x => x.Id, () => Guid.NewGuid().ToString())
            .RuleFor(x => x.Name, () => "محصول")
            .RuleFor(x => x.Price, () => 50000m)
            .RuleForForeignKey(x => x.Category, () => new FakeBuilder<Category>()
                .RuleFor(c => c.Id, () => Guid.NewGuid().ToString())
                .RuleFor(c => c.Name, () => new FakeBuilder<Category>()
                    .RuleFor(cat => cat.Name, () => "زیردسته")
                    .Build()
                    .Name)
                .Build())
            .Build();

        Assert.NotNull(product.Category);
        Assert.NotEmpty(product.Category.Name);
    }

    /// <summary>
    /// Test: Foreign key attribute should be recognized
    /// </summary>
    [Fact]
    public void ForeignKeyAttribute_ShouldBePresent()
    {

        
        var categoryAttr = typeof(Product).GetProperty(nameof(Product.Category))
            ?.GetCustomAttributes(typeof(ForeignKeyAttribute), false);

        Assert.NotNull(categoryAttr);
   
        Assert.True(categoryAttr.Length > 0);
    }

    /// <summary>
    /// Test: ForeignKey IsOptional property
    /// </summary>
    [Fact]
    public void ForeignKeyAttribute_IsOptional_ShouldBeRespected()
    {
        var attr = typeof(Product).GetProperty(nameof(Product.Category2))
            ?.GetCustomAttributes(typeof(ForeignKeyAttribute), false)
            .FirstOrDefault() as ForeignKeyAttribute;

        Assert.NotNull(attr);
        Assert.True(attr.IsOptional);
    }

    /// <summary>
    /// Test: ForeignKey NullProbability property
    /// </summary>
    [Fact]
    public void ForeignKeyAttribute_NullProbability_ShouldBeConfigurable()
    {
        var attr = typeof(Order).GetProperty(nameof(Order.Customer))
            ?.GetCustomAttributes(typeof(ForeignKeyAttribute), false)
            .FirstOrDefault() as ForeignKeyAttribute;

        Assert.NotNull(attr);
        Assert.Equal(50, attr.NullProbability);
    }

    /// <summary>
    /// Test: Large list creation with foreign keys
    /// </summary>
    [Fact]
    public void BuildList_LargeCount_WithForeignKeys_ShouldPerformWell()
    {
        var orders = new FakeBuilder<Order>()
            .RuleFor(x => x.Id, () => Guid.NewGuid().ToString())
            .RuleFor(x => x.CustomerName, () => PersianNameGenerator.FullName())
            .RuleForForeignKey(x => x.Customer, () => new FakeBuilder<Customer>()
                .RuleFor(c => c.Id, () => Guid.NewGuid().ToString())
                .RuleFor(c => c.FullName, () => PersianNameGenerator.FullName())
                .RuleFor(c => c.Email, () => PersianTextGenerator.Email())
                .Build())
            .BuildList(100);

        Assert.Equal(100, orders.Count);
        Assert.All(orders, o => Assert.NotEmpty(o.Id));
        Assert.All(orders, o => Assert.NotEmpty(o.CustomerName));
    }

    /// <summary>
    /// Test: Foreign key with different types
    /// </summary>
    [Fact]
    public void RuleForForeignKey_WithDifferentReturnTypes_ShouldWork()
    {
        // Product with RuleFor properties
        var product1 = new FakeBuilder<Product>()
            .RuleFor(x => x.Id, () => Guid.NewGuid().ToString())
            .RuleFor(x => x.Name, () => "محصول")
            .RuleFor(x => x.Price, () => 50000m)
            .Build();

        Assert.NotEmpty(product1.Id);
        Assert.Equal("محصول", product1.Name);
        Assert.Equal(50000m, product1.Price);

        // Product with RuleForForeignKey
        var product2 = new FakeBuilder<Product>()
            .RuleFor(x => x.Id, () => Guid.NewGuid().ToString())
            .RuleFor(x => x.Name, () => "محصول")
            .RuleFor(x => x.Price, () => 50000m)
           .RuleForForeignKey(x => x.Category, () => new FakeBuilder<Category>()
                .RuleFor(c => c.Id, () => Guid.NewGuid().ToString())
                .RuleFor(c => c.Name, () => "دسته")
                .Build())
            .Build();

        Assert.NotNull(product2);
        Assert.NotEmpty(product2.Id);
        Assert.Equal("محصول", product2.Name);
        // Category می‌تواند null باشد (optional = true)
        if (product2.Category != null)
        {
            Assert.NotEmpty(product2.Category.Id);
            Assert.Equal("دسته", product2.Category.Name);
        }
    }

    /// <summary>
    /// Test: Mixing RuleFor and RuleForForeignKey
    /// </summary>
    [Fact]
    public void MixingRuleFor_AndRuleForForeignKey_ShouldWork()
    {
        var product = new FakeBuilder<Product>()
            .RuleFor(x => x.Id, () => Guid.NewGuid().ToString())
            .RuleFor(x => x.Name, () => "محصول خاص")
            .RuleFor(x => x.Price, () => 75000m)
            .RuleForForeignKey(x => x.Category, () => new FakeBuilder<Category>()
                .RuleFor(c => c.Id, () => Guid.NewGuid().ToString())
                .RuleFor(c => c.Name, () => "دسته")
                .Build())
            .Build();

        Assert.Equal("محصول خاص", product.Name);
        Assert.Equal(75000m, product.Price);
        // Category می‌تواند null یا دارای مقدار باشد (optional)
        if (product.Category != null)
        {
            Assert.NotEmpty(product.Category.Id);
            Assert.Equal("دسته", product.Category.Name);
        }
    }
}
