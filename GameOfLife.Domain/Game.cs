namespace GameOfLife.Domain;

public class Game
{
    private RuleSet _ruleSet { get; }
    public Board Board { get; private set; }
    public Game(RuleSet ruleSet, Board board)
    {
        _ruleSet = ruleSet;
        Board = board;
    }

    public void NextGeneration()
    {
       Board = _ruleSet.NextGeneration(Board);
    }
}