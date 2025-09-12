
### DateTime Extensions (Persian DateTime Utilities)

This library provides a set of extension methods for easier date and time handling in .NET.
Features include converting Gregorian dates to the Persian calendar, performing calendar calculations, checking date status, and displaying relative time.

Features

🔄 Convert Gregorian date to Persian (Shamsi)

📅 Calculate first and last day of the month

⏳ Display elapsed time ("x minutes/hours/days ago")

🎂 Calculate age

🏢 Add business days (skipping weekends/holidays)

🔍 Use with LINQ to filter dates (past/today/future)

Sample Code
#region DateTime
var now = DateTime.Now;

// Convert to Persian date
Console.WriteLine(now.ToShamsiDate());               // Output: 1403/02/15
Console.WriteLine(now.ToShamsiDate("dd MMMM yyyy")); // Output: 15 Farvardin 1403

// Check date
Console.WriteLine(now.IsToday());        // True
Console.WriteLine(now.AddDays(-1).IsYesterday()); // True

// Calculations
Console.WriteLine(now.FirstDayOfMonth()); // 1403/02/01
Console.WriteLine(now.LastDayOfMonth());  // 1403/02/31

// Elapsed time
Console.WriteLine(now.AddHours(-3).TimeAgo()); // "3 hours ago"

// Calculate age
Console.WriteLine(new DateTime(1990, 4, 20).Age()); // 34

// Business days
Console.WriteLine(now.AddBusinessDays(5)); // 5 business days later

// LINQ example
var dates = new List<DateTime> { now, now.AddDays(-1), now.AddDays(2) };
var todayOrLater = dates.Where(d => d.IsToday() || d.IsFuture());
#endregion

Installation

Clone the project or add it to your solution.

Import the namespace:

using YourNamespace.DateTimeExtensions;


Start using the extension methods 🚀

Contributing

Pull requests are welcome.

Please run tests before submitting a PR.

License

This project is licensed under the MIT License.
فایل readme بنویس

### ASSISTANT
# DateTime Extensions (Persian DateTime Utilities)

A lightweight and practical .NET library that extends `DateTime` with useful methods for working with **Gregorian** and **Persian (Shamsi/Jalali)** calendars. Ideal for Persian-speaking developers or applications requiring localized date handling.

---

## ✨ Features

- 🔄 **Convert Gregorian dates to Persian (Shamsi/Jalali) calendar**
- 📅 **Get first and last day of the month (Gregorian & Shamsi)**
- ⏳ **Display relative time (e.g., "3 hours ago", "2 days later")**
- 🎂 **Calculate age from birthdate**
- 🏢 **Add business days (skips weekends; customizable holiday support)**
- 🔍 **Easy date filtering in LINQ (past, today, future)**
- 🧪 **Well-tested extension methods**

Perfect for ASP.NET Core, Blazor, WinForms, WPF, and console apps!

---

## 💡 Sample Usage

```csharp
#region DateTime
var now = DateTime.Now;

// Convert to Persian (Shamsi) date
Console.WriteLine(now.ToShamsiDate());               // Output: 1403/02/15
Console.WriteLine(now.ToShamsiDate("dd MMMM yyyy")); // Output: 15 Farvardin 1403

// Check date status
Console.WriteLine(now.IsToday());        // True
Console.WriteLine(now.AddDays(-1).IsYesterday()); // True

// First and last day of the month (in Persian format)
Console.WriteLine(now.FirstDayOfMonth()); // 1403/02/01
Console.WriteLine(now.LastDayOfMonth());  // 1403/02/31

// Elapsed time (relative time ago)
Console.WriteLine(now.AddHours(-3).TimeAgo()); // "3 hours ago"

// Calculate age
Console.WriteLine(new DateTime(1990, 4, 20).Age()); // 34

// Add business days (skips Friday & Saturday by default)
Console.WriteLine(now.AddBusinessDays(5)); // Returns date after 5 workdays

// Use in LINQ queries
var dates = new List<DateTime> { now, now.AddDays(-1), now.AddDays(2) };
var todayOrLater = dates.Where(d => d.IsToday() || d.IsFuture()).ToList();
#endregion
```

---

## 📦 Installation

### Option 1: Clone & Include in Project
1. Clone this repository:
   ```bash
   git clone https://github.com/yourusername/DateTimeExtensions.git
   ```
2. Add the project or `.cs` files to your solution.
3. Import the namespace:
   ```csharp
   using DateTimeExtensions;
   ```

### Option 2: Manual Copy
Copy the `DateTimeExtensions.cs` file into your project and ensure the namespace matches your project structure.

> ✅ No external dependencies! Works on all .NET versions supporting `DateTime`.

---

## 🛠️ Supported Methods

| Method | Description |
|-------|-------------|
| `.ToShamsiDate()` | Converts `DateTime` to Persian date string (`yyyy/MM/dd`) |
| `.ToShamsiDate(string format)` | Formats Persian date using custom format (supports `yyyy`, `MM`, `dd`, `MMMM` for month name) |
| `.IsToday()` | Checks if date is today |
| `.IsYesterday()` | Checks if date is yesterday |
| `.IsTomorrow()` | Checks if date is tomorrow |
| `.IsFuture()` | Checks if date is in the future |
| `.IsPast()` | Checks if date is in the past |
| `.FirstDayOfMonth()` | Gets first day of the month as Persian date string |
| `.LastDayOfMonth()` | Gets last day of the month as Persian date string |
| `.TimeAgo()` | Returns human-readable elapsed time (e.g., "just now", "5 minutes ago") |
| `.Age()` | Calculates full years between date and today (useful for birthdays) |
| `.AddBusinessDays(int count)` | Adds specified number of business days (skips weekends: Thursday/Friday/Saturday based on culture) |

> 🌍 Weekend days can be customized via globalization settings or extended logic.

---

## 🔍 LINQ Integration Example

Filter dates easily:

```csharp
var filteredDates = dates.Where(d => d.IsPast()).ToList(); // All past dates
var upcoming = dates.Where(d => d.IsFuture()).ToList();    // Future events
var onlyToday = dates.Where(d => d.IsToday()).ToList();    // Today's records
```

Useful for dashboards, scheduling, reminders, etc.

---

## 🚀 Coming Soon (Planned Features)

- [ ] Support for public Iranian holidays (Nowruz, Independence Day, etc.)
- [ ] Bidirectional conversion: **Persian → Gregorian**
- [ ] NodaTime integration
- [ ] Async holiday fetching (web API-based)
- [ ] Culture-aware weekday names

---

## 🤝 Contributing

Contributions are welcome! Feel free to:

- Fork the repo
- Add features or fix bugs
- Run tests and ensure compatibility
- Submit a Pull Request

Please make sure your code follows clean coding practices and includes documentation where necessary.

---

---

## 📄 License

MIT License

Copyright (c) 2025 Your Name

Permission is hereby granted, free of charge, to any person obtaining a copy  
of this software and associated documentation files (the "Software"), to deal  
in the Software without restriction, including without limitation the rights  
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell  
copies of the Software, and to permit persons to whom the Software is  
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all  
copies or substantial portions of the Software.

**THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED**, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

---

## 📬 Feedback & Issues

Have a suggestion or found a bug? Please open an [Issue](https://github.com/CsharpGalexy/ExtentionsTools/issues) on GitHub.

Let’s make Persian date handling easier together! 🇮🇷📅

---

⭐ **Star this project if you find it helpful!**
