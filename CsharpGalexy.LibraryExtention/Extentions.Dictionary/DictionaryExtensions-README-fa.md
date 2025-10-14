# کلاس DictionaryExtensions

این مستندات مربوط به کلاس `DictionaryExtensions` است که متدهای توسعه‌ای مفید برای کار با دیکشنری‌ها را فراهم می‌کند.

## متدهای توسعه‌ای

### GetKeyFromValue<TKey, TValue>

| پارامتر | نوع | توضیحات |
|---------|-----|----------|
| this dictionary | IDictionary<TKey, TValue> | دیکشنری برای جستجو |
| value | TValue | مقداری که باید پیدا شود |
| خروجی | TKey | کلید مطابق در صورت پیدا شدن؛ در غیر این صورت مقدار پیش‌فرض TKey |

### GetValueFromKey<TKey, TValue>

| پارامتر | نوع | توضیحات |
|---------|-----|----------|
| this dictionary | IDictionary<TKey, TValue> | دیکشنری برای جستجو |
| key | TKey | کلیدی که باید پیدا شود |
| خروجی | TValue | مقدار مطابق در صورت پیدا شدن؛ در غیر این صورت مقدار پیش‌فرض TValue |

### CheckDictionaryIsNull<TKey, TValue>

| پارامتر | نوع | توضیحات |
|---------|-----|----------|
| this dictionary | IDictionary<TKey, TValue> | دیکشنری برای بررسی |
| خروجی | bool | true اگر دیکشنری null باشد؛ در غیر این صورت false |

### AddIfNotExists<TKey, TValue>

| پارامتر | نوع | توضیحات |
|---------|-----|----------|
| this dictionary | Dictionary<TKey, TValue> | دیکشنری برای تغییر |
| key | TKey | کلید برای اضافه کردن |
| value | TValue | مقدار برای اضافه کردن |
| خروجی | bool | true اگر کلید اضافه شد؛ false اگر کلید وجود دارد یا دیکشنری null است |

### DeleteIfExistsKey<TKey, TValue>

| پارامتر | نوع | توضیحات |
|---------|-----|----------|
| this dictionary | Dictionary<TKey, TValue> | دیکشنری برای تغییر |
| key | TKey | کلید برای حذف |
| خروجی | bool | true اگر کلید پیدا و حذف شد؛ در غیر این صورت false |

### Update<TKey, TValue>

| پارامتر | نوع | توضیحات |
|---------|-----|----------|
| this dictionary | Dictionary<TKey, TValue> | دیکشنری برای تغییر |
| key | TKey | کلید برای به‌روزرسانی |
| value | TValue | مقدار جدید برای تنظیم |
| خروجی | bool | true اگر مقدار به‌روزرسانی شد؛ در غیر این صورت false |

## مثال‌های استفاده

### یافتن کلیدها و مقادیر
```csharp
var dict = new Dictionary<string, int> { {"one", 1}, {"two", 2} };

// یافتن کلید با مقدار
string key = dict.GetKeyFromValue(2); // "two" را برمی‌گرداند

// یافتن مقدار با کلید
int value = dict.GetValueFromKey("one"); // 1 را برمی‌گرداند
```

### افزودن و به‌روزرسانی ورودی‌ها
```csharp
var dict = new Dictionary<string, int>();

// افزودن ورودی جدید اگر کلید وجود نداشته باشد
bool added = dict.AddIfNotExists("three", 3); // true برمی‌گرداند

// به‌روزرسانی ورودی موجود
bool updated = dict.Update("three", 30); // true برمی‌گرداند
```

### حذف ورودی‌ها
```csharp
var dict = new Dictionary<string, int> { {"temp", 0} };

// حذف ورودی اگر کلید وجود داشته باشد
bool removed = dict.DeleteIfExistsKey("temp"); // true برمی‌گرداند
```

### بررسی null
```csharp
Dictionary<string, int> dict = null;

// بررسی اینکه آیا دیکشنری null است
bool isNull = dict.CheckDictionaryIsNull(); // true برمی‌گرداند
```

## نکات مهم

1. **مدیریت null**:
   - متدها شامل بررسی null برای نمونه‌های دیکشنری هستند
   - متد Update کلیدها و مقادیر null را بررسی می‌کند
   - مدیریت ایمن دیکشنری‌های null

2. **مقادیر برگشتی**:
   - GetKeyFromValue در صورت پیدا نشدن مقدار، default(TKey) را برمی‌گرداند
   - GetValueFromKey در صورت پیدا نشدن کلید، default(TValue) را برمی‌گرداند
   - متدهای بولین برای عملیات نامعتبر false برمی‌گردانند

3. **ملاحظات کارایی**:
   - GetKeyFromValue و GetValueFromKey از LINQ FirstOrDefault استفاده می‌کنند
   - سایر متدها از عملیات مستقیم دیکشنری استفاده می‌کنند
   - بررسی‌های null سربار حداقلی اضافه می‌کنند

## وابستگی‌ها

- فضای نام System.Collections.Generic
- فضای نام System.Linq