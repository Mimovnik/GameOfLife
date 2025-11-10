namespace GameOfLife.Domain;

public class Coords : IEquatable<Coords>
{
    public int X { get; }
    public int Y { get; }

    public Coords(int x, int y)
    {
        X = x;
        Y = y;
    }

    public bool Equals(Coords? other)
    {
        if (other is null) return false;
        return X == other.X && Y == other.Y;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Coords);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }
}