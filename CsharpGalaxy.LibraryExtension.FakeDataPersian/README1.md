# CsharpGalaxy.LibraryExtension.FakeDataPersian

کتابخانهٔ تولید داده‌های تصادفی فارسی برای تست و توسعه

## ویژگی‌ها

### 🎭 PersianNameGenerator
نام‌های فارسی معتبر را تولید می‌کند:
- `FirstName()` - نام اول: زهرا، علی، کیان
- `LastName()` - نام خانوادگی: رضوی، نجفی، کاظمی
- `FullName()` - نام کامل: "زهرا رضوی"
- `FatherName()` - نام پدر: "غلام‌رضا"

### 🎲 EnumGenerator
مقادیر تصادفی از انواع Enum:
- `GetRandomValue<T>()` - یک مقدار تصادفی از enum
- `GetRandomValues<T>(count)` - لیستی از مقادیر تصادفی
- `GetAllValues<T>()` - تمام مقادیر enum
- `GetRandomEnumValue(enumType)` - بر اساس Type
- `GetRandomEnumValueByName(enumTypeName)` - بر اساس نام کلاس

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

#### ویژگی های جدید تاریخ شمسی:
- `GetDayNameFarsi()` - نام روز: شنبه، یکشنبه، ...
- `GetMonthNameFarsi(month)` - نام ماه: فروردین، اردیبهشت، ...
- `GetRandomMonthNameFarsi()` - نام ماه تصادفی
- `GetRandomShamsiYear(min, max)` - سال شمسی تصادفی
- `GetRandomShamsiMonth()` - ماه شمسی تصادفی (۱-۱۲)
- `GetRandomShamsiDay()` - روز شمسی تصادفی (۱-۳۰)
- `GetDaysInMonth(year, month)` - تعداد روزهای ماه
- `GetDaysInYear(year)` - تعداد روزهای سال (۳۶۵ یا ۳۶۶)
- `IsLeapYear(year)` - بررسی سال کبیسه
- `GetDayOfYear(year, month, day)` - روز سال (۱-۳۶۶)
- `GetWeekOfYear(year, month, day)` - هفتهٔ سال (۱-۵۳)
- `GetRandomPersianDateTime()` - تاریخ شمسی کامل
- `GetTodayPersian()` - امروز به شمسی
- `GetYesterdayPersian()` - دیروز به شمسی
- `GetTomorrowPersian()` - فردا به شمسی

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

### 🔧 FakeDataSeeder
تولید خودکار داده‌ها براساس Attribute‌ها:
- `Seed<T>()` - ایجاد یک نمونه با Attribute‌های کاستوم
- `SeedList<T>(count)` - ایجاد لیست نمونه‌ها
- Custom Attributes:
  - `[Constant(value)]` - مقدار ثابت برای property
  - `[Ignore]` - نادیده گرفتن property
  - `[Enum(enumType)]` - مقدار تصادفی از enum
  - `[Enum(enumType, allowedValues)]` - مقدار تصادفی از مقادیر محدود
  - `[FirstName]` - نام اول
  - `[LastName]` - نام خانوادگی
  - `[FullName]` - نام کامل
  - `[Email]` - ایمیل
  - `[Mobile]` - شماره موبایل
  - `[Username]` - نام‌کاربری
  - `[NationalCode]` - کد ملی
  - `[Address]` - آدرس
  - `[City]` - شهر
  - `[Province]` - استان
  - `[Word]` - کلمه
  - `[Sentence]` - جملهٔ
  - `[CompanyName]` - نام شرکت
  - `[JobTitle]` - عنوان شغلی
  - `[Iban]` - شماره شبا
  - `[CardNumber]` - شماره کارت
  - `[DateTime]` - تاریخ و ساعت
  - `[Boolean]` - مقدار بولی
  - `[Status]` - وضعیت
  - `[ForeignKey(type)]` - کلید خارجی
- پشتیبانی خودکار برای انواع:
  - `Guid` - GUID جدید
  - `Enum` - هر enum type تصادفی
  - `int` / `int?` - عدد صحیح تصادفی
  - `long` / `long?` - عدد بزرگ تصادفی
  - `decimal` / `decimal?` - عدد اعشاری تصادفی
  - `double` / `double?` - عدد ممیز شناور
  - `bool` / `bool?` - مقدار بولی تصادفی
  - `DateTime` / `DateTime?` - تاریخ تصادفی
  - `string` - کلمهٔ فارسی تصادفی
- **Attributes جدید تاریخ شمسی:**
  - `[PersianDate]` - تاریخ شمسی کامل
  - `[PersianDayName]` - نام روز فارسی
  - `[PersianMonthName]` - نام ماه فارسی
  - `[PersianYear]` - سال شمسی (بازهٔ پیش‌فرض: ۱۳۸۰-۱۴۱۰)
  - `[PersianYear(minYear, maxYear)]` - سال شمسی با بازهٔ سفارشی
  - `[PersianDateRange(startDate, endDate)]` - تاریخ در بازهٔ مشخص

