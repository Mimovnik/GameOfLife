using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GameOfLife.Domain;

namespace GameOfLife.Presentation.ViewModels;

public partial class GameViewModel : ObservableObject
{
    private readonly Game _game;

    [ObservableProperty]
    private BoardViewModel _boardViewModel;

    public GameViewModel()
    {
        var dimensions = BoardDimensions.Create(100, 100);
        var board = new Board(dimensions);
        var ruleSet = new RuleSet(birthConditions: new[] { 3 }, survivalConditions: new[] { 2, 3 });
        
        _game = new Game(ruleSet, board);
        _boardViewModel = new BoardViewModel(_game.Board);
    }

    [RelayCommand]
    private void NextGeneration()
    {
        _game.NextGeneration();
        BoardViewModel.UpdateFromBoard(_game.Board);
    }
}
