using System;
using System.Globalization;
using System.Windows.Data;

namespace ProjectManager.App.Converters;

public class IdRoleToRoleDescription : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var idRole = (int)value;
        return idRole switch
        {
            1 => "Данный пользователь имеет полный доступ к функционалу: добавление, редактирование, удаление данных",
            2 => "У данного пользователя нет полного доступа к функционалу: выбор проектов, их завершение / отмена",
            _ => "Нет данных"
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}