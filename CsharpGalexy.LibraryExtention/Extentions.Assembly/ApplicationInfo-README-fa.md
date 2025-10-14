# کلاس ApplicationInfo 

این مستندات مربوط به کلاس `ApplicationInfo` است که خواص استاتیک برای دسترسی به اطلاعات assembly مانند نسخه، عنوان و سایر متادیتا را فراهم می‌کند.

## خواص

| خاصیت | نوع | توضیحات |
|--------|-----|----------|
| Version | Version | نسخه assembly فراخواننده را دریافت می‌کند |
| Title | string | عنوان را از AssemblyTitleAttribute یا نام اجرایی می‌گیرد |
| ProductName | string | نام محصول را از AssemblyProductAttribute می‌گیرد |
| Description | string | توضیحات را از AssemblyDescriptionAttribute می‌گیرد |
| CopyrightHolder | string | اطلاعات کپی‌رایت را از AssemblyCopyrightAttribute می‌گیرد |
| CompanyName | string | نام شرکت را از AssemblyCompanyAttribute می‌گیرد |

## جزئیات خواص

### Version
- شماره نسخه را از assembly فراخواننده برمی‌گرداند
- از Assembly.GetCallingAssembly().GetName().Version استفاده می‌کند
- هرگز null برنمی‌گرداند (نسخه assembly الزامی است)

### Title
- منبع اصلی: AssemblyTitleAttribute
- جایگزین: نام فایل اجرایی بدون پسوند
- هرگز null یا رشته خالی برنمی‌گرداند

### ProductName
- منبع: AssemblyProductAttribute
- اگر صفت تعریف نشده باشد، رشته خالی برمی‌گرداند
- مقدار جایگزین ندارد

### Description
- منبع: AssemblyDescriptionAttribute
- اگر صفت تعریف نشده باشد، رشته خالی برمی‌گرداند
- مقدار جایگزین ندارد

### CopyrightHolder
- منبع: AssemblyCopyrightAttribute
- اگر صفت تعریف نشده باشد، رشته خالی برمی‌گرداند
- مقدار جایگزین ندارد

### CompanyName
- منبع: AssemblyCompanyAttribute
- اگر صفت تعریف نشده باشد، رشته خالی برمی‌گرداند
- مقدار جایگزین ندارد

## مثال‌های استفاده

### استفاده پایه
```csharp
// دریافت نسخه assembly
Version version = ApplicationInfo.Version;
Console.WriteLine($"نسخه: {version}");

// دریافت عنوان برنامه
string title = ApplicationInfo.Title;
Console.WriteLine($"عنوان: {title}");
```

### نمایش اطلاعات Assembly
```csharp
// نمایش اطلاعات کامل برنامه
Console.WriteLine($"محصول: {ApplicationInfo.ProductName}");
Console.WriteLine($"شرکت: {ApplicationInfo.CompanyName}");
Console.WriteLine($"کپی‌رایت: {ApplicationInfo.CopyrightHolder}");
Console.WriteLine($"توضیحات: {ApplicationInfo.Description}");
```

### مقایسه نسخه
```csharp
if (ApplicationInfo.Version.Major >= 2)
{
    // مدیریت نسخه 2.0 یا بالاتر
}
```

## نکات مهم

1. **زمینه Assembly**:
   - برای همه خواص از GetCallingAssembly() استفاده می‌کند
   - اطلاعات assembly فراخواننده خاصیت را برمی‌گرداند
   - ممکن است بر اساس زمینه فراخوانی نتایج متفاوتی برگرداند

2. **وابستگی‌های صفت**:
   - خواص به صفات assembly وابسته هستند
   - صفات تعریف نشده رشته خالی برمی‌گردانند (به جز Title)
   - Title به نام اجرایی به عنوان جایگزین برمی‌گردد

3. **ملاحظات کارایی**:
   - خواص پرس‌وجوهای صفت را ذخیره می‌کنند
   - تأثیر حداقلی بر کارایی
   - امن برای دسترسی مکرر

## موارد استفاده رایج

1. **نمایش اطلاعات برنامه**:
   - صفحات درباره
   - مستندات راهنما
   - ثبت خطا

2. **مدیریت نسخه**:
   - تغییر ویژگی‌ها
   - بررسی سازگاری
   - تأیید به‌روزرسانی

3. **برندسازی محصول**:
   - نمایش رابط کاربری
   - تولید گزارش
   - مستندسازی

## وابستگی‌ها

- فضای نام System
- فضای نام System.Reflection