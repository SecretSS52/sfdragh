using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using ExpenseExam.Core.Diagnostics;
using ExpenseExam.Core.Models;

namespace ExpenseExam.Core.FileStorage;

/// <summary>
/// Сохраняет и загружает расходы в JSON-файл.
/// </summary>
public class JsonStorage
{
    private static readonly UTF8Encoding FileEncoding = new(encoderShouldEmitUTF8Identifier: true);

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        WriteIndented = true
    };

    /// <summary>
    /// Сохраняет список расходов в JSON-файл.
    /// </summary>
    public void Save(string path, List<Expense> expenses)
    {
        DebugHelper.WriteDebug($"Сохранение JSON: {path}");

        var json = JsonSerializer.Serialize(expenses, JsonOptions);
        File.WriteAllText(path, json, FileEncoding);
    }

    /// <summary>
    /// Загружает список расходов из JSON-файла.
    /// </summary>
    public List<Expense> Load(string path)
    {
        DebugHelper.WriteTrace($"Загрузка JSON: {path}");

        var json = File.ReadAllText(path, FileEncoding);
        return JsonSerializer.Deserialize<List<Expense>>(json, JsonOptions) ?? [];
    }
}
