namespace CsharpGalaxy.LibraryExtension.FakeDataPersian.Examples;

using Abstracts;
using Generators;
using Models;

/// <summary>
/// نمونه‌های استفاده از FakeBuilder برای تولید داده‌های تصادفی
/// </summary>
public class FakeBuilderExamples
{
    /// <summary>
    /// نمونه ۱: ساخت کاربر تصادفی با قوانین سفارشی
    /// </summary>
    public static void Example1_CreateFakeUser()
    {
        var user = new FakeBuilder<FakeUser>()
            .RuleFor(x => x.Id, () => InternetCryptoGenerator.GuidString())
            .RuleFor(x => x.FirstName, () => PersianNameGenerator.FirstName())
            .RuleFor(x => x.LastName, () => PersianNameGenerator.LastName())
            .RuleFor(x => x.Email, () => PersianTextGenerator.Email())
            .RuleFor(x => x.Mobile, () => IranianMobileGenerator.Mobile())
            .RuleFor(x => x.Username, () => PersianTextGenerator.Username())
            .RuleFor(x => x.MelliCode, () => IranianNationalCodeGenerator.MelliCode())
            .RuleFor(x => x.Address, () => PersianAddressGenerator.FullAddress())
            .RuleFor(x => x.City, () => PersianAddressGenerator.City())
            .RuleFor(x => x.Province, () => PersianAddressGenerator.Province())
            .RuleFor(x => x.CreatedAt, () => DateTime.Now.AddDays(-new Random().Next(1, 365)))
            .RuleFor(x => x.IsActive, () => new Random().Next(2) == 0)
            .Build();

        Console.WriteLine($"کاربر ساخته شد: {user.FullName}");
    }

    /// <summary>
    /// نمونه ۲: ساخت لیستی از کاربران
    /// </summary>
    public static void Example2_CreateUsersList()
    {
        var users = new FakeBuilder<FakeUser>()
            .RuleFor(x => x.Id, () => InternetCryptoGenerator.GuidString())
            .RuleFor(x => x.FirstName, () => PersianNameGenerator.FirstName())
            .RuleFor(x => x.LastName, () => PersianNameGenerator.LastName())
            .RuleFor(x => x.Email, () => PersianTextGenerator.Email())
            .RuleFor(x => x.Mobile, () => IranianMobileGenerator.Mobile())
            .RuleFor(x => x.Username, () => PersianTextGenerator.Username())
            .RuleFor(x => x.MelliCode, () => IranianNationalCodeGenerator.MelliCode())
            .RuleFor(x => x.Address, () => PersianAddressGenerator.FullAddress())
            .RuleFor(x => x.City, () => PersianAddressGenerator.City())
            .RuleFor(x => x.Province, () => PersianAddressGenerator.Province())
            .RuleFor(x => x.CreatedAt, () => DateTime.Now.AddDays(-new Random().Next(1, 365)))
            .RuleFor(x => x.IsActive, () => new Random().Next(2) == 0)
            .BuildList(10);

        Console.WriteLine($"{users.Count} کاربر ساخته شدند");
    }

    /// <summary>
    /// نمونه ۳: ساخت محصول با قوانین سفارشی
    /// </summary>
    public static void Example3_CreateFakeProduct()
    {
        var product = new FakeBuilder<FakeProduct>()
            .RuleFor(x => x.Id, () => InternetCryptoGenerator.GuidString())
            .RuleFor(x => x.Name, () => $"محصول {PersianNameGenerator.FirstName()}")
            .RuleFor(x => x.Description, () => PersianTextGenerator.Sentence())
            .RuleFor(x => x.SKU, () => BusinessDataGenerator.ProductSKU())
            .RuleFor(x => x.Price, () => Convert.ToDecimal(new Random().Next(10000, 5000000)))
            .RuleFor(x => x.DiscountPrice, () => null)
            .RuleFor(x => x.Stock, () => new Random().Next(0, 1000))
            .RuleFor(x => x.Category, () => "دسته‌بندی نمونه")
            .RuleFor(x => x.ImageBase64, () => ImageGenerator.PlaceholderBase64(200, 200))
            .RuleFor(x => x.CreatedAt, () => DateTime.Now.AddDays(-new Random().Next(1, 365)))
            .RuleFor(x => x.IsActive, () => true)
            .Build();

        Console.WriteLine($"محصول ساخته شد: {product.Name}");
    }

