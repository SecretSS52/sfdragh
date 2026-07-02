using ExpenseTracker.Core.Models;

namespace ExpenseTracker.Core.FileStorage;

/// <summary>
/// Общий интерфейс для файловых модулей.
/// Благодаря ему консольное приложение одинаково работает с CSV, JSON и XLSX.
/// </summary>
public interface IExpenseStorage
{
    string FormatName { get; }

    string FileExtension { get; }

    void Save(string path, IReadOnlyCollection<Expense> expenses);

    IReadOnlyList<Expense> Load(string path);
}
