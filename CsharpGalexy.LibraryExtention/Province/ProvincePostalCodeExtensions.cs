using System;
using System.Collections.Generic;





public static class ProvincePostalCodeExtensions
{
    private static readonly Dictionary<string, string> ProvincePostalCodes =
        new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "تهران", "15957" },
            { "اصفهان", "81647" },
            { "خراسان رضوی", "91735" },
            { "فارس", "71967" },
            { "آذربایجان شرقی", "51667" },
            { "خوزستان", "61011" },
            { "البرز", "31487" },
            { "قم", "37137" },
            { "یزد", "89169" },
            { "گیلان", "41997" },
            { "آذربایجان غربی", "57157" },
            { "سیستان و بلوچستان", "98137" },
            { "کردستان", "66157" },
            { "خراسان شمالی", "94177" },
            { "مازندران", "47157" },
            { "هرمزگان", "96157" },
            { "لرستان", "68137" },
            { "قزوین", "34137" },
            { "همدان", "65137" },
            { "کهگیلویه و بویراحمد", "75137" },
            { "اردبیل", "56137" },
            { "خراسان جنوبی", "97177" },
            { "زنجان", "45137" },
            { "سمنان", "38137" },
            { "ایلام", "69317" },
            { "گلستان", "49137" },
            { "بوشهر", "75137" },
            { "مرکزی", "38137" },
            { "چهارمحال و بختیاری", "88137" },
            { "کرمان", "76137" }
        };

    public static string? GetProvincePostalCode(this string provinceName)
    {
        if (string.IsNullOrWhiteSpace(provinceName))
            throw new ArgumentException("نام استان نمی‌تواند خالی باشد.", nameof(provinceName));

        return ProvincePostalCodes.TryGetValue(provinceName.Trim(), out var code) ? code : null;
    }
}