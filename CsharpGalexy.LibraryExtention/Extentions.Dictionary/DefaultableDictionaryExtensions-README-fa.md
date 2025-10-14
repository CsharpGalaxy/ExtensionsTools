# کلاس DefaultableDictionaryExtensions

این مستندات مربوط به کلاس `DefaultableDictionaryExtensions` است که متدهای توسعه‌ای برای ایجاد دیکشنری‌های پیش‌فرض‌پذیر را فراهم می‌کند.

## متدهای توسعه‌ای

### WithDefaultValue<TValue, TKey>

| پارامتر | نوع | توضیحات |
|---------|-----|----------|
| this dictionary | IDictionary<TKey, TValue> | دیکشنری که باید گسترش یابد |
| defaultValue | TValue | مقدار پیش‌فرضی که برای کلیدهای موجود نشده برگردانده می‌شود |
| خروجی | IDictionary<TKey, TValue> | یک DefaultableDictionary جدید که دیکشنری اصلی را پوشش می‌دهد |

## مثال‌های استفاده

### استفاده پایه
```csharp
var dictionary = new Dictionary<string, int>();
var defaultableDictionary = dictionary.WithDefaultValue(-1);

// حالا دسترسی به کلیدهای موجود نشده به جای پرتاب خطا، -1 برمی‌گرداند
int value = defaultableDictionary["nonexistent"]; // -1 را برمی‌گرداند
```

### با انواع مختلف
```csharp
// با مقادیر رشته‌ای
var stringDict = new Dictionary<int, string>();
var defaultStringDict = stringDict.WithDefaultValue("پیدا نشد");

// با اشیاء سفارشی
var userDict = new Dictionary<string, User>();
var defaultUserDict = userDict.WithDefaultValue(User.Guest);

// با انواع null‌پذیر
var nullableDict = new Dictionary<string, int?>();
var defaultNullDict = nullableDict.WithDefaultValue(null);
```

### زنجیره‌سازی با عملیات LINQ
```csharp
var result = dictionary
    .WithDefaultValue(0)
    .Where(kvp => kvp.Value > 10)
    .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
```

## نکات مهم

1. **زمینه استفاده**:
   - متد توسعه‌ای برای هر IDictionary<TKey, TValue>
   - یک نمونه پوشش‌دهنده جدید ایجاد می‌کند
   - دیکشنری اصلی بدون تغییر باقی می‌ماند

2. **سازگاری نوع**:
   - با هر نوع مقدار یا نوع مرجع کار می‌کند
   - مقدار پیش‌فرض باید با نوع مقدار دیکشنری مطابقت داشته باشد
   - محدودیت‌های نوع جنریک را حفظ می‌کند

3. **ملاحظات کارایی**:
   - سربار حداقلی برای ایجاد
   - ویژگی‌های کارایی مشابه با DefaultableDictionary
   - بدون تخصیص حافظه اضافی برای ورودی‌های موجود

## وابستگی‌ها

- کلاس DefaultableDictionary<TKey, TValue>
- فضای نام System.Collections.Generic