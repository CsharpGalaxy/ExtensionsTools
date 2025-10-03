

## 🔍 **Null / Empty / Whitespace Checks**
| Method | Purpose |
|--------|--------|
| `IsEmpty()` | Returns `true` if string is `null`, empty, or whitespace |
| `HasValue()` | Returns `true` if string has actual content (opposite of `IsEmpty`) |
| `OrDefault(defaultValue)` | Returns the string if valid; otherwise returns the provided default |
| `OrEmpty()` | Returns an empty string if input is `null` |

---

## 🧹 **Cleaning & Formatting**
| Method | Purpose |
|--------|--------|
| `TruncateMore(maxLength)` | Truncates to max chars and appends `"..."` |
| `TruncateWords(maxWords)` | Truncates to max words and appends `"..."` |
| `RemoveWhitespace()` | Removes all spaces, tabs, newlines, and carriage returns |
| `CleanSpaces()` | Normalizes multiple spaces into single space and trims |
| `RemoveDiacritics()` | Strips accents (e.g., `à`, `ñ`) and returns ASCII equivalent |

---

## 🇮🇷 **Persian / Finglish / Number Conversion**
| Method | Purpose |
|--------|--------|
| `ToEnglishNumbers()` | Converts Persian/Arabic digits (۰–۹, ٠–٩) to English (0–9) |
| `ToPersianNumbers()` | Converts English digits (0–9) to Persian (۰–۹) |
| `ConvertLayout(direction)` | Converts keyboard layout between Persian ↔ English (e.g., "سلام" → "sglm") |

---

## ✅ **Validation**
| Method | Purpose |
|--------|--------|
| `IsEmail()` | Validates if string is a properly formatted email |
| `IsValidUrl()` | Checks if string is a valid HTTP/HTTPS URL |
| `IsIranianMobile()` | Validates Iranian mobile number format (e.g., `09123456789`) |
| `IsNationalCode()` | Validates Iranian national ID (کد ملی) using official algorithm |
| `IsNumeric()` | Checks if string contains only digits |
| `IsGuid()` | Validates if string is a proper GUID |
| `IsValidJson()` | Checks if string is valid JSON (object or array) |

---

## ✂️ **Text Processing**
| Method | Purpose |
|--------|--------|
| `Truncate(maxLength, suffix = "...")` | Cuts string at given length and appends suffix |
| `ToSlug()` | Converts to URL-friendly format (e.g., `"Hello World"` → `"hello-world"`) |
| `WordCount()` | Counts number of words (non-whitespace sequences) |
| `Repeat(count)` | Repeats the string `n` times |
| `GetCharacterCount()` | Returns total character count |
| `CalculateReadTime()` | Estimates reading time (based on 200 chars/minute) |
| `Reverse()` | Reverses the characters in the string |

---

## 🛡️ **Security & Sanitization**
| Method | Purpose |
|--------|--------|
| `SanitizeHtml()` | Removes `<script>` tags and escapes `<` / `>` (basic XSS protection) |
| `Mask(unmaskedStart, unmaskedEnd)` | Masks middle characters (e.g., `"0912******789"`) |
| `MaskAll()` | Replaces entire string with `*` characters |

---

## 🔠 **Casing & Capitalization**
| Method | Purpose |
|--------|--------|
| `FirstCharToUpper()` | Capitalizes the first character, lowercases the rest |
| `ToTitleCase()` | Converts to title case (each word capitalized, supports Persian) |

---

## 🔢 **Numeric Conversion & Parsing**
| Method | Purpose |
|--------|--------|
| `ToHex()` | Converts numeric string to 8-digit uppercase hex (e.g., `"255"` → `"000000FF"`) |
| `ToDecimal(decimals)` | Parses to decimal and rounds to specified decimal places |
| `ToDouble()`, `ToInt()`, `ToLong()` | Safe parsing to numeric types with fallback default |
| `CleanAsInt(defaultValue)` | Extracts digits only and parses to `int` |
| `CleanAsDecimal(decimals)` | Extracts digits and commas, then parses to `decimal` |
| `CleanAsString()` | Extracts only alphabetic characters |
| `Val()` | Extracts leading numeric value (including one decimal point) |

