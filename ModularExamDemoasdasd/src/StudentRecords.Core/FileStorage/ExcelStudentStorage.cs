using ClosedXML.Excel;
using StudentRecords.Core.Models;

namespace StudentRecords.Core.FileStorage;

/// <summary>
/// Сохраняет и загружает ведомость как книгу Excel.
/// </summary>
public sealed class ExcelStudentStorage : IStudentFileStorage
{
    private const string SheetName = "Студенты";

    public string FormatName => "XLSX";

    public string FileExtension => ".xlsx";

    public void Save(string path, IReadOnlyCollection<StudentRecord> records)
    {
        using var workbook = new XLWorkbook();
        var sheet = workbook.Worksheets.Add(SheetName);

        sheet.Cell(1, 1).Value = "ФИО";
        sheet.Cell(1, 2).Value = "Предмет";
        sheet.Cell(1, 3).Value = "Балл";
        sheet.Range("A1:C1").Style.Font.Bold = true;

        // Заполняем таблицу построчно: одна запись ведомости = одна строка Excel.
        var row = 2;
        foreach (var record in records)
        {
            sheet.Cell(row, 1).Value = record.StudentName;
            sheet.Cell(row, 2).Value = record.Subject;
            sheet.Cell(row, 3).Value = record.Score;
            row++;
        }

        sheet.Columns().AdjustToContents();
        workbook.SaveAs(path);
    }

    public IReadOnlyList<StudentRecord> Load(string path)
    {
        using var workbook = new XLWorkbook(path);
        var sheet = workbook.Worksheet(SheetName);
        var records = new List<StudentRecord>();
        var row = 2;

        while (!sheet.Cell(row, 1).IsEmpty())
        {
            records.Add(new StudentRecord(
                sheet.Cell(row, 1).GetString(),
                sheet.Cell(row, 2).GetString(),
                sheet.Cell(row, 3).GetValue<int>()));

            row++;
        }

        return records;
    }
}
