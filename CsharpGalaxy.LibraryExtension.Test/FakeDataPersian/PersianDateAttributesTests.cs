using CsharpGalaxy.LibraryExtension.FakeDataPersian.Generators;
using CsharpGalaxy.LibraryExtension.FakeDataPersian.Attributes;
using CsharpGalaxy.LibraryExtension.FakeDataPersian.Helpers;
using Xunit;

namespace CsharpGalaxy.LibraryExtension.Test.FakeDataPersian;

// ===== Test Models =====

public class ModelWithPersianDateAttribute
{
    public string Id { get; set; }
    
    [PersianDate]
    public PersianDateGenerator.PersianDateTime PublishedDate { get; set; }
}

public class ModelWithPersianDayNameAttribute
{
    [PersianDayName]
    public string DayName { get; set; }
}

public class ModelWithPersianMonthNameAttribute
{
    [PersianMonthName]
    public string MonthName { get; set; }
}

public class ModelWithPersianYearAttribute
{
    [PersianYear]
    public int Year { get; set; }
}

public class ModelWithPersianYearAttributeCustomRange
{
    [PersianYear(1400, 1405)]
    public int Year { get; set; }
}

public class ModelWithPersianDateRangeAttribute
{
    [PersianDateRange("1400/01/01", "1402/12/29")]
    public PersianDateGenerator.PersianDateTime DateInRange { get; set; }
}

public class ComplexModelWithMultiplePersianAttributes
{
    [Guid]
    public string Id { get; set; }
    
    [PersianDayName]
    public string PublishedDay { get; set; }
    
    [PersianMonthName]
    public string PublishedMonth { get; set; }
    
    [PersianYear]
    public int PublishedYear { get; set; }
    
    [PersianDate]
    public PersianDateGenerator.PersianDateTime FullDate { get; set; }
    
    [PersianDateRange("1400/01/01", "1402/12/29")]
    public PersianDateGenerator.PersianDateTime UpdatedDate { get; set; }
}

// ===== Tests =====

public class PersianDateAttributeTests
{
    [Fact]
    public void PersianDateAttribute_ShouldGeneratePersianDateTime()
    {
        var model = FakeDataSeeder.Seed<ModelWithPersianDateAttribute>();
        
        Assert.NotNull(model);
        Assert.NotNull(model.PublishedDate);
        // Year should be reasonable (1300-1500 Shamsi)
        Assert.True(model.PublishedDate.Year > 0, "Year should be positive");
        Assert.InRange(model.PublishedDate.Month, 1, 12);
        Assert.InRange(model.PublishedDate.Day, 1, 31);
    }

    [Fact]
    public void PersianDateAttribute_ShouldGenerateDifferentDatesOnMultipleCalls()
    {
        var dates = new HashSet<string>();
        for (int i = 0; i < 20; i++)
        {
            var model = FakeDataSeeder.Seed<ModelWithPersianDateAttribute>();
            dates.Add(model.PublishedDate.ToString());
        }
        
        Assert.True(dates.Count > 1, "Should generate different Persian dates");
    }

    [Fact]
    public void PersianDayNameAttribute_ShouldGenerateValidDayName()
    {
        var model = FakeDataSeeder.Seed<ModelWithPersianDayNameAttribute>();
        var validDays = new[] { "شنبه", "یکشنبه", "دوشنبه", "سه‌شنبه", "چهارشنبه", "پنج‌شنبه", "جمعه" };
        
        Assert.NotNull(model.DayName);
        Assert.Contains(model.DayName, validDays);
    }

    [Fact]
    public void PersianDayNameAttribute_ShouldGenerateDifferentDaysOnMultipleCalls()
    {
        var days = new HashSet<string>();
        for (int i = 0; i < 50; i++)
        {
            var model = FakeDataSeeder.Seed<ModelWithPersianDayNameAttribute>();
            days.Add(model.DayName);
        }
        
        Assert.True(days.Count > 1, "Should generate different day names");
    }

    [Fact]
    public void PersianMonthNameAttribute_ShouldGenerateValidMonthName()
    {
        var model = FakeDataSeeder.Seed<ModelWithPersianMonthNameAttribute>();
        var validMonths = new[] 
        { 
            "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", 
            "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند" 
        };
        
        Assert.NotNull(model.MonthName);
        Assert.Contains(model.MonthName, validMonths);
    }

    [Fact]
    public void PersianMonthNameAttribute_ShouldGenerateDifferentMonthsOnMultipleCalls()
    {
        var months = new HashSet<string>();
        for (int i = 0; i < 50; i++)
        {
            var model = FakeDataSeeder.Seed<ModelWithPersianMonthNameAttribute>();
            months.Add(model.MonthName);
        }
        
        Assert.True(months.Count > 1, "Should generate different month names");
    }

    [Fact]
    public void PersianYearAttribute_ShouldGenerateYearInDefaultRange()
    {
        var model = FakeDataSeeder.Seed<ModelWithPersianYearAttribute>();
        
        Assert.NotNull(model);
        Assert.InRange(model.Year, 1380, 1410);
    }

    [Fact]
    public void PersianYearAttribute_ShouldGenerateDifferentYearsOnMultipleCalls()
    {
        var years = new HashSet<int>();
        for (int i = 0; i < 20; i++)
        {
            var model = FakeDataSeeder.Seed<ModelWithPersianYearAttribute>();
            years.Add(model.Year);
        }
        
        Assert.True(years.Count > 1, "Should generate different years");
    }

    [Fact]
    public void PersianYearAttribute_WithCustomRange_ShouldGenerateYearInCustomRange()
    {
        var model = FakeDataSeeder.Seed<ModelWithPersianYearAttributeCustomRange>();
        
        Assert.NotNull(model);
        Assert.InRange(model.Year, 1400, 1405);
    }

