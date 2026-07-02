using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using ExpenseTracker.Core.Models;

namespace ExpenseTracker.Core.FileStorage;

/// <summary>
/// Модуль работы с JSON. Формат удобен для обмена данными между программами.
/// </summary>
public sealed class JsonExpenseStorage : IExpenseStorage
{
    private static readonly UTF8Encoding FileEncoding = new(encoderShouldEmitUTF8Identifier: true);

    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web)
    {
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        WriteIndented = true
    };

    public string FormatName => "JSON";

    public string FileExtension => ".json";

    public void Save(string path, IReadOnlyCollection<Expense> expenses)
    {
        var json = JsonSerializer.Serialize(expenses, JsonOptions);
        File.WriteAllText(path, json, FileEncoding);
    }

    public IReadOnlyList<Expense> Load(string path)
    {
        var json = File.ReadAllText(path, FileEncoding);
        return JsonSerializer.Deserialize<List<Expense>>(json, JsonOptions) ?? [];
    }
}
