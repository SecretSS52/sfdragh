using ExpenseTracker.Core.Models;
using ExpenseTracker.Core.Services;

namespace ExpenseTracker.Tests;

[TestClass]
public sealed class ExpenseAnalyzerTests
{
    [TestMethod]
    public void Сводка_СчитаетКоличествоСуммуИСреднее()
    {
        var expenses = new[]
        {
            new Expense(new DateOnly(2026, 7, 1), "Еда", 500m, "Обед"),
            new Expense(new DateOnly(2026, 7, 1), "Учеба", 1500m, "Книга"),
            new Expense(new DateOnly(2026, 7, 2), "Еда", 300m, "Завтрак")
        };

        var summary = new ExpenseAnalyzer().BuildSummary(expenses);

        Assert.AreEqual(3, summary.Count);
        Assert.AreEqual(2300m, summary.TotalAmount);
        Assert.AreEqual(766.67m, summary.AverageAmount);
        Assert.AreEqual(1, summary.BigExpenseCount);
        Assert.AreEqual("Учеба", summary.BiggestCategory);
    }

    [TestMethod]
    public void Сводка_ДляПустогоСпискаВозвращаетНули()
    {
        var summary = new ExpenseAnalyzer().BuildSummary([]);

        Assert.AreEqual(0, summary.Count);
        Assert.AreEqual(0m, summary.TotalAmount);
        Assert.AreEqual(0m, summary.AverageAmount);
        Assert.AreEqual(0, summary.BigExpenseCount);
    }

    [TestMethod]
    public void Сводка_ЗапрещаетОтрицательныеРасходы()
    {
        try
        {
            _ = new ExpenseAnalyzer().BuildSummary(
            [
                new Expense(new DateOnly(2026, 7, 1), "Ошибка", -10m, "Нельзя")
            ]);

            Assert.Fail("Ожидалась ошибка из-за отрицательной суммы.");
        }
        catch (ArgumentOutOfRangeException)
        {
            // Ожидаемое поведение: отрицательный расход запрещен.
        }
    }
}
