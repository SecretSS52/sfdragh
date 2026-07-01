using StudentRecords.Core.Debugging;
using StudentRecords.Core.DemoData;
using StudentRecords.Core.FileStorage;
using StudentRecords.Core.Services;

// Папка data создается рядом с exe-файлом, чтобы результат запуска было легко найти.
var dataDirectory = Path.Combine(AppContext.BaseDirectory, "data");
Directory.CreateDirectory(dataDirectory);

var records = SampleStudents.Create();
var analyzer = new GradeAnalyzer();
var debugger = new StudentRecordsDebugger(new ConsoleDebugLogger());
var storages = new IStudentFileStorage[]
{
    new CsvStudentStorage(),
    new JsonStudentStorage(),
    new ExcelStudentStorage()
};

Console.WriteLine("Демонстрация ведомости студентов");
Console.WriteLine("================================");

// В Debug-сборке покажем исходные записи, чтобы удобнее было отлаживать программу.
debugger.DumpRecords(records);

foreach (var storage in storages)
{
    var path = Path.Combine(dataDirectory, $"students{storage.FileExtension}");
    storage.Save(path, records);

    var loaded = storage.Load(path);
    Console.WriteLine($"{storage.FormatName}: сохранено и прочитано записей: {loaded.Count} -> {path}");
}

var summary = analyzer.BuildSummary(records);

Console.WriteLine();
Console.WriteLine($"Всего записей: {summary.TotalRecords}");
Console.WriteLine($"Средний балл: {summary.AverageScore}");
Console.WriteLine($"Сдали: {summary.PassedCount}");
Console.WriteLine($"Не сдали: {summary.FailedCount}");
Console.WriteLine($"Лучший результат: {summary.BestRecord?.StudentName} ({summary.BestRecord?.Score})");
