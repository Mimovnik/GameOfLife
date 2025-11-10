using System.Collections;

namespace GameOfLife.Domain;

public class Board : IEquatable<Board>
{
    public BoardDimensions Dimensions { get; }
    public BoardStats Stats { get; }

    private Dictionary<Coords, Cell> Cells { get; }

    public Board(BoardDimensions dimensions)
    {
        Dimensions = dimensions;
        Stats = new BoardStats();
        Cells = new Dictionary<Coords, Cell>();
        for (int x = 0; x < dimensions.Width; x++)
        {
            for (int y = 0; y < dimensions.Height; y++)
            {
                var cell = new Cell(isAlive: false, new Coords(x, y));
                Stats.SubscribeToCell(cell);
                Cells[new Coords(x, y)] = cell;
            }
        }
    }

    public Board Clone()
    {
        var newBoard = new Board(Dimensions);
        foreach (var cell in Cells.Values)
        {
            var newCell = newBoard.GetCellAt(cell.Coords);
            newBoard.Stats.UnsubscribeFromCell(newCell);
            newCell.IsAlive = cell.IsAlive;
            newBoard.Stats.SubscribeToCell(newCell);
        }
        return newBoard;
    }

    public Cell GetCellAt(Coords coords)
    {
        if (!isInBounds(coords))
        {
            throw new ArgumentOutOfRangeException(nameof(coords), "Coordinates are out of board bounds.");
        }
        return Cells[coords];
    }

    public IEnumerable<Cell> GetAllCells()
    {
        return Cells.Values;
    }

    public IEnumerable<Cell> GetNeighborsOf(Coords coords, int range = 1)
    {
        var neighbors = new List<Cell>();
        for (int dx = -range; dx <= range; dx++)
        {
            for (int dy = -range; dy <= range; dy++)
            {
                if (dx == 0 && dy == 0) continue;
                var neighborCoords = new Coords(coords.X + dx, coords.Y + dy);
                if (isInBounds(neighborCoords))
                {
                    neighbors.Add(GetCellAt(neighborCoords));
                }
            }
        }
        return neighbors;
    }

    public void Wipe()
    {
        foreach (var cell in Cells.Values)
        {
            cell.IsAlive = false;
        }
    }

    public void Randomize()
    {
        var random = new Random();
        foreach (var cell in Cells.Values)
        {
            cell.IsAlive = random.Next(0, 2) == 1;
        }
    }

    private bool isInBounds(Coords coords)
    {
        return coords.X >= 0 && coords.X < Dimensions.Width &&
               coords.Y >= 0 && coords.Y < Dimensions.Height;
    }

    public bool Equals(Board? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;

        return Dimensions.Equals(other.Dimensions) &&
               Cells.SequenceEqual(other.Cells);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Board);
    }

    public override int GetHashCode()
    {
        int hash = Dimensions.GetHashCode();
        foreach (var cell in Cells.Values)
        {
            hash = HashCode.Combine(hash, cell.GetHashCode());
        }
        return hash;
    }
}