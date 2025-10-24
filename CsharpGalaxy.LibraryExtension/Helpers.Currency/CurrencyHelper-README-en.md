# CurrencyHelper and CurrencyInfo Classes

This documentation covers the `CurrencyHelper` and `CurrencyInfo` classes which provide functionality to retrieve and manage currency information for different countries.

## CurrencyInfo Class

A data model class that represents currency information.

| Property | Type | Description |
|----------|------|-------------|
| CountryName | string | The name of the country |
| CurrencyCode | string | The currency code (e.g., USD) |
| CurrencyName | string | The name of the currency (e.g., US Dollar) |
| Symbol | string | The currency symbol (e.g., $) |

## CurrencyHelper Class

A static helper class that provides methods to work with currency information loaded from an online JSON source.

### Methods

| Method | Parameters | Return Type | Description |
|--------|------------|-------------|-------------|
| InitializeAsync | none | Task | Initializes and caches the currency data. Loads data only once and reuses it for subsequent calls. |
| LoadFromJsonAsync | none | Task<List<CurrencyInfo>> | Loads currency data from a JSON file hosted online. Returns a list of currency information. |
| GetAllCurrenciesAsync | none | Task<IReadOnlyList<CurrencyInfo>> | Returns a read-only list of all currency information. |
| GetCurrencyCodeByCountryAsync | string countryName | Task<string?> | Gets the currency code for a country (e.g., "USD" for "United States"). Returns null if not found. |
| GetCurrencyNameByCountryAsync | string countryName | Task<string?> | Gets the currency name for a country (e.g., "US Dollar" for "United States"). Returns null if not found. |
| GetCurrencySymbolByCountryAsync | string countryName | Task<string?> | Gets the currency symbol for a country (e.g., "$" for "United States"). Returns null if not found. |
| GetCountryByCurrencyCodeAsync | string currencyCode | Task<string?> | Gets the first country that uses the specified currency code. Returns null if not found. |
| GetCountriesByCurrencyCodeAsync | string currencyCode | Task<IReadOnlyList<string>> | Gets all countries that use the specified currency code. Returns empty list if none found. |

### Usage Examples

```csharp
// Initialize the helper
await CurrencyHelper.InitializeAsync();

// Get all currencies
var currencies = await CurrencyHelper.GetAllCurrenciesAsync();

// Get currency information for a country
string? usdCode = await CurrencyHelper.GetCurrencyCodeByCountryAsync("United States");
string? usdName = await CurrencyHelper.GetCurrencyNameByCountryAsync("United States");
string? usdSymbol = await CurrencyHelper.GetCurrencySymbolByCountryAsync("United States");

// Find countries using a currency
string? country = await CurrencyHelper.GetCountryByCurrencyCodeAsync("USD");
var countries = await CurrencyHelper.GetCountriesByCurrencyCodeAsync("EUR");
```

### Error Handling

- Returns `null` for single items not found
- Returns empty collections for multiple items not found
- Throws `InvalidOperationException` if:
  - Cannot load data from the online JSON file
  - JSON deserialization fails

### Data Source

The currency data is loaded from:
```
https://raw.githubusercontent.com/CsharpGalexy/ExtensionsTools/main/CsharpGalexy.LibraryExtention.Data/Iran/Provinces/currency-codes.json
```

### Thread Safety

The class is thread-safe and caches data efficiently using static fields.

### Case Sensitivity

Country names and currency codes are case-insensitive for better usability.