# کلاس‌های CurrencyHelper و CurrencyInfo

این مستندات شامل کلاس‌های `CurrencyHelper` و `CurrencyInfo` است که قابلیت‌هایی برای دریافت و مدیریت اطلاعات واحد پول کشورهای مختلف را فراهم می‌کنند.

## کلاس CurrencyInfo

یک کلاس مدل داده که اطلاعات واحد پول را نمایش می‌دهد.

| پراپرتی | نوع | توضیحات |
|---------|-----|----------|
| CountryName | string | نام کشور |
| CurrencyCode | string | کد واحد پول (مانند USD) |
| CurrencyName | string | نام واحد پول (مانند دلار آمریکا) |
| Symbol | string | نماد واحد پول (مانند $) |

## کلاس CurrencyHelper

یک کلاس کمکی استاتیک که متدهایی برای کار با اطلاعات واحد پول بارگذاری شده از یک منبع JSON آنلاین ارائه می‌دهد.

### متدها

| متد | پارامترها | نوع خروجی | توضیحات |
|-----|-----------|------------|----------|
| InitializeAsync | ندارد | Task | داده‌های واحد پول را مقداردهی اولیه و ذخیره می‌کند. داده‌ها فقط یک بار بارگذاری شده و برای درخواست‌های بعدی مجدداً استفاده می‌شوند. |
| LoadFromJsonAsync | ندارد | Task<List<CurrencyInfo>> | داده‌های واحد پول را از یک فایل JSON آنلاین بارگذاری می‌کند. لیستی از اطلاعات واحد پول را برمی‌گرداند. |
| GetAllCurrenciesAsync | ندارد | Task<IReadOnlyList<CurrencyInfo>> | لیست فقط-خواندنی از تمام اطلاعات واحد پول را برمی‌گرداند. |
| GetCurrencyCodeByCountryAsync | string countryName | Task<string?> | کد واحد پول یک کشور را دریافت می‌کند (مثلاً "USD" برای "ایالات متحده"). اگر پیدا نشود null برمی‌گرداند. |
| GetCurrencyNameByCountryAsync | string countryName | Task<string?> | نام واحد پول یک کشور را دریافت می‌کند (مثلاً "دلار آمریکا" برای "ایالات متحده"). اگر پیدا نشود null برمی‌گرداند. |
| GetCurrencySymbolByCountryAsync | string countryName | Task<string?> | نماد واحد پول یک کشور را دریافت می‌کند (مثلاً "$" برای "ایالات متحده"). اگر پیدا نشود null برمی‌گرداند. |
| GetCountryByCurrencyCodeAsync | string currencyCode | Task<string?> | اولین کشوری که از کد واحد پول مشخص شده استفاده می‌کند را دریافت می‌کند. اگر پیدا نشود null برمی‌گرداند. |
| GetCountriesByCurrencyCodeAsync | string currencyCode | Task<IReadOnlyList<string>> | تمام کشورهایی که از کد واحد پول مشخص شده استفاده می‌کنند را دریافت می‌کند. اگر هیچ کشوری پیدا نشود لیست خالی برمی‌گرداند. |

### مثال‌های استفاده

```csharp
// مقداردهی اولیه کلاس کمکی
await CurrencyHelper.InitializeAsync();

// دریافت تمام واحدهای پول
var currencies = await CurrencyHelper.GetAllCurrenciesAsync();

// دریافت اطلاعات واحد پول برای یک کشور
string? usdCode = await CurrencyHelper.GetCurrencyCodeByCountryAsync("ایالات متحده");
string? usdName = await CurrencyHelper.GetCurrencyNameByCountryAsync("ایالات متحده");
string? usdSymbol = await CurrencyHelper.GetCurrencySymbolByCountryAsync("ایالات متحده");

// یافتن کشورهای استفاده کننده از یک واحد پول
string? country = await CurrencyHelper.GetCountryByCurrencyCodeAsync("USD");
var countries = await CurrencyHelper.GetCountriesByCurrencyCodeAsync("EUR");
```

### مدیریت خطا

- برای موارد تکی پیدا نشده `null` برمی‌گرداند
- برای موارد چندتایی پیدا نشده مجموعه‌های خالی برمی‌گرداند
- در موارد زیر `InvalidOperationException` پرتاب می‌شود:
  - عدم امکان بارگذاری داده از فایل JSON آنلاین
  - خطا در دی‌سریالایز کردن JSON

### منبع داده

داده‌های واحد پول از این آدرس بارگذاری می‌شوند:
```
https://raw.githubusercontent.com/CsharpGalexy/ExtentionsTools/main/CsharpGalexy.LibraryExtention.Data/Iran/Provinces/currency-codes.json
```

### ایمنی در برابر چند نخی

این کلاس thread-safe است و داده‌ها را به صورت کارآمد در فیلدهای استاتیک ذخیره می‌کند.

### حساسیت به حروف کوچک و بزرگ

نام کشورها و کدهای واحد پول غیرحساس به حروف کوچک و بزرگ هستند تا استفاده از آنها راحت‌تر باشد.