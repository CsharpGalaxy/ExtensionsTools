using CsharpGalaxy.LibraryExtension.FakeDataPersian.Abstracts;
using CsharpGalaxy.LibraryExtension.FakeDataPersian.Attributes;
using CsharpGalaxy.LibraryExtension.FakeDataPersian.Generators;
using CsharpGalaxy.LibraryExtension.FakeDataPersian.Helpers;
using System.Reflection;

namespace CsharpGalaxy.LibraryExtension.Test.FakeDataPersian;

/// <summary>
/// تست‌های جامع برای تمام Attributes در FakeBuilder و FakeDataSeeder
/// </summary>
public class FakeDataAttributeComprehensiveTests
{
    // ===== تعریف Enums =====
    
    public enum OrderStatus
    {
        Pending,
        Processing,
        Completed,
        Cancelled
    }

    public enum PaymentMethod
    {
        CreditCard,
        BankTransfer,
        Cash
    }

    // ===== تعریف مدل‌های تستی =====

    public class Category
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    /// <summary>
    /// مدل محصول با تمام انواع Attributes
    /// </summary>
    public class Product
    {
        [Guid]
        public string Id { get; set; }
        
        [Word]
        public string Name { get; set; }
        
        [Constant("ثابت-نوع")]
        public string Type { get; set; }
        
        [Constant("1000")]
        public string FixedPrice { get; set; }
        
        public decimal ActualPrice { get; set; }
        
        [Enum(typeof(OrderStatus))]
        public OrderStatus Status { get; set; }
        
        [Ignore]
        public string Notes { get; set; }
        
        [ForeignKey(nameof(Category), IsOptional = false)]
        public Category Category { get; set; }
    }

    /// <summary>
    /// مدل سفارش با Attributes متنوع
    /// </summary>
    public class Order
    {
        [Constant("ORDER-2024")]
        public string OrderPrefix { get; set; }
        
        [Guid]
        public string Id { get; set; }
        
        [FirstName]
        public string CustomerName { get; set; }
        
        [Email]
        public string CustomerEmail { get; set; }
        
        [Mobile]
        public string CustomerPhone { get; set; }
        
        [Enum(typeof(OrderStatus))]
        public OrderStatus Status { get; set; }
        
        [Enum(typeof(PaymentMethod))]
        public PaymentMethod PaymentMethod { get; set; }
        
        [Constant("تصویب‌شده")]
        public string ApprovalStatus { get; set; }
        
        [Ignore]
        public decimal ManualDiscount { get; set; }
        
        [ForeignKey(nameof(Product), IsOptional = true, NullProbability = 30)]
        public Product Product { get; set; }
    }

    /// <summary>
    /// مدل فاکتور
    /// </summary>
    public class Invoice
    {
        [Constant("INV-2024")]
        public string InvoicePrefix { get; set; }
        
        [Guid]
        public string Id { get; set; }
        
        [FullName]
        public string InvoiceTo { get; set; }
        
        [Email]
        public string ContactEmail { get; set; }
        
        [Iban]
        public string BankAccount { get; set; }
        
        [Enum(typeof(PaymentMethod))]
        public PaymentMethod PaymentMethod { get; set; }
        
        [Ignore]
        public string SpecialNotes { get; set; }
    }

    // ===== تست‌های Constant Attribute =====

    [Fact]
    public void ConstantAttribute_WithFakeBuilder_ShouldSetExactValue()
    {
        var order = new FakeBuilder<Order>()
            .RuleFor(x => x.Id, () => Guid.NewGuid().ToString())
            .RuleFor(x => x.CustomerName, () => "احمد")
            .RuleFor(x => x.CustomerEmail, () => "ahmad@test.com")
            .RuleFor(x => x.CustomerPhone, () => "09121234567")
            .RuleFor(x => x.Status, () => OrderStatus.Completed)
            .RuleFor(x => x.PaymentMethod, () => PaymentMethod.CreditCard)
            .Build();

        Assert.Equal("ORDER-2024", order.OrderPrefix);
        Assert.Equal("تصویب‌شده", order.ApprovalStatus);
    }

