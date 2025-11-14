using CsharpGalaxy.LibraryExtension.FakeDataPersian.Generators;

namespace CsharpGalaxy.LibraryExtension.Test.FakeDataPersian;

public class ImageGeneratorTests
{
    [Fact]
    public void PlaceholderBase64_ShouldReturnBase64String()
    {
        var placeholder = ImageGenerator.PlaceholderBase64(100, 100);
        Assert.False(string.IsNullOrWhiteSpace(placeholder));
        // Should be valid base64
        Assert.True(IsValidBase64(placeholder));
    }

    [Fact]
    public void PlaceholderBase64_ShouldContainDataUri()
    {
        var placeholder = ImageGenerator.PlaceholderBase64(100, 100);
        Assert.StartsWith("data:image", placeholder);
    }

    [Fact]
    public void MaleAvatarBase64_ShouldReturnBase64String()
    {
        var avatar = ImageGenerator.MaleAvatarBase64();
        Assert.False(string.IsNullOrWhiteSpace(avatar));
        Assert.True(IsValidBase64(avatar));
    }

    [Fact]
    public void MaleAvatarBase64_ShouldStartWithDataUri()
    {
        var avatar = ImageGenerator.MaleAvatarBase64();
        Assert.StartsWith("data:image", avatar);
    }

    [Fact]
    public void FemaleAvatarBase64_ShouldReturnBase64String()
    {
        var avatar = ImageGenerator.FemaleAvatarBase64();
        Assert.False(string.IsNullOrWhiteSpace(avatar));
        Assert.True(IsValidBase64(avatar));
    }

    [Fact]
    public void FemaleAvatarBase64_ShouldStartWithDataUri()
    {
        var avatar = ImageGenerator.FemaleAvatarBase64();
        Assert.StartsWith("data:image", avatar);
    }

    [Fact]
    public void SimpleQRCodeBase64_ShouldReturnBase64String()
    {
        var qrCode = ImageGenerator.SimpleQRCodeBase64("https://example.com");
        Assert.False(string.IsNullOrWhiteSpace(qrCode));
        Assert.True(IsValidBase64(qrCode));
    }

    [Fact]
    public void SimpleQRCodeBase64_ShouldStartWithDataUri()
    {
        var qrCode = ImageGenerator.SimpleQRCodeBase64("Test");
        Assert.StartsWith("data:image", qrCode);
    }

    [Fact]
    public void SimpleChartBase64_ShouldReturnBase64String()
    {
        var chart = ImageGenerator.SimpleChartBase64(
            new[] { 10, 20, 30 },
            new[] { "A", "B", "C" }
        );
        Assert.False(string.IsNullOrWhiteSpace(chart));
        Assert.True(IsValidBase64(chart));
    }

    [Fact]
    public void SimpleChartBase64_ShouldStartWithDataUri()
    {
        var chart = ImageGenerator.SimpleChartBase64(
            new[] { 10, 20 },
            new[] { "X", "Y" }
        );
        Assert.StartsWith("data:image", chart);
    }

    [Fact]
    public void RandomCheckerboardBase64_ShouldReturnBase64String()
    {
        var checkerboard = ImageGenerator.RandomCheckerboardBase64(10);
        Assert.False(string.IsNullOrWhiteSpace(checkerboard));
        Assert.True(IsValidBase64(checkerboard));
    }

    [Fact]
    public void RandomCheckerboardBase64_ShouldStartWithDataUri()
    {
        var checkerboard = ImageGenerator.RandomCheckerboardBase64(5);
        Assert.StartsWith("data:image", checkerboard);
    }

    [Fact]
    public void MaleAvatarBase64_ShouldReturnDifferentValues()
    {
        var avatar1 = ImageGenerator.MaleAvatarBase64();
        var avatar2 = ImageGenerator.MaleAvatarBase64();
        Assert.False(string.IsNullOrWhiteSpace(avatar1));
        Assert.False(string.IsNullOrWhiteSpace(avatar2));
    }

    [Fact]
    public void FemaleAvatarBase64_ShouldReturnDifferentValues()
    {
        var avatar1 = ImageGenerator.FemaleAvatarBase64();
        var avatar2 = ImageGenerator.FemaleAvatarBase64();
        Assert.False(string.IsNullOrWhiteSpace(avatar1));
        Assert.False(string.IsNullOrWhiteSpace(avatar2));
    }

    [Fact]
    public void RandomCheckerboardBase64_ShouldReturnDifferentValues()
    {
        var checkerboards = new HashSet<string>();
        for (int i = 0; i < 5; i++)
        {
            checkerboards.Add(ImageGenerator.RandomCheckerboardBase64(8));
        }
        Assert.True(checkerboards.Count > 1, "Should generate different checkerboards");
    }

    [Fact]
    public void PlaceholderBase64_WithDifferentSizes_ShouldWork()
    {
        var placeholder1 = ImageGenerator.PlaceholderBase64(50, 50);
        var placeholder2 = ImageGenerator.PlaceholderBase64(200, 100);
        
        Assert.False(string.IsNullOrWhiteSpace(placeholder1));
        Assert.False(string.IsNullOrWhiteSpace(placeholder2));
        // Different sizes might produce different data
    }

    [Fact]
    public void AllImageGenerators_ShouldProduceValidDataUri()
    {
        var images = new[]
        {
            ImageGenerator.PlaceholderBase64(100, 100),
            ImageGenerator.MaleAvatarBase64(),
            ImageGenerator.FemaleAvatarBase64(),
            ImageGenerator.SimpleQRCodeBase64("test"),
            ImageGenerator.SimpleChartBase64(new[] { 1, 2, 3 }, new[] { "A", "B", "C" }),
            ImageGenerator.RandomCheckerboardBase64(5)
        };

        Assert.All(images, image =>
        {
            Assert.StartsWith("data:image", image);
            Assert.True(IsValidBase64(image));
        });
    }

    private bool IsValidBase64(string value)
    {
        // Remove the data URI scheme if present
        if (value.StartsWith("data:image"))
        {
            var commaIndex = value.IndexOf(',');
            if (commaIndex >= 0)
            {
                value = value[(commaIndex + 1)..];
            }
        }

        try
        {
            Convert.FromBase64String(value);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
