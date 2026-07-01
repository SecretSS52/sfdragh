using MiniExam.Core;

var dataFolder = Path.Combine(AppContext.BaseDirectory, "data");
Directory.CreateDirectory(dataFolder);

var grades = GradeBook.DemoData();
new DebugPrinter().Print(grades);

var csvPath = Path.Combine(dataFolder, "grades.csv");
var jsonPath = Path.Combine(dataFolder, "grades.json");
var xlsxPath = Path.Combine(dataFolder, "grades.xlsx");

GradeBook.SaveCsv(csvPath, grades);
GradeBook.SaveJson(jsonPath, grades);
GradeBook.SaveXlsx(xlsxPath, grades);

var summary = GradeBook.GetSummary(grades);

Console.WriteLine("Мини-проект для экзамена");
Console.WriteLine($"Записей: {summary.Count}");
Console.WriteLine($"Средний балл: {summary.Average}");
Console.WriteLine($"Сдали: {summary.Passed}");
Console.WriteLine($"Не сдали: {summary.Failed}");
Console.WriteLine($"Файлы созданы в папке: {dataFolder}");
