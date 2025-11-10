using System.Collections;

namespace GameOfLife.Domain;

public class Board
{
    public BoardDimensions Dimensions { get; }

    private Dictionary<Coords, Cell> Cells { get; }

    public Board(BoardDimensions dimensions)
    {
        Dimensions = dimensions;
        Cells = new Dictionary<Coords, Cell>();
        for (int x = 0; x < dimensions.Width; x++)
        {
            for (int y = 0; y < dimensions.Height; y++)
            {
                Cells[new Coords(x, y)] = new Cell(isAlive: false, new Coords(x, y));
            }
        }
    }

    public Cell GetCellAt(Coords coords)
    {
        if (!isInBounds(coords))
        {
            throw new ArgumentOutOfRangeException(nameof(coords), "Coordinates are out of board bounds.");
        }
        return Cells[coords];
    }

    public void SetCellStateAt(Coords coords, bool isAlive)
    {
        if (!isInBounds(coords))
        {
            throw new ArgumentOutOfRangeException(nameof(coords), "Coordinates are out of board bounds.");
        }
        var cell = Cells[coords];
        if (isAlive)
        {
            cell.SetAlive();
        }
        else
        {
            cell.SetDead();
        }
    }

    private bool isInBounds(Coords coords)
    {
        return coords.X >= 0 && coords.X < Dimensions.Width &&
               coords.Y >= 0 && coords.Y < Dimensions.Height;
    }
}