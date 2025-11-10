using System;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using GameOfLife.Presentation.ViewModels;

namespace GameOfLife.Presentation.Views.Components;

public partial class GameBoardView : UserControl
{
    private const double MinZoom = 0.1;
    private const double MaxZoom = 5.0;
    private const double ZoomSpeed = 0.1;
    private double _currentZoom = 1.0;

    public GameBoardView()
    {
        InitializeComponent();
    }

    private void ScrollViewer_PointerWheelChanged(object? sender, PointerWheelEventArgs e)
    {
        if (e.KeyModifiers.HasFlag(KeyModifiers.Control))
        {
            var delta = e.Delta.Y;
            var newScale = _currentZoom + (delta * ZoomSpeed);

            // Clamp the zoom level
            newScale = Math.Max(MinZoom, Math.Min(MaxZoom, newScale));

            _currentZoom = newScale;
            
            // Update the scale transform
            if (ZoomBorder.RenderTransform is ScaleTransform scaleTransform)
            {
                scaleTransform.ScaleX = newScale;
                scaleTransform.ScaleY = newScale;
            }

            e.Handled = true;
        }
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