### 🏗️ FakeBuilder
پترن Builder برای تولید داده‌های سفارشی:
- `RuleFor<T>(property, generator)` - قانون برای یک property
- `RuleForForeignKey<T>(property, generator)` - قانون برای کلید خارجی
- `RuleForEnum<T>(property, generator)` - قانون برای enum
- `RuleForListSelection<T>(property, items)` - انتخاب رندوم از لیست
- `RuleForPersianDayName<T>(property)` - نام روز فارسی
- `RuleForPersianMonthName<T>(property)` - نام ماه فارسی
- `RuleForPersianYear<T>(property, minYear, maxYear)` - سال شمسی
- `RuleForRandomPersianDate<T>(property)` - تاریخ شمسی کامل
- `RuleForPersianDateRange<T>(property, startDate, endDate)` - تاریخ در بازهٔ مشخص
- `RuleForPersianBirthDate<T>(property, age)` - تاریخ تولد شمسی
- `RuleForAllStrings(generator)` - قانون برای تمام string‌ها
- `RuleForAllInts(generator)` - قانون برای تمام int‌ها
- `RuleForAllBools(generator)` - قانون برای تمام bool‌ها
- `RuleForAllDecimals(generator)` - قانون برای تمام decimal‌ها
- `RuleForAllDateTimes(generator)` - قانون برای تمام DateTime‌ها
- `RuleForAllEnums(generator)` - قانون برای تمام enum‌ها
- `RuleForAllProperties(type, generator)` - قانون برای نوع مشخص
- `Build()` - ایجاد یک نمونه
- `BuildList(count)` - ایجاد لیست نمونه‌ها

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

### استفاده از FakeDataSeeder با Attribute‌ها
```csharp
using CsharpGalaxy.LibraryExtension.FakeDataPersian.Helpers;
using CsharpGalaxy.LibraryExtension.FakeDataPersian.Attributes;

// تعریف یک کلاس با Attribute‌ها
public class UserModel
{
    [Guid]
    public string Id { get; set; }
    
    [FirstName]
    public string FirstName { get; set; }
    
    [LastName]
    public string LastName { get; set; }
    
    [Email]
    public string Email { get; set; }
    
    [Mobile]
    public string Mobile { get; set; }
    
    [NationalCode]
    public string NationalCode { get; set; }
    
    [Address]
    public string Address { get; set; }
    
    [City]
    public string City { get; set; }
    
    [Ignore]  // این property نادیده گرفته می‌شود
    public string ManualProperty { get; set; }
    
    // نوع‌های پشتیبانی‌شده خودکار
    public Guid UserId { get; set; }
    public int Age { get; set; }
    public decimal Balance { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
}

// استفاده
var user = FakeDataSeeder.Seed<UserModel>();
Console.WriteLine($"{user.FirstName} {user.LastName} - {user.Email}");

// ایجاد لیست
var users = FakeDataSeeder.SeedList<UserModel>(10);
foreach (var u in users)
{
    Console.WriteLine($"{u.FirstName} - {u.Mobile}");
}
```

### استفاده از ConstantAttribute برای مقادیر ثابت
```csharp
using CsharpGalaxy.LibraryExtension.FakeDataPersian.Attributes;
using CsharpGalaxy.LibraryExtension.FakeDataPersian.Helpers;

// تعریف یک کلاس با مقادیر ثابت
public class ProductModel
{
    [Guid]
    public string Id { get; set; }
    
    [Word]
    public string Name { get; set; }
    
    // مقادیر ثابت
    [Constant("IRR")]  // واحد پولی ثابت
    public string Currency { get; set; }
    
    [Constant(0.09)]  // درصد مالیات ثابت
    public decimal TaxRate { get; set; }
    
    [Constant("فعال")]  // وضعیت ثابت
    public string Status { get; set; }
    
    [Constant(2024)]  // سال ثابت
    public int Year { get; set; }
    
    // داده‌های تصادفی
    public decimal Price { get; set; }
}

// استفاده
var product = FakeDataSeeder.Seed<ProductModel>();
Console.WriteLine($"{product.Name}");
Console.WriteLine($"Currency: {product.Currency}");  // همیشه "IRR"
Console.WriteLine($"Tax Rate: {product.TaxRate}");  // همیشه 0.09
Console.WriteLine($"Status: {product.Status}");  // همیشه "فعال"
Console.WriteLine($"Year: {product.Year}");  // همیشه 2024

// ایجاد لیست - همه محصولات مقادیر ثابت یکسانی دارند
var products = FakeDataSeeder.SeedList<ProductModel>(5);
foreach (var p in products)
{
    Assert.Equal("IRR", p.Currency);
    Assert.Equal(0.09, p.TaxRate);
    Assert.Equal("فعال", p.Status);
    Assert.Equal(2024, p.Year);
}
```

