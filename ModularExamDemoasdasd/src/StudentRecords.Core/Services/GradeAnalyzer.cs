using StudentRecords.Core.Models;

namespace StudentRecords.Core.Services;

/// <summary>
/// Выполняет расчеты по ведомости. Здесь нет работы с файлами и консолью,
/// поэтому класс легко тестировать модульными тестами.
/// </summary>
public sealed class GradeAnalyzer
{
    public GradeSummary BuildSummary(IEnumerable<StudentRecord> records)
    {
        var list = records.ToList();

        // Для пустого списка возвращаем нулевую сводку, а не падаем с ошибкой.
        if (list.Count == 0)
        {
            return new GradeSummary(0, 0, 0, 0, null);
        }

        // До расчетов проверяем входные данные, чтобы ошибка была понятной.
        foreach (var record in list)
        {
            ValidateScore(record);
        }

        var average = Math.Round(list.Average(record => record.Score), 2);
        var passed = list.Count(record => record.IsPassed);
        var best = list.OrderByDescending(record => record.Score).First();

        return new GradeSummary(
            TotalRecords: list.Count,
            AverageScore: average,
            PassedCount: passed,
            FailedCount: list.Count - passed,
            BestRecord: best);
    }

    public bool IsExcellent(StudentRecord record)
    {
        ValidateScore(record);
        return record.Score >= 90;
    }

    private static void ValidateScore(StudentRecord record)
    {
        if (record.Score is < 0 or > 100)
        {
            throw new ArgumentOutOfRangeException(
                nameof(record),
                $"Балл должен быть в диапазоне от 0 до 100. Получено: {record.Score}.");
        }
    }
}
