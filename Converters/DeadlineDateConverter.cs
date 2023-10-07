using System;
using System.Globalization;
using System.Windows.Data;

namespace ProjectManager.App.Converters;

public class DeadlineDateConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is null)
            return "неограниченно";

        var deadlineDate = (DateTime)value;
        return deadlineDate.ToShortDateString();
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}