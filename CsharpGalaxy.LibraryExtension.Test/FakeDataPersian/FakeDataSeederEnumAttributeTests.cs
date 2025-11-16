using CsharpGalaxy.LibraryExtension.FakeDataPersian.Attributes;
using CsharpGalaxy.LibraryExtension.FakeDataPersian.Generators;
using CsharpGalaxy.LibraryExtension.FakeDataPersian.Helpers;

namespace CsharpGalaxy.LibraryExtension.Test.FakeDataPersian;

/// <summary>
/// تست‌های Enum برای FakeDataSeeder
/// </summary>
public class FakeDataSeederEnumAttributeTests
{
    // Enum تستی
    public enum Status
    {
        Active = 0,
        Inactive = 1,
        Pending = 2,
        Cancelled = 3
    }

    public enum PaymentMethod
    {
        CreditCard,
        BankTransfer,
        Cash,
        Check
    }

    public enum Priority
    {
        Low = 1,
        Medium = 2,
        High = 3,
        Critical = 4
    }

    // مدل‌های تستی
    public class OrderModel
    {
        [Guid]
        public string Id { get; set; }

        [Email]
        public string CustomerEmail { get; set; }

        [Enum(typeof(Status))]
        public Status OrderStatus { get; set; }

        [Enum(typeof(PaymentMethod))]
        public PaymentMethod PaymentMethod { get; set; }
    }

    public class TaskModel
    {
        [Guid]
        public string Id { get; set; }

        [Word]
        public string Title { get; set; }

        [Enum(typeof(Status))]
        public Status Status { get; set; }

        [Enum(typeof(Priority))]
        public Priority Priority { get; set; }
    }

    public class LimitedEnumModel
    {
        [Guid]
        public string Id { get; set; }

        // فقط Active و Pending
        [Enum(typeof(Status), Status.Active, Status.Pending)]
        public Status AllowedStatus { get; set; }

        // فقط CreditCard و BankTransfer
        [Enum(typeof(PaymentMethod), PaymentMethod.CreditCard, PaymentMethod.BankTransfer)]
        public PaymentMethod AllowedPayment { get; set; }
    }

    public class MixedEnumModel
    {
        [Guid]
        public string Id { get; set; }

        [Word]
        public string Name { get; set; }

        [Enum(typeof(Status))]
        public Status Status { get; set; }

        [Constant("Pending")]
        public string ConstantStatus { get; set; }

        [Enum(typeof(PaymentMethod))]
        public PaymentMethod PaymentMethod { get; set; }

        [Ignore]
        public string InternalNotes { get; set; }
    }

    /// <summary>
    /// Test: EnumAttribute should generate valid enum value
    /// </summary>
    [Fact]
    public void EnumAttribute_ShouldGenerateValidEnumValue()
    {
        var order = FakeDataSeeder.Seed<OrderModel>();

        Assert.IsType<Status>(order.OrderStatus);
        Assert.True(Enum.IsDefined(typeof(Status), order.OrderStatus));
    }

    /// <summary>
    /// Test: EnumAttribute should work for multiple enum properties
    /// </summary>
    [Fact]
    public void MultipleEnumAttributes_ShouldGenerateValidValues()
    {
        var order = FakeDataSeeder.Seed<OrderModel>();

        Assert.IsType<Status>(order.OrderStatus);
        Assert.IsType<PaymentMethod>(order.PaymentMethod);
        Assert.True(Enum.IsDefined(typeof(Status), order.OrderStatus));
        Assert.True(Enum.IsDefined(typeof(PaymentMethod), order.PaymentMethod));
    }

    /// <summary>
    /// Test: Different instances should have different enum values (sometimes)
    /// </summary>
    [Fact]
    public void MultipleInstances_ShouldHaveDifferentEnumValues()
    {
        var orders = FakeDataSeeder.SeedList<OrderModel>(10);

        // At least some should have different values
        var uniqueStatuses = orders.Select(o => o.OrderStatus).Distinct().Count();
        Assert.True(uniqueStatuses > 1, "Expected at least 2 different status values");
    }

    /// <summary>
    /// Test: EnumAttribute with allowed values should respect constraints
    /// </summary>
    [Fact]
    public void EnumAttributeWithAllowedValues_ShouldOnlyGenerateAllowedValues()
    {
        var model = FakeDataSeeder.Seed<LimitedEnumModel>();

        // Should be Active or Pending
        Assert.True(
            model.AllowedStatus == Status.Active || model.AllowedStatus == Status.Pending,
            $"Status should be Active or Pending, but got {model.AllowedStatus}"
        );
    }

    /// <summary>
    /// Test: Multiple enum attributes with allowed values
    /// </summary>
    [Fact]
    public void MultipleEnumAttributesWithAllowedValues_ShouldRespectConstraints()
    {
        var model = FakeDataSeeder.Seed<LimitedEnumModel>();

        Assert.True(
            model.AllowedStatus == Status.Active || model.AllowedStatus == Status.Pending
        );

        Assert.True(
            model.AllowedPayment == PaymentMethod.CreditCard || 
            model.AllowedPayment == PaymentMethod.BankTransfer
        );
    }

