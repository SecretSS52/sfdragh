namespace ExpenseTracker.Core.Services;

/// <summary>
/// Короткий итог по списку расходов. Его удобно выводить в консоль и проверять в тестах.
/// </summary>
public sealed record ExpenseSummary(
    int Count,
    decimal TotalAmount,
    decimal AverageAmount,
    int BigExpenseCount,
    string BiggestCategory);
