using System.Windows;
using ProjectManager.App.Helpers;
using ProjectManager.App.Services;

namespace ProjectManager.App;

public partial class App : Application
{
    protected override async void OnStartup(StartupEventArgs e)
    {
        AppContainer.ConfigureContainer();

        await new InitializeService().InitializeAsync();

        base.OnStartup(e);
    }
}