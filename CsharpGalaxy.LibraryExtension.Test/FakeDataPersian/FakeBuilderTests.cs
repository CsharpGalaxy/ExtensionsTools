using CsharpGalaxy.LibraryExtension.FakeDataPersian.Abstracts;
using CsharpGalaxy.LibraryExtension.FakeDataPersian.Generators;
using CsharpGalaxy.LibraryExtension.FakeDataPersian.Models;

namespace CsharpGalaxy.LibraryExtension.Test.FakeDataPersian;

public class FakeBuilderTests
{
    /// <summary>
    /// Test: RuleFor should set property with provided value
    /// </summary>
    [Fact]
    public void RuleFor_ShouldSetPropertyWithProvidedValue()
    {
        var builder = new FakeBuilder<TestModel>()
            .RuleFor(x => x.FirstName, () => "احمد");

        var model = builder.Build();

        Assert.Equal("احمد", model.FirstName);
    }

    /// <summary>
    /// Test: Multiple RuleFor calls should set all properties
    /// </summary>
    [Fact]
    public void MultiplePuleFor_ShouldSetMultipleProperties()
    {
        var builder = new FakeBuilder<TestModel>()
            .RuleFor(x => x.FirstName, () => "احمد")
            .RuleFor(x => x.LastName, () => "علوی")
            .RuleFor(x => x.Email, () => "ahmad@example.com");

        var model = builder.Build();

        Assert.Equal("احمد", model.FirstName);
        Assert.Equal("علوی", model.LastName);
        Assert.Equal("ahmad@example.com", model.Email);
    }

    /// <summary>
    /// Test: RuleFor with dynamic generator should work
    /// </summary>
    [Fact]
    public void RuleFor_WithDynamicGenerator_ShouldGenerateDifferentValues()
    {
        var builder = new FakeBuilder<TestModel>()
            .RuleFor(x => x.FirstName, () => PersianNameGenerator.FirstName())
            .RuleFor(x => x.LastName, () => PersianNameGenerator.LastName());

        var model1 = builder.Build();
        var model2 = builder.Build();

        Assert.NotEqual(model1.FirstName, model2.FirstName);
    }

    /// <summary>
    /// Test: BuildList should create multiple instances
    /// </summary>
    [Fact]
    public void BuildList_ShouldCreateRequestedNumberOfInstances()
    {
        var builder = new FakeBuilder<TestModel>()
            .RuleFor(x => x.FirstName, () => PersianNameGenerator.FirstName())
            .RuleFor(x => x.LastName, () => PersianNameGenerator.LastName());

        var models = builder.BuildList(5);

        Assert.Equal(5, models.Count);
    }

    /// <summary>
    /// Test: BuildList should create instances with different values
    /// </summary>
    [Fact]
    public void BuildList_ShouldCreateInstancesWithDifferentValues()
    {
        var builder = new FakeBuilder<TestModel>()
            .RuleFor(x => x.FirstName, () => PersianNameGenerator.FirstName())
            .RuleFor(x => x.Email, () => PersianTextGenerator.Email());

        var models = builder.BuildList(10);
        var firstNames = models.Select(m => m.FirstName).Distinct().Count();
        var emails = models.Select(m => m.Email).Distinct().Count();

        Assert.True(firstNames > 1);
        Assert.True(emails > 1);
    }

    /// <summary>
    /// Test: RuleForAllStrings should set all string properties
    /// </summary>
    [Fact]
    public void RuleForAllStrings_ShouldSetAllStringProperties()
    {
        var builder = new FakeBuilder<TestModel>()
            .RuleForAllStrings(() => "تست");

        var model = builder.Build();

        Assert.Equal("تست", model.FirstName);
        Assert.Equal("تست", model.LastName);
        Assert.Equal("تست", model.Email);
        Assert.Equal("تست", model.Mobile);
    }

    /// <summary>
    /// Test: RuleForAllInts should set all int properties
    /// </summary>
    [Fact]
    public void RuleForAllInts_ShouldSetAllIntProperties()
    {
        var builder = new FakeBuilder<TestModel>()
            .RuleForAllInts(() => 42);

        var model = builder.Build();

        Assert.Equal(42, model.Age);
        Assert.Equal(42, model.OptionalAge);
    }

    /// <summary>
    /// Test: RuleForAllBools should set all bool properties
    /// </summary>
    [Fact]
    public void RuleForAllBools_ShouldSetAllBoolProperties()
    {
        var builder = new FakeBuilder<TestModel>()
            .RuleForAllBools(() => true);

        var model = builder.Build();

        Assert.True(model.IsActive);
        Assert.True(model.Active);
    }

    /// <summary>
    /// Test: RuleForAllDecimals should set all decimal properties
    /// </summary>
    [Fact]
    public void RuleForAllDecimals_ShouldSetAllDecimalProperties()
    {
        var builder = new FakeBuilder<TestModel>()
            .RuleForAllDecimals(() => 123.45m);

        var model = builder.Build();

        Assert.Equal(123.45m, model.Price);
        Assert.Equal(123.45m, model.OptionalPrice);
    }

