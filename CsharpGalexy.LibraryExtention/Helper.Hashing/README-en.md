# 🔒 Hashing Helper Documentation

## 🛡️ **Password Security Methods**
| Method | Description | Parameters | Return Type |
|--------|-------------|------------|-------------|
| `GenerateSalt` | Generates a random salt for password hashing | `size` (optional, default: 16): Length of the salt in bytes | `string` (Base64 encoded salt) |
| `HashPassword` | Creates a SHA256 hash of password + salt | `password`: Password to hash<br>`salt`: Salt to use | `string` (hex encoded hash) |
| `ComparePassword` | Securely compares a password with stored hash | `password`: Stored hash<br>`newPassword`: Password to check<br>`sult`: Salt used for hashing | `bool` |

---

## 📝 **Usage Examples**

### Creating a New Password Hash
```csharp
// Generate a new salt
string salt = HashingHelper.GenerateSalt(); // Default 16 bytes
// Or specify custom size
string customSalt = HashingHelper.GenerateSalt(32); // 32 bytes

// Hash a password
string password = "MySecurePassword123";
string hashedPassword = HashingHelper.HashPassword(password, salt);
```

### Verifying a Password
```csharp
string storedHash = "..."; // Previously stored hash
string salt = "...";       // Previously stored salt
string inputPassword = "MySecurePassword123";

bool isValid = HashingHelper.ComparePassword(storedHash, inputPassword, salt);
```

---

## ⚠️ **Security Notes**
- Always store both the salt and hash securely
- Never store plain text passwords
- Use unique salt for each password
- Salt size of 16 bytes (128 bits) is generally sufficient
- Uses SHA256 for hashing which is cryptographically secure
- All operations are using secure random number generation (RNGCryptoServiceProvider)

---

## 🔍 **Technical Details**
- Uses `SHA256` for hashing
- Salt is Base64 encoded
- Final hash is hex encoded (lowercase)
- All operations are performed using UTF-8 encoding
- Uses `RNGCryptoServiceProvider` for cryptographically secure random salt generation