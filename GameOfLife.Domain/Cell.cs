namespace GameOfLife.Domain;

public class Cell
{
    public bool IsAlive { get; set; }
    public Coords Coords { get; }

    public Cell(bool isAlive, Coords coords)
    {
        IsAlive = isAlive;
        Coords = coords;
    }
}