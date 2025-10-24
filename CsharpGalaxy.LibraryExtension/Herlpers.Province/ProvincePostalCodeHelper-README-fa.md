# کلاس‌های ProvincePostalCode و ProvincePostalCodeHelper

این مستندات شامل کلاس‌های `ProvincePostalCode` و `ProvincePostalCodeHelper` است که قابلیت‌هایی برای دریافت و مدیریت کدهای پستی استان‌های ایران را فراهم می‌کنند.

## کلاس ProvincePostalCode

یک کلاس مدل داده که اطلاعات کد پستی استان را نمایش می‌دهد.

| پراپرتی | نوع | توضیحات |
|---------|-----|----------|
| ProvinceName | string | نام استان |
| PostalCode | string | کد پستی استان |

## کلاس ProvincePostalCodeHelper

یک کلاس کمکی استاتیک که متدهایی برای کار با کدهای پستی استان‌های ایران ارائه می‌دهد.

### متدها

| متد | پارامترها | نوع خروجی | توضیحات |
|-----|-----------|------------|----------|
| InitializeAsync | ندارد | Task | داده‌های کد پستی استان‌ها را مقداردهی اولیه و ذخیره می‌کند. داده‌ها فقط یک بار بارگذاری شده و برای درخواست‌های بعدی مجدداً استفاده می‌شوند. |
| LoadFromJsonAsync | ندارد | Task<Dictionary<string, string>> | کدهای پستی استان‌ها را از یک فایل JSON آنلاین بارگذاری می‌کند. یک دیکشنری با کلیدهای نام استان و مقادیر کد پستی برمی‌گرداند. |
| GetPostalCodeDictionaryAsync | ندارد | Task<Dictionary<string, string>> | دیکشنری ذخیره شده کدهای پستی استان را برمی‌گرداند. اگر داده‌ها قبلاً بارگذاری نشده باشند، آنها را مقداردهی اولیه می‌کند. |
| GetPostalCodeAsync | string provinceName | Task<string?> | کد پستی یک استان خاص را با نام آن دریافت می‌کند. اگر استان پیدا نشود null برمی‌گرداند. |
| IsSupportedProvinceAsync | string provinceName | Task<bool> | بررسی می‌کند که آیا یک استان در لیست کدهای پستی پشتیبانی می‌شود یا خیر. |
| GetAllProvinceNamesAsync | ندارد | Task<string[]> | آرایه‌ای از نام تمام استان‌های پشتیبانی شده را برمی‌گرداند. |
| GetAllPostalCodesAsync | ندارد | Task<IReadOnlyList<(string ProvinceName, string PostalCode)>> | تمام کدهای پستی استان‌ها را به صورت یک لیست فقط خواندنی از تاپل‌ها برمی‌گرداند. |

### مثال‌های استفاده

```csharp
// مقداردهی اولیه کلاس کمکی
await ProvincePostalCodeHelper.InitializeAsync();

// دریافت کد پستی برای یک استان خاص
string? tehranCode = await ProvincePostalCodeHelper.GetPostalCodeAsync("تهران");

// بررسی پشتیبانی از یک استان
bool isSupported = await ProvincePostalCodeHelper.IsSupportedProvinceAsync("اصفهان");

// دریافت نام تمام استان‌ها
string[] provinces = await ProvincePostalCodeHelper.GetAllProvinceNamesAsync();

// دریافت تمام کدهای پستی
var allCodes = await ProvincePostalCodeHelper.GetAllPostalCodesAsync();
```

### مدیریت خطاها

- در صورت خالی یا null بودن نام استان، `ArgumentException` پرتاب می‌شود
- در موارد زیر `InvalidOperationException` پرتاب می‌شود:
  - خطا در دی‌سریالایز کردن JSON
  - عدم امکان بارگذاری داده از فایل JSON آنلاین

### منبع داده

داده‌های کد پستی از این آدرس بارگذاری می‌شوند:
```
https://raw.githubusercontent.com/CsharpGalexy/ExtensionsTools/main/CsharpGalexy.LibraryExtention.Data/Iran/Provinces/province-postal-codes.json
```

### ایمنی در برابر چند نخی

این کلاس thread-safe است و داده‌ها را به صورت کارآمد در فیلدهای استاتیک ذخیره می‌کند.