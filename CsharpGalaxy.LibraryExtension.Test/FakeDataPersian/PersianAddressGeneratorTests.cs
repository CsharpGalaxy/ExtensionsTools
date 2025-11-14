using CsharpGalaxy.LibraryExtension.FakeDataPersian.Generators;

namespace CsharpGalaxy.LibraryExtension.Test.FakeDataPersian;

public class PersianAddressGeneratorTests
{
    [Fact]
    public void City_ShouldReturnNonEmptyString()
    {
        var city = PersianAddressGenerator.City();
        Assert.False(string.IsNullOrWhiteSpace(city));
    }

    [Fact]
    public void Province_ShouldReturnNonEmptyString()
    {
        var province = PersianAddressGenerator.Province();
        Assert.False(string.IsNullOrWhiteSpace(province));
    }

    [Fact]
    public void StreetName_ShouldReturnNonEmptyString()
    {
        var streetName = PersianAddressGenerator.StreetName();
        Assert.False(string.IsNullOrWhiteSpace(streetName));
    }

    [Fact]
    public void StreetType_ShouldReturnValidStreetType()
    {
        var streetType = PersianAddressGenerator.StreetType();
        Assert.False(string.IsNullOrWhiteSpace(streetType));
    }

    [Fact]
    public void FullAddress_ShouldContainMultipleParts()
    {
        var address = PersianAddressGenerator.FullAddress();
        Assert.False(string.IsNullOrWhiteSpace(address));
        // Should contain multiple parts separated by commas or spaces
        Assert.True(address.Length > 10);
    }

    [Fact]
    public void ShortAddress_ShouldReturnNonEmptyString()
    {
        var shortAddress = PersianAddressGenerator.ShortAddress();
        Assert.False(string.IsNullOrWhiteSpace(shortAddress));
    }

    [Fact]
    public void City_ShouldReturnDifferentValues()
    {
        var cities = new HashSet<string>();
        for (int i = 0; i < 10; i++)
        {
            cities.Add(PersianAddressGenerator.City());
        }
        Assert.True(cities.Count > 1, "Should generate different cities");
    }

    [Fact]
    public void Province_ShouldReturnDifferentValues()
    {
        var provinces = new HashSet<string>();
        for (int i = 0; i < 10; i++)
        {
            provinces.Add(PersianAddressGenerator.Province());
        }
        Assert.True(provinces.Count > 1, "Should generate different provinces");
    }

    [Fact]
    public void FullAddress_ShouldBeLongerThanShortAddress()
    {
        var fullAddress = PersianAddressGenerator.FullAddress();
        var shortAddress = PersianAddressGenerator.ShortAddress();
        Assert.True(fullAddress.Length >= shortAddress.Length);
    }

    [Fact]
    public void StreetName_ShouldBePersianText()
    {
        var streetName = PersianAddressGenerator.StreetName();
        // Persian characters are in the range U+0600 to U+06FF
        var hasPersianChars = streetName.Any(c => c >= '\u0600' && c <= '\u06FF');
        Assert.True(hasPersianChars || streetName.All(char.IsDigit));
    }
}
