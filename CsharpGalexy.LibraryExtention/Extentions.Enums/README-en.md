
---

## 🔍 **دریافت توضیحات و متون نمایش (Description / Display)**
| متد | کاربرد |
|------|--------|
| `GetDescription()` | گرفتن مقدار `DescriptionAttribute` روی عضو enum؛ در صورت نبودن description، نام enum برگردانده می‌شود. |
| `GetDisplayName()` | گرفتن مقدار `DisplayAttribute.Name` (در صورت وجود) و در غیر این‌صورت `DescriptionAttribute.Description`؛ اگر هیچ‌کدام موجود نباشد `ToString()` باز می‌گردد. |

**نکات:** `GetDisplayName()` ابتدا `DisplayAttribute` را بررسی می‌کند و سپس به سراغ `DescriptionAttribute` می‌رود. اگر فیلد enum پیدا نشود، مقدار `ToString()` بازگردانده می‌شود.

---

## 🔽 **تبدیل enum به لیست برای استفاده در Dropdown / Selects**
| متد | کاربرد |
|------|--------|
| `ToList<T>() where T : Enum` | تبدیل نوع enum به `IEnumerable<KeyValuePair<int,string>>` برای استفاده در dropdownها (کلید: مقدار عددی، مقدار: متن نمایش). |
| `ToSelectList<T>() where T : Enum` | نسخهٔ تکuple: `IEnumerable<(int Value, string Text)>` شامل مقدار عددی و متن نمایش (`GetDisplayName`). |

**نکته:** متن نمایش برای هر عضو با `GetDisplayName()` تولید می‌شود، پس اگر از `DisplayAttribute` یا `DescriptionAttribute` استفاده کرده باشید، آن متن نشان داده خواهد شد.

---