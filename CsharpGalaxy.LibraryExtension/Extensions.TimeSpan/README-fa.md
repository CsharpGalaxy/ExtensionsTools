# ⏱️ مستندات توابع تمدیدی TimeSpan

## 📝 **فرمت‌بندی و نمایش**
| متد | توضیحات | پارامترها | نوع خروجی | مثال |
|-----|----------|-----------|------------|-------|
| `ToHumanReadable` | تبدیل TimeSpan به متن خوانای انگلیسی | `TimeSpan` | `string` | "2 days, 3 hours, 5 minutes" |
| `ToHumanReadablePersian` | تبدیل TimeSpan به متن خوانای فارسی | `TimeSpan` | `string` | "2 روز, 3 ساعت, 5 دقیقه" |
| `ToShortString` | ایجاد نمایش فشرده | `TimeSpan` | `string` | "2d 3h 5m" |
| `ToDictionary` | تبدیل به دیکشنری با کلیدهای انگلیسی | `TimeSpan` | `Dictionary<string,int>` | `{"Days": 2, "Hours": 3, ...}` |
| `ToDictionaryPersian` | تبدیل به دیکشنری با کلیدهای فارسی | `TimeSpan` | `Dictionary<string,int>` | `{"روز": 2, "ساعت": 3, ...}` |

## 📊 **محاسبات و اندازه‌گیری‌ها**
| متد | توضیحات | پارامترها | نوع خروجی | مثال |
|-----|----------|-----------|------------|-------|
| `TotalMinutesExact` | محاسبه کل دقایق | `TimeSpan` | `double` | `90.5` برای 1.5 ساعت |
| `TotalWeeks` | محاسبه کل هفته‌ها | `TimeSpan` | `double` | `1.0` برای 7 روز |
| `PercentageOf` | محاسبه درصد نسبت به TimeSpan دیگر | `TimeSpan, TimeSpan other` | `double` | `50.0` برای نصف دیگری |
| `Multiply` | ضرب TimeSpan در یک ضریب | `TimeSpan, double factor` | `TimeSpan` | `2 ساعت` * 2 = `4 ساعت` |
| `Divide` | تقسیم TimeSpan بر یک عدد | `TimeSpan, double divisor` | `TimeSpan` | `4 ساعت` / 2 = `2 ساعت` |

## ✂️ **گرد کردن و برش**
| متد | توضیحات | پارامترها | نوع خروجی |
|-----|----------|-----------|------------|
| `RoundToNearestMinute` | گرد کردن به نزدیکترین دقیقه | `TimeSpan` | `TimeSpan` |
| `RoundToNearestHour` | گرد کردن به نزدیکترین ساعت | `TimeSpan` | `TimeSpan` |
| `RoundToNearestDay` | گرد کردن به نزدیکترین روز | `TimeSpan` | `TimeSpan` |
| `TruncateToMinutes` | حذف ثانیه‌ها و میلی‌ثانیه‌ها | `TimeSpan` | `TimeSpan` |
| `TruncateToHours` | حذف دقیقه‌ها و کوچکتر | `TimeSpan` | `TimeSpan` |
| `TruncateToDays` | حذف ساعت‌ها و کوچکتر | `TimeSpan` | `TimeSpan` |

## 🔍 **اعتبارسنجی و بررسی‌ها**
| متد | توضیحات | پارامترها | نوع خروجی |
|-----|----------|-----------|------------|
| `IsPositive` | بررسی مثبت بودن TimeSpan | `TimeSpan` | `bool` |
| `IsNegative` | بررسی منفی بودن TimeSpan | `TimeSpan` | `bool` |
| `IsZero` | بررسی صفر بودن TimeSpan | `TimeSpan` | `bool` |
| `Between` | بررسی قرار داشتن بین دو مقدار | `TimeSpan, TimeSpan min, TimeSpan max` | `bool` |

## 🔄 **دستکاری**
| متد | توضیحات | پارامترها | نوع خروجی |
|-----|----------|-----------|------------|
| `Clamp` | محدود کردن TimeSpan به یک بازه | `TimeSpan, TimeSpan min, TimeSpan max` | `TimeSpan` |
| `Abs` | گرفتن قدر مطلق | `TimeSpan` | `TimeSpan` |
| `AddBusinessDays` | اضافه کردن روزهای کاری (بدون آخر هفته) | `TimeSpan, int businessDays` | `TimeSpan` |

## 📝 **مثال‌های استفاده**

```csharp
var timeSpan = TimeSpan.FromDays(2.5);

// فرمت‌بندی
Console.WriteLine(timeSpan.ToHumanReadable());     // "2 days, 12 hours"
Console.WriteLine(timeSpan.ToHumanReadablePersian()); // "2 روز, 12 ساعت"
Console.WriteLine(timeSpan.ToShortString());       // "2d 12h"

// محاسبات
Console.WriteLine(timeSpan.TotalWeeks());         // 0.357...
Console.WriteLine(timeSpan.Multiply(2));          // 5 روز
Console.WriteLine(timeSpan.PercentageOf(TimeSpan.FromDays(5))); // 50%

// گرد کردن
Console.WriteLine(timeSpan.RoundToNearestDay());  // 3 روز
Console.WriteLine(timeSpan.TruncateToDays());     // 2 روز

// اعتبارسنجی
Console.WriteLine(timeSpan.IsPositive());         // true
Console.WriteLine(timeSpan.Between(
    TimeSpan.FromDays(1), 
    TimeSpan.FromDays(3))); // true
```