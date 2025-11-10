namespace GameOfLife.Domain;

public class Cell
{
    public bool IsAlive { get; private set; }
    public Coords Coords { get; }

    public Cell(bool isAlive, Coords coords)
    {
        IsAlive = isAlive;
        Coords = coords;
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