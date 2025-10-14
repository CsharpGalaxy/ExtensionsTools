# Compression Class

This documentation covers the `Compression` class which provides enhanced functionality for working with ZIP archives, including file extraction and compression with various overwrite policies.

## Enums

### Overwrite
Specifies the overwrite policy for file extraction.

| Value | Description |
|-------|-------------|
| Always | Always overwrite existing files |
| IfNewer | Overwrite only if the archive file is newer |
| Never | Never overwrite existing files |

### ArchiveAction
Specifies the action to take when creating/updating ZIP archives.

| Value | Description |
|-------|-------------|
| Merge | Add files to existing archive |
| Replace | Delete existing archive and create new |
| Error | Throw error if archive exists |
| Ignore | Do nothing if archive exists |

## Methods

### ImprovedExtractToDirectory

| Parameter | Type | Description |
|-----------|------|-------------|
| sourceArchiveFileName | string | The path to the ZIP file to extract |
| destinationDirectoryName | string | The directory where files will be extracted |
| overwriteMethod | Overwrite | How to handle existing files (default: IfNewer) |
| Returns | void | - |

### ImprovedExtractToFile

| Parameter | Type | Description |
|-----------|------|-------------|
| file | ZipArchiveEntry | The ZIP entry to extract |
| destinationPath | string | The root path for extraction |
| overwriteMethod | Overwrite | How to handle existing files (default: IfNewer) |
| Returns | void | - |

### AddToArchive

| Parameter | Type | Description |
|-----------|------|-------------|
| archiveFullName | string | Path to the ZIP archive |
| files | List<string> | List of files to add |
| action | ArchiveAction | How to handle existing archive (default: Replace) |
| fileOverwrite | Overwrite | How to handle existing files (default: IfNewer) |
| compression | CompressionLevel | Compression level to use (default: Optimal) |
| Returns | void | - |

## Usage Examples

### Extracting Files
```csharp
// Extract all files, overwriting only if newer
Compression.ImprovedExtractToDirectory(
    "archive.zip",
    @"C:\ExtractPath",
    Compression.Overwrite.IfNewer
);

// Extract all files, always overwriting
Compression.ImprovedExtractToDirectory(
    "archive.zip",
    @"C:\ExtractPath",
    Compression.Overwrite.Always
);
```

### Creating/Updating Archives
```csharp
// Create a new archive
var files = new List<string> { 
    @"C:\files\doc1.pdf", 
    @"C:\files\doc2.pdf" 
};

// Create new or merge with existing
Compression.AddToArchive(
    "documents.zip",
    files,
    Compression.ArchiveAction.Merge,
    Compression.Overwrite.IfNewer,
    CompressionLevel.Optimal
);

// Create new archive, replacing if exists
Compression.AddToArchive(
    "documents.zip",
    files,
    Compression.ArchiveAction.Replace
);
```

## Important Notes

1. **File Safety**:
   - Methods handle missing paths and create directories as needed
   - Multiple overwrite policies protect against unintended file loss
   - Timestamp checks ensure data integrity when using IfNewer option

2. **Archive Management**:
   - Supports both creating new and updating existing archives
   - Can merge files into existing archives
   - Handles file naming conflicts through overwrite policies

3. **Performance**:
   - Uses optimal compression by default
   - Efficient handling of large files through streaming
   - Minimal memory footprint for large archives

## Dependencies

- System.IO.Compression namespace
- System.IO namespace
- System.Collections.Generic namespace
- System.Linq namespace