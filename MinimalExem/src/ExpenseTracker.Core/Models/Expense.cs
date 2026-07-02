namespace ExpenseTracker.Core.Models;

/// <summary>
/// Одна запись расхода: дата, категория, сумма и описание.
/// </summary>
public sealed record Expense(
    DateOnly Date,
    string Category,
    decimal Amount,
    string Comment)
{
    /// <summary>
    /// Показывает, является ли расход крупным.
    /// </summary>
    public bool IsBig => Amount >= 1000m;
}
