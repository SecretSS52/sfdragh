using ExpenseTracker.Core.Models;

namespace ExpenseTracker.Core.DemoData;

/// <summary>
/// Демонстрационные расходы для запуска консольного приложения.
/// </summary>
public static class SampleExpenses
{
    /// <summary>
    /// Создает небольшой список расходов для примера.
    /// </summary>
    public static IReadOnlyList<Expense> Create()
    {
        return
        [
            new Expense(new DateOnly(2026, 7, 1), "Еда", 450.50m, "Обед в столовой"),
            new Expense(new DateOnly(2026, 7, 1), "Транспорт", 120m, "Метро"),
            new Expense(new DateOnly(2026, 7, 2), "Учеба", 1500m, "Курс по C#"),
            new Expense(new DateOnly(2026, 7, 2), "Еда", 320m, "Продукты")
        ];
    }
}
