using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ProjectManager.App.Converters;

internal class IdStatusToVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var idStatus = (int)value;
        return idStatus == 1 ? Visibility.Visible : Visibility.Collapsed;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}