using StudentRecords.Core.Models;

namespace StudentRecords.Core.Services;

/// <summary>
/// Итог анализа ведомости. Такой отдельный тип удобно проверять в тестах.
/// </summary>
public sealed record GradeSummary(
    int TotalRecords,
    double AverageScore,
    int PassedCount,
    int FailedCount,
    StudentRecord? BestRecord);
