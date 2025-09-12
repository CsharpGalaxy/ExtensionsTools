using CsharpGalexy.Console.Models;
using ExtentionLibrary.DateTimes;
using ExtentionLibrary.Enums;



#region DateTime
var now = DateTime.Now;

// تبدیل به شمسی
Console.WriteLine(now.ToShamsiDate()); // خروجی: 1403/02/15
Console.WriteLine(now.ToShamsiDate("dd MMMM yyyy")); // 15 فروردین 1403

// بررسی تاریخ
Console.WriteLine(now.IsToday()); // True
Console.WriteLine(now.AddDays(-1).IsYesterday()); // True

// محاسبات
Console.WriteLine(now.FirstDayOfMonth()); // 1403/02/01
Console.WriteLine(now.LastDayOfMonth()); // 1403/02/31

// زمان سپری شده
Console.WriteLine(now.AddHours(-3).TimeAgo()); // "3 ساعت پیش"

// سن
Console.WriteLine(new DateTime(1990, 4, 20).Age()); // 34

// روزهای کاری
Console.WriteLine(now.AddBusinessDays(5)); // 5 روز کاری بعد

// ترکیب با LINQ
var dates = new List<DateTime> { now, now.AddDays(-1), now.AddDays(2) };
var todayOrLater = dates.Where(d => d.IsToday() || d.IsFuture());
#endregion


#region Enum
// نمایش توضیحات
Console.WriteLine(UserStatus.Admin.GetDescription()); // "مدیر سیستم"
Console.WriteLine(UserStatus.Blocked.GetDisplayName()); // "غیرفعال"

// تبدیل به لیست (مثلاً برای dropdown)
var list = EnumExtensions.ToList<UserStatus>();
foreach (var item in list)
{
Console.WriteLine($"{item.Key}: {item.Value}");
}

// یا با tuple:
var selectList = EnumExtensions.ToSelectList<UserStatus>();
// (1, "کاربر عادی"), (2, "مدیر سیستم"), (3, "غیرفعال")

// تبدیل ایمن از رشته
string input = "Admin";
var status = input.ToEnum<UserStatus>();
Console.WriteLine(status.GetDescription()); // "مدیر سیستم"

// بررسی معتبر بودن
Console.WriteLine(status.IsValid()); // True



//var perm = Permissions.Read | Permissions.Write;
//Console.WriteLine(perm.Has(Permissions.Read)); // True
//perm = perm.Clear(Permissions.Write).Set(Permissions.Delete);
#endregion


#region DateTime

#endregion
