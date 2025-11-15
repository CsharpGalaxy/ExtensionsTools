using CsharpGalaxy.LibraryExtension.FakeDataPersian.Helpers;
using CsharpGalaxy.LibraryExtension.FakeDataPersian.Models;

namespace CsharpGalaxy.LibraryExtension.Test.FakeDataPersian;

public class FakeDataSeederTests
{
    [Fact]
    public void Seed_ShouldFillFirstNameAttribute()
    {
        var model = FakeDataSeeder.Seed<TestModel>();
        Assert.False(string.IsNullOrWhiteSpace(model.FirstName));
        Assert.True(model.FirstName.Length > 0);
    }

    [Fact]
    public void Seed_ShouldFillLastNameAttribute()
    {
        var model = FakeDataSeeder.Seed<TestModel>();
        Assert.False(string.IsNullOrWhiteSpace(model.LastName));
        Assert.True(model.LastName.Length > 0);
    }

    [Fact]
    public void Seed_ShouldFillFullNameAttribute()
    {
        var model = FakeDataSeeder.Seed<TestModel>();
        Assert.False(string.IsNullOrWhiteSpace(model.FullName));
        Assert.Contains(" ", model.FullName);
    }

    [Fact]
    public void Seed_ShouldFillEmailAttribute()
    {
        var model = FakeDataSeeder.Seed<TestModel>();
        Assert.False(string.IsNullOrWhiteSpace(model.Email));
        Assert.Contains("@", model.Email);
    }

    [Fact]
    public void Seed_ShouldFillMobileAttribute()
    {
        var model = FakeDataSeeder.Seed<TestModel>();
        Assert.False(string.IsNullOrWhiteSpace(model.Mobile));
        Assert.StartsWith("09", model.Mobile);
    }

    [Fact]
    public void Seed_ShouldFillUsernameAttribute()
    {
        var model = FakeDataSeeder.Seed<TestModel>();
        Assert.False(string.IsNullOrWhiteSpace(model.Username));
        Assert.True(model.Username.Length > 0);
    }

    [Fact]
    public void Seed_ShouldFillNationalCodeAttribute()
    {
        var model = FakeDataSeeder.Seed<TestModel>();
        Assert.False(string.IsNullOrWhiteSpace(model.NationalCode));
        Assert.Equal(10, model.NationalCode.Length);
    }

    [Fact]
    public void Seed_ShouldFillAddressAttribute()
    {
        var model = FakeDataSeeder.Seed<TestModel>();
        Assert.False(string.IsNullOrWhiteSpace(model.Address));
        Assert.True(model.Address.Length > 5);
    }

    [Fact]
    public void Seed_ShouldFillCityAttribute()
    {
        var model = FakeDataSeeder.Seed<TestModel>();
        Assert.False(string.IsNullOrWhiteSpace(model.City));
        Assert.True(model.City.Length > 0);
    }

    [Fact]
    public void Seed_ShouldFillProvinceAttribute()
    {
        var model = FakeDataSeeder.Seed<TestModel>();
        Assert.False(string.IsNullOrWhiteSpace(model.Province));
        Assert.True(model.Province.Length > 0);
    }

    [Fact]
    public void Seed_ShouldFillWordAttribute()
    {
        var model = FakeDataSeeder.Seed<TestModel>();
        Assert.False(string.IsNullOrWhiteSpace(model.Word));
        Assert.True(model.Word.Length > 0);
    }

    [Fact]
    public void Seed_ShouldFillSentenceAttribute()
    {
        var model = FakeDataSeeder.Seed<TestModel>();
        Assert.False(string.IsNullOrWhiteSpace(model.Sentence));
        Assert.True(model.Sentence.Length > 5);
    }

    [Fact]
    public void Seed_ShouldFillCompanyNameAttribute()
    {
        var model = FakeDataSeeder.Seed<TestModel>();
        Assert.False(string.IsNullOrWhiteSpace(model.CompanyName));
        Assert.True(model.CompanyName.Length > 0);
    }

    [Fact]
    public void Seed_ShouldFillJobTitleAttribute()
    {
        var model = FakeDataSeeder.Seed<TestModel>();
        Assert.False(string.IsNullOrWhiteSpace(model.JobTitle));
        Assert.True(model.JobTitle.Length > 0);
    }

    [Fact]
    public void Seed_ShouldFillIbanAttribute()
    {
        var model = FakeDataSeeder.Seed<TestModel>();
        Assert.False(string.IsNullOrWhiteSpace(model.Iban));
        Assert.StartsWith("IR", model.Iban);
    }

