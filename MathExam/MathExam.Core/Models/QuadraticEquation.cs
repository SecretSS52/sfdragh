namespace MathExam.Core.Models;

/// <summary>
/// Модель квадратного уравнения вида ax^2 + bx + c = 0.
/// </summary>
public class QuadraticEquation
{
    public string Name { get; set; } = string.Empty;

    public double A { get; set; }

    public double B { get; set; }

    public double C { get; set; }
}
