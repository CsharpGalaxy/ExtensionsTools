# DictionaryExtensions Class

This documentation covers the `DictionaryExtensions` class which provides useful extension methods for working with dictionaries.

## Extension Methods

### GetKeyFromValue<TKey, TValue>

| Parameter | Type | Description |
|-----------|------|-------------|
| this dictionary | IDictionary<TKey, TValue> | The dictionary to search |
| value | TValue | The value to find |
| Returns | TKey | The matching key if found; otherwise default value of TKey |

### GetValueFromKey<TKey, TValue>

| Parameter | Type | Description |
|-----------|------|-------------|
| this dictionary | IDictionary<TKey, TValue> | The dictionary to search |
| key | TKey | The key to find |
| Returns | TValue | The matching value if found; otherwise default value of TValue |

### CheckDictionaryIsNull<TKey, TValue>

| Parameter | Type | Description |
|-----------|------|-------------|
| this dictionary | IDictionary<TKey, TValue> | The dictionary to check |
| Returns | bool | true if dictionary is null; otherwise false |

### AddIfNotExists<TKey, TValue>

| Parameter | Type | Description |
|-----------|------|-------------|
| this dictionary | Dictionary<TKey, TValue> | The dictionary to modify |
| key | TKey | The key to add |
| value | TValue | The value to add |
| Returns | bool | true if key was added; false if key exists or dictionary is null |

### DeleteIfExistsKey<TKey, TValue>

| Parameter | Type | Description |
|-----------|------|-------------|
| this dictionary | Dictionary<TKey, TValue> | The dictionary to modify |
| key | TKey | The key to remove |
| Returns | bool | true if key was found and removed; otherwise false |

### Update<TKey, TValue>

| Parameter | Type | Description |
|-----------|------|-------------|
| this dictionary | Dictionary<TKey, TValue> | The dictionary to modify |
| key | TKey | The key to update |
| value | TValue | The new value to set |
| Returns | bool | true if value was updated; otherwise false |

## Usage Examples

### Finding Keys and Values
```csharp
var dict = new Dictionary<string, int> { {"one", 1}, {"two", 2} };

// Find key by value
string key = dict.GetKeyFromValue(2); // Returns "two"

// Find value by key
int value = dict.GetValueFromKey("one"); // Returns 1
```

### Adding and Updating Entries
```csharp
var dict = new Dictionary<string, int>();

// Add new entry if key doesn't exist
bool added = dict.AddIfNotExists("three", 3); // Returns true

// Update existing entry
bool updated = dict.Update("three", 30); // Returns true
```

### Removing Entries
```csharp
var dict = new Dictionary<string, int> { {"temp", 0} };

// Remove entry if key exists
bool removed = dict.DeleteIfExistsKey("temp"); // Returns true
```

### Null Checking
```csharp
Dictionary<string, int> dict = null;

// Check if dictionary is null
bool isNull = dict.CheckDictionaryIsNull(); // Returns true
```

## Important Notes

1. **Null Handling**:
   - Methods include null checks for dictionary instances
   - Update method checks for null keys and values
   - Safe handling of null dictionaries

2. **Return Values**:
   - GetKeyFromValue returns default(TKey) if value not found
   - GetValueFromKey returns default(TValue) if key not found
   - Boolean methods return false for invalid operations

3. **Performance Considerations**:
   - GetKeyFromValue and GetValueFromKey use LINQ FirstOrDefault
   - Other methods use direct dictionary operations
   - Null checks add minimal overhead

## Dependencies

- System.Collections.Generic namespace
- System.Linq namespace