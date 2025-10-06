using CsharpGalexy.Console.Models;
using CsharpGalexy.LibraryExtention.Models;
using System.ComponentModel;
using System.Text;
string str = "dasdad assad sd sds sdsd";
var a = str.TruncateMore(3);
#region DateTime
var now = DateTime.Now;
var yesterday = now.AddDays(-1);
var tomorrow = now.AddDays(1);
var birthDate = new DateTime(1990, 5, 20);

StringBuilder stringBuilder = new StringBuilder();
stringBuilder.AppendLine("=== Persian Date ===");
stringBuilder.AppendLine($"Today (Shamsi): {now.ToShamsiDate()}");
stringBuilder.AppendLine($"Today (Shamsi custom format): {now.ToShamsiDate("dd MMMM yyyy")}");
stringBuilder.AppendLine($"Day of week (Persian): {now.GetPersianDayOfWeek()}");
stringBuilder.AppendLine($"Get Month Of Year (Persian): {now.GetPersianMonth()}");

stringBuilder.AppendLine("\n=== Date Comparisons ===");
stringBuilder.AppendLine($"IsToday: {now.IsToday()}");
stringBuilder.AppendLine($"IsYesterday: {yesterday.IsYesterday()}");
stringBuilder.AppendLine($"IsTomorrow: {tomorrow.IsTomorrow()}");
stringBuilder.AppendLine($"IsFuture: {tomorrow.IsFuture()}");
stringBuilder.AppendLine($"IsPast: {yesterday.IsPast()}");
stringBuilder.AppendLine($"IsThisWeek: {now.IsThisWeek()}");
stringBuilder.AppendLine($"IsThisMonth: {now.IsThisMonth()}");
stringBuilder.AppendLine($"IsThisYear: {now.IsThisYear()}");

stringBuilder.AppendLine("\n=== Date Calculations ===");
stringBuilder.AppendLine($"Add 5 business days: {now.AddBusinessDays(5)}");
stringBuilder.AppendLine($"First day of month: {now.FirstDayOfMonth()}");
stringBuilder.AppendLine($"Last day of month: {now.LastDayOfMonth()}");
stringBuilder.AppendLine($"First day of year: {now.FirstDayOfYear()}");
stringBuilder.AppendLine($"Last day of year: {now.LastDayOfYear()}");
stringBuilder.AppendLine($"Days in month: {now.DaysInMonth()}");

stringBuilder.AppendLine("\n=== Time Ago ===");
stringBuilder.AppendLine($"Now: {now.TimeAgo()}");
stringBuilder.AppendLine($"1 hour ago: {now.AddHours(-1).TimeAgo()}");
stringBuilder.AppendLine($"2 days ago: {now.AddDays(-2).TimeAgo()}");
stringBuilder.AppendLine($"3 weeks ago: {now.AddDays(-21).TimeAgo()}");
stringBuilder.AppendLine($"5 months ago: {now.AddMonths(-5).TimeAgo()}");
stringBuilder.AppendLine($"2 years ago: {now.AddYears(-2).TimeAgo()}");

stringBuilder.AppendLine("\n=== Unix Timestamp ===");
var dto = new DateTimeOffset(now);
long timestamp = dto.ToUnixTimestamp();
stringBuilder.AppendLine($"Unix timestamp now: {timestamp}");
stringBuilder.AppendLine($"From timestamp: {DateTimeExtensions.FromUnixTimestamp(timestamp)}");

stringBuilder.AppendLine("\n=== Start/End of Day ===");
stringBuilder.AppendLine($"Start of day: {now.StartOfDay()}");
stringBuilder.AppendLine($"End of day: {now.EndOfDay()}");

stringBuilder.AppendLine("\n=== Age ===");
stringBuilder.AppendLine($"Birthdate: {birthDate}, Age: {birthDate.Age()}");

