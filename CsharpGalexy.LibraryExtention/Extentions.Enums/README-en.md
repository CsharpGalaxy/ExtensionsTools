## 🔍 **Get Description and Display (Description / Display)**

| Method             | Purpose                                                                                                                                                         |
| ------------------ | --------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| `GetDescription()` | Retrieves the `DescriptionAttribute` value of an enum member. If no `Description` attribute is found, returns the enum member's name.                           |
| `GetDisplayName()` | Retrieves `DisplayAttribute.Name` (if present); otherwise tries `DescriptionAttribute.Description`. Falls back to `ToString()` if neither attribute is present. |

**Notes:** `GetDisplayName()` checks `DisplayAttribute` first, then `DescriptionAttribute`. If the enum field cannot be found via reflection, the method returns `ToString()`.

---

## 🔽 **Convert enum to list for Dropdown / Selects**

| Method                             | Purpose                                                                                                                            |
| ---------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------- |
| `ToList<T>() where T : Enum`       | Converts the enum type into `IEnumerable<KeyValuePair<int, string>>` for dropdowns (key = numeric value, value = display text).    |
| `ToSelectList<T>() where T : Enum` | Tuple-style version returning `IEnumerable<(int Value, string Text)>` with numeric value and display text (uses `GetDisplayName`). |

**Tip:** Display text for each member is produced by `GetDisplayName()` so `DisplayAttribute` / `DescriptionAttribute` are honored.

---

## 🔁 **Safe String to Enum Conversion**

| Method                                                                                                | Purpose                                                                                                           |
| ----------------------------------------------------------------------------------------------------- | ----------------------------------------------------------------------------------------------------------------- |
| `ToEnum<T>(this string value, bool ignoreCase = true) where T : struct, IConvertible`                 | Attempts to parse the string to enum `T`. If the input is null/whitespace or parsing fails, returns `default(T)`. |
| `ToEnum<T>(this string value, T defaultValue, bool ignoreCase = true) where T : struct, IConvertible` | Attempts to parse the string to enum `T`. If parsing fails, returns the supplied `defaultValue`.                  |

**Notes:** These methods use `Enum.TryParse` and do not throw exceptions for invalid input — they are safe for user-provided strings. The generic constraint `where T : struct, IConvertible` is used because `Enum` cannot be used directly as a generic constraint on all target frameworks.

---

## ✅ **Enum Value Validation**

| Method                                    | Purpose                                                                |
| ----------------------------------------- | ---------------------------------------------------------------------- |
| `IsValid<T>(this T value) where T : Enum` | Checks whether `value` is defined in the enum type (`Enum.IsDefined`). |

**Note:** Useful to validate values before UI display or processing.

---

## 📋 **Get Enum Values and Names**

| Method                          | Purpose                                                 |
| ------------------------------- | ------------------------------------------------------- |
| `GetValues<T>() where T : Enum` | Returns an array of all enum values (`T[]`).            |
| `GetNames<T>() where T : Enum`  | Returns an array of all enum member names (`string[]`). |

**Note:** These are thin wrappers around `Enum.GetValues` and `Enum.GetNames` — handy for building menus and lists.

---

## 🧩 **Get Attribute from Enum Member**

| Method                                                                   | Purpose                                                                                                                       |
| ------------------------------------------------------------------------ | ----------------------------------------------------------------------------------------------------------------------------- |
| `GetAttribute<TAttribute>(this Enum value) where TAttribute : Attribute` | Retrieves a custom attribute (e.g. `DescriptionAttribute`, `DisplayAttribute`) from an enum member, or `null` if not present. |

**Example:**

```csharp
var descAttr = myEnumValue.GetAttribute<DescriptionAttribute>();
```

---

## 🚩 **Working with [Flags] (bit flags)**

| Method                                                 | Purpose                                                                                         |
| ------------------------------------------------------ | ----------------------------------------------------------------------------------------------- |
| `Has(this Enum enumValue, Enum flag)`                  | Checks whether the specified `flag` is set on `enumValue` (for enums annotated with `[Flags]`). |
| `Set<T>(this Enum enumValue, T flag) where T : Enum`   | Adds a flag to `enumValue` and returns the resulting enum (for `[Flags]`).                      |
| `Clear<T>(this Enum enumValue, T flag) where T : Enum` | Removes a flag from `enumValue` and returns the resulting enum (for `[Flags]`).                 |

**Important notes:**

* If `enumValue` or `flag` is `null`, `Has(...)` returns `false`.
* If the enum type is **not** decorated with `[Flags]`, `Has/Set/Clear` throw an `ArgumentException`.
* Flag operations are performed by converting to `UInt64` and doing bitwise operations so they work across different underlying enum integral types.

---

## 🔧 **Helpers and Additional Implementations (Legacy / Alternate Implementations)**

| Method                                    | Purpose                                                                                                  |
| ----------------------------------------- | -------------------------------------------------------------------------------------------------------- |
| `AsName(this Enum @this)`                 | Returns the enum member name (equivalent to `Enum.GetName`).                                             |
| `AsDescription(this Enum @this)`          | Returns `DescriptionAttribute` if present; otherwise returns the member name.                            |
| `AsValue(this Enum @this)`                | Converts the enum to its underlying integer value (`int`).                                               |
| `ToEnumString<TEnum>(this int enumValue)` | Gets the enum member name for the numeric value; if value is not defined, returns the numeric string.    |
| `ToEnum<T>(this string enumString)`       | **Unsafe version**: parses string to enum using `Enum.Parse` — will throw an exception if parsing fails. |
| `EnumJoin<T>()`                           | Joins all enum members into a comma-separated string.                                                    |
| `Has(this Enum @this, int value)`         | Checks whether an integer value is defined in the enum (`Enum.IsDefined`).                               |
| `Has(this Enum @this, string value)`      | Checks whether a string name is defined in the enum (`Enum.IsDefined`).                                  |
| `ToDict(this Enum theEnum)`               | Converts enum members to `Dictionary<int, string>`: key = numeric value, value = member name.            |

**Warnings / Technical Notes:**

* `ToEnum<T>(enumString)` is **unsafe** and will throw an exception if the input is invalid — prefer the safe `ToEnum` (TryParse-based) versions unless you are certain of the input.
* `AsValue()` and `ToDict()` assume conversion to `int` is appropriate; if your enum has a different underlying type (e.g. `byte`, `long`) the behavior might not match expectations and you may need to adjust the implementation.
* `ToDict()` uses `Cast<int>()`; for enums whose underlying type is not `int`, consider changing the cast or implementation.

---
