namespace StudentRecords.Core.Debugging;

/// <summary>
/// Пишет диагностические сообщения в консоль.
/// </summary>
public sealed class ConsoleDebugLogger : IDebugLogger
{
    public void Log(string message)
    {
        Console.WriteLine($"[ОТЛАДКА] {message}");
    }
}
