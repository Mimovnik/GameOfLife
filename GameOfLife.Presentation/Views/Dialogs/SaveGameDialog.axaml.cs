using Avalonia.Controls;
using Avalonia.Interactivity;

namespace GameOfLife.Presentation.Views.Dialogs;

public partial class SaveGameDialog : Window
{
    public string SaveName { get; private set; } = string.Empty;
    public bool Confirmed { get; private set; }

    public SaveGameDialog()
    {
        InitializeComponent();
        Confirmed = false;
    }

    private void Save_Click(object? sender, RoutedEventArgs e)
    {
        var saveName = SaveNameInput?.Text?.Trim() ?? string.Empty;
        
        if (string.IsNullOrWhiteSpace(saveName))
        {
            return;
        }

        SaveName = saveName;
        Confirmed = true;
        Close();
    }

    private void Cancel_Click(object? sender, RoutedEventArgs e)
    {
        Confirmed = false;
        Close();
    }
}
