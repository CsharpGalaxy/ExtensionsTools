using System;
using System.Collections.Generic;

public static class ProvinceCapitalExtensions
{
    private static readonly Dictionary<string, string> ProvinceCapitals =
        new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "تهران", "تهران" },
            { "اصفهان", "اصفهان" },
            { "خراسان رضوی", "مشهد" },
            { "فارس", "شیراز" },
            { "آذربایجان شرقی", "تبریز" },
            { "خوزستان", "اهواز" },
            { "البرز", "کرج" },
            { "قم", "قم" },
            { "یزد", "یزد" },
            { "گیلان", "رشت" },
            { "آذربایجان غربی", "ارومیه" },
            { "سیستان و بلوچستان", "زاهدان" },
            { "کردستان", "سنندج" },
            { "خراسان شمالی", "بجنورد" },
            { "مازندران", "ساری" },
            { "هرمزگان", "بندرعباس" },
            { "لرستان", "خرم‌آباد" },
            { "قزوین", "قزوین" },
            { "همدان", "همدان" },
            { "کهگیلویه و بویراحمد", "یاسوج" },
            { "اردبیل", "اردبیل" },
            { "خراسان جنوبی", "بیرجند" },
            { "زنجان", "زنجان" },
            { "سمنان", "سمنان" },
            { "ایلام", "ایلام" },
            { "گلستان", "گرگان" },
            { "بوشهر", "بوشهر" },
            { "مرکزی", "اراک" },
            { "چهارمحال و بختیاری", "شهرکرد" },
            { "کرمان", "کرمان" }
        };

    public static string? GetProvinceCapital(this string provinceName)
    {
        if (string.IsNullOrWhiteSpace(provinceName))
            throw new ArgumentException("نام استان نمی‌تواند خالی باشد.", nameof(provinceName));

        return ProvinceCapitals.TryGetValue(provinceName.Trim(), out var capital) ? capital : null;
    }
}