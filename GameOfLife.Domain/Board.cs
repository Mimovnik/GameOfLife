namespace GameOfLife.Domain;

public class Board
{
    public BoardDimensions Dimensions { get; }

    public Cell[,] Cells { get; }

    public Board(BoardDimensions dimensions)
    {
        Dimensions = dimensions;
        Cells = new Cell[dimensions.Width, dimensions.Height];
        for (int x = 0; x < dimensions.Width; x++)
        {
            for (int y = 0; y < dimensions.Height; y++)
            {
                Cells[x, y] = new Cell(isAlive: false);
            }
        }
    }

    public IEnumerable<Cell> GetCellsRowByRow()
    {
        for (int y = 0; y < Dimensions.Height; y++)
        {
            for (int x = 0; x < Dimensions.Width; x++)
            {
                yield return Cells[x, y];
            }
        }
    }

    public Cell ChangeCellStateAt(Coords coords)
    {
        var cell = Cells[coords.X, coords.Y];
        if (cell.IsAlive)
        {
            cell.SetDead();
        }
        else
        {
            cell.SetAlive();
        }

        return cell;
    }
}