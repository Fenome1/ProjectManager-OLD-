using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using ProjectManager.App.Helpers;

namespace ProjectManager.App.Converters;

internal class GetCurrentStatusByIdConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var idStatus = (int)value;
        return DataHolder.Statuses.FirstOrDefault(s => s.IdStatus == idStatus).Title;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}