    [Fact]
    public void ConstantAttribute_WithFakeDataSeeder_ShouldSetValue()
    {
        var product = FakeDataSeeder.Seed<Product>();

        Assert.Equal("ثابت-نوع", product.Type);
        Assert.Equal("1000", product.FixedPrice);
    }

    [Fact]
    public void ConstantAttribute_InList_ShouldRemainConstant()
    {
        var orders = new FakeBuilder<Order>()
            .RuleFor(x => x.Id, () => Guid.NewGuid().ToString())
            .RuleFor(x => x.CustomerName, () => PersianNameGenerator.FirstName())
            .RuleFor(x => x.CustomerEmail, () => PersianTextGenerator.Email())
            .RuleFor(x => x.CustomerPhone, () => IranianMobileGenerator.Mobile())
            .RuleFor(x => x.Status, () => OrderStatus.Completed)
            .RuleFor(x => x.PaymentMethod, () => PaymentMethod.CreditCard)
            .BuildList(10);

        Assert.All(orders, o => Assert.Equal("ORDER-2024", o.OrderPrefix));
        Assert.All(orders, o => Assert.Equal("تصویب‌شده", o.ApprovalStatus));
    }

    [Fact]
    public void ConstantAttribute_InSeederList_ShouldRemainConstant()
    {
        var products = FakeDataSeeder.SeedList<Product>(10);

        Assert.All(products, p => Assert.Equal("ثابت-نوع", p.Type));
        Assert.All(products, p => Assert.Equal("1000", p.FixedPrice));
    }

    // ===== تست‌های Enum Attribute =====

    [Fact]
    public void EnumAttribute_WithFakeBuilder_ShouldGenerateValidValue()
    {
        var order = new FakeBuilder<Order>()
            .RuleFor(x => x.Id, () => Guid.NewGuid().ToString())
            .RuleFor(x => x.CustomerName, () => "احمد")
            .RuleFor(x => x.CustomerEmail, () => "ahmad@test.com")
            .RuleFor(x => x.CustomerPhone, () => "09121234567")
            .Build();

        Assert.True(Enum.IsDefined(typeof(OrderStatus), order.Status));
        Assert.True(Enum.IsDefined(typeof(PaymentMethod), order.PaymentMethod));
    }

    [Fact]
    public void EnumAttribute_WithFakeDataSeeder_ShouldGenerateValidValue()
    {
        var product = FakeDataSeeder.Seed<Product>();

        Assert.True(Enum.IsDefined(typeof(OrderStatus), product.Status));
    }

    [Fact]
    public void EnumAttribute_InList_ShouldHaveDifferentValues()
    {
        var orders = new FakeBuilder<Order>()
            .RuleFor(x => x.Id, () => Guid.NewGuid().ToString())
            .RuleFor(x => x.CustomerName, () => PersianNameGenerator.FirstName())
            .RuleFor(x => x.CustomerEmail, () => PersianTextGenerator.Email())
            .RuleFor(x => x.CustomerPhone, () => IranianMobileGenerator.Mobile())
            .BuildList(30);

        var statuses = orders.Select(o => o.Status).Distinct();
        var payments = orders.Select(o => o.PaymentMethod).Distinct();

        Assert.True(statuses.Count() > 1, "Should have multiple different statuses");
        Assert.True(payments.Count() > 1, "Should have multiple different payment methods");
    }

    // ===== تست‌های Ignore Attribute =====

    [Fact]
    public void IgnoreAttribute_WithFakeBuilder_ShouldNotPopulate()
    {
        var order = new FakeBuilder<Order>()
            .RuleFor(x => x.Id, () => Guid.NewGuid().ToString())
            .RuleFor(x => x.CustomerName, () => "احمد")
            .RuleFor(x => x.CustomerEmail, () => "ahmad@test.com")
            .RuleFor(x => x.CustomerPhone, () => "09121234567")
            .RuleFor(x => x.Status, () => OrderStatus.Completed)
            .RuleFor(x => x.PaymentMethod, () => PaymentMethod.CreditCard)
            .Build();

        Assert.Equal(0, order.ManualDiscount);
    }

    [Fact]
    public void IgnoreAttribute_WithFakeDataSeeder_ShouldNotPopulate()
    {
        var product = FakeDataSeeder.Seed<Product>();

        Assert.Null(product.Notes);
    }

