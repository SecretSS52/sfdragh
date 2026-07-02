using ClosedXML.Excel;
using MathExam.Core.Diagnostics;
using MathExam.Core.Models;

namespace MathExam.Core.FileStorage;

/// <summary>
/// Сохраняет и загружает квадратные уравнения в XLSX-файл.
/// </summary>
public class ExcelStorage
{
    private const string SheetName = "Equations";

    /// <summary>
    /// Сохраняет список уравнений в Excel-файл.
    /// </summary>
    public void Save(string path, List<QuadraticEquation> equations)
    {
        DebugHelper.WriteDebug($"Сохранение XLSX: {path}");

        using var workbook = new XLWorkbook();
        var sheet = workbook.Worksheets.Add(SheetName);

        sheet.Cell(1, 1).Value = "Name";
        sheet.Cell(1, 2).Value = "A";
        sheet.Cell(1, 3).Value = "B";
        sheet.Cell(1, 4).Value = "C";
        sheet.Range("A1:D1").Style.Font.Bold = true;

        for (var i = 0; i < equations.Count; i++)
        {
            var row = i + 2;
            sheet.Cell(row, 1).Value = equations[i].Name;
            sheet.Cell(row, 2).Value = equations[i].A;
            sheet.Cell(row, 3).Value = equations[i].B;
            sheet.Cell(row, 4).Value = equations[i].C;
        }

        sheet.Columns().AdjustToContents();
        workbook.SaveAs(path);
    }

    /// <summary>
    /// Загружает список уравнений из Excel-файла.
    /// </summary>
    public List<QuadraticEquation> Load(string path)
    {
        DebugHelper.WriteTrace($"Загрузка XLSX: {path}");

        using var workbook = new XLWorkbook(path);
        var sheet = workbook.Worksheet(SheetName);
        var equations = new List<QuadraticEquation>();

        for (var row = 2; !sheet.Cell(row, 1).IsEmpty(); row++)
        {
            equations.Add(new QuadraticEquation
            {
                Name = sheet.Cell(row, 1).GetString(),
                A = sheet.Cell(row, 2).GetValue<double>(),
                B = sheet.Cell(row, 3).GetValue<double>(),
                C = sheet.Cell(row, 4).GetValue<double>()
            });
        }

        return equations;
    }
}
