namespace CsharpGalaxy.LibraryExtension.FakeDataPersian.Models;

using Attributes;

/// <summary>
/// مدل تستی برای FakeDataSeeder
/// </summary>
public class TestModel
{
    [Ignore]
    public int PersonalId { get; set; }
    [Guid]
    public string? Id { get; set; }

    [FirstName]
    public string? FirstName { get; set; }

    [LastName]
    public string? LastName { get; set; }

    [FullName]
    public string? FullName { get; set; }

    [Email]
    public string? Email { get; set; }

    [Mobile]
    public string? Mobile { get; set; }

    [Username]
    public string? Username { get; set; }

    [NationalCode]
    public string? NationalCode { get; set; }

    [Address]
    public string? Address { get; set; }

    [City]
    public string? City { get; set; }

    [Province]
    public string? Province { get; set; }

    [Word]
    public string? Word { get; set; }

    [Sentence]
    public string? Sentence { get; set; }

    [CompanyName]
    public string? CompanyName { get; set; }

    [JobTitle]
    public string? JobTitle { get; set; }

    [Iban]
    public string? Iban { get; set; }

    [CardNumber]
    public string? CardNumber { get; set; }

    [DateTime]
    public DateTime CreatedDate { get; set; }

    [Boolean]
    public bool IsActive { get; set; }

    [Status]
    public string? Status { get; set; }

    // Type-based properties
    public Guid GuidId { get; set; }

    public int Age { get; set; }

    public int? OptionalAge { get; set; }

    public long LongValue { get; set; }

    public long? OptionalLongValue { get; set; }

    public decimal Price { get; set; }

    public decimal? OptionalPrice { get; set; }

    public double Score { get; set; }

    public double? OptionalScore { get; set; }

    public bool Active { get; set; }

    public bool? OptionalActive { get; set; }

    public DateTime UpdatedDate { get; set; }

    public DateTime? OptionalUpdatedDate { get; set; }

    public string? Description { get; set; }
}
