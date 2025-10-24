# 📁 File Helper Documentation

## 🔍 **File Validation Methods**
| Method | Description | Parameters | Return Type |
|--------|-------------|------------|-------------|
| `GetContentType` | Gets MIME type for file extension | `path: string` | `string` |
| `IsValidFile` | Validates file by type and content | `bytFile: byte[], flType: FileType, fileContentType: string` | `bool` |
| `IsValidImageFile` | Validates image files (JPG, PNG, BMP, GIF) | `bytFile: byte[], fileContentType: string` | `bool` |
| `IsValidVideoFile` | Validates video files (WMV, AVI, FLV, MPG, MP4) | `bytFile: byte[], fileContentType: string` | `bool` |
| `IsValidPdfFile` | Validates PDF files | `bytFile: byte[], fileContentType: string` | `bool` |
| `IsValidDocDocxFile` | Validates DOC/DOCX files | `bytFile: byte[], fileContentType: string` | `bool` |
| `IsValidZipRarFile` | Validates ZIP/RAR archives | `bytFile: byte[], fileContentType: string` | `bool` |

## 📤 **File Upload Methods**
| Method | Description | Parameters | Return Type |
|--------|-------------|------------|-------------|
| `UploadFileAsync` | Uploads and validates file asynchronously | `stream: Stream, fileName: string, type: FileType, path: string, generateNewFileName: bool` | `Task<UploadFileResult>` |
| `UploadFileBase64` | Uploads file from base64 string | `base64: string, path: string` | `void` |
| `SaveBase64File` | Saves base64 file with MIME type detection | `base64WithPrefix: string, outputDir: string, relativeRoot: string` | `string` |
| `SaveBase64FileNotMIMEtype` | Saves base64 file with specified extension | `base64WithPrefix: string, extension: FileExtension, outputDir: string, relativeRoot: string` | `string` |

## 🗑️ **File Deletion Methods**
| Method | Description | Parameters | Return Type |
|--------|-------------|------------|-------------|
| `DeleteFile` | Deletes file from relative path | `relativePath: string, relativeRoot: string` | `bool` |

## 🔄 **Utility Methods**
| Method | Description | Parameters | Return Type |
|--------|-------------|------------|-------------|
| `GetFileTypeFromExtension` | Determines FileType from extension | `extension: string` | `FileType` |
| `SafeBase64Decode` | Safely decodes base64 strings | `base64String: string` | `byte[]` |
| `GetFileExtensionFromBase64` | Extracts extension from base64 data | `base64WithPrefix: string` | `string` |
| `GetFilePathUploadFile` | Gets upload path for file type | `type: string` | `string` |

## 📦 **Supported File Types**
```csharp
public enum FileType
{
    Image,  // .jpg, .jpeg, .png, .gif, .bmp
    Video,  // .wmv, .mpg, .mpeg, .mp4, .avi, .flv
    PDF,    // .pdf
    DOC,    // .doc
    DOCX,   // .docx
    ZIP,    // .zip
    RAR,    // .rar
    Text    // default
}
```

## 📝 **Usage Examples**

### File Upload
```csharp
// Upload file asynchronously
var result = await FileHelper.UploadFileAsync(
    fileStream,
    "document.pdf",
    FileType.PDF,
    "uploads/documents",
    generateNewFileName: true
);

if (result.IsSuccess)
    Console.WriteLine($"File uploaded as: {result.NewFileName}");
else
    Console.WriteLine($"Errors: {string.Join(", ", result.Errors)}");

// Upload base64 file
string base64Data = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAA...";
string path = FileHelper.SaveBase64File(base64Data, "uploads/images");
```

### File Validation
```csharp
// Validate PDF file
byte[] fileBytes = File.ReadAllBytes("document.pdf");
bool isValid = ValidateFiles.IsValidPdfFile(fileBytes, "application/pdf");

// Get file type from extension
string extension = ".jpg";
FileType fileType = extension.GetFileTypeFromExtension();
```

## ⚠️ **Important Notes**

1. File Validation:
   - Validates file signatures (magic numbers)
   - Supports common file formats
   - Thread-safe implementation

2. Upload Safety:
   - Creates directories if they don't exist
   - Generates unique filenames when requested
   - Validates file content before saving

3. Base64 Handling:
   - Supports MIME type detection
   - Handles data URL format
   - Safely removes padding and whitespace

4. Error Handling:
   - Returns detailed errors in debug mode
   - Sanitized error messages in production
   - Thread-safe operations