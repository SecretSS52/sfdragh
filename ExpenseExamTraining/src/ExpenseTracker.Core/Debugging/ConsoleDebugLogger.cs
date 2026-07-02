namespace ExpenseTracker.Core.Debugging;

/// <summary>
/// Реальный логгер для консольного приложения.
/// </summary>
public sealed class ConsoleDebugLogger : IDebugLogger
{
    public void Log(string message)
    {
        Console.WriteLine($"[ОТЛАДКА] {message}");
    }
}
