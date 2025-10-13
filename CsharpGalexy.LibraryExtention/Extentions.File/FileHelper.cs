using CsharpGalexy.LibraryExtention.Models.Files;
using static CsharpGalexy.LibraryExtention.File.ValidateFiles;

namespace CsharpGalexy.LibraryExtention.File;

public static partial class ValidateFiles
{
    public static string GetContentType(string path)
    {
        var types = GetMimeTypes();
        var ext = Path.GetExtension(path).ToLowerInvariant();
        return types[ext];
    }

    private static Dictionary<string, string> GetMimeTypes()
    {
        return new Dictionary<string, string>
        {
            {".txt", "text/plain"},
            {".pdf", "application/pdf"},
            {".doc", "application/vnd.ms-word"},
            {".docx", "application/vnd.ms-word"},
            {".xls", "application/vnd.ms-excel"},
            {".xlsx", "application/vnd.openxmlformatsofficedocument.spreadsheetml.sheet"},
            {".png", "image/png"},
            {".jpg", "image/jpeg"},
            {".jpeg", "image/jpeg"},
            {".gif", "image/gif"},
            {".csv", "text/csv"},
            {".zip","application/zip" },
            {".rar","application/x-rar" }
        };
    }

    public static bool IsValidFile(byte[] bytFile, FileType flType, string fileContentType)
    {
        bool isvalid = false;

        if (flType == FileType.Image)
        {
            isvalid = IsValidImageFile(bytFile, fileContentType);
        }
        else if (flType == FileType.Video)
        {
            isvalid = IsValidVideoFile(bytFile, fileContentType);
        }
        else if (flType == FileType.PDF)
        {
            isvalid = IsValidPdfFile(bytFile, fileContentType);
        }

        else if (flType == FileType.DOC || flType == FileType.DOCX)
        {
            isvalid = IsValidDocDocxFile(bytFile, fileContentType);
        }

        else if (flType == FileType.RAR || flType == FileType.ZIP)
        {
            isvalid = IsValidZipRarFile(bytFile, fileContentType);
        }

        return isvalid;
    }

    public static bool IsValidImageFile(byte[] bytFile, string fileContentType)
    {
        bool isvalid = false;

        byte[] chkBytejpg = { 255, 216, 255 };
        byte[] chkBytebmp = { 66, 77 };
        byte[] chkBytegif = { 71, 73, 70, 56 };
        byte[] chkBytepng = { 137, 80, 78, 71 };


        ImageFileExtension imgfileExtn = ImageFileExtension.none;

        if (fileContentType.Contains("jpg") | fileContentType.Contains("jpeg"))
        {
            imgfileExtn = ImageFileExtension.jpg;
        }
        else if (fileContentType.Contains("png"))
        {
            imgfileExtn = ImageFileExtension.png;
        }
        else if (fileContentType.Contains("bmp"))
        {
            imgfileExtn = ImageFileExtension.bmp;
        }
        else if (fileContentType.Contains("gif"))
        {
            imgfileExtn = ImageFileExtension.gif;
        }

        if (imgfileExtn == ImageFileExtension.jpg || imgfileExtn == ImageFileExtension.jpeg)
        {
            if (bytFile.Length >= 4)
            {
                int j = 0;
                for (int i = 0; i <= 2; i++)
                {
                    if (bytFile[i] == chkBytejpg[i])
                    {
                        j = j + 1;
                        if (j == 3)
                        {
                            isvalid = true;
                        }
                    }
                }
            }
        }


        else if (imgfileExtn == ImageFileExtension.png)
        {
            if (bytFile.Length >= 4)
            {
                int j = 0;
                for (int i = 0; i <= 3; i++)
                {
                    if (bytFile[i] == chkBytepng[i])
                    {
                        j = j + 1;
                        if (j == 4)
                        {
                            isvalid = true;
                        }
                    }
                }
            }
        }
        else if (imgfileExtn == ImageFileExtension.bmp)
        {
            if (bytFile.Length >= 4)
            {
                int j = 0;
                for (int i = 0; i <= 1; i++)
                {
                    if (bytFile[i] == chkBytebmp[i])
                    {
                        j = j + 1;
                        if (j == 2)
                        {
                            isvalid = true;
                        }
                    }
                }
            }
        }
        else if (imgfileExtn == ImageFileExtension.gif)
        {
            if (bytFile.Length >= 4)
            {
                int j = 0;
                for (int i = 0; i <= 1; i++)
                {
                    if (bytFile[i] == chkBytegif[i])
                    {
                        j = j + 1;
                        if (j == 2)
                        {
                            isvalid = true;
                        }
                    }
                }
            }
        }
        return isvalid;
    }

