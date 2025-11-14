# CsharpGalaxy.LibraryExtension.FakeDataPersian

کتابخانهٔ تولید داده‌های تصادفی فارسی برای تست و توسعه

## ویژگی‌ها

### 🎭 PersianNameGenerator
نام‌های فارسی معتبر را تولید می‌کند:
- `FirstName()` - نام اول: زهرا، علی، کیان
- `LastName()` - نام خانوادگی: رضوی، نجفی، کاظمی
- `FullName()` - نام کامل: "زهرا رضوی"
- `FatherName()` - نام پدر: "غلام‌رضا"

### 📱 IranianMobileGenerator
شماره موبایل معتبر ایرانی:
- `Mobile()` - شماره ۱۱ رقمی: ۰۹۱۲۳۴۵۶۷۸۹
- `MobileFormatted()` - فرمت شده: ۰۹۱۲-۹۹۹-۹۹۹۹
- `Operator()` - نام اپراتور: همراه‌اول، ایرانسل، رایتل
- `IsValidMobile(mobile)` - بررسی معتبر بودن

### 🆔 IranianNationalCodeGenerator
کد ملی معتبر:
- `MelliCode()` - کد ملی ۱۰ رقمی معتبر
- `IsValidMelliCode(code)` - بررسی با الگوریتم کنترل‌رقم
- `ValidateMelliCode(code)` - بررسی تفصیلی

### 📍 PersianAddressGenerator
آدرس‌های فارسی:
- `City()` - شهر: ساری، بابل، قائم‌شهر
- `Province()` - استان: مازندران، گیلان، تهران
- `FullAddress()` - آدرس کامل
- `ShortAddress()` - آدرس خلاصه

### 📅 PersianDateGenerator
تاریخ و ساعت شمسی:
- `ShamsiDate()` - تاریخ: ۱۴۰۳/۰۳/۱۴
- `ShamsiDateTime()` - تاریخ و ساعت: ۱۴۰۳/۰۳/۱۴ ۱۶:۴۵:۲۲
- `Age(from, to)` - سن: ۱۸..۶۰
- `BirthDate(age)` - تاریخ تولد

### 📝 PersianTextGenerator
متن‌های فارسی:
- `Sentence()` - جملهٔ تصادفی
- `Sentences(count)` - چندین جملهٔ
- `Word()` - کلمهٔ تصادفی
- `Words(count)` - چندین کلمه
- `Email()` - ایمیل: zahra.rezavi@example.com
- `Username()` - نام‌کاربری: zahra_85

### 💳 BankingMoneyGenerator
اطلاعات بانکی:
- `Sheba()` - شماره شبا: IR۰۱۰۰۱۰۰۰۰۰۰۰۰۰۰۰۰۰۰۰۰
- `ShebaFormatted()` - فرمت شده
- `CardNumber()` - شماره کارت ۱۶ رقمی
- `CardNumberFormatted()` - فرمت: ۶۰۳۷-۹۹۹۹-۹۹۹۹-۹۹۹۹
- `BankName()` - نام بانک: ملت، ملی، سامان
- `AccountNumber()` - شماره حساب
- `CardCVV2()` - رمز کارت
- `CardExpiryDate()` - تاریخ انقضا

### 🌐 InternetCryptoGenerator
اطلاعات شبکه:
- `IPv4()` - آدرس IP: ۱۰.۲.۳.۴
- `IPv4Private()` - IP خصوصی
- `MAC()` - آدرس MAC: ۰۰:۱A:۲B:۳C:۴D:۵E
- `Guid()` - GUID
- `GuidString()` - GUID متنی
- `Token()` - توکن تصادفی
- `Url()` - URL: https://hello-world.com

### 🎲 CollectionHelper
کمک‌های مجموعه‌ای:
- `RandomList<T>(generator, count)` - لیست تصادفی
- `UniqueList<T>(generator, count)` - لیست یکتا
- `ToDataTable<T>(items)` - تبدیل به DataTable
- `RandomItem<T>(items)` - نمونهٔ تصادفی
- `Shuffle<T>(items)` - مخلوط کردن
- `Batch<T>(items, size)` - تقسیم به دسته‌ها