---

## 📏 **Substring Operations**
| Method | Purpose |
|--------|--------|
| `Left(n)` | Returns first `n` characters |
| `Right(n)` | Returns last `n` characters |
| `Truncate(len)` | Removes `len` characters from the **end** |
| `TruncFromLeft(n)` | Removes first `n` characters |
| `CopyUntil(start, len)` | Copies substring from `start` index with given length |
| `CopyUntilChar(startIndex, char)` | Copies from index up to (but not including) the specified character |
| `CopyFromChar(char)` | Copies everything after the first occurrence of the character |
| `ReplaceAt(index, char)` | Replaces character at specific index |

---

## 🔄 **Type Conversion & Parsing Helpers**
| Method | Purpose |
|--------|--------|
| `ToBoolean()` | Safely converts string to `bool` (`false` on error) |
| `IsTrue()` | Returns `true` unless string is `"false"` or `"0"` |
| `ToUtf8Bytes()` | Serializes object to UTF-8 byte array using `System.Text.Json` |

---

## 📦 **JSON Serialization / Deserialization**

### Using **System.Text.Json**:
| Method | Purpose |
|--------|--------|
| `ToJson()` | Serializes object to JSON string |
| `ParseTo<T>()` | Deserializes JSON string to type `T` |

### Using **Newtonsoft.Json**:
| Method | Purpose |
|--------|--------|
| `ToJson()` | Serializes with default settings (camelCase, ignore loops, escape HTML) |
| `ToJson(settings)` | Serializes with custom `JsonSerializerSettings` |
| `ParseTo<T>()` | Deserializes JSON to `T` (logs error to console on failure) |
| `IsValidJson()` | Validates JSON structure using `JToken.Parse` |

---

## 🧩 **Utility & Miscellaneous**
| Method | Purpose |
|--------|--------|
| `ArraySearch(ArrayList, value)` | Performs binary search in a **sorted** `ArrayList` |
| `Apostrophe()` | Wraps value in single quotes (e.g., `"Hello".Apostrophe()` → `"'Hello'"`) |
| `In(params args)` | Checks if value exists in a list (e.g., `"net".In("dot", "net")` → `true`) |
| `Space(n)` | Returns a string of `n` space characters |
| `EncodeToBase64()` / `DecodeFromBase64()` | Converts string ↔ Base64 (UTF-8 encoded) |
| `ToBase64String()` / `GetBytes()` | Converts `byte[]` ↔ string using Base64 or UTF-8 |
| `WordsCount()` | Alternative word counter (same as `WordCount`) |
| `Replace(word, with, options)` | Regex-based replace with custom `RegexOptions` (e.g., `IgnoreCase`) |
| `TryParse(defaultValue)` | Safe `int` parsing with fallback |

---

## 📅 **Persian Date Conversion**
| Method | Purpose |
|--------|--------|
| `ToGregorianDate()` | Converts Persian date string (`"1404/07/03"`) to `DateTime` |
| `ToGregorianDate(hour, minute, second)` | Converts with specified time components |

---

## 🧽 **Trimming & String Comparison**
| Method | Purpose |
|--------|--------|
| `TrimAll()` | Trims whitespace from both ends (`null`-safe) |
| `TrimStartAll()` / `TrimEndAll()` | Trims only start or end |
| `EqualsIgnoreCase(other)` | Case-insensitive string comparison |
| `EqualsInvariant(other)` | Culture-invariant, case-insensitive comparison |
| `IsNullOrEmptyEx()` / `IsNullOrWhiteSpaceEx()` | Extension-style wrappers for built-in `string` checks |

---

## 🎯 **Special-Purpose Methods**
| Method | Purpose |
|--------|--------|
| `ValidateOrGenerateHex24()` | Validates or generates a 24-character hex string (e.g., MongoDB ObjectId) |