    /// <summary>
    /// نمونه ۴: ساخت سفارش با قوانین سفارشی
    /// </summary>
    public static void Example4_CreateFakeOrder()
    {
        var order = new FakeBuilder<FakeOrder>()
            .RuleFor(x => x.Id, () => InternetCryptoGenerator.GuidString())
            .RuleFor(x => x.OrderNumber, () => BusinessDataGenerator.OrderNumber())
            .RuleFor(x => x.CustomerId, () => InternetCryptoGenerator.GuidString())
            .RuleFor(x => x.CustomerName, () => PersianNameGenerator.FullName())
            .RuleFor(x => x.CustomerEmail, () => PersianTextGenerator.Email())
            .RuleFor(x => x.CustomerPhone, () => IranianMobileGenerator.Mobile())
            .RuleFor(x => x.TotalAmount, () => Convert.ToDecimal(new Random().Next(100000, 10000000)))
            .RuleFor(x => x.DiscountAmount, () => Convert.ToDecimal(new Random().Next(0, 1000000)))
            .RuleFor(x => x.FinalAmount, () => Convert.ToDecimal(new Random().Next(100000, 10000000)))
            .RuleFor(x => x.Status, () => BusinessDataGenerator.ProjectStatus())
            .RuleFor(x => x.PaymentMethod, () => BusinessDataGenerator.PaymentMethod())
            .RuleFor(x => x.DeliveryAddress, () => PersianAddressGenerator.FullAddress())
            .RuleFor(x => x.OrderDate, () => DateTime.Now.AddDays(-new Random().Next(1, 30)))
            .RuleFor(x => x.DeliveryDate, () => DateTime.Now.AddDays(new Random().Next(1, 15)))
            .RuleFor(x => x.Items, () => new List<FakeOrderItem>())
            .Build();

        Console.WriteLine($"سفارش ساخته شد: {order.OrderNumber}");
    }

    /// <summary>
    /// نمونه ۵: ساخت فاکتور با قوانین سفارشی
    /// </summary>
    public static void Example5_CreateFakeInvoice()
    {
        var invoice = new FakeBuilder<FakeInvoice>()
            .RuleFor(x => x.Id, () => InternetCryptoGenerator.GuidString())
            .RuleFor(x => x.InvoiceNumber, () => BusinessDataGenerator.InvoiceNumber())
            .RuleFor(x => x.CompanyName, () => BusinessDataGenerator.CompanyName())
            .RuleFor(x => x.CustomerName, () => PersianNameGenerator.FullName())
            .RuleFor(x => x.CustomerMelliId, () => IranianNationalCodeGenerator.MelliCode())
            .RuleFor(x => x.Amount, () => Convert.ToDecimal(new Random().Next(50000, 5000000)))
            .RuleFor(x => x.Tax, () => Convert.ToDecimal(new Random().Next(10000, 500000)))
            .RuleFor(x => x.Total, () => Convert.ToDecimal(new Random().Next(60000, 5500000)))
            .RuleFor(x => x.Status, () => BusinessDataGenerator.InvoiceStatus())
            .RuleFor(x => x.PaymentMethod, () => BusinessDataGenerator.PaymentMethod())
            .RuleFor(x => x.IssueDate, () => DateTime.Now.AddDays(-new Random().Next(1, 90)))
            .RuleFor(x => x.DueDate, () => DateTime.Now.AddDays(new Random().Next(10, 60)))
            .RuleFor(x => x.PaidDate, () => null)
            .Build();

        Console.WriteLine($"فاکتور ساخته شد: {invoice.InvoiceNumber}");
    }

    /// <summary>
    /// نمونه ۶: ساخت کارمند با قوانین سفارشی
    /// </summary>
    public static void Example6_CreateFakeEmployee()
    {
        var employee = new FakeBuilder<FakeEmployee>()
            .RuleFor(x => x.Id, () => InternetCryptoGenerator.GuidString())
            .RuleFor(x => x.FirstName, () => PersianNameGenerator.FirstName())
            .RuleFor(x => x.LastName, () => PersianNameGenerator.LastName())
            .RuleFor(x => x.Email, () => PersianTextGenerator.Email())
            .RuleFor(x => x.Mobile, () => IranianMobileGenerator.Mobile())
            .RuleFor(x => x.MelliCode, () => IranianNationalCodeGenerator.MelliCode())
            .RuleFor(x => x.JobTitle, () => BusinessDataGenerator.JobTitle())
            .RuleFor(x => x.Department, () => $"بخش {new Random().Next(1, 10)}")
            .RuleFor(x => x.Salary, () => Convert.ToDecimal(new Random().Next(3000000, 50000000)))
            .RuleFor(x => x.HireDate, () => DateTime.Now.AddDays(-new Random().Next(30, 3650)))
            .RuleFor(x => x.EmployeeNumber, () => $"EMP-{new Random().Next(100000, 999999)}")
            .RuleFor(x => x.IsActive, () => true)
            .Build();

        Console.WriteLine($"کارمند ساخته شد: {employee.FullName}");
    }

