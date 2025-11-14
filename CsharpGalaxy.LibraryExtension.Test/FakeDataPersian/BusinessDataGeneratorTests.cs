using CsharpGalaxy.LibraryExtension.FakeDataPersian.Generators;

namespace CsharpGalaxy.LibraryExtension.Test.FakeDataPersian;

public class BusinessDataGeneratorTests
{
    [Fact]
    public void CompanyName_ShouldReturnNonEmptyString()
    {
        var companyName = BusinessDataGenerator.CompanyName();
        Assert.False(string.IsNullOrWhiteSpace(companyName));
    }

    [Fact]
    public void CompanyMelliId_ShouldBeNumeric()
    {
        var melliId = BusinessDataGenerator.CompanyMelliId();
        Assert.True(melliId.All(char.IsDigit));
        Assert.True(melliId.Length > 0);
    }

    [Fact]
    public void JobTitle_ShouldReturnNonEmptyString()
    {
        var jobTitle = BusinessDataGenerator.JobTitle();
        Assert.False(string.IsNullOrWhiteSpace(jobTitle));
    }

    [Fact]
    public void ContractNumber_ShouldReturnNonEmptyString()
    {
        var contractNumber = BusinessDataGenerator.ContractNumber();
        Assert.False(string.IsNullOrWhiteSpace(contractNumber));
    }

    [Fact]
    public void ProjectNumber_ShouldReturnNonEmptyString()
    {
        var projectNumber = BusinessDataGenerator.ProjectNumber();
        Assert.False(string.IsNullOrWhiteSpace(projectNumber));
    }

    [Fact]
    public void ProjectStatus_ShouldReturnValidStatus()
    {
        var status = BusinessDataGenerator.ProjectStatus();
        Assert.False(string.IsNullOrWhiteSpace(status));
    }

    [Fact]
    public void ProjectProgress_ShouldBeValidPercentage()
    {
        var progress = BusinessDataGenerator.ProjectProgress();
        Assert.InRange(progress, 0, 100);
    }

    [Fact]
    public void InvoiceNumber_ShouldReturnNonEmptyString()
    {
        var invoiceNumber = BusinessDataGenerator.InvoiceNumber();
        Assert.False(string.IsNullOrWhiteSpace(invoiceNumber));
    }

    [Fact]
    public void InvoiceStatus_ShouldReturnValidStatus()
    {
        var status = BusinessDataGenerator.InvoiceStatus();
        Assert.False(string.IsNullOrWhiteSpace(status));
    }

    [Fact]
    public void Amount_ShouldReturnPositiveValue()
    {
        var amount = BusinessDataGenerator.Amount();
        Assert.True(amount > 0);
    }

    [Fact]
    public void RoundedAmount_ShouldBeRoundedValue()
    {
        var amount = BusinessDataGenerator.RoundedAmount();
        Assert.True(amount > 0);
        // Should be a round number (divisible by 1000)
        Assert.Equal(0, amount % 1000);
    }

    [Fact]
    public void PaymentMethod_ShouldReturnValidMethod()
    {
        var method = BusinessDataGenerator.PaymentMethod();
        Assert.False(string.IsNullOrWhiteSpace(method));
    }

    [Fact]
    public void TransactionReference_ShouldReturnNonEmptyString()
    {
        var reference = BusinessDataGenerator.TransactionReference();
        Assert.False(string.IsNullOrWhiteSpace(reference));
    }

    [Fact]
    public void BankSlipNumber_ShouldReturnNonEmptyString()
    {
        var slipNumber = BusinessDataGenerator.BankSlipNumber();
        Assert.False(string.IsNullOrWhiteSpace(slipNumber));
    }

    [Fact]
    public void OrderNumber_ShouldReturnNonEmptyString()
    {
        var orderNumber = BusinessDataGenerator.OrderNumber();
        Assert.False(string.IsNullOrWhiteSpace(orderNumber));
    }

    [Fact]
    public void ProductSKU_ShouldReturnNonEmptyString()
    {
        var sku = BusinessDataGenerator.ProductSKU();
        Assert.False(string.IsNullOrWhiteSpace(sku));
        Assert.Matches(@"^[A-Z0-9-]+$", sku);
    }

    [Fact]
    public void UnitPrice_ShouldReturnPositiveValue()
    {
        var price = BusinessDataGenerator.UnitPrice();
        Assert.True(price > 0);
    }

    [Fact]
    public void CommissionOrDiscount_ShouldReturnValidPercentage()
    {
        var commission = BusinessDataGenerator.CommissionOrDiscount();
        Assert.InRange(commission, 0, 50);
    }

    [Fact]
    public void CustomerAccountNumber_ShouldBeNumeric()
    {
        var accountNumber = BusinessDataGenerator.CustomerAccountNumber();
        Assert.True(accountNumber.ToCharArray().Skip(4).All(char.IsDigit));
    }

    [Fact]
    public void CustomerCreditRating_ShouldBeValidRating()
    {
        var rating = BusinessDataGenerator.CustomerCreditRating();
        Assert.False(string.IsNullOrWhiteSpace(rating));
    }

    [Fact]
    public void CustomerCreditLimit_ShouldReturnPositiveValue()
    {
        var limit = BusinessDataGenerator.CustomerCreditLimit();
        Assert.True(limit > 0);
    }

    [Fact]
    public void CompanyName_ShouldReturnDifferentValues()
    {
        var companies = new HashSet<string>();
        for (int i = 0; i < 10; i++)
        {
            companies.Add(BusinessDataGenerator.CompanyName());
        }
        Assert.True(companies.Count > 1, "Should generate different company names");
    }

    [Fact]
    public void JobTitle_ShouldReturnDifferentValues()
    {
        var titles = new HashSet<string>();
        for (int i = 0; i < 10; i++)
        {
            titles.Add(BusinessDataGenerator.JobTitle());
        }
        Assert.True(titles.Count > 1, "Should generate different job titles");
    }

    [Fact]
    public void Amount_ShouldReturnDifferentValues()
    {
        var amounts = new HashSet<decimal>();
        for (int i = 0; i < 10; i++)
        {
            amounts.Add(BusinessDataGenerator.Amount());
        }
        Assert.True(amounts.Count > 1, "Should generate different amounts");
    }
}
