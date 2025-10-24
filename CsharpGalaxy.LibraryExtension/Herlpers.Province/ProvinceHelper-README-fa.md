# کلاس‌های ProvinceHelper و مدل‌های مرتبط

این مستندات شامل کلاس `ProvinceHelper` و مدل‌های مرتبط با آن است که قابلیت‌هایی برای مدیریت و دریافت اطلاعات استان‌های ایران را فراهم می‌کنند.

## مدل‌ها

### کلاس ProvinceModel

یک کلاس مدل داده که اطلاعات استان را نمایش می‌دهد.

| پراپرتی | نوع | توضیحات |
|---------|-----|----------|
| ProvinceId | string | شناسه یکتای استان |
| ProvinceName | string | نام استان |

### کلاس CityModel

یک کلاس مدل داده که اطلاعات شهر را نمایش می‌دهد.

| پراپرتی | نوع | توضیحات |
|---------|-----|----------|
| CityId | string | شناسه یکتای شهر |
| ProvinceId | string | ارجاع به شناسه استان |
| ProvinceName | string | نام استان |
| CityName | string | نام شهر |

## کلاس ProvinceHelper

یک کلاس کمکی استاتیک که متدهایی برای کار با داده‌های استان‌های ایران ارائه می‌دهد.

### متدها

| متد | پارامترها | نوع خروجی | توضیحات |
|-----|-----------|------------|----------|
| InitializeAsync | ندارد | Task | داده‌های استان‌ها را مقداردهی اولیه و ذخیره می‌کند. داده‌ها فقط یک بار بارگذاری شده و برای درخواست‌های بعدی مجدداً استفاده می‌شوند. |
| LoadProvincesFromJsonAsync | ندارد | Task<List<ProvinceModel>> | استان‌ها را از یک فایل JSON آنلاین بارگذاری می‌کند. یک لیست از مدل‌های استان برمی‌گرداند. |
| GetAllProvincesAsync | ندارد | Task<IReadOnlyList<ProvinceModel>> | لیست فقط-خواندنی از تمام استان‌ها را برمی‌گرداند. اگر داده‌ها قبلاً بارگذاری نشده باشند، آنها را مقداردهی اولیه می‌کند. |
| GetByProvinceIdAsync | string provinceId | Task<ProvinceModel?> | استان را با شناسه آن دریافت می‌کند. اگر پیدا نشود null برمی‌گرداند. |
| GetByProvinceNameAsync | string provinceName | Task<ProvinceModel?> | استان را با نام آن دریافت می‌کند (غیرحساس به حروف کوچک و بزرگ). اگر پیدا نشود null برمی‌گرداند. |
| ExistsByProvinceIdAsync | string provinceId | Task<bool> | بررسی می‌کند که آیا استانی با شناسه مشخص وجود دارد. |
| ExistsByProvinceNameAsync | string provinceName | Task<bool> | بررسی می‌کند که آیا استانی با نام مشخص وجود دارد (غیرحساس به حروف کوچک و بزرگ). |

### مثال‌های استفاده

```csharp
// مقداردهی اولیه کلاس کمکی
await ProvinceHelper.InitializeAsync();

// دریافت تمام استان‌ها
var provinces = await ProvinceHelper.GetAllProvincesAsync();

// یافتن استان با شناسه
var province = await ProvinceHelper.GetByProvinceIdAsync("08");

// یافتن استان با نام
var tehran = await ProvinceHelper.GetByProvinceNameAsync("تهران");

// بررسی وجود استان
bool exists = await ProvinceHelper.ExistsByProvinceNameAsync("اصفهان");
```

### مدیریت خطاها

- برای استان‌های موجود نشده در متدهای Get مقدار `null` برگردانده می‌شود
- برای استان‌های موجود نشده در متدهای Exists مقدار `false` برگردانده می‌شود
- در موارد زیر `InvalidOperationException` پرتاب می‌شود:
  - عدم امکان بارگذاری داده از فایل JSON آنلاین
  - خطا در دی‌سریالایز کردن JSON

### منبع داده

داده‌های استان‌ها از این آدرس بارگذاری می‌شوند:
```
https://raw.githubusercontent.com/CsharpGalexy/ExtensionsTools/main/CsharpGalexy.LibraryExtention.Data/Iran/Provinces/provinces.json
```

### ایمنی در برابر چند نخی

این کلاس thread-safe است و داده‌ها را به صورت کارآمد در فیلدهای استاتیک ذخیره می‌کند.

### حساسیت به حروف کوچک و بزرگ

جستجوی نام استان‌ها غیرحساس به حروف کوچک و بزرگ است تا استفاده از آن راحت‌تر باشد.