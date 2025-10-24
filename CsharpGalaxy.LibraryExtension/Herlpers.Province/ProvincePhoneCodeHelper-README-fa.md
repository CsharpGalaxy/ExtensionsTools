# کلاس‌های ProvincePhoneCode و ProvincePhoneCodeHelper

این مستندات شامل کلاس‌های `ProvincePhoneCode` و `ProvincePhoneCodeHelper` است که قابلیت‌هایی برای دریافت و مدیریت پیش‌شماره‌های تلفن استان‌های ایران را فراهم می‌کنند.

## کلاس ProvincePhoneCode

یک کلاس مدل داده که اطلاعات پیش‌شماره تلفن استان را نمایش می‌دهد.

| پراپرتی | نوع | توضیحات |
|---------|-----|----------|
| ProvinceName | string | نام استان |
| PhoneCode | string | پیش‌شماره تلفن استان |

## کلاس ProvincePhoneCodeHelper

یک کلاس کمکی استاتیک که متدهایی برای کار با پیش‌شماره‌های تلفن استان‌های ایران ارائه می‌دهد.

### متدها

| متد | پارامترها | نوع خروجی | توضیحات |
|-----|-----------|------------|----------|
| InitializeAsync | ندارد | Task | داده‌های پیش‌شماره تلفن استان‌ها را مقداردهی اولیه و ذخیره می‌کند. داده‌ها فقط یک بار بارگذاری شده و برای درخواست‌های بعدی مجدداً استفاده می‌شوند. |
| LoadFromJsonAsync | ندارد | Task<Dictionary<string, string>> | پیش‌شماره‌های تلفن استان‌ها را از یک فایل JSON آنلاین بارگذاری می‌کند. یک دیکشنری با کلیدهای نام استان و مقادیر پیش‌شماره برمی‌گرداند. |
| GetPhoneCodeDictionaryAsync | ندارد | Task<Dictionary<string, string>> | دیکشنری ذخیره شده پیش‌شماره‌های تلفن استان را برمی‌گرداند. اگر داده‌ها قبلاً بارگذاری نشده باشند، آنها را مقداردهی اولیه می‌کند. |
| GetPhoneCodeAsync | string provinceName | Task<string?> | پیش‌شماره تلفن یک استان خاص را با نام آن دریافت می‌کند. اگر استان پیدا نشود null برمی‌گرداند. |
| IsSupportedProvinceAsync | string provinceName | Task<bool> | بررسی می‌کند که آیا یک استان در لیست پیش‌شماره‌های تلفن پشتیبانی می‌شود یا خیر. |
| GetAllProvinceNamesAsync | ندارد | Task<string[]> | آرایه‌ای از نام تمام استان‌های پشتیبانی شده را برمی‌گرداند. |
| GetAllPhoneCodesAsync | ندارد | Task<IReadOnlyList<(string ProvinceName, string PhoneCode)>> | تمام پیش‌شماره‌های تلفن استان‌ها را به صورت یک لیست فقط خواندنی از تاپل‌ها برمی‌گرداند. |

### مثال‌های استفاده

```csharp
// مقداردهی اولیه کلاس کمکی
await ProvincePhoneCodeHelper.InitializeAsync();

// دریافت پیش‌شماره برای یک استان خاص
string? tehranCode = await ProvincePhoneCodeHelper.GetPhoneCodeAsync("تهران");

// بررسی پشتیبانی از یک استان
bool isSupported = await ProvincePhoneCodeHelper.IsSupportedProvinceAsync("اصفهان");

// دریافت نام تمام استان‌ها
string[] provinces = await ProvincePhoneCodeHelper.GetAllProvinceNamesAsync();

// دریافت تمام پیش‌شماره‌ها
var allCodes = await ProvincePhoneCodeHelper.GetAllPhoneCodesAsync();
```

### مدیریت خطاها

- در صورت خالی یا null بودن نام استان، `ArgumentException` پرتاب می‌شود
- در موارد زیر `InvalidOperationException` پرتاب می‌شود:
  - خطا در دی‌سریالایز کردن JSON
  - عدم امکان بارگذاری داده از فایل JSON آنلاین

### منبع داده

داده‌های پیش‌شماره از این آدرس بارگذاری می‌شوند:
```
https://raw.githubusercontent.com/CsharpGalexy/ExtensionsTools/main/CsharpGalexy.LibraryExtention.Data/Iran/Provinces/province-phone-codes.json
```

### ایمنی در برابر چند نخی

این کلاس thread-safe است و داده‌ها را به صورت کارآمد در فیلدهای استاتیک ذخیره می‌کند.