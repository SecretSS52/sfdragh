using System.Text.Json.Serialization;

namespace StudentRecords.Core.Models;

/// <summary>
/// Одна строка ведомости: студент, предмет и полученный балл.
/// </summary>
public sealed record StudentRecord(
    [property: JsonPropertyName("ФИО")] string StudentName,
    [property: JsonPropertyName("Предмет")] string Subject,
    [property: JsonPropertyName("Балл")] int Score)
{
    /// <summary>
    /// Считаем, что студент сдал предмет, если набрал 60 баллов или больше.
    /// </summary>
    [JsonIgnore]
    public bool IsPassed => Score >= 60;
}
