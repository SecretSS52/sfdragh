using ExpenseExam.Core.FileStorage;
using ExpenseExam.Core.Models;

namespace ExpenseExam.Tests;

[TestClass]
public sealed class FileStorageTests
{
    [TestMethod]
    public void CsvStorage_SavesAndLoadsExpenses()
    {
        var path = CreateTempPath(".csv");
        var storage = new CsvStorage();
        var expenses = CreateExpenses();

        storage.Save(path, expenses);
        var loaded = storage.Load(path);

        AssertExpensesEqual(expenses, loaded);
    }

    [TestMethod]
    public void ExcelStorage_SavesAndLoadsExpenses()
    {
        var path = CreateTempPath(".xlsx");
        var storage = new ExcelStorage();
        var expenses = CreateExpenses();

        storage.Save(path, expenses);
        var loaded = storage.Load(path);

        AssertExpensesEqual(expenses, loaded);
    }

    [TestMethod]
    public void JsonStorage_SavesAndLoadsExpenses()
    {
        var path = CreateTempPath(".json");
        var storage = new JsonStorage();
        var expenses = CreateExpenses();

        storage.Save(path, expenses);
        var loaded = storage.Load(path);

        AssertExpensesEqual(expenses, loaded);
    }

    private static string CreateTempPath(string extension)
    {
        return Path.Combine(Path.GetTempPath(), $"expense-exam-{Guid.NewGuid():N}{extension}");
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

    private static void AssertExpensesEqual(List<Expense> expected, List<Expense> actual)
    {
        Assert.HasCount(expected.Count, actual);

        for (var i = 0; i < expected.Count; i++)
        {
            Assert.AreEqual(expected[i].Date, actual[i].Date);
            Assert.AreEqual(expected[i].Category, actual[i].Category);
            Assert.AreEqual(expected[i].Amount, actual[i].Amount);
            Assert.AreEqual(expected[i].Comment, actual[i].Comment);
        }
    }
}
