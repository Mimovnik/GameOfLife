using Avalonia.Controls;
using GameOfLife.Presentation.ViewModels;

namespace GameOfLife.Presentation;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new GameViewModel();
    }
}
