
---

## 🔍 **دریافت توضیحات و متون نمایش (Description / Display)**
| متد | کاربرد |
|------|--------|
| `GetDescription()` | گرفتن مقدار `DescriptionAttribute` روی عضو enum؛ در صورت نبودن description، نام enum برگردانده می‌شود. |
| `GetDisplayName()` | گرفتن مقدار `DisplayAttribute.Name` (در صورت وجود) و در غیر این‌صورت `DescriptionAttribute.Description`؛ اگر هیچ‌کدام موجود نباشد `ToString()` باز می‌گردد. |

**نکات:** `GetDisplayName()` ابتدا `DisplayAttribute` را بررسی می‌کند و سپس به سراغ `DescriptionAttribute` می‌رود. اگر فیلد enum پیدا نشود، مقدار `ToString()` بازگردانده می‌شود.

---

## 🔽 **تبدیل enum به لیست برای استفاده در Dropdown / Selects**
| متد | کاربرد |
|------|--------|
| `ToList<T>() where T : Enum` | تبدیل نوع enum به `IEnumerable<KeyValuePair<int,string>>` برای استفاده در dropdownها (کلید: مقدار عددی، مقدار: متن نمایش). |
| `ToSelectList<T>() where T : Enum` | نسخهٔ تکuple: `IEnumerable<(int Value, string Text)>` شامل مقدار عددی و متن نمایش (`GetDisplayName`). |

**نکته:** متن نمایش برای هر عضو با `GetDisplayName()` تولید می‌شود، پس اگر از `DisplayAttribute` یا `DescriptionAttribute` استفاده کرده باشید، آن متن نشان داده خواهد شد.

---

## 🔁 **تبدیل امن رشته به Enum (Safe String to Enum Conversion)**
| متد | کاربرد |
|------|--------|
| `ToEnum<T>(this string value, bool ignoreCase = true) where T : struct, IConvertible` | تلاش برای پارس کردن رشته به `T` (enum). در صورت نامعتبر بودن یا خالی بودن رشته، مقدار پیش‌فرض `default(T)` بازمی‌گردد. |
| `ToEnum<T>(this string value, T defaultValue, bool ignoreCase = true) where T : struct, IConvertible` | تلاش برای پارس کردن رشته؛ اگر موفق نبود `defaultValue` بازگردانده می‌شود. |

**نکات:** این متدها از `Enum.TryParse` استفاده می‌کنند و خطا پرتاب نمی‌کنند؛ بنابراین امن برای ورودی‌های نا معتبر هستند. توجه داشته باشید قید `where T : struct, IConvertible` به این دلیل است که `Enum` به‌طور مستقیم قابل استفاده در محدودیت‌های generic در همه نسخه‌ها نیست.

---

## ✅ **اعتبارسنجی مقدار Enum**
| متد | کاربرد |
|------|--------|
| `IsValid<T>(this T value) where T : Enum` | بررسی می‌کند که مقدار `value` در نوع enum مربوطه تعریف شده باشد (`Enum.IsDefined`). |

**نکته:** این متد مناسب بررسی قبل از نمایش یا پردازش مقادیر ورودی است.

---

## 📋 **دریافت مقادیر و نام‌های Enum**
| متد | کاربرد |
|------|--------|
| `GetValues<T>() where T : Enum` | گرفتن آرایه‌ای از تمامی مقادیر enum (`T[]`). |
| `GetNames<T>() where T : Enum` | گرفتن آرایه‌ای از نام‌های اعضای enum (`string[]`). |

**نکته:** این متدها مستقیماً از `Enum.GetValues` و `Enum.GetNames` استفاده می‌کنند و برای ساخت منوها/لیست‌ها کاربردی هستند.

---

## 🧩 **دریافت Attribute از عضو Enum**
| متد | کاربرد |
|------|--------|
| `GetAttribute<TAttribute>(this Enum value) where TAttribute : Attribute` | گرفتن یک attribute اختصاصی (مثلاً `DescriptionAttribute`, `DisplayAttribute`, ...) از عضو enum یا `null` در صورت نبودن. |

**مثال ساده:** `myEnumValue.GetAttribute<DescriptionAttribute>()`

---