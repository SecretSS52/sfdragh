using System.Globalization;
using System.Text;
using MathExam.Core.Diagnostics;
using MathExam.Core.Models;

namespace MathExam.Core.FileStorage;

/// <summary>
/// Сохраняет и загружает квадратные уравнения в CSV-файл.
/// </summary>
public class CsvStorage
{
    private static readonly UTF8Encoding FileEncoding = new(encoderShouldEmitUTF8Identifier: true);

    /// <summary>
    /// Сохраняет список уравнений в CSV-файл с разделителем ";".
    /// </summary>
    public void Save(string path, List<QuadraticEquation> equations)
    {
        DebugHelper.WriteDebug($"Сохранение CSV: {path}");

        using var writer = new StreamWriter(path, append: false, FileEncoding);
        writer.WriteLine("Name;A;B;C");

        foreach (var equation in equations)
        {
            writer.WriteLine(
                $"{equation.Name};{equation.A.ToString(CultureInfo.InvariantCulture)};{equation.B.ToString(CultureInfo.InvariantCulture)};{equation.C.ToString(CultureInfo.InvariantCulture)}");
        }
    }

    /// <summary>
    /// Загружает список уравнений из CSV-файла.
    /// </summary>
    public List<QuadraticEquation> Load(string path)
    {
        DebugHelper.WriteTrace($"Загрузка CSV: {path}");

        var equations = new List<QuadraticEquation>();
        var lines = File.ReadAllLines(path, FileEncoding).Skip(1);

        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            var parts = line.Split(';');

            equations.Add(new QuadraticEquation
            {
                Name = parts[0],
                A = double.Parse(parts[1], CultureInfo.InvariantCulture),
                B = double.Parse(parts[2], CultureInfo.InvariantCulture),
                C = double.Parse(parts[3], CultureInfo.InvariantCulture)
            });
        }

        return equations;
    }
}
