namespace CsharpGalexy.LibraryExtention.File;

public static partial class ValidateFiles
{
    public enum FileType
    {
        Image = 1,
        Video = 2,
        PDF = 3,
        Text = 4,
        DOC = 5,
        DOCX = 6,
        PPT = 7,
        ZIP = 8,
        RAR = 9
    }


}

public enum FileExtension
{
    Unknown,    // برای فایل‌های ناشناخته
    Jpg,        // .jpg / .jpeg
    Png,        // .png
    Gif,        // .gif
    Bmp,        // .bmp
    Pdf,        // .pdf
    Txt,        // .txt
    Doc,        // .doc
    Docx,       // .docx
    Xls,        // .xls
    Xlsx,       // .xlsx
    Ppt,        // .ppt
    Pptx,       // .pptx
    Mp3,        // .mp3
    Mp4,        // .mp4
    Avi,        // .avi
    Mov,        // .mov
    Zip,        // .zip
    Rar,        // .rar
    Bin         // .bin / فایل ناشناخته
}