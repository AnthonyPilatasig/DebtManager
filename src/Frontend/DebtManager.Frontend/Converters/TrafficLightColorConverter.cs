using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media;

namespace DebtManager.Frontend.Converters;

public class TrafficLightColorConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is string status)
        {
            return status.ToLowerInvariant() switch
            {
                "verde" => new SolidColorBrush(Color.Parse("#34C759")),   // Success
                "amarillo" => new SolidColorBrush(Color.Parse("#FFCC00")), // Warning
                "rojo" => new SolidColorBrush(Color.Parse("#FF3B30")),     // Critical
                _ => new SolidColorBrush(Color.Parse("#1C1C1E"))          // Default / CardBrush
            };
        }

        return new SolidColorBrush(Color.Parse("#1C1C1E"));
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
