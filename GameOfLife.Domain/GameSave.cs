namespace GameOfLife.Domain;

public class GameSave
{
    public string Name { get; set; }
    public int GenerationCount { get; set; }
    public int TotalBirthCount { get; set; }
    public int TotalDeathCount { get; set; }
    public int BoardWidth { get; set; }
    public int BoardHeight { get; set; }
    public int[] BirthConditions { get; set; }
    public int[] SurvivalConditions { get; set; }
    public bool[] CellStates { get; set; }

    public GameSave(
        string name,
        int generationCount,
        int totalBirthCount,
        int totalDeathCount,
        int boardWidth,
        int boardHeight,
        int[] birthConditions,
        int[] survivalConditions,
        bool[] cellStates)
    {
        Name = name;
        GenerationCount = generationCount;
        TotalBirthCount = totalBirthCount;
        TotalDeathCount = totalDeathCount;
        BoardWidth = boardWidth;
        BoardHeight = boardHeight;
        BirthConditions = birthConditions;
        SurvivalConditions = survivalConditions;
        CellStates = cellStates;
    }
}
