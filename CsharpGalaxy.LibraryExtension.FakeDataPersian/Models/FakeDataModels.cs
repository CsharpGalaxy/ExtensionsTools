using CsharpGalaxy.LibraryExtension.FakeDataPersian.Attributes;

namespace CsharpGalaxy.LibraryExtension.FakeDataPersian.Models;

/// <summary>
/// نماینی کاربر (User) برای تست‌ها
/// </summary>
public class FakeUser
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    [FirstName]
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public string Email { get; set; } = "";
    public string Mobile { get; set; } = "";
    public string Username { get; set; } = "";
    public string MelliCode { get; set; } = "";
    public string Address { get; set; } = "";
    public string City { get; set; } = "";
    public string Province { get; set; } = "";
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public bool IsActive { get; set; } = true;

    public string FullName => $"{FirstName} {LastName}";
}

/// <summary>
/// نماینی محصول (Product) برای تست‌ها
/// </summary>
public class FakeProduct
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public string SKU { get; set; } = "";
    public decimal Price { get; set; }
    public decimal? DiscountPrice { get; set; }
    public int Stock { get; set; }
    public string Category { get; set; } = "";
    public string ImageBase64 { get; set; } = "";
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public bool IsActive { get; set; } = true;
}

/// <summary>
/// نماینی سفارش (Order) برای تست‌ها
/// </summary>
public class FakeOrder
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string OrderNumber { get; set; } = "";
    public string CustomerId { get; set; } = "";
    public string CustomerName { get; set; } = "";
    public string CustomerEmail { get; set; } = "";
    public string CustomerPhone { get; set; } = "";
    public decimal TotalAmount { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal FinalAmount { get; set; }
    public string Status { get; set; } = "";
    public string PaymentMethod { get; set; } = "";
    public string DeliveryAddress { get; set; } = "";
    public DateTime OrderDate { get; set; } = DateTime.Now;
    public DateTime? DeliveryDate { get; set; }
    public List<FakeOrderItem> Items { get; set; } = new();
}

/// <summary>
/// آیتم سفارش
/// </summary>
public class FakeOrderItem
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string ProductId { get; set; } = "";
    public string ProductName { get; set; } = "";
    public string SKU { get; set; } = "";
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Subtotal => Quantity * UnitPrice;
}

/// <summary>
/// نماینی فاکتور (Invoice) برای تست‌ها
/// </summary>
public class FakeInvoice
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string InvoiceNumber { get; set; } = "";
    public string CompanyName { get; set; } = "";
    public string CustomerName { get; set; } = "";
    public string CustomerMelliId { get; set; } = "";
    public decimal Amount { get; set; }
    public decimal Tax { get; set; }
    public decimal Total { get; set; }
    public string Status { get; set; } = "";
    public string PaymentMethod { get; set; } = "";
    public DateTime IssueDate { get; set; } = DateTime.Now;
    public DateTime? DueDate { get; set; }
    public DateTime? PaidDate { get; set; }
}

/// <summary>
/// نماینی کارمند (Employee) برای تست‌ها
/// </summary>
public class FakeEmployee
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public string Email { get; set; } = "";
    public string Mobile { get; set; } = "";
    public string MelliCode { get; set; } = "";
    public string JobTitle { get; set; } = "";
    public string Department { get; set; } = "";
    public decimal Salary { get; set; }
    public DateTime HireDate { get; set; } = DateTime.Now;
    public string EmployeeNumber { get; set; } = "";
    public bool IsActive { get; set; } = true;

    public string FullName => $"{FirstName} {LastName}";
}

/// <summary>
/// نماینی بیمار (Patient) برای تست‌های پزشکی
/// </summary>
public class FakePatient
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public string MelliCode { get; set; } = "";
    public string PatientFileNumber { get; set; } = "";
    public string Mobile { get; set; } = "";
    public string Email { get; set; } = "";
    public string BloodType { get; set; } = "";
    public int Age { get; set; }
    public string Gender { get; set; } = ""; // مرد / زن
    public int Height { get; set; } // سانتی‌متر
    public int Weight { get; set; } // کیلوگرم
    public List<string> Allergies { get; set; } = new();
    public List<string> Diseases { get; set; } = new();
    public string HealthInsuranceNumber { get; set; } = "";
    public DateTime RegistrationDate { get; set; } = DateTime.Now;

    public string FullName => $"{FirstName} {LastName}";
}
