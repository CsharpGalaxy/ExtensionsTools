# DefaultableDictionaryExtensions Class

This documentation covers the `DefaultableDictionaryExtensions` class which provides extension methods for creating defaultable dictionaries.

## Extension Methods

### WithDefaultValue<TValue, TKey>

| Parameter | Type | Description |
|-----------|------|-------------|
| this dictionary | IDictionary<TKey, TValue> | The dictionary to extend |
| defaultValue | TValue | The default value to return for missing keys |
| Returns | IDictionary<TKey, TValue> | A new DefaultableDictionary wrapping the original dictionary |

## Usage Examples

### Basic Usage
```csharp
var dictionary = new Dictionary<string, int>();
var defaultableDictionary = dictionary.WithDefaultValue(-1);

// Now accessing missing keys returns -1 instead of throwing
int value = defaultableDictionary["nonexistent"]; // Returns -1
```

### With Different Types
```csharp
// With string values
var stringDict = new Dictionary<int, string>();
var defaultStringDict = stringDict.WithDefaultValue("Not Found");

// With custom objects
var userDict = new Dictionary<string, User>();
var defaultUserDict = userDict.WithDefaultValue(User.Guest);

// With nullable types
var nullableDict = new Dictionary<string, int?>();
var defaultNullDict = nullableDict.WithDefaultValue(null);
```

### Chaining with Other LINQ Operations
```csharp
var result = dictionary
    .WithDefaultValue(0)
    .Where(kvp => kvp.Value > 10)
    .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
```

## Important Notes

1. **Usage Context**:
   - Extension method for any IDictionary<TKey, TValue>
   - Creates a new wrapper instance
   - Original dictionary remains unchanged

2. **Type Compatibility**:
   - Works with any value type or reference type
   - Default value must match dictionary value type
   - Preserves generic type constraints

3. **Performance Considerations**:
   - Minimal overhead for creation
   - Same performance characteristics as DefaultableDictionary
   - No additional memory allocation for existing entries

## Dependencies

- DefaultableDictionary<TKey, TValue> class
- System.Collections.Generic namespace