    /// <summary>
    /// Test: RuleForAllDateTimes should set all DateTime properties
    /// </summary>
    [Fact]
    public void RuleForAllDateTimes_ShouldSetAllDateTimeProperties()
    {
        var now = DateTime.Now;
        var builder = new FakeBuilder<TestModel>()
            .RuleForAllDateTimes(() => now);

        var model = builder.Build();

        Assert.Equal(now, model.CreatedDate);
        Assert.Equal(now, model.UpdatedDate);
    }

    /// <summary>
    /// Test: Fluent chain should work correctly
    /// </summary>
    [Fact]
    public void FluentChain_ShouldWorkCorrectly()
    {
        var model = new FakeBuilder<TestModel>()
            .RuleFor(x => x.FirstName, () => "علی")
            .RuleFor(x => x.LastName, () => "محمدی")
            .RuleFor(x => x.Email, () => "ali@example.com")
            .RuleFor(x => x.Mobile, () => "09121234567")
            .RuleFor(x => x.Age, () => 30)
            .RuleFor(x => x.IsActive, () => true)
            .Build();

        Assert.Equal("علی", model.FirstName);
        Assert.Equal("محمدی", model.LastName);
        Assert.Equal("ali@example.com", model.Email);
        Assert.Equal("09121234567", model.Mobile);
        Assert.Equal(30, model.Age);
        Assert.True(model.IsActive);
    }

    /// <summary>
    /// Test: RuleFor should override previous rule
    /// </summary>
    [Fact]
    public void RuleFor_ShouldOverridePreviousRule()
    {
        var builder = new FakeBuilder<TestModel>()
            .RuleFor(x => x.FirstName, () => "احمد")
            .RuleFor(x => x.FirstName, () => "علی");

        var model = builder.Build();

        Assert.Equal("علی", model.FirstName);
    }

    /// <summary>
    /// Test: Build should not modify the builder
    /// </summary>
    [Fact]
    public void Build_ShouldNotModifyBuilder()
    {
        var builder = new FakeBuilder<TestModel>()
            .RuleFor(x => x.FirstName, () => PersianNameGenerator.FirstName());

        var model1 = builder.Build();
        var model2 = builder.Build();

        Assert.NotEqual(model1.FirstName, model2.FirstName);
    }

    /// <summary>
    /// Test: RuleForAllStrings combined with RuleFor should work correctly
    /// </summary>
    [Fact]
    public void CombinedRules_ShouldWorkCorrectly()
    {
        var builder = new FakeBuilder<TestModel>()
            .RuleForAllStrings(() => "تست")
            .RuleFor(x => x.Email, () => "custom@example.com");

        var model = builder.Build();

        Assert.Equal("custom@example.com", model.Email);
        Assert.Equal("تست", model.FirstName);
        Assert.Equal("تست", model.LastName);
    }

    /// <summary>
    /// Test: BuildList(0) should return empty list
    /// </summary>
    [Fact]
    public void BuildList_WithZeroCount_ShouldReturnEmptyList()
    {
        var builder = new FakeBuilder<TestModel>();

        var models = builder.BuildList(0);

        Assert.Empty(models);
    }

    /// <summary>
    /// Test: BuildList should return list with different instances
    /// </summary>
    [Fact]
    public void BuildList_ShouldReturnListWithDifferentInstances()
    {
        var builder = new FakeBuilder<TestModel>()
            .RuleFor(x => x.FirstName, () => PersianNameGenerator.FirstName())
            .RuleFor(x => x.Age, () => new Random().Next(1, 100));

        var models = builder.BuildList(3);

        Assert.NotSame(models[0], models[1]);
        Assert.NotSame(models[1], models[2]);
    }

    /// <summary>
    /// Test: Large BuildList should work correctly
    /// </summary>
    [Fact]
    public void BuildList_WithLargeCount_ShouldWorkCorrectly()
    {
        var builder = new FakeBuilder<TestModel>()
            .RuleFor(x => x.FirstName, () => PersianNameGenerator.FirstName());

        var models = builder.BuildList(100);

        Assert.Equal(100, models.Count);
        Assert.All(models, m => Assert.NotEmpty(m.FirstName));
    }

    /// <summary>
    /// Test: FakeBuilder should handle null values
    /// </summary>
    [Fact]
    public void RuleFor_ShouldHandleNullValues()
    {
        var builder = new FakeBuilder<TestModel>()
            .RuleFor(x => x.FirstName, () => null);

        var model = builder.Build();

        Assert.Null(model.FirstName);
    }

    /// <summary>
    /// Test: FakeBuilder with complex object creation
    /// </summary>
    [Fact]
    public void RuleFor_ShouldHandleComplexObjects()
    {
        var builder = new FakeBuilder<FakeUser>()
            .RuleFor(x => x.Id, () => InternetCryptoGenerator.GuidString())
            .RuleFor(x => x.FirstName, () => PersianNameGenerator.FirstName())
            .RuleFor(x => x.LastName, () => PersianNameGenerator.LastName())
            .RuleFor(x => x.Email, () => PersianTextGenerator.Email())
            .RuleFor(x => x.Mobile, () => IranianMobileGenerator.Mobile());

        var user = builder.Build();

        Assert.NotEmpty(user.Id);
        Assert.NotEmpty(user.FirstName);
        Assert.NotEmpty(user.LastName);
        Assert.NotEmpty(user.Email);
        Assert.NotEmpty(user.Mobile);
    }
}
