# کلاس‌های CountryDialCode و CountryDialCodeHelper

این مستندات شامل کلاس‌های `CountryDialCode` و `CountryDialCodeHelper` است که قابلیت‌هایی برای مدیریت و دریافت کدهای شماره‌گیری کشورها را فراهم می‌کنند.

## کلاس CountryDialCode

یک کلاس مدل داده که نشان‌دهنده یک کشور با کد شماره‌گیری آن است.

| پراپرتی | نوع | توضیحات |
|---------|-----|----------|
| DialCode | string | کد شماره‌گیری بین‌المللی کشور (مانند "+98") |
| PersianCountryName | string | نام کشور به زبان فارسی |
| EnglishCountryName | string | نام کشور به زبان انگلیسی |

## کلاس CountryDialCodeHelper

یک کلاس کمکی استاتیک که متدهایی برای کار با کدهای شماره‌گیری کشورها از یک منبع JSON آنلاین ارائه می‌دهد.

### متدها

| متد | پارامترها | نوع خروجی | توضیحات |
|-----|-----------|------------|----------|
| InitializeAsync | ندارد | Task | داده‌های کشورها را مقداردهی اولیه و ذخیره می‌کند. داده‌ها فقط یک بار بارگذاری شده و برای درخواست‌های بعدی مجدداً استفاده می‌شوند. |
| LoadFromJsonAsync | ندارد | Task<List<CountryDialCode>> | داده‌های کشورها را از یک فایل JSON آنلاین بارگذاری می‌کند. یک لیست از کدهای شماره‌گیری کشورها برمی‌گرداند. |
| GetAllCountriesAsync | ندارد | Task<IReadOnlyList<CountryDialCode>> | لیست فقط-خواندنی از تمام کشورها با کدهای شماره‌گیری آنها را برمی‌گرداند. |
| GetPersianCountryByDialCodeAsync | string dialCode | Task<string?> | نام فارسی کشور را با کد شماره‌گیری آن دریافت می‌کند. اگر پیدا نشود null برمی‌گرداند. |
| GetEnglishCountryByDialCodeAsync | string dialCode | Task<string?> | نام انگلیسی کشور را با کد شماره‌گیری آن دریافت می‌کند. اگر پیدا نشود null برمی‌گرداند. |
| GetDialCodeByPersianCountryAsync | string persianCountryName | Task<string?> | کد شماره‌گیری را با نام فارسی کشور دریافت می‌کند (غیرحساس به حروف کوچک و بزرگ). اگر پیدا نشود null برمی‌گرداند. |
| GetDialCodeByEnglishCountryAsync | string englishCountryName | Task<string?> | کد شماره‌گیری را با نام انگلیسی کشور دریافت می‌کند (غیرحساس به حروف کوچک و بزرگ). اگر پیدا نشود null برمی‌گرداند. |
| GetAllCountriesSortedByDialCodeAsync | ندارد | Task<IReadOnlyList<CountryDialCode>> | تمام کشورها را به ترتیب عددی کد شماره‌گیری مرتب شده برمی‌گرداند. |

### مثال‌های استفاده

```csharp
// مقداردهی اولیه کلاس کمکی
await CountryDialCodeHelper.InitializeAsync();

// دریافت تمام کشورها
var countries = await CountryDialCodeHelper.GetAllCountriesAsync();

// دریافت نام کشور با کد شماره‌گیری
string? persianName = await CountryDialCodeHelper.GetPersianCountryByDialCodeAsync("+98");
string? englishName = await CountryDialCodeHelper.GetEnglishCountryByDialCodeAsync("+1");

// دریافت کد شماره‌گیری با نام کشور
string? iranCode = await CountryDialCodeHelper.GetDialCodeByPersianCountryAsync("ایران");
string? usCode = await CountryDialCodeHelper.GetDialCodeByEnglishCountryAsync("United States");

// دریافت کشورها به ترتیب کد شماره‌گیری
var sortedCountries = await CountryDialCodeHelper.GetAllCountriesSortedByDialCodeAsync();
```

### مدیریت خطاها

- برای کشورها یا کدهای شماره‌گیری موجود نشده مقدار `null` برگردانده می‌شود
- در موارد زیر `InvalidOperationException` پرتاب می‌شود:
  - عدم امکان بارگذاری داده از فایل JSON آنلاین
  - خطا در دی‌سریالایز کردن JSON

### منبع داده

داده‌های کد شماره‌گیری کشورها از این آدرس بارگذاری می‌شوند:
```
https://raw.githubusercontent.com/CsharpGalexy/ExtentionsTools/main/CsharpGalexy.LibraryExtention.Data/Iran/Provinces/country-dial-codes.json
```

### ایمنی در برابر چند نخی

این کلاس thread-safe است و داده‌ها را به صورت کارآمد در فیلدهای استاتیک ذخیره می‌کند.

### حساسیت به حروف کوچک و بزرگ

جستجوی نام کشورها غیرحساس به حروف کوچک و بزرگ است تا استفاده از آن راحت‌تر باشد.

### رفتار مرتب‌سازی

متد `GetAllCountriesSortedByDialCodeAsync`:
- موارد با کد شماره‌گیری خالی یا نامعتبر را فیلتر می‌کند
- بر اساس مقدار عددی بعد از علامت '+' مرتب می‌کند
- فقط کدهای شماره‌گیری که با '+' شروع می‌شوند را شامل می‌شود