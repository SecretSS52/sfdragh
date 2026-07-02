using ExpenseTracker.Core.Models;

namespace ExpenseTracker.Core.Services;

/// <summary>
/// Модуль расчетов. Здесь нет работы с консолью и файлами,
/// поэтому класс легко проверять модульными тестами.
/// </summary>
public sealed class ExpenseAnalyzer
{
    public ExpenseSummary BuildSummary(IEnumerable<Expense> expenses)
    {
        var list = expenses.ToList();

        // Проверяем данные до расчетов, чтобы ошибка была понятной.
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
            return new ExpenseSummary(0, 0, 0, 0, "нет данных");
        }

        var total = list.Sum(expense => expense.Amount);
        var average = Math.Round(total / list.Count, 2);
        var bigCount = list.Count(expense => expense.IsBig);

        // Группируем расходы по категориям и ищем категорию с максимальной суммой.
        var biggestCategory = list
            .GroupBy(expense => expense.Category)
            .OrderByDescending(group => group.Sum(expense => expense.Amount))
            .First()
            .Key;

        return new ExpenseSummary(list.Count, total, average, bigCount, biggestCategory);
    }
}
