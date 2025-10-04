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