namespace ExpenseTracker.Core.Services;

/// <summary>
/// Результат анализа списка расходов.
/// </summary>
public sealed record ExpenseSummary(
    int Count,
    decimal TotalAmount,
    decimal AverageAmount,
    int BigExpenseCount,
    string BiggestCategory);
