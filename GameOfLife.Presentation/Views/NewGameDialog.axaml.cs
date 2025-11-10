using Avalonia.Controls;
using Avalonia.Interactivity;

namespace GameOfLife.Presentation.Views;

public partial class NewGameDialog : Window
{
    public int BoardWidth { get; private set; }
    public int BoardHeight { get; private set; }
    public bool Confirmed { get; private set; }

    public NewGameDialog()
    {
        InitializeComponent();
        Confirmed = false;
    }

    private void Create_Click(object? sender, RoutedEventArgs e)
    {
        BoardWidth = (int)(WidthInput.Value ?? 100);
        BoardHeight = (int)(HeightInput.Value ?? 100);
        Confirmed = true;
        Close();
    }

    private void Cancel_Click(object? sender, RoutedEventArgs e)
    {
        Confirmed = false;
        Close();
    }
}
