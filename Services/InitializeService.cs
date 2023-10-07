using System.Threading.Tasks;
using System.Windows;
using ProjectManager.App.Helpers;
using ProjectManager.App.Services.Interfaces;
using static ProjectManager.App.Helpers.DataHolder;

namespace ProjectManager.App.Services;

internal class InitializeService
{
    private readonly IRoleService _roleService;
    private readonly IStatusService _statusService;

    public InitializeService()
    {
        _statusService = AppContainer.Resolve<IStatusService>();
        _roleService = AppContainer.Resolve<IRoleService>();
    }

    public async Task InitializeAsync()
    {
        Roles = await _roleService.GetRolesAsync();

        if (Roles == null) MessageBox.Show("Ошибка инициализации ролей");

        Statuses = await _statusService.GetStatusesAsync();

        if (Statuses == null) MessageBox.Show("Ошибка инициализации статусов");
    }
}