# 📁 مستندات کلاس‌های کار با فایل

## 🔍 **متدهای اعتبارسنجی فایل**
| متد | توضیحات | پارامترها | نوع خروجی |
|-----|----------|-----------|------------|
| `GetContentType` | دریافت نوع MIME برای پسوند فایل | `path: string` | `string` |
| `IsValidFile` | اعتبارسنجی فایل بر اساس نوع و محتوا | `bytFile: byte[], flType: FileType, fileContentType: string` | `bool` |
| `IsValidImageFile` | اعتبارسنجی فایل‌های تصویری (JPG, PNG, BMP, GIF) | `bytFile: byte[], fileContentType: string` | `bool` |
| `IsValidVideoFile` | اعتبارسنجی فایل‌های ویدئویی (WMV, AVI, FLV, MPG, MP4) | `bytFile: byte[], fileContentType: string` | `bool` |
| `IsValidPdfFile` | اعتبارسنجی فایل‌های PDF | `bytFile: byte[], fileContentType: string` | `bool` |
| `IsValidDocDocxFile` | اعتبارسنجی فایل‌های DOC/DOCX | `bytFile: byte[], fileContentType: string` | `bool` |
| `IsValidZipRarFile` | اعتبارسنجی فایل‌های فشرده ZIP/RAR | `bytFile: byte[], fileContentType: string` | `bool` |

## 📤 **متدهای آپلود فایل**
| متد | توضیحات | پارامترها | نوع خروجی |
|-----|----------|-----------|------------|
| `UploadFileAsync` | آپلود و اعتبارسنجی فایل به صورت ناهمزمان | `stream: Stream, fileName: string, type: FileType, path: string, generateNewFileName: bool` | `Task<UploadFileResult>` |
| `UploadFileBase64` | آپلود فایل از رشته base64 | `base64: string, path: string` | `void` |
| `SaveBase64File` | ذخیره فایل base64 با تشخیص نوع MIME | `base64WithPrefix: string, outputDir: string, relativeRoot: string` | `string` |
| `SaveBase64FileNotMIMEtype` | ذخیره فایل base64 با پسوند مشخص | `base64WithPrefix: string, extension: FileExtension, outputDir: string, relativeRoot: string` | `string` |

## 🗑️ **متدهای حذف فایل**
| متد | توضیحات | پارامترها | نوع خروجی |
|-----|----------|-----------|------------|
| `DeleteFile` | حذف فایل از مسیر نسبی | `relativePath: string, relativeRoot: string` | `bool` |

## 🔄 **متدهای کمکی**
| متد | توضیحات | پارامترها | نوع خروجی |
|-----|----------|-----------|------------|
| `GetFileTypeFromExtension` | تشخیص نوع فایل از پسوند | `extension: string` | `FileType` |
| `SafeBase64Decode` | رمزگشایی ایمن رشته‌های base64 | `base64String: string` | `byte[]` |
| `GetFileExtensionFromBase64` | استخراج پسوند از داده base64 | `base64WithPrefix: string` | `string` |
| `GetFilePathUploadFile` | دریافت مسیر آپلود برای نوع فایل | `type: string` | `string` |

## 📦 **انواع فایل پشتیبانی شده**
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
    Text    // پیش‌فرض
}
```

## 📝 **مثال‌های استفاده**

### آپلود فایل
```csharp
// آپلود فایل به صورت ناهمزمان
var result = await FileHelper.UploadFileAsync(
    fileStream,
    "document.pdf",
    FileType.PDF,
    "uploads/documents",
    generateNewFileName: true
);

if (result.IsSuccess)
    Console.WriteLine($"فایل با نام {result.NewFileName} آپلود شد");
else
    Console.WriteLine($"خطاها: {string.Join(", ", result.Errors)}");

// آپلود فایل base64
string base64Data = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAA...";
string path = FileHelper.SaveBase64File(base64Data, "uploads/images");
```

### اعتبارسنجی فایل
```csharp
// اعتبارسنجی فایل PDF
byte[] fileBytes = File.ReadAllBytes("document.pdf");
bool isValid = ValidateFiles.IsValidPdfFile(fileBytes, "application/pdf");

// دریافت نوع فایل از پسوند
string extension = ".jpg";
FileType fileType = extension.GetFileTypeFromExtension();
```

## ⚠️ **نکات مهم**

1. اعتبارسنجی فایل:
   - بررسی امضای فایل (magic numbers)
   - پشتیبانی از فرمت‌های رایج
   - پیاده‌سازی thread-safe

2. امنیت آپلود:
   - ایجاد خودکار پوشه‌ها در صورت عدم وجود
   - تولید نام‌های یکتا در صورت درخواست
   - اعتبارسنجی محتوای فایل قبل از ذخیره

3. مدیریت Base64:
   - تشخیص نوع MIME
   - پشتیبانی از فرمت data URL
   - حذف ایمن padding و فضای خالی

4. مدیریت خطا:
   - نمایش خطاهای جزئی در حالت debug
   - پیام‌های خطای امن در محیط تولید
   - عملیات thread-safe