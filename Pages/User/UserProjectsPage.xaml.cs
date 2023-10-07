using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ProjectManager.App.Helpers;
using ProjectManager.App.Services.Interfaces;
using static ProjectManager.App.Helpers.DataHolder;

namespace ProjectManager.App.Pages.User;

public partial class UserProjectsPage : Page
{
    private const int NotStartedProjectStatusId = 1;

    private readonly ProfilePage _profile;
    private readonly IProjectService _projectService;

    public UserProjectsPage(ProfilePage profilePage)
    {
        _profile = profilePage;
        _projectService = AppContainer.Resolve<IProjectService>();

        InitializeComponent();
    }

    private void MyProjectsButton_OnClick(object sender, RoutedEventArgs e)
    {
        NavigationService.GoBack();
    }

    private void ProfileButton_OnClick(object sender, RoutedEventArgs e)
    {
        NavigationService.Navigate(_profile);
    }

    private async void AllProjectsListView_OnLoaded(object sender, RoutedEventArgs e)
    {
        await GetAllProjectsAsync();
    }

    private async void TakeProjectButton_OnClick(object sender, RoutedEventArgs e)
    {
        var button = (Button)sender;

        if (!await _projectService.AssignProjectToUserAsync((int)button.Tag, CurrentUser.IdUser))
            return;

        await GetAllProjectsAsync();
        MessageBox.Show("Внимание! Проект добавлен в 'Мои проекты'");
    }

    private async Task GetAllProjectsAsync()
    {
        AllProjectsListView.ItemsSource = await _projectService.GetProjectsByStatusId(NotStartedProjectStatusId);
    }
}