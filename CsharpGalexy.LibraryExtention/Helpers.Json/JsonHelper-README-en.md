# JsonHelper Class

This documentation covers the `JsonHelper` static class which provides comprehensive functionality for JSON manipulation, parsing, and transformation using System.Text.Json.

## JSON Serialization/Deserialization

| Method | Parameters | Return Type | Description |
|--------|------------|-------------|-------------|
| ToJson | object value, JsonSerializerOptions options | string | Converts an object to JSON string. Returns null on error. |
| ParseTo<T> | string str, JsonSerializerOptions options | T | Deserializes JSON string to type T. Returns default(T) on error. |
| IsValidJson | string text | bool | Validates if a string is valid JSON. |

## Base Conversion

| Method | Parameters | Return Type | Description |
|--------|------------|-------------|-------------|
| ArrayToJson<T> | IEnumerable<T> list, bool pretty | string | Converts array/list to JSON string with optional pretty printing. |
| JsonToArray<T> | string jsonString | List<T> | Parses JSON string to List<T>. Returns empty list on error. |
| JsonToMap | string jsonString | Dictionary<string, JsonNode> | Converts JSON string to dictionary. Returns empty dictionary on error. |

## Formatting & Compression

| Method | Parameters | Return Type | Description |
|--------|------------|-------------|-------------|
| ToPrettyJson<T> | T obj | string | Converts object to human-readable JSON string. |
| FormatJson | string jsonString | string | Pretty prints JSON string with proper indentation. |
| MinifyJson | string jsonString | string | Compresses JSON by removing unnecessary whitespace. |
| EscapeJsonString | string rawString | string | Escapes special characters for JSON string. |

## Access & Extraction

| Method | Parameters | Return Type | Description |
|--------|------------|-------------|-------------|
| DeepGet | string jsonString, string path | JsonNode | Gets nested value using dot notation path. |
| GetString | string jsonString, string path, string defaultValue | string | Extracts string value by path with default value. |
| GetInt | string jsonString, string path, int defaultValue | int | Extracts integer value by path with default value. |
| GetDouble | string jsonString, string path, double defaultValue | double | Extracts double value by path with default value. |
| GetBoolean | string jsonString, string path, bool defaultValue | bool | Extracts boolean value by path with default value. |
| HasKey | string jsonString, string path | bool | Checks if a key exists at specified path. |
| GetJsonArray | string jsonString, string path | JsonArray | Extracts JSON array at specified path. Returns empty array on error. |
| GetJsonObject | string jsonString, string path | JsonObject | Extracts JSON object at specified path. Returns empty object on error. |

## Manipulation & Modification

| Method | Parameters | Return Type | Description |
|--------|------------|-------------|-------------|
| CloneJson<T> | T obj | T | Creates a deep copy of a JSON object. |
| DeepSet | JsonObject jObject, string path, JsonNode value | JsonObject | Sets nested value using dot notation path. |
| UpdateKey | string jsonString, string path, object newValue | string | Updates or adds value at specified path. |
| RemoveKey | string jsonString, string path | string | Removes key at specified path. |
| RemoveNulls | string jsonString | string | Recursively removes all null values. |

## Case Conversion

| Method | Parameters | Return Type | Description |
|--------|------------|-------------|-------------|
| ConvertKeysToCamelCase | string jsonString | string | Converts all JSON keys to camelCase. |
| ConvertKeysToSnakeCase | string jsonString | string | Converts all JSON keys to snake_case. |

## Structural Transformation

| Method | Parameters | Return Type | Description |
|--------|------------|-------------|-------------|
| FlattenJson | string jsonString | Dictionary<string, JsonNode> | Converts nested JSON to flat structure using dot notation. |
| UnflattenJson | Dictionary<string, JsonNode> flattenedMap | string | Converts flat structure back to nested JSON. |

## XML/JSON Conversion

| Method | Parameters | Return Type | Description |
|--------|------------|-------------|-------------|
| XmlToJson | string xmlString | string | Converts XML string to equivalent JSON. |
| JsonToXml | string jsonString, string rootNodeName | string | Converts JSON string to equivalent XML. |

### Usage Examples

```csharp
// Serialization
var json = someObject.ToJson();

// Pretty Print
var formatted = JsonHelper.ToPrettyJson(someObject);

// Deep Access
var value = JsonHelper.GetString(json, "user.address.city");

// Modification
var updated = JsonHelper.UpdateKey(json, "user.email", "new@email.com");

// Case Conversion
var camelCase = JsonHelper.ConvertKeysToCamelCase(json);

// Flatten/Unflatten
var flat = JsonHelper.FlattenJson(json);
var nested = JsonHelper.UnflattenJson(flat);
```

### Error Handling
- Most methods return null, empty collections, or original input on error
- Validation methods return false for invalid input
- Methods are designed to fail gracefully without throwing exceptions

### Performance Notes
- Uses System.Text.Json for better performance
- Caches JsonSerializerOptions for common operations
- Avoids unnecessary parsing when input is invalid