using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace DebtManager.Frontend.Converters;

public class IndexToVisibilityConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is int selectedIndex && parameter != null)
        {
            if (int.TryParse(parameter.ToString(), out int targetIndex))
            {
                return selectedIndex == targetIndex;
            }
        }
        return false;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
