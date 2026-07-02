using ExpenseTracker.Core.Debugging;
using ExpenseTracker.Core.Models;

namespace ExpenseTracker.Tests;

[TestClass]
public sealed class ExpenseDebuggerTests
{
    [TestMethod]
    public void Отладчик_ПишетРасходыВДебагСборке()
    {
        var logger = new MemoryDebugLogger();
        var expenses = new[]
        {
            new Expense(new DateOnly(2026, 7, 1), "Еда", 500m, "Обед"),
            new Expense(new DateOnly(2026, 7, 2), "Учеба", 1500m, "Курс")
        };

        new ExpenseDebugger(logger).Dump(expenses);

#if DEBUG
        Assert.HasCount(2, logger.Messages);
        StringAssert.Contains(logger.Messages[0], "Еда");
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