    [Fact]
    public void PersianYearAttribute_WithCustomRange_ShouldRespectMinAndMaxBoundaries()
    {
        for (int i = 0; i < 20; i++)
        {
            var model = FakeDataSeeder.Seed<ModelWithPersianYearAttributeCustomRange>();
            Assert.InRange(model.Year, 1400, 1405);
        }
    }

    [Fact]
    public void PersianDateRangeAttribute_ShouldGenerateDateInRange()
    {
        var model = FakeDataSeeder.Seed<ModelWithPersianDateRangeAttribute>();
        
        Assert.NotNull(model.DateInRange);
        Assert.InRange(model.DateInRange.Year, 1400, 1410);
        Assert.InRange(model.DateInRange.Month, 1, 12);
        Assert.InRange(model.DateInRange.Day, 1, 30);
    }

    [Fact]
    public void PersianDateRangeAttribute_ShouldGenerateDifferentDatesInRange()
    {
        var dates = new HashSet<string>();
        for (int i = 0; i < 20; i++)
        {
            var model = FakeDataSeeder.Seed<ModelWithPersianDateRangeAttribute>();
            dates.Add(model.DateInRange.ToString());
        }
        
        Assert.True(dates.Count > 1, "Should generate different dates in range");
    }

    [Fact]
    public void ComplexModel_WithMultiplePersianAttributes_ShouldSeedAllProperties()
    {
        var model = FakeDataSeeder.Seed<ComplexModelWithMultiplePersianAttributes>();
        
        // Guid should be set
        Assert.NotNull(model.Id);
        Assert.False(string.IsNullOrWhiteSpace(model.Id));
        
        // Day name should be valid
        var validDays = new[] { "شنبه", "یکشنبه", "دوشنبه", "سه‌شنبه", "چهارشنبه", "پنج‌شنبه", "جمعه" };
        Assert.Contains(model.PublishedDay, validDays);
        
        // Month name should be valid
        var validMonths = new[] 
        { 
            "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", 
            "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند" 
        };
        Assert.Contains(model.PublishedMonth, validMonths);
        
        // Year should be in default range
        Assert.InRange(model.PublishedYear, 1380, 1410);
        
        // Full date should be valid
        Assert.NotNull(model.UpdatedDate);
        Assert.InRange(model.UpdatedDate.Year, 1380, 1410);
        Assert.InRange(model.UpdatedDate.Month, 1, 12);
        Assert.InRange(model.UpdatedDate.Day, 1, 30);
        
        // Updated date should be in specified range
        Assert.NotNull(model.UpdatedDate);
        Assert.InRange(model.UpdatedDate.Year, 1400, 1402);
    }

    [Fact]
    public void ComplexModel_ShouldSeedMultipleInstancesWithDifferentValues()
    {
        var models = FakeDataSeeder.SeedList<ComplexModelWithMultiplePersianAttributes>(10);
        
        Assert.Equal(10, models.Count);
        
        // Check that different instances have different values
        var ids = models.Select(m => m.Id).Distinct().Count();
        Assert.Equal(10, ids);
        
        var days = models.Select(m => m.PublishedDay).Distinct().Count();
        Assert.True(days > 1, "Multiple instances should have different day names");
        
        var years = models.Select(m => m.PublishedYear).Distinct().Count();
        Assert.True(years > 1, "Multiple instances should have different years");
    }

    [Fact]
    public void PersianDateRangeAttribute_ShouldRespectStartAndEndDate()
    {
        for (int i = 0; i < 30; i++)
        {
            var model = FakeDataSeeder.Seed<ModelWithPersianDateRangeAttribute>();
            
            // Check year is within range (1400-1402)
            Assert.InRange(model.DateInRange.Year, 1400, 1402);
            
            // Check month is valid
            Assert.InRange(model.DateInRange.Month, 1, 12);
            
            // Check day is valid
            Assert.InRange(model.DateInRange.Day, 1, 30);
        }
    }

    [Fact]
    public void PersianDateAttribute_ShouldContainAllRequiredProperties()
    {
        var model = FakeDataSeeder.Seed<ModelWithPersianDateAttribute>();
        
        Assert.NotNull(model.PublishedDate);
        Assert.True(model.PublishedDate.Year > 0, "Year should be set");
        Assert.True(model.PublishedDate.Month > 0, "Month should be set");
        Assert.True(model.PublishedDate.Day > 0, "Day should be set");
        Assert.True(model.PublishedDate.Hour >= 0, "Hour should be set");
        Assert.True(model.PublishedDate.Minute >= 0, "Minute should be set");
        Assert.True(model.PublishedDate.Second >= 0, "Second should be set");
    }

    [Fact]
    public void PersianDateRangeAttribute_ShouldParseStartAndEndDatesCorrectly()
    {
        var model = FakeDataSeeder.Seed<ModelWithPersianDateRangeAttribute>();
        
        // Start date: 1400/01/01
        // End date: 1402/12/29
        
        // Generated date should be after or equal to start date
        Assert.True(
            model.DateInRange.Year > 1400 || 
            (model.DateInRange.Year == 1400 && model.DateInRange.Month >= 1 && model.DateInRange.Day >= 1),
            "Date should be after start date"
        );
        
        // Generated date should be before or equal to end date
        Assert.True(
            model.DateInRange.Year < 1402 || 
            (model.DateInRange.Year == 1402 && model.DateInRange.Month <= 12 && model.DateInRange.Day <= 29),
            "Date should be before end date"
        );
    }
}
