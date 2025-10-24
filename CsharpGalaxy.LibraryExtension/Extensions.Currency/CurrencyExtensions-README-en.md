# CurrencyExtensions Class

This documentation covers the `CurrencyExtensions` class which provides extensive methods for handling Iranian currency (Rial/Toman) formatting and calculations.

## Display Format Methods

### Basic Currency Formatting

| Method | Parameters | Return Type | Description |
|--------|------------|-------------|-------------|
| ToRialString | decimal value | string | Formats amount in Rials with thousands separator |
| ToTomanString | decimal value | string | Converts to Tomans (divides by 10) and formats |
| ToCurrencyString | decimal value, string unit | string | Formats amount with custom currency unit |

### Localized Formatting

| Method | Parameters | Return Type | Description |
|--------|------------|-------------|-------------|
| ToPersianDigits | decimal value, string format="N0" | string | Converts numbers to Persian digits |
| ToLocalizedMoney | decimal value, CultureInfo culture | string | Formats amount based on culture |
| FormatWithCulture | decimal value, string cultureCode="fa-IR" | string | Formats using specified culture code |

### Readable Formats

| Method | Parameters | Return Type | Description |
|--------|------------|-------------|-------------|
| ToMoneyWithSuffix | decimal value | string | Displays with suffix (thousand, million) |
| ToCompactMoney | decimal value | string | Compact display with K/M suffixes |
| ToColoredMoney | decimal value | string | Adds color hints for positive/negative |
| ToMoneyWords | decimal value | string | Converts amount to Persian words |

## Financial Operations

### Calculations

| Method | Parameters | Return Type | Description |
|--------|------------|-------------|-------------|
| RoundToNearest | decimal value, int step=100 | decimal | Rounds to nearest step value |
| ApplyDiscount | decimal value, decimal percent | decimal | Calculates amount after discount |
| AddTax | decimal value, decimal percent | decimal | Adds tax percentage to amount |
| ConvertToCurrency | decimal value, decimal rate | decimal | Converts using exchange rate |

### Business Logic

| Method | Parameters | Return Type | Description |
|--------|------------|-------------|-------------|
| IsAffordable | decimal value, decimal balance | bool | Checks if balance is sufficient |
| ToPercentageOf | decimal value, decimal total | decimal | Calculates percentage of total |
| ToDiscountedPrice | decimal discounted, decimal original | string | Shows discount percentage |

## Usage Examples

### Basic Currency Display
```csharp
decimal amount = 15000m;
string rial = amount.ToRialString();      // "15,000 ریال"
string toman = amount.ToTomanString();     // "1,500 تومان"
string custom = amount.ToCurrencyString("دلار"); // "15,000 دلار"
```

### Readable Formats
```csharp
decimal amount = 1500000m;
string suffix = amount.ToMoneyWithSuffix();   // "1.5 میلیون تومان"
string compact = amount.ToCompactMoney();      // "1.5M تومان"
string words = amount.ToMoneyWords();          // "یک میلیون و پانصد هزار تومان"
```

### Financial Calculations
```csharp
decimal price = 1000m;
decimal withTax = price.AddTax(9);         // 1090 (9% VAT)
decimal withDiscount = price.ApplyDiscount(10); // 900 (10% off)
bool canBuy = price.IsAffordable(1500m);   // true
```

## Important Notes

1. **Currency Conversion**:
   - Toman = Rial / 10
   - Uses Persian (fa-IR) culture for formatting
   - Supports both Rial and Toman display

2. **Formatting Features**:
   - Persian digit conversion
   - Thousands separators
   - Optional decimal places
   - Currency symbols and positioning

3. **Performance Considerations**:
   - Caches CultureInfo for better performance
   - Efficient string handling
   - Minimal object creation

## Dependencies

- System.Globalization namespace
- System.Text namespace
- System.Text.RegularExpressions namespace