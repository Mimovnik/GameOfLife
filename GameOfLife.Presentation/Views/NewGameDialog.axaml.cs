using System;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace GameOfLife.Presentation.Views;

public partial class NewGameDialog : Window
{
    public int BoardWidth { get; private set; }
    public int BoardHeight { get; private set; }
    public int[] BirthConditions { get; private set; } = Array.Empty<int>();
    public int[] SurvivalConditions { get; private set; } = Array.Empty<int>();
    public bool Confirmed { get; private set; }

    private readonly ObservableCollection<int> _birthConditions;
    private readonly ObservableCollection<int> _survivalConditions;

    public NewGameDialog()
    {
        InitializeComponent();
        Confirmed = false;

        _birthConditions = new ObservableCollection<int> { 3 };
        _survivalConditions = new ObservableCollection<int> { 2, 3 };

        BirthList.ItemsSource = _birthConditions;
        SurvivalList.ItemsSource = _survivalConditions;
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
}
