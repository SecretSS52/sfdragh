using System.Globalization;
using System.Text;
using ExpenseTracker.Core.Models;

namespace ExpenseTracker.Core.FileStorage;

/// <summary>
/// Модуль работы с CSV. В русских таблицах часто используют разделитель ";".
/// </summary>
public sealed class CsvExpenseStorage : IExpenseStorage
{
    private static readonly UTF8Encoding FileEncoding = new(encoderShouldEmitUTF8Identifier: true);

    public string FormatName => "CSV";

    public string FileExtension => ".csv";

    public void Save(string path, IReadOnlyCollection<Expense> expenses)
    {
        using var writer = new StreamWriter(path, append: false, FileEncoding);

        writer.WriteLine("Дата;Категория;Сумма;Описание");

        foreach (var expense in expenses)
        {
            writer.WriteLine(string.Join(
                ';',
                expense.Date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                Escape(expense.Category),
                expense.Amount.ToString(CultureInfo.InvariantCulture),
                Escape(expense.Comment)));
        }
    }

    public IReadOnlyList<Expense> Load(string path)
    {
        using var reader = new StreamReader(path, FileEncoding);
        var result = new List<Expense>();

        _ = reader.ReadLine(); // Пропускаем строку заголовков.

        var lineNumber = 1;
        while (reader.ReadLine() is { } line)
        {
            lineNumber++;

            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            var columns = SplitLine(line);

            if (columns.Count != 4)
            {
                throw new FormatException($"Строка CSV {lineNumber} должна содержать 4 колонки.");
            }

            result.Add(new Expense(
                DateOnly.ParseExact(columns[0], "yyyy-MM-dd", CultureInfo.InvariantCulture),
                columns[1],
                decimal.Parse(columns[2], CultureInfo.InvariantCulture),
                columns[3]));
        }

        return result;
    }

    private static string Escape(string value)
    {
        // Если внутри текста есть разделитель или кавычки, значение нужно обернуть в кавычки.
        if (!value.Contains(';') && !value.Contains('"') && !value.Contains('\n'))
        {
            return value;
        }

        return $"\"{value.Replace("\"", "\"\"", StringComparison.Ordinal)}\"";
    }

    private static IReadOnlyList<string> SplitLine(string line)
    {
        // Небольшой CSV-парсер нужен, чтобы корректно прочитать значения в кавычках.
        var columns = new List<string>();
        var current = new StringBuilder();
        var inQuotes = false;

        for (var i = 0; i < line.Length; i++)
        {
            var symbol = line[i];

            if (symbol == '"')
            {
                if (inQuotes && i + 1 < line.Length && line[i + 1] == '"')
                {
                    current.Append('"');
                    i++;
                }
                else
                {
                    inQuotes = !inQuotes;
                }

                continue;
            }

            if (symbol == ';' && !inQuotes)
            {
                columns.Add(current.ToString());
                current.Clear();
                continue;
            }

            current.Append(symbol);
        }

        columns.Add(current.ToString());
        return columns;
    }
}
