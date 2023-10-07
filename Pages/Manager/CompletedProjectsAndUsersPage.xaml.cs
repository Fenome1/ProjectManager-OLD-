using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ProjectManager.App.Helpers;
using ProjectManager.App.Services.Interfaces;

namespace ProjectManager.App.Pages.Manager;

public partial class CompletedProjectsAndUsersPage : Page
{
    private readonly AdministrationPage _administrationPage;
    private readonly ICompletedProjectService _completedProjectService;
    private readonly ProfilePage _profilePage;
    private readonly IUsersProjectsViewService _usersProjectsViewService;

    private readonly IUserViewService _userViewService;

    public CompletedProjectsAndUsersPage(ProfilePage profilePage, AdministrationPage administrationPage)
    {
        _profilePage = profilePage;
        _administrationPage = administrationPage;

        _userViewService = AppContainer.Resolve<IUserViewService>();
        _completedProjectService = AppContainer.Resolve<ICompletedProjectService>();
        _usersProjectsViewService = AppContainer.Resolve<IUsersProjectsViewService>();

        InitializeComponent();
    }

    private async void CompletedProjectsAndUsersPage_OnLoaded(object sender, RoutedEventArgs e)
    {
        await GetUsersViewAsync();
        await GetCompletedProjectsAsync();
        await GetUsersProjectsAsync();

        CurrentGridView();
    }

    private void ManagerProjectsButton_OnClick(object sender, RoutedEventArgs e)
    {
        NavigationService.GoBack();
    }

    private void ProfileButton_OnClick(object sender, RoutedEventArgs e)
    {
        NavigationService.Navigate(_profilePage);
    }

    private void AdministrationButton_OnClick(object sender, RoutedEventArgs e)
    {
        NavigationService.Navigate(_administrationPage);
    }

    private async Task GetCompletedProjectsAsync()
    {
        CompletedProjectsDataGrid.ItemsSource = await _completedProjectService.GetCompletedProjectsAsync();
    }

    private async Task GetUsersViewAsync()
    {
        UsersDataGrid.ItemsSource = await _userViewService.GetUsersViewAsync();
    }

    private async Task GetUsersProjectsAsync()
    {
        UsersProjectsDataGrid.ItemsSource = await _usersProjectsViewService.GetUsersProjectsViewAsync();
    }

    private void GridViewComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        CurrentGridView();
    }

    private void CurrentGridView()
    {
        var selectedItem = (ComboBoxItem)GridViewComboBox.SelectedItem;
        GridViewChange(selectedItem.Content.ToString());
    }

    private void GridViewChange(string gridContext)
    {
        if (UsersDataGrid is null || CompletedProjectsDataGrid is null)
            return;

        switch (gridContext)
        {
            case "Пользователи":
                UsersDataGrid.Visibility = Visibility.Visible;
                CompletedProjectsDataGrid.Visibility = Visibility.Collapsed;
                UsersProjectsDataGrid.Visibility = Visibility.Collapsed;
                break;
            case "Законченные проекты":
                CompletedProjectsDataGrid.Visibility = Visibility.Visible;
                UsersDataGrid.Visibility = Visibility.Collapsed;
                UsersProjectsDataGrid.Visibility = Visibility.Collapsed;
                break;
            case "Проекты пользователей":
                UsersProjectsDataGrid.Visibility = Visibility.Visible;
                UsersDataGrid.Visibility = Visibility.Collapsed;
                CompletedProjectsDataGrid.Visibility = Visibility.Collapsed;
                break;
        }
    }
}