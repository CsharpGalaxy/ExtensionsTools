# ⏱️ TimeSpan Extensions Documentation

## 📝 **Formatting & Display**
| Method | Description | Parameters | Return Type | Example |
|--------|-------------|------------|-------------|---------|
| `ToHumanReadable` | Converts TimeSpan to readable English text | `TimeSpan` | `string` | "2 days, 3 hours, 5 minutes" |
| `ToHumanReadablePersian` | Converts TimeSpan to readable Persian text | `TimeSpan` | `string` | "2 روز, 3 ساعت, 5 دقیقه" |
| `ToShortString` | Creates compact representation | `TimeSpan` | `string` | "2d 3h 5m" |
| `ToDictionary` | Converts to English component dictionary | `TimeSpan` | `Dictionary<string,int>` | `{"Days": 2, "Hours": 3, ...}` |
| `ToDictionaryPersian` | Converts to Persian component dictionary | `TimeSpan` | `Dictionary<string,int>` | `{"روز": 2, "ساعت": 3, ...}` |

## 📊 **Calculations & Measurements**
| Method | Description | Parameters | Return Type | Example |
|--------|-------------|------------|-------------|---------|
| `TotalMinutesExact` | Gets total minutes | `TimeSpan` | `double` | `90.5` for 1.5 hours |
| `TotalWeeks` | Calculates total weeks | `TimeSpan` | `double` | `1.0` for 7 days |
| `PercentageOf` | Calculates percentage of another TimeSpan | `TimeSpan, TimeSpan other` | `double` | `50.0` for half of other |
| `Multiply` | Multiplies TimeSpan by a factor | `TimeSpan, double factor` | `TimeSpan` | `2 hours` * 2 = `4 hours` |
| `Divide` | Divides TimeSpan by a number | `TimeSpan, double divisor` | `TimeSpan` | `4 hours` / 2 = `2 hours` |

## ✂️ **Rounding & Truncation**
| Method | Description | Parameters | Return Type |
|--------|-------------|------------|-------------|
| `RoundToNearestMinute` | Rounds to nearest minute | `TimeSpan` | `TimeSpan` |
| `RoundToNearestHour` | Rounds to nearest hour | `TimeSpan` | `TimeSpan` |
| `RoundToNearestDay` | Rounds to nearest day | `TimeSpan` | `TimeSpan` |
| `TruncateToMinutes` | Removes seconds & milliseconds | `TimeSpan` | `TimeSpan` |
| `TruncateToHours` | Removes minutes & below | `TimeSpan` | `TimeSpan` |
| `TruncateToDays` | Removes hours & below | `TimeSpan` | `TimeSpan` |

## 🔍 **Validation & Checks**
| Method | Description | Parameters | Return Type |
|--------|-------------|------------|-------------|
| `IsPositive` | Checks if TimeSpan is positive | `TimeSpan` | `bool` |
| `IsNegative` | Checks if TimeSpan is negative | `TimeSpan` | `bool` |
| `IsZero` | Checks if TimeSpan is zero | `TimeSpan` | `bool` |
| `Between` | Checks if TimeSpan is between two values | `TimeSpan, TimeSpan min, TimeSpan max` | `bool` |

## 🔄 **Manipulation**
| Method | Description | Parameters | Return Type |
|--------|-------------|------------|-------------|
| `Clamp` | Restricts TimeSpan to range | `TimeSpan, TimeSpan min, TimeSpan max` | `TimeSpan` |
| `Abs` | Gets absolute value | `TimeSpan` | `TimeSpan` |
| `AddBusinessDays` | Adds working days (skip weekends) | `TimeSpan, int businessDays` | `TimeSpan` |

## 📝 **Usage Examples**

```csharp
var timeSpan = TimeSpan.FromDays(2.5);

// Formatting
Console.WriteLine(timeSpan.ToHumanReadable());     // "2 days, 12 hours"
Console.WriteLine(timeSpan.ToHumanReadablePersian()); // "2 روز, 12 ساعت"
Console.WriteLine(timeSpan.ToShortString());       // "2d 12h"

// Calculations
Console.WriteLine(timeSpan.TotalWeeks());         // 0.357...
Console.WriteLine(timeSpan.Multiply(2));          // 5 days
Console.WriteLine(timeSpan.PercentageOf(TimeSpan.FromDays(5))); // 50%

// Rounding
Console.WriteLine(timeSpan.RoundToNearestDay());  // 3 days
Console.WriteLine(timeSpan.TruncateToDays());     // 2 days

// Validation
Console.WriteLine(timeSpan.IsPositive());         // true
Console.WriteLine(timeSpan.Between(
    TimeSpan.FromDays(1), 
    TimeSpan.FromDays(3))); // true
```