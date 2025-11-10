using System;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace GameOfLife.Presentation.Views;

public class RulePreset
{
    public string Name { get; set; } = string.Empty;
    public int[] BirthConditions { get; set; } = Array.Empty<int>();
    public int[] SurvivalConditions { get; set; } = Array.Empty<int>();

    public override string ToString() => Name;
}

public partial class NewGameDialog : Window
{
    public int BoardWidth { get; private set; }
    public int BoardHeight { get; private set; }
    public int[] BirthConditions { get; private set; } = Array.Empty<int>();
    public int[] SurvivalConditions { get; private set; } = Array.Empty<int>();
    public bool Confirmed { get; private set; }

    private readonly ObservableCollection<int> _birthConditions;
    private readonly ObservableCollection<int> _survivalConditions;
    private readonly ObservableCollection<RulePreset> _presets;
    private bool _isUpdatingFromPreset;

    public NewGameDialog()
    {
        _birthConditions = new ObservableCollection<int> { 3 };
        _survivalConditions = new ObservableCollection<int> { 2, 3 };

        _presets = new ObservableCollection<RulePreset>
        {
            new RulePreset { Name = "Conway's Life (B3/S23)", BirthConditions = new[] { 3 }, SurvivalConditions = new[] { 2, 3 } },
            new RulePreset { Name = "HighLife (B36/S23)", BirthConditions = new[] { 3, 6 }, SurvivalConditions = new[] { 2, 3 } },
            new RulePreset { Name = "Day & Night (B3678/S34678)", BirthConditions = new[] { 3, 6, 7, 8 }, SurvivalConditions = new[] { 3, 4, 6, 7, 8 } },
            new RulePreset { Name = "Seeds (B2/S)", BirthConditions = new[] { 2 }, SurvivalConditions = Array.Empty<int>() },
            new RulePreset { Name = "Replicator (B1357/S1357)", BirthConditions = new[] { 1, 3, 5, 7 }, SurvivalConditions = new[] { 1, 3, 5, 7 } },
            new RulePreset { Name = "Life Without Death (B3/S012345678)", BirthConditions = new[] { 3 }, SurvivalConditions = new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 } },
            new RulePreset { Name = "34 Life (B34/S34)", BirthConditions = new[] { 3, 4 }, SurvivalConditions = new[] { 3, 4 } },
            new RulePreset { Name = "Custom Rules", BirthConditions = Array.Empty<int>(), SurvivalConditions = Array.Empty<int>() }
        };

        InitializeComponent();
        Confirmed = false;

        PresetComboBox.ItemsSource = _presets;

        BirthList.ItemsSource = _birthConditions;
        SurvivalList.ItemsSource = _survivalConditions;

        _birthConditions.CollectionChanged += (s, e) => UpdatePresetSelection();
        _survivalConditions.CollectionChanged += (s, e) => UpdatePresetSelection();

        _isUpdatingFromPreset = true;
        PresetComboBox.SelectedIndex = 0;
        _isUpdatingFromPreset = false;
    }

    private void AddBirth_Click(object? sender, RoutedEventArgs e)
    {
        int value = (int)(BirthInput.Value ?? 0);
        
        if (!_birthConditions.Contains(value))
        {
            _birthConditions.Add(value);
            var sorted = _birthConditions.OrderBy(x => x).ToList();
            _birthConditions.Clear();
            foreach (var item in sorted)
            {
                _birthConditions.Add(item);
            }
        }
    }

    private void RemoveBirth_Click(object? sender, RoutedEventArgs e)
    {
        if (BirthList.SelectedItem is int selected)
        {
            _birthConditions.Remove(selected);
        }
    }

    private void AddSurvival_Click(object? sender, RoutedEventArgs e)
    {
        int value = (int)(SurvivalInput.Value ?? 0);
        
        if (!_survivalConditions.Contains(value))
        {
            _survivalConditions.Add(value);
            var sorted = _survivalConditions.OrderBy(x => x).ToList();
            _survivalConditions.Clear();
            foreach (var item in sorted)
            {
                _survivalConditions.Add(item);
            }
        }
    }

    private void RemoveSurvival_Click(object? sender, RoutedEventArgs e)
    {
        if (SurvivalList.SelectedItem is int selected)
        {
            _survivalConditions.Remove(selected);
        }
    }

    private void Create_Click(object? sender, RoutedEventArgs e)
    {
        BoardWidth = (int)(WidthInput.Value ?? 100);
        BoardHeight = (int)(HeightInput.Value ?? 100);
        
        BirthConditions = _birthConditions.ToArray();
        SurvivalConditions = _survivalConditions.ToArray();
        
        Confirmed = true;
        Close();
    }

    private void Cancel_Click(object? sender, RoutedEventArgs e)
    {
        Confirmed = false;
        Close();
    }

    private void PresetComboBox_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (_birthConditions == null || _survivalConditions == null)
            return;

        if (PresetComboBox.SelectedItem is not RulePreset preset || _isUpdatingFromPreset)
            return;

        if (preset.Name == "Custom Rules")
            return;

        _isUpdatingFromPreset = true;

        _birthConditions.Clear();
        foreach (var condition in preset.BirthConditions.OrderBy(x => x))
        {
            _birthConditions.Add(condition);
        }

        _survivalConditions.Clear();
        foreach (var condition in preset.SurvivalConditions.OrderBy(x => x))
        {
            _survivalConditions.Add(condition);
        }

        _isUpdatingFromPreset = false;
    }

    private void UpdatePresetSelection()
    {
        if (_isUpdatingFromPreset)
            return;

        var currentBirth = _birthConditions.OrderBy(x => x).ToArray();
        var currentSurvival = _survivalConditions.OrderBy(x => x).ToArray();

        foreach (var preset in _presets)
        {
            if (preset.Name == "Custom Rules")
                continue;

            var presetBirth = preset.BirthConditions.OrderBy(x => x).ToArray();
            var presetSurvival = preset.SurvivalConditions.OrderBy(x => x).ToArray();

            if (currentBirth.SequenceEqual(presetBirth) && currentSurvival.SequenceEqual(presetSurvival))
            {
                _isUpdatingFromPreset = true;
                PresetComboBox.SelectedItem = preset;
                _isUpdatingFromPreset = false;
                return;
            }
        }

        _isUpdatingFromPreset = true;
        PresetComboBox.SelectedItem = _presets.Last();
        _isUpdatingFromPreset = false;
    }
}
