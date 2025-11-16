namespace CsharpGalaxy.LibraryExtension.FakeDataPersian.Helpers;

using CsharpGalaxy.LibraryExtension.FakeDataPersian.Attributes;
using CsharpGalaxy.LibraryExtension.FakeDataPersian.Generators;
using System.Reflection;

/// <summary>
/// کلاس برای تولید داده‌های تصادفی براساس Attribute‌های کاستوم
/// </summary>
public static class FakeDataSeeder
{
    /// <summary>
    /// یک نمونه را براساس Attribute‌های آن پر می‌کند
    /// </summary>
    public static T Seed<T>() where T : new()
    {
        var entity = new T();
        Seed(entity);
        return entity;
    }

    /// <summary>
    /// یک لیست از نمونه‌ها را براساس Attribute‌های آن‌ها ایجاد می‌کند
    /// </summary>
    public static List<T> SeedList<T>(int count) where T : new()
    {
        var list = new List<T>();

        for (int i = 0; i < count; i++)
        {
            list.Add(Seed<T>());
        }

        return list;
    }

    /// <summary>
    /// یک نمونه را براساس Attribute‌های آن پر می‌کند
    /// </summary>
    private static void Seed<T>(T entity)
    {
        var props = typeof(T).GetProperties();
        var random = new Random();

        foreach (var prop in props)
        {
            if (!prop.CanWrite)
                continue;

            // اگر Ignore قرار داشت، از این property رد شو
            if (prop.GetCustomAttribute<IgnoreAttribute>() != null)
                continue;

            // اگر Constant قرار داشت، از آن استفاده کن
            var constantAttr = prop.GetCustomAttribute<ConstantAttribute>();
            if (constantAttr != null)
            {
                prop.SetValue(entity, constantAttr.Value);
                continue;
            }

            // بررسی Attribute‌های مختلف
            if (prop.GetCustomAttribute<FirstNameAttribute>() != null)
                prop.SetValue(entity, PersianNameGenerator.FirstName());

            else if (prop.GetCustomAttribute<LastNameAttribute>() != null)
                prop.SetValue(entity, PersianNameGenerator.LastName());

            else if (prop.GetCustomAttribute<FullNameAttribute>() != null)
                prop.SetValue(entity, PersianNameGenerator.FullName());

            else if (prop.GetCustomAttribute<EmailAttribute>() != null)
                prop.SetValue(entity, PersianTextGenerator.Email());

            else if (prop.GetCustomAttribute<MobileAttribute>() != null)
                prop.SetValue(entity, IranianMobileGenerator.Mobile());

            else if (prop.GetCustomAttribute<UsernameAttribute>() != null)
                prop.SetValue(entity, PersianTextGenerator.Username());

            else if (prop.GetCustomAttribute<NationalCodeAttribute>() != null)
                prop.SetValue(entity, IranianNationalCodeGenerator.MelliCode());

            else if (prop.GetCustomAttribute<AddressAttribute>() != null)
                prop.SetValue(entity, PersianAddressGenerator.FullAddress());

            else if (prop.GetCustomAttribute<CityAttribute>() != null)
                prop.SetValue(entity, PersianAddressGenerator.City());

            else if (prop.GetCustomAttribute<ProvinceAttribute>() != null)
                prop.SetValue(entity, PersianAddressGenerator.Province());

            else if (prop.GetCustomAttribute<WordAttribute>() != null)
                prop.SetValue(entity, PersianTextGenerator.Word());

            else if (prop.GetCustomAttribute<SentenceAttribute>() != null)
                prop.SetValue(entity, PersianTextGenerator.Sentence());

            else if (prop.GetCustomAttribute<CompanyNameAttribute>() != null)
                prop.SetValue(entity, BusinessDataGenerator.CompanyName());

            else if (prop.GetCustomAttribute<JobTitleAttribute>() != null)
                prop.SetValue(entity, BusinessDataGenerator.JobTitle());

            else if (prop.GetCustomAttribute<IbanAttribute>() != null)
                prop.SetValue(entity, BankingMoneyGenerator.Sheba());

            else if (prop.GetCustomAttribute<CardNumberAttribute>() != null)
                prop.SetValue(entity, BankingMoneyGenerator.CardNumber());

            else if (prop.GetCustomAttribute<GuidAttribute>() != null)
                prop.SetValue(entity, InternetCryptoGenerator.GuidString());

            else if (prop.GetCustomAttribute<DateTimeAttribute>() != null)
                prop.SetValue(entity, DateTime.Now.AddDays(-random.Next(1, 365)));

            else if (prop.GetCustomAttribute<BooleanAttribute>() != null)
                prop.SetValue(entity, random.Next(2) == 0);

            else if (prop.GetCustomAttribute<StatusAttribute>() != null)
                prop.SetValue(entity, BusinessDataGenerator.ProjectStatus());

            // Persian Date Attributes
            else if (prop.GetCustomAttribute<PersianDateAttribute>() != null)
                prop.SetValue(entity, PersianDateGenerator.GetRandomPersianDateTime());

            else if (prop.GetCustomAttribute<PersianDayNameAttribute>() != null)
                prop.SetValue(entity, PersianDateGenerator.GetDayNameFarsi());

            else if (prop.GetCustomAttribute<PersianMonthNameAttribute>() != null)
                prop.SetValue(entity, PersianDateGenerator.GetRandomMonthNameFarsi());

            else if (prop.GetCustomAttribute<PersianYearAttribute>() != null)
            {
                var yearAttr = prop.GetCustomAttribute<PersianYearAttribute>();
                prop.SetValue(entity, PersianDateGenerator.GetRandomShamsiYear(yearAttr.MinYear, yearAttr.MaxYear));
            }

            else if (prop.GetCustomAttribute<PersianDateRangeAttribute>() != null)
            {
                var rangeAttr = prop.GetCustomAttribute<PersianDateRangeAttribute>();
                prop.SetValue(entity, PersianDateGenerator.GetRandomDateBetween(rangeAttr.StartDate, rangeAttr.EndDate));
            }

            else if (prop.GetCustomAttribute<EnumAttribute>() != null)
            {
                var enumAttr = prop.GetCustomAttribute<EnumAttribute>();
                if (enumAttr.AllowedValues != null && enumAttr.AllowedValues.Length > 0)
                {
                    // اگر مقادیر محدود داشت، از میان آن‌ها انتخاب کن
                    var randomIndex = random.Next(enumAttr.AllowedValues.Length);
                    prop.SetValue(entity, enumAttr.AllowedValues[randomIndex]);
                }
                else
                {
                    // تمام مقادیر enum را استفاده کن
                    prop.SetValue(entity, EnumGenerator.GetRandomEnumValue(enumAttr.EnumType));
                }
            }

            // بررسی Type‌های مختلف
            else if (prop.PropertyType.IsEnum)
                prop.SetValue(entity, EnumGenerator.GetRandomEnumValue(prop.PropertyType));

            else if (prop.PropertyType == typeof(Guid))
                prop.SetValue(entity, System.Guid.NewGuid());

            else if (prop.PropertyType == typeof(int))
                prop.SetValue(entity, random.Next(1, 10000));

            else if (prop.PropertyType == typeof(int?))
                prop.SetValue(entity, random.Next(1, 10000));

            else if (prop.PropertyType == typeof(long))
                prop.SetValue(entity, (long)random.Next(1, int.MaxValue));

            else if (prop.PropertyType == typeof(long?))
                prop.SetValue(entity, (long)random.Next(1, int.MaxValue));

            else if (prop.PropertyType == typeof(decimal))
                prop.SetValue(entity, Convert.ToDecimal(random.Next(100, 1000000) / 100.0));

            else if (prop.PropertyType == typeof(decimal?))
                prop.SetValue(entity, Convert.ToDecimal(random.Next(100, 1000000) / 100.0));

            else if (prop.PropertyType == typeof(double))
                prop.SetValue(entity, random.NextDouble() * 10000);

            else if (prop.PropertyType == typeof(double?))
                prop.SetValue(entity, random.NextDouble() * 10000);

            else if (prop.PropertyType == typeof(bool))
                prop.SetValue(entity, random.Next(2) == 0);

            else if (prop.PropertyType == typeof(bool?))
                prop.SetValue(entity, random.Next(2) == 0);

            else if (prop.PropertyType == typeof(DateTime))
                prop.SetValue(entity, DateTime.Now.AddDays(-random.Next(1, 365)));

            else if (prop.PropertyType == typeof(DateTime?))
                prop.SetValue(entity, DateTime.Now.AddDays(-random.Next(1, 365)));

            else if (prop.PropertyType == typeof(string))
                prop.SetValue(entity, PersianTextGenerator.Word());
        }
    }
}

