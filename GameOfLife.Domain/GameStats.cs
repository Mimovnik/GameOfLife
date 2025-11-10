namespace GameOfLife.Domain;

public class GameStats
{
    public int GenerationCount { get; private set; }
    public int TotalBirthCount { get; private set; }
    public int TotalDeathCount { get; private set; }

    public GameStats(int generationCount = 0, int totalBirthCount = 0, int totalDeathCount = 0)
    {
        GenerationCount = generationCount;
        TotalBirthCount = totalBirthCount;
        TotalDeathCount = totalDeathCount;
    }

    public void UpdateFromBoard(Board board)
    {
        TotalBirthCount += board.Stats.BirthCount;
        TotalDeathCount += board.Stats.DeathCount;
        GenerationCount++;
    }

    public void Reset()
    {
        GenerationCount = 0;
        TotalBirthCount = 0;
        TotalDeathCount = 0;
    }
}
