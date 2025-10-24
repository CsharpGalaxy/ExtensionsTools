# RegistryExtensions Class

This documentation covers the `RegistryExtensions` class which provides extension methods for working with the Windows Registry, particularly for retrieving information about installed software.

## Methods

### GetInstalledSoftware

| Parameter | Return Type | Description |
|-----------|-------------|-------------|
| None | List<AppInfo> | Returns a list of installed applications on the system. |

#### Return Value Details (AppInfo Properties)

| Property | Type | Description |
|----------|------|-------------|
| Application | string | Display name of the installed application |
| InstallLocation | string | Installation directory path of the application |

### Technical Details

- **Registry Path**: `SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall`
- **Registry Access**: Uses `Registry.LocalMachine`
- **Key Reading**: Reads from the Uninstall registry key to gather installation information
- **Null Handling**: Filters out entries with null application names

### Usage Example

```csharp
// Get list of installed applications
List<AppInfo> installedApps = RegistryExtensions.GetInstalledSoftware();

// Process the results
foreach(var app in installedApps)
{
    Console.WriteLine($"Application: {app.Application}");
    Console.WriteLine($"Install Location: {app.InstallLocation}");
}
```

### Important Notes

1. **Security Considerations**:
   - Requires appropriate registry access permissions
   - Should be run with sufficient privileges to read HKEY_LOCAL_MACHINE

2. **Platform Compatibility**:
   - Windows-specific functionality
   - Not available on non-Windows platforms

3. **Data Quality**:
   - Some applications might not have complete registry entries
   - InstallLocation might be null or empty for some applications
   - Returns only applications with non-null DisplayName

4. **Performance**:
   - Efficiently uses LINQ for processing registry entries
   - Performs minimal memory allocations
   - Returns filtered results to avoid null references

### Best Practices

1. **Error Handling**:
   ```csharp
   try
   {
       var apps = RegistryExtensions.GetInstalledSoftware();
       // Process the results
   }
   catch (SecurityException)
   {
       // Handle insufficient permissions
   }
   catch (Exception)
   {
       // Handle other potential registry access issues
   }
   ```

2. **Null Checking**:
   ```csharp
   foreach(var app in installedApps)
   {
       if (!string.IsNullOrEmpty(app.InstallLocation))
       {
           // Process installation directory
       }
   }
   ```

### Dependencies

- `Microsoft.Win32` namespace
- `CsharpGalexy.LibraryExtention.Models.IO.AppInfo` class