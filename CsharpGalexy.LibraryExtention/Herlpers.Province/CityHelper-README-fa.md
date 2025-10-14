# کلاس CityHelper

## 🏛️ **نمای کلی**
CityHelper یک کلاس استاتیک است که متدهایی برای کار با داده‌های شهرها و استان‌های ایران فراهم می‌کند. این کلاس داده‌ها را از یک فایل JSON بارگذاری کرده و متدهای مختلفی برای جستجو و دریافت اطلاعات شهر و استان ارائه می‌دهد.

## 📋 **متدهای راه‌اندازی**
| متد | توضیحات | پارامترها | نوع خروجی |
|-----|----------|-----------|------------|
| `InitializeAsync` | راه‌اندازی و ذخیره موقت داده‌های شهر | ندارد | `Task` |
| `LoadCitiesFromJsonAsync` | بارگذاری داده‌های شهرها از فایل JSON | ندارد | `Task<List<CityModel>>` |

## 🔍 **متدهای جستجو**
| متد | توضیحات | پارامترها | نوع خروجی |
|-----|----------|-----------|------------|
| `GetByCityIdAsync` | یافتن شهر با شناسه | `string cityId` | `Task<CityModel?>` |
| `GetByCityNameAsync` | یافتن شهر با نام | `string cityName` | `Task<CityModel?>` |
| `GetCitiesByProvinceIdAsync` | دریافت همه شهرهای یک استان با شناسه | `string provinceId` | `Task<IReadOnlyList<CityModel>>` |
| `GetCitiesByProvinceNameAsync` | دریافت همه شهرهای یک استان با نام | `string provinceName` | `Task<IReadOnlyList<CityModel>>` |

## ✅ **متدهای اعتبارسنجی**
| متد | توضیحات | پارامترها | نوع خروجی |
|-----|----------|-----------|------------|
| `ExistsByCityIdAsync` | بررسی وجود شهر با شناسه | `string cityId` | `Task<bool>` |
| `ExistsByCityNameAsync` | بررسی وجود شهر با نام | `string cityName` | `Task<bool>` |

## 📦 **ساختار CityModel**
| خاصیت | نوع | توضیحات |
|--------|-----|----------|
| `CityId` | `string` | شناسه یکتای شهر |
| `CityName` | `string` | نام شهر |
| `ProvinceId` | `string` | شناسه استان مربوطه |
| `ProvinceName` | `string` | نام استان مربوطه |

## 📝 **مثال‌های استفاده**

### راه‌اندازی
```csharp
// راه‌اندازی داده‌های شهر (یکبار در شروع برنامه)
await CityHelper.InitializeAsync();
```

### جستجوی شهرها
```csharp
// یافتن شهر با شناسه
var tehran = await CityHelper.GetByCityIdAsync("021");

// یافتن شهر با نام
var shiraz = await CityHelper.GetByCityNameAsync("شیراز");

// دریافت تمام شهرهای یک استان
var tehranCities = await CityHelper.GetCitiesByProvinceNameAsync("تهران");
```

### اعتبارسنجی
```csharp
// بررسی وجود شهر
bool exists = await CityHelper.ExistsByCityNameAsync("اصفهان");
```

## ⚠️ **نکات مهم**

1. منبع داده:
   - داده‌ها از محتوای خام GitHub بارگذاری می‌شود
   - آدرس: `provinces_cities.json` در مخزن
   - داده‌های JSON پس از اولین بارگذاری ذخیره موقت می‌شوند

2. ملاحظات کارایی:
   - استفاده از الگوی async/await برای تمام عملیات
   - داده‌ها پس از اولین بارگذاری در حافظه ذخیره می‌شوند
   - پیاده‌سازی thread-safe

3. مدیریت خطا:
   - پرتاب `InvalidOperationException` در صورت خطا در بارگذاری JSON
   - برگرداندن کالکشن خالی به جای null برای متدهای لیست
   - مقایسه نام‌ها بدون حساسیت به حروف کوچک و بزرگ

4. بهترین شیوه‌ها:
   - فراخوانی `InitializeAsync` در شروع برنامه
   - مدیریت مشکلات اتصال به شبکه
   - در نظر گرفتن داده‌های پشتیبان محلی
   - استفاده از متدهای مبتنی بر شناسه برای تطبیق دقیق
   - استفاده از متدهای مبتنی بر نام برای ورودی کاربر

## 🔄 **وابستگی‌ها**
- System.Text.Json برای پارس JSON
- HttpClient برای دریافت داده از راه دور
- .NET Standard 2.0 یا بالاتر