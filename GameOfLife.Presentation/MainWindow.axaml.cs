using Avalonia.Controls;
using Avalonia.Interactivity;
using GameOfLife.Domain;
using GameOfLife.Presentation.ViewModels;
using GameOfLife.Presentation.Views;

namespace GameOfLife.Presentation;

public partial class MainWindow : Window
{
    private bool _isMenuOpen = false;

    public MainWindow()
    {
        InitializeComponent();
        DataContext = new GameViewModel();
    }

    private void MenuButton_Click(object? sender, RoutedEventArgs e)
    {
        if (MenuPanel != null)
        {
            _isMenuOpen = !_isMenuOpen;
            
            MenuPanel.Classes.Remove("open");
            MenuPanel.Classes.Remove("closed");
            
            if (_isMenuOpen)
            {
                MenuPanel.Classes.Add("open");
            }
            else
            {
                MenuPanel.Classes.Add("closed");
            }
        }
    }

    private void NewGame_Click(object? sender, RoutedEventArgs e)
    {
        if (MenuPanel != null && _isMenuOpen)
        {
            _isMenuOpen = false;
            MenuPanel.Classes.Remove("open");
            MenuPanel.Classes.Add("closed");
        }
        
        var dialog = new NewGameDialog();
        
        dialog.Closed += (s, args) =>
        {
            if (dialog.Confirmed)
            {
                var newWindow = CreateNewGameWindow(
                    dialog.BoardWidth, 
                    dialog.BoardHeight, 
                    dialog.BirthConditions, 
                    dialog.SurvivalConditions);
                newWindow.Show();
            }
        };
        
        dialog.Show();
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
