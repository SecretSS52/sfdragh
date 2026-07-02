using System.Text.Json.Serialization;

namespace ExpenseTracker.Core.Models;

/// <summary>
/// Одна запись расхода: дата, категория, сумма и короткое описание.
/// Это простая модель данных, с которой работают остальные модули проекта.
/// </summary>
public sealed record Expense(
    [property: JsonPropertyName("Дата")] DateOnly Date,
    [property: JsonPropertyName("Категория")] string Category,
    [property: JsonPropertyName("Сумма")] decimal Amount,
    [property: JsonPropertyName("Описание")] string Comment)
{
    /// <summary>
    /// Вычисляемое свойство: крупным считаем расход от 1000 рублей.
    /// В файлы его не сохраняем, потому что его всегда можно пересчитать.
    /// </summary>
    [JsonIgnore]
    public bool IsBig => Amount >= 1000m;
}
