using System.Diagnostics;

namespace ExpenseExam.Core.Diagnostics;

/// <summary>
/// Простой класс для отладочных сообщений.
/// </summary>
public static class DebugHelper
{
    /// <summary>
    /// Пишет сообщение в окно Debug.
    /// </summary>
    public static void WriteDebug(string message)
    {
        Debug.WriteLine($"[DEBUG] {message}");
    }

    /// <summary>
    /// Пишет сообщение в Trace.
    /// </summary>
    public static void WriteTrace(string message)
    {
        Trace.WriteLine($"[TRACE] {message}");
    }
}
