using System;
using System.Globalization;
using System.Windows.Data;

namespace ProjectManager.App.Converters;

internal class IdStatusToStatusDescription : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var idStatus = (int)value;
        return idStatus switch
        {
            1 => "Проект еще никто не взял в работу",
            2 => "Проект находится в работе",
            3 => "Провект выполнен",
            _ => "Нет данных"
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}