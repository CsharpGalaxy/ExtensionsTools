# BoolHelper and BoolExtensions Classes

This documentation covers the `BoolHelper` static class and `BoolExtensions` extension methods that provide comprehensive functionality for boolean operations.

## BoolExtensions Methods

### Basic & Inverse Operations

| Method | Parameters | Return Type | Description |
|--------|------------|-------------|-------------|
| Not | this bool value | bool | Negates the boolean value. |
| Toggle | this bool value | bool | Switches between true and false. |
| Negate | this bool value | bool | Alternative name for Not operation. |

### Two-Boolean Operations

| Method | Parameters | Return Type | Description |
|--------|------------|-------------|-------------|
| Xor | this bool a, bool b | bool | Performs XOR operation between two booleans. |
| Nand | this bool a, bool b | bool | Performs NAND (NOT AND) operation. |
| Nor | this bool a, bool b | bool | Performs NOR (NOT OR) operation. |
| Xnor | this bool a, bool b | bool | Performs XNOR (equality) operation. |
| Implies | this bool a, bool b | bool | Logical implication (A → B). |
| Iff | this bool a, bool b | bool | Logical equivalence (A ↔ B). |
| Equals | this bool a, bool b | bool | Compares two booleans for equality. |
| Compare | this bool a, bool b | int | Compares booleans, returns -1, 0, or 1. |

### Checking Operations

| Method | Parameters | Return Type | Description |
|--------|------------|-------------|-------------|
| IsTrue | this bool value | bool | Checks if value equals true. |
| IsFalse | this bool value | bool | Checks if value equals false. |

### Conversion Operations

| Method | Parameters | Return Type | Description |
|--------|------------|-------------|-------------|
| ToInt | this bool value | int | Converts true→1, false→0. |
| ToStringYN | this bool value | string | Returns "Y"/"N". |
| ToString10 | this bool value | string | Returns "1"/"0". |
| ToStringOnOff | this bool value | string | Returns "ON"/"OFF". |
| ToStringYesNo | this bool value | string | Returns "Yes"/"No". |

## BoolHelper Methods

### Collection Operations

| Method | Parameters | Return Type | Description |
|--------|------------|-------------|-------------|
| And | params bool[] values | bool | Logical AND of multiple booleans. |
| Or | params bool[] values | bool | Logical OR of multiple booleans. |
| AllTrue | IEnumerable<bool> values | bool | Checks if all elements are true. |
| AllFalse | IEnumerable<bool> values | bool | Checks if all elements are false. |
| AnyTrue | IEnumerable<bool> values | bool | Checks if at least one true exists. |
| AnyFalse | IEnumerable<bool> values | bool | Checks if at least one false exists. |
| CountTrue | IEnumerable<bool> values | int | Counts number of true values. |
| CountFalse | IEnumerable<bool> values | int | Counts number of false values. |

### Parsing & Random Generation

| Method | Parameters | Return Type | Description |
|--------|------------|-------------|-------------|
| FromInt | int value | bool | Converts 0→false, non-0→true. |
| ParseLenient | string s | bool | Case-insensitive parsing of various boolean strings. |
| Random | none | bool | Generates random boolean value. |
| RandomWithProbability | double probability | bool | Generates true with given probability (0...1). |

### Usage Examples

```csharp
// BoolExtensions examples
bool value = true;
bool negated = value.Not();          // false
bool toggled = value.Toggle();       // false
int asInt = value.ToInt();          // 1
string asYN = value.ToStringYN();    // "Y"

// Logic operations
bool a = true, b = false;
bool xor = a.Xor(b);                // true
bool implies = a.Implies(b);         // false

// BoolHelper examples
bool allTrue = BoolHelper.AllTrue(new[] { true, true });     // true
bool anyFalse = BoolHelper.AnyFalse(new[] { true, false }); // true
int trueCount = BoolHelper.CountTrue(new[] { true, false }); // 1

// Parsing and random
bool parsed = BoolHelper.ParseLenient("yes");                // true
bool random = BoolHelper.RandomWithProbability(0.7);         // 70% chance of true
```

### Performance Notes

- Most operations are optimized with AggressiveInlining
- Collection operations handle null inputs safely
- String conversions are optimized for common cases