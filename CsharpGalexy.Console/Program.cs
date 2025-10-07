using CsharpGalexy.Console.Models;
using CsharpGalexy.LibraryExtention.Extentions.Province;
using CsharpGalexy.LibraryExtention.Models;
using System.ComponentModel;
using System.Text;
using YourNamespace.Helpers;
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


//#region Enum
//stringBuilder.Clear();
//stringBuilder.AppendLine("=== GetDescription ===");
//stringBuilder.AppendLine(SampleEnum.First.GetDescription());   // این مقدار اول است
//stringBuilder.AppendLine(SampleEnum.Second.GetDescription());  // این مقدار دوم است
//stringBuilder.AppendLine(SampleEnum.Third.GetDescription());   // Third

//stringBuilder.AppendLine("\n=== GetDisplayName ===");
//stringBuilder.AppendLine(SampleEnum.First.GetDisplayName());   // این مقدار اول است
//stringBuilder.AppendLine(SampleEnum.Second.GetDisplayName());  // مقدار دوم (DisplayName بر Description ارجحیت دارد)
//stringBuilder.AppendLine(SampleEnum.Third.GetDisplayName());   // Third

//stringBuilder.AppendLine("\n=== ToList / ToSelectList ===");
//var list = EnumExtensions.ToList<SampleEnum>();
//foreach (var kv in list)
//stringBuilder.AppendLine($"{kv.Key} - {kv.Value}");

//var selectList = EnumExtensions.ToSelectList<SampleEnum>();
//foreach (var item in selectList)
//stringBuilder.AppendLine($"{item.Value} - {item.Text}");

//stringBuilder.AppendLine("\n=== ToEnum ===");
//string val1 = "Second";
//string val2 = "second";
//string val3 = "Unknown";

//stringBuilder.AppendLine(val1.ToEnum<SampleEnum>().ToString());           // Second
//stringBuilder.AppendLine(val2.ToEnum<SampleEnum>().ToString());           // Second (ignoreCase = true)
//stringBuilder.AppendLine(val3.ToEnum<SampleEnum>(SampleEnum.None).ToString()); // None (fallback)

//stringBuilder.AppendLine("\n=== IsValid ===");
//stringBuilder.AppendLine(SampleEnum.First.IsValid().ToString());   // True
//stringBuilder.AppendLine(((SampleEnum)100).IsValid().ToString());    // False

//stringBuilder.AppendLine("\n=== GetValues / GetNames ===");
//var values = EnumExtensions.GetValues<SampleEnum>();
//var names = EnumExtensions.GetNames<SampleEnum>();
//stringBuilder.AppendLine(string.Join(", ", values));
//stringBuilder.AppendLine(string.Join(", ", names));

//stringBuilder.AppendLine("\n=== GetAttribute ===");
//var attr = SampleEnum.Second.GetAttribute<DescriptionAttribute>();
//stringBuilder.AppendLine(attr?.Description);  // این مقدار دوم است

//stringBuilder.AppendLine("\n=== Flags Tests ===");


//var flags = FlagEnum.A.Set(FlagEnum.C);
//stringBuilder.AppendLine(flags.ToString());           // A, C
//stringBuilder.AppendLine(flags.Has(FlagEnum.A).ToString()); // True
//stringBuilder.AppendLine(flags.Has(FlagEnum.B).ToString()); // False
//flags = flags.Clear(FlagEnum.A);
//stringBuilder.AppendLine(flags.ToString());           // C
//File.WriteAllText("EnumExtensionsDemo.txt", stringBuilder.ToString());
//#endregion


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


        // null → چون "کرمان" در لیست نیست (استان "کرمان" → مرکز: "کرمان" → کد: "034")




var aaa= ProvincePostalCodeHelper.GetAllPostalCodes();

//گرفتن کل استان ها
//var provinces = ProvinceHelper.GetAllProvinces();
//foreach (var province in provinces)
//{
//    stringBuilder.AppendLine(province);
//}
// یا مرتب شده:
//var sorted = ProvinceHelper.GetAllProvincesSorted();
//foreach (var p in sorted)
//{
//    stringBuilder.AppendLine(p);
//}


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
stringBuilder.AppendLine(person.Name); // ✅ امن! اگر null بود، یک Person جدید (با مقادیر پیش‌فرض) برمی‌گرده.
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


#region CountryDialCodeHelper
stringBuilder.Clear();

stringBuilder.AppendLine("🧪 شروع تست‌های CountryDialCodeHelper...\n");