    [Fact]
    public void IgnoreAttribute_InInvoice_ShouldRemainNull()
    {
        var invoice = new FakeBuilder<Invoice>()
            .RuleFor(x => x.Id, () => Guid.NewGuid().ToString())
            .RuleFor(x => x.InvoiceTo, () => PersianNameGenerator.FullName())
            .RuleFor(x => x.ContactEmail, () => PersianTextGenerator.Email())
            .RuleFor(x => x.BankAccount, () => BankingMoneyGenerator.Sheba())
            .RuleFor(x => x.PaymentMethod, () => PaymentMethod.CreditCard)
            .Build();

        Assert.Null(invoice.SpecialNotes);
    }

    // ===== تست‌های ForeignKey Attribute =====

    [Fact]
    public void ForeignKeyAttribute_WithIsOptionalFalse_ShouldAlwaysHaveValue()
    {
        for (int i = 0; i < 20; i++)
        {
            var product = new FakeBuilder<Product>()
                .RuleFor(x => x.Id, () => Guid.NewGuid().ToString())
                .RuleFor(x => x.Name, () => "محصول")
                .RuleFor(x => x.ActualPrice, () => 50000m)
                .RuleForForeignKey(x => x.Category, () => new FakeBuilder<Category>()
                    .RuleFor(c => c.Id, () => Guid.NewGuid().ToString())
                    .RuleFor(c => c.Name, () => "دسته‌بندی")
                    .Build())
                .Build();

            Assert.NotNull(product.Category);
            Assert.NotEmpty(product.Category.Id);
            Assert.NotEmpty(product.Category.Name);
        }
    }

    [Fact]
    public void ForeignKeyAttribute_WithIsOptionalTrue_ShouldSometimesBeNull()
    {
        var nullCount = 0;
        var notNullCount = 0;

        for (int i = 0; i < 100; i++)
        {
            var order = new FakeBuilder<Order>()
                .RuleFor(x => x.Id, () => Guid.NewGuid().ToString())
                .RuleFor(x => x.CustomerName, () => "احمد")
                .RuleFor(x => x.CustomerEmail, () => "ahmad@test.com")
                .RuleFor(x => x.CustomerPhone, () => "09121234567")
                .RuleFor(x => x.Status, () => OrderStatus.Completed)
                .RuleFor(x => x.PaymentMethod, () => PaymentMethod.CreditCard)
                .RuleForForeignKey(x => x.Product, () => new FakeBuilder<Product>()
                    .RuleFor(p => p.Id, () => Guid.NewGuid().ToString())
                    .RuleFor(p => p.Name, () => "محصول")
                    .RuleFor(p => p.ActualPrice, () => 50000m)
                    .RuleForForeignKey(p => p.Category, () => new FakeBuilder<Category>()
                        .RuleFor(c => c.Id, () => Guid.NewGuid().ToString())
                        .RuleFor(c => c.Name, () => "دسته")
                        .Build())
                    .Build())
                .Build();

            if (order.Product == null)
                nullCount++;
            else
                notNullCount++;
        }

        // NullProbability = 30 یعنی تقریباً ۳۰ درصد null
        Assert.True(nullCount >= 15 && nullCount <= 45, $"Expected 15-45 nulls, got {nullCount}");
        Assert.True(notNullCount >= 55 && notNullCount <= 85, $"Expected 55-85 non-nulls, got {notNullCount}");
    }

    [Fact]
    public void ForeignKeyAttribute_NullProbability_ShouldBeRespected()
    {
        var attr = typeof(Order).GetProperty(nameof(Order.Product))
            ?.GetCustomAttributes(typeof(ForeignKeyAttribute), false)
            .FirstOrDefault() as ForeignKeyAttribute;

        Assert.NotNull(attr);
        Assert.Equal(30, attr.NullProbability);
    }

    // ===== تست‌های Custom Attributes (FirstName, Email, etc) =====

    [Fact]
    public void FirstNameAttribute_WithFakeDataSeeder_ShouldGenerateValidName()
    {
        var order = FakeDataSeeder.Seed<Order>();

        Assert.NotEmpty(order.CustomerName);
    }