### ترکیب Constant و سایر Attributes
```csharp
public class OrderModel
{
    [Guid]
    public string Id { get; set; }
    
    [Email]
    public string CustomerEmail { get; set; }
    
    [Constant("درحال‌پردازش")]  // وضعیت ثابت
    public string OrderStatus { get; set; }
    
    [Constant(1)]  // نسخهٔ ثابت
    public int Version { get; set; }
    
    [DateTime]
    public DateTime CreatedDate { get; set; }
    
    [Ignore]  // نادیده می‌شود
    public string InternalNotes { get; set; }
}

// استفاده
var order = FakeDataSeeder.Seed<OrderModel>();
Console.WriteLine($"Order: {order.Id}");
Console.WriteLine($"Email: {order.CustomerEmail}");
Console.WriteLine($"Status: {order.OrderStatus}");  // همیشه "درحال‌پردازش"
Console.WriteLine($"Version: {order.Version}");  // همیشه 1
```

### استفاده از FakeBuilder با Fluent API
```csharp
using CsharpGalaxy.LibraryExtension.FakeDataPersian.Abstracts;
using CsharpGalaxy.LibraryExtension.FakeDataPersian.Generators;

// ساخت یک کاربر با قوانین سفارشی
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
    .RuleFor(x => x.IsActive, () => true)
    .Build();

Console.WriteLine($"{user.FullName} - {user.Email}");

// استفاده از RuleForAll برای گروه‌های property
var product = new FakeBuilder<FakeProduct>()
    .RuleForAllStrings(() => "محصول")
    .RuleFor(x => x.Id, () => InternetCryptoGenerator.GuidString())
    .RuleFor(x => x.Name, () => $"محصول {new Random().Next(1000, 9999)}")
    .RuleFor(x => x.Price, () => Convert.ToDecimal(new Random().Next(10000, 5000000)))
    .RuleFor(x => x.Stock, () => new Random().Next(0, 1000))
    .RuleFor(x => x.IsActive, () => true)
    .Build();

Console.WriteLine($"{product.Name} - {product.Price:C}");

// ایجاد لیست با BuildList
var products = new FakeBuilder<FakeProduct>()
    .RuleFor(x => x.Id, () => InternetCryptoGenerator.GuidString())
    .RuleFor(x => x.Name, () => $"محصول {new Random().Next(1000, 9999)}")
    .RuleFor(x => x.Price, () => Convert.ToDecimal(new Random().Next(10000, 5000000)))
    .RuleFor(x => x.IsActive, () => true)
    .BuildList(10);

Console.WriteLine($"{products.Count} محصول ساخته شد");
```

### مقایسه روش‌های مختلف
```csharp
// روش ۱: استفاده مستقیم از Generator ها
var user1 = new FakeUser
{
    Id = InternetCryptoGenerator.GuidString(),
    FirstName = PersianNameGenerator.FirstName(),
    LastName = PersianNameGenerator.LastName(),
    Email = PersianTextGenerator.Email(),
    Mobile = IranianMobileGenerator.Mobile()
};

// روش ۲: استفاده از FakeDataFactory
var user2 = FakeDataFactory.CreateFakeUser();

// روش ۳: استفاده از FakeDataSeeder
[Guid] public string Id { get; set; }
[FirstName] public string FirstName { get; set; }
[LastName] public string LastName { get; set; }
// ...
var user3 = FakeDataSeeder.Seed<UserModel>();

// روش ۴: استفاده از FakeBuilder
var user4 = new FakeBuilder<FakeUser>()
    .RuleFor(x => x.Id, () => InternetCryptoGenerator.GuidString())
    .RuleFor(x => x.FirstName, () => PersianNameGenerator.FirstName())
    .RuleFor(x => x.LastName, () => PersianNameGenerator.LastName())
    .RuleFor(x => x.Email, () => PersianTextGenerator.Email())
    .RuleFor(x => x.Mobile, () => IranianMobileGenerator.Mobile())
    .Build();
```

