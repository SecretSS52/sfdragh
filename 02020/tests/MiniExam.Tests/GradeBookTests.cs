using MiniExam.Core;

namespace MiniExam.Tests;

[TestClass]
public sealed class GradeBookTests
{
    [TestMethod]
    public void Сводка_СчитаетСреднийБалл()
    {
        var grades = new[]
        {
            new Grade("Анна", "C#", 100),
            new Grade("Иван", "C#", 80),
            new Grade("Мария", "C#", 50)
        };

        var summary = GradeBook.GetSummary(grades);

        Assert.AreEqual(3, summary.Count);
        Assert.AreEqual(76.67, summary.Average, 0.001);
        Assert.AreEqual(2, summary.Passed);
        Assert.AreEqual(1, summary.Failed);
    }

    [TestMethod]
    public void НекорректныйБалл_ВызываетОшибку()
    {
        try
        {
            GradeBook.GetSummary([new Grade("Анна", "C#", 101)]);
            Assert.Fail("Ожидалась ошибка из-за балла больше 100.");
        }
        catch (ArgumentOutOfRangeException)
        {
            // Так и должно быть: 101 балл запрещен.
        }
    }

    [TestMethod]
    public void Файлы_CsvJsonXlsx_СохраняютсяИЧитаются()
    {
        var folder = Path.Combine(Path.GetTempPath(), $"mini-exam-{Guid.NewGuid():N}");
        Directory.CreateDirectory(folder);

        try
        {
            var grades = GradeBook.DemoData();

            var csv = Path.Combine(folder, "grades.csv");
            var json = Path.Combine(folder, "grades.json");
            var xlsx = Path.Combine(folder, "grades.xlsx");

            GradeBook.SaveCsv(csv, grades);
            GradeBook.SaveJson(json, grades);
            GradeBook.SaveXlsx(xlsx, grades);

            CollectionAssert.AreEqual(grades, GradeBook.LoadCsv(csv));
            CollectionAssert.AreEqual(grades, GradeBook.LoadJson(json));
            CollectionAssert.AreEqual(grades, GradeBook.LoadXlsx(xlsx));
        }
        finally
        {
            Directory.Delete(folder, recursive: true);
        }
    }
}
