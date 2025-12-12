using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace CsharpGalaxy.LibraryExtension.Extensions.Enums;

/// <summary>
/// Extension methods for enums
/// </summary>
public static class EnumExtensions
{
    #region Get Description Attribute

    /// <summary>
    /// Gets the Description attribute of an enum value. If no description is found, returns the enum name.
    /// </summary>
    /// <param name="value">Enum value</param>
    /// <returns>Description or enum name</returns>
    public static string GetDescription(this Enum value)
    {
        var field = value.GetType().GetField(value.ToString());
        if (field == null) return value.ToString();

        var attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
        return attribute?.Description ?? value.ToString();
    }

    #endregion

    #region Get Display Name (DisplayName or Description)

    /// <summary>
    /// Gets DisplayName or Description of enum value. Falls back to ToString().
    /// Useful for UI presentation.
    /// </summary>
    /// <param name="value">Enum value</param>
    /// <returns>Display name</returns>
    public static string GetDisplayName(this Enum value)
    {
        var field = value.GetType().GetField(value.ToString());
        if (field == null) return value.ToString();

        // Try DisplayAttribute first
        var displayAttr = field.GetCustomAttribute<DisplayAttribute>();
        if (displayAttr != null && !string.IsNullOrEmpty(displayAttr.Name))
            return displayAttr.Name;

        // Then try DescriptionAttribute
        var descriptionAttr = field.GetCustomAttribute<DescriptionAttribute>();
        if (descriptionAttr != null && !string.IsNullOrEmpty(descriptionAttr.Description))
            return descriptionAttr.Description;

        return value.ToString();
    }

    #endregion

    #region Convert Enum to List of Key-Value Pairs (for dropdowns)

    /// <summary>
    /// Converts an enum type to a list of KeyValuePair for use in dropdowns
    /// </summary>
    /// <typeparam name="T">Enum type</typeparam>
    /// <returns>List of { Value, Text }</returns>
    public static IEnumerable<KeyValuePair<int, string>> ToList<T>() where T : Enum
    {
        return Enum.GetValues(typeof(T)).Cast<T>().Select(e => new KeyValuePair<int, string>(
            Convert.ToInt32(e),
            e.GetDisplayName()
        ));
    }

    /// <summary>
    /// Converts enum to list with Description or name (ValueTuple version)
    /// </summary>
    public static IEnumerable<(int Value, string Text)> ToSelectList<T>() where T : Enum
    {
        return Enum.GetValues(typeof(T))
                   .Cast<T>()
                   .Select(e => (Value: Convert.ToInt32(e), Text: e.GetDisplayName()));
    }

    #endregion

    #region Safe String to Enum Conversion

    /// <summary>
    /// Safely parses a string to enum. Returns default value if failed.
    /// </summary>
    /// <typeparam name="T">Enum type</typeparam>
    /// <param name="value">String value</param>
    /// <param name="ignoreCase">Whether to ignore case</param>
    /// <returns>Enum value or default</returns>
    public static T ToEnum<T>(this string value, bool ignoreCase = true) where T : struct, IConvertible
    {
        if (string.IsNullOrWhiteSpace(value))
            return default;

        return Enum.TryParse(value, ignoreCase, out T result) ? result : default;
    }

    /// <summary>
    /// Safely parses string to enum with fallback
    /// </summary>
    public static T ToEnum<T>(this string value, T defaultValue, bool ignoreCase = true) where T : struct, IConvertible
    {
        return Enum.TryParse(value, ignoreCase, out T result) ? result : defaultValue;
    }

    #endregion

    #region Check if Enum Value is Valid

    /// <summary>
    /// Checks if the enum value is defined in the enum type
    /// </summary>
    /// <typeparam name="T">Enum type</typeparam>
    /// <param name="value">Value to check</param>
    /// <returns>True if valid</returns>
    public static bool IsValid<T>(this T value) where T : Enum
    {
        return Enum.IsDefined(typeof(T), value);
    }

    #endregion

    #region Get Enum Values and Names

    /// <summary>
    /// Gets all enum values as array
    /// </summary>
    public static T[] GetValues<T>() where T : Enum
    {
        return (T[])Enum.GetValues(typeof(T));
    }

    /// <summary>
    /// Gets all enum names as array
    /// </summary>
    public static string[] GetNames<T>() where T : Enum
    {
        return Enum.GetNames(typeof(T));
    }

    #endregion

    #region Get Attribute from Enum

    /// <summary>
    /// Gets a custom attribute from an enum value
    /// </summary>
    /// <typeparam name="TAttribute">Attribute type</typeparam>
    /// <param name="value">Enum value</param>
    /// <returns>Attribute instance or null</returns>
    public static TAttribute GetAttribute<TAttribute>(this Enum value) where TAttribute : Attribute
    {
        var field = value.GetType().GetField(value.ToString());
        return field == null ? null : (TAttribute)Attribute.GetCustomAttribute(field, typeof(TAttribute));
    }

    #endregion