### مدیریت کلیدهای خارجی در FakeBuilder
```csharp
using CsharpGalaxy.LibraryExtension.FakeDataPersian.Attributes;
using CsharpGalaxy.LibraryExtension.FakeDataPersian.Abstracts;

// تعریف مدل‌های مرتبط
public class Category
{
    public string Id { get; set; }
    public string Name { get; set; }
}

public class Product
{
    public string Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    
    // کلید خارجی الزامی
    [ForeignKey(nameof(Category))]
    public string CategoryId { get; set; }
    
    // کلید خارجی اختیاری (۵۰٪ احتمال null)
    [ForeignKey(nameof(Category), IsOptional = true)]
    public Category Category { get; set; }
}

// استفاده: کلید خارجی الزامی
var product = new FakeBuilder<Product>()
    .RuleFor(x => x.Id, () => Guid.NewGuid().ToString())
    .RuleFor(x => x.Name, () => $"محصول {new Random().Next(1000, 9999)}")
    .RuleFor(x => x.Price, () => Convert.ToDecimal(new Random().Next(10000, 5000000)))
    .RuleForForeignKey(x => x.CategoryId, () => Guid.NewGuid().ToString())
    .Build();

Console.WriteLine($"{product.Name} - {product.CategoryId}");

// استفاده: کلید خارجی با related object
var productWithCategory = new FakeBuilder<Product>()
    .RuleFor(x => x.Id, () => Guid.NewGuid().ToString())
    .RuleFor(x => x.Name, () => $"محصول {new Random().Next(1000, 9999)}")
    .RuleFor(x => x.Price, () => Convert.ToDecimal(new Random().Next(10000, 5000000)))
    .RuleForForeignKey(x => x.Category, () => new FakeBuilder<Category>()
        .RuleFor(c => c.Id, () => Guid.NewGuid().ToString())
        .RuleFor(c => c.Name, () => "دسته‌بندی")
        .Build())
    .Build();

Console.WriteLine($"{productWithCategory.Name} - {productWithCategory.Category?.Name}");

// استفاده: ایجاد لیست با کلیدهای خارجی اختیاری
var products = new FakeBuilder<Product>()
    .RuleFor(x => x.Id, () => Guid.NewGuid().ToString())
    .RuleFor(x => x.Name, () => $"محصول {new Random().Next(1000, 9999)}")
    .RuleFor(x => x.Price, () => Convert.ToDecimal(new Random().Next(10000, 5000000)))
    .RuleForForeignKey(x => x.Category, () => new FakeBuilder<Category>()
        .RuleFor(c => c.Id, () => Guid.NewGuid().ToString())
        .RuleFor(c => c.Name, () => "دسته‌بندی")
        .Build())
    .BuildList(50);

// نصف محصولات Category خالی خواهند داشت، نصف دیگر دسته‌بندی دارند
var productsWithCategories = products.Where(p => p.Category != null).Count();
Console.WriteLine($"{productsWithCategories} محصول دسته‌بندی دارند");
```

### کنترل احتمال null برای کلیدهای خارجی اختیاری
```csharp
public class Order
{
    public string Id { get; set; }
    public string CustomerName { get; set; }
    
    // ۷۰٪ احتمال null
    [ForeignKey(nameof(Discount), IsOptional = true, NullProbability = 70)]
    public Discount Discount { get; set; }
    
    // ۲۰٪ احتمال null
    [ForeignKey(nameof(ShippingAddress), IsOptional = true, NullProbability = 20)]
    public ShippingAddress ShippingAddress { get; set; }
}

// استفاده
var order = new FakeBuilder<Order>()
    .RuleFor(x => x.Id, () => Guid.NewGuid().ToString())
    .RuleFor(x => x.CustomerName, () => PersianNameGenerator.FullName())
    .RuleForForeignKey(x => x.Discount, () => new FakeBuilder<Discount>()
        .RuleFor(d => d.Id, () => Guid.NewGuid().ToString())
        .RuleFor(d => d.Percentage, () => new Random().Next(5, 50))
        .Build())
    .RuleForForeignKey(x => x.ShippingAddress, () => new FakeBuilder<ShippingAddress>()
        .RuleFor(a => a.Address, () => PersianAddressGenerator.FullAddress())
        .Build())
    .Build();

// Discount: ۷۰٪ احتمال null
// ShippingAddress: ۲۰٪ احتمال null
```

### استفاده از EnumGenerator برای تولید مقادیر enum
```csharp
using CsharpGalaxy.LibraryExtension.FakeDataPersian.Generators;

// تعریف enum
public enum OrderStatus
{
    Pending = 0,
    Processing = 1,
    Shipped = 2,
    Delivered = 3,
    Cancelled = 4
}

public enum PaymentMethod
{
    CreditCard,
    BankTransfer,
    Cash
}

// استفاده مستقیم
var status = EnumGenerator.GetRandomValue<OrderStatus>();
Console.WriteLine($"وضعیت: {status}");  // مثلاً: Shipped

// ایجاد لیست
var statuses = EnumGenerator.GetRandomValues<OrderStatus>(5);

// تمام مقادیر
var allStatuses = EnumGenerator.GetAllValues<OrderStatus>();
Console.WriteLine($"تعداد وضعیت‌ها: {allStatuses.Length}");

// بر اساس نوع (Type)
var randomPayment = EnumGenerator.GetRandomEnumValue(typeof(PaymentMethod));
Console.WriteLine($"روش پرداخت: {randomPayment}");
```

### استفاده از Enum Attribute در FakeDataSeeder
```csharp
using CsharpGalaxy.LibraryExtension.FakeDataPersian.Attributes;
using CsharpGalaxy.LibraryExtension.FakeDataPersian.Helpers;

public class Order
{
    [Guid]
    public string Id { get; set; }
    
    [Email]
    public string CustomerEmail { get; set; }
    
    // استفاده از Enum Attribute
    [Enum(typeof(OrderStatus))]
    public OrderStatus Status { get; set; }
    
    [Enum(typeof(PaymentMethod))]
    public PaymentMethod PaymentMethod { get; set; }
}

// استفاده
var order = FakeDataSeeder.Seed<Order>();
Console.WriteLine($"Order {order.Id}");
Console.WriteLine($"Status: {order.Status}");  // تصادفی
Console.WriteLine($"Payment: {order.PaymentMethod}");  // تصادفی

// ایجاد لیست
var orders = FakeDataSeeder.SeedList<Order>(5);
```

