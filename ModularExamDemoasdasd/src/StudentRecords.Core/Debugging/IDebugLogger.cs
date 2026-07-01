namespace StudentRecords.Core.Debugging;

/// <summary>
/// Небольшая абстракция для отладочного вывода. В тестах консоль можно заменить
/// объектом, который просто хранит сообщения в памяти.
/// </summary>
public interface IDebugLogger
{
    void Log(string message);
}
