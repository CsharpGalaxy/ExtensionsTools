using CsharpGalaxy.LibraryExtension.FakeDataPersian.Helpers;

namespace CsharpGalaxy.LibraryExtension.Test.FakeDataPersian;

public class FakeDataFactoryTests
{
    [Fact]
    public void CreateFakeUser_ShouldReturnValidUser()
    {
        var user = FakeDataFactory.CreateFakeUser();
        Assert.NotNull(user);
        Assert.False(string.IsNullOrWhiteSpace(user.FirstName));
        Assert.False(string.IsNullOrWhiteSpace(user.LastName));
        Assert.False(string.IsNullOrWhiteSpace(user.Email));
        Assert.False(string.IsNullOrWhiteSpace(user.Mobile));
    }

    [Fact]
    public void CreateFakeUser_ShouldHaveValidEmail()
    {
        var user = FakeDataFactory.CreateFakeUser();
        Assert.Contains("@", user.Email);
        Assert.Contains(".", user.Email);
    }

    [Fact]
    public void CreateFakeUser_ShouldHaveValidMobile()
    {
        var user = FakeDataFactory.CreateFakeUser();
        Assert.StartsWith("09", user.Mobile);
        Assert.True(user.Mobile.All(char.IsDigit) || user.Mobile.Contains("-"));
    }

    [Fact]
    public void CreateFakeUser_ShouldHaveValidMelliCode()
    {
        var user = FakeDataFactory.CreateFakeUser();
        Assert.Equal(10, user.MelliCode.Length);
        Assert.True(user.MelliCode.All(char.IsDigit));
    }

    [Fact]
    public void CreateFakeUsers_ShouldReturnRequestedCount()
    {
        var users = FakeDataFactory.CreateFakeUsers(10);
        Assert.Equal(10, users.Count);
    }

    [Fact]
    public void CreateFakeUsers_ShouldReturnUniqueUsers()
    {
        var users = FakeDataFactory.CreateFakeUsers(10);
        var uniqueEmails = users.Select(u => u.Email).Distinct().ToList();
        Assert.Equal(users.Count, uniqueEmails.Count);
    }

    [Fact]
    public void CreateFakeProduct_ShouldReturnValidProduct()
    {
        var product = FakeDataFactory.CreateFakeProduct();
        Assert.NotNull(product);
        Assert.False(string.IsNullOrWhiteSpace(product.Name));
        Assert.True(product.Price > 0);
    }

    [Fact]
    public void CreateFakeProduct_ShouldHaveValidImage()
    {
        var product = FakeDataFactory.CreateFakeProduct();
        Assert.StartsWith("data:image", product.ImageBase64);
    }

    [Fact]
    public void CreateFakeProduct_ShouldHaveDiscountedPrice()
    {
        var product = FakeDataFactory.CreateFakeProduct();
        // Discount price should be less than original price (if applied)
        if (product.DiscountPrice > 0)
        {
            Assert.True(product.DiscountPrice < product.Price);
        }
    }

    [Fact]
    public void CreateFakeProducts_ShouldReturnRequestedCount()
    {
        var products = FakeDataFactory.CreateFakeProducts(5);
        Assert.Equal(5, products.Count);
    }

    [Fact]
    public void CreateFakeOrder_ShouldReturnValidOrder()
    {
        var order = FakeDataFactory.CreateFakeOrder();
        Assert.NotNull(order);
        Assert.False(string.IsNullOrWhiteSpace(order.OrderNumber));
        Assert.True(order.TotalAmount > 0);
    }

    [Fact]
    public void CreateFakeOrder_ShouldHaveOrderItems()
    {
        var order = FakeDataFactory.CreateFakeOrder();
        Assert.NotEmpty(order.Items);
    }

    [Fact]
    public void CreateFakeOrder_ShouldCalculateCorrectTotals()
    {
        var order = FakeDataFactory.CreateFakeOrder();
        var itemsTotal = order.Items.Sum(i => i.Subtotal);
        var expectedTotal = itemsTotal - order.DiscountAmount;
        Assert.Equal(expectedTotal, order.FinalAmount);
    }

    [Fact]
    public void CreateFakeOrder_ShouldHaveValidStatus()
    {
        var order = FakeDataFactory.CreateFakeOrder();
        Assert.False(string.IsNullOrWhiteSpace(order.Status));
    }

    [Fact]
    public void CreateFakeOrders_ShouldReturnRequestedCount()
    {
        var orders = FakeDataFactory.CreateFakeOrders(3);
        Assert.Equal(3, orders.Count);
    }

    [Fact]
    public void CreateFakeInvoice_ShouldReturnValidInvoice()
    {
        var invoice = FakeDataFactory.CreateFakeInvoice();
        Assert.NotNull(invoice);
        Assert.False(string.IsNullOrWhiteSpace(invoice.InvoiceNumber));
        Assert.True(invoice.Total > 0);
    }

    [Fact]
    public void CreateFakeInvoice_ShouldHaveValidDates()
    {
        var invoice = FakeDataFactory.CreateFakeInvoice();
        Assert.True(invoice.IssueDate <= DateTime.Now);
        Assert.True(invoice.DueDate > invoice.IssueDate);
    }

    [Fact]
    public void CreateFakeInvoice_ShouldHaveValidStatus()
    {
        var invoice = FakeDataFactory.CreateFakeInvoice();
        Assert.False(string.IsNullOrWhiteSpace(invoice.Status));
    }

    [Fact]
    public void CreateFakeInvoices_ShouldReturnRequestedCount()
    {
        var invoices = FakeDataFactory.CreateFakeInvoices(7);
        Assert.Equal(7, invoices.Count);
    }

    [Fact]
    public void CreateFakeEmployee_ShouldReturnValidEmployee()
    {
        var employee = FakeDataFactory.CreateFakeEmployee();
        Assert.NotNull(employee);
        Assert.False(string.IsNullOrWhiteSpace(employee.FirstName));
        Assert.False(string.IsNullOrWhiteSpace(employee.JobTitle));
        Assert.True(employee.Salary > 0);
    }

    [Fact]
    public void CreateFakeEmployee_ShouldHaveValidMelliCode()
    {
        var employee = FakeDataFactory.CreateFakeEmployee();
        Assert.Equal(10, employee.MelliCode.Length);
        Assert.True(employee.MelliCode.All(char.IsDigit));
    }

    [Fact]
    public void CreateFakeEmployee_ShouldHaveValidDates()
    {
        var employee = FakeDataFactory.CreateFakeEmployee();
        Assert.True(employee.HireDate <= DateTime.Now);
    }

    [Fact]
    public void CreateFakeEmployees_ShouldReturnRequestedCount()
    {
        var employees = FakeDataFactory.CreateFakeEmployees(4);
        Assert.Equal(4, employees.Count);
    }

    [Fact]
    public void CreateFakePatient_ShouldReturnValidPatient()
    {
        var patient = FakeDataFactory.CreateFakePatient();
        Assert.NotNull(patient);
        Assert.False(string.IsNullOrWhiteSpace(patient.FirstName));
        Assert.False(string.IsNullOrWhiteSpace(patient.BloodType));
    }

    [Fact]
    public void CreateFakePatient_ShouldHaveValidHealth()
    {
        var patient = FakeDataFactory.CreateFakePatient();
        Assert.InRange(patient.Height, 140, 220);
        Assert.InRange(patient.Weight, 40, 150);
    }

    [Fact]
    public void CreateFakePatient_ShouldHaveDiseasesAndAllergies()
    {
        var patient = FakeDataFactory.CreateFakePatient();
        // May or may not have diseases/allergies, but lists should be initialized
        Assert.NotNull(patient.Diseases);
        Assert.NotNull(patient.Allergies);
    }

    [Fact]
    public void CreateFakePatients_ShouldReturnRequestedCount()
    {
        var patients = FakeDataFactory.CreateFakePatients(6);
        Assert.Equal(6, patients.Count);
    }

    [Fact]
    public void FactoryMethods_ShouldCreateUniqueInstances()
    {
        var user1 = FakeDataFactory.CreateFakeUser();
        var user2 = FakeDataFactory.CreateFakeUser();
        Assert.NotEqual(user1.Email, user2.Email);
    }

    [Fact]
    public void LargeScaleCreation_ShouldWork()
    {
        var users = FakeDataFactory.CreateFakeUsers(100);
        var products = FakeDataFactory.CreateFakeProducts(50);
        var orders = FakeDataFactory.CreateFakeOrders(75);

        Assert.Equal(100, users.Count);
        Assert.Equal(50, products.Count);
        Assert.Equal(75, orders.Count);
    }
}
