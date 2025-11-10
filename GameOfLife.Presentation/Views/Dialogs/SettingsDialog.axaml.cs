using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Platform.Storage;
using GameOfLife.Domain;

namespace GameOfLife.Presentation.Views.Dialogs;

public partial class SettingsDialog : Window
{
    public bool Confirmed { get; private set; }
    public AppSettings Settings { get; private set; }

    public SettingsDialog()
    {
        InitializeComponent();
        Confirmed = false;
        Settings = new AppSettings();
    }

    public SettingsDialog(AppSettings currentSettings) : this()
    {
        Settings = currentSettings;
        LoadSettings();
    }

    private void LoadSettings()
    {
        if (SavePathInput != null)
            SavePathInput.Text = Settings.SavePath;
        
        if (DeadCellColorInput != null)
        {
            DeadCellColorInput.Text = Settings.DeadCellColor;
            UpdateColorButton(DeadCellColorButton, Settings.DeadCellColor);
        }
        
        if (AliveCellColorInput != null)
        {
            AliveCellColorInput.Text = Settings.AliveCellColor;
            UpdateColorButton(AliveCellColorButton, Settings.AliveCellColor);
        }
        
        if (DeadCellCharacterInput != null)
            DeadCellCharacterInput.Text = Settings.DeadCellCharacter;
        if (AliveCellCharacterInput != null)
            AliveCellCharacterInput.Text = Settings.AliveCellCharacter;
        
        // Subscribe to text changed events
        if (DeadCellColorInput != null)
            DeadCellColorInput.TextChanged += (s, e) => UpdateColorButton(DeadCellColorButton, DeadCellColorInput.Text);
        if (AliveCellColorInput != null)
            AliveCellColorInput.TextChanged += (s, e) => UpdateColorButton(AliveCellColorButton, AliveCellColorInput.Text);
    }

    private void UpdateColorButton(Button? button, string? colorHex)
    {
        if (button == null || string.IsNullOrWhiteSpace(colorHex)) return;
        
        try
        {
            button.Background = new SolidColorBrush(Color.Parse(colorHex));
        }
        catch
        {
            // Invalid color, keep previous background
        }
    }

    private async void BrowseSavePath_Click(object? sender, RoutedEventArgs e)
    {
        var topLevel = TopLevel.GetTopLevel(this);
        if (topLevel == null) return;

        var folders = await topLevel.StorageProvider.OpenFolderPickerAsync(new FolderPickerOpenOptions
        {
            Title = "Select Save Directory",
            AllowMultiple = false
        });

        if (folders.Count > 0 && SavePathInput != null)
        {
            SavePathInput.Text = folders[0].Path.LocalPath;
        }
    }

    private void PickDeadCellColor_Click(object? sender, RoutedEventArgs e)
    {
        var currentColor = Color.Parse("#1E1E1E");
        try
        {
            if (!string.IsNullOrWhiteSpace(DeadCellColorInput?.Text))
                currentColor = Color.Parse(DeadCellColorInput.Text);
        }
        catch { }

        var colorDialog = new ColorPickerDialog(currentColor);
        colorDialog.Closed += (s, args) =>
        {
            if (colorDialog.Confirmed && colorDialog.SelectedColor.HasValue && DeadCellColorInput != null)
            {
                DeadCellColorInput.Text = colorDialog.SelectedColor.Value.ToString();
            }
        };
        colorDialog.Show();
    }

    private void PickAliveCellColor_Click(object? sender, RoutedEventArgs e)
    {
        var currentColor = Color.Parse("#4CAF50");
        try
        {
            if (!string.IsNullOrWhiteSpace(AliveCellColorInput?.Text))
                currentColor = Color.Parse(AliveCellColorInput.Text);
        }
        catch { }

        var colorDialog = new ColorPickerDialog(currentColor);
        colorDialog.Closed += (s, args) =>
        {
            if (colorDialog.Confirmed && colorDialog.SelectedColor.HasValue && AliveCellColorInput != null)
            {
                AliveCellColorInput.Text = colorDialog.SelectedColor.Value.ToString();
            }
        };
        colorDialog.Show();
    }

    private void PickDeadCellEmoji_Click(object? sender, RoutedEventArgs e)
    {
        var emojiDialog = new EmojiPickerDialog();
        emojiDialog.Closed += (s, args) =>
        {
            if (emojiDialog.Confirmed && DeadCellCharacterInput != null)
            {
                DeadCellCharacterInput.Text = emojiDialog.SelectedEmoji ?? "";
            }
        };
        emojiDialog.Show();
    }

    private void PickAliveCellEmoji_Click(object? sender, RoutedEventArgs e)
    {
        var emojiDialog = new EmojiPickerDialog();
        emojiDialog.Closed += (s, args) =>
        {
            if (emojiDialog.Confirmed && AliveCellCharacterInput != null)
            {
                AliveCellCharacterInput.Text = emojiDialog.SelectedEmoji ?? "";
            }
        };
        emojiDialog.Show();
    }

    private void Save_Click(object? sender, RoutedEventArgs e)
    {
        Settings = new AppSettings(
            SavePathInput?.Text?.Trim() ?? Settings.SavePath,
            DeadCellColorInput?.Text?.Trim() ?? Settings.DeadCellColor,
            AliveCellColorInput?.Text?.Trim() ?? Settings.AliveCellColor,
            DeadCellCharacterInput?.Text?.Trim() ?? Settings.DeadCellCharacter,
            AliveCellCharacterInput?.Text?.Trim() ?? Settings.AliveCellCharacter
        );

        Confirmed = true;
        Close();
    }

    private void Cancel_Click(object? sender, RoutedEventArgs e)
    {
        Confirmed = false;
        Close();
    }
}
