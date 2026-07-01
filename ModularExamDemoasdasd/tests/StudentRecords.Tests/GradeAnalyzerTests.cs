using StudentRecords.Core.Models;
using StudentRecords.Core.Services;

namespace StudentRecords.Tests;

[TestClass]
public sealed class GradeAnalyzerTests
{
    [TestMethod]
    public void Сводка_СчитаетСреднийБаллИКоличествоСдавших()
    {
        var records = new[]
        {
            new StudentRecord("Анна", "C#", 100),
            new StudentRecord("Иван", "C#", 80),
            new StudentRecord("Мария", "C#", 50)
        };

        var summary = new GradeAnalyzer().BuildSummary(records);

        Assert.AreEqual(3, summary.TotalRecords);
        Assert.AreEqual(76.67, summary.AverageScore, 0.001);
        Assert.AreEqual(2, summary.PassedCount);
        Assert.AreEqual(1, summary.FailedCount);
        Assert.AreEqual("Анна", summary.BestRecord?.StudentName);
    }

    [TestMethod]
    public void ОтличнаяОценка_ВозвращаетTrueЕслиБаллНеМеньшеДевяноста()
    {
        var record = new StudentRecord("Анна", "Тестирование", 90);

        Assert.IsTrue(new GradeAnalyzer().IsExcellent(record));
    }

    [TestMethod]
    public void Сводка_ОтклоняетНекорректныйБалл()
    {
        var records = new[]
        {
            new StudentRecord("Анна", "C#", 101)
        };

        try
        {
            _ = new GradeAnalyzer().BuildSummary(records);
            Assert.Fail("Ожидалось исключение ArgumentOutOfRangeException.");
        }
        catch (ArgumentOutOfRangeException)
        {
            // Ожидаемый результат для некорректного балла.
        }
    }
}
