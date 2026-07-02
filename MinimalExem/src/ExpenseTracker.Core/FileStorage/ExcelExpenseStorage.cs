using ClosedXML.Excel;
using ExpenseTracker.Core.Diagnostics;
using ExpenseTracker.Core.Models;

namespace ExpenseTracker.Core.FileStorage;

/// <summary>
/// Файловое хранилище расходов в формате XLSX.
/// </summary>
public sealed class ExcelExpenseStorage : IExpenseStorage
{
    private const string SheetName = "Расходы";

    public string FormatName => "XLSX";

    public string FileExtension => ".xlsx";

    /// <summary>
    /// Сохраняет расходы в Excel-файл.
    /// </summary>
    public void Save(string path, IReadOnlyCollection<Expense> expenses)
    {
        DebugHelper.WriteDebug($"Сохранение XLSX: {path}");
        using var workbook = new XLWorkbook();
        var sheet = workbook.Worksheets.Add(SheetName);

        sheet.Cell(1, 1).Value = "Дата";
        sheet.Cell(1, 2).Value = "Категория";
        sheet.Cell(1, 3).Value = "Сумма";
        sheet.Cell(1, 4).Value = "Описание";
        sheet.Range("A1:D1").Style.Font.Bold = true;

        var row = 2;
        foreach (var expense in expenses)
        {
            sheet.Cell(row, 1).Value = expense.Date.ToDateTime(TimeOnly.MinValue);
            sheet.Cell(row, 1).Style.DateFormat.Format = "yyyy-mm-dd";
            sheet.Cell(row, 2).Value = expense.Category;
            sheet.Cell(row, 3).Value = expense.Amount;
            sheet.Cell(row, 4).Value = expense.Comment;
            row++;
        }

        sheet.Columns().AdjustToContents();
        workbook.SaveAs(path);
        DebugHelper.WriteTrace($"XLSX сохранен. Записей: {expenses.Count}.");
    }

    /// <summary>
    /// Загружает расходы из Excel-файла.
    /// </summary>
    public IReadOnlyList<Expense> Load(string path)
    {
        DebugHelper.WriteDebug($"Загрузка XLSX: {path}");
        using var workbook = new XLWorkbook(path);
        var sheet = workbook.Worksheet(SheetName);
        var result = new List<Expense>();

        for (var row = 2; !sheet.Cell(row, 1).IsEmpty(); row++)
        {
            result.Add(new Expense(
                DateOnly.FromDateTime(sheet.Cell(row, 1).GetDateTime()),
                sheet.Cell(row, 2).GetString(),
                sheet.Cell(row, 3).GetValue<decimal>(),
                sheet.Cell(row, 4).GetString()));
        }

        DebugHelper.WriteTrace($"XLSX загружен. Записей: {result.Count}.");
        return result;
    }
}
