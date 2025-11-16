using CsharpGalaxy.LibraryExtension.FakeDataPersian.Generators;
using Xunit;
using static CsharpGalaxy.LibraryExtension.FakeDataPersian.Generators.PersianDateGenerator;

namespace CsharpGalaxy.LibraryExtension.Test.FakeDataPersian;

public class PersianDateGeneratorExtendedTests
{
    [Fact]
    public void GetDayNameFarsi_ShouldReturnValidPersianDayName()
    {
        var dayName = PersianDateGenerator.GetDayNameFarsi();
        var validDays = new[] { "شنبه", "یکشنبه", "دوشنبه", "سه‌شنبه", "چهارشنبه", "پنج‌شنبه", "جمعه" };
        
        Assert.Contains(dayName, validDays);
    }

    [Theory]
    [InlineData(1, "فروردین")]
    [InlineData(2, "اردیبهشت")]
    [InlineData(3, "خرداد")]
    [InlineData(12, "اسفند")]
    public void GetMonthNameFarsi_ShouldReturnCorrectMonthName(int month, string expected)
    {
        var monthName = PersianDateGenerator.GetMonthNameFarsi(month);
        Assert.Equal(expected, monthName);
    }

    [Fact]
    public void GetRandomMonthNameFarsi_ShouldReturnValidPersianMonthName()
    {
        var monthName = PersianDateGenerator.GetRandomMonthNameFarsi();
        var validMonths = new[] { "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", 
                                  "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند" };
        
        Assert.Contains(monthName, validMonths);
    }

    [Fact]
    public void GetRandomShamsiYear_ShouldReturnValidYear()
    {
        var year = PersianDateGenerator.GetRandomShamsiYear();
        Assert.True(year >= 1380 && year <= 1410);
    }

    [Fact]
    public void GetRandomShamsiYear_WithCustomRange_ShouldReturnValidYear()
    {
        var year = PersianDateGenerator.GetRandomShamsiYear(1400, 1405);
        Assert.True(year >= 1400 && year <= 1405);
    }

    [Fact]
    public void GetRandomShamsiMonth_ShouldReturnValidMonth()
    {
        var month = PersianDateGenerator.GetRandomShamsiMonth();
        Assert.True(month >= 1 && month <= 12);
    }

    [Fact]
    public void GetRandomShamsiDay_ShouldReturnValidDay()
    {
        var day = PersianDateGenerator.GetRandomShamsiDay();
        Assert.True(day >= 1 && day <= 30);
    }

    [Theory]
    [InlineData(1400, 1, 31)]
    [InlineData(1400, 6, 31)]
    [InlineData(1400, 7, 30)]
    [InlineData(1400, 11, 30)]
    public void GetDaysInMonth_ShouldReturnCorrectDays(int year, int month, int expectedDays)
    {
        var days = PersianDateGenerator.GetDaysInMonth(year, month);
        Assert.Equal(expectedDays, days);
    }

    [Theory]
    [InlineData(1400, 366)]
    [InlineData(1403, 366)]
    public void GetDaysInYear_ShouldReturnCorrectDays(int year, int expectedDays)
    {
        var days = PersianDateGenerator.GetDaysInYear(year);
        Assert.Equal(expectedDays, days);
    }

    [Theory]
    [InlineData(1399, true)]
    [InlineData(1400, true)]
    public void IsLeapYear_ShouldReturnCorrectValue(int year, bool expectedIsLeap)
    {
        var isLeap = PersianDateGenerator.IsLeapYear(year);
        Assert.Equal(expectedIsLeap, isLeap);
    }

    [Fact]
    public void GetDayOfYear_ShouldReturnCorrectValue()
    {
        var dayOfYear = PersianDateGenerator.GetDayOfYear(1400, 1, 1);
        Assert.Equal(1, dayOfYear);

        dayOfYear = PersianDateGenerator.GetDayOfYear(1400, 2, 1);
        Assert.Equal(32, dayOfYear);
    }

    [Fact]
    public void GetWeekOfYear_ShouldReturnCorrectValue()
    {
        var weekOfYear = PersianDateGenerator.GetWeekOfYear(1400, 1, 1);
        Assert.Equal(1, weekOfYear);
    }

    [Fact]
    public void MultipleCallsToGetDayNameFarsi_ShouldReturnDifferentValues()
    {
        var names = new HashSet<string>();
        for (int i = 0; i < 100; i++)
        {
            names.Add(PersianDateGenerator.GetDayNameFarsi());
        }

        Assert.True(names.Count > 1);
    }
}
