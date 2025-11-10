namespace GameOfLife.Domain;

public class Cell
{
    public bool IsAlive { get; private set; }

    public Cell(bool isAlive)
    {
        IsAlive = isAlive;
    }

    public void SetAlive()
    {
        IsAlive = true;
    }

    public void SetDead()
    {
        IsAlive = false;
    }
}