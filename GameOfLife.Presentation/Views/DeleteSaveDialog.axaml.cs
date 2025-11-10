using Avalonia.Controls;
using Avalonia.Interactivity;

namespace GameOfLife.Presentation.Views;

public partial class DeleteSaveDialog : Window
{
    public bool Confirmed { get; private set; }

    public DeleteSaveDialog()
    {
        InitializeComponent();
        Confirmed = false;
    }

    public DeleteSaveDialog(string saveName) : this()
    {
        if (ConfirmationText != null)
        {
            ConfirmationText.Text = $"Are you sure you want to delete the save '{saveName}'? This action cannot be undone.";
        }
    }

    private void Delete_Click(object? sender, RoutedEventArgs e)
    {
        Confirmed = true;
        Close();
    }

    private void Cancel_Click(object? sender, RoutedEventArgs e)
    {
        Confirmed = false;
        Close();
    }
}
