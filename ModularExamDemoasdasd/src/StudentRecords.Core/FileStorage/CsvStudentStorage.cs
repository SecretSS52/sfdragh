using System.Globalization;
using System.Text;
using StudentRecords.Core.Models;

namespace StudentRecords.Core.FileStorage;

/// <summary>
/// Сохраняет и загружает ведомость в текстовом формате CSV.
/// </summary>
public sealed class CsvStudentStorage : IStudentFileStorage
{
    public string FormatName => "CSV";

    public string FileExtension => ".csv";

    public void Save(string path, IReadOnlyCollection<StudentRecord> records)
    {
        using var writer = new StreamWriter(path, append: false, Encoding.UTF8);

        writer.WriteLine("ФИО,Предмет,Балл");

        foreach (var record in records)
        {
            writer.WriteLine(string.Join(
                ',',
                Escape(record.StudentName),
                Escape(record.Subject),
                record.Score.ToString(CultureInfo.InvariantCulture)));
        }
    }

    public IReadOnlyList<StudentRecord> Load(string path)
    {
        using var reader = new StreamReader(path, Encoding.UTF8);
        var records = new List<StudentRecord>();

        _ = reader.ReadLine(); // Пропускаем строку заголовков.

        var lineNumber = 1;
        while (reader.ReadLine() is { } line)
        {
            lineNumber++;

            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            var columns = ParseLine(line);

            if (columns.Count != 3)
            {
                throw new FormatException($"Строка CSV {lineNumber} должна содержать ровно 3 колонки.");
            }

            if (!int.TryParse(columns[2], NumberStyles.Integer, CultureInfo.InvariantCulture, out var score))
            {
                throw new FormatException($"Строка CSV {lineNumber} содержит некорректный балл: {columns[2]}.");
            }

            records.Add(new StudentRecord(columns[0], columns[1], score));
        }

        return records;
    }

    private static string Escape(string value)
    {
        // Если в тексте есть запятая или кавычка, CSV требует обернуть значение в кавычки.
        if (!value.Contains(',') && !value.Contains('"') && !value.Contains('\n'))
        {
            return value;
        }

        return $"\"{value.Replace("\"", "\"\"", StringComparison.Ordinal)}\"";
    }

    private static IReadOnlyList<string> ParseLine(string line)
    {
        // Мини-парсер CSV нужен, чтобы корректно читать значения в кавычках.
        var result = new List<string>();
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

            if (symbol == ',' && !inQuotes)
            {
                result.Add(current.ToString());
                current.Clear();
                continue;
            }

            current.Append(symbol);
        }

        result.Add(current.ToString());
        return result;
    }
}
