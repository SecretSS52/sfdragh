using ExpenseTracker.Core.Diagnostics;
using ExpenseTracker.Core.Models;

namespace ExpenseTracker.Core.Services;

/// <summary>
/// Сервис анализа расходов: считает сумму, среднее значение и дополнительные показатели.
/// </summary>
public sealed class ExpenseAnalyzer
{
    /// <summary>
    /// Строит краткую статистику по списку расходов.
    /// </summary>
    public ExpenseSummary BuildSummary(IEnumerable<Expense> expenses)
    {
        var list = expenses.ToList();
        DebugHelper.WriteDebug($"Начат анализ расходов. Количество записей: {list.Count}.");

        foreach (var expense in list)
        {
            if (expense.Amount < 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(expenses),
                    "Сумма расхода не может быть отрицательной.");
            }
        }

        if (list.Count == 0)
        {
            DebugHelper.WriteTrace("Список расходов пуст, возвращена нулевая статистика.");
            return new ExpenseSummary(0, 0, 0, 0, "нет данных");
        }

        var total = list.Sum(expense => expense.Amount);
        var average = Math.Round(total / list.Count, 2);
        var bigCount = list.Count(expense => expense.IsBig);

        var biggestCategory = list
            .GroupBy(expense => expense.Category)
            .OrderByDescending(group => group.Sum(expense => expense.Amount))
            .First()
            .Key;

        DebugHelper.WriteTrace($"Анализ завершен. Общая сумма: {total}.");
        return new ExpenseSummary(list.Count, total, average, bigCount, biggestCategory);
    }
}