    private static bool IsValidVideoFile(byte[] bytFile, string fileContentType)
    {
        byte[] chkBytewmv = { 48, 38, 178, 117 };
        byte[] chkByteavi = { 82, 73, 70, 70 };
        byte[] chkByteflv = { 70, 76, 86, 1 };
        byte[] chkBytempg = { 0, 0, 1, 186 };
        byte[] chkBytemp4 = { 0, 0, 0 };
        bool isvalid = false;

        VideoFileExtension vdofileExtn = VideoFileExtension.none;
        if (fileContentType.Contains("wmv"))
        {
            vdofileExtn = VideoFileExtension.wmv;
        }
        else if (fileContentType.Contains("mpg") || fileContentType.Contains("mpeg"))
        {
            vdofileExtn = VideoFileExtension.mpg;
        }
        else if (fileContentType.Contains("mp4"))
        {
            vdofileExtn = VideoFileExtension.mp4;
        }
        else if (fileContentType.Contains("avi"))
        {
            vdofileExtn = VideoFileExtension.avi;
        }
        else if (fileContentType.Contains("flv"))
        {
            vdofileExtn = VideoFileExtension.flv;
        }

        if (vdofileExtn == VideoFileExtension.wmv)
        {
            if (bytFile.Length >= 4)
            {
                int j = 0;
                for (int i = 0; i <= 3; i++)
                {
                    if (bytFile[i] == chkBytewmv[i])
                    {
                        j = j + 1;
                        if (j == 3)
                        {
                            isvalid = true;
                        }
                    }
                }
            }
        }
        else if (vdofileExtn == VideoFileExtension.mpg || vdofileExtn == VideoFileExtension.mpeg)
        {
            if (bytFile.Length >= 4)
            {
                int j = 0;
                for (int i = 0; i <= 3; i++)
                {
                    if (bytFile[i] == chkBytempg[i])
                    {
                        j = j + 1;
                        if (j == 3)
                        {
                            isvalid = true;
                        }
                    }
                }
            }
        }
        else if (vdofileExtn == VideoFileExtension.mp4)
        {
            if (bytFile.Length >= 4)
            {
                int j = 0;
                for (int i = 0; i <= 2; i++)
                {
                    if (bytFile[i] == chkBytemp4[i])
                    {
                        j = j + 1;
                        if (j == 3)
                        {
                            isvalid = true;
                        }
                    }
                }
            }
        }
        else if (vdofileExtn == VideoFileExtension.avi)
        {
            if (bytFile.Length >= 4)
            {
                int j = 0;
                for (int i = 0; i <= 3; i++)
                {
                    if (bytFile[i] == chkByteavi[i])
                    {
                        j = j + 1;
                        if (j == 3)
                        {
                            isvalid = true;
                        }
                    }
                }
            }
        }
        else if (vdofileExtn == VideoFileExtension.flv)
        {
            if (bytFile.Length >= 4)
            {
                int j = 0;
                for (int i = 0; i <= 3; i++)
                {
                    if (bytFile[i] == chkByteflv[i])
                    {
                        j = j + 1;
                        if (j == 3)
                        {
                            isvalid = true;
                        }
                    }
                }
            }
        }

        return isvalid;

    }

    public static bool IsValidPdfFile(byte[] bytFile, string fileContentType)
    {
        byte[] chkBytepdf = { 37, 80, 68, 70 };
        bool isvalid = false;

        PdfFileExtension pdffileExtn = PdfFileExtension.none;
        if (fileContentType.Contains("pdf"))
        {
            pdffileExtn = PdfFileExtension.PDF;
        }

        if (pdffileExtn == PdfFileExtension.PDF)
        {
            if (bytFile.Length >= 4)
            {
                int j = 0;
                for (int i = 0; i <= 3; i++)
                {
                    if (bytFile[i] == chkBytepdf[i])
                    {
                        j = j + 1;
                        if (j == 4)
                        {
                            isvalid = true;
                        }
                    }
                }
            }
        }

        return isvalid;
    }

    public static bool IsValidDocDocxFile(byte[] bytFile, string fileContentType)
    {
        byte[] chkByteDoc = { 208, 207, 17, 224 };
        byte[] chkByteDocx = { 80, 75, 3, 4 };
        bool isvalid = false;

        DocDocxFileExtention docfileExtn = DocDocxFileExtention.none;
        if (fileContentType.Contains("doc"))
        {
            docfileExtn = DocDocxFileExtention.DOC;
        }

        else if (fileContentType.Contains("docx"))
        {
            docfileExtn = DocDocxFileExtention.DOCX;
        }

        if (docfileExtn == DocDocxFileExtention.DOC)
        {
            if (bytFile.Length >= 4)
            {
                int j = 0;
                for (int i = 0; i <= 3; i++)
                {
                    if (bytFile[i] == chkByteDoc[i])
                    {
                        j = j + 1;
                        if (j == 4)
                        {
                            isvalid = true;
                        }
                    }
                }
            }
        }

        else if (docfileExtn == DocDocxFileExtention.DOCX)
        {
            if (bytFile.Length >= 4)
            {
                int j = 0;
                for (int i = 0; i <= 3; i++)
                {
                    if (bytFile[i] == chkByteDocx[i])
                    {
                        j = j + 1;
                        if (j == 4)
                        {
                            isvalid = true;
                        }
                    }
                }
            }
        }

        return isvalid;
    }

