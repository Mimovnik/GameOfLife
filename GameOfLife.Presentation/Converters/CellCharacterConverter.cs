using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace GameOfLife.Presentation.Converters;

public class CellCharacterConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool isAlive)
        {
            var settings = AppSettingsService.Current;
            return isAlive ? settings.AliveCellCharacter : settings.DeadCellCharacter;
        }
        return string.Empty;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}