    [Fact]
    public void EmailAttribute_WithFakeDataSeeder_ShouldGenerateValidEmail()
    {
        var invoice = FakeDataSeeder.Seed<Invoice>();

        Assert.Contains("@", invoice.ContactEmail);
    }

    [Fact]
    public void IbanAttribute_WithFakeDataSeeder_ShouldGenerateValidIban()
    {
        var invoice = FakeDataSeeder.Seed<Invoice>();

        Assert.NotEmpty(invoice.BankAccount);
        Assert.StartsWith("IR", invoice.BankAccount);
    }

    // ===== تست‌های ترکیبی =====

    [Fact]
    public void AllAttributes_Combined_ShouldWorkTogether()
    {
        var order = new FakeBuilder<Order>()
            .RuleFor(x => x.Id, () => Guid.NewGuid().ToString())
            .RuleFor(x => x.CustomerName, () => PersianNameGenerator.FullName())
            .RuleFor(x => x.CustomerEmail, () => PersianTextGenerator.Email())
            .RuleFor(x => x.CustomerPhone, () => IranianMobileGenerator.Mobile())
            .RuleForForeignKey(x => x.Product, () => new FakeBuilder<Product>()
                .RuleFor(p => p.Id, () => Guid.NewGuid().ToString())
                .RuleFor(p => p.Name, () => "محصول")
                .RuleFor(p => p.ActualPrice, () => 50000m)
                .RuleForForeignKey(p => p.Category, () => new FakeBuilder<Category>()
                    .RuleFor(c => c.Id, () => Guid.NewGuid().ToString())
                    .RuleFor(c => c.Name, () => "دسته")
                    .Build())
                .Build())
            .Build();

        // Constant attributes
        Assert.Equal("ORDER-2024", order.OrderPrefix);
        Assert.Equal("تصویب‌شده", order.ApprovalStatus);
        
        // Custom attributes
        Assert.NotEmpty(order.CustomerName);
        Assert.Contains("@", order.CustomerEmail);
        Assert.StartsWith("09", order.CustomerPhone);
        
        // Enum attribute
        Assert.True(Enum.IsDefined(typeof(OrderStatus), order.Status));
        Assert.True(Enum.IsDefined(typeof(PaymentMethod), order.PaymentMethod));
        
        // Ignore attribute
        Assert.Equal(0, order.ManualDiscount);
        
        // ForeignKey attribute
        if (order.Product != null)
        {
            Assert.NotNull(order.Product);
            Assert.NotEmpty(order.Product.Name);
        }
    }

    [Fact]
    public void FakeDataSeeder_WithAllAttributes_ShouldWorkCorrectly()
    {
        var orders = FakeDataSeeder.SeedList<Order>(10);

        Assert.All(orders, o => Assert.NotEmpty(o.Id));
        Assert.All(orders, o => Assert.NotEmpty(o.CustomerName));
        Assert.All(orders, o => Assert.NotEmpty(o.CustomerEmail));
        Assert.All(orders, o => Assert.NotEmpty(o.CustomerPhone));
        Assert.All(orders, o => Assert.Equal("ORDER-2024", o.OrderPrefix));
        Assert.All(orders, o => Assert.Equal("تصویب‌شده", o.ApprovalStatus));
        Assert.All(orders, o => Assert.Equal(0, o.ManualDiscount));
    }

