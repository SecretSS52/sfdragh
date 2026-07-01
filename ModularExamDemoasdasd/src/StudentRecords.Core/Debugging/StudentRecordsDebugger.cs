using StudentRecords.Core.Models;

namespace StudentRecords.Core.Debugging;

/// <summary>
/// Отдельный отладочный класс. Его использует консольное приложение,
/// но сообщения выводятся только в Debug-сборке.
/// </summary>
public sealed class StudentRecordsDebugger
{
    private readonly IDebugLogger _logger;

    public StudentRecordsDebugger(IDebugLogger logger)
    {
        _logger = logger;
    }

    public void DumpRecords(IEnumerable<StudentRecord> records)
    {
#if DEBUG
        // В Release-сборке этот блок не попадет в итоговую DLL.
        foreach (var record in records)
        {
            _logger.Log($"{record.StudentName}: {record.Subject}, балл {record.Score}");
        }
#endif
    }
}
