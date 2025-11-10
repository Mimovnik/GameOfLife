using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GameOfLife.Domain;

namespace GameOfLife.Presentation.ViewModels;

public partial class CellViewModel : ObservableObject
{
    private Cell _cell;

    [ObservableProperty]
    private bool _isAlive;

    public int X => _cell.Coords.X;
    public int Y => _cell.Coords.Y;

    public CellViewModel(Cell cell)
    {
        _cell = cell;
        _isAlive = cell.IsAlive;
    }

    partial void OnIsAliveChanged(bool value)
    {
        _cell.IsAlive = value;
    }

    public void SetAlive()
    {
        IsAlive = true;
    }

    public void SetDead()
    {
        IsAlive = false;
    }

    public void UpdateFromCell(Cell cell)
    {
        _cell = cell;
        IsAlive = cell.IsAlive;
    }
}
