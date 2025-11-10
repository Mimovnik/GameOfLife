using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GameOfLife.Domain;

namespace GameOfLife.Presentation.ViewModels;

public partial class GameViewModel : ObservableObject
{
    private readonly Game _game;
    private Timer? _timer;
    private readonly ConcurrentQueue<Board> _boardHistory;

    public Game Game => _game;

    [ObservableProperty]
    private BoardViewModel _boardViewModel;

    [ObservableProperty]
    private bool _isRunning;

    [ObservableProperty]
    private string _startButtonText = "Start";

    [ObservableProperty]
    private int _speedMs = 100;

    [ObservableProperty]
    private bool _autoPauseOnStableState = true;

    [ObservableProperty]
    private int _historyDepth = 10;

    [ObservableProperty]
    private int _generation;

    [ObservableProperty]
    private int _birthCount;

    [ObservableProperty]
    private int _deathCount;

    public GameViewModel()
    {
        var dimensions = BoardDimensions.Create(100, 100);
        var board = new Board(dimensions);
        var ruleSet = new RuleSet(birthConditions: new[] { 3 }, survivalConditions: new[] { 2, 3 });
        
        _game = new Game(ruleSet, board);
        _boardViewModel = new BoardViewModel(_game.Board);
        _boardHistory = new ConcurrentQueue<Board>();
    }

    public GameViewModel(Game game)
    {
        _game = game;
        _boardViewModel = new BoardViewModel(_game.Board);
        _boardHistory = new ConcurrentQueue<Board>();
        
        // Initialize stats from the loaded game
        Generation = _game.Stats.GenerationCount;
        BirthCount = _game.Stats.TotalBirthCount;
        DeathCount = _game.Stats.TotalDeathCount;
    }

    partial void OnSpeedMsChanged(int value)
    {
        if (IsRunning)
        {
            Stop();
            Start();
        }
    }

    [RelayCommand]
    private void NextGeneration()
    {
        _boardHistory.Enqueue(_game.Board);
        
        while (_boardHistory.Count > HistoryDepth)
        {
            _boardHistory.TryDequeue(out _);
        }
        
        _game.NextGeneration();
        BoardViewModel.UpdateFromBoard(_game.Board);
        Generation = _game.Stats.GenerationCount;
        BirthCount = _game.Stats.TotalBirthCount;
        DeathCount = _game.Stats.TotalDeathCount;
        
        if (AutoPauseOnStableState)
        {
            CheckForStableState();
        }
    }

    private void CheckForStableState()
    {
        var historySnapshot = _boardHistory.ToArray();
        
        foreach (var previousBoard in historySnapshot)
        {
            if (_game.Board.Equals(previousBoard))
            {
                if (IsRunning)
                {
                    Stop();
                }
                return;
            }
        }
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
        _timer = new Timer(_ => NextGeneration(), null, TimeSpan.Zero, TimeSpan.FromMilliseconds(SpeedMs));
    }

    private void Stop()
    {
        IsRunning = false;
        StartButtonText = "Start";
        _timer?.Dispose();
        _timer = null;
    }
}
