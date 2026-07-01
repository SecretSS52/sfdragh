using System.Globalization;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using ClosedXML.Excel;

namespace MiniExam.Core;

/// <summary>
/// Главный класс DLL: считает сводку и умеет читать/писать CSV, JSON, XLSX.
/// </summary>
public static class GradeBook
{
    private const string SheetName = "Оценки";
    private static readonly UTF8Encoding Utf8WithBom = new(encoderShouldEmitUTF8Identifier: true);

    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web)
    {
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        WriteIndented = true
    };

    public static Grade[] DemoData() =>
    [
        new Grade("Анна Петрова", "C#", 95),
        new Grade("Иван Соколов", "Базы данных", 72),
        new Grade("Мария Волкова", "Тестирование", 58)
    ];

    public static GradeSummary GetSummary(IEnumerable<Grade> grades)
    {
        var list = grades.ToList();

        foreach (var grade in list)
        {
            if (grade.Score is < 0 or > 100)
            {
                throw new ArgumentOutOfRangeException(nameof(grades), "Балл должен быть от 0 до 100.");
            }
        }

        if (list.Count == 0)
        {
            return new GradeSummary(0, 0, 0, 0);
        }

        var passed = list.Count(grade => grade.Passed);
        var average = Math.Round(list.Average(grade => grade.Score), 2);

        return new GradeSummary(list.Count, average, passed, list.Count - passed);
    }

    public static void SaveCsv(string path, IEnumerable<Grade> grades)
    {
        using var writer = new StreamWriter(path, append: false, Utf8WithBom);

        writer.WriteLine("ФИО;Предмет;Балл");
        foreach (var grade in grades)
        {
            writer.WriteLine($"{Escape(grade.StudentName)};{Escape(grade.Subject)};{grade.Score}");
        }
    }

    public static Grade[] LoadCsv(string path)
    {
        var result = new List<Grade>();
        var lines = File.ReadAllLines(path, Utf8WithBom).Skip(1);

        foreach (var line in lines)
        {
            var parts = SplitCsvLine(line);
            result.Add(new Grade(parts[0], parts[1], int.Parse(parts[2], CultureInfo.InvariantCulture)));
        }

        return result.ToArray();
    }

    public static void SaveJson(string path, IEnumerable<Grade> grades)
    {
        var json = JsonSerializer.Serialize(grades, JsonOptions);
        File.WriteAllText(path, json, Utf8WithBom);
    }

    public static Grade[] LoadJson(string path)
    {
        var json = File.ReadAllText(path, Utf8WithBom);
        return JsonSerializer.Deserialize<Grade[]>(json, JsonOptions) ?? [];
    }

    public static void SaveXlsx(string path, IEnumerable<Grade> grades)
    {
        using var workbook = new XLWorkbook();
        var sheet = workbook.Worksheets.Add(SheetName);

        sheet.Cell("A1").Value = "ФИО";
        sheet.Cell("B1").Value = "Предмет";
        sheet.Cell("C1").Value = "Балл";
        sheet.Range("A1:C1").Style.Font.Bold = true;

        var row = 2;
        foreach (var grade in grades)
        {
            sheet.Cell(row, 1).Value = grade.StudentName;
            sheet.Cell(row, 2).Value = grade.Subject;
            sheet.Cell(row, 3).Value = grade.Score;
            row++;
        }

        sheet.Columns().AdjustToContents();
        workbook.SaveAs(path);
    }

    public static Grade[] LoadXlsx(string path)
    {
        using var workbook = new XLWorkbook(path);
        var sheet = workbook.Worksheet(SheetName);
        var result = new List<Grade>();

        for (var row = 2; !sheet.Cell(row, 1).IsEmpty(); row++)
        {
            result.Add(new Grade(
                sheet.Cell(row, 1).GetString(),
                sheet.Cell(row, 2).GetString(),
                sheet.Cell(row, 3).GetValue<int>()));
        }

        return result.ToArray();
    }

    private static string Escape(string value)
    {
        return value.Contains(';') ? $"\"{value.Replace("\"", "\"\"", StringComparison.Ordinal)}\"" : value;
    }

    private static string[] SplitCsvLine(string line)
    {
        // Для учебного примера достаточно простого разделения по точке с запятой.
        return line.Split(';');
    }
}
