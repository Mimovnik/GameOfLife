namespace GameOfLife.Domain;

public class BoardDimensions
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
}
