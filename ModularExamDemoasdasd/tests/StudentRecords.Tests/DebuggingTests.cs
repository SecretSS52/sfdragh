using StudentRecords.Core.Debugging;
using StudentRecords.Core.Models;

namespace StudentRecords.Tests;

[TestClass]
public sealed class DebuggingTests
{
    [TestMethod]
    public void Отладчик_ПишетСообщенияВДебагСборке()
    {
        var logger = new MemoryDebugLogger();
        var records = new[]
        {
            new StudentRecord("Анна", "C#", 95),
            new StudentRecord("Иван", "Git", 76)
        };

        new StudentRecordsDebugger(logger).DumpRecords(records);

#if DEBUG
        Assert.HasCount(2, logger.Messages);
        StringAssert.Contains(logger.Messages[0], "Анна");
#else
        Assert.HasCount(0, logger.Messages);
#endif
    }

    private sealed class MemoryDebugLogger : IDebugLogger
    {
        public List<string> Messages { get; } = [];

        public void Log(string message)
        {
            Messages.Add(message);
        }
    }
}
