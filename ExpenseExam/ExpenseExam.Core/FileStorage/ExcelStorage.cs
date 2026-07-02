using ClosedXML.Excel;
using ExpenseExam.Core.Diagnostics;
using ExpenseExam.Core.Models;

namespace ExpenseExam.Core.FileStorage;

/// <summary>
/// Сохраняет и загружает расходы в XLSX-файл.
/// </summary>
public class ExcelStorage
{
    private const string SheetName = "Expenses";

    /// <summary>
    /// Сохраняет список расходов в Excel-файл.
    /// </summary>
    public void Save(string path, List<Expense> expenses)
    {
        DebugHelper.WriteDebug($"Сохранение XLSX: {path}");

        using var workbook = new XLWorkbook();
        var sheet = workbook.Worksheets.Add(SheetName);

        sheet.Cell(1, 1).Value = "Date";
        sheet.Cell(1, 2).Value = "Category";
        sheet.Cell(1, 3).Value = "Amount";
        sheet.Cell(1, 4).Value = "Comment";
        sheet.Range("A1:D1").Style.Font.Bold = true;

        for (var i = 0; i < expenses.Count; i++)
        {
            var row = i + 2;
            sheet.Cell(row, 1).Value = expenses[i].Date;
            sheet.Cell(row, 1).Style.DateFormat.Format = "yyyy-mm-dd";
            sheet.Cell(row, 2).Value = expenses[i].Category;
            sheet.Cell(row, 3).Value = expenses[i].Amount;
            sheet.Cell(row, 4).Value = expenses[i].Comment;
        }

        sheet.Columns().AdjustToContents();
        workbook.SaveAs(path);
    }

    /// <summary>
    /// Загружает список расходов из Excel-файла.
    /// </summary>
    public List<Expense> Load(string path)
    {
        DebugHelper.WriteTrace($"Загрузка XLSX: {path}");

        using var workbook = new XLWorkbook(path);
        var sheet = workbook.Worksheet(SheetName);
        var expenses = new List<Expense>();

        for (var row = 2; !sheet.Cell(row, 1).IsEmpty(); row++)
        {
            expenses.Add(new Expense
            {
                Date = sheet.Cell(row, 1).GetDateTime(),
                Category = sheet.Cell(row, 2).GetString(),
                Amount = sheet.Cell(row, 3).GetValue<decimal>(),
                Comment = sheet.Cell(row, 4).GetString()
            });
        }

        return expenses;
    }
}
