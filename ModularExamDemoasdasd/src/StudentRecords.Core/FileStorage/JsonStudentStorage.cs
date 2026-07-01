using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using StudentRecords.Core.Models;

namespace StudentRecords.Core.FileStorage;

/// <summary>
/// Сохраняет и загружает ведомость в читаемом формате JSON.
/// </summary>
public sealed class JsonStudentStorage : IStudentFileStorage
{
    private static readonly UTF8Encoding FileEncoding = new(encoderShouldEmitUTF8Identifier: true);

    private static readonly JsonSerializerOptions SerializerOptions = new(JsonSerializerDefaults.Web)
    {
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        WriteIndented = true
    };

    public string FormatName => "JSON";

    public string FileExtension => ".json";

    public void Save(string path, IReadOnlyCollection<StudentRecord> records)
    {
        var json = JsonSerializer.Serialize(records, SerializerOptions);
        File.WriteAllText(path, json, FileEncoding);
    }

    public IReadOnlyList<StudentRecord> Load(string path)
    {
        var json = File.ReadAllText(path, FileEncoding);
        return JsonSerializer.Deserialize<List<StudentRecord>>(json, SerializerOptions) ?? [];
    }
}
