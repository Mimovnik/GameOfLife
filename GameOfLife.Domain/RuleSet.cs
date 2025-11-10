namespace GameOfLife.Domain;

public class RuleSet
{
    public int[] BirthConditions { get; }
    public int[] SurvivalConditions { get; }

    public RuleSet(int[] birthConditions, int[] survivalConditions)
    {
        BirthConditions = birthConditions;
        SurvivalConditions = survivalConditions;
    }

    public Board NextGeneration(Board currentBoard)
    {
        var nextGeneration = currentBoard.Clone();

        foreach (Cell cell in currentBoard.GetAllCells())
        {
            var coords = cell.Coords;
            var aliveNeighbors = currentBoard.GetNeighborsOf(coords)
                .Count(c => c.IsAlive);

            if (cell.IsAlive)
            {
                var survives = SurvivalConditions.Contains(aliveNeighbors);
                nextGeneration.GetCellAt(coords).IsAlive = survives;
            }
            else
            {
                var isBorn = BirthConditions.Contains(aliveNeighbors);
                nextGeneration.GetCellAt(coords).IsAlive = isBorn;
            }
        }

        return nextGeneration;
    }
}