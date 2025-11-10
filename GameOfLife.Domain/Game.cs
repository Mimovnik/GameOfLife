namespace GameOfLife.Domain;

public class Game
{
    private RuleSet _ruleSet { get; }
    public Board Board { get; private set; }
    public GameStats Stats { get; }
    
    public Game(RuleSet ruleSet, Board board)
    {
        _ruleSet = ruleSet;
        Stats = new GameStats();
        Board = board;
    }

    private Game(RuleSet ruleSet, Board board, GameStats stats)
    {
        _ruleSet = ruleSet;
        Board = board;
        Stats = stats;
    }

    public void NextGeneration()
    {
        Board = _ruleSet.NextGeneration(Board);
        Stats.UpdateFromBoard(Board);
    }

    public GameSave ToSave(string name)
    {
        var cellStates = new bool[Board.Dimensions.Width, Board.Dimensions.Height];
        
        foreach (var cell in Board.GetAllCells())
        {
            cellStates[cell.Coords.X, cell.Coords.Y] = cell.IsAlive;
        }

        return new GameSave(
            name,
            Stats.GenerationCount,
            Stats.TotalBirthCount,
            Stats.TotalDeathCount,
            Board.Dimensions.Width,
            Board.Dimensions.Height,
            _ruleSet.BirthConditions,
            _ruleSet.SurvivalConditions,
            cellStates
        );
    }

    public static Game FromSave(GameSave save)
    {
        var dimensions = BoardDimensions.Create(save.BoardWidth, save.BoardHeight);
        var board = new Board(dimensions);
        
        for (int x = 0; x < save.BoardWidth; x++)
        {
            for (int y = 0; y < save.BoardHeight; y++)
            {
                var cell = board.GetCellAt(new Coords(x, y));
                cell.IsAlive = save.CellStates[x, y];
            }
        }

        var ruleSet = new RuleSet(save.BirthConditions, save.SurvivalConditions);
        
        var stats = new GameStats(save.GenerationCount, save.TotalBirthCount, save.TotalDeathCount);

        return new Game(ruleSet, board, stats);
    }
}