### 🖼️ ImageGenerator
تصاویر تصادفی (Base64):
- `PlaceholderBase64(width, height)` - تصویر پلیسهولدر رنگی
- `MaleAvatarBase64()` - آواتار مرد
- `FemaleAvatarBase64()` - آواتار زن
- `SimpleQRCodeBase64(text)` - QR Code ساده
- `SimpleChartBase64(values, labels)` - نمودار ستونی
- `RandomCheckerboardBase64(gridSize)` - شطرنجی تصادفی

### 💼 BusinessDataGenerator
داده‌های تجاری:
- `CompanyName()` - نام شرکت
- `CompanyMelliId()` - شماره ملی شرکت
- `JobTitle()` - عنوان شغلی
- `ContractNumber()` - شماره قرارداد
- `ProjectNumber()` / `ProjectStatus()` / `ProjectProgress()`
- `InvoiceNumber()` / `InvoiceStatus()`
- `Amount()` - مبلغ تصادفی
- `PaymentMethod()` - روش پرداخت
- `OrderNumber()` - شماره سفارش
- `ProductSKU()` - کد محصول
- `CustomerAccountNumber()` - شماره حساب مشتری

### 🏥 HealthMedicalGenerator
داده‌های پزشکی:
- `BloodType()` - گروه خونی
- `Height()` / `Weight()` - قد و وزن
- `CalculateBMI()` - شاخص توده‌بدن
- `BloodPressure()` - فشار خون
- `HeartRate()` - ضربان قلب
- `BloodOxygenLevel()` - سطح اکسیژن
- `BodyTemperature()` - درجهٔ حرارت
- `CommonDisease()` - بیماری شایع
- `Medication()` / `MedicationDose()` / `MedicationFrequency()`
- `Allergy()` - آلرژی
- `DoctorSpecialty()` - تخصص پزشک
- `PatientFileNumber()` - شماره پرونده بیمار
- `HealthInsuranceNumber()` - شماره بیمه

### 🏭 FakeDataFactory
تولید اشیاء کامل:
- `CreateFakeUser()` - کاربر کامل
- `CreateFakeProduct()` - محصول کامل
- `CreateFakeOrder()` - سفارش کامل (با آیتم‌ها)
- `CreateFakeInvoice()` - فاکتور کامل
- `CreateFakeEmployee()` - کارمند کامل
- `CreateFakePatient()` - بیمار کامل (پزشکی)

## مثال استفاده

### ایجاد اشیاء تستی ساده
```csharp
using CsharpGalaxy.LibraryExtension.FakeDataPersian.Generators;
using CsharpGalaxy.LibraryExtension.FakeDataPersian.Helpers;

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

// داده‌های پزشکی
var bloodType = HealthMedicalGenerator.BloodType();
var bmi = HealthMedicalGenerator.CalculateBMI(170, 70);
var patient = HealthMedicalGenerator.DoctorSpecialty();

// مجموعه‌ای
var names = CollectionHelper.RandomList(
    () => PersianNameGenerator.FullName(), 
    count: 10
);
```

### استفاده از Factory برای اشیاء کامل
```csharp
// ایجاد یک کاربر کامل
var user = FakeDataFactory.CreateFakeUser();
Console.WriteLine($"{user.FullName} - {user.Email}");

// ایجاد یک محصول کامل
var product = FakeDataFactory.CreateFakeProduct();
Console.WriteLine($"{product.Name} - {product.Price}");

// ایجاد یک سفارش کامل
var order = FakeDataFactory.CreateFakeOrder();
Console.WriteLine($"سفارش {order.OrderNumber}: {order.FinalAmount} تومان");

// ایجاد یک فاکتور کامل
var invoice = FakeDataFactory.CreateFakeInvoice();
Console.WriteLine($"فاکتور {invoice.InvoiceNumber}: {invoice.Total}");

// ایجاد یک کارمند کامل
var employee = FakeDataFactory.CreateFakeEmployee();
Console.WriteLine($"{employee.FullName} - {employee.JobTitle}");

// ایجاد یک بیمار کامل
var patient = FakeDataFactory.CreateFakePatient();
Console.WriteLine($"{patient.FullName} - {patient.BloodType}");

// ایجاد چندین نمونه
var users = FakeDataFactory.CreateFakeUsers(100);
var products = FakeDataFactory.CreateFakeProducts(50);
var orders = FakeDataFactory.CreateFakeOrders(200);
var patients = FakeDataFactory.CreateFakePatients(30);
```

## نیازمندی‌ها

- .NET 8.0+

## مجوز

این پروژه تحت مجوز MIT منتشر شده است.
