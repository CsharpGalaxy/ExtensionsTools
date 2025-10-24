# CityHelper Class Documentation

## 🏛️ **Overview**
CityHelper is a static class that provides methods for working with Iranian cities and provinces data. It loads data from a JSON file and provides various methods to query city and province information.

## 📋 **Initialization Methods**
| Method | Description | Parameters | Return Type |
|--------|-------------|------------|-------------|
| `InitializeAsync` | Initialize and cache city data | None | `Task` |
| `LoadCitiesFromJsonAsync` | Load cities data from JSON file | None | `Task<List<CityModel>>` |

## 🔍 **Query Methods**
| Method | Description | Parameters | Return Type |
|--------|-------------|------------|-------------|
| `GetByCityIdAsync` | Find city by ID | `string cityId` | `Task<CityModel?>` |
| `GetByCityNameAsync` | Find city by name | `string cityName` | `Task<CityModel?>` |
| `GetCitiesByProvinceIdAsync` | Get all cities in a province by ID | `string provinceId` | `Task<IReadOnlyList<CityModel>>` |
| `GetCitiesByProvinceNameAsync` | Get all cities in a province by name | `string provinceName` | `Task<IReadOnlyList<CityModel>>` |

## ✅ **Validation Methods**
| Method | Description | Parameters | Return Type |
|--------|-------------|------------|-------------|
| `ExistsByCityIdAsync` | Check if city exists by ID | `string cityId` | `Task<bool>` |
| `ExistsByCityNameAsync` | Check if city exists by name | `string cityName` | `Task<bool>` |

## 📦 **CityModel Structure**
| Property | Type | Description |
|----------|------|-------------|
| `CityId` | `string` | Unique identifier for the city |
| `CityName` | `string` | Name of the city |
| `ProvinceId` | `string` | ID of the province containing this city |
| `ProvinceName` | `string` | Name of the province containing this city |

## 📝 **Usage Examples**

### Initialization
```csharp
// Initialize the city data (call once at startup)
await CityHelper.InitializeAsync();
```

### Finding Cities
```csharp
// Find city by ID
var tehran = await CityHelper.GetByCityIdAsync("021");

// Find city by name
var shiraz = await CityHelper.GetByCityNameAsync("Shiraz");

// Get all cities in a province
var tehranCities = await CityHelper.GetCitiesByProvinceNameAsync("Tehran");
```

### Validation
```csharp
// Check if city exists
bool exists = await CityHelper.ExistsByCityNameAsync("Isfahan");
```

## ⚠️ **Important Notes**

1. Data Source:
   - Data is loaded from GitHub raw content
   - URL: `provinces_cities.json` in the repository
   - JSON data is cached after first load

2. Performance Considerations:
   - Uses async/await pattern for all operations
   - Data is cached in memory after first load
   - Thread-safe implementation

3. Error Handling:
   - Throws `InvalidOperationException` if JSON loading fails
   - Returns empty collections instead of null for list methods
   - Case-insensitive name comparisons

4. Best Practices:
   - Call `InitializeAsync` at application startup
   - Handle network connectivity issues
   - Consider implementing local fallback data
   - Use ID-based methods for exact matches
   - Use name-based methods for user input

## 🔄 **Dependencies**
- System.Text.Json for JSON deserialization
- HttpClient for remote data fetching
- .NET Standard 2.0 or higher