stringBuilder.AppendLine("\n=== Weekend / Weekday ===");
stringBuilder.AppendLine($"IsWeekend today: {now.IsWeekend()}");
stringBuilder.AppendLine($"IsWeekday today: {now.IsWeekday()}");

stringBuilder.AppendLine("\n=== Truncate ===");
stringBuilder.AppendLine($"Original time: {now:HH:mm:ss.fff}");
stringBuilder.AppendLine($"Truncate to second: {now.TruncateToSecond():HH:mm:ss.fff}");
stringBuilder.AppendLine($"Truncate to minute: {now.TruncateToMinute():HH:mm:ss.fff}");
stringBuilder.AppendLine($"Truncate to hour: {now.TruncateToHour():HH:mm:ss.fff}");

//File.WriteAllText("DateTimeExtensionsDemo.txt", stringBuilder.ToString());
#endregion


#region Enum
stringBuilder.Clear();
stringBuilder.AppendLine("=== GetDescription ===");
stringBuilder.AppendLine(SampleEnum.First.GetDescription());   // این مقدار اول است
stringBuilder.AppendLine(SampleEnum.Second.GetDescription());  // این مقدار دوم است
stringBuilder.AppendLine(SampleEnum.Third.GetDescription());   // Third

stringBuilder.AppendLine("\n=== GetDisplayName ===");
stringBuilder.AppendLine(SampleEnum.First.GetDisplayName());   // این مقدار اول است
stringBuilder.AppendLine(SampleEnum.Second.GetDisplayName());  // مقدار دوم (DisplayName بر Description ارجحیت دارد)
stringBuilder.AppendLine(SampleEnum.Third.GetDisplayName());   // Third

stringBuilder.AppendLine("\n=== ToList / ToSelectList ===");
var list = EnumExtensions.ToList<SampleEnum>();
foreach (var kv in list)
stringBuilder.AppendLine($"{kv.Key} - {kv.Value}");

var selectList = EnumExtensions.ToSelectList<SampleEnum>();
foreach (var item in selectList)
stringBuilder.AppendLine($"{item.Value} - {item.Text}");

stringBuilder.AppendLine("\n=== ToEnum ===");
string val1 = "Second";
string val2 = "second";
string val3 = "Unknown";

stringBuilder.AppendLine(val1.ToEnum<SampleEnum>().ToString());           // Second
stringBuilder.AppendLine(val2.ToEnum<SampleEnum>().ToString());           // Second (ignoreCase = true)
stringBuilder.AppendLine(val3.ToEnum<SampleEnum>(SampleEnum.None).ToString()); // None (fallback)

stringBuilder.AppendLine("\n=== IsValid ===");
stringBuilder.AppendLine(SampleEnum.First.IsValid().ToString());   // True
stringBuilder.AppendLine(((SampleEnum)100).IsValid().ToString());    // False

stringBuilder.AppendLine("\n=== GetValues / GetNames ===");
var values = EnumExtensions.GetValues<SampleEnum>();
var names = EnumExtensions.GetNames<SampleEnum>();
stringBuilder.AppendLine(string.Join(", ", values));
stringBuilder.AppendLine(string.Join(", ", names));

stringBuilder.AppendLine("\n=== GetAttribute ===");
var attr = SampleEnum.Second.GetAttribute<DescriptionAttribute>();
stringBuilder.AppendLine(attr?.Description);  // این مقدار دوم است

stringBuilder.AppendLine("\n=== Flags Tests ===");


var flags = FlagEnum.A.Set(FlagEnum.C);
stringBuilder.AppendLine(flags.ToString());           // A, C
stringBuilder.AppendLine(flags.Has(FlagEnum.A).ToString()); // True
stringBuilder.AppendLine(flags.Has(FlagEnum.B).ToString()); // False
flags = flags.Clear(FlagEnum.A);
stringBuilder.AppendLine(flags.ToString());           // C
File.WriteAllText("EnumExtensionsDemo.txt", stringBuilder.ToString());
#endregion


#region String
stringBuilder.Clear();
// Null/Empty/Whitespace
stringBuilder.AppendLine("\n--- Null/Empty/Whitespace ---");
stringBuilder.AppendLine($"\"\".IsEmpty() => {"".IsEmpty()}");
stringBuilder.AppendLine($"\"Hello\".HasValue() => {"Hello".HasValue()}");
stringBuilder.AppendLine($"null.OrDefault(\"X\") => {((string)null).OrDefault("X")}");
stringBuilder.AppendLine($"null.OrEmpty() => '{((string)null).OrEmpty()}'");

// Cleaning & Formatting
stringBuilder.AppendLine("\n--- Cleaning & Formatting ---");
stringBuilder.AppendLine($"\" A   B  \".CleanSpaces() => \"{" A   B  ".CleanSpaces()}\"");
stringBuilder.AppendLine($"\"aáà\".RemoveDiacritics() => \"{"aáà".RemoveDiacritics()}\"");

// Persian / Numbers
stringBuilder.AppendLine("\n--- Persian / Numbers ---");
stringBuilder.AppendLine($"\"۱۲۳۴\".ToEnglishNumbers() => \"{"۱۲۳۴".ToEnglishNumbers()}\"");
stringBuilder.AppendLine($"\"5678\".ToPersianNumbers() => \"{"5678".ToPersianNumbers()}\"");



string persian = "ضه";
string toEnglish = persian.ConvertLayout(KeyboardLayoutDirection.PersianToEnglish);
// خروجی: "hello"

string english = "hello";
string toPersian = english.ConvertLayout(KeyboardLayoutDirection.EnglishToPersian);

stringBuilder.AppendLine($"\"ضه\".ToFinglish() => \"{"ضه".ConvertLayout(CsharpGalexy.LibraryExtention.Models.KeyboardLayoutDirection.PersianToEnglish)}\"");
stringBuilder.AppendLine($"\"hello\".ToFinglish() => \"{"hello".ConvertLayout(CsharpGalexy.LibraryExtention.Models.KeyboardLayoutDirection.EnglishToPersian)}\"");
// Validation
stringBuilder.AppendLine("\n--- Validation ---");
stringBuilder.AppendLine($"\"test@test.com\".IsEmail() => {"test@test.com".IsEmail()}");
stringBuilder.AppendLine($"\"09123456789\".IsIranianMobile() => {"09123456789".IsIranianMobile()}");
stringBuilder.AppendLine($"\"0013520849\".IsNationalCode() => {"0013520849".IsNationalCode()}");
stringBuilder.AppendLine($"\"123.45\".IsNumeric() => {"123.45".IsNumeric()}");

// Text Processing
stringBuilder.AppendLine("\n--- Text Processing ---");
stringBuilder.AppendLine($"\"HelloWorld\".Truncate(5) => \"{"HelloWorld".Truncate(5)}\"");
stringBuilder.AppendLine($"\"Hello World!\".ToSlug() => \"{"Hello World!".ToSlug()}\"");
stringBuilder.AppendLine($"\"One two three\".WordCount() => {"One two three".WordCount()}");
stringBuilder.AppendLine($"\"Hi\".Repeat(3) => \"{"Hi".Repeat(3)}\"");

// Security & Sanitization
stringBuilder.AppendLine("\n--- Security & Sanitization ---");
stringBuilder.AppendLine($"\"<script>alert(1)</script>Hi\".SanitizeHtml() => \"{"<script>alert(1)</script>Hi".SanitizeHtml()}\"");
stringBuilder.AppendLine($"\"09123456789\".Mask() => \"{"09123456789".Mask()}\"");
stringBuilder.AppendLine($"\"Secret\".MaskAll() => \"{"Secret".MaskAll()}\"");

// Case & Capitalization
stringBuilder.AppendLine("\n--- Case & Capitalization ---");
stringBuilder.AppendLine($"\"hello\".FirstCharToUpper() => \"{"hello".FirstCharToUpper()}\"");
stringBuilder.AppendLine($"\"hello world\".ToTitleCase() => \"{"hello world".ToTitleCase()}\"");

stringBuilder.AppendLine("\n===== END TESTS =====");
stringBuilder.AppendLine("------------------");


// گرفتن پیش شماره هر استان
stringBuilder.AppendLine("تهران".GetProvincePhoneCode());           // 021
stringBuilder.AppendLine("خراسان رضوی".GetProvincePhoneCode());     // 051
stringBuilder.AppendLine("فارس".GetProvincePhoneCode());            // 071
stringBuilder.AppendLine("گیلان".GetProvincePhoneCode());           // 013
stringBuilder.AppendLine("کرمان".GetProvincePhoneCode());           // null → چون "کرمان" در لیست نیست (استان "کرمان" → مرکز: "کرمان" → کد: "034")

// برای دریافت کدپستی مرکز استان‌های ایران بر اساس نام استان 
stringBuilder.AppendLine("تهران".GetProvincePostalCode());     // 15957
stringBuilder.AppendLine("فارس".GetProvincePostalCode());      // 71967
stringBuilder.AppendLine("کرمان".GetProvincePostalCode());     // 76137
stringBuilder.AppendLine("یزد".GetProvincePostalCode());       // 89169


//گرفتن نام شهر مرکز استان
stringBuilder.AppendLine("خراسان رضوی".GetProvinceCapital());   // مشهد
stringBuilder.AppendLine("فارس".GetProvinceCapital());          // شیراز
stringBuilder.AppendLine("البرز".GetProvinceCapital());         // کرج
stringBuilder.AppendLine("لرستان".GetProvinceCapital());        // خرم‌آباد


//گرفتن کل استان ها
var provinces = ProvinceHelper.GetAllProvinces();
foreach (var province in provinces)
{
    Console.WriteLine(province);
}
// یا مرتب شده:
var sorted = ProvinceHelper.GetAllProvincesSorted();
foreach (var p in sorted)
{
    Console.WriteLine(p);
}


// تست اعتبارسنجی
File.WriteAllText("StringExtensionsDemo.txt", stringBuilder.ToString());
#endregion

#region Guid
stringBuilder.Clear();
var guid = Guid.NewGuid();

// تبدیل به رشته کوتاه
string shortStr = guid.ToShortString();
stringBuilder.AppendLine($"Short: {shortStr}");

// بازگردانی به Guid
Guid restored = shortStr.ToGuid();
stringBuilder.AppendLine($"Restored: {restored}");

// چک کردن خالی بودن
stringBuilder.AppendLine($"Is Empty: {guid.IsEmpty()}");

// رشته تمیز (بدون خط تیره)
stringBuilder.AppendLine($"Clean: {guid.ToCleanString()}");

// ایجاد Guid فرزند قطعی
Guid child = guid.DeriveGuid("ChildKey");
stringBuilder.AppendLine($"Child: {child}");

// تلاش برای تبدیل رشته به Guid
string input = "invalid-guid";
if (input.TryParseGuid(out Guid parsed))
{
    stringBuilder.AppendLine($"Parsed: {parsed}");
}
else
{
    stringBuilder.AppendLine("Parse failed.");
}
// تست روی Guid معمولی
Guid empty = Guid.Empty;
Guid valid = Guid.NewGuid();

stringBuilder.AppendLine(empty.OrDefault().ToString());        // → Guid.Empty
stringBuilder.AppendLine(valid.OrDefault().ToString());        // → خود valid GUID

Guid fallback = new("11111111-1111-1111-1111-111111111111");
stringBuilder.AppendLine(empty.OrDefault(fallback).ToString()); // → fallback

// تست روی Nullable Guid
Guid? nullGuid = null;
Guid? emptyGuid = Guid.Empty;
Guid? realGuid = valid;

