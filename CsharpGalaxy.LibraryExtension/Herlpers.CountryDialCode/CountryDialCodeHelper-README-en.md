# CountryDialCode and CountryDialCodeHelper Classes

This documentation covers the `CountryDialCode` and `CountryDialCodeHelper` classes which provide functionality to manage and retrieve country dialing codes.

## CountryDialCode Class

A data model class that represents a country with its dialing code.

| Property | Type | Description |
|----------|------|-------------|
| DialCode | string | The international dialing code for the country (e.g., "+98") |
| PersianCountryName | string | The country name in Persian language |
| EnglishCountryName | string | The country name in English language |

## CountryDialCodeHelper Class

A static helper class that provides methods to work with country dialing codes loaded from an online JSON source.

### Methods

| Method | Parameters | Return Type | Description |
|--------|------------|-------------|-------------|
| InitializeAsync | none | Task | Initializes and caches the country data. Loads data only once and reuses it for subsequent calls. |
| LoadFromJsonAsync | none | Task<List<CountryDialCode>> | Loads country data from a JSON file hosted online. Returns a list of country dial codes. |
| GetAllCountriesAsync | none | Task<IReadOnlyList<CountryDialCode>> | Returns a read-only list of all countries with their dial codes. |
| GetPersianCountryByDialCodeAsync | string dialCode | Task<string?> | Gets the Persian name of a country by its dial code. Returns null if not found. |
| GetEnglishCountryByDialCodeAsync | string dialCode | Task<string?> | Gets the English name of a country by its dial code. Returns null if not found. |
| GetDialCodeByPersianCountryAsync | string persianCountryName | Task<string?> | Gets the dial code by Persian country name (case-insensitive). Returns null if not found. |
| GetDialCodeByEnglishCountryAsync | string englishCountryName | Task<string?> | Gets the dial code by English country name (case-insensitive). Returns null if not found. |
| GetAllCountriesSortedByDialCodeAsync | none | Task<IReadOnlyList<CountryDialCode>> | Returns all countries sorted by their dial codes numerically. |

### Usage Examples

```csharp
// Initialize the helper
await CountryDialCodeHelper.InitializeAsync();

// Get all countries
var countries = await CountryDialCodeHelper.GetAllCountriesAsync();

// Get country names by dial code
string? persianName = await CountryDialCodeHelper.GetPersianCountryByDialCodeAsync("+98");
string? englishName = await CountryDialCodeHelper.GetEnglishCountryByDialCodeAsync("+1");

// Get dial code by country name
string? iranCode = await CountryDialCodeHelper.GetDialCodeByPersianCountryAsync("ایران");
string? usCode = await CountryDialCodeHelper.GetDialCodeByEnglishCountryAsync("United States");

// Get sorted countries by dial code
var sortedCountries = await CountryDialCodeHelper.GetAllCountriesSortedByDialCodeAsync();
```

### Error Handling

- Returns `null` for non-existent countries or dial codes
- Throws `InvalidOperationException` if:
  - Cannot load data from the online JSON file
  - JSON deserialization fails

### Data Source

The country dial code data is loaded from:
```
https://raw.githubusercontent.com/CsharpGalexy/ExtensionsTools/main/CsharpGalexy.LibraryExtention.Data/Iran/Provinces/country-dial-codes.json
```

### Thread Safety

The class is thread-safe and caches data efficiently using static fields.

### Case Sensitivity

Country name searches are case-insensitive for better usability.

### Sorting Behavior

The `GetAllCountriesSortedByDialCodeAsync` method:
- Filters out entries with empty or invalid dial codes
- Sorts based on the numeric value after the '+' symbol
- Only includes dial codes that start with '+'