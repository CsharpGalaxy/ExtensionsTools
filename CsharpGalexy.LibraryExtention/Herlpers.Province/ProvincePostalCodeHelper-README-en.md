# ProvincePostalCode and ProvincePostalCodeHelper Classes

This documentation covers the `ProvincePostalCode` and `ProvincePostalCodeHelper` classes which provide functionality to retrieve and manage postal codes for Iranian provinces.

## ProvincePostalCode Class

A data model class that represents province postal code information.

| Property | Type | Description |
|----------|------|-------------|
| ProvinceName | string | The name of the province |
| PostalCode | string | The postal code of the province |

## ProvincePostalCodeHelper Class

A static helper class that provides methods to work with Iranian province postal codes.

### Methods

| Method | Parameters | Return Type | Description |
|--------|------------|-------------|-------------|
| InitializeAsync | none | Task | Initializes and caches the province postal code data. Loads data only once and reuses it for subsequent calls. |
| LoadFromJsonAsync | none | Task<Dictionary<string, string>> | Loads province postal codes from a JSON file hosted online. Returns a dictionary with province names as keys and postal codes as values. |
| GetPostalCodeDictionaryAsync | none | Task<Dictionary<string, string>> | Returns the cached dictionary of province postal codes. Initializes the data if not already loaded. |
| GetPostalCodeAsync | string provinceName | Task<string?> | Gets the postal code for a specific province by name. Returns null if province not found. |
| IsSupportedProvinceAsync | string provinceName | Task<bool> | Checks if a province is supported in the postal code list. |
| GetAllProvinceNamesAsync | none | Task<string[]> | Returns an array of all supported province names. |
| GetAllPostalCodesAsync | none | Task<IReadOnlyList<(string ProvinceName, string PostalCode)>> | Returns all province postal codes as a read-only list of tuples. |

### Usage Examples

```csharp
// Initialize the helper
await ProvincePostalCodeHelper.InitializeAsync();

// Get postal code for a specific province
string? tehranCode = await ProvincePostalCodeHelper.GetPostalCodeAsync("تهران");

// Check if a province is supported
bool isSupported = await ProvincePostalCodeHelper.IsSupportedProvinceAsync("اصفهان");

// Get all province names
string[] provinces = await ProvincePostalCodeHelper.GetAllProvinceNamesAsync();

// Get all postal codes
var allCodes = await ProvincePostalCodeHelper.GetAllPostalCodesAsync();
```

### Error Handling

- Throws `ArgumentException` if province name is null or empty
- Throws `InvalidOperationException` if:
  - JSON deserialization fails
  - Cannot load data from the online JSON file

### Data Source

The postal code data is loaded from:
```
https://raw.githubusercontent.com/CsharpGalexy/ExtentionsTools/main/CsharpGalexy.LibraryExtention.Data/Iran/Provinces/province-postal-codes.json
```

### Thread Safety

The class is thread-safe and caches data efficiently using static fields.