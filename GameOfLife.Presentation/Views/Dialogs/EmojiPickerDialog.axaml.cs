using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;

namespace GameOfLife.Presentation.Views.Dialogs;

public partial class EmojiPickerDialog : Window
{
    public string? SelectedEmoji { get; private set; }
    public bool Confirmed { get; private set; }

    private static readonly string[] CommonEmojis = new[]
    {
        "😀", "😃", "😄", "😁", "😆", "😅", "🤣", "😂", "🙂", "🙃",
        "😉", "😊", "😇", "🥰", "😍", "🤩", "😘", "😗", "😚", "😙",
        "🥲", "😋", "😛", "😜", "🤪", "😝", "🤑", "🤗", "🤭", "🤫",
        "🤔", "🤐", "🤨", "😐", "😑", "😶", "😏", "😒", "🙄", "😬",
        "🤥", "😌", "😔", "😪", "🤤", "😴", "😷", "🤒", "🤕", "🤢",
        "🤮", "🤧", "🥵", "🥶", "😵", "🤯", "🤠", "🥳", "😎", "🤓",
        "🧐", "😕", "😟", "🙁", "☹️", "😮", "😯", "😲", "😳", "🥺",
        "😦", "😧", "😨", "😰", "😥", "😢", "😭", "😱", "😖", "😣",
        "💀", "☠️", "👻", "👽", "👾", "🤖", "💩", "😺", "😸", "😹",
        "❤️", "🧡", "💛", "💚", "💙", "💜", "🖤", "🤍", "🤎", "💔",
        "⭐", "🌟", "✨", "💫", "⚡", "🔥", "💥", "💯", "✔️", "❌",
        "⚠️", "🚫", "🔴", "🟠", "🟡", "🟢", "🔵", "🟣", "⚫", "⚪",
        "🟤", "🔶", "🔷", "🔸", "🔹", "▪️", "▫️", "◼️", "◻️", "◾",
        "◽", "●", "○", "■", "□", "▲", "△", "▼", "▽", "★"
    };

    public EmojiPickerDialog()
    {
        InitializeComponent();
        Confirmed = false;
        PopulateEmojis();
    }

    private void PopulateEmojis()
    {
        if (EmojiPanel == null) return;

        foreach (var emoji in CommonEmojis)
        {
            var button = new Button
            {
                Content = emoji,
                FontSize = 24,
                Width = 50,
                Height = 50,
                Margin = new Avalonia.Thickness(5),
                Background = new SolidColorBrush(Color.Parse("#3C3C3C")),
                Tag = emoji
            };

            button.Click += EmojiButton_Click;
            EmojiPanel.Children.Add(button);
        }
    }

    private void EmojiButton_Click(object? sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.Tag is string emoji)
        {
            SelectedEmoji = emoji;
            Confirmed = true;
            Close();
        }
    }

    private void Clear_Click(object? sender, RoutedEventArgs e)
    {
        SelectedEmoji = string.Empty;
        Confirmed = true;
        Close();
    }

    private void Cancel_Click(object? sender, RoutedEventArgs e)
    {
        Confirmed = false;
        Close();
    }
}