    public static bool IsValidZipRarFile(byte[] bytFile, string fileContentType)
    {
        byte[] chkByteZip = { 80, 75, 3, 4 };
        byte[] chkByteRar = { 82, 97, 114, 33 };
        bool isvalid = false;

        ZipRarFileExtention ziprarfileExtn = ZipRarFileExtention.none;
        if (fileContentType.Contains("zip"))
        {
            ziprarfileExtn = ZipRarFileExtention.ZIP;
        }

        else if (fileContentType.Contains("rar"))
        {
            ziprarfileExtn = ZipRarFileExtention.RAR;
        }

        if (ziprarfileExtn == ZipRarFileExtention.ZIP)
        {
            if (bytFile.Length >= 4)
            {
                int j = 0;
                for (int i = 0; i <= 3; i++)
                {
                    if (bytFile[i] == chkByteZip[i])
                    {
                        j = j + 1;
                        if (j == 4)
                        {
                            isvalid = true;
                        }
                    }
                }
            }
        }

        else if (ziprarfileExtn == ZipRarFileExtention.RAR)
        {
            if (bytFile.Length >= 4)
            {
                int j = 0;
                for (int i = 0; i <= 3; i++)
                {
                    if (bytFile[i] == chkByteRar[i])
                    {
                        j = j + 1;
                        if (j == 4)
                        {
                            isvalid = true;
                        }
                    }
                }
            }
        }

        return isvalid;
    }

    /// <summary>
    /// upload and validating files
    /// </summary>
    /// <param name="file"></param>
    /// <param name="path"></param>
    /// <param name="type"></param>
    /// <param name="fileName"></param>
    /// <param name="generateNewFileName"></param>
    /// <returns>UploadFileResult(bool IsSuccess, string NewFileName, List<string> Errors)</returns>








}
public static class FileHelper
{

    public static async Task<UploadFileResult> UploadFileAsync(Stream stream, string fileName, FileType type, string path, bool generateNewFileName = false)
    {
        var errors = new List<string>();

        if (stream == null || stream.Length == 0)
        {
            errors.Add("فایل خالی یا نامعتبر است.");
            return new UploadFileResult(false, errors);
        }

        try
        {
            var extension = Path.GetExtension(fileName)?.ToLowerInvariant();
            var newFileName = generateNewFileName
                ? $"{Guid.NewGuid()}{extension}"
                : fileName;

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            var fullPath = Path.Combine(path, newFileName);

            using var memory = new MemoryStream();
            await stream.CopyToAsync(memory);
            var fileBytes = memory.ToArray();

            if (!IsValidFile(fileBytes, type, extension))
            {
                errors.Add("نوع فایل مجاز نیست.");
                return new UploadFileResult(false, errors);
            }

            await System.IO.File.WriteAllBytesAsync(fullPath, fileBytes);

            return new UploadFileResult(true, newFileName);
        }
        catch (Exception ex)
        {
#if DEBUG
            errors.Add(ex.Message);
            errors.Add(ex.StackTrace ?? "");
#else
            errors.Add("خطا در آپلود فایل.");
#endif
            return new UploadFileResult(false, errors);
        }
    }
    public static bool DeleteFile(string relativePath, string relativeRoot = "wwwroot")
    {
        if (string.IsNullOrEmpty(relativePath))
            return false;

        // ساخت مسیر کامل روی دیسک
        string fullPath = Path.Combine(Directory.GetCurrentDirectory(), relativeRoot, relativePath.TrimStart('/').Replace("/", Path.DirectorySeparatorChar.ToString()));

        if (System.IO.File.Exists(fullPath))
        {
            System.IO.File.Delete(fullPath);
            return true;
        }

        return false;
    }


    /// <summary>
    /// removes any kind of files with file path
    /// </summary>
    /// <param name="path"></param>
    public static void DeleteFile(string path)
    {
      
    }
    public static ValidateFiles.FileType GetFileTypeFromExtension(this string extension)
    {
        return extension switch
        {
            ".jpg" or ".jpeg" or ".png" or ".gif" or ".bmp" => ValidateFiles.FileType.Image,
            ".wmv" or ".mpg" or ".mpeg" or ".mp4" or ".avi" or ".flv" => ValidateFiles.FileType.Video,
            ".pdf" => ValidateFiles.FileType.PDF,
            ".doc" => ValidateFiles.FileType.DOC,
            ".docx" => ValidateFiles.FileType.DOCX,
            ".zip" => ValidateFiles.FileType.ZIP,
            ".rar" => ValidateFiles.FileType.RAR,
            _ => ValidateFiles.FileType.Text // نامعتبر یا پشتیبانی نشده
        };
    }

