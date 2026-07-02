using ExpenseTracker.Core.DemoData;
using ExpenseTracker.Core.FileStorage;
using ExpenseTracker.Core.Services;

var dataFolder = Path.Combine(AppContext.BaseDirectory, "data");
Directory.CreateDirectory(dataFolder);

var expenses = SampleExpenses.Create();
var analyzer = new ExpenseAnalyzer();

IExpenseStorage csvStorage = new CsvExpenseStorage();
IExpenseStorage excelStorage = new ExcelExpenseStorage();

Console.WriteLine("Учебный проект: учет расходов");
Console.WriteLine("=============================");

var csvPath = Path.Combine(dataFolder, $"expenses{csvStorage.FileExtension}");
csvStorage.Save(csvPath, expenses);
var csvExpenses = csvStorage.Load(csvPath);
Console.WriteLine($"CSV: сохранено и прочитано записей: {csvExpenses.Count}");

var xlsxPath = Path.Combine(dataFolder, $"expenses{excelStorage.FileExtension}");
excelStorage.Save(xlsxPath, expenses);
var xlsxExpenses = excelStorage.Load(xlsxPath);
Console.WriteLine($"XLSX: сохранено и прочитано записей: {xlsxExpenses.Count}");

var summary = analyzer.BuildSummary(xlsxExpenses);

Console.WriteLine();
Console.WriteLine($"Всего расходов: {summary.Count}");
Console.WriteLine($"Общая сумма: {summary.TotalAmount:F2} руб.");
Console.WriteLine($"Средний расход: {summary.AverageAmount:F2} руб.");
Console.WriteLine($"Крупных расходов: {summary.BigExpenseCount}");
Console.WriteLine($"Самая дорогая категория: {summary.BiggestCategory}");
Console.WriteLine($"Файлы лежат здесь: {dataFolder}");
