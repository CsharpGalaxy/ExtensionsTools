# DefaultableDictionary<TKey, TValue> Class

This documentation covers the `DefaultableDictionary<TKey, TValue>` class, a dictionary wrapper that returns a default value instead of throwing an exception when a key is not found.

## Class Properties

| Property | Type | Description |
|----------|------|-------------|
| Count | int | Gets the number of elements in the dictionary |
| IsReadOnly | bool | Gets whether the dictionary is read-only |
| Keys | ICollection<TKey> | Gets the collection of keys in the dictionary |
| Values | ICollection<TValue> | Gets the collection of values (includes default value at the end) |

## Constructor

| Parameter | Type | Description |
|-----------|------|-------------|
| dictionary | IDictionary<TKey, TValue> | The underlying dictionary to wrap |
| defaultValue | TValue | The value to return when a key is not found |

## Methods

### Indexer

| Operation | Parameter | Return Type | Description |
|-----------|-----------|-------------|-------------|
| get | TKey | TValue | Returns the value for the key, or default value if key not found |
| set | TKey, TValue | void | Sets the value for the specified key |

### Dictionary Operations

| Method | Parameters | Return Type | Description |
|--------|------------|-------------|-------------|
| Add | TKey key, TValue value | void | Adds an element with the provided key and value |
| Add | KeyValuePair<TKey, TValue> item | void | Adds a key-value pair to the dictionary |
| Clear | - | void | Removes all items from the dictionary |
| Contains | KeyValuePair<TKey, TValue> item | bool | Checks if a specific key-value pair exists |
| ContainsKey | TKey key | bool | Checks if a specific key exists |
| CopyTo | KeyValuePair<TKey, TValue>[] array, int arrayIndex | void | Copies elements to an array |
| Remove | TKey key | bool | Removes the element with the specified key |
| Remove | KeyValuePair<TKey, TValue> item | bool | Removes the specified key-value pair |
| TryGetValue | TKey key, out TValue value | bool | Gets value for key, returns default if not found |

## Usage Examples

### Basic Usage
```csharp
var dict = new Dictionary<string, int>();
var defaultDict = new DefaultableDictionary<string, int>(dict, -1);

// Adding values
defaultDict["one"] = 1;
defaultDict["two"] = 2;

// Getting values
int value1 = defaultDict["one"];     // Returns 1
int value3 = defaultDict["three"];   // Returns -1 (default value)
```

### Using TryGetValue
```csharp
var defaultDict = new DefaultableDictionary<string, string>(
    new Dictionary<string, string>(), 
    "Not Found"
);

defaultDict.TryGetValue("missing", out string result);
// result will be "Not Found" instead of throwing exception
```

### Working with Collections
```csharp
var defaultDict = new DefaultableDictionary<int, string>(
    new Dictionary<int, string>(), 
    "Default"
);

defaultDict[1] = "One";
defaultDict[2] = "Two";

// Keys collection
foreach(var key in defaultDict.Keys)
{
    Console.WriteLine(key);
}

// Values collection (includes default value)
foreach(var value in defaultDict.Values)
{
    Console.WriteLine(value);
}
```

## Important Notes

1. **Default Value Behavior**:
   - Never throws KeyNotFoundException
   - Always returns default value for missing keys
   - TryGetValue always returns true
   - Values collection includes default value

2. **Implementation Details**:
   - Implements IDictionary<TKey, TValue>
   - Wraps an existing dictionary
   - Preserves original dictionary behavior for existing keys

3. **Performance Considerations**:
   - Same performance as underlying dictionary for existing keys
   - No additional overhead for missing keys
   - Values property creates new list on each access

## Dependencies

- System.Collections namespace
- System.Collections.Generic namespace