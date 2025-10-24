# 🔒 مستندات کلاس HashingHelper

## 🛡️ **متدهای امنیتی رمز عبور**
| متد | توضیحات | پارامترها | نوع خروجی |
|-----|----------|-----------|------------|
| `GenerateSalt` | تولید Salt تصادفی برای هش کردن رمز عبور | `size` (اختیاری، پیش‌فرض: 16): طول Salt به بایت | `string` (کدشده با Base64) |
| `HashPassword` | ایجاد هش SHA256 از ترکیب رمز عبور و Salt | `password`: رمز عبور برای هش کردن<br>`salt`: Salt مورد استفاده | `string` (هش کدشده با hex) |
| `ComparePassword` | مقایسه امن رمز عبور با هش ذخیره شده | `password`: هش ذخیره شده<br>`newPassword`: رمز عبور برای بررسی<br>`sult`: Salt استفاده شده برای هش | `bool` |

---

## 📝 **مثال‌های استفاده**

### ایجاد هش جدید برای رمز عبور
```csharp
// تولید Salt جدید
string salt = HashingHelper.GenerateSalt(); // پیش‌فرض 16 بایت
// یا تعیین اندازه دلخواه
string customSalt = HashingHelper.GenerateSalt(32); // 32 بایت

// هش کردن رمز عبور
string password = "MySecurePassword123";
string hashedPassword = HashingHelper.HashPassword(password, salt);
```

### تایید رمز عبور
```csharp
string storedHash = "..."; // هش ذخیره شده قبلی
string salt = "...";       // Salt ذخیره شده قبلی
string inputPassword = "MySecurePassword123";

bool isValid = HashingHelper.ComparePassword(storedHash, inputPassword, salt);
```

---

## ⚠️ **نکات امنیتی**
- همیشه هم Salt و هم هش را به صورت امن ذخیره کنید
- هرگز رمز عبور را به صورت متن ساده ذخیره نکنید
- برای هر رمز عبور از یک Salt منحصر به فرد استفاده کنید
- اندازه Salt برابر 16 بایت (128 بیت) معمولاً کافی است
- از SHA256 برای هش کردن استفاده می‌کند که از نظر رمزنگاری امن است
- تمام عملیات از تولید اعداد تصادفی امن استفاده می‌کنند (RNGCryptoServiceProvider)

---

## 🔍 **جزئیات فنی**
- استفاده از `SHA256` برای هش کردن
- Salt با کدگذاری Base64 ذخیره می‌شود
- هش نهایی با کدگذاری hex (حروف کوچک) ذخیره می‌شود
- تمام عملیات با استفاده از کدگذاری UTF-8 انجام می‌شود
- استفاده از `RNGCryptoServiceProvider` برای تولید Salt تصادفی امن از نظر رمزنگاری