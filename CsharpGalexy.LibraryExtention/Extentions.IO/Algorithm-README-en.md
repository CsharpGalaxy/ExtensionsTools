# Algorithm Class

This documentation covers the `Algorithm` class which provides implementations of various algorithms, currently focusing on the Luhn algorithm for validation of identification numbers.

## Methods

### Luhn

| Parameter | Type | Description |
|-----------|------|-------------|
| value | string | The number to validate using the Luhn algorithm |
| Returns | byte | 0 if the number is valid, otherwise returns the check digit needed to make it valid |

## About Luhn Algorithm

The Luhn algorithm (also known as the "modulus 10" or "mod 10" algorithm) was created by IBM scientist Hans Peter Luhn. It is a checksum formula used to validate various identification numbers, including:

- Credit card numbers
- IMEI numbers
- Canadian Social Insurance Numbers
- Israel ID Numbers
- South African ID Numbers
- Greek Social Security Numbers (ΑΜΚΑ)

## Algorithm Process

1. Starting from the rightmost digit (excluding the check digit):
   - Double the value of every second digit
   - If doubling results in a number > 9, subtract 9

2. Sum all digits (both doubled and untouched)

3. If the total modulo 10 is 0, the number is valid

## Usage Examples

### Validating a Number
```csharp
// Example with a credit card number (without check digit)
string cardNumber = "7992739871";
byte checkDigit = Algorithm.Luhn(cardNumber);

if (checkDigit == 0)
{
    Console.WriteLine("Number is valid");
}
else
{
    Console.WriteLine($"Check digit should be: {checkDigit}");
}
```

### Generating Check Digit
```csharp
// Generate check digit for a number
string baseNumber = "79927398713";
byte checkDigit = Algorithm.Luhn(baseNumber);
string completeNumber = baseNumber + checkDigit;
```

## Important Notes

1. **Input Format**:
   - Input should be a string containing only numeric characters
   - No spaces or special characters are handled
   - Length of the input is not validated

2. **Return Values**:
   - Returns 0 if the number is valid
   - Returns 1-9 indicating the check digit needed to make the number valid

3. **Performance**:
   - Time complexity: O(n) where n is the length of the input string
   - Space complexity: O(1) as it uses constant extra space

## Technical Details

### Implementation Notes

- Uses `Convert.ToByte` for numeric conversions
- Processes digits right to left
- Alternates between doubling and keeping original values
- Handles doubled values > 9 by subtracting 9
- Uses modulo 10 for final validation

### Error Handling

The method assumes valid input and does not include explicit error handling for:
- Non-numeric characters
- Empty strings
- Null values

Consider adding validation if using in production code.

## Dependencies

- System namespace (for Convert class)