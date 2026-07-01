using StudentRecords.Core.Models;

namespace StudentRecords.Core.FileStorage;

/// <summary>
/// Общий контракт для файловых модулей. Благодаря интерфейсу консольное приложение
/// одинаково работает с CSV, JSON и XLSX.
/// </summary>
public interface IStudentFileStorage
{
    string FormatName { get; }

    string FileExtension { get; }

    void Save(string path, IReadOnlyCollection<StudentRecord> records);

    IReadOnlyList<StudentRecord> Load(string path);
}
