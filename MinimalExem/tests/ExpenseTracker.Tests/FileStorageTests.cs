using ExpenseTracker.Core.FileStorage;
using ExpenseTracker.Core.Models;

namespace ExpenseTracker.Tests;

[TestClass]
public sealed class FileStorageTests
{
    private static readonly Expense[] Expenses =
    [
        new Expense(new DateOnly(2026, 7, 1), "Еда", 450.50m, "Обед"),
        new Expense(new DateOnly(2026, 7, 2), "Учеба", 1500m, "Курс по C#"),
        new Expense(new DateOnly(2026, 7, 3), "Дом", 250m, "Лампа")
    ];

    [TestMethod]
    public void Csv_СохраняетИЗагружаетРасходы()
    {
        AssertRoundTrip(new CsvExpenseStorage());
    }

    [TestMethod]
    public void Xlsx_СохраняетИЗагружаетРасходы()
    {
        AssertRoundTrip(new ExcelExpenseStorage());
    }

    private static void AssertRoundTrip(IExpenseStorage storage)
    {
        var folder = Path.Combine(Path.GetTempPath(), $"expense-test-{Guid.NewGuid():N}");
        Directory.CreateDirectory(folder);

        try
        {
            var path = Path.Combine(folder, $"expenses{storage.FileExtension}");

            storage.Save(path, Expenses);
            var loaded = storage.Load(path);

            CollectionAssert.AreEqual(Expenses, loaded.ToArray());
        }
        finally
        {
            Directory.Delete(folder, recursive: true);
        }
    }
}
