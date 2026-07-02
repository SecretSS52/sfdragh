using ExpenseTracker.Core.Models;

namespace ExpenseTracker.Core.FileStorage;

/// <summary>
/// Общий интерфейс для файловых модулей.
/// Благодаря ему консольное приложение одинаково работает с CSV и XLSX.
/// </summary>
public interface IExpenseStorage
{
    string FormatName { get; }

    string FileExtension { get; }

    /// <summary>
    /// Сохраняет расходы в файл.
    /// </summary>
    void Save(string path, IReadOnlyCollection<Expense> expenses);

    /// <summary>
    /// Загружает расходы из файла.
    /// </summary>
    IReadOnlyList<Expense> Load(string path);
}
