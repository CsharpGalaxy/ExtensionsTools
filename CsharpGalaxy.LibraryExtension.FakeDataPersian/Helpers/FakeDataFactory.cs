namespace CsharpGalaxy.LibraryExtension.FakeDataPersian.Helpers;

using Generators;
using Models;

/// <summary>
/// کارخانهٔ ایجاد اشیاء تستی کامل
/// </summary>
public static class FakeDataFactory
{
    /// <summary>
    /// یک کاربر تصادفی کامل ایجاد می‌کند
    /// </summary>
    public static FakeUser CreateFakeUser()
    {
        return new FakeUser
        {
            Id = InternetCryptoGenerator.GuidString(),
            FirstName = PersianNameGenerator.FirstName(),
            LastName = PersianNameGenerator.LastName(),
            Email = PersianTextGenerator.Email(),
            Mobile = IranianMobileGenerator.Mobile(),
            Username = PersianTextGenerator.Username(),
            MelliCode = IranianNationalCodeGenerator.MelliCode(),
            Address = PersianAddressGenerator.FullAddress(),
            City = PersianAddressGenerator.City(),
            Province = PersianAddressGenerator.Province(),
            CreatedAt = DateTime.Now.AddDays(-new Random().Next(1, 365)),
            IsActive = new Random().Next(2) == 0
        };
    }

    /// <summary>
    /// چندین کاربر تصادفی ایجاد می‌کند
    /// </summary>
    public static List<FakeUser> CreateFakeUsers(int count)
    {
        return Enumerable.Range(0, count)
            .Select(_ => CreateFakeUser())
            .ToList();
    }

    /// <summary>
    /// یک محصول تصادفی کامل ایجاد می‌کند
    /// </summary>
    public static FakeProduct CreateFakeProduct()
    {
        decimal price = BankingMoneyGenerator.CardNumber().Length > 0 
            ? BusinessDataGenerator.UnitPrice(10000m, 5000000m)
            : 10000m;

        return new FakeProduct
        {
            Id = InternetCryptoGenerator.GuidString(),
            Name = $"محصول {PersianNameGenerator.FirstName()}",
            Description = PersianTextGenerator.Sentence(),
            SKU = BusinessDataGenerator.ProductSKU(),
            Price = price,
            DiscountPrice = new Random().Next(2) == 0 ? price * 0.9m : null,
            Stock = new Random().Next(0, 1000),
            Category = "دسته‌بندی نمونه",
            ImageBase64 = ImageGenerator.PlaceholderBase64(200, 200),
            CreatedAt = DateTime.Now.AddDays(-new Random().Next(1, 365)),
            IsActive = new Random().Next(2) == 0
        };
    }

    /// <summary>
    /// چندین محصول تصادفی ایجاد می‌کند
    /// </summary>
    public static List<FakeProduct> CreateFakeProducts(int count)
    {
        return Enumerable.Range(0, count)
            .Select(_ => CreateFakeProduct())
            .ToList();
    }

    /// <summary>
    /// یک سفارش تصادفی کامل ایجاد می‌کند
    /// </summary>
    public static FakeOrder CreateFakeOrder()
    {
        var customer = CreateFakeUser();
        var items = Enumerable.Range(0, new Random().Next(1, 6))
            .Select(_ => new FakeOrderItem
            {
                Id = InternetCryptoGenerator.GuidString(),
                ProductId = InternetCryptoGenerator.GuidString(),
                ProductName = $"محصول {new Random().Next(1000, 9999)}",
                SKU = BusinessDataGenerator.ProductSKU(),
                Quantity = new Random().Next(1, 10),
                UnitPrice = BusinessDataGenerator.UnitPrice(10000m, 1000000m)
            })
            .ToList();

        decimal totalAmount = items.Sum(i => i.Subtotal);
        decimal discountAmount = totalAmount * (decimal)(new Random().NextDouble() * 0.2);
        decimal finalAmount = totalAmount - discountAmount;

        return new FakeOrder
        {
            Id = InternetCryptoGenerator.GuidString(),
            OrderNumber = BusinessDataGenerator.OrderNumber(),
            CustomerId = customer.Id,
            CustomerName = customer.FullName,
            CustomerEmail = customer.Email,
            CustomerPhone = customer.Mobile,
            TotalAmount = totalAmount,
            DiscountAmount = discountAmount,
            FinalAmount = finalAmount,
            Status = BusinessDataGenerator.ProjectStatus(),
            PaymentMethod = BusinessDataGenerator.PaymentMethod(),
            DeliveryAddress = PersianAddressGenerator.FullAddress(),
            OrderDate = DateTime.Now.AddDays(-new Random().Next(1, 30)),
            DeliveryDate = DateTime.Now.AddDays(new Random().Next(1, 15)),
            Items = items
        };
    }

