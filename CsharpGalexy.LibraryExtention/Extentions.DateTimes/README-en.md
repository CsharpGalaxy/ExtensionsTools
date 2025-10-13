# 📅 DateTime Extensions Documentation

## 🌏 **Persian (Shamsi) Date Conversions**
| Method | Description | Parameters | Return Type |
|--------|-------------|------------|-------------|
| `ToShamsiDate` | Converts to Persian date | `DateTime` | `string` (1403/02/15) |
| `ToShamsiDate` (with format) | Converts with custom format | `DateTime, string format` | `string` |
| `GetPersianDayOfWeek` | Gets Persian day name | `DateTime` | `string` (e.g., "دوشنبه") |
| `GetPersianMonth` | Gets Persian month name | `DateTime` | `string` (e.g., "فروردین") |
| `GetTodayWeekDay` | Gets today's weekday | `DateTime?` | `WeekDay` enum |
| `GetNameOfDay` | Gets Persian name of weekday | `WeekDay` | `string` |

## ⏰ **Time Comparisons**
| Method | Description | Parameters | Return Type |
|--------|-------------|------------|-------------|
| `DifTime` | Check time difference within tolerance | `DateTime, DateTime, int minutes` | `bool` |
| `IsBetween` | Check if date is between range | `DateTime, start, end` | `bool` |
| `IsToday` | Check if date is today | `DateTime` | `bool` |
| `IsYesterday` | Check if date is yesterday | `DateTime` | `bool` |
| `IsTomorrow` | Check if date is tomorrow | `DateTime` | `bool` |
| `IsFuture` | Check if date is in future | `DateTime` | `bool` |
| `IsPast` | Check if date is in past | `DateTime` | `bool` |

## 📊 **Date Calculations**
| Method | Description | Parameters | Return Type |
|--------|-------------|------------|-------------|
| `AddBusinessDays` | Add working days (skip weekends) | `DateTime, int days` | `DateTime` |
| `FirstDayOfMonth` | Get month's first day | `DateTime` | `DateTime` |
| `LastDayOfMonth` | Get month's last day | `DateTime` | `DateTime` |
| `FirstDayOfYear` | Get year's first day | `DateTime` | `DateTime` |
| `LastDayOfYear` | Get year's last day | `DateTime` | `DateTime` |
| `DaysInMonth` | Get days count in month | `DateTime` | `int` |
| `Age` | Calculate age from birth date | `DateTime` | `int` |

## 🕒 **Time Units**
| Method | Description | Parameters | Return Type |
|--------|-------------|------------|-------------|
| `StartOfDay` | Set time to 00:00:00 | `DateTime` | `DateTime` |
| `EndOfDay` | Set time to 23:59:59.999 | `DateTime` | `DateTime` |
| `TruncateToSecond` | Remove milliseconds | `DateTime` | `DateTime` |
| `TruncateToMinute` | Remove seconds and below | `DateTime` | `DateTime` |
| `TruncateToHour` | Remove minutes and below | `DateTime` | `DateTime` |

## 📝 **Human Readable Time**
| Method | Description | Parameters | Return Type |
|--------|-------------|------------|-------------|
| `TimeAgo` | Get readable time difference | `DateTime` | `string` (e.g., "2 دقیقه پیش") |

## 🔄 **Date Status**
| Method | Description | Parameters | Return Type |
|--------|-------------|------------|-------------|
| `IsThisWeek` | Check if in current week | `DateTime` | `bool` |
| `IsThisMonth` | Check if in current month | `DateTime` | `bool` |
| `IsThisYear` | Check if in current year | `DateTime` | `bool` |
| `IsWeekend` | Check if weekend (Fri/Sat) | `DateTime` | `bool` |
| `IsWeekday` | Check if weekday | `DateTime` | `bool` |
| `IsPersianLeapYear` | Check if Persian year is leap | `int persianYear` | `bool` |

## 🔄 **Unix Timestamp**
| Method | Description | Parameters | Return Type |
|--------|-------------|------------|-------------|
| `ToUnixTimestamp` | Convert to Unix timestamp | `DateTimeOffset` | `long` |
| `FromUnixTimestamp` | Convert from Unix timestamp | `long timestamp` | `DateTimeOffset` |

## 📝 **Usage Examples**

### Persian Date Conversion
```csharp
var date = DateTime.Now;
Console.WriteLine(date.ToShamsiDate());                    // "1403/02/15"
Console.WriteLine(date.ToShamsiDate("dd MMMM yyyy"));     // "15 اردیبهشت 1403"
Console.WriteLine(date.GetPersianDayOfWeek());            // "دوشنبه"
Console.WriteLine(date.GetPersianMonth());                // "اردیبهشت"
```

### Time Calculations
```csharp
var date = DateTime.Now;
Console.WriteLine(date.AddBusinessDays(5));               // Add 5 working days
Console.WriteLine(date.Age());                            // Calculate age
Console.WriteLine(date.TimeAgo());                        // "2 ساعت پیش"

// Date checks
Console.WriteLine(date.IsToday());                        // true
Console.WriteLine(date.IsFuture());                       // false
Console.WriteLine(date.IsWeekend());                      // false
```

## ⚠️ **Important Notes**

1. Persian Calendar:
   - Uses `PersianCalendar` class internally
   - Supports years from 1 to 9378 in Persian calendar
   - Handles leap years correctly

2. Business Days:
   - Weekend is considered Friday and Saturday (Iranian calendar)
   - Does not account for holidays

3. Time Zones:
   - Methods work with local time by default
   - Use DateTimeOffset for time zone awareness

4. Performance:
   - Most methods are optimized for performance
   - Date calculations cache results where appropriate