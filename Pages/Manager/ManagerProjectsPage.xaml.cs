using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ProjectManager.App.Helpers;
using ProjectManager.App.Models;
using ProjectManager.App.Pages.Manager.Windows;
using ProjectManager.App.Services.Interfaces;

namespace ProjectManager.App.Pages.Manager;

public partial class ManagerProjectsPage : Page
{
    private readonly AdministrationPage _administrationPage;
    private readonly CompletedProjectsAndUsersPage _completedProjectsAndUsersPage;
    private readonly ProfilePage _profilePage;

    private readonly IProjectService _projectService;

    public ManagerProjectsPage()
    {
        _profilePage = new ProfilePage();
        _administrationPage = new AdministrationPage();
        _completedProjectsAndUsersPage = new CompletedProjectsAndUsersPage(_profilePage, _administrationPage);

        _projectService = AppContainer.Resolve<IProjectService>();
        InitializeComponent();
    }

    private void CompletedProjectsAndUsersButton_OnClick(object sender, RoutedEventArgs e)
    {
        NavigationService.Navigate(_completedProjectsAndUsersPage);
    }

    private void ProfileButton_OnClick(object sender, RoutedEventArgs e)
    {
        NavigationService.Navigate(_profilePage);
    }

    private void AdministrationButton_OnClick(object sender, RoutedEventArgs e)
    {
        NavigationService.Navigate(_administrationPage);
    }

    private async void AllProjectsListView_OnLoaded(object sender, RoutedEventArgs e)
    {
        await GetAllProjectsAsync();
    }

    private async Task GetAllProjectsAsync()
    {
        AllProjectsListView.ItemsSource = await _projectService.GetAllProjectsAsync();
    }

    private async void UpdateProjectButton_OnClick(object sender, RoutedEventArgs e)
    {
        var project = (Project)((Button)sender).DataContext;
        new UpdateProjectWindow(project, _projectService).ShowDialog();
        await GetAllProjectsAsync();
    }

    private async void DeleteProjectButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (MessageBoxHelper.QuestionMessageBoxShow("Вы точно хотите удалить проект?", "Удаление проекта"))
            return;

        var project = (Project)((Button)sender).DataContext;

        var isDeleted = await _projectService.DeleteProjectAsync(project.IdProject);

        if (!isDeleted)
        {
            MessageBox.Show("Ошибка удаления");
            return;
        }

        await GetAllProjectsAsync();
    }

    private async void AddNewProjectButton_OnClick(object sender, RoutedEventArgs e)
    {
        new CreateNewProjectWindow().ShowDialog();
        await GetAllProjectsAsync();
    }
}