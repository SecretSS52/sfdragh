using System.Text.Json.Serialization;

namespace MiniExam.Core;

/// <summary>
/// Одна оценка студента: кто, по какому предмету и сколько баллов получил.
/// </summary>
public sealed record Grade(
    [property: JsonPropertyName("ФИО")] string StudentName,
    [property: JsonPropertyName("Предмет")] string Subject,
    [property: JsonPropertyName("Балл")] int Score)
{
    /// <summary>
    /// Студент сдал, если набрал 60 баллов или больше.
    /// </summary>
    [JsonIgnore]
    public bool Passed => Score >= 60;
}

/// <summary>
/// Короткая сводка по всей ведомости.
/// </summary>
public sealed record GradeSummary(int Count, double Average, int Passed, int Failed);