### استفاده از Enum Attribute با محدودیت مقادیر
```csharp
public class Invoice
{
    [Guid]
    public string Id { get; set; }
    
    // فقط موارد موفق و تحویل‌شده
    [Enum(typeof(OrderStatus), OrderStatus.Delivered, OrderStatus.Processing)]
    public OrderStatus Status { get; set; }
    
    // فقط کارت اعتباری و انتقال بانکی
    [Enum(typeof(PaymentMethod), PaymentMethod.CreditCard, PaymentMethod.BankTransfer)]
    public PaymentMethod PaymentMethod { get; set; }
}

// استفاده
var invoice = FakeDataSeeder.Seed<Invoice>();
// Status تنها می‌تواند Delivered یا Processing باشد
// PaymentMethod تنها می‌تواند CreditCard یا BankTransfer باشد
```

### استفاده از Enum در FakeBuilder
```csharp
public class Product
{
    public string Id { get; set; }
    public string Name { get; set; }
    public OrderStatus Status { get; set; }
}

// استفاده
var products = new FakeBuilder<Product>()
    .RuleFor(x => x.Id, () => Guid.NewGuid().ToString())
    .RuleFor(x => x.Name, () => PersianTextGenerator.Word())
    .RuleForEnum(x => x.Status, () => 
    {
        var statuses = new[] { OrderStatus.Shipped, OrderStatus.Delivered };
        return statuses[new Random().Next(statuses.Length)];
    })
    .BuildList(5);

// یا استفاده از RuleForAllEnums برای تمام enum properties
var items = new FakeBuilder<Product>()
    .RuleFor(x => x.Id, () => Guid.NewGuid().ToString())
    .RuleFor(x => x.Name, () => PersianTextGenerator.Word())
    .RuleForAllEnums(() => EnumGenerator.GetRandomEnumValue(typeof(OrderStatus)))
    .BuildList(10);
```

### استفاده از RuleForListSelection برای انتخاب رندوم از لیست
```csharp
// تعریف مدل
public class Product
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Color { get; set; }
    public string Size { get; set; }
    public int Stock { get; set; }
}

// استفاده: انتخاب رندوم از لیست اپشن‌ها
var colors = new[] { "قرمز", "آبی", "سبز", "زرد" };
var sizes = new[] { "S", "M", "L", "XL" };
var stocks = new[] { 10, 20, 50, 100 };

var product = new FakeBuilder<Product>()
    .RuleFor(x => x.Id, () => Guid.NewGuid().ToString())
    .RuleFor(x => x.Name, () => "محصول")
    .RuleForListSelection(x => x.Color, colors)  // انتخاب رندوم از رنگ‌ها
    .RuleForListSelection(x => x.Size, sizes)    // انتخاب رندوم از سایز‌ها
    .RuleForListSelection(x => x.Stock, stocks)  // انتخاب رندوم از موجودی‌ها
    .Build();

Console.WriteLine($"{product.Name} - {product.Color} ({product.Size})");

// ایجاد لیست - هر محصول یک انتخاب رندوم از لیست دریافت می‌کند
var products = new FakeBuilder<Product>()
    .RuleFor(x => x.Id, () => Guid.NewGuid().ToString())
    .RuleFor(x => x.Name, () => "محصول")
    .RuleForListSelection(x => x.Color, colors)
    .RuleForListSelection(x => x.Size, sizes)
    .BuildList(50);

// هر کدام یک رنگ و سایز تصادفی دارند
Console.WriteLine($"Created {products.Count} products with random colors and sizes");

// استفاده با IEnumerable
var statusList = new List<string> { "فعال", "غیرفعال", "معلق" };
var product2 = new FakeBuilder<Product>()
    .RuleFor(x => x.Id, () => Guid.NewGuid().ToString())
    .RuleFor(x => x.Name, () => "محصول")
    .RuleForListSelection(x => x.Color, statusList)
    .Build();

// استفاده با متد زنجیری (Chaining)
var product3 = new FakeBuilder<Product>()
    .RuleFor(x => x.Id, () => Guid.NewGuid().ToString())
    .RuleFor(x => x.Name, () => "محصول خاص")
    .RuleForListSelection(x => x.Color, colors)
    .RuleFor(x => x.Stock, () => 500)
    .RuleForListSelection(x => x.Size, sizes)
    .Build();

Console.WriteLine($"{product3.Name} - Stock: {product3.Stock}");
```

## مثال جامع - استفاده تمام Attributes و Features

