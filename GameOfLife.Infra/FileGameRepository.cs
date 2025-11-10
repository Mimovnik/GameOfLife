using System.Text.Json;
using GameOfLife.Domain;

namespace GameOfLife.Infra;

public class FileGameRepository : IGameRepository
{
    private readonly string _savesDirectory;
    private const string FileExtension = ".gol.json";

    public FileGameRepository(string? savesDirectory = null)
    {
        _savesDirectory = savesDirectory ?? Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "GameOfLife",
            "Saves"
        );

        Directory.CreateDirectory(_savesDirectory);
    }

    public async Task SaveGameAsync(GameSave gameSave)
    {
        var fileName = GetSafeFileName(gameSave.Name) + FileExtension;
        var filePath = Path.Combine(_savesDirectory, fileName);

        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        var json = JsonSerializer.Serialize(gameSave, options);
        await File.WriteAllTextAsync(filePath, json);
    }

    public async Task<GameSave?> LoadGameAsync(string name)
    {
        var fileName = GetSafeFileName(name) + FileExtension;
        var filePath = Path.Combine(_savesDirectory, fileName);

        if (!File.Exists(filePath))
        {
            return null;
        }

        var json = await File.ReadAllTextAsync(filePath);
        return JsonSerializer.Deserialize<GameSave>(json);
    }

    public Task<IEnumerable<string>> GetSavedGameNamesAsync()
    {
        var files = Directory.GetFiles(_savesDirectory, $"*{FileExtension}");
        var names = files.Select(f => 
        {
            var fileName = Path.GetFileNameWithoutExtension(Path.GetFileNameWithoutExtension(f));
            return fileName;
        });

        return Task.FromResult(names);
    }

    public Task DeleteGameAsync(string name)
    {
        var fileName = GetSafeFileName(name) + FileExtension;
        var filePath = Path.Combine(_savesDirectory, fileName);

        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }

        return Task.CompletedTask;
    }

    private static string GetSafeFileName(string name)
    {
        var invalidChars = Path.GetInvalidFileNameChars();
        return new string(name.Select(c => invalidChars.Contains(c) ? '_' : c).ToArray());
    }
}
