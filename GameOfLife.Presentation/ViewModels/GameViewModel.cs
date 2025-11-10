using System;
using System.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GameOfLife.Domain;

namespace GameOfLife.Presentation.ViewModels;

public partial class GameViewModel : ObservableObject
{
    private readonly Game _game;
    private Timer? _timer;

    [ObservableProperty]
    private BoardViewModel _boardViewModel;

    [ObservableProperty]
    private bool _isRunning;

    [ObservableProperty]
    private string _startButtonText = "Start";

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

    [RelayCommand]
    private void ToggleStart()
    {
        if (IsRunning)
        {
            Stop();
        }
        else
        {
            Start();
        }
    }

    private void Start()
    {
        IsRunning = true;
        StartButtonText = "Pause";
        _timer = new Timer(_ => NextGeneration(), null, TimeSpan.Zero, TimeSpan.FromMilliseconds(100));
    }

    private void Stop()
    {
        IsRunning = false;
        StartButtonText = "Start";
        _timer?.Dispose();
        _timer = null;
    }
}
