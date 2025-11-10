using Avalonia.Controls;
using Avalonia.Interactivity;
using GameOfLife.Domain;
using GameOfLife.Presentation.ViewModels;
using GameOfLife.Presentation.Views;

namespace GameOfLife.Presentation;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new GameViewModel();
    }

    private async void NewGame_Click(object? sender, RoutedEventArgs e)
    {
        var dialog = new NewGameDialog();
        await dialog.ShowDialog(this);

        if (dialog.Confirmed)
        {
            var newWindow = CreateNewGameWindow(
                dialog.BoardWidth, 
                dialog.BoardHeight, 
                dialog.BirthConditions, 
                dialog.SurvivalConditions);
            newWindow.Show();
        }
    }

    private static MainWindow CreateNewGameWindow(int width, int height, int[] birthConditions, int[] survivalConditions)
    {
        var dimensions = BoardDimensions.Create(width, height);
        var board = new Board(dimensions);
        var ruleSet = new RuleSet(birthConditions: birthConditions, survivalConditions: survivalConditions);
        var game = new Game(ruleSet, board);
        
        var window = new MainWindow
        {
            DataContext = new GameViewModel(game),
            Title = $"Game of Life - {width}x{height} Grid",
            Width = 1000,
            Height = 1000
        };
        
        return window;
    }
}
