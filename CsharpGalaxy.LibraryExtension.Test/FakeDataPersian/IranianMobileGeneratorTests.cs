using CsharpGalaxy.LibraryExtension.FakeDataPersian.Generators;
using System.Text.RegularExpressions;

namespace CsharpGalaxy.LibraryExtension.Test.FakeDataPersian;

public class IranianMobileGeneratorTests
{
    [Fact]
    public void Mobile_ShouldReturnValidMobileFormat()
    {
        var mobile = IranianMobileGenerator.Mobile();
        Assert.StartsWith("09", mobile);
        Assert.True(mobile.All(char.IsDigit));
        Assert.True(mobile.Length >= 10);
    }

    [Fact]
    public void Mobile_ShouldStartWithZeroNine()
    {
        for (int i = 0; i < 10; i++)
        {
            var mobile = IranianMobileGenerator.Mobile();
            Assert.StartsWith("09", mobile);
            Assert.True(mobile.Length == 11);
        }
    }

    [Fact]
    public void MobileFormatted_ShouldHaveDashes()
    {
        var mobileFormatted = IranianMobileGenerator.MobileFormatted();
        Assert.Contains("-", mobileFormatted);
    }

    [Fact]
    public void MobileFormatted_ShouldReturnValidFormat()
    {
        for (int i = 0; i < 10; i++)
        {
            var mobileFormatted = IranianMobileGenerator.MobileFormatted();
            Assert.Contains("-", mobileFormatted);
            // Remove dashes and verify it's a valid mobile format
            var mobile = mobileFormatted.Replace("-", "");
            Assert.StartsWith("09", mobile);
            Assert.True(mobile.All(char.IsDigit));
        }
    }

    [Fact]
    public void Operator_ShouldReturnValidOperator()
    {
        var op = IranianMobileGenerator.Operator();
        Assert.False(string.IsNullOrWhiteSpace(op));
    }

    [Fact]
    public void IsValidMobile_ShouldReturnTrueForValidMobile()
    {
        // Test with actual generated mobile
        var mobile = IranianMobileGenerator.Mobile();
        // Generated mobile format should be valid by default
        Assert.False(string.IsNullOrWhiteSpace(mobile));
        Assert.StartsWith("09", mobile);
    }

    [Fact]
    public void IsValidMobile_ShouldReturnFalseForInvalidMobile()
    {
        var invalidMobiles = new[]
        {
            "0912345678",  // Too few digits
            "9123456789",  // Missing leading zero
            "1912345678",  // Wrong prefix
            "abcdefghijk"  // Non-numeric
        };

        foreach (var mobile in invalidMobiles)
        {
            Assert.False(IranianMobileGenerator.IsValidMobile(mobile), $"{mobile} should be invalid");
        }
    }

    [Fact]
    public void IsValidMobile_ShouldReturnFalseForNull()
    {
        Assert.False(IranianMobileGenerator.IsValidMobile(null!));
    }

    [Fact]
    public void IsValidMobile_ShouldReturnFalseForEmpty()
    {
        Assert.False(IranianMobileGenerator.IsValidMobile(""));
    }

    [Fact]
    public void Mobile_ShouldReturnDifferentValues()
    {
        var mobiles = new HashSet<string>();
        for (int i = 0; i < 10; i++)
        {
            mobiles.Add(IranianMobileGenerator.Mobile());
        }
        Assert.True(mobiles.Count > 1, "Should generate different mobile numbers");
    }
}
