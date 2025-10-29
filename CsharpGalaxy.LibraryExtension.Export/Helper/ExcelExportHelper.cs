namespace CsharpGalaxy.LibraryExtension.Export.Helper;

using ClosedXML.Excel;
using System.Data;
using System.Reflection;

public static class ExcelExportHelper
{
    public static byte[] ExportToExcel<T>(List<T> data, string sheetName = "Sheet1")
    {
        using var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add(sheetName);

        var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        // Header
        for (int i = 0; i < properties.Length; i++)
        {
            worksheet.Cell(1, i + 1).Value = properties[i].Name;
            worksheet.Cell(1, i + 1).Style.Font.Bold = true;
        }

        // Rows
        for (int row = 0; row < data.Count; row++)
        {
            for (int col = 0; col < properties.Length; col++)
            {
                var cellValue = properties[col].GetValue(data[row]);
                worksheet.Cell(row + 2, col + 1).Value = cellValue?.ToString() ?? "";

            }
        }

        worksheet.Columns().AdjustToContents();

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        return stream.ToArray();
    }
}
