using CsharpGalexy.Console.Models;
using ExtentionLibrary.DateTimes;
using ExtentionLibrary.Enums;
using ExtentionLibrary.Strings;
using System.ComponentModel;
using System.Text;



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

#endregion
File.WriteAllText("EnumExtensionsDemo.txt", stringBuilder.ToString());

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
stringBuilder.AppendLine($"\"سلام\".ToFinglish() => \"{"سلام".ToFinglish()}\"");

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
File.WriteAllText("StringExtensionsDemo.txt", stringBuilder.ToString());
#endregion


Console.Write("------------------");
Console.ReadKey();