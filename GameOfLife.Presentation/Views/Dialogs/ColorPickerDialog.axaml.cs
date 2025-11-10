using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;

namespace GameOfLife.Presentation.Views.Dialogs;

public partial class ColorPickerDialog : Window
{
    public Color? SelectedColor { get; private set; }
    public bool Confirmed { get; private set; }
    private bool _isUpdating = false;

    public ColorPickerDialog()
    {
        InitializeComponent();
        Confirmed = false;
    }

    public ColorPickerDialog(Color initialColor) : this()
    {
        _isUpdating = true;
        if (RedSlider != null)
            RedSlider.Value = initialColor.R;
        if (GreenSlider != null)
            GreenSlider.Value = initialColor.G;
        if (BlueSlider != null)
            BlueSlider.Value = initialColor.B;
        if (HexInput != null)
            HexInput.Text = initialColor.ToString();
        _isUpdating = false;
        UpdatePreview();
    }

    private void ColorSlider_ValueChanged(object? sender, Avalonia.Controls.Primitives.RangeBaseValueChangedEventArgs e)
    {
        if (_isUpdating) return;
        
        _isUpdating = true;
        UpdatePreview();
        UpdateHexInput();
        _isUpdating = false;
    }

    private void HexInput_TextChanged(object? sender, Avalonia.Controls.TextChangedEventArgs e)
    {
        if (_isUpdating) return;
        
        _isUpdating = true;
        try
        {
            if (!string.IsNullOrWhiteSpace(HexInput?.Text))
            {
                var color = Color.Parse(HexInput.Text);
                if (RedSlider != null) RedSlider.Value = color.R;
                if (GreenSlider != null) GreenSlider.Value = color.G;
                if (BlueSlider != null) BlueSlider.Value = color.B;
                UpdatePreview();
            }
        }
        catch
        {
            // Invalid color, ignore
        }
        _isUpdating = false;
    }

    private void UpdatePreview()
    {
        if (ColorPreview == null || RedSlider == null || GreenSlider == null || BlueSlider == null)
            return;

        var color = Color.FromRgb(
            (byte)RedSlider.Value,
            (byte)GreenSlider.Value,
            (byte)BlueSlider.Value
        );
        
        ColorPreview.Background = new SolidColorBrush(color);
    }

    private void UpdateHexInput()
    {
        if (HexInput == null || RedSlider == null || GreenSlider == null || BlueSlider == null)
            return;

        var color = Color.FromRgb(
            (byte)RedSlider.Value,
            (byte)GreenSlider.Value,
            (byte)BlueSlider.Value
        );
        
        HexInput.Text = color.ToString();
    }

    private void OK_Click(object? sender, RoutedEventArgs e)
    {
        if (RedSlider != null && GreenSlider != null && BlueSlider != null)
        {
            SelectedColor = Color.FromRgb(
                (byte)RedSlider.Value,
                (byte)GreenSlider.Value,
                (byte)BlueSlider.Value
            );
            Confirmed = true;
        }
        Close();
    }

    private void Cancel_Click(object? sender, RoutedEventArgs e)
    {
        Confirmed = false;
        Close();
    }
}
