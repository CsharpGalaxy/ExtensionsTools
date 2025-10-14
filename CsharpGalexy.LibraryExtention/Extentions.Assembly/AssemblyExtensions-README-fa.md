# کلاس AssemblyExtensions

این مستندات مربوط به کلاس `AssemblyExtensions` است که متدهای توسعه‌ای برای کار با assembly‌ها و انواع با استفاده از reflection را فراهم می‌کند.

## متدهای توسعه‌ای

### متدهای مسیر Assembly

| متد | پارامترها | نوع خروجی | توضیحات |
|-----|-----------|------------|----------|
| GetDirectoryPathX | Assembly assembly | string | مسیر پوشه assembly مشخص شده را برمی‌گرداند |

### متدهای کشف نوع

| متد | پارامترها | نوع خروجی | توضیحات |
|-----|-----------|------------|----------|
| GetAllIEntityTypeConfigurationAssembliesByNamespaceContains | string namespace | List<Type> | انواعی با متد "Configure" در فضای نام مشخص شده را می‌یابد |
| GetAllAssembliesByInterface<T> | - | List<Type> | انواعی که رابط T را پیاده‌سازی می‌کنند می‌یابد |
| GetAllIEntityTypeConfigurationAssembliesInterface<T> | - | List<Type> | متد جایگزین برای یافتن انواعی که رابط T را پیاده‌سازی می‌کنند |
| GetTypeOf<T> | string name | Type | نوع کلاس را با نام در assembly در حال اجرا می‌یابد |
| GetClassOfType<T> | - | IEnumerable<Type> | همه کلاس‌های قابل انتساب از نوع T را می‌یابد |
| GetTypesAssignableFrom<T> | Assembly assembly | List<Type> | انواع قابل انتساب از T در assembly مشخص شده را می‌یابد |
| GetTypesAssignableFrom | Assembly assembly, Type compareType | List<Type> | انواع قابل انتساب از نوع مشخص شده را می‌یابد |

### متدهای دسترسی به خصوصیت

| متد | پارامترها | نوع خروجی | توضیحات |
|-----|-----------|------------|----------|
| GetPropertyValue<T> | object obj, string propName | T | مقدار خصوصیت را با نام و تبدیل نوع می‌گیرد |

## مثال‌های استفاده

### کار با مسیرهای Assembly
```csharp
var assembly = Assembly.GetExecutingAssembly();
string path = assembly.GetDirectoryPathX();
// مسیر پوشه حاوی assembly را برمی‌گرداند
```

### یافتن انواع با رابط
```csharp
// یافتن تمام کلاس‌های غیر انتزاعی که IMyInterface را پیاده‌سازی می‌کنند
var types = AssemblyExtensions.GetAllAssembliesByInterface<IMyInterface>();

// یافتن انواع در فضای نام خاص با متد Configure
var configTypes = AssemblyExtensions.GetAllIEntityTypeConfigurationAssembliesByNamespaceContains("MyNamespace");
```

### کشف نوع
```csharp
// یافتن نوع کلاس خاص
Type type = AssemblyExtensions.GetTypeOf<BaseClass>("TargetClassName");

// گرفتن تمام کلاس‌های مشتق شده از BaseClass
var derivedClasses = AssemblyExtensions.GetClassOfType<BaseClass>();

// یافتن انواع قابل انتساب در assembly خاص
Assembly assembly = Assembly.GetExecutingAssembly();
var assignableTypes = assembly.GetTypesAssignableFrom<BaseClass>();
```

### دسترسی به خصوصیت
```csharp
var obj = new MyClass();
string value = obj.GetPropertyValue<string>("PropertyName");
```

## نکات مهم

1. **ایمنی نوع**:
   - متدهای جنریک ایمنی نوع را در زمان کامپایل تضمین می‌کنند
   - برای درخواست‌های نوع نامعتبر استثنا پرتاب می‌کند
   - موارد null را به درستی مدیریت می‌کند

2. **ملاحظات کارایی**:
   - از reflection استفاده می‌کند که می‌تواند بر کارایی تأثیر بگذارد
   - Assembly.GetExecutingAssembly() را در صورت امکان ذخیره می‌کند
   - پرس‌وجوهای LINQ کارآمد برای فیلتر کردن نوع

3. **زمینه استفاده**:
   - بهترین برای راه‌اندازی/پیکربندی برنامه
   - مفید برای معماری‌های پلاگین
   - مفید برای بارگذاری پویای نوع

## موارد استفاده رایج

1. **پیکربندی Entity Framework**:
   - یافتن کلاس‌های پیکربندی
   - ثبت خودکار پیکربندی‌های موجودیت
   - ساخت پویای مدل

2. **سیستم‌های پلاگین**:
   - کشف پیاده‌سازی‌های پلاگین
   - بارگذاری پویای انواع
   - یافتن انواع سازگار

3. **ابزارهای Reflection**:
   - دسترسی به خصوصیت با نام
   - کشف نوع
   - اطلاعات Assembly

## وابستگی‌ها

- فضای نام System
- فضای نام System.Collections.Generic
- فضای نام System.IO
- فضای نام System.Linq
- فضای نام System.Reflection