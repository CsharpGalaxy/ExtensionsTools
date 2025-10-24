# ProvinceCapital & ProvinceCapitalHelper Documentation

## 📦 **ProvinceCapital Class**
| Property | Type | Description |
|----------|------|-------------|
| `ProvinceId` | `string` | Unique identifier for the province |
| `ProvinceName` | `string` | Name of the province |
| `Capital` | `string` | Name of the province's capital city |

## 📋 **Initialization Methods**
| Method | Description | Parameters | Return Type |
|--------|-------------|------------|-------------|
| `InitializeAsync` | Initialize and cache province data | None | `Task` |
| `LoadFromJsonAsync` | Load provinces data from JSON file | None | `Task<List<ProvinceCapital>>` |

## 🔍 **Query Methods**
| Method | Description | Parameters | Return Type |
|--------|-------------|------------|-------------|
| `GetAllProvincesAsync` | Get all provinces with their capitals | None | `Task<IReadOnlyList<ProvinceCapital>>` |
| `GetCapitalByProvinceIdAsync` | Get capital city by province ID | `string provinceId` | `Task<string?>` |
| `GetCapitalByProvinceNameAsync` | Get capital city by province name | `string provinceName` | `Task<string?>` |
| `GetProvinceNameByCapitalAsync` | Get province name by capital city | `string capitalName` | `Task<string?>` |

## ✅ **Validation Methods**
| Method | Description | Parameters | Return Type |
|--------|-------------|------------|-------------|
| `ExistsByProvinceIdAsync` | Check if province exists by ID | `string provinceId` | `Task<bool>` |
| `ExistsByProvinceNameAsync` | Check if province exists by name | `string provinceName` | `Task<bool>` |

## 📝 **Usage Examples**

### Initialization
```csharp
// Initialize the province data (call once at startup)
await ProvinceCapitalHelper.InitializeAsync();
```

### Querying Province Data
```csharp
// Get all provinces
var allProvinces = await ProvinceCapitalHelper.GetAllProvincesAsync();

// Find capital by province name
var tehranCapital = await ProvinceCapitalHelper.GetCapitalByProvinceNameAsync("Tehran");
Console.WriteLine(tehranCapital); // Output: "Tehran"

// Find province by capital name
var provinceName = await ProvinceCapitalHelper.GetProvinceNameByCapitalAsync("Shiraz");
Console.WriteLine(provinceName); // Output: "Fars"

// Check if province exists
bool exists = await ProvinceCapitalHelper.ExistsByProvinceNameAsync("Isfahan");
```

## ⚠️ **Important Notes**

1. Data Source:
   - Data is loaded from GitHub raw content
   - URL: `province-capitals.json` in the repository
   - JSON data is cached after first load

2. Performance Considerations:
   - Uses async/await pattern for all operations
   - Data is cached in memory after first load
   - Thread-safe implementation
   - Case-insensitive name comparisons

3. Error Handling:
   - Throws `InvalidOperationException` if JSON loading fails
   - Returns null for non-existent provinces/capitals
   - Empty strings are used as defaults in the model

4. Best Practices:
   - Call `InitializeAsync` at application startup
   - Handle network connectivity issues
   - Use string.Empty instead of null for model properties
   - Implement proper error handling for async operations

## 🔄 **Dependencies**
- System.Text.Json for JSON deserialization
- HttpClient for remote data fetching
- .NET Standard 2.0 or higher

## 🌐 **Data Format Example**
```json
[
  {
    "provinceId": "021",
    "provinceName": "Tehran",
    "capital": "Tehran"
  },
  {
    "provinceId": "031",
    "provinceName": "Isfahan",
    "capital": "Isfahan"
  }
]
```