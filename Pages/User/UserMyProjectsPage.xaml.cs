using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ProjectManager.App.Helpers;
using ProjectManager.App.Services.Interfaces;
using static ProjectManager.App.Helpers.DataHolder;

namespace ProjectManager.App.Pages.User;

public partial class UserMyProjectsPage : Page
{
    private const int CompleteStatusId = 3;
    private const int ResetStatusId = 1;
    private readonly ProfilePage _profilePage;
    private readonly IProjectService _projectService;
    private readonly UserProjectsPage _userProjectsPage;

    public UserMyProjectsPage()
    {
        _profilePage = new ProfilePage();
        _userProjectsPage = new UserProjectsPage(_profilePage);

        _projectService = AppContainer.Resolve<IProjectService>();

        InitializeComponent();
    }

    private void ProfileButton_OnClick(object sender, RoutedEventArgs e)
    {
        NavigationService.Navigate(_profilePage);
    }

    private void AllProjects_OnClick(object sender, RoutedEventArgs e)
    {
        NavigationService.Navigate(_userProjectsPage);
    }

    private async void MyProjectsListView_OnLoaded(object sender, RoutedEventArgs e)
    {
        await GetAllUserProjects();
    }

    private async void UpdateProjectStatusButton_OnClick(object sender, RoutedEventArgs e)
    {
        var button = (Button)sender;

        var idProject = (int)button.Tag;

        var idStatus = button.Uid switch
        {
            "reset" => ResetStatusId,
            "complete" => CompleteStatusId,
            _ => throw new NotImplementedException()
        };

        switch (idStatus)
        {
            case CompleteStatusId
                when MessageBoxHelper.QuestionMessageBoxShow("Вы точно хотите завершить проект?", "Завершение проекта"):
            case ResetStatusId
                when MessageBoxHelper.QuestionMessageBoxShow("Вы точно хотите отменить проект?", "Отмена проекта"):
                return;
            default:
                await UpdateProjectStatusAsync(idProject, idStatus);
                break;
        }
    }

    private async Task GetAllUserProjects()
    {
        MyProjectsListView.ItemsSource = await _projectService.GetUserProjectsAsync(CurrentUser.IdUser);
    }

    private async Task UpdateProjectStatusAsync(int idProject, int operationStatus)
    {
        if (await _projectService.UpdateProjectStatusAsync(idProject, operationStatus))
        {
            await GetAllUserProjects();
            return;
        }

        var message = operationStatus == 1 ? "Ошибка отмены проекта" : "Ошибка завершения проекта";
        MessageBox.Show(message);
    }
}