using System.Globalization;
using System.Text;
using ExpenseExam.Core.Diagnostics;
using ExpenseExam.Core.Models;

namespace ExpenseExam.Core.FileStorage;

/// <summary>
/// Сохраняет и загружает расходы в CSV-файл.
/// </summary>
public class CsvStorage
{
    private static readonly UTF8Encoding FileEncoding = new(encoderShouldEmitUTF8Identifier: true);

    /// <summary>
    /// Сохраняет список расходов в CSV-файл с разделителем ";".
    /// </summary>
    public void Save(string path, List<Expense> expenses)
    {
        DebugHelper.WriteDebug($"Сохранение CSV: {path}");

        using var writer = new StreamWriter(path, append: false, FileEncoding);
        writer.WriteLine("Date;Category;Amount;Comment");

        foreach (var expense in expenses)
        {
            writer.WriteLine(
                $"{expense.Date:yyyy-MM-dd};{expense.Category};{expense.Amount.ToString(CultureInfo.InvariantCulture)};{expense.Comment}");
        }
    }

    /// <summary>
    /// Загружает список расходов из CSV-файла.
    /// </summary>
    public List<Expense> Load(string path)
    {
        DebugHelper.WriteTrace($"Загрузка CSV: {path}");

        var expenses = new List<Expense>();
        var lines = File.ReadAllLines(path, FileEncoding).Skip(1);

        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            var parts = line.Split(';');

            expenses.Add(new Expense
            {
                Date = DateTime.ParseExact(parts[0], "yyyy-MM-dd", CultureInfo.InvariantCulture),
                Category = parts[1],
                Amount = decimal.Parse(parts[2], CultureInfo.InvariantCulture),
                Comment = parts[3]
            });
        }

        return expenses;
    }
}
