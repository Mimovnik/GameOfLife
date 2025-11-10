using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using GameOfLife.Domain;

namespace GameOfLife.Presentation.ViewModels;

public partial class BoardViewModel : ObservableObject
{
    private readonly Board _board;

    [ObservableProperty]
    private ObservableCollection<CellViewModel> _cells;

    [ObservableProperty]
    private int _columns;

    [ObservableProperty]
    private int _rows;

    public BoardViewModel()
    {
        var dimensions = BoardDimensions.Create(100, 100);
        _board = new Board(dimensions);
        
        _columns = dimensions.Width;
        _rows = dimensions.Height;
        
        _cells = new ObservableCollection<CellViewModel>();
        
        // Populate cells in row-major order for proper grid display
        for (int y = 0; y < dimensions.Height; y++)
        {
            for (int x = 0; x < dimensions.Width; x++)
            {
                var cell = _board.GetCellAt(new Coords(x, y));
                _cells.Add(new CellViewModel(cell));
            }
        }
    }
}
