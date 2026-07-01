using StudentRecords.Core.FileStorage;
using StudentRecords.Core.Models;

namespace StudentRecords.Tests;

[TestClass]
public sealed class FileStorageRoundTripTests
{
    private static readonly StudentRecord[] Records =
    [
        new StudentRecord("Анна", "C#", 95),
        new StudentRecord("Иван", "Git", 76),
        new StudentRecord("Мария", "Тестирование", 58)
    ];

    [TestMethod]
    public void CsvХранилище_СохраняетИЗагружаетЗаписи()
    {
        AssertRoundTrip(new CsvStudentStorage());
    }

    [TestMethod]
    public void JsonХранилище_СохраняетИЗагружаетЗаписи()
    {
        AssertRoundTrip(new JsonStudentStorage());
    }

    [TestMethod]
    public void ExcelХранилище_СохраняетИЗагружаетЗаписи()
    {
        AssertRoundTrip(new ExcelStudentStorage());
    }

    private static void AssertRoundTrip(IStudentFileStorage storage)
    {
        var directory = Path.Combine(Path.GetTempPath(), $"student-records-{Guid.NewGuid():N}");
        Directory.CreateDirectory(directory);

        try
        {
            var path = Path.Combine(directory, $"records{storage.FileExtension}");

            // Round-trip: сохраняем данные в файл, читаем обратно и сравниваем с исходным массивом.
            storage.Save(path, Records);
            var loaded = storage.Load(path);

            CollectionAssert.AreEqual(Records, loaded.ToArray());
        }
        finally
        {
            Directory.Delete(directory, recursive: true);
        }
    }
}
