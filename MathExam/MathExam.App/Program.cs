using MathExam.Core.FileStorage;
using MathExam.Core.Models;
using MathExam.Core.Services;

var equations = new List<QuadraticEquation>
{
    new QuadraticEquation { Name = "Два корня", A = 1, B = -3, C = 2 },
    new QuadraticEquation { Name = "Один корень", A = 1, B = 2, C = 1 },
    new QuadraticEquation { Name = "Нет корней", A = 1, B = 2, C = 5 }
};

var service = new EquationService();
var csvStorage = new CsvStorage();
var excelStorage = new ExcelStorage();

var dataFolder = Path.Combine(AppContext.BaseDirectory, "data");
Directory.CreateDirectory(dataFolder);

var csvPath = Path.Combine(dataFolder, "equations.csv");
var xlsxPath = Path.Combine(dataFolder, "equations.xlsx");

Console.WriteLine("Решение квадратных уравнений");
Console.WriteLine("============================");

foreach (var equation in equations)
{
    var result = service.Solve(equation);
    Console.WriteLine($"{equation.Name}: {result.Message}, D = {result.Discriminant}, x1 = {Format(result.X1)}, x2 = {Format(result.X2)}");
}

csvStorage.Save(csvPath, equations);
var equationsFromCsv = csvStorage.Load(csvPath);
Console.WriteLine($"CSV загружено уравнений: {equationsFromCsv.Count}");

excelStorage.Save(xlsxPath, equations);
var equationsFromXlsx = excelStorage.Load(xlsxPath);
Console.WriteLine($"XLSX загружено уравнений: {equationsFromXlsx.Count}");

Console.WriteLine($"Файлы созданы в папке: {dataFolder}");

static string Format(double? value)
{
    return value?.ToString("F2") ?? "нет";
}
