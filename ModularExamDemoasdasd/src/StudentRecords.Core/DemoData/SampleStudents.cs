using StudentRecords.Core.Models;

namespace StudentRecords.Core.DemoData;

/// <summary>
/// Фиксированные демонстрационные данные для консольного приложения и ручной проверки.
/// </summary>
public static class SampleStudents
{
    public static IReadOnlyList<StudentRecord> Create()
    {
        return
        [
            new StudentRecord("Анна Петрова", "C#", 95),
            new StudentRecord("Иван Соколов", "Базы данных", 72),
            new StudentRecord("Мария Волкова", "Тестирование", 58),
            new StudentRecord("Олег Смирнов", "Git", 84)
        ];
    }
}
