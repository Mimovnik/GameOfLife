namespace GameOfLife.Domain;

public class BoardDimensions : IEquatable<BoardDimensions>
{
    public int Width { get; }
    public int Height { get; }

    private BoardDimensions(int width, int height)
    {
        Width = width;
        Height = height;
    }

    public static BoardDimensions Create(int width, int height)
    {
        if (width <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(width), "Width must be greater than zero.");
        }
        if (height <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(height), "Height must be greater than zero.");
        }

        return new BoardDimensions(width, height);
    }

    public bool Equals(BoardDimensions? other)
    {
        if (other is null) return false;
        return Width == other.Width && Height == other.Height;
    }

    public override bool Equals(object? obj) =>
        Equals(obj as BoardDimensions);
    
    public override int GetHashCode() =>
        HashCode.Combine(Width, Height);
}