    /// <summary>
    /// نمونه ۷: ساخت بیمار با قوانین سفارشی
    /// </summary>
    public static void Example7_CreateFakePatient()
    {
        var patient = new FakeBuilder<FakePatient>()
            .RuleFor(x => x.Id, () => InternetCryptoGenerator.GuidString())
            .RuleFor(x => x.FirstName, () => PersianNameGenerator.FirstName())
            .RuleFor(x => x.LastName, () => PersianNameGenerator.LastName())
            .RuleFor(x => x.MelliCode, () => IranianNationalCodeGenerator.MelliCode())
            .RuleFor(x => x.PatientFileNumber, () => HealthMedicalGenerator.PatientFileNumber())
            .RuleFor(x => x.Mobile, () => IranianMobileGenerator.Mobile())
            .RuleFor(x => x.Email, () => PersianTextGenerator.Email())
            .RuleFor(x => x.BloodType, () => HealthMedicalGenerator.BloodType())
            .RuleFor(x => x.Age, () => PersianDateGenerator.Age(1, 100))
            .RuleFor(x => x.Gender, () => new Random().Next(2) == 0 ? "مرد" : "زن")
            .RuleFor(x => x.Height, () => HealthMedicalGenerator.Height())
            .RuleFor(x => x.Weight, () => HealthMedicalGenerator.Weight())
            .RuleFor(x => x.Allergies, () => new List<string>())
            .RuleFor(x => x.Diseases, () => new List<string>())
            .RuleFor(x => x.HealthInsuranceNumber, () => HealthMedicalGenerator.HealthInsuranceNumber())
            .RuleFor(x => x.RegistrationDate, () => DateTime.Now.AddDays(-new Random().Next(1, 1095)))
            .Build();

        Console.WriteLine($"بیمار ساخته شد: {patient.FullName}");
    }

    /// <summary>
    /// نمونه ۸: استفاده از RuleForAllStrings برای تمام string properties
    /// </summary>
    public static void Example8_RuleForAllStrings()
    {
        var user = new FakeBuilder<FakeUser>()
            .RuleForAllStrings(() => PersianTextGenerator.Word())
            .Build();

        Console.WriteLine($"کاربر با تمام string properties ساخته شد");
    }

    /// <summary>
    /// نمونه ۹: ترکیب RuleFor و RuleForAllStrings
    /// </summary>
    public static void Example9_MixRules()
    {
        var user = new FakeBuilder<FakeUser>()
            .RuleForAllStrings(() => PersianTextGenerator.Word())
            .RuleFor(x => x.Email, () => PersianTextGenerator.Email())
            .RuleFor(x => x.Mobile, () => IranianMobileGenerator.Mobile())
            .RuleFor(x => x.IsActive, () => true)
            .Build();

        Console.WriteLine($"کاربر با قوانین ترکیبی ساخته شد");
    }

    /// <summary>
    /// نمونه ۱۰: ساخت لیستی از محصولات با قوانین سفارشی
    /// </summary>
    public static void Example10_CreateProductsList()
    {
        var products = new FakeBuilder<FakeProduct>()
            .RuleFor(x => x.Id, () => InternetCryptoGenerator.GuidString())
            .RuleFor(x => x.Name, () => $"محصول {new Random().Next(1000, 9999)}")
            .RuleFor(x => x.Description, () => PersianTextGenerator.Sentence())
            .RuleFor(x => x.SKU, () => BusinessDataGenerator.ProductSKU())
            .RuleFor(x => x.Price, () => Convert.ToDecimal(new Random().Next(10000, 5000000)))
            .RuleFor(x => x.Stock, () => new Random().Next(0, 1000))
            .RuleFor(x => x.IsActive, () => true)
            .BuildList(5);

        Console.WriteLine($"{products.Count} محصول ساخته شدند");
    }
}
