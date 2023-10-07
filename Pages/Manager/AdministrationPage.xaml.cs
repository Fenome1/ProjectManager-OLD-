using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ProjectManager.App.Helpers;
using ProjectManager.App.Models;
using ProjectManager.App.Services.Interfaces;
using static ProjectManager.App.Helpers.DataHolder;

namespace ProjectManager.App.Pages.Manager;

public partial class AdministrationPage : Page
{
    private readonly IRoleService _roleService;
    private readonly IStatusService _statusService;

    public AdministrationPage()
    {
        _roleService = AppContainer.Resolve<IRoleService>();
        _statusService = AppContainer.Resolve<IStatusService>();

        InitializeComponent();
    }

    private void AdministrationPage_OnLoaded(object sender, RoutedEventArgs e)
    {
        GetRoles();
        GetStatuses();
    }

    private void ReturnButton_OnClick(object sender, RoutedEventArgs e)
    {
        NavigationService.GoBack();
    }

    private async void SubmitButton_OnClick(object sender, RoutedEventArgs e)
    {
        var roles = (List<Role>)RolesDataGrid.ItemsSource;
        var statuses = (List<Status>)StatusesDataGrid.ItemsSource;

        if (await UpdateRolesAsync(roles) || await UpdateStatusesAsync(statuses))
        {
            MessageBox.Show("Ошибка обновления данных");
            return;
        }

        Roles = roles;
        Statuses = statuses;

        GetRoles();
        GetStatuses();

        MessageBox.Show("Данные обновлены");
    }

    private async Task<bool> UpdateRolesAsync(List<Role> roles)
    {
        foreach (var role in roles)
        {
            var isUpdated = await _roleService.UpdateRoleAsync(role.IdRole, role.Name);

            if (!isUpdated)
                return true;
        }

        return false;
    }

    private async Task<bool> UpdateStatusesAsync(List<Status> statuses)
    {
        foreach (var status in statuses)
        {
            var isUpdated = await _statusService.UpdateStatusAsync(status.IdStatus, status.Title);

            if (!isUpdated)
                return true;
        }

        return false;
    }

    private void GetRoles()
    {
        RolesDataGrid.ItemsSource = Roles;
    }

    private void GetStatuses()
    {
        StatusesDataGrid.ItemsSource = Statuses;
    }
}