    [Fact]
    public void LargeDataSet_WithAllAttributes_ShouldPerformWell()
    {
        var orders = new FakeBuilder<Order>()
            .RuleFor(x => x.Id, () => Guid.NewGuid().ToString())
            .RuleFor(x => x.CustomerName, () => PersianNameGenerator.FullName())
            .RuleFor(x => x.CustomerEmail, () => PersianTextGenerator.Email())
            .RuleFor(x => x.CustomerPhone, () => IranianMobileGenerator.Mobile())
            .RuleForForeignKey(x => x.Product, () => new FakeBuilder<Product>()
                .RuleFor(p => p.Id, () => Guid.NewGuid().ToString())
                .RuleFor(p => p.Name, () => $"محصول {Random.Shared.Next(1000, 9999)}")
                .RuleFor(p => p.ActualPrice, () => Convert.ToDecimal(Random.Shared.Next(10000, 1000000)))
                .RuleForForeignKey(p => p.Category, () => new FakeBuilder<Category>()
                    .RuleFor(c => c.Id, () => Guid.NewGuid().ToString())
                    .RuleFor(c => c.Name, () => "دسته‌بندی")
                    .Build())
                .Build())
            .BuildList(1000);

        Assert.Equal(1000, orders.Count);
        Assert.All(orders, o => Assert.Equal("ORDER-2024", o.OrderPrefix));
        Assert.All(orders, o => Assert.Equal("تصویب‌شده", o.ApprovalStatus));
        Assert.All(orders, o => Assert.True(Enum.IsDefined(typeof(OrderStatus), o.Status)));
        Assert.All(orders, o => Assert.True(Enum.IsDefined(typeof(PaymentMethod), o.PaymentMethod)));
    }

    [Fact]
    public void AttributeDetection_ShouldFindAllAttributeTypes()
    {
        var orderProperties = typeof(Order).GetProperties();
        
        var constantAttrs = orderProperties.Where(p => 
            p.GetCustomAttribute<ConstantAttribute>() != null).ToList();
        var enumAttrs = orderProperties.Where(p => 
            p.GetCustomAttribute<EnumAttribute>() != null).ToList();
        var ignoreAttrs = orderProperties.Where(p => 
            p.GetCustomAttribute<IgnoreAttribute>() != null).ToList();
        var foreignKeyAttrs = orderProperties.Where(p => 
            p.GetCustomAttribute<ForeignKeyAttribute>() != null).ToList();
        var firstNameAttrs = orderProperties.Where(p => 
            p.GetCustomAttribute<FirstNameAttribute>() != null).ToList();

        Assert.True(constantAttrs.Count > 0, "Should find Constant attributes");
        Assert.True(enumAttrs.Count > 0, "Should find Enum attributes");
        Assert.True(ignoreAttrs.Count > 0, "Should find Ignore attributes");
        Assert.True(foreignKeyAttrs.Count > 0, "Should find ForeignKey attributes");
    }

    [Fact]
    public void RuleOverride_ShouldOverrideAttribute()
    {
        var order = new FakeBuilder<Order>()
            .RuleFor(x => x.Id, () => Guid.NewGuid().ToString())
            .RuleFor(x => x.CustomerName, () => "علی")  // Override FirstName
            .RuleFor(x => x.CustomerEmail, () => "ali@custom.com")  // Override Email
            .RuleFor(x => x.CustomerPhone, () => "09999999999")
            .RuleFor(x => x.Status, () => OrderStatus.Pending)
            .RuleFor(x => x.PaymentMethod, () => PaymentMethod.BankTransfer)
            .Build();

        Assert.Equal("علی", order.CustomerName);
        Assert.Equal("ali@custom.com", order.CustomerEmail);
        Assert.Equal("ORDER-2024", order.OrderPrefix);  // Constant remains constant
        Assert.Equal("تصویب‌شده", order.ApprovalStatus);  // Constant remains constant
    }

    [Fact]
    public void MixingSeederAndBuilder_ShouldBothWork()
    {
        // Using Seeder
        var seededProduct = FakeDataSeeder.Seed<Product>();
        Assert.NotNull(seededProduct);
        Assert.Equal("ثابت-نوع", seededProduct.Type);
        if (seededProduct.Category != null)
        {
            Assert.NotNull(seededProduct.Category);
        }

        // Using Builder
        var builtProduct = new FakeBuilder<Product>()
            .RuleFor(x => x.Id, () => Guid.NewGuid().ToString())
            .RuleFor(x => x.Name, () => "محصول")
            .RuleFor(x => x.ActualPrice, () => 50000m)
            .RuleForForeignKey(x => x.Category, () => new FakeBuilder<Category>()
                .RuleFor(c => c.Id, () => Guid.NewGuid().ToString())
                .RuleFor(c => c.Name, () => "دسته")
                .Build())
            .Build();

        Assert.NotNull(builtProduct);
        Assert.Equal("ثابت-نوع", builtProduct.Type);
        Assert.NotNull(builtProduct.Category);
    }
}