```csharp
using CsharpGalaxy.LibraryExtension.FakeDataPersian.Abstracts;
using CsharpGalaxy.LibraryExtension.FakeDataPersian.Attributes;
using CsharpGalaxy.LibraryExtension.FakeDataPersian.Generators;
using CsharpGalaxy.LibraryExtension.FakeDataPersian.Helpers;

// ===== تعریف Enums =====
public enum OrderStatus
{
    Pending,
    Processing,
    Shipped,
    Delivered,
    Cancelled
}

public enum PaymentMethod
{
    CreditCard,
    BankTransfer,
    Cash
}

// ===== تعریف مدل‌های مرتبط =====
public class Category
{
    public string Id { get; set; }
    public string Name { get; set; }
}

public class Product
{
    [Guid]
    public string Id { get; set; }
    
    [Word]
    public string Name { get; set; }
    
    // مقدار ثابت
    [Constant("IRR")]
    public string Currency { get; set; }
    
    // مقدار ثابت درصد
    [Constant(0.09)]
    public decimal TaxRate { get; set; }
    
    // Enum تصادفی
    [Enum(typeof(OrderStatus))]
    public OrderStatus Status { get; set; }
    
    // نادیده گرفتن
    [Ignore]
    public string Notes { get; set; }
    
    // کلید خارجی الزامی
    [ForeignKey(nameof(Category))]
    public Category Category { get; set; }
}

public class Order
{
    // مقدار ثابت
    [Constant("ORDER-2024")]
    public string OrderPrefix { get; set; }
    
    [Guid]
    public string Id { get; set; }
    
    [FullName]
    public string CustomerName { get; set; }
    
    [Email]
    public string CustomerEmail { get; set; }
    
    [Mobile]
    public string CustomerPhone { get; set; }
    
    // Enum تصادفی
    [Enum(typeof(OrderStatus))]
    public OrderStatus Status { get; set; }
    
    // Enum محدود
    [Enum(typeof(PaymentMethod), PaymentMethod.CreditCard, PaymentMethod.BankTransfer)]
    public PaymentMethod PaymentMethod { get; set; }
    
    [DateTime]
    public DateTime CreatedDate { get; set; }
    
    // کلید خارجی اختیاری با احتمال null ۲۰٪
    [ForeignKey(nameof(Product), IsOptional = true, NullProbability = 20)]
    public Product Product { get; set; }
    
    // نادیده گرفتن
    [Ignore]
    public decimal ManualDiscount { get; set; }
}

// ===== استفاده کامل =====

// ۱. استفاده با FakeDataSeeder (Attribute-based)
Console.WriteLine("=== FakeDataSeeder Example ===");
var order1 = FakeDataSeeder.Seed<Order>();
Console.WriteLine($"Order: {order1.OrderPrefix}");  // ORDER-2024
Console.WriteLine($"Customer: {order1.CustomerName}");
Console.WriteLine($"Email: {order1.CustomerEmail}");
Console.WriteLine($"Status: {order1.Status}");  // تصادفی
Console.WriteLine($"Payment: {order1.PaymentMethod}");  // فقط CreditCard یا BankTransfer
Console.WriteLine($"Product: {order1.Product?.Name ?? "No Product"}");  // ۸۰٪ احتمال
Console.WriteLine($"Discount: {order1.ManualDiscount}");  // 0 (ignored)
Console.WriteLine();

// ۲. ایجاد لیست با FakeDataSeeder
Console.WriteLine("=== FakeDataSeeder List ===");
var orders = FakeDataSeeder.SeedList<Order>(5);
Console.WriteLine($"Created {orders.Count} orders");
foreach (var o in orders)
{
    Console.WriteLine($"  - {o.OrderPrefix}: {o.CustomerName} ({o.Status})");
}
Console.WriteLine();

// ۳. استفاده با FakeBuilder (Fluent API)
Console.WriteLine("=== FakeBuilder Example ===");
var order2 = new FakeBuilder<Order>()
    .RuleFor(x => x.OrderPrefix, () => "ORDER-2024")
    .RuleFor(x => x.Id, () => Guid.NewGuid().ToString())
    .RuleFor(x => x.CustomerName, () => PersianNameGenerator.FullName())
    .RuleFor(x => x.CustomerEmail, () => PersianTextGenerator.Email())
    .RuleFor(x => x.CustomerPhone, () => IranianMobileGenerator.Mobile())
    .RuleForEnum(x => x.Status, () => OrderStatus.Completed)
    .RuleForEnum(x => x.PaymentMethod, () => PaymentMethod.BankTransfer)
    .RuleFor(x => x.CreatedDate, () => DateTime.Now.AddDays(-Random.Shared.Next(30)))
    .RuleForForeignKey(x => x.Product, () => new FakeBuilder<Product>()
        .RuleFor(p => p.Id, () => Guid.NewGuid().ToString())
        .RuleFor(p => p.Name, () => $"محصول {Random.Shared.Next(1000, 9999)}")
        .RuleForForeignKey(p => p.Category, () => new FakeBuilder<Category>()
            .RuleFor(c => c.Id, () => Guid.NewGuid().ToString())
            .RuleFor(c => c.Name, () => "الکترونیکس")
            .Build())
        .Build())
    .Build();

Console.WriteLine($"Order: {order2.OrderPrefix}");
Console.WriteLine($"Customer: {order2.CustomerName}");
Console.WriteLine($"Product: {order2.Product?.Name}");
Console.WriteLine($"Category: {order2.Product?.Category?.Name}");
Console.WriteLine();

// ۴. ایجاد لیست بزرگ با FakeBuilder
Console.WriteLine("=== FakeBuilder List (1000 items) ===");
var largeOrders = new FakeBuilder<Order>()
    .RuleFor(x => x.OrderPrefix, () => "ORDER-2024")
    .RuleFor(x => x.Id, () => Guid.NewGuid().ToString())
    .RuleFor(x => x.CustomerName, () => PersianNameGenerator.FullName())
    .RuleFor(x => x.CustomerEmail, () => PersianTextGenerator.Email())
    .RuleFor(x => x.CustomerPhone, () => IranianMobileGenerator.Mobile())
    .RuleForEnum(x => x.Status, () => EnumGenerator.GetRandomValue<OrderStatus>())
    .RuleForEnum(x => x.PaymentMethod, () => EnumGenerator.GetRandomValue<PaymentMethod>())
    .RuleFor(x => x.CreatedDate, () => DateTime.Now.AddDays(-Random.Shared.Next(365)))
    .RuleForForeignKey(x => x.Product, () => new FakeBuilder<Product>()
        .RuleFor(p => p.Id, () => Guid.NewGuid().ToString())
        .RuleFor(p => p.Name, () => $"محصول {Random.Shared.Next(1000, 9999)}")
        .RuleForForeignKey(p => p.Category, () => new FakeBuilder<Category>()
            .RuleFor(c => c.Id, () => Guid.NewGuid().ToString())
            .RuleFor(c => c.Name, () => "دسته‌بندی")
            .Build())
        .Build())
    .BuildList(1000);

Console.WriteLine($"Created {largeOrders.Count} orders");
var withProducts = largeOrders.Count(o => o.Product != null);
Console.WriteLine($"Orders with products: {withProducts} (expected ~800)");
Console.WriteLine($"Orders without products: {largeOrders.Count - withProducts} (expected ~200)");
Console.WriteLine();

// ۵. مقایسه Constant مقادیر در لیست
Console.WriteLine("=== Constant Values Consistency ===");
var products = FakeDataSeeder.SeedList<Product>(10);
var currencies = products.Select(p => p.Currency).Distinct();
var taxes = products.Select(p => p.TaxRate).Distinct();
Console.WriteLine($"Unique currencies in 10 products: {currencies.Count()} (expected 1)");
Console.WriteLine($"Unique tax rates in 10 products: {taxes.Count()} (expected 1)");
foreach (var p in products.Take(3))
{
    Console.WriteLine($"  - Product: {p.Name}, Currency: {p.Currency}, Tax: {p.TaxRate}");
}
Console.WriteLine();

// ۶. بررسی Enum محدود
Console.WriteLine("=== Limited Enum Values ===");
var limitedOrders = FakeDataSeeder.SeedList<Order>(20);
var payments = limitedOrders.Select(o => o.PaymentMethod).Distinct();
Console.WriteLine($"Unique payment methods in 20 orders: {payments.Count()} (expected 2)");
foreach (var payment in payments)
{
    Console.WriteLine($"  - {payment}");
}
Console.WriteLine();

// ۷. استفاده Factory برای اشیاء کامل
Console.WriteLine("=== Factory Pattern ===");
var factoryUser = FakeDataFactory.CreateFakeUser();
Console.WriteLine($"User: {factoryUser.FullName}");
Console.WriteLine($"Email: {factoryUser.Email}");
Console.WriteLine($"Mobile: {factoryUser.Mobile}");
Console.WriteLine();

// ۸. Generator مستقیم
Console.WriteLine("=== Direct Generator Usage ===");
Console.WriteLine($"Random enum: {EnumGenerator.GetRandomValue<OrderStatus>()}");
Console.WriteLine($"Random mobile: {IranianMobileGenerator.Mobile()}");
Console.WriteLine($"Random email: {PersianTextGenerator.Email()}");
Console.WriteLine($"Random GUID: {InternetCryptoGenerator.GuidString()}");
```