    /// <summary>
    /// چندین سفارش تصادفی ایجاد می‌کند
    /// </summary>
    public static List<FakeOrder> CreateFakeOrders(int count)
    {
        return Enumerable.Range(0, count)
            .Select(_ => CreateFakeOrder())
            .ToList();
    }

    /// <summary>
    /// یک فاکتور تصادفی کامل ایجاد می‌کند
    /// </summary>
    public static FakeInvoice CreateFakeInvoice()
    {
        decimal amount = BusinessDataGenerator.RoundedAmount(50000m, 5000000m);
        decimal tax = amount * 0.09m; // مالیات ۹ درصد
        decimal total = amount + tax;

        return new FakeInvoice
        {
            Id = InternetCryptoGenerator.GuidString(),
            InvoiceNumber = BusinessDataGenerator.InvoiceNumber(),
            CompanyName = BusinessDataGenerator.CompanyName(),
            CustomerName = PersianNameGenerator.FullName(),
            CustomerMelliId = IranianNationalCodeGenerator.MelliCode(),
            Amount = amount,
            Tax = tax,
            Total = total,
            Status = BusinessDataGenerator.InvoiceStatus(),
            PaymentMethod = BusinessDataGenerator.PaymentMethod(),
            IssueDate = DateTime.Now.AddDays(-new Random().Next(1, 90)),
            DueDate = DateTime.Now.AddDays(new Random().Next(10, 60)),
            PaidDate = new Random().Next(2) == 0 ? DateTime.Now : null
        };
    }

    /// <summary>
    /// چندین فاکتور تصادفی ایجاد می‌کند
    /// </summary>
    public static List<FakeInvoice> CreateFakeInvoices(int count)
    {
        return Enumerable.Range(0, count)
            .Select(_ => CreateFakeInvoice())
            .ToList();
    }

    /// <summary>
    /// یک کارمند تصادفی کامل ایجاد می‌کند
    /// </summary>
    public static FakeEmployee CreateFakeEmployee()
    {
        return new FakeEmployee
        {
            Id = InternetCryptoGenerator.GuidString(),
            FirstName = PersianNameGenerator.FirstName(),
            LastName = PersianNameGenerator.LastName(),
            Email = PersianTextGenerator.Email(),
            Mobile = IranianMobileGenerator.Mobile(),
            MelliCode = IranianNationalCodeGenerator.MelliCode(),
            JobTitle = BusinessDataGenerator.JobTitle(),
            Department = $"بخش {new Random().Next(1, 10)}",
            Salary = BusinessDataGenerator.RoundedAmount(3000000m, 50000000m),
            HireDate = DateTime.Now.AddDays(-new Random().Next(30, 3650)),
            EmployeeNumber = $"EMP-{new Random().Next(100000, 999999)}",
            IsActive = true
        };
    }

    /// <summary>
    /// چندین کارمند تصادفی ایجاد می‌کند
    /// </summary>
    public static List<FakeEmployee> CreateFakeEmployees(int count)
    {
        return Enumerable.Range(0, count)
            .Select(_ => CreateFakeEmployee())
            .ToList();
    }

    /// <summary>
    /// یک بیمار تصادفی کامل ایجاد می‌کند
    /// </summary>
    public static FakePatient CreateFakePatient()
    {
        var age = PersianDateGenerator.Age(1, 100);
        var height = HealthMedicalGenerator.Height();
        var weight = HealthMedicalGenerator.Weight();

        return new FakePatient
        {
            Id = InternetCryptoGenerator.GuidString(),
            FirstName = PersianNameGenerator.FirstName(),
            LastName = PersianNameGenerator.LastName(),
            MelliCode = IranianNationalCodeGenerator.MelliCode(),
            PatientFileNumber = HealthMedicalGenerator.PatientFileNumber(),
            Mobile = IranianMobileGenerator.Mobile(),
            Email = PersianTextGenerator.Email(),
            BloodType = HealthMedicalGenerator.BloodType(),
            Age = age,
            Gender = new Random().Next(2) == 0 ? "مرد" : "زن",
            Height = height,
            Weight = weight,
            Allergies = Enumerable.Range(0, new Random().Next(0, 3))
                .Select(_ => HealthMedicalGenerator.Allergy())
                .Distinct()
                .ToList(),
            Diseases = Enumerable.Range(0, new Random().Next(0, 4))
                .Select(_ => HealthMedicalGenerator.CommonDisease())
                .Distinct()
                .ToList(),
            HealthInsuranceNumber = HealthMedicalGenerator.HealthInsuranceNumber(),
            RegistrationDate = DateTime.Now.AddDays(-new Random().Next(1, 1095))
        };
    }

    /// <summary>
    /// چندین بیمار تصادفی ایجاد می‌کند
    /// </summary>
    public static List<FakePatient> CreateFakePatients(int count)
    {
        return Enumerable.Range(0, count)
            .Select(_ => CreateFakePatient())
            .ToList();
    }
}
