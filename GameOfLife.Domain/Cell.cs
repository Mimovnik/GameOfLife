namespace GameOfLife.Domain;

public class Cell : IEquatable<Cell>
{
    public bool IsAlive { get; set; }
    public Coords Coords { get; }

    public Cell(bool isAlive, Coords coords)
    {
        IsAlive = isAlive;
        Coords = coords;
    }

    public bool Equals(Cell? other)
    {
        if (other is null) return false;
        return IsAlive == other.IsAlive && Coords.Equals(other.Coords);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Cell);
    }

    public override int GetHashCode()
    {
        int hash = 17;
        hash = HashCode.Combine(hash, IsAlive.GetHashCode(), Coords.GetHashCode());
        return hash;
    }
}