using System.Diagnostics;

namespace ExpenseTracker.Core.Diagnostics;

/// <summary>
/// Простой отладочный помощник для сообщений Debug и Trace.
/// </summary>
public static class DebugHelper
{
    /// <summary>
    /// Пишет сообщение в окно отладки Visual Studio.
    /// </summary>
    public static void WriteDebug(string message)
    {
        Debug.WriteLine($"[DEBUG] {message}");
    }

    /// <summary>
    /// Пишет трассировочное сообщение.
    /// </summary>
    public static void WriteTrace(string message)
    {
        Trace.WriteLine($"[TRACE] {message}");
    }
}
