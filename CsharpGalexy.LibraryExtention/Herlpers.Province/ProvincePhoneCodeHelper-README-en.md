# ProvincePhoneCode and ProvincePhoneCodeHelper Classes

This documentation covers the `ProvincePhoneCode` and `ProvincePhoneCodeHelper` classes which provide functionality to retrieve and manage phone area codes for Iranian provinces.

## ProvincePhoneCode Class

A data model class that represents province phone code information.

| Property | Type | Description |
|----------|------|-------------|
| ProvinceName | string | The name of the province |
| PhoneCode | string | The phone area code of the province |

## ProvincePhoneCodeHelper Class

A static helper class that provides methods to work with Iranian province phone codes.

### Methods

| Method | Parameters | Return Type | Description |
|--------|------------|-------------|-------------|
| InitializeAsync | none | Task | Initializes and caches the province phone code data. Loads data only once and reuses it for subsequent calls. |
| LoadFromJsonAsync | none | Task<Dictionary<string, string>> | Loads province phone codes from a JSON file hosted online. Returns a dictionary with province names as keys and phone codes as values. |
| GetPhoneCodeDictionaryAsync | none | Task<Dictionary<string, string>> | Returns the cached dictionary of province phone codes. Initializes the data if not already loaded. |
| GetPhoneCodeAsync | string provinceName | Task<string?> | Gets the phone code for a specific province by name. Returns null if province not found. |
| IsSupportedProvinceAsync | string provinceName | Task<bool> | Checks if a province is supported in the phone code list. |
| GetAllProvinceNamesAsync | none | Task<string[]> | Returns an array of all supported province names. |
| GetAllPhoneCodesAsync | none | Task<IReadOnlyList<(string ProvinceName, string PhoneCode)>> | Returns all province phone codes as a read-only list of tuples. |

### Usage Examples

```csharp
// Initialize the helper
await ProvincePhoneCodeHelper.InitializeAsync();

// Get phone code for a specific province
string? tehranCode = await ProvincePhoneCodeHelper.GetPhoneCodeAsync("تهران");

// Check if a province is supported
bool isSupported = await ProvincePhoneCodeHelper.IsSupportedProvinceAsync("اصفهان");

// Get all province names
string[] provinces = await ProvincePhoneCodeHelper.GetAllProvinceNamesAsync();

// Get all phone codes
var allCodes = await ProvincePhoneCodeHelper.GetAllPhoneCodesAsync();
```

### Error Handling

- Throws `ArgumentException` if province name is null or empty
- Throws `InvalidOperationException` if:
  - JSON deserialization fails
  - Cannot load data from the online JSON file

### Data Source

The phone code data is loaded from:
```
https://raw.githubusercontent.com/CsharpGalexy/ExtentionsTools/main/CsharpGalexy.LibraryExtention.Data/Iran/Provinces/province-phone-codes.json
```

### Thread Safety

The class is thread-safe and caches data efficiently using static fields.