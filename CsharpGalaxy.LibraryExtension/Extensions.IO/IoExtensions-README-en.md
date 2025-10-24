# IoExtensions Class

This documentation covers the `IoExtensions` class which provides extension methods for file and folder operations, including JSON loading, text appending, copying, deletion, and string counting.

## Methods

### LoadJson<T>

| Parameter | Type | Description |
|-----------|------|-------------|
| jsonPath | string | The full path to the JSON file |
| T | generic | The type to deserialize the JSON into |
| Returns | T | The deserialized object if successful; otherwise the default value of T |

### AppendText

| Parameter | Type | Description |
|-----------|------|-------------|
| path | string | The path of the file |
| text | string | The text to append |
| Returns | void | - |

### CopyFolder

| Parameter | Type | Description |
|-----------|------|-------------|
| sourcePath | string | The source folder path |
| targetPath | string | The target folder path |
| Returns | bool | true if the folder exists and files were copied; otherwise false |

### DeleteFolder

| Parameter | Type | Description |
|-----------|------|-------------|
| dir | string | The folder path |
| recursivo | bool | If true, deletes the folder recursively including all subfolders and files |
| Returns | bool | true if the folder existed and was deleted; otherwise false |

### CountStr

| Parameter | Type | Description |
|-----------|------|-------------|
| file | string | The file path to search in |
| str | string | The string to count |
| Returns | int | The number of occurrences of the string in the file |

## Usage Examples

### Loading JSON File
```csharp
// Load a configuration file into a Config class
var config = IoExtensions.LoadJson<Config>("config.json");
if (config != null)
{
    // Use the config object
}
```

### Appending Text
```csharp
// Append a log entry to a file
IoExtensions.AppendText("log.txt", "New log entry: " + DateTime.Now.ToString());
```

### Copying Folders
```csharp
// Copy source folder to backup location
bool success = IoExtensions.CopyFolder(@"C:\SourceFolder", @"C:\Backup\SourceFolder");
if (success)
{
    Console.WriteLine("Backup completed successfully");
}
```

### Deleting Folders
```csharp
// Delete a temporary folder and all its contents
bool deleted = IoExtensions.DeleteFolder(@"C:\TempFolder", true);
if (deleted)
{
    Console.WriteLine("Temporary folder cleaned up");
}
```

### Counting String Occurrences
```csharp
// Count how many times "ERROR" appears in a log file
int errorCount = IoExtensions.CountStr("application.log", "ERROR");
Console.WriteLine($"Found {errorCount} errors in log");
```

## Important Notes

1. **Error Handling**:
   - `LoadJson<T>` includes built-in exception handling and returns default(T) on failure
   - Other methods may throw standard IO exceptions that should be handled by the caller

2. **File Operations**:
   - `AppendText` will create the file if it doesn't exist
   - `CopyFolder` overwrites existing files in the target directory
   - `DeleteFolder` with recursivo=true will delete all contents without confirmation

3. **Performance Considerations**:
   - `CountStr` reads the entire file into memory
   - `CopyFolder` performs synchronous file operations

## Dependencies

- System.IO namespace
- System.Text.RegularExpressions namespace
- CsharpGalexy.LibraryExtention.Helpers.Json namespace