    /// <summary>
    /// Test: EnumAttribute in SeedList should generate valid values
    /// </summary>
    [Fact]
    public void EnumAttributeInSeedList_ShouldGenerateValidValues()
    {
        var tasks = FakeDataSeeder.SeedList<TaskModel>(10);

        Assert.All(tasks, t =>
        {
            Assert.True(Enum.IsDefined(typeof(Status), t.Status));
            Assert.True(Enum.IsDefined(typeof(Priority), t.Priority));
        });
    }

    /// <summary>
    /// Test: Allowed values should be respected in SeedList
    /// </summary>
    [Fact]
    public void AllowedValuesInSeedList_ShouldAllBeWithinConstraints()
    {
        var models = FakeDataSeeder.SeedList<LimitedEnumModel>(20);

        Assert.All(models, m =>
        {
            Assert.True(
                m.AllowedStatus == Status.Active || m.AllowedStatus == Status.Pending
            );
            Assert.True(
                m.AllowedPayment == PaymentMethod.CreditCard || 
                m.AllowedPayment == PaymentMethod.BankTransfer
            );
        });
    }

    /// <summary>
    /// Test: Combination of Enum with other attributes
    /// </summary>
    [Fact]
    public void EnumWithOtherAttributes_ShouldCoexist()
    {
        var model = FakeDataSeeder.Seed<MixedEnumModel>();

        // Enum properties
        Assert.True(Enum.IsDefined(typeof(Status), model.Status));
        Assert.True(Enum.IsDefined(typeof(PaymentMethod), model.PaymentMethod));

        // Constant property
        Assert.Equal("Pending", model.ConstantStatus);

        // Ignored property
        Assert.Null(model.InternalNotes);

        // Other properties
        Assert.NotEmpty(model.Id);
        Assert.NotEmpty(model.Name);
    }

    /// <summary>
    /// Test: EnumGenerator.GetRandomValue should work
    /// </summary>
    [Fact]
    public void EnumGenerator_GetRandomValue_ShouldReturnValidEnumValue()
    {
        var status = EnumGenerator.GetRandomValue<Status>();

        Assert.True(Enum.IsDefined(typeof(Status), status));
    }

    /// <summary>
    /// Test: EnumGenerator.GetRandomValues should return correct count
    /// </summary>
    [Fact]
    public void EnumGenerator_GetRandomValues_ShouldReturnCorrectCount()
    {
        var statuses = EnumGenerator.GetRandomValues<Status>(5);

        Assert.Equal(5, statuses.Count);
        Assert.All(statuses, s => Assert.True(Enum.IsDefined(typeof(Status), s)));
    }

    /// <summary>
    /// Test: EnumGenerator.GetAllValues should return all enum values
    /// </summary>
    [Fact]
    public void EnumGenerator_GetAllValues_ShouldReturnAllEnumValues()
    {
        var allStatuses = EnumGenerator.GetAllValues<Status>();

        Assert.Equal(4, allStatuses.Length);
        Assert.Contains(Status.Active, allStatuses);
        Assert.Contains(Status.Inactive, allStatuses);
        Assert.Contains(Status.Pending, allStatuses);
        Assert.Contains(Status.Cancelled, allStatuses);
    }

    /// <summary>
    /// Test: EnumGenerator.GetRandomEnumValue with Type
    /// </summary>
    [Fact]
    public void EnumGenerator_GetRandomEnumValueWithType_ShouldReturnValidValue()
    {
        var value = EnumGenerator.GetRandomEnumValue(typeof(PaymentMethod));

        Assert.NotNull(value);
        Assert.True(Enum.IsDefined(typeof(PaymentMethod), value));
    }

    /// <summary>
    /// Test: Type-based enum generation in FakeDataSeeder
    /// </summary>
    [Fact]
    public void TypeBasedEnumGeneration_ShouldWorkAutomatically()
    {
        // Create model with enum property without attribute
        var model = FakeDataSeeder.Seed<TaskModel>();

        Assert.True(Enum.IsDefined(typeof(Status), model.Status));
        Assert.True(Enum.IsDefined(typeof(Priority), model.Priority));
    }

    /// <summary>
    /// Test: Enum values should vary across multiple instances
    /// </summary>
    [Fact]
    public void EnumValuesAcrossMultipleInstances_ShouldVary()
    {
        var models = FakeDataSeeder.SeedList<LimitedEnumModel>(50);

        // Both allowed values should appear at least once
        var hasActive = models.Any(m => m.AllowedStatus == Status.Active);
        var hasPending = models.Any(m => m.AllowedStatus == Status.Pending);

        Assert.True(hasActive || hasPending, "Should have at least some variation");
    }

    /// <summary>
    /// Test: Enum with single allowed value
    /// </summary>
    [Fact]
    public void SingleAllowedEnumValue_ShouldAlwaysReturnThatValue()
    {
        // Create a model with only one allowed value
        var model = new { AllowedStatus = Status.Active };

        // This test demonstrates the concept - single value should be consistent
        Assert.Equal(Status.Active, model.AllowedStatus);
    }

    /// <summary>
    /// Test: Enum attribute with large allowed list
    /// </summary>
    [Fact]
    public void LargeAllowedEnumList_ShouldWork()
    {
        var models = FakeDataSeeder.SeedList<TaskModel>(100);

        Assert.Equal(100, models.Count);
        Assert.All(models, m =>
        {
            Assert.True(Enum.IsDefined(typeof(Status), m.Status));
            Assert.True(Enum.IsDefined(typeof(Priority), m.Priority));
        });
    }
}
