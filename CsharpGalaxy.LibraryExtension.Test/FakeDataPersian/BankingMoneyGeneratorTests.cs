using CsharpGalaxy.LibraryExtension.FakeDataPersian.Generators;

namespace CsharpGalaxy.LibraryExtension.Test.FakeDataPersian;

public class BankingMoneyGeneratorTests
{
    [Fact]
    public void Sheba_ShouldFollowValidFormat()
    {
        var sheba = BankingMoneyGenerator.Sheba();
        Assert.NotNull(sheba);
        Assert.StartsWith("IR", sheba);
        // IBAN has specific length (26 characters for Iran)
        Assert.True(sheba.Length > 20);
    }

    [Fact]
    public void ShebaFormatted_ShouldHaveGrouping()
    {
        var shebaFormatted = BankingMoneyGenerator.ShebaFormatted();
        Assert.Contains(" ", shebaFormatted);
        // Remove spaces and verify it matches unformatted version format
        var unformatted = shebaFormatted.Replace(" ", "");
        Assert.StartsWith("IR", unformatted);
    }

    [Fact]
    public void CardNumber_ShouldBe16Digits()
    {
        var cardNumber = BankingMoneyGenerator.CardNumber();
        Assert.Equal(16, cardNumber.Length);
        Assert.True(cardNumber.All(char.IsDigit));
    }

    [Fact]
    public void CardNumberFormatted_ShouldHaveDashes()
    {
        var cardFormatted = BankingMoneyGenerator.CardNumberFormatted();
        Assert.Contains("-", cardFormatted);
        // Remove dashes and verify 16 digits
        var unformatted = cardFormatted.Replace("-", "");
        Assert.Equal(16, unformatted.Length);
        Assert.True(unformatted.All(char.IsDigit));
    }

    [Fact]
    public void BankName_ShouldReturnValidBankName()
    {
        var bankName = BankingMoneyGenerator.BankName();
        Assert.False(string.IsNullOrWhiteSpace(bankName));
    }

    [Fact]
    public void AccountNumber_ShouldReturnNonEmptyString()
    {
        var accountNumber = BankingMoneyGenerator.AccountNumber();
        Assert.False(string.IsNullOrWhiteSpace(accountNumber));
        Assert.True(accountNumber.All(char.IsDigit));
    }

    [Fact]
    public void CardCVV2_ShouldBe3Or4Digits()
    {
        var cvv2 = BankingMoneyGenerator.CardCVV2();
        Assert.True(cvv2.Length >= 3 && cvv2.Length <= 4);
        Assert.True(cvv2.All(char.IsDigit));
    }

    [Fact]
    public void CardExpiryDate_ShouldFollowValidFormat()
    {
        var expiryDate = BankingMoneyGenerator.CardExpiryDate();
        // Format should be MM/YY
        Assert.Matches(@"^\d{2}/\d{2}$", expiryDate);
        
        var parts = expiryDate.Split('/');
        var month = int.Parse(parts[0]);
        Assert.InRange(month, 1, 12);
    }

    [Fact]
    public void CardNumber_ShouldReturnDifferentValues()
    {
        var cardNumbers = new HashSet<string>();
        for (int i = 0; i < 10; i++)
        {
            cardNumbers.Add(BankingMoneyGenerator.CardNumber());
        }
        Assert.True(cardNumbers.Count > 1, "Should generate different card numbers");
    }

    [Fact]
    public void BankName_ShouldReturnDifferentValues()
    {
        var banks = new HashSet<string>();
        for (int i = 0; i < 10; i++)
        {
            banks.Add(BankingMoneyGenerator.BankName());
        }
        Assert.True(banks.Count > 1, "Should generate different bank names");
    }

    [Fact]
    public void Sheba_ShouldReturnDifferentValues()
    {
        var shebas = new HashSet<string>();
        for (int i = 0; i < 10; i++)
        {
            shebas.Add(BankingMoneyGenerator.Sheba());
        }
        Assert.True(shebas.Count > 1, "Should generate different IBAN/Sheba numbers");
    }

    [Fact]
    public void CardExpiryDate_ShouldBeInFuture()
    {
        var expiryDate = BankingMoneyGenerator.CardExpiryDate();
        var parts = expiryDate.Split('/');
        var year = int.Parse(parts[1]);
        var currentYear = DateTime.Now.Year % 100;
        Assert.True(year >= currentYear);
    }

    [Fact]
    public void AccountNumber_ShouldBeNumeric()
    {
        for (int i = 0; i < 10; i++)
        {
            var accountNumber = BankingMoneyGenerator.AccountNumber();
            Assert.True(accountNumber.All(char.IsDigit));
            Assert.True(accountNumber.Length > 0);
        }
    }
}
