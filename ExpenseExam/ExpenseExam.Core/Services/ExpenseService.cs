using ExpenseExam.Core.Diagnostics;
using ExpenseExam.Core.Models;

namespace ExpenseExam.Core.Services;

/// <summary>
/// Сервис с простыми расчетами по списку расходов.
/// </summary>
public class ExpenseService
{
    /// <summary>
    /// Возвращает общую сумму расходов.
    /// </summary>
    public decimal GetTotal(List<Expense> expenses)
    {
        DebugHelper.WriteDebug($"Расчет общей суммы. Записей: {expenses.Count}");
        return expenses.Sum(expense => expense.Amount);
    }

    /// <summary>
    /// Возвращает средний расход.
    /// </summary>
    public decimal GetAverage(List<Expense> expenses)
    {
        DebugHelper.WriteDebug("Расчет среднего расхода.");

        if (expenses.Count == 0)
        {
            return 0;
        }

        return GetTotal(expenses) / expenses.Count;
    }

    /// <summary>
    /// Возвращает самый большой расход.
    /// </summary>
    public Expense? GetMaxExpense(List<Expense> expenses)
    {
        DebugHelper.WriteTrace("Поиск самого большого расхода.");

        if (expenses.Count == 0)
        {
            return null;
        }

        return expenses.OrderByDescending(expense => expense.Amount).First();
    }
}