    /// <summary>
    /// upload file with base64 string and path
    /// </summary>
    /// <param name="base64"></param>
    /// <param name="path"></param>
    public static void UploadFileBase64(this string base64, string path)
    {
        byte[] bytes = Convert.FromBase64String(base64);
        System.IO.File.WriteAllBytes(path, bytes);
    }
    public static byte[] SafeBase64Decode(string base64String)
    {
        if (string.IsNullOrWhiteSpace(base64String))
            throw new ArgumentException("Base64 string is empty");

        // اگر رشته با "data:..." شروع میشه بخش قبل از "," رو حذف کن
        if (base64String.Contains(","))
            base64String = base64String.Substring(base64String.IndexOf(",") + 1);

        // پاکسازی کاراکترهای اضافی
        base64String = base64String.Trim()
                                   .Replace("\n", "")
                                   .Replace("\r", "")
                                   .Replace(" ", "")
                                   .Replace("\"", "");

        // طول Base64 باید مضربی از 4 باشه → در صورت نیاز padding اضافه کن
        int mod4 = base64String.Length % 4;
        if (mod4 > 0)
        {
            base64String += new string('=', 4 - mod4);
        }

        return Convert.FromBase64String(base64String);
    }


    public static string SaveBase64File(string base64WithPrefix, string outputDir, string relativeRoot = "wwwroot")
    {
        // جدا کردن prefix از داده base64
        var parts = base64WithPrefix.Split(',');
        if (parts.Length != 2)
            throw new ArgumentException("Invalid base64 string format");

        string metaData = parts[0]; // مثل: data:image/png;base64
        string base64Data = parts[1];

        // استخراج پسوند از mime-type
        string extension = "bin"; // پیش‌فرض
        int slashIndex = metaData.IndexOf('/');
        int semicolonIndex = metaData.IndexOf(';');

        if (slashIndex > 0 && semicolonIndex > slashIndex)
        {
            extension = metaData.Substring(slashIndex + 1, semicolonIndex - slashIndex - 1);
        }

        // ایجاد پوشه اگر وجود نداشت
        if (!Directory.Exists(outputDir))
            Directory.CreateDirectory(outputDir);

        // اسم یکتا برای فایل
        string fileName = $"{Guid.NewGuid()}.{extension}";
        string filePath = Path.Combine(outputDir, fileName);

        // ذخیره فایل
        byte[] fileBytes = Convert.FromBase64String(base64Data);
        System.IO.File.WriteAllBytes(filePath, fileBytes);

        // ساخت مسیر نسبی (برای ذخیره در DB)
        string relativePath = filePath.Replace(Path.Combine(Directory.GetCurrentDirectory(), relativeRoot), "")
                                      .Replace("\\", "/")
                                      .TrimStart('/');

        return "/" + relativePath;
    }
    public static string SaveBase64FileNotMIMEtype(string base64WithPrefix, FileExtension extension, string outputDir, string relativeRoot = "wwwroot")
    {

        // ایجاد پوشه اگر وجود نداشت
        if (!Directory.Exists(outputDir))
            Directory.CreateDirectory(outputDir);

        // اسم یکتا برای فایل
        string fileName = $"{Guid.NewGuid()}.{(extension).ToString().ToLower()}";
        string filePath = Path.Combine(outputDir, fileName);

        // ذخیره فایل
        byte[] fileBytes = SafeBase64Decode(base64WithPrefix);
        System.IO.File.WriteAllBytes(filePath, fileBytes);

        // ساخت مسیر نسبی (برای ذخیره در DB)
        string relativePath = filePath.Replace(Path.Combine(Directory.GetCurrentDirectory(), relativeRoot), "")
                                      .Replace("\\", "/")
                                      .TrimStart('/');

        return "/" + relativePath;
    }
    public static string GetFileExtensionFromBase64(string base64WithPrefix)
    {
        var parts = base64WithPrefix.Split(',');
        if (parts.Length != 2)
            throw new ArgumentException("Invalid base64 string format");
        string metaData = parts[0]; // مثل: data:image/png;base64
        // استخراج پسوند از mime-type
        string extension = "bin"; // پیش‌فرض
        int slashIndex = metaData.IndexOf('/');
        int semicolonIndex = metaData.IndexOf(';');
        if (slashIndex > 0 && semicolonIndex > slashIndex)
        {
            extension = metaData.Substring(slashIndex + 1, semicolonIndex - slashIndex - 1);
        }
        return extension;
    }

    public static string GetFilePathUploadFile(string type)
    {
        return Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads", type);
    }
}