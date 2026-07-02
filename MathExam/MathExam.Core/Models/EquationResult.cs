namespace MathExam.Core.Models;

/// <summary>
/// Результат решения квадратного уравнения.
/// </summary>
public class EquationResult
{
    public double Discriminant { get; set; }

    public double? X1 { get; set; }

    public double? X2 { get; set; }

    public string Message { get; set; } = string.Empty;
}
