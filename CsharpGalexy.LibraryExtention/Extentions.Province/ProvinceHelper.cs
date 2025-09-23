using System;
using System.Collections.Generic;
using System.Linq;

public static class ProvinceHelper
{
    private static readonly string[] _provinces = new[]
    {
        "تهران",
        "اصفهان",
        "خراسان رضوی",
        "فارس",
        "آذربایجان شرقی",
        "خوزستان",
        "البرز",
        "قم",
        "یزد",
        "گیلان",
        "آذربایجان غربی",
        "سیستان و بلوچستان",
        "کردستان",
        "خراسان شمالی",
        "مازندران",
        "هرمزگان",
        "لرستان",
        "قزوین",
        "همدان",
        "کهگیلویه و بویراحمد",
        "اردبیل",
        "خراسان جنوبی",
        "زنجان",
        "سمنان",
        "ایلام",
        "گلستان",
        "بوشهر",
        "مرکزی",
        "چهارمحال و بختیاری",
        "کرمان"
    };

    /// <summary>
    /// دریافت لیست تمام استان‌های ایران
    /// </summary>
    /// <returns>آرایه‌ای از نام استان‌ها</returns>
    public static string[] GetAllProvinces()
    {
        return (string[])_provinces.Clone(); // برای جلوگیری از تغییر خارجی
    }

    /// <summary>
    /// [اختیاری] دریافت لیست مرتب شده بر اساس حروف الفبا
    /// </summary>
    public static string[] GetAllProvincesSorted()
    {
        return _provinces.OrderBy(x => x).ToArray();
    }
}