using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using GameOfLife.Domain;
using GameOfLife.Infra;
using GameOfLife.Presentation.ViewModels;
using GameOfLife.Presentation.Views;

namespace GameOfLife.Presentation;

public partial class MainWindow : Window
{
    private bool _isMenuOpen = false;
    private bool _isSavesListVisible = false;
    private readonly FileGameRepository _repository;

    public MainWindow()
    {
        InitializeComponent();
        DataContext = new GameViewModel();
        _repository = new FileGameRepository();
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

    private async void SaveGame_Click(object? sender, RoutedEventArgs e)
    {
        if (MenuPanel != null && _isMenuOpen)
        {
            _isMenuOpen = false;
            MenuPanel.Classes.Remove("open");
            MenuPanel.Classes.Add("closed");
        }

        var dialog = new SaveGameDialog();
        
        dialog.Closed += async (s, args) =>
        {
            if (dialog.Confirmed && !string.IsNullOrWhiteSpace(dialog.SaveName))
            {
                if (DataContext is GameViewModel viewModel)
                {
                    var save = viewModel.Game.ToSave(dialog.SaveName);
                    await _repository.SaveGameAsync(save);
                }
            }
        };
        
        dialog.Show();
    }

    private async void LoadGame_Click(object? sender, RoutedEventArgs e)
    {
        _isSavesListVisible = !_isSavesListVisible;
        
        if (SavesListPanel != null)
        {
            SavesListPanel.IsVisible = _isSavesListVisible;
        }

        if (_isSavesListVisible)
        {
            await RefreshSavesList();
        }
    }

    private async Task RefreshSavesList()
    {
        if (SavesListContainer == null || NoSavesText == null)
            return;

        SavesListContainer.Children.Clear();

        var saveNames = await _repository.GetSavedGameNamesAsync();
        var savesList = saveNames.ToList();

        if (!savesList.Any())
        {
            SavesListContainer.Children.Add(NoSavesText);
        }
        else
        {
            foreach (var saveName in savesList)
            {
                var button = new Button
                {
                    Content = saveName,
                    Padding = new Avalonia.Thickness(15, 10),
                    FontSize = 13,
                    HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Stretch,
                    HorizontalContentAlignment = Avalonia.Layout.HorizontalAlignment.Left,
                    Background = new SolidColorBrush(Color.Parse("#252525")),
                    Foreground = new SolidColorBrush(Colors.White),
                    Tag = saveName
                };
                
                button.Click += async (s, args) =>
                {
                    if (s is Button btn && btn.Tag is string name)
                    {
                        await LoadSavedGame(name);
                    }
                };

                SavesListContainer.Children.Add(button);
            }
        }
    }

    private async Task LoadSavedGame(string saveName)
    {
        var save = await _repository.LoadGameAsync(saveName);
        
        if (save != null)
        {
            var game = Game.FromSave(save);
            DataContext = new GameViewModel(game);
            
            if (MenuPanel != null && _isMenuOpen)
            {
                _isMenuOpen = false;
                MenuPanel.Classes.Remove("open");
                MenuPanel.Classes.Add("closed");
            }
            
            _isSavesListVisible = false;
            if (SavesListPanel != null)
            {
                SavesListPanel.IsVisible = false;
            }
        }
    }

    private void Exit_Click(object? sender, RoutedEventArgs e)
    {
        Close();
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
