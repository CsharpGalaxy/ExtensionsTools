
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

## 🚩 **کار با [Flags] (پرچم‌ها)**
| متد | کاربرد |
|------|--------|
| `Has(this Enum enumValue, Enum flag)` | بررسی می‌کند که آیا فلگ مشخص شده در مقدار enum ست شده است یا خیر. (برای enumهایی که `[Flags]` دارند) |
| `Set<T>(this Enum enumValue, T flag) where T : Enum` | افزودن یک flag به مقدار enum و بازگرداندن مقدار جدید (برای `[Flags]`). |
| `Clear<T>(this Enum enumValue, T flag) where T : Enum` | حذف یک flag از مقدار enum و بازگرداندن مقدار جدید (برای `[Flags]`). |

**نکات مهم:**  
- اگر `enumValue` یا `flag` برابر `null` باشد، `Has(...)` مقدار `false` برمی‌گرداند.  
- اگر enum مورد نظر با `[Flags]` تزئین نشده باشد، متدها `Has/Set/Clear` یک `ArgumentException` پرتاب می‌کنند.  
- این متدها با تبدیل به `UInt64` عمل بیت‌به‌بیتی را انجام می‌دهند تا با انواع ساختارهای پایهٔ مختلف enum سازگار باشند.

---

## 🔧 **کمک‌ کننده‌ها و پیاده‌سازی‌های اضافی (Legacy / Alternate Implementations)**
| متد | کاربرد |
|------|--------|
| `AsName(this Enum @this)` | برگرداندن نام عضو enum (معادل `Enum.GetName`). |
| `AsDescription(this Enum @this)` | گرفتن `DescriptionAttribute` در صورت وجود و در غیر این‌صورت نام عضو. |
| `AsValue(this Enum @this)` | تبدیل enum به مقدار عددی `int` (مقدار underlying به‌صورت int بازگردانده می‌شود). |
| `ToEnumString<TEnum>(this int enumValue)` | گرفتن نام enum مربوط به مقدار عددی؛ اگر مقدار تعریف نشده باشد، رشتهٔ عددی را برمی‌گرداند. |
| `ToEnum<T>(this string enumString)` | **نسخهٔ ناایمن**: پارس رشته به enum با `Enum.Parse` — در صورت نامعتبر بودن رشته **استثناء** پرتاب می‌شود. |
| `EnumJoin<T>()` | اتصال همهٔ مقادیر enum به یک رشتهٔ جداشده با `,`. |
| `Has(this Enum @this, int value)` | بررسی می‌کند که آیا مقدار عددی مشخص در تعریف enum وجود دارد (`Enum.IsDefined`). |
| `Has(this Enum @this, string value)` | بررسی می‌کند که آیا نام رشته‌ای مشخص در تعریف enum وجود دارد (`Enum.IsDefined`). |
| `ToDict(this Enum theEnum)` | تبدیل enum به `Dictionary<int,string>` که کلیدها مقدار عددی و مقادیر نام عضو هستند. |

**هشدار/نکات فنی:**  
- `ToEnum<T>(enumString)` ناایمن است و اگر رشته‌ای نامعتبر باشد استثناء پرتاب می‌کند — از `ToEnum<T>(...)` امن‌تر (با TryParse) در بخش قبل استفاده کنید مگر اینکه مطمئن باشید ورودی درست است.  
- `AsValue()` و `ToDict()` فرض کرده‌اند که تبدیل به `int` منطقی است؛ اگر underlying type enum شما چیزی غیر از `int` باشد (مثلاً `byte` یا `long`) ممکن است رفتار مورد انتظار را نداشته باشد یا نیاز به تبدیل متفاوت داشته باشید.  
- `ToDict()` از `Cast<int>()` استفاده می‌کند؛ برای enumهایی که underlying type آن‌ها `int` نیست، ممکن است لازم باشد کدنویسی را بازبینی کنید.

---