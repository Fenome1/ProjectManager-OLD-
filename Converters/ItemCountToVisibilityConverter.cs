using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ProjectManager.App.Converters;

public class ItemCountToVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is int itemCount) return itemCount == 0 ? Visibility.Visible : Visibility.Collapsed;

        return Visibility.Collapsed;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}