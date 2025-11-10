using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using GameOfLife.Domain;

namespace GameOfLife.Presentation.ViewModels;

public partial class BoardViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<CellViewModel> _cells;

    [ObservableProperty]
    private int _columns;

    [ObservableProperty]
    private int _rows;

    public BoardViewModel(Board board)
    {
        _columns = board.Dimensions.Width;
        _rows = board.Dimensions.Height;
        
        _cells = new ObservableCollection<CellViewModel>();
        
        PopulateCells(board);
    }

    private void PopulateCells(Board board)
    {
        Cells.Clear();
        
        for (int y = 0; y < board.Dimensions.Height; y++)
        {
            for (int x = 0; x < board.Dimensions.Width; x++)
            {
                var cell = board.GetCellAt(new Coords(x, y));
                Cells.Add(new CellViewModel(cell));
            }
        }
    }

    public void UpdateFromBoard(Board board)
    {
        for (int y = 0; y < board.Dimensions.Height; y++)
        {
            for (int x = 0; x < board.Dimensions.Width; x++)
            {
                var index = y * board.Dimensions.Width + x;
                var cell = board.GetCellAt(new Coords(x, y));
                Cells[index].UpdateFromCell(cell);
            }
        }
    }
}
