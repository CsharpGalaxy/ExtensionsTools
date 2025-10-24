# کلاس IoExtensions

این مستندات مربوط به کلاس `IoExtensions` است که متدهای توسعه‌ای برای عملیات فایل و پوشه، از جمله بارگذاری JSON، افزودن متن، کپی، حذف و شمارش رشته را فراهم می‌کند.

## متدها

### LoadJson<T>

| پارامتر | نوع | توضیحات |
|---------|-----|----------|
| jsonPath | string | مسیر کامل فایل JSON |
| T | generic | نوع داده‌ای که JSON باید به آن تبدیل شود |
| خروجی | T | شیء تبدیل شده در صورت موفقیت؛ در غیر این صورت مقدار پیش‌فرض T |

### AppendText

| پارامتر | نوع | توضیحات |
|---------|-----|----------|
| path | string | مسیر فایل |
| text | string | متنی که باید اضافه شود |
| خروجی | void | - |

### CopyFolder

| پارامتر | نوع | توضیحات |
|---------|-----|----------|
| sourcePath | string | مسیر پوشه مبدأ |
| targetPath | string | مسیر پوشه مقصد |
| خروجی | bool | true اگر پوشه وجود داشته و فایل‌ها کپی شدند؛ در غیر این صورت false |

### DeleteFolder

| پارامتر | نوع | توضیحات |
|---------|-----|----------|
| dir | string | مسیر پوشه |
| recursivo | bool | اگر true باشد، پوشه را به صورت بازگشتی با تمام زیرپوشه‌ها و فایل‌ها حذف می‌کند |
| خروجی | bool | true اگر پوشه وجود داشت و حذف شد؛ در غیر این صورت false |

### CountStr

| پارامتر | نوع | توضیحات |
|---------|-----|----------|
| file | string | مسیر فایلی که باید جستجو شود |
| str | string | رشته‌ای که باید شمارش شود |
| خروجی | int | تعداد دفعات تکرار رشته در فایل |

## مثال‌های استفاده

### بارگذاری فایل JSON
```csharp
// بارگذاری فایل پیکربندی در کلاس Config
var config = IoExtensions.LoadJson<Config>("config.json");
if (config != null)
{
    // استفاده از شیء config
}
```

### افزودن متن
```csharp
// افزودن یک ورودی لاگ به فایل
IoExtensions.AppendText("log.txt", "ورودی جدید لاگ: " + DateTime.Now.ToString());
```

### کپی کردن پوشه‌ها
```csharp
// کپی پوشه منبع به محل پشتیبان
bool success = IoExtensions.CopyFolder(@"C:\SourceFolder", @"C:\Backup\SourceFolder");
if (success)
{
    Console.WriteLine("پشتیبان‌گیری با موفقیت انجام شد");
}
```

### حذف پوشه‌ها
```csharp
// حذف یک پوشه موقت و تمام محتویات آن
bool deleted = IoExtensions.DeleteFolder(@"C:\TempFolder", true);
if (deleted)
{
    Console.WriteLine("پوشه موقت پاک‌سازی شد");
}
```

### شمارش تکرار رشته
```csharp
// شمارش تعداد دفعات تکرار "ERROR" در فایل لاگ
int errorCount = IoExtensions.CountStr("application.log", "ERROR");
Console.WriteLine($"تعداد {errorCount} خطا در لاگ پیدا شد");
```

## نکات مهم

1. **مدیریت خطا**:
   - `LoadJson<T>` دارای مدیریت خطای داخلی است و در صورت شکست default(T) را برمی‌گرداند
   - سایر متدها ممکن است استثناهای استاندارد IO را پرتاب کنند که باید توسط فراخواننده مدیریت شوند

2. **عملیات فایل**:
   - `AppendText` در صورت عدم وجود فایل، آن را ایجاد می‌کند
   - `CopyFolder` فایل‌های موجود در پوشه مقصد را بازنویسی می‌کند
   - `DeleteFolder` با recursivo=true تمام محتویات را بدون تأیید حذف می‌کند

3. **ملاحظات کارایی**:
   - `CountStr` کل فایل را در حافظه می‌خواند
   - `CopyFolder` عملیات فایل را به صورت همگام انجام می‌دهد

## وابستگی‌ها

- فضای نام System.IO
- فضای نام System.Text.RegularExpressions
- فضای نام CsharpGalexy.LibraryExtention.Helpers.Json