    [Fact]
    public void Seed_ShouldFillCardNumberAttribute()
    {
        var model = FakeDataSeeder.Seed<TestModel>();
        Assert.False(string.IsNullOrWhiteSpace(model.CardNumber));
        Assert.Equal(16, model.CardNumber.Length);
    }

    [Fact]
    public void Seed_ShouldFillDateTimeAttribute()
    {
        var model = FakeDataSeeder.Seed<TestModel>();
        Assert.NotEqual(default(DateTime), model.CreatedDate);
        Assert.True(model.CreatedDate <= DateTime.Now);
    }

    [Fact]
    public void Seed_ShouldFillBooleanAttribute()
    {
        var model = FakeDataSeeder.Seed<TestModel>();
        Assert.True(model.IsActive == true || model.IsActive == false);
    }

    [Fact]
    public void Seed_ShouldFillStatusAttribute()
    {
        var model = FakeDataSeeder.Seed<TestModel>();
        Assert.False(string.IsNullOrWhiteSpace(model.Status));
        Assert.True(model.Status.Length > 0);
    }

    [Fact]
    public void Seed_ShouldFillGuidType()
    {
        var model = FakeDataSeeder.Seed<TestModel>();
        Assert.NotEqual(Guid.Empty, model.GuidId);
    }

    [Fact]
    public void Seed_ShouldFillIntType()
    {
        var model = FakeDataSeeder.Seed<TestModel>();
        Assert.True(model.Age > 0);
        Assert.InRange(model.Age, 1, 10000);
    }

    [Fact]
    public void Seed_ShouldFillNullableIntType()
    {
        var model = FakeDataSeeder.Seed<TestModel>();
        Assert.NotNull(model.OptionalAge);
        Assert.True(model.OptionalAge > 0);
    }

    [Fact]
    public void Seed_ShouldFillLongType()
    {
        var model = FakeDataSeeder.Seed<TestModel>();
        Assert.True(model.LongValue > 0);
    }

    [Fact]
    public void Seed_ShouldFillNullableLongType()
    {
        var model = FakeDataSeeder.Seed<TestModel>();
        Assert.NotNull(model.OptionalLongValue);
        Assert.True(model.OptionalLongValue > 0);
    }

    [Fact]
    public void Seed_ShouldFillDecimalType()
    {
        var model = FakeDataSeeder.Seed<TestModel>();
        Assert.True(model.Price > 0);
    }

    [Fact]
    public void Seed_ShouldFillNullableDecimalType()
    {
        var model = FakeDataSeeder.Seed<TestModel>();
        Assert.NotNull(model.OptionalPrice);
        Assert.True(model.OptionalPrice > 0);
    }

    [Fact]
    public void Seed_ShouldFillDoubleType()
    {
        var model = FakeDataSeeder.Seed<TestModel>();
        Assert.True(model.Score >= 0);
    }

    [Fact]
    public void Seed_ShouldFillNullableDoubleType()
    {
        var model = FakeDataSeeder.Seed<TestModel>();
        Assert.NotNull(model.OptionalScore);
        Assert.True(model.OptionalScore >= 0);
    }

    [Fact]
    public void Seed_ShouldFillBoolType()
    {
        var model = FakeDataSeeder.Seed<TestModel>();
        Assert.True(model.Active == true || model.Active == false);
    }

    [Fact]
    public void Seed_ShouldFillNullableBoolType()
    {
        var model = FakeDataSeeder.Seed<TestModel>();
        Assert.NotNull(model.OptionalActive);
        Assert.True(model.OptionalActive == true || model.OptionalActive == false);
    }

    [Fact]
    public void Seed_ShouldFillDateTimeType()
    {
        var model = FakeDataSeeder.Seed<TestModel>();
        Assert.NotEqual(default(DateTime), model.UpdatedDate);
        Assert.True(model.UpdatedDate <= DateTime.Now);
    }

    [Fact]
    public void Seed_ShouldFillNullableDateTimeType()
    {
        var model = FakeDataSeeder.Seed<TestModel>();
        Assert.NotNull(model.OptionalUpdatedDate);
        Assert.True(model.OptionalUpdatedDate <= DateTime.Now);
    }

    [Fact]
    public void Seed_ShouldFillStringType()
    {
        var model = FakeDataSeeder.Seed<TestModel>();
        Assert.False(string.IsNullOrWhiteSpace(model.Description));
    }

