# کلاس DefaultableDictionary<TKey, TValue>

این مستندات مربوط به کلاس `DefaultableDictionary<TKey, TValue>` است، یک پوشش دهنده دیکشنری که به جای پرتاب استثنا هنگام پیدا نشدن کلید، مقدار پیش‌فرض را برمی‌گرداند.

## خواص کلاس

| خاصیت | نوع | توضیحات |
|--------|-----|----------|
| Count | int | تعداد عناصر موجود در دیکشنری را برمی‌گرداند |
| IsReadOnly | bool | نشان می‌دهد که آیا دیکشنری فقط-خواندنی است |
| Keys | ICollection<TKey> | مجموعه کلیدهای موجود در دیکشنری را برمی‌گرداند |
| Values | ICollection<TValue> | مجموعه مقادیر را برمی‌گرداند (شامل مقدار پیش‌فرض در انتها) |

## سازنده

| پارامتر | نوع | توضیحات |
|---------|-----|----------|
| dictionary | IDictionary<TKey, TValue> | دیکشنری اصلی که پوشش داده می‌شود |
| defaultValue | TValue | مقداری که در صورت پیدا نشدن کلید برگردانده می‌شود |

## متدها

### ایندکسر

| عملیات | پارامتر | نوع برگشتی | توضیحات |
|---------|----------|-------------|----------|
| get | TKey | TValue | مقدار کلید را برمی‌گرداند، یا مقدار پیش‌فرض اگر کلید پیدا نشود |
| set | TKey, TValue | void | مقدار را برای کلید مشخص شده تنظیم می‌کند |

### عملیات دیکشنری

| متد | پارامترها | نوع برگشتی | توضیحات |
|------|------------|-------------|----------|
| Add | TKey key, TValue value | void | عنصری با کلید و مقدار ارائه شده اضافه می‌کند |
| Add | KeyValuePair<TKey, TValue> item | void | یک جفت کلید-مقدار به دیکشنری اضافه می‌کند |
| Clear | - | void | تمام موارد را از دیکشنری حذف می‌کند |
| Contains | KeyValuePair<TKey, TValue> item | bool | بررسی می‌کند آیا جفت کلید-مقدار خاصی وجود دارد |
| ContainsKey | TKey key | bool | بررسی می‌کند آیا کلید خاصی وجود دارد |
| CopyTo | KeyValuePair<TKey, TValue>[] array, int arrayIndex | void | عناصر را در آرایه کپی می‌کند |
| Remove | TKey key | bool | عنصر با کلید مشخص شده را حذف می‌کند |
| Remove | KeyValuePair<TKey, TValue> item | bool | جفت کلید-مقدار مشخص شده را حذف می‌کند |
| TryGetValue | TKey key, out TValue value | bool | مقدار کلید را می‌گیرد، در صورت عدم وجود مقدار پیش‌فرض برمی‌گرداند |

## مثال‌های استفاده

### استفاده پایه
```csharp
var dict = new Dictionary<string, int>();
var defaultDict = new DefaultableDictionary<string, int>(dict, -1);

// افزودن مقادیر
defaultDict["one"] = 1;
defaultDict["two"] = 2;

// گرفتن مقادیر
int value1 = defaultDict["one"];     // 1 را برمی‌گرداند
int value3 = defaultDict["three"];   // -1 را برمی‌گرداند (مقدار پیش‌فرض)
```

### استفاده از TryGetValue
```csharp
var defaultDict = new DefaultableDictionary<string, string>(
    new Dictionary<string, string>(), 
    "پیدا نشد"
);

defaultDict.TryGetValue("missing", out string result);
// result مقدار "پیدا نشد" خواهد بود به جای پرتاب استثنا
```

### کار با مجموعه‌ها
```csharp
var defaultDict = new DefaultableDictionary<int, string>(
    new Dictionary<int, string>(), 
    "پیش‌فرض"
);

defaultDict[1] = "یک";
defaultDict[2] = "دو";

// مجموعه کلیدها
foreach(var key in defaultDict.Keys)
{
    Console.WriteLine(key);
}

// مجموعه مقادیر (شامل مقدار پیش‌فرض)
foreach(var value in defaultDict.Values)
{
    Console.WriteLine(value);
}
```

## نکات مهم

1. **رفتار مقدار پیش‌فرض**:
   - هرگز KeyNotFoundException پرتاب نمی‌کند
   - همیشه برای کلیدهای موجود نشده مقدار پیش‌فرض را برمی‌گرداند
   - TryGetValue همیشه true برمی‌گرداند
   - مجموعه Values شامل مقدار پیش‌فرض است

2. **جزئیات پیاده‌سازی**:
   - IDictionary<TKey, TValue> را پیاده‌سازی می‌کند
   - یک دیکشنری موجود را پوشش می‌دهد
   - رفتار دیکشنری اصلی را برای کلیدهای موجود حفظ می‌کند

3. **ملاحظات کارایی**:
   - کارایی یکسان با دیکشنری زیرین برای کلیدهای موجود
   - سربار اضافی برای کلیدهای موجود نشده ندارد
   - خاصیت Values در هر بار دسترسی لیست جدید ایجاد می‌کند

## وابستگی‌ها

- فضای نام System.Collections
- فضای نام System.Collections.Generic