namespace GameOfLife.Domain;

public interface IGameRepository
{
    Task SaveGameAsync(GameSave gameSave);
    Task<GameSave?> LoadGameAsync(string name);
    Task<IEnumerable<string>> GetSavedGameNamesAsync();
    Task DeleteGameAsync(string name);
}
