namespace ExpenseTracker.Core.Debugging;

/// <summary>
/// Абстракция для отладочного вывода. В тестах можно подставить логгер в память.
/// </summary>
public interface IDebugLogger
{
    void Log(string message);
}
