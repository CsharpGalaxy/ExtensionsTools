using CsharpGalaxy.LibraryExtension.FakeDataPersian.Generators;

namespace CsharpGalaxy.LibraryExtension.Test.FakeDataPersian;

public class InternetCryptoGeneratorTests
{
    [Fact]
    public void IPv4_ShouldFollowValidFormat()
    {
        var ipv4 = InternetCryptoGenerator.IPv4();
        var parts = ipv4.Split('.');
        Assert.Equal(4, parts.Length);
        Assert.All(parts, part => 
        {
            Assert.True(int.TryParse(part, out var num));
            Assert.InRange(num, 0, 255);
        });
    }

    [Fact]
    public void IPv4Private_ShouldReturnPrivateAddress()
    {
        var ipv4Private = InternetCryptoGenerator.IPv4Private();
        var parts = ipv4Private.Split('.');
        Assert.Equal(4, parts.Length);
        var firstOctet = int.Parse(parts[0]);
        // Private ranges: 10.x.x.x, 172.16.x.x - 172.31.x.x, 192.168.x.x
        Assert.True(
            firstOctet == 10 || 
            firstOctet == 172 || 
            firstOctet == 192
        );
    }

    [Fact]
    public void MAC_ShouldFollowValidMACFormat()
    {
        var mac = InternetCryptoGenerator.MAC();
        // MAC should be in format XX:XX:XX:XX:XX:XX
        Assert.Matches(@"^([0-9A-Fa-f]{2}[:]){5}([0-9A-Fa-f]{2})$", mac);
    }

    [Fact]
    public void Guid_ShouldReturnValidGuid()
    {
        var guid = InternetCryptoGenerator.Guid();
        Assert.NotEqual(Guid.Empty, guid);
        Assert.NotEqual(default(Guid), guid);
    }

    [Fact]
    public void GuidString_ShouldReturnValidGuidFormat()
    {
        var guidString = InternetCryptoGenerator.GuidString();
        Assert.True(Guid.TryParse(guidString, out _));
        Assert.Contains("-", guidString);
    }

    [Fact]
    public void GuidStringNoHyphen_ShouldReturnValidGuidWithoutHyphens()
    {
        var guidString = InternetCryptoGenerator.GuidStringNoHyphen();
        Assert.DoesNotContain("-", guidString);
        Assert.True(Guid.TryParse(guidString, out _));
        Assert.Equal(32, guidString.Length); // 32 hex characters without hyphens
    }

    [Fact]
    public void Slug_ShouldReturnValidSlugFormat()
    {
        var slug = InternetCryptoGenerator.Slug();
        Assert.False(string.IsNullOrWhiteSpace(slug));
        Assert.Matches(@"^[a-z0-9-]+$", slug);
        Assert.DoesNotContain(" ", slug);
    }

    [Fact]
    public void Token_ShouldReturnNonEmptyString()
    {
        var token = InternetCryptoGenerator.Token();
        Assert.False(string.IsNullOrWhiteSpace(token));
        Assert.True(token.Length > 10);
    }

    [Fact]
    public void Url_ShouldFollowValidUrlFormat()
    {
        var url = InternetCryptoGenerator.Url();
        Assert.StartsWith("http", url);
        Assert.True(Uri.TryCreate(url, UriKind.Absolute, out _));
    }

    [Fact]
    public void IPv4_ShouldReturnDifferentValues()
    {
        var ips = new HashSet<string>();
        for (int i = 0; i < 10; i++)
        {
            ips.Add(InternetCryptoGenerator.IPv4());
        }
        Assert.True(ips.Count > 1, "Should generate different IP addresses");
    }

    [Fact]
    public void MAC_ShouldReturnDifferentValues()
    {
        var macs = new HashSet<string>();
        for (int i = 0; i < 10; i++)
        {
            macs.Add(InternetCryptoGenerator.MAC());
        }
        Assert.True(macs.Count > 1, "Should generate different MAC addresses");
    }

    [Fact]
    public void Guid_ShouldReturnDifferentValues()
    {
        var guids = new HashSet<Guid>();
        for (int i = 0; i < 10; i++)
        {
            guids.Add(InternetCryptoGenerator.Guid());
        }
        Assert.True(guids.Count > 1, "Should generate different GUIDs");
    }

    [Fact]
    public void Token_ShouldReturnDifferentValues()
    {
        var tokens = new HashSet<string>();
        for (int i = 0; i < 10; i++)
        {
            tokens.Add(InternetCryptoGenerator.Token());
        }
        Assert.True(tokens.Count > 1, "Should generate different tokens");
    }

    [Fact]
    public void Url_ShouldReturnDifferentValues()
    {
        var urls = new HashSet<string>();
        for (int i = 0; i < 10; i++)
        {
            urls.Add(InternetCryptoGenerator.Url());
        }
        Assert.True(urls.Count > 1, "Should generate different URLs");
    }
}
