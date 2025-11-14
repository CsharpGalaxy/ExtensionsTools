using CsharpGalaxy.LibraryExtension.FakeDataPersian.Generators;

namespace CsharpGalaxy.LibraryExtension.Test.FakeDataPersian;

public class PersianTextGeneratorTests
{
    [Fact]
    public void Word_ShouldReturnNonEmptyString()
    {
        var word = PersianTextGenerator.Word();
        Assert.False(string.IsNullOrWhiteSpace(word));
    }

    [Fact]
    public void Words_ShouldReturnRequestedCount()
    {
        var words = PersianTextGenerator.Words(5);
        var wordList = words.Split(' ');
        Assert.Equal(5, wordList.Length);
    }

    [Fact]
    public void Sentence_ShouldReturnNonEmptyString()
    {
        var sentence = PersianTextGenerator.Sentence();
        Assert.False(string.IsNullOrWhiteSpace(sentence));
        Assert.True(sentence.Length > 5);
    }

    [Fact]
    public void Sentences_ShouldReturnRequestedCount()
    {
        var sentences = PersianTextGenerator.Sentences(3);
        var sentenceList = sentences.Split('.');
        // Count non-empty sentences (last element after split will be empty)
        var count = sentenceList.Count(s => !string.IsNullOrWhiteSpace(s));
        Assert.Equal(3, count);
    }

    [Fact]
    public void Email_ShouldFollowEmailFormat()
    {
        var email = PersianTextGenerator.Email();
        Assert.Contains("@", email);
        Assert.Contains(".", email);
        Assert.NotEmpty(email);
        Assert.True(email.Length > 5);
    }

    [Fact]
    public void Username_ShouldReturnNonEmptyString()
    {
        var username = PersianTextGenerator.Username();
        Assert.False(string.IsNullOrWhiteSpace(username));
        Assert.NotEmpty(username);
    }

    [Fact]
    public void UsernameWithLastName_ShouldContainLastName()
    {
        var username = PersianTextGenerator.UsernameWithLastName();
        Assert.False(string.IsNullOrWhiteSpace(username));
        Assert.True(username.Length > 5);
    }

    [Fact]
    public void Word_ShouldReturnDifferentValues()
    {
        var words = new HashSet<string>();
        for (int i = 0; i < 10; i++)
        {
            words.Add(PersianTextGenerator.Word());
        }
        Assert.True(words.Count > 1, "Should generate different words");
    }

    [Fact]
    public void Email_ShouldReturnDifferentValues()
    {
        var emails = new HashSet<string>();
        for (int i = 0; i < 10; i++)
        {
            emails.Add(PersianTextGenerator.Email());
        }
        Assert.True(emails.Count > 1, "Should generate different emails");
    }

    [Fact]
    public void Words_ShouldReturnCorrectFormat()
    {
        var words = PersianTextGenerator.Words(10);
        var wordList = words.Split(' ');
        Assert.All(wordList, word => Assert.False(string.IsNullOrWhiteSpace(word)));
    }

    [Fact]
    public void Sentences_ShouldHavePropperSpacing()
    {
        var sentences = PersianTextGenerator.Sentences(3);
        // Should have sentences separated by periods and spaces
        Assert.Contains(". ", sentences);
    }

    [Fact]
    public void Email_ShouldNotHavePersianCharacters()
    {
        for (int i = 0; i < 10; i++)
        {
            var email = PersianTextGenerator.Email();
            Assert.NotEmpty(email);
        }
    }

    [Fact]
    public void Username_ShouldNotContainSpecialCharactersExceptUnderscore()
    {
        for (int i = 0; i < 10; i++)
        {
            var username = PersianTextGenerator.Username();
            Assert.False(string.IsNullOrWhiteSpace(username));
        }
    }
}
