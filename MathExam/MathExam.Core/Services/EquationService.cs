using MathExam.Core.Diagnostics;
using MathExam.Core.Models;

namespace MathExam.Core.Services;

/// <summary>
/// Сервис для решения квадратных уравнений.
/// </summary>
public class EquationService
{
    /// <summary>
    /// Считает дискриминант по формуле D = b^2 - 4ac.
    /// </summary>
    public double GetDiscriminant(QuadraticEquation equation)
    {
        DebugHelper.WriteDebug($"Расчет дискриминанта для уравнения: {equation.Name}");
        return equation.B * equation.B - 4 * equation.A * equation.C;
    }

    /// <summary>
    /// Решает квадратное уравнение и возвращает корни.
    /// </summary>
    public EquationResult Solve(QuadraticEquation equation)
    {
        if (equation.A == 0)
        {
            throw new ArgumentException("Коэффициент A не должен быть равен 0.");
        }

        var discriminant = GetDiscriminant(equation);
        DebugHelper.WriteTrace($"Дискриминант равен {discriminant}.");

        if (discriminant < 0)
        {
            return new EquationResult
            {
                Discriminant = discriminant,
                Message = "Нет действительных корней"
            };
        }

        if (discriminant == 0)
        {
            var x = -equation.B / (2 * equation.A);

            return new EquationResult
            {
                Discriminant = discriminant,
                X1 = x,
                X2 = x,
                Message = "Один корень"
            };
        }

        var sqrt = Math.Sqrt(discriminant);

        return new EquationResult
        {
            Discriminant = discriminant,
            X1 = (-equation.B - sqrt) / (2 * equation.A),
            X2 = (-equation.B + sqrt) / (2 * equation.A),
            Message = "Два корня"
        };
    }
}
