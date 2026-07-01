namespace MiniExam.Core;

/// <summary>
/// Простой отладочный класс. В Debug-сборке показывает, какие записи обрабатываются.
/// </summary>
public sealed class DebugPrinter
{
    public void Print(IEnumerable<Grade> grades)
    {
#if DEBUG
        foreach (var grade in grades)
        {
            Console.WriteLine($"[ОТЛАДКА] {grade.StudentName}: {grade.Subject}, {grade.Score} баллов");
        }
#endif
    }
}