    #region Has Flag (for Flags enums)

    /// <summary>
    /// Checks if a flag is set (works with [Flags] enums)
    /// </summary>
    /// <param name="enumValue">The enum value</param>
    /// <param name="flag">The flag to check</param>
    /// <returns>True if flag is set</returns>
    public static bool Has(this Enum enumValue, Enum flag)
    {
        if (enumValue == null || flag == null)
            return false;

        var enumType = enumValue.GetType();
        if (!enumType.IsDefined(typeof(FlagsAttribute), false))
            throw new ArgumentException("Enum is not marked with [Flags] attribute.");

        var enumValueAsULong = Convert.ToUInt64(enumValue);
        var flagAsULong = Convert.ToUInt64(flag);

        return (enumValueAsULong & flagAsULong) == flagAsULong;
    }

    /// <summary>
    /// Adds a flag to an enum (for [Flags])
    /// </summary>
    public static T Set<T>(this Enum enumValue, T flag) where T : Enum
    {
        var enumType = enumValue.GetType();
        if (!enumType.IsDefined(typeof(FlagsAttribute), false))
            throw new ArgumentException("Enum is not marked with [Flags] attribute.");

        var result = Convert.ToUInt64(enumValue) | Convert.ToUInt64(flag);
        return (T)Enum.ToObject(typeof(T), result);
    }

    /// <summary>
    /// Removes a flag from an enum (for [Flags])
    /// </summary>
    public static T Clear<T>(this Enum enumValue, T flag) where T : Enum
    {
        var enumType = enumValue.GetType();
        if (!enumType.IsDefined(typeof(FlagsAttribute), false))
            throw new ArgumentException("Enum is not marked with [Flags] attribute.");

        var result = Convert.ToUInt64(enumValue) & ~Convert.ToUInt64(flag);
        return (T)Enum.ToObject(typeof(T), result);
    }

    #endregion

    #region Additional Helpers (Legacy/Alternate Implementations)

    /// <summary>
    /// Gets the name of the enum value.
    /// </summary>
    public static string AsName(this Enum @this)
    {
        Type type = @this.GetType();
        return Enum.GetName(type, @this);
    }

    /// <summary>
    /// Gets the Description attribute or falls back to name.
    /// </summary>
    public static string AsDescription(this Enum @this)
    {
        Type type = @this.GetType();
        string name = Enum.GetName(type, @this);

        MemberInfo member = type
            .GetMembers()
            .FirstOrDefault(w => w.Name == name);

        DescriptionAttribute attribute = member != null
            ? member
                .GetCustomAttributes(true)
                .FirstOrDefault(w => w.GetType() == typeof(DescriptionAttribute)) as DescriptionAttribute
            : null;

        return attribute != null ? attribute.Description : name;
    }

    /// <summary>
    /// Converts enum to its underlying integer value.
    /// </summary>
    public static int AsValue(this Enum @this)
    {
        Type type = @this.GetType();
        return (int)Enum.Parse(type, @this.ToString());
    }

    /// <summary>
    /// Converts an integer to its enum string representation.
    /// </summary>
    public static string ToEnumString<TEnum>(this int enumValue)
    {
        var enumString = enumValue.ToString();
        if (!Enum.IsDefined(typeof(TEnum), enumValue)) return enumString;

        enumString = ((TEnum)Enum.ToObject(typeof(TEnum), enumValue)).ToString();
        return enumString;
    }

    /// <summary>
    /// Parses string to enum (unsafe version - throws exception if invalid).
    /// </summary>
    public static T ToEnum<T>(this string enumString) => (T)Enum.Parse(typeof(T), enumString);

    /// <summary>
    /// Joins all enum values into a comma-separated string.
    /// </summary>
    public static string EnumJoin<T>()
    {
        var values = (T[])Enum.GetValues(typeof(T));
        return string.Join(",", values.Select(x => x));
    }

    /// <summary>
    /// Checks if enum has a value by integer.
    /// </summary>
    public static bool Has(this Enum @this, int value)
    {
        Type type = @this.GetType();
        return Enum.IsDefined(type, value);
    }

    /// <summary>
    /// Checks if enum has a value by string name.
    /// </summary>
    public static bool Has(this Enum @this, string value)
    {
        Type type = @this.GetType();
        return Enum.IsDefined(type, value);
    }

    /// <summary>
    /// Converts enum to dictionary of int-value to string-name.
    /// </summary>
    public static Dictionary<int, string> ToDict(this Enum theEnum)
    {
        return Enum.GetValues(theEnum.GetType())
                   .Cast<int>()
                   .ToDictionary(enumValue => enumValue, enumValue => enumValue.ToString());
    }

    // تبدیل به int
    public static int ToInt<TEnum>(this TEnum value) where TEnum : Enum
    {
        return Convert.ToInt32(value);
    }

    // تبدیل به string (نام enum)
    public static string ToStringValue<TEnum>(this TEnum value) where TEnum : Enum
    {
        return value.ToString();
    }




    #endregion
}