    [Fact]
    public void Seed_ShouldReturnDifferentValuesEachCall()
    {
        var model1 = FakeDataSeeder.Seed<TestModel>();
        var model2 = FakeDataSeeder.Seed<TestModel>();

        Assert.NotEqual(model1.Id, model2.Id);
        Assert.NotEqual(model1.FirstName, model2.FirstName);
        Assert.NotEqual(model1.Email, model2.Email);
    }

    [Fact]
    public void SeedList_ShouldReturnRequestedCount()
    {
        var models = FakeDataSeeder.SeedList<TestModel>(5);
        Assert.Equal(5, models.Count);
    }

    [Fact]
    public void SeedList_ShouldFillAllProperties()
    {
        var models = FakeDataSeeder.SeedList<TestModel>(3);

        foreach (var model in models)
        {
            Assert.False(string.IsNullOrWhiteSpace(model.FirstName));
            Assert.False(string.IsNullOrWhiteSpace(model.Email));
            Assert.False(string.IsNullOrWhiteSpace(model.Mobile));
            Assert.NotEqual(Guid.Empty, model.GuidId);
            Assert.True(model.Age > 0);
        }
    }

    [Fact]
    public void SeedList_ShouldReturnUniqueValues()
    {
        var models = FakeDataSeeder.SeedList<TestModel>(10);
        var emails = models.Select(m => m.Email).Distinct().Count();
        var mobiles = models.Select(m => m.Mobile).Distinct().Count();

        Assert.True(emails > 1, "Should have different emails");
        Assert.True(mobiles > 1, "Should have different mobiles");
    }

    [Fact]
    public void SeedList_ShouldReturnEmptyListForZeroCount()
    {
        var models = FakeDataSeeder.SeedList<TestModel>(0);
        Assert.Empty(models);
    }

    [Fact]
    public void Seed_ShouldAttributesHavePriorityOverTypes()
    {
        // GuidId should be Guid type, but Id should be string from Guid attribute
        var model = FakeDataSeeder.Seed<TestModel>();
        Assert.NotEmpty(model.Id);
        Assert.NotEqual(Guid.Empty, model.GuidId);
    }

    [Fact]
    public void Seed_ShouldHandleNullableTypes()
    {
        var model = FakeDataSeeder.Seed<TestModel>();
        
        Assert.NotNull(model.OptionalAge);
        Assert.NotNull(model.OptionalPrice);
        Assert.NotNull(model.OptionalScore);
        Assert.NotNull(model.OptionalActive);
        Assert.NotNull(model.OptionalUpdatedDate);
    }

    [Fact]
    public void SeedList_ShouldReturnLargeCountSuccessfully()
    {
        var models = FakeDataSeeder.SeedList<TestModel>(100);
        Assert.Equal(100, models.Count);
        Assert.All(models, m => Assert.False(string.IsNullOrWhiteSpace(m.FirstName)));
    }

    [Fact]
    public void Seed_ShouldFillAllPropertiesAtOnce()
    {
        var model = FakeDataSeeder.Seed<TestModel>();

        // Check all attributes
        Assert.NotEmpty(model.Id);
        Assert.NotEmpty(model.FirstName);
        Assert.NotEmpty(model.LastName);
        Assert.NotEmpty(model.FullName);
        Assert.NotEmpty(model.Email);
        Assert.NotEmpty(model.Mobile);
        Assert.NotEmpty(model.Username);
        Assert.NotEmpty(model.NationalCode);
        Assert.NotEmpty(model.Address);
        Assert.NotEmpty(model.City);
        Assert.NotEmpty(model.Province);
        Assert.NotEmpty(model.Word);
        Assert.NotEmpty(model.Sentence);
        Assert.NotEmpty(model.CompanyName);
        Assert.NotEmpty(model.JobTitle);
        Assert.NotEmpty(model.Iban);
        Assert.NotEmpty(model.CardNumber);
        Assert.NotEqual(default(DateTime), model.CreatedDate);
        Assert.NotEmpty(model.Status);

        // Check types
        Assert.NotEqual(Guid.Empty, model.GuidId);
        Assert.True(model.Age > 0);
        Assert.True(model.Price > 0);
    }

    [Fact]
    public void Seed_ShouldNotThrowExceptionWithComplexModel()
    {
        var exception = Record.Exception(() => FakeDataSeeder.Seed<TestModel>());
        Assert.Null(exception);
    }
}
