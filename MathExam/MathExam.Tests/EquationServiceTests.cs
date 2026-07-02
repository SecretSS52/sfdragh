using MathExam.Core.Models;
using MathExam.Core.Services;

namespace MathExam.Tests;

[TestClass]
public sealed class EquationServiceTests
{
    [TestMethod]
    public void GetDiscriminant_ReturnsCorrectValue()
    {
        var service = new EquationService();
        var equation = new QuadraticEquation { Name = "Тест", A = 1, B = -3, C = 2 };

        var discriminant = service.GetDiscriminant(equation);

        Assert.AreEqual(1, discriminant);
    }

    [TestMethod]
    public void Solve_ReturnsTwoRoots()
    {
        var service = new EquationService();
        var equation = new QuadraticEquation { Name = "Тест", A = 1, B = -3, C = 2 };

        var result = service.Solve(equation);

        Assert.AreEqual("Два корня", result.Message);
        Assert.AreEqual(1, result.X1);
        Assert.AreEqual(2, result.X2);
    }

    [TestMethod]
    public void Solve_ReturnsNoRealRoots()
    {
        var service = new EquationService();
        var equation = new QuadraticEquation { Name = "Тест", A = 1, B = 2, C = 5 };

        var result = service.Solve(equation);

        Assert.AreEqual("Нет действительных корней", result.Message);
        Assert.IsNull(result.X1);
        Assert.IsNull(result.X2);
    }
}
