using ExpenseExam.Core.FileStorage;
using ExpenseExam.Core.Models;
using ExpenseExam.Core.Services;

var expenses = new List<Expense>
{
    new Expense { Date = new DateTime(2026, 7, 1), Category = "Еда", Amount = 450, Comment = "Обед" },
    new Expense { Date = new DateTime(2026, 7, 2), Category = "Транспорт", Amount = 120, Comment = "Автобус" },
    new Expense { Date = new DateTime(2026, 7, 3), Category = "Учеба", Amount = 1500, Comment = "Курс C#" }
};

var service = new ExpenseService();
var csvStorage = new CsvStorage();
var excelStorage = new ExcelStorage();

var dataFolder = Path.Combine(AppContext.BaseDirectory, "data");
Directory.CreateDirectory(dataFolder);

var csvPath = Path.Combine(dataFolder, "expenses.csv");
var xlsxPath = Path.Combine(dataFolder, "expenses.xlsx");

Console.WriteLine("Учёт расходов");
Console.WriteLine("=============");
Console.WriteLine($"Общая сумма: {service.GetTotal(expenses):F2}");
Console.WriteLine($"Средний расход: {service.GetAverage(expenses):F2}");
Console.WriteLine($"Самый большой расход: {service.GetMaxExpense(expenses)?.Amount:F2}");

csvStorage.Save(csvPath, expenses);
var expensesFromCsv = csvStorage.Load(csvPath);
Console.WriteLine($"CSV загружено записей: {expensesFromCsv.Count}");

excelStorage.Save(xlsxPath, expenses);
var expensesFromXlsx = excelStorage.Load(xlsxPath);
Console.WriteLine($"XLSX загружено записей: {expensesFromXlsx.Count}");

Console.WriteLine($"Файлы созданы в папке: {dataFolder}");
