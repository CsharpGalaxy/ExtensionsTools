using CsharpGalaxy.LibraryExtension.FakeDataPersian.Generators;

namespace CsharpGalaxy.LibraryExtension.Test.FakeDataPersian;

public class PersianNameGeneratorTests
{
    [Fact]
    public void FirstName_ShouldReturnNonEmptyString()
    {
        var firstName = PersianNameGenerator.FirstName();
        Assert.False(string.IsNullOrWhiteSpace(firstName));
        Assert.True(firstName.Length > 0);
    }

    [Fact]
    public void LastName_ShouldReturnNonEmptyString()
    {
        var lastName = PersianNameGenerator.LastName();
        Assert.False(string.IsNullOrWhiteSpace(lastName));
        Assert.True(lastName.Length > 0);
    }
    [Fact]
    public void UserName_ShouldReturnNonEmptyString()
    {
        var userName = PersianNameGenerator.UserName();
        Assert.False(string.IsNullOrWhiteSpace(userName));
        Assert.True(userName.Length > 0);
    }

    [Fact]
    public void FullName_ShouldContainFirstAndLastName()
    {
        var fullName = PersianNameGenerator.FullName();
        Assert.False(string.IsNullOrWhiteSpace(fullName));
        Assert.Contains(" ", fullName);
    }

    [Fact]
    public void FatherName_ShouldReturnNonEmptyString()
    {
        var fatherName = PersianNameGenerator.FatherName();
        Assert.False(string.IsNullOrWhiteSpace(fatherName));
        Assert.True(fatherName.Length > 0);
    }

    [Fact]
    public void FirstName_ShouldReturnDifferentValues()
    {
        var names = new HashSet<string>();
        for (int i = 0; i < 10; i++)
        {
            names.Add(PersianNameGenerator.FirstName());
        }
        Assert.True(names.Count > 1, "Should generate different first names");
    }

    [Fact]
    public void FullName_ShouldReturnDifferentValues()
    {
        var names = new HashSet<string>();
        for (int i = 0; i < 10; i++)
        {
            names.Add(PersianNameGenerator.FullName());
        }
        Assert.True(names.Count > 1, "Should generate different full names");
    }

    [Fact]
    public void FullName_ShouldHaveTwoOrMoreParts()
    {
        var fullName = PersianNameGenerator.FullName();
        var parts = fullName.Split(' ');
        Assert.True(parts.Length >= 2, "Full name should have at least first name and last name");
    }
}