**خلاصهٔ مثال:**

✅ **FakeDataSeeder:**
- Attribute-based generation
- Constant values
- Enum attributes (محدود و عادی)
- Ignore properties
- Foreign keys

✅ **FakeBuilder:**
- Fluent API
- Custom rules
- Nested objects
- Large lists
- RuleForEnum و RuleForForeignKey
- **روز و ماه به فارسی** ⭐
- **تاریخ شمسی پیشرفته** ⭐

✅ **Features:**
- مقادیر ثابت (Currency, TaxRate)
- Enum تصادفی (OrderStatus)
- Enum محدود (PaymentMethod)
- کلیدهای خارجی (Category, Product)
- کلیدهای اختیاری با NullProbability
- نادیده گرفتن (Ignore)
- **نام روزهای هفته فارسی**
- **نام ماه‌های شمسی فارسی**
- **محاسبات تاریخ شمسی (کبیسه، روز سال، هفتهٔ سال)**

## بخش جدید: Persian Date Features

### نمونه استفاده از روز و ماه فارسی:

```csharp
using CsharpGalaxy.LibraryExtension.FakeDataPersian.Generators;
using CsharpGalaxy.LibraryExtension.FakeDataPersian.Abstracts;

// ۱. نام روز فارسی
var dayName = PersianDateGenerator.GetDayNameFarsi();
Console.WriteLine($"روز: {dayName}");  // شنبه، یکشنبه، دوشنبه، ...

// ۲. نام ماه فارسی
var monthName = PersianDateGenerator.GetMonthNameFarsi(1);  // فروردین
Console.WriteLine($"ماه: {monthName}");

var randomMonth = PersianDateGenerator.GetRandomMonthNameFarsi();
Console.WriteLine($"ماه تصادفی: {randomMonth}");

// ۳. سال شمسی تصادفی
var year = PersianDateGenerator.GetRandomShamsiYear();
Console.WriteLine($"سال: {year}");  // ۱۳۸۰ - ۱۴۱۰

var customYear = PersianDateGenerator.GetRandomShamsiYear(1400, 1405);
Console.WriteLine($"سال سفارشی: {customYear}");

// ۴. محاسبات تاریخ شمسی
var daysInMonth = PersianDateGenerator.GetDaysInMonth(1400, 1);
Console.WriteLine($"روزهای فروردین: {daysInMonth}");  // ۳۱

var daysInYear = PersianDateGenerator.GetDaysInYear(1400);
Console.WriteLine($"روزهای ۱۴۰۰: {daysInYear}");  // ۳۶۵ یا ۳۶۶

bool isLeap = PersianDateGenerator.IsLeapYear(1403);
Console.WriteLine($"۱۴۰۳ کبیسه است؟ {isLeap}");

// ۵. روز سال و هفتهٔ سال
var dayOfYear = PersianDateGenerator.GetDayOfYear(1400, 1, 1);
Console.WriteLine($"روز ۱۴۰۰/۱/۱: {dayOfYear}");  // ۱

var weekOfYear = PersianDateGenerator.GetWeekOfYear(1400, 1, 1);
Console.WriteLine($"هفتهٔ ۱۴۰۰/۱/۱: {weekOfYear}");  // ۱

// ۶. تاریخ شمسی به صورت کامل
var persianDate = PersianDateGenerator.GetRandomPersianDateTime();
Console.WriteLine($"تاریخ شمسی: {persianDate}");
Console.WriteLine($"  سال: {persianDate.Year}");
Console.WriteLine($"  ماه: {persianDate.Month}");
Console.WriteLine($"  روز: {persianDate.Day}");

// ۷. تاریخ‌های خاص
var today = PersianDateGenerator.GetTodayPersian();
var yesterday = PersianDateGenerator.GetYesterdayPersian();
var tomorrow = PersianDateGenerator.GetTomorrowPersian();

Console.WriteLine($"امروز: {today}");
Console.WriteLine($"دیروز: {yesterday}");
Console.WriteLine($"فردا: {tomorrow}");
```

