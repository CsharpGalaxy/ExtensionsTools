# ProvinceHelper Classes and Models

This documentation covers the `ProvinceHelper` class and its related models which provide functionality to manage and retrieve information about Iranian provinces.

## Models

### ProvinceModel Class

A data model class that represents province information.

| Property | Type | Description |
|----------|------|-------------|
| ProvinceId | string | Unique identifier for the province |
| ProvinceName | string | The name of the province |

### CityModel Class

A data model class that represents city information.

| Property | Type | Description |
|----------|------|-------------|
| CityId | string | Unique identifier for the city |
| ProvinceId | string | Reference to the province ID |
| ProvinceName | string | The name of the province |
| CityName | string | The name of the city |

## ProvinceHelper Class

A static helper class that provides methods to work with Iranian provinces data.

### Methods

| Method | Parameters | Return Type | Description |
|--------|------------|-------------|-------------|
| InitializeAsync | none | Task | Initializes and caches the provinces data. Loads data only once and reuses it for subsequent calls. |
| LoadProvincesFromJsonAsync | none | Task<List<ProvinceModel>> | Loads provinces from a JSON file hosted online. Returns a list of province models. |
| GetAllProvincesAsync | none | Task<IReadOnlyList<ProvinceModel>> | Returns a read-only list of all provinces. Initializes the data if not already loaded. |
| GetByProvinceIdAsync | string provinceId | Task<ProvinceModel?> | Gets a province by its ID. Returns null if not found. |
| GetByProvinceNameAsync | string provinceName | Task<ProvinceModel?> | Gets a province by its name (case-insensitive). Returns null if not found. |
| ExistsByProvinceIdAsync | string provinceId | Task<bool> | Checks if a province exists by its ID. |
| ExistsByProvinceNameAsync | string provinceName | Task<bool> | Checks if a province exists by its name (case-insensitive). |

### Usage Examples

```csharp
// Initialize the helper
await ProvinceHelper.InitializeAsync();

// Get all provinces
var provinces = await ProvinceHelper.GetAllProvincesAsync();

// Find a province by ID
var province = await ProvinceHelper.GetByProvinceIdAsync("08");

// Find a province by name
var tehran = await ProvinceHelper.GetByProvinceNameAsync("تهران");

// Check if province exists
bool exists = await ProvinceHelper.ExistsByProvinceNameAsync("اصفهان");
```

### Error Handling

- Returns `null` for non-existent provinces when using Get methods
- Returns `false` for non-existent provinces when using Exists methods
- Throws `InvalidOperationException` if:
  - Cannot load data from the online JSON file
  - JSON deserialization fails

### Data Source

The provinces data is loaded from:
```
https://raw.githubusercontent.com/CsharpGalexy/ExtensionsTools/main/CsharpGalexy.LibraryExtention.Data/Iran/Provinces/provinces.json
```

### Thread Safety

The class is thread-safe and caches data efficiently using static fields.

### Case Sensitivity

Province name searches are case-insensitive for better usability.