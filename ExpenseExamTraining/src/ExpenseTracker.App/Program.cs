using ExpenseTracker.Core.Debugging;
using ExpenseTracker.Core.DemoData;
using ExpenseTracker.Core.FileStorage;
using ExpenseTracker.Core.Services;

var dataFolder = Path.Combine(AppContext.BaseDirectory, "data");
Directory.CreateDirectory(dataFolder);

var expenses = SampleExpenses.Create();
var analyzer = new ExpenseAnalyzer();
var debugger = new ExpenseDebugger(new ConsoleDebugLogger());

var storages = new IExpenseStorage[]
{
    new CsvExpenseStorage(),
    new JsonExpenseStorage(),
    new ExcelExpenseStorage()
};

Console.WriteLine("Учебный проект: учет расходов");
Console.WriteLine("=============================");

// Отладочный класс показывает исходные данные в Debug-сборке.
debugger.Dump(expenses);

foreach (var storage in storages)
{
    var path = Path.Combine(dataFolder, $"expenses{storage.FileExtension}");
    storage.Save(path, expenses);

    var loaded = storage.Load(path);
    Console.WriteLine($"{storage.FormatName}: сохранено и прочитано записей: {loaded.Count}");
}

var summary = analyzer.BuildSummary(expenses);

Console.WriteLine();
Console.WriteLine($"Всего расходов: {summary.Count}");
Console.WriteLine($"Общая сумма: {summary.TotalAmount} руб.");
Console.WriteLine($"Средний расход: {summary.AverageAmount} руб.");
Console.WriteLine($"Крупных расходов: {summary.BigExpenseCount}");
Console.WriteLine($"Самая дорогая категория: {summary.BiggestCategory}");
Console.WriteLine($"Файлы лежат здесь: {dataFolder}");
