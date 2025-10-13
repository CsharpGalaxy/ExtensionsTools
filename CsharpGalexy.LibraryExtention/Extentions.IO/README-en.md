# 🔍 Registry Extensions Documentation

## 📋 **Available Methods**
| Method | Description | Parameters | Return Type |
|--------|-------------|------------|-------------|
| `GetInstalledSoftware` | Retrieves information about installed software from Windows Registry | None | `List<AppInfo>` |

## 📦 **AppInfo Structure**
| Property | Type | Description |
|----------|------|-------------|
| `Application` | `string` | Display name of the installed application |
| `InstallLocation` | `string` | Installation directory path |

## 🔍 **Usage Example**

```csharp
// Get list of installed applications
List<AppInfo> installedApps = RegistryExtentions.GetInstalledSoftware();

// Process the results
foreach (var app in installedApps)
{
    Console.WriteLine($"Application: {app.Application}");
    Console.WriteLine($"Install Location: {app.InstallLocation}");
    Console.WriteLine("-------------------");
}
```

## ⚠️ **Important Notes**

1. Registry Access:
   - Reads from `HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall`
   - Requires appropriate registry access permissions
   - Works on Windows operating systems only

2. Security Considerations:
   - Application requires sufficient privileges to read registry
   - Consider running with elevated permissions if needed
   - Handle access exceptions appropriately

3. Return Value Details:
   - Returns only applications with non-null DisplayName
   - InstallLocation might be null for some applications
   - List is filtered to exclude entries without application names

4. Performance:
   - Uses LINQ for efficient registry key processing
   - Implements proper resource disposal with `using` statement
   - Performs single registry key traversal

5. Best Practices:
   - Use in Windows-specific code sections
   - Implement platform checks before calling
   - Handle potential security exceptions
   - Consider caching results if called frequently

## 🔒 **Prerequisites**
- Windows Operating System
- Appropriate registry access permissions
- .NET Framework/Core running with sufficient privileges

## 🛠️ **Technical Implementation**
- Uses `Microsoft.Win32.Registry` namespace
- Implements safe registry key handling
- Utilizes LINQ for data processing
- Implements proper resource cleanup