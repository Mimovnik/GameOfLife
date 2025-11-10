using Avalonia.Controls;
using Avalonia.Input;
using GameOfLife.Presentation.ViewModels;

namespace GameOfLife.Presentation.Views;

public partial class GameBoardView : UserControl
{
    public GameBoardView()
    {
        InitializeComponent();
    }

    private void Cell_PointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (sender is Border border && border.DataContext is CellViewModel cellViewModel)
        {
            var properties = e.GetCurrentPoint(border).Properties;

            if (properties.IsLeftButtonPressed)
            {
                cellViewModel.SetAlive();
            }
            else if (properties.IsRightButtonPressed)
            {
                cellViewModel.SetDead();
            }
        }
    }

    private void Grid_PointerMoved(object? sender, PointerEventArgs e)
    {
        var properties = e.GetCurrentPoint(this).Properties;
        
        if (properties.IsLeftButtonPressed || properties.IsRightButtonPressed)
        {
            var position = e.GetPosition(sender as Control);
            var element = (sender as Control)?.InputHitTest(position);
            
            if (element is Border border && border.DataContext is CellViewModel cellViewModel)
            {
                if (properties.IsLeftButtonPressed)
                {
                    cellViewModel.SetAlive();
                }
                else if (properties.IsRightButtonPressed)
                {
                    cellViewModel.SetDead();
                }
            }
        }
    }
}
