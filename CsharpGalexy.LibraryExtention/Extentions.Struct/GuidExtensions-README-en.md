# GuidExtensions Class

This documentation covers the `GuidExtensions` class which provides extension methods for the `Guid` structure to enhance its functionality and usability.

## Methods

### Conversion Methods

| Method | Parameters | Return Type | Description |
|--------|------------|-------------|-------------|
| ToShortString | this Guid guid | string | Converts GUID to a shorter, URL-friendly string using Base64 encoding. |
| ToGuid | this string shortString | Guid | Converts a URL-safe Base64 string back to a GUID. |
| ToCleanString | this Guid guid | string | Returns a 32-character lowercase hexadecimal string without hyphens. |

### Validation Methods

| Method | Parameters | Return Type | Description |
|--------|------------|-------------|-------------|
| IsEmpty | this Guid guid | bool | Checks whether the GUID is empty (Guid.Empty). |
| TryParseGuid | this string input, out Guid result | bool | Attempts to parse a string into a GUID. |

### Generation Methods

| Method | Parameters | Return Type | Description |
|--------|------------|-------------|-------------|
| DeriveGuid | this Guid guid, string salt | Guid | Generates a new deterministic GUID based on parent GUID and salt string. |

### Default Value Handling

| Method | Parameters | Return Type | Description |
|--------|------------|-------------|-------------|
| OrDefault | this Guid guid | Guid | Returns the GUID if not empty; otherwise, returns Guid.Empty. |
| OrDefault | this Guid guid, Guid defaultValue | Guid | Returns the GUID if not empty; otherwise, returns specified default value. |
| OrDefault | this Guid? guid | Guid | Returns the nullable GUID's value if has value and not empty; otherwise, returns Guid.Empty. |
| OrDefault | this Guid? guid, Guid defaultValue | Guid | Returns the nullable GUID's value if has value and not empty; otherwise, returns specified default value. |

## Usage Examples

### Short String Conversion
```csharp
// Convert GUID to short string
var guid = Guid.NewGuid();
string shortGuid = guid.ToShortString(); // e.g., "XyZ12-AbC34dEfG56H"

// Convert back to GUID
Guid reconstructed = shortGuid.ToGuid();
```

### Clean String Format
```csharp
var guid = new Guid("123e4567-e89b-12d3-a456-426614174000");
string clean = guid.ToCleanString(); // "123e4567e89b12d3a456426614174000"
```

### Deterministic GUID Generation
```csharp
var parent = Guid.NewGuid();
var child = parent.DeriveGuid("UserProfile");
```

### Validation and Parsing
```csharp
var guid = Guid.Empty;
bool isEmpty = guid.IsEmpty(); // true

string input = "123e4567-e89b-12d3-a456-426614174000";
if (input.TryParseGuid(out var parsedGuid))
{
    Console.WriteLine($"Parsed: {parsedGuid}");
}
```

### Default Value Handling
```csharp
Guid? nullableGuid = null;
Guid safe = nullableGuid.OrDefault(); // returns Guid.Empty

Guid empty = Guid.Empty;
Guid fallback = Guid.NewGuid();
Guid result = empty.OrDefault(fallback); // returns fallback
```

## Error Handling

- `ToGuid`: Throws `ArgumentException` if input string is null, empty, or invalid format
- `DeriveGuid`: Throws `ArgumentNullException` if salt is null
- Other methods handle errors gracefully by returning default values or false

## Performance Considerations

- Most methods are designed for efficient operation
- `DeriveGuid` uses SHA1 for deterministic generation
- String operations use optimized .NET Core methods