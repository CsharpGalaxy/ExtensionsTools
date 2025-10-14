# کلاس CurrencyExtensions

این مستندات مربوط به کلاس `CurrencyExtensions` است که متدهای گسترده‌ای برای مدیریت فرمت‌بندی و محاسبات پول ایران (ریال/تومان) ارائه می‌دهد.

## متدهای نمایش فرمت

### فرمت‌بندی پایه پول

| متد | پارامترها | نوع خروجی | توضیحات |
|-----|-----------|------------|----------|
| ToRialString | decimal value | string | مبلغ را به ریال با جداکننده هزارگان فرمت می‌کند |
| ToTomanString | decimal value | string | به تومان تبدیل می‌کند (تقسیم بر 10) و فرمت می‌کند |
| ToCurrencyString | decimal value, string unit | string | مبلغ را با واحد پولی دلخواه فرمت می‌کند |

### فرمت‌بندی محلی

| متد | پارامترها | نوع خروجی | توضیحات |
|-----|-----------|------------|----------|
| ToPersianDigits | decimal value, string format="N0" | string | اعداد را به ارقام فارسی تبدیل می‌کند |
| ToLocalizedMoney | decimal value, CultureInfo culture | string | مبلغ را بر اساس فرهنگ فرمت می‌کند |
| FormatWithCulture | decimal value, string cultureCode="fa-IR" | string | با استفاده از کد فرهنگ مشخص شده فرمت می‌کند |

### فرمت‌های خوانا

| متد | پارامترها | نوع خروجی | توضیحات |
|-----|-----------|------------|----------|
| ToMoneyWithSuffix | decimal value | string | با پسوند (هزار، میلیون) نمایش می‌دهد |
| ToCompactMoney | decimal value | string | نمایش فشرده با پسوندهای K/M |
| ToColoredMoney | decimal value | string | اشاره‌های رنگی برای مثبت/منفی اضافه می‌کند |
| ToMoneyWords | decimal value | string | مبلغ را به حروف فارسی تبدیل می‌کند |

## عملیات مالی

### محاسبات

| متد | پارامترها | نوع خروجی | توضیحات |
|-----|-----------|------------|----------|
| RoundToNearest | decimal value, int step=100 | decimal | به نزدیک‌ترین مقدار گام گرد می‌کند |
| ApplyDiscount | decimal value, decimal percent | decimal | مبلغ بعد از تخفیف را محاسبه می‌کند |
| AddTax | decimal value, decimal percent | decimal | درصد مالیات را به مبلغ اضافه می‌کند |
| ConvertToCurrency | decimal value, decimal rate | decimal | با نرخ تبدیل ارز تبدیل می‌کند |

### منطق تجاری

| متد | پارامترها | نوع خروجی | توضیحات |
|-----|-----------|------------|----------|
| IsAffordable | decimal value, decimal balance | bool | بررسی می‌کند آیا موجودی کافی است |
| ToPercentageOf | decimal value, decimal total | decimal | درصد از کل را محاسبه می‌کند |
| ToDiscountedPrice | decimal discounted, decimal original | string | درصد تخفیف را نمایش می‌دهد |

## مثال‌های استفاده

### نمایش پایه پول
```csharp
decimal amount = 15000m;
string rial = amount.ToRialString();      // "15,000 ریال"
string toman = amount.ToTomanString();     // "1,500 تومان"
string custom = amount.ToCurrencyString("دلار"); // "15,000 دلار"
```

### فرمت‌های خوانا
```csharp
decimal amount = 1500000m;
string suffix = amount.ToMoneyWithSuffix();   // "1.5 میلیون تومان"
string compact = amount.ToCompactMoney();      // "1.5M تومان"
string words = amount.ToMoneyWords();          // "یک میلیون و پانصد هزار تومان"
```

### محاسبات مالی
```csharp
decimal price = 1000m;
decimal withTax = price.AddTax(9);         // 1090 (9% مالیات بر ارزش افزوده)
decimal withDiscount = price.ApplyDiscount(10); // 900 (10% تخفیف)
bool canBuy = price.IsAffordable(1500m);   // true
```

## نکات مهم

1. **تبدیل واحد پول**:
   - تومان = ریال تقسیم بر 10
   - از فرهنگ فارسی (fa-IR) برای فرمت‌بندی استفاده می‌کند
   - از هر دو نمایش ریال و تومان پشتیبانی می‌کند

2. **ویژگی‌های فرمت‌بندی**:
   - تبدیل ارقام به فارسی
   - جداکننده‌های هزارگان
   - ارقام اعشاری اختیاری
   - نمادها و موقعیت واحد پول

3. **ملاحظات کارایی**:
   - ذخیره‌سازی CultureInfo برای عملکرد بهتر
   - مدیریت کارآمد رشته‌ها
   - حداقل ایجاد شیء

## وابستگی‌ها

- فضای نام System.Globalization
- فضای نام System.Text
- فضای نام System.Text.RegularExpressions