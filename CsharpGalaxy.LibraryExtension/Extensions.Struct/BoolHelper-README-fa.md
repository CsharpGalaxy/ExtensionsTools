# کلاس‌های BoolHelper و BoolExtensions

این مستندات شامل کلاس استاتیک `BoolHelper` و متدهای توسعه `BoolExtensions` است که قابلیت‌های جامعی برای عملیات منطقی ارائه می‌دهند.

## متدهای BoolExtensions

### عملیات پایه و معکوس

| متد | پارامترها | نوع خروجی | توضیحات |
|-----|-----------|------------|----------|
| Not | this bool value | bool | مقدار بولین را نقیض می‌کند. |
| Toggle | this bool value | bool | بین true و false جابجا می‌کند. |
| Negate | this bool value | bool | نام دیگری برای عملیات Not. |

### عملیات دو مقدار بولین

| متد | پارامترها | نوع خروجی | توضیحات |
|-----|-----------|------------|----------|
| Xor | this bool a, bool b | bool | عملیات XOR بین دو مقدار بولین. |
| Nand | this bool a, bool b | bool | عملیات NAND (NOT AND). |
| Nor | this bool a, bool b | bool | عملیات NOR (NOT OR). |
| Xnor | this bool a, bool b | bool | عملیات XNOR (برابری). |
| Implies | this bool a, bool b | bool | استلزام منطقی (A → B). |
| Iff | this bool a, bool b | bool | هم‌ارزی منطقی (A ↔ B). |
| Equals | this bool a, bool b | bool | مقایسه برابری دو مقدار بولین. |
| Compare | this bool a, bool b | int | مقایسه بولین‌ها، برگرداندن -1، 0 یا 1. |

### عملیات بررسی

| متد | پارامترها | نوع خروجی | توضیحات |
|-----|-----------|------------|----------|
| IsTrue | this bool value | bool | بررسی می‌کند آیا مقدار برابر true است. |
| IsFalse | this bool value | bool | بررسی می‌کند آیا مقدار برابر false است. |

### عملیات تبدیل

| متد | پارامترها | نوع خروجی | توضیحات |
|-----|-----------|------------|----------|
| ToInt | this bool value | int | تبدیل true→1، false→0. |
| ToStringYN | this bool value | string | "Y"/"N" برمی‌گرداند. |
| ToString10 | this bool value | string | "1"/"0" برمی‌گرداند. |
| ToStringOnOff | this bool value | string | "ON"/"OFF" برمی‌گرداند. |
| ToStringYesNo | this bool value | string | "Yes"/"No" برمی‌گرداند. |

## متدهای BoolHelper

### عملیات مجموعه

| متد | پارامترها | نوع خروجی | توضیحات |
|-----|-----------|------------|----------|
| And | params bool[] values | bool | AND منطقی چندین مقدار بولین. |
| Or | params bool[] values | bool | OR منطقی چندین مقدار بولین. |
| AllTrue | IEnumerable<bool> values | bool | بررسی می‌کند آیا همه عناصر true هستند. |
| AllFalse | IEnumerable<bool> values | bool | بررسی می‌کند آیا همه عناصر false هستند. |
| AnyTrue | IEnumerable<bool> values | bool | بررسی می‌کند آیا حداقل یک true وجود دارد. |
| AnyFalse | IEnumerable<bool> values | bool | بررسی می‌کند آیا حداقل یک false وجود دارد. |
| CountTrue | IEnumerable<bool> values | int | تعداد مقادیر true را می‌شمارد. |
| CountFalse | IEnumerable<bool> values | int | تعداد مقادیر false را می‌شمارد. |

### تجزیه و تولید تصادفی

| متد | پارامترها | نوع خروجی | توضیحات |
|-----|-----------|------------|----------|
| FromInt | int value | bool | تبدیل 0→false، غیر0→true. |
| ParseLenient | string s | bool | تجزیه رشته‌های مختلف بولین بدون حساسیت به حروف کوچک و بزرگ. |
| Random | ندارد | bool | تولید مقدار بولین تصادفی. |
| RandomWithProbability | double probability | bool | تولید true با احتمال مشخص شده (0...1). |

### مثال‌های استفاده

```csharp
// مثال‌های BoolExtensions
bool value = true;
bool negated = value.Not();          // false
bool toggled = value.Toggle();       // false
int asInt = value.ToInt();          // 1
string asYN = value.ToStringYN();    // "Y"

// عملیات منطقی
bool a = true, b = false;
bool xor = a.Xor(b);                // true
bool implies = a.Implies(b);         // false

// مثال‌های BoolHelper
bool allTrue = BoolHelper.AllTrue(new[] { true, true });     // true
bool anyFalse = BoolHelper.AnyFalse(new[] { true, false }); // true
int trueCount = BoolHelper.CountTrue(new[] { true, false }); // 1

// تجزیه و تصادفی
bool parsed = BoolHelper.ParseLenient("yes");                // true
bool random = BoolHelper.RandomWithProbability(0.7);         // 70% احتمال true
```

### نکات کارایی

- اکثر عملیات با AggressiveInlining بهینه شده‌اند
- عملیات مجموعه به طور ایمن با ورودی‌های null برخورد می‌کنند
- تبدیل‌های رشته‌ای برای حالت‌های رایج بهینه شده‌اند