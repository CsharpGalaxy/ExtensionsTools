# 📅 مستندات توابع تمدیدی تاریخ و زمان

## 🌏 **تبدیل تاریخ شمسی**
| متد | توضیحات | پارامترها | نوع خروجی |
|-----|----------|-----------|------------|
| `ToShamsiDate` | تبدیل به تاریخ شمسی | `DateTime` | `string` (1403/02/15) |
| `ToShamsiDate` (با فرمت) | تبدیل با فرمت دلخواه | `DateTime, string format` | `string` |
| `GetPersianDayOfWeek` | دریافت نام روز هفته | `DateTime` | `string` (مثل "دوشنبه") |
| `GetPersianMonth` | دریافت نام ماه شمسی | `DateTime` | `string` (مثل "فروردین") |
| `GetTodayWeekDay` | دریافت روز هفته امروز | `DateTime?` | `WeekDay` enum |
| `GetNameOfDay` | دریافت نام فارسی روز هفته | `WeekDay` | `string` |

## ⏰ **مقایسه زمان**
| متد | توضیحات | پارامترها | نوع خروجی |
|-----|----------|-----------|------------|
| `DifTime` | بررسی اختلاف زمانی در محدوده مشخص | `DateTime, DateTime, int minutes` | `bool` |
| `IsBetween` | بررسی قرار داشتن تاریخ در بازه | `DateTime, start, end` | `bool` |
| `IsToday` | بررسی امروز بودن تاریخ | `DateTime` | `bool` |
| `IsYesterday` | بررسی دیروز بودن تاریخ | `DateTime` | `bool` |
| `IsTomorrow` | بررسی فردا بودن تاریخ | `DateTime` | `bool` |
| `IsFuture` | بررسی آینده بودن تاریخ | `DateTime` | `bool` |
| `IsPast` | بررسی گذشته بودن تاریخ | `DateTime` | `bool` |

## 📊 **محاسبات تاریخ**
| متد | توضیحات | پارامترها | نوع خروجی |
|-----|----------|-----------|------------|
| `AddBusinessDays` | افزودن روزهای کاری (بدون آخر هفته) | `DateTime, int days` | `DateTime` |
| `FirstDayOfMonth` | دریافت روز اول ماه | `DateTime` | `DateTime` |
| `LastDayOfMonth` | دریافت روز آخر ماه | `DateTime` | `DateTime` |
| `FirstDayOfYear` | دریافت روز اول سال | `DateTime` | `DateTime` |
| `LastDayOfYear` | دریافت روز آخر سال | `DateTime` | `DateTime` |
| `DaysInMonth` | دریافت تعداد روزهای ماه | `DateTime` | `int` |
| `Age` | محاسبه سن از تاریخ تولد | `DateTime` | `int` |

## 🕒 **واحدهای زمانی**
| متد | توضیحات | پارامترها | نوع خروجی |
|-----|----------|-----------|------------|
| `StartOfDay` | تنظیم ساعت به 00:00:00 | `DateTime` | `DateTime` |
| `EndOfDay` | تنظیم ساعت به 23:59:59.999 | `DateTime` | `DateTime` |
| `TruncateToSecond` | حذف میلی‌ثانیه | `DateTime` | `DateTime` |
| `TruncateToMinute` | حذف ثانیه و کوچکتر | `DateTime` | `DateTime` |
| `TruncateToHour` | حذف دقیقه و کوچکتر | `DateTime` | `DateTime` |

## 📝 **زمان خوانا برای انسان**
| متد | توضیحات | پارامترها | نوع خروجی |
|-----|----------|-----------|------------|
| `TimeAgo` | دریافت فاصله زمانی به صورت متنی | `DateTime` | `string` (مثل "2 دقیقه پیش") |

## 🔄 **وضعیت تاریخ**
| متد | توضیحات | پارامترها | نوع خروجی |
|-----|----------|-----------|------------|
| `IsThisWeek` | بررسی در هفته جاری | `DateTime` | `bool` |
| `IsThisMonth` | بررسی در ماه جاری | `DateTime` | `bool` |
| `IsThisYear` | بررسی در سال جاری | `DateTime` | `bool` |
| `IsWeekend` | بررسی آخر هفته بودن (جمعه/شنبه) | `DateTime` | `bool` |
| `IsWeekday` | بررسی روز کاری بودن | `DateTime` | `bool` |
| `IsPersianLeapYear` | بررسی کبیسه بودن سال شمسی | `int persianYear` | `bool` |

## 🔄 **Unix Timestamp**
| متد | توضیحات | پارامترها | نوع خروجی |
|-----|----------|-----------|------------|
| `ToUnixTimestamp` | تبدیل به Unix timestamp | `DateTimeOffset` | `long` |
| `FromUnixTimestamp` | تبدیل از Unix timestamp | `long timestamp` | `DateTimeOffset` |

## 📝 **مثال‌های استفاده**

### تبدیل تاریخ شمسی
```csharp
var date = DateTime.Now;
Console.WriteLine(date.ToShamsiDate());                    // "1403/02/15"
Console.WriteLine(date.ToShamsiDate("dd MMMM yyyy"));     // "15 اردیبهشت 1403"
Console.WriteLine(date.GetPersianDayOfWeek());            // "دوشنبه"
Console.WriteLine(date.GetPersianMonth());                // "اردیبهشت"
```

### محاسبات زمانی
```csharp
var date = DateTime.Now;
Console.WriteLine(date.AddBusinessDays(5));               // افزودن 5 روز کاری
Console.WriteLine(date.Age());                            // محاسبه سن
Console.WriteLine(date.TimeAgo());                        // "2 ساعت پیش"

// بررسی‌های تاریخ
Console.WriteLine(date.IsToday());                        // true
Console.WriteLine(date.IsFuture());                       // false
Console.WriteLine(date.IsWeekend());                      // false
```

## ⚠️ **نکات مهم**

1. تقویم شمسی:
   - استفاده داخلی از کلاس `PersianCalendar`
   - پشتیبانی از سال‌های 1 تا 9378 شمسی
   - مدیریت صحیح سال‌های کبیسه

2. روزهای کاری:
   - آخر هفته جمعه و شنبه (تقویم ایران)
   - عدم محاسبه تعطیلات رسمی

3. منطقه‌های زمانی:
   - متدها به صورت پیش‌فرض با زمان محلی کار می‌کنند
   - برای آگاهی از منطقه زمانی از DateTimeOffset استفاده کنید

4. کارایی:
   - بهینه‌سازی اکثر متدها برای عملکرد بهتر
   - ذخیره‌سازی موقت نتایج محاسبات تاریخ در موارد مناسب