stringBuilder.AppendLine(nullGuid.OrDefault().ToString());          // → Guid.Empty
stringBuilder.AppendLine(emptyGuid.OrDefault().ToString());         // → Guid.Empty
stringBuilder.AppendLine(realGuid.OrDefault().ToString());          // → valid GUID
stringBuilder.AppendLine(nullGuid.OrDefault(fallback).ToString());  // → fallback

// تست اعتبارسنجی
File.WriteAllText("StringExtensionsDemo.txt", stringBuilder.ToString());
#endregion

#region Object

Person person = new Person().NothingIfNull();
Console.WriteLine(person.Name); // ✅ امن! اگر null بود، یک Person جدید (با مقادیر پیش‌فرض) برمی‌گرده.
#endregion

#region TimeSpan
stringBuilder.Clear();
stringBuilder.AppendLine("--- TimeSpan Extensions Demo ---\n");


var ts = new TimeSpan(1, 2, 30, 45);


stringBuilder.AppendLine($"Original TimeSpan: {ts}");
stringBuilder.AppendLine($"Human Readable: {ts.ToHumanReadable()}");
stringBuilder.AppendLine($"Human Readable: {ts.ToHumanReadablePersian()}");
stringBuilder.AppendLine($"Short String: {ts.ToShortString()}");
stringBuilder.AppendLine($"Total Weeks: {ts.TotalWeeks()}");
stringBuilder.AppendLine($"Is Positive: {ts.IsPositive()}");
stringBuilder.AppendLine($"Is Negative: {TimeSpan.FromMinutes(-5).IsNegative()}");


stringBuilder.AppendLine($"Clamp (min 1h, max 2h): {TimeSpan.FromHours(3).Clamp(TimeSpan.FromHours(1), TimeSpan.FromHours(2))}");
stringBuilder.AppendLine($"Abs of -30m: {TimeSpan.FromMinutes(-30).Abs()}");


stringBuilder.AppendLine($"Rounded to nearest minute: {ts.RoundToNearestMinute()}");
stringBuilder.AppendLine($"Rounded to nearest hour: {ts.RoundToNearestHour()}");
stringBuilder.AppendLine($"Rounded to nearest day: {ts.RoundToNearestDay()}");


var ts1 = TimeSpan.FromHours(1);
var ts2 = TimeSpan.FromHours(2);
stringBuilder.AppendLine($"Percentage of 1h over 2h: {ts1.PercentageOf(ts2)}%");
stringBuilder.AppendLine($"Is 3h between 1h and 5h? {TimeSpan.FromHours(3).Between(TimeSpan.FromHours(1), TimeSpan.FromHours(5))}");


stringBuilder.AppendLine($"Multiply 2h by 2: {TimeSpan.FromHours(2).Multiply(2)}");
stringBuilder.AppendLine($"Divide 2h by 2: {TimeSpan.FromHours(2).Divide(2)}");


stringBuilder.AppendLine($"Truncate to minutes: {ts.TruncateToMinutes()}");
stringBuilder.AppendLine($"Truncate to hours: {ts.TruncateToHours()}");
stringBuilder.AppendLine($"Truncate to days: {ts.TruncateToDays()}");


stringBuilder.AppendLine($"Is Zero? {TimeSpan.Zero.IsZero()}");


var dict = ts.ToDictionary();
var dictPersian = ts.ToDictionaryPersian();
stringBuilder.AppendLine("ToDictionary:");
foreach (var kv in dict)
{
    stringBuilder.AppendLine($" {kv.Key}: {kv.Value}");
}
foreach (var kv in dictPersian)
{
    stringBuilder.AppendLine($" {kv.Key}: {kv.Value}");
}

stringBuilder.AppendLine("\n--- Demo Completed ---");
File.WriteAllText("TimeSpanExtensionsDemo.txt", stringBuilder.ToString());
#endregion

Console.Write("------------------");

Console.ReadKey();