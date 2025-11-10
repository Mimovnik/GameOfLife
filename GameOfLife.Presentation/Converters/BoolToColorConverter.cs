using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media;

namespace GameOfLife.Presentation.Converters;

public class BoolToColorConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool isAlive)
        {
            return isAlive ? Brushes.Black : Brushes.White;
        }
        return Brushes.White;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is ISolidColorBrush brush)
        {
            return brush.Color == Colors.Black;
        }
        return false;
    }
}
