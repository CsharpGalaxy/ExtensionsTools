# مستندات کلاس‌های ProvinceCapital و ProvinceCapitalHelper

## 📦 **کلاس ProvinceCapital**
| خاصیت | نوع | توضیحات |
|--------|-----|----------|
| `ProvinceId` | `string` | شناسه یکتای استان |
| `ProvinceName` | `string` | نام استان |
| `Capital` | `string` | نام شهر مرکز استان |

## 📋 **متدهای راه‌اندازی**
| متد | توضیحات | پارامترها | نوع خروجی |
|-----|----------|-----------|------------|
| `InitializeAsync` | راه‌اندازی و ذخیره موقت داده‌های استان | ندارد | `Task` |
| `LoadFromJsonAsync` | بارگذاری داده‌های استان‌ها از فایل JSON | ندارد | `Task<List<ProvinceCapital>>` |

## 🔍 **متدهای جستجو**
| متد | توضیحات | پارامترها | نوع خروجی |
|-----|----------|-----------|------------|
| `GetAllProvincesAsync` | دریافت تمام استان‌ها و مراکز آن‌ها | ندارد | `Task<IReadOnlyList<ProvinceCapital>>` |
| `GetCapitalByProvinceIdAsync` | دریافت مرکز استان با شناسه استان | `string provinceId` | `Task<string?>` |
| `GetCapitalByProvinceNameAsync` | دریافت مرکز استان با نام استان | `string provinceName` | `Task<string?>` |
| `GetProvinceNameByCapitalAsync` | دریافت نام استان با نام مرکز استان | `string capitalName` | `Task<string?>` |

## ✅ **متدهای اعتبارسنجی**
| متد | توضیحات | پارامترها | نوع خروجی |
|-----|----------|-----------|------------|
| `ExistsByProvinceIdAsync` | بررسی وجود استان با شناسه | `string provinceId` | `Task<bool>` |
| `ExistsByProvinceNameAsync` | بررسی وجود استان با نام | `string provinceName` | `Task<bool>` |

## 📝 **مثال‌های استفاده**

### راه‌اندازی
```csharp
// راه‌اندازی داده‌های استان (یکبار در شروع برنامه)
await ProvinceCapitalHelper.InitializeAsync();
```

### جستجوی داده‌های استان
```csharp
// دریافت تمام استان‌ها
var allProvinces = await ProvinceCapitalHelper.GetAllProvincesAsync();

// یافتن مرکز استان با نام استان
var tehranCapital = await ProvinceCapitalHelper.GetCapitalByProvinceNameAsync("تهران");
Console.WriteLine(tehranCapital); // خروجی: "تهران"

// یافتن استان با نام مرکز استان
var provinceName = await ProvinceCapitalHelper.GetProvinceNameByCapitalAsync("شیراز");
Console.WriteLine(provinceName); // خروجی: "فارس"

// بررسی وجود استان
bool exists = await ProvinceCapitalHelper.ExistsByProvinceNameAsync("اصفهان");
```

## ⚠️ **نکات مهم**

1. منبع داده:
   - داده‌ها از محتوای خام GitHub بارگذاری می‌شود
   - آدرس: `province-capitals.json` در مخزن
   - داده‌های JSON پس از اولین بارگذاری ذخیره موقت می‌شوند

2. ملاحظات کارایی:
   - استفاده از الگوی async/await برای تمام عملیات
   - داده‌ها پس از اولین بارگذاری در حافظه ذخیره می‌شوند
   - پیاده‌سازی thread-safe
   - مقایسه نام‌ها بدون حساسیت به حروف کوچک و بزرگ

3. مدیریت خطا:
   - پرتاب `InvalidOperationException` در صورت خطا در بارگذاری JSON
   - برگرداندن null برای استان‌ها/مراکز موجود نبوده
   - استفاده از رشته خالی به عنوان مقادیر پیش‌فرض در مدل

4. بهترین شیوه‌ها:
   - فراخوانی `InitializeAsync` در شروع برنامه
   - مدیریت مشکلات اتصال به شبکه
   - استفاده از string.Empty به جای null برای خواص مدل
   - پیاده‌سازی مدیریت خطای مناسب برای عملیات ناهمزمان

## 🔄 **وابستگی‌ها**
- System.Text.Json برای پارس JSON
- HttpClient برای دریافت داده از راه دور
- .NET Standard 2.0 یا بالاتر

## 🌐 **نمونه فرمت داده**
```json
[
  {
    "provinceId": "021",
    "provinceName": "تهران",
    "capital": "تهران"
  },
  {
    "provinceId": "031",
    "provinceName": "اصفهان",
    "capital": "اصفهان"
  }
]
```