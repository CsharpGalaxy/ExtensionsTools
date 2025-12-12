// See https://aka.ms/new-console-template for more information
using CsharpGalaxy.LibraryExtension.EFCore.Models.PagedList;
using CsharpGalaxy.LibraryExtension.Extensions.Object;
using CsharpGalaxy.LibraryExtension.Extensions.Strings;
using CsharpGalaxy.LibraryExtension.FakeDataPersian.Generators;
using CsharpGalaxy.LibraryExtension.FakeDataPersian.Helpers;
using CsharpGalexy.LibraryExtention.Helpers.Collections;
using System.IO;
var pageInfo = new PagedListInfo
{
    PageNumber = 2,
    PageSize = 20,
    TotalCount = 150,
    Filters = new Dictionary<string, object>()
    {
        {"Search","abolfazl" },
        {"locationId","12" },
    }
};
var pageInfo1=pageInfo.GetFilter("Search");
Console.WriteLine($"Page {pageInfo.PageNumber} of {pageInfo.TotalPages}");
//Console.WriteLine($"Search: {pageInfo.Filters.Search}, LocationId: {pageInfo.Filters.LocationId}");


// نام‌های تصادفی
var firstName = PersianNameGenerator.FirstName();
var fullName = PersianNameGenerator.FullName();

// موبایل معتبر
var mobile = IranianMobileGenerator.Mobile();
var isValid = IranianMobileGenerator.IsValidMobile(mobile);

// کد ملی
var melliCode = IranianNationalCodeGenerator.MelliCode();

// آدرس
var address = PersianAddressGenerator.FullAddress();

// تاریخ شمسی
var shamsiDate = PersianDateGenerator.ShamsiDate();
var age = PersianDateGenerator.Age(18, 60);

// متن
var email = PersianTextGenerator.Email();
var username = PersianTextGenerator.Username();

// بانکی
var sheba = BankingMoneyGenerator.Sheba();
var cardNumber = BankingMoneyGenerator.CardNumberFormatted();

// شبکه
var ipv4 = InternetCryptoGenerator.IPv4Private();
var guid = InternetCryptoGenerator.GuidString();

// تصاویر (Base64)
var avatarMale = ImageGenerator.MaleAvatarBase64();
var qrCode = ImageGenerator.SimpleQRCodeBase64("https://example.com");
var chart = ImageGenerator.SimpleChartBase64(
    new[] { 10, 20, 15, 25 },
    new[] { "فروردین", "اردیبهشت", "خرداد", "تیر" }
);

// داده‌های تجاری
var company = BusinessDataGenerator.CompanyName();
var jobTitle = BusinessDataGenerator.JobTitle();
var invoice = BusinessDataGenerator.InvoiceNumber();
// مجموعه‌ای
var names = CollectionHelper.RandomList(
    () => PersianNameGenerator.FullName(),
    count: 10
);
// داده‌های پزشکی
var bloodType = HealthMedicalGenerator.BloodType();
var bmi = HealthMedicalGenerator.CalculateBMI(170, 70);
var patient = HealthMedicalGenerator.DoctorSpecialty();

public class Person
{
    public Address Address { get; set; }
}

public class Address
{
    public City City { get; set; }
}

public class City
{
    public string Street { get; set; }
}