### استفاده با FakeBuilder:

```csharp
class Article
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string PublishedDay { get; set; }
    public string PublishedMonth { get; set; }
    public int PublishedYear { get; set; }
}

// استفاده PersianDate در FakeBuilder
var article = new FakeBuilder<Article>()
    .RuleFor(x => x.Id, () => Random.Shared.Next(1, 1000))
    .RuleFor(x => x.Title, () => $"مقاله {Random.Shared.Next(100)}")
    .RuleForPersianDayName(x => x.PublishedDay)
    .RuleForPersianMonthName(x => x.PublishedMonth)
    .RuleForPersianYear(x => x.PublishedYear, 1400, 1410)
    .Build();

Console.WriteLine($"عنوان: {article.Title}");
Console.WriteLine($"تاریخ: {article.PublishedDay} {article.PublishedMonth} {article.PublishedYear}");
```

### Attributes برای تاریخ شمسی:

```csharp
class BlogPost
{
    public int Id { get; set; }
    
    [PersianDayName]
    public string DayName { get; set; }
    
    [PersianMonthName]
    public string MonthName { get; set; }
    
    [PersianYear]
    public int Year { get; set; }
    
    [PersianYear(1400, 1405)]
    public int YearInRange { get; set; }
    
    [PersianDateRange("1400/01/01", "1410/12/29")]
    public PersianDateGenerator.PersianDateTime PublishedDate { get; set; }
}

// استفاده Seeder
var post = FakeDataSeeder.Seed<BlogPost>();
Console.WriteLine($"روز: {post.DayName}");
Console.WriteLine($"ماه: {post.MonthName}");
Console.WriteLine($"سال: {post.Year}");
Console.WriteLine($"تاریخ انتشار: {post.PublishedDate}");
```

## نیازمندی‌ها

- .NET 8.0+

## مجوز

این پروژه تحت مجوز MIT منتشر شده است.

