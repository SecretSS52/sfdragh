using ExpenseTracker.Core.Models;

namespace ExpenseTracker.Core.Debugging;

/// <summary>
/// Отладочный класс. Он показывает, какие расходы попали в программу.
/// В Release-сборке метод ничего не выводит.
/// </summary>
public sealed class ExpenseDebugger
{
    private readonly IDebugLogger _logger;

    public ExpenseDebugger(IDebugLogger logger)
    {
        _logger = logger;
    }

    public void Dump(IEnumerable<Expense> expenses)
    {
#if DEBUG
        foreach (var expense in expenses)
        {
            _logger.Log($"{expense.Date:yyyy-MM-dd}: {expense.Category}, {expense.Amount} руб., {expense.Comment}");
        }
#endif
    }
}
