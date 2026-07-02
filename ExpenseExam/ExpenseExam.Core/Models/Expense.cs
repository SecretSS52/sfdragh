namespace ExpenseExam.Core.Models;

/// <summary>
/// Модель одного расхода.
/// </summary>
public class Expense
{
    public DateTime Date { get; set; }

    public string Category { get; set; } = string.Empty;

    public decimal Amount { get; set; }

    public string Comment { get; set; } = string.Empty;
}