try
{
    Test_GetPersianCountryByDialCode(stringBuilder);
    Test_GetEnglishCountryByDialCode(stringBuilder);
    Test_GetDialCodeByPersianCountry(stringBuilder);
    Test_GetDialCodeByEnglishCountry(stringBuilder);
    Test_GetAllCountriesSortedByDialCode(stringBuilder);
    Test_CaseInsensitiveSearch(stringBuilder);
    Test_InvalidInputs(stringBuilder);

    stringBuilder.AppendLine("✅ تمام تست‌ها با موفقیت انجام شدند!");
}
catch (Exception ex)
{
    stringBuilder.AppendLine($"❌ خطا در تست: {ex.Message}");
    stringBuilder.AppendLine(ex.StackTrace);
}

stringBuilder.AppendLine("\nبرای خروج کلیدی را فشار دهید...");
File.WriteAllText("CountryDialCodeHelper.txt", stringBuilder.ToString());


static void Test_GetPersianCountryByDialCode(in StringBuilder stringBuilder)
{
    stringBuilder.AppendLine("• تست GetPersianCountryByDialCode...");
    var result = CountryDialCodeHelper.GetPersianCountryByDialCode("+1");
    if (result != "آمریکا")
        throw new Exception("نتیجه مورد انتظار 'آمریکا' نیست.");
    stringBuilder.AppendLine("  ✔️ OK");
}

static void Test_GetEnglishCountryByDialCode(in StringBuilder stringBuilder)
{
    stringBuilder.AppendLine("• تست GetEnglishCountryByDialCode...");
    var result = CountryDialCodeHelper.GetEnglishCountryByDialCode("+1");
    if (result != "United States")
        throw new Exception("نتیجه مورد انتظار 'United States' نیست.");
    stringBuilder.AppendLine("  ✔️ OK");
}

static void Test_GetDialCodeByPersianCountry(in StringBuilder stringBuilder)
{
    stringBuilder.AppendLine("• تست GetDialCodeByPersianCountry...");
    var result = CountryDialCodeHelper.GetDialCodeByPersianCountry("آمریکا");
    if (result != "+1")
        throw new Exception("کد تلفن آمریکا باید '+1' باشد.");
    stringBuilder.AppendLine("  ✔️ OK");
}

static void Test_GetDialCodeByEnglishCountry(in StringBuilder stringBuilder)
{
    stringBuilder.AppendLine("• تست GetDialCodeByEnglishCountry...");
    var result = CountryDialCodeHelper.GetDialCodeByEnglishCountry("Canada");
    if (result != "+1")
        throw new Exception("کد تلفن کانادا باید '+1' باشد.");
    stringBuilder.AppendLine("  ✔️ OK");
}

static void Test_GetAllCountriesSortedByDialCode(in StringBuilder stringBuilder)
{
    stringBuilder.AppendLine("• تست GetAllCountriesSortedByDialCode...");
    var sorted = CountryDialCodeHelper.GetAllCountriesSortedByDialCode();
    if (sorted.Count == 0)
        throw new Exception("لیست کشورها خالی است.");

    // بررسی اینکه اولین کشور کد +1 دارد
    if (sorted[0].DialCode != "+1")
        throw new Exception("اولین کشور باید کد +1 داشته باشد.");

    stringBuilder.AppendLine($"  ✔️ OK (تعداد کشورها: {sorted.Count})");
}

static void Test_CaseInsensitiveSearch(in StringBuilder stringBuilder)
{
    stringBuilder.AppendLine("• تست جستجوی بدون حساسیت به بزرگ/کوچکی...");
    var code1 = CountryDialCodeHelper.GetDialCodeByEnglishCountry("canada");
    var code2 = CountryDialCodeHelper.GetDialCodeByEnglishCountry("CANADA");
    var code3 = CountryDialCodeHelper.GetDialCodeByPersianCountry("آمریکا"); // فارسی همیشه case-sensitive نیست ولی تست می‌کنیم

    if (code1 != "+1" || code2 != "+1")
        throw new Exception("جستجوی بدون حساسیت به حروف کار نمی‌کند.");

    stringBuilder.AppendLine("  ✔️ OK");
}

static void Test_InvalidInputs(in StringBuilder stringBuilder)
{
    stringBuilder.AppendLine("• تست ورودی‌های نامعتبر...");

    // جستجوی کد تلفن ناموجود
    var unknown = CountryDialCodeHelper.GetPersianCountryByDialCode("+999");
    if (unknown != null)
        throw new Exception("کد تلفن ناموجود نباید نتیجه‌ای برگرداند.");

    // جستجوی نام کشور ناموجود
    var unknownCode = CountryDialCodeHelper.GetDialCodeByEnglishCountry("Atlantis");
    if (unknownCode != null)
        throw new Exception("نام کشور ناموجود نباید نتیجه‌ای برگرداند.");

    // ورودی خالی
    var empty = CountryDialCodeHelper.GetDialCodeByEnglishCountry("");
    if (empty != null)
        throw new Exception("ورودی خالی نباید نتیجه‌ای برگرداند.");

    stringBuilder.AppendLine("  ✔️ OK");
}
#endregion
Console.ReadKey();



Console.ReadKey();

