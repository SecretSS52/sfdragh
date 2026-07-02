using ExpenseExam.Core.Models;
using ExpenseExam.Core.Services;

namespace ExpenseExam.Tests;

[TestClass]
public sealed class ExpenseServiceTests
{
    [TestMethod]
    public void GetTotal_ReturnsTotalAmount()
    {
        var service = new ExpenseService();
        var expenses = CreateExpenses();

        var total = service.GetTotal(expenses);

        Assert.AreEqual(2070m, total);
    }

    [TestMethod]
    public void GetAverage_ReturnsAverageAmount()
    {
        var service = new ExpenseService();
        var expenses = CreateExpenses();

        var average = service.GetAverage(expenses);

        Assert.AreEqual(690m, average);
    }

    [TestMethod]
    public void GetMaxExpense_ReturnsLargestExpense()
    {
        var service = new ExpenseService();
        var expenses = CreateExpenses();

        var maxExpense = service.GetMaxExpense(expenses);

        Assert.IsNotNull(maxExpense);
        Assert.AreEqual(1500m, maxExpense.Amount);
        Assert.AreEqual("Учеба", maxExpense.Category);
    }

    private static List<Expense> CreateExpenses()
    {
        return
        [
            new Expense { Date = new DateTime(2026, 7, 1), Category = "Еда", Amount = 450m, Comment = "Обед" },
            new Expense { Date = new DateTime(2026, 7, 2), Category = "Транспорт", Amount = 120m, Comment = "Автобус" },
            new Expense { Date = new DateTime(2026, 7, 3), Category = "Учеба", Amount = 1500m, Comment = "Курс C#" }
        ];
    }
}
