# IntHelper and IntExtensions Classes

This documentation covers the `IntHelper` static class and `IntExtensions` extension methods that provide comprehensive functionality for integer operations.

## IntExtensions Methods

### Basic & Mathematical Operations

| Method | Parameters | Return Type | Description |
|--------|------------|-------------|-------------|
| Abs | int number | int | Returns the absolute value of a number. |
| Factorial | int n | long | Calculates factorial for small numbers (returns long). |
| Mod | int a, int n | int | Performs modulo operation that always returns positive result. |
| Sign | int number | int | Returns the sign of number: -1, 0, or +1. |
| SqrtInt | int n | int | Calculates integer square root (returns int). |

### Checking & Clamping Operations

| Method | Parameters | Return Type | Description |
|--------|------------|-------------|-------------|
| IsBetween | int number, int min, int max | bool | Checks if number is in range (min, max). |
| Clamp | int number, int min, int max | int | Clamps number within specified range. |
| Cycle | int number, int min, int max | int | Cycles number within range (loop-back). |
| IsEven | int number | bool | Checks if number is even. |
| IsOdd | int number | bool | Checks if number is odd. |
| IsPowerOfTwo | int number | bool | Checks if number is a power of two. |
| IsPrime | int number | bool | Tests if number is prime. |
| RealValue | int? number | int | Returns the value of nullable int or 0 if null. |

### Bitwise & Binary Operations

| Method | Parameters | Return Type | Description |
|--------|------------|-------------|-------------|
| BitCount | int number | int | Counts number of 1 bits in binary form. |
| CeilToPowerOfTwo | int n | int | Returns smallest power of 2 that is ≥ input. |
| HammingDistance | int a, int b | int | Calculates Hamming distance between two integers. |
| Log2 | int number | int | Calculates base-2 logarithm. |
| Log10 | int number | int | Calculates base-10 logarithm. |
| RotateLeft | int value, int offset | int | Rotates bits left. |
| RotateRight | int value, int offset | int | Rotates bits right. |

### Byte & Endianness Operations

| Method | Parameters | Return Type | Description |
|--------|------------|-------------|-------------|
| ToBytesBE | int value | byte[] | Converts int to 4 bytes in Big-Endian. |
| ToBytesLE | int value | byte[] | Converts int to 4 bytes in Little-Endian. |

### String & Digit Conversion

| Method | Parameters | Return Type | Description |
|--------|------------|-------------|-------------|
| ReverseDigits | int number | int | Reverses digits of number (123 → 321). |
| ToBinaryStringPadded | int number | string | Returns 32-character binary string. |
| ToHexStringPadded | int number | string | Returns 8-character hex string. |
| ToOctalString | int number | string | Returns octal string representation. |

## IntHelper Methods

### Two-Number Operations

| Method | Parameters | Return Type | Description |
|--------|------------|-------------|-------------|
| AddClamped | int a, int b | int | Adds two numbers with clamping to MIN/MAX. |
| DivideRoundUp | int dividend, int divisor | int | Divides and rounds up to ceiling. |
| Gcd | int a, int b | int | Calculates Greatest Common Divisor. |
| Lcm | int a, int b | long | Calculates Least Common Multiple. |
| MultiplyClamped | int a, int b | int | Multiplies with clamping to MIN/MAX. |
| Pow | int base, int exponent | long | Calculates integer power. |
| SubtractClamped | int a, int b | int | Subtracts with clamping to MIN/MAX. |
| Swap | ref int a, ref int b | void | Swaps two int values without temp variable. |

### Safe Arithmetic

| Method | Parameters | Return Type | Description |
|--------|------------|-------------|-------------|
| SafeAdd | int a, int b | int | Addition with overflow exception. |
| SafeMultiply | int a, int b | int | Multiplication with overflow exception. |
| SafeSubtract | int a, int b | int | Subtraction with overflow exception. |

### Array Operations

| Method | Parameters | Return Type | Description |
|--------|------------|-------------|-------------|
| Average | params int[] numbers | int | Calculates average without overflow. |
| Max | params int[] numbers | int | Returns maximum value. |
| Min | params int[] numbers | int | Returns minimum value. |
| SumArray | params int[] numbers | long | Sums array without overflow. |
| ToCommaSeparatedString | params int[] numbers | string | Converts array to comma-separated string. |

### Byte/Endianness Operations

| Method | Parameters | Return Type | Description |
|--------|------------|-------------|-------------|
| BytesToIntBE | byte[] bytes | int | Converts 4 bytes to int in Big-Endian. |
| BytesToIntLE | byte[] bytes | int | Converts 4 bytes to int in Little-Endian. |

### Parsing & Random

| Method | Parameters | Return Type | Description |
|--------|------------|-------------|-------------|
| NextRandomInt | int min, int max | int | Generates random int in range. |
| ParseIntOrDefault | string s, int defaultValue | int | Parses string or returns default. |
| ParseIntOrNull | string s | int? | Parses string or returns null. |

### Usage Examples

```csharp
// IntExtensions examples
int num = 123;
bool isEven = num.IsEven();
int abs = num.Abs();
int factorial = 5.Factorial();
string binary = num.ToBinaryStringPadded();

// IntHelper examples
int sum = IntHelper.AddClamped(int.MaxValue, 1);
int gcd = IntHelper.Gcd(24, 36);
int random = IntHelper.NextRandomInt(1, 100);
int? parsed = IntHelper.ParseIntOrNull("123");
```

### Performance Notes
- Most operations are optimized with AggressiveInlining
- Bit operations use efficient BitOperations class
- Array operations handle overflow correctly