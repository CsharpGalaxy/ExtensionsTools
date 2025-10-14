# کلاس‌های IntHelper و IntExtensions

این مستندات شامل کلاس استاتیک `IntHelper` و متدهای توسعه `IntExtensions` است که قابلیت‌های جامعی برای عملیات روی اعداد صحیح ارائه می‌دهند.

## متدهای IntExtensions

### عملیات پایه و ریاضی

| متد | پارامترها | نوع خروجی | توضیحات |
|-----|-----------|------------|----------|
| Abs | int number | int | قدر مطلق عدد را برمی‌گرداند. |
| Factorial | int n | long | فاکتوریل اعداد کوچک را محاسبه می‌کند (خروجی long). |
| Mod | int a, int n | int | عملیات باقیمانده را انجام می‌دهد که همیشه نتیجه مثبت است. |
| Sign | int number | int | علامت عدد را برمی‌گرداند: -1، 0 یا +1. |
| SqrtInt | int n | int | ریشه دوم صحیح را محاسبه می‌کند (خروجی int). |

### عملیات بررسی و محدودسازی

| متد | پارامترها | نوع خروجی | توضیحات |
|-----|-----------|------------|----------|
| IsBetween | int number, int min, int max | bool | بررسی می‌کند آیا عدد در بازه (min, max) قرار دارد. |
| Clamp | int number, int min, int max | int | عدد را در محدوده مشخص شده محدود می‌کند. |
| Cycle | int number, int min, int max | int | عدد را در محدوده می‌چرخاند (loop-back). |
| IsEven | int number | bool | بررسی می‌کند آیا عدد زوج است. |
| IsOdd | int number | bool | بررسی می‌کند آیا عدد فرد است. |
| IsPowerOfTwo | int number | bool | بررسی می‌کند آیا عدد توان دو است. |
| IsPrime | int number | bool | آزمایش می‌کند آیا عدد اول است. |
| RealValue | int? number | int | مقدار int قابل null را برمی‌گرداند یا 0 اگر null باشد. |

### عملیات بیت و باینری

| متد | پارامترها | نوع خروجی | توضیحات |
|-----|-----------|------------|----------|
| BitCount | int number | int | تعداد بیت‌های 1 در فرم باینری را می‌شمارد. |
| CeilToPowerOfTwo | int n | int | کوچکترین توان 2 که بزرگتر یا مساوی ورودی است را برمی‌گرداند. |
| HammingDistance | int a, int b | int | فاصله همینگ بین دو عدد صحیح را محاسبه می‌کند. |
| Log2 | int number | int | لگاریتم در مبنای 2 را محاسبه می‌کند. |
| Log10 | int number | int | لگاریتم در مبنای 10 را محاسبه می‌کند. |
| RotateLeft | int value, int offset | int | بیت‌ها را به چپ می‌چرخاند. |
| RotateRight | int value, int offset | int | بیت‌ها را به راست می‌چرخاند. |

### عملیات بایت و Endianness

| متد | پارامترها | نوع خروجی | توضیحات |
|-----|-----------|------------|----------|
| ToBytesBE | int value | byte[] | عدد را به 4 بایت در قالب Big-Endian تبدیل می‌کند. |
| ToBytesLE | int value | byte[] | عدد را به 4 بایت در قالب Little-Endian تبدیل می‌کند. |

### تبدیل رشته و رقم

| متد | پارامترها | نوع خروجی | توضیحات |
|-----|-----------|------------|----------|
| ReverseDigits | int number | int | ارقام عدد را معکوس می‌کند (123 → 321). |
| ToBinaryStringPadded | int number | string | رشته باینری 32 کاراکتری برمی‌گرداند. |
| ToHexStringPadded | int number | string | رشته هگزادسیمال 8 کاراکتری برمی‌گرداند. |
| ToOctalString | int number | string | نمایش رشته‌ای اکتال برمی‌گرداند. |

## متدهای IntHelper

### عملیات دو عددی

| متد | پارامترها | نوع خروجی | توضیحات |
|-----|-----------|------------|----------|
| AddClamped | int a, int b | int | دو عدد را با محدودسازی به MIN/MAX جمع می‌کند. |
| DivideRoundUp | int dividend, int divisor | int | تقسیم می‌کند و به سقف گرد می‌کند. |
| Gcd | int a, int b | int | بزرگترین مقسوم‌علیه مشترک را محاسبه می‌کند. |
| Lcm | int a, int b | long | کوچکترین مضرب مشترک را محاسبه می‌کند. |
| MultiplyClamped | int a, int b | int | با محدودسازی به MIN/MAX ضرب می‌کند. |
| Pow | int base, int exponent | long | توان عدد صحیح را محاسبه می‌کند. |
| SubtractClamped | int a, int b | int | با محدودسازی به MIN/MAX تفریق می‌کند. |
| Swap | ref int a, ref int b | void | دو مقدار int را بدون متغیر موقت جابجا می‌کند. |

### محاسبات ایمن

| متد | پارامترها | نوع خروجی | توضیحات |
|-----|-----------|------------|----------|
| SafeAdd | int a, int b | int | جمع با استثنای سرریز. |
| SafeMultiply | int a, int b | int | ضرب با استثنای سرریز. |
| SafeSubtract | int a, int b | int | تفریق با استثنای سرریز. |

### عملیات آرایه

| متد | پارامترها | نوع خروجی | توضیحات |
|-----|-----------|------------|----------|
| Average | params int[] numbers | int | میانگین را بدون سرریز محاسبه می‌کند. |
| Max | params int[] numbers | int | بیشترین مقدار را برمی‌گرداند. |
| Min | params int[] numbers | int | کمترین مقدار را برمی‌گرداند. |
| SumArray | params int[] numbers | long | مجموع آرایه را بدون سرریز محاسبه می‌کند. |
| ToCommaSeparatedString | params int[] numbers | string | آرایه را به رشته جدا شده با کاما تبدیل می‌کند. |

### عملیات بایت/Endianness

| متد | پارامترها | نوع خروجی | توضیحات |
|-----|-----------|------------|----------|
| BytesToIntBE | byte[] bytes | int | 4 بایت را در قالب Big-Endian به int تبدیل می‌کند. |
| BytesToIntLE | byte[] bytes | int | 4 بایت را در قالب Little-Endian به int تبدیل می‌کند. |

### تجزیه و تصادفی

| متد | پارامترها | نوع خروجی | توضیحات |
|-----|-----------|------------|----------|
| NextRandomInt | int min, int max | int | عدد تصادفی در محدوده تولید می‌کند. |
| ParseIntOrDefault | string s, int defaultValue | int | رشته را تجزیه می‌کند یا مقدار پیش‌فرض برمی‌گرداند. |
| ParseIntOrNull | string s | int? | رشته را تجزیه می‌کند یا null برمی‌گرداند. |

### مثال‌های استفاده

```csharp
// مثال‌های IntExtensions
int num = 123;
bool isEven = num.IsEven();
int abs = num.Abs();
int factorial = 5.Factorial();
string binary = num.ToBinaryStringPadded();

// مثال‌های IntHelper
int sum = IntHelper.AddClamped(int.MaxValue, 1);
int gcd = IntHelper.Gcd(24, 36);
int random = IntHelper.NextRandomInt(1, 100);
int? parsed = IntHelper.ParseIntOrNull("123");
```

### نکات کارایی
- اکثر عملیات با AggressiveInlining بهینه شده‌اند
- عملیات بیتی از کلاس کارآمد BitOperations استفاده می‌کنند
- عملیات آرایه به درستی سرریز را مدیریت می‌کنند