# ApplicationInfo Class

This documentation covers the `ApplicationInfo` class which provides static properties for accessing assembly information such as version, title, and other metadata.

## Properties

| Property | Type | Description |
|----------|------|-------------|
| Version | Version | Gets the version of the calling assembly |
| Title | string | Gets the title from AssemblyTitleAttribute or executable name |
| ProductName | string | Gets the product name from AssemblyProductAttribute |
| Description | string | Gets the description from AssemblyDescriptionAttribute |
| CopyrightHolder | string | Gets the copyright info from AssemblyCopyrightAttribute |
| CompanyName | string | Gets the company name from AssemblyCompanyAttribute |

## Property Details

### Version
- Returns the version number from the calling assembly
- Uses Assembly.GetCallingAssembly().GetName().Version
- Never returns null (assembly version is required)

### Title
- Primary source: AssemblyTitleAttribute
- Fallback: Executable filename without extension
- Never returns null or empty string

### ProductName
- Source: AssemblyProductAttribute
- Returns empty string if attribute not defined
- No fallback value

### Description
- Source: AssemblyDescriptionAttribute
- Returns empty string if attribute not defined
- No fallback value

### CopyrightHolder
- Source: AssemblyCopyrightAttribute
- Returns empty string if attribute not defined
- No fallback value

### CompanyName
- Source: AssemblyCompanyAttribute
- Returns empty string if attribute not defined
- No fallback value

## Usage Examples

### Basic Usage
```csharp
// Get assembly version
Version version = ApplicationInfo.Version;
Console.WriteLine($"Version: {version}");

// Get application title
string title = ApplicationInfo.Title;
Console.WriteLine($"Title: {title}");
```

### Assembly Information Display
```csharp
// Display full application information
Console.WriteLine($"Product: {ApplicationInfo.ProductName}");
Console.WriteLine($"Company: {ApplicationInfo.CompanyName}");
Console.WriteLine($"Copyright: {ApplicationInfo.CopyrightHolder}");
Console.WriteLine($"Description: {ApplicationInfo.Description}");
```

### Version Comparison
```csharp
if (ApplicationInfo.Version.Major >= 2)
{
    // Handle version 2.0 or higher
}
```

## Important Notes

1. **Assembly Context**:
   - Uses GetCallingAssembly() for all properties
   - Returns information about the assembly calling the property
   - May return different results based on call context

2. **Attribute Dependencies**:
   - Properties rely on assembly attributes
   - Missing attributes return empty strings (except Title)
   - Title has fallback to executable name

3. **Performance Considerations**:
   - Properties cache attribute queries
   - Minimal performance impact
   - Safe for repeated access

## Common Use Cases

1. **Application Information Display**:
   - About screens
   - Help documentation
   - Error logging

2. **Version Management**:
   - Feature toggling
   - Compatibility checks
   - Update verification

3. **Product Branding**:
   - UI display
   - Report generation
   - Documentation

## Dependencies

- System namespace
- System.Reflection namespace