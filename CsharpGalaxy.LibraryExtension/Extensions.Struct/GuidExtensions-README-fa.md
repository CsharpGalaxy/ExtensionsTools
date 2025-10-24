# کلاس GuidExtensions

این مستندات شامل کلاس `GuidExtensions` است که متدهای توسعه‌ای برای ساختار `Guid` جهت افزایش قابلیت و کاربردپذیری آن ارائه می‌دهد.

## متدها

### متدهای تبدیل

| متد | پارامترها | نوع خروجی | توضیحات |
|-----|-----------|------------|----------|
| ToShortString | this Guid guid | string | GUID را به یک رشته کوتاه‌تر و URL-friendly با استفاده از کدگذاری Base64 تبدیل می‌کند. |
| ToGuid | this string shortString | Guid | یک رشته Base64 سازگار با URL را به GUID تبدیل می‌کند. |
| ToCleanString | this Guid guid | string | یک رشته هگزادسیمال 32 کاراکتری بدون خط تیره برمی‌گرداند. |

### متدهای اعتبارسنجی

| متد | پارامترها | نوع خروجی | توضیحات |
|-----|-----------|------------|----------|
| IsEmpty | this Guid guid | bool | بررسی می‌کند که آیا GUID خالی است (Guid.Empty). |
| TryParseGuid | this string input, out Guid result | bool | تلاش می‌کند یک رشته را به GUID تبدیل کند. |

### متدهای تولید

| متد | پارامترها | نوع خروجی | توضیحات |
|-----|-----------|------------|----------|
| DeriveGuid | this Guid guid, string salt | Guid | یک GUID قطعی جدید بر اساس GUID والد و رشته salt تولید می‌کند. |

### مدیریت مقادیر پیش‌فرض

| متد | پارامترها | نوع خروجی | توضیحات |
|-----|-----------|------------|----------|
| OrDefault | this Guid guid | Guid | اگر GUID خالی نباشد آن را برمی‌گرداند؛ در غیر این صورت Guid.Empty برمی‌گرداند. |
| OrDefault | this Guid guid, Guid defaultValue | Guid | اگر GUID خالی نباشد آن را برمی‌گرداند؛ در غیر این صورت مقدار پیش‌فرض مشخص شده را برمی‌گرداند. |
| OrDefault | this Guid? guid | Guid | اگر Guid قابل null مقدار داشته باشد و خالی نباشد آن را برمی‌گرداند؛ در غیر این صورت Guid.Empty برمی‌گرداند. |
| OrDefault | this Guid? guid, Guid defaultValue | Guid | اگر Guid قابل null مقدار داشته باشد و خالی نباشد آن را برمی‌گرداند؛ در غیر این صورت مقدار پیش‌فرض مشخص شده را برمی‌گرداند. |

## مثال‌های استفاده

### تبدیل به رشته کوتاه
```csharp
// تبدیل GUID به رشته کوتاه
var guid = Guid.NewGuid();
string shortGuid = guid.ToShortString(); // مثال: "XyZ12-AbC34dEfG56H"

// تبدیل مجدد به GUID
Guid reconstructed = shortGuid.ToGuid();
```

### فرمت رشته تمیز
```csharp
var guid = new Guid("123e4567-e89b-12d3-a456-426614174000");
string clean = guid.ToCleanString(); // "123e4567e89b12d3a456426614174000"
```

### تولید GUID قطعی
```csharp
var parent = Guid.NewGuid();
var child = parent.DeriveGuid("UserProfile");
```

### اعتبارسنجی و تجزیه
```csharp
var guid = Guid.Empty;
bool isEmpty = guid.IsEmpty(); // true

string input = "123e4567-e89b-12d3-a456-426614174000";
if (input.TryParseGuid(out var parsedGuid))
{
    Console.WriteLine($"تجزیه شده: {parsedGuid}");
}
```

### مدیریت مقادیر پیش‌فرض
```csharp
Guid? nullableGuid = null;
Guid safe = nullableGuid.OrDefault(); // Guid.Empty برمی‌گرداند

Guid empty = Guid.Empty;
Guid fallback = Guid.NewGuid();
Guid result = empty.OrDefault(fallback); // fallback را برمی‌گرداند
```

## مدیریت خطا

- `ToGuid`: در صورت null، خالی یا نامعتبر بودن رشته ورودی `ArgumentException` پرتاب می‌کند
- `DeriveGuid`: در صورت null بودن salt، `ArgumentNullException` پرتاب می‌کند
- سایر متدها خطاها را با برگرداندن مقادیر پیش‌فرض یا false به خوبی مدیریت می‌کنند

## ملاحظات کارایی

- اکثر متدها برای عملکرد کارآمد طراحی شده‌اند
- `DeriveGuid` از SHA1 برای تولید قطعی استفاده می‌کند
- عملیات رشته‌ای از متدهای بهینه‌شده .NET Core استفاده می‌کنند