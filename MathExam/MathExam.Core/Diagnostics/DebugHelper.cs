using System.Diagnostics;

namespace MathExam.Core.Diagnostics;

/// <summary>
/// Простой отладочный класс для сообщений Debug и Trace.
/// </summary>
public static class DebugHelper
{
    /// <summary>
    /// Пишет сообщение в Debug.
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
