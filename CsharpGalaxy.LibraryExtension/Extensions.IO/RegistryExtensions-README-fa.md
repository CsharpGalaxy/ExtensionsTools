# کلاس RegistryExtensions

این مستندات مربوط به کلاس `RegistryExtensions` است که متدهای توسعه‌ای برای کار با رجیستری ویندوز، به ویژه برای بازیابی اطلاعات نرم‌افزارهای نصب شده را فراهم می‌کند.

## متدها

### GetInstalledSoftware

| پارامتر | نوع خروجی | توضیحات |
|---------|------------|----------|
| ندارد | List<AppInfo> | لیستی از برنامه‌های نصب شده در سیستم را برمی‌گرداند. |

#### جزئیات مقدار برگشتی (ویژگی‌های AppInfo)

| ویژگی | نوع | توضیحات |
|--------|-----|----------|
| Application | string | نام نمایشی برنامه نصب شده |
| InstallLocation | string | مسیر دایرکتوری نصب برنامه |

### جزئیات فنی

- **مسیر رجیستری**: `SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall`
- **دسترسی به رجیستری**: از `Registry.LocalMachine` استفاده می‌کند
- **خواندن کلید**: از کلید Uninstall رجیستری برای جمع‌آوری اطلاعات نصب استفاده می‌کند
- **مدیریت مقادیر تهی**: ورودی‌های با نام برنامه تهی را فیلتر می‌کند

### مثال استفاده

```csharp
// دریافت لیست برنامه‌های نصب شده
List<AppInfo> installedApps = RegistryExtensions.GetInstalledSoftware();

// پردازش نتایج
foreach(var app in installedApps)
{
    Console.WriteLine($"برنامه: {app.Application}");
    Console.WriteLine($"محل نصب: {app.InstallLocation}");
}
```

### نکات مهم

1. **ملاحظات امنیتی**:
   - نیاز به مجوزهای دسترسی مناسب به رجیستری دارد
   - باید با سطح دسترسی کافی برای خواندن HKEY_LOCAL_MACHINE اجرا شود

2. **سازگاری پلتفرم**:
   - قابلیت مختص ویندوز
   - در پلتفرم‌های غیر ویندوز در دسترس نیست

3. **کیفیت داده‌ها**:
   - برخی برنامه‌ها ممکن است ورودی‌های رجیستری کاملی نداشته باشند
   - InstallLocation ممکن است برای برخی برنامه‌ها تهی یا خالی باشد
   - فقط برنامه‌هایی با DisplayName غیر تهی را برمی‌گرداند

4. **کارایی**:
   - به طور کارآمد از LINQ برای پردازش ورودی‌های رجیستری استفاده می‌کند
   - تخصیص حافظه حداقلی انجام می‌دهد
   - نتایج فیلتر شده را برای جلوگیری از ارجاع‌های تهی برمی‌گرداند

### بهترین شیوه‌ها

1. **مدیریت خطا**:
   ```csharp
   try
   {
       var apps = RegistryExtensions.GetInstalledSoftware();
       // پردازش نتایج
   }
   catch (SecurityException)
   {
       // مدیریت دسترسی ناکافی
   }
   catch (Exception)
   {
       // مدیریت سایر مشکلات احتمالی دسترسی به رجیستری
   }
   ```

2. **بررسی مقادیر تهی**:
   ```csharp
   foreach(var app in installedApps)
   {
       if (!string.IsNullOrEmpty(app.InstallLocation))
       {
           // پردازش دایرکتوری نصب
       }
   }
   ```

### وابستگی‌ها

- فضای نام `Microsoft.Win32`
- کلاس `CsharpGalexy.LibraryExtention.Models.IO.AppInfo`