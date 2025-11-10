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

    public void NextGeneration()
    {
        Board = _ruleSet.NextGeneration(Board);
        Stats.UpdateFromBoard(Board);
    }
}