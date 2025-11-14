using CsharpGalaxy.LibraryExtension.FakeDataPersian.Generators;

namespace CsharpGalaxy.LibraryExtension.Test.FakeDataPersian;

public class PersianDateGeneratorTests
{
    [Fact]
    public void ShamsiDate_ShouldReturnNonEmptyString()
    {
        var shamsiDate = PersianDateGenerator.ShamsiDate();
        Assert.False(string.IsNullOrWhiteSpace(shamsiDate));
    }

    [Fact]
    public void ShamsiDate_ShouldFollowProperFormat()
    {
        var shamsiDate = PersianDateGenerator.ShamsiDate();
        // Format should be YYYY/MM/DD
        Assert.Matches(@"^\d{4}/\d{2}/\d{2}$", shamsiDate);
    }

    [Fact]
    public void ShamsiDateTime_ShouldReturnNonEmptyString()
    {
        var shamsiDateTime = PersianDateGenerator.ShamsiDateTime();
        Assert.False(string.IsNullOrWhiteSpace(shamsiDateTime));
    }

    [Fact]
    public void ShamsiDateTime_ShouldIncludeTime()
    {
        var shamsiDateTime = PersianDateGenerator.ShamsiDateTime();
        Assert.Contains(" ", shamsiDateTime); // Should have date and time separated by space
    }

    [Fact]
    public void Age_ShouldReturnValueInRange()
    {
        for (int i = 0; i < 10; i++)
        {
            var age = PersianDateGenerator.Age(18, 65);
            Assert.InRange(age, 18, 65);
        }
    }

    [Fact]
    public void Age_WithMinMax_ShouldRespectBoundaries()
    {
        var ages = new List<int>();
        for (int i = 0; i < 100; i++)
        {
            ages.Add(PersianDateGenerator.Age(20, 50));
        }
        Assert.All(ages, age => Assert.InRange(age, 20, 50));
    }

    [Fact]
    public void BirthDate_ShouldReturnValidDate()
    {
        var birthDate = PersianDateGenerator.BirthDate(30);
        // Should return a date string in Shamsi format
        Assert.Matches(@"^\d{4}/\d{2}/\d{2}$", birthDate);
    }

    [Fact]
    public void BirthDate_ShouldReflectSpecifiedAge()
    {
        var age = 25;
        var birthDate = PersianDateGenerator.BirthDate(age);
        // Extract year from Shamsi date string
        var shamsiYear = int.Parse(birthDate[..4]);
        // Calculate expected Shamsi year
        var currentMiladiYear = DateTime.Now.Year;
        var expectedShamsiYear = currentMiladiYear - age - 621;
        // Allow 1 year tolerance
        Assert.InRange(shamsiYear, expectedShamsiYear - 1, expectedShamsiYear + 1);
    }

    [Fact]
    public void ShamsiDate_ShouldReturnDifferentValues()
    {
        var dates = new HashSet<string>();
        for (int i = 0; i < 10; i++)
        {
            dates.Add(PersianDateGenerator.ShamsiDate());
        }
        Assert.True(dates.Count > 1, "Should generate different dates");
    }

    [Fact]
    public void ShamsiDate_ShouldHaveValidYearRange()
    {
        for (int i = 0; i < 10; i++)
        {
            var shamsiDate = PersianDateGenerator.ShamsiDate();
            var year = int.Parse(shamsiDate[..4]);
            // Shamsi year should be reasonable (around 1300-1410)
            Assert.InRange(year, 1300, 1450);
        }
    }

    [Fact]
    public void Age_ShouldReturnNonNegative()
    {
        for (int i = 0; i < 10; i++)
        {
            var age = PersianDateGenerator.Age(0, 120);
            Assert.True(age >= 0);
        }
    }
}
