using System.Windows;
using System.Windows.Controls;
using ProjectManager.App.Helpers;
using ProjectManager.App.Services.Interfaces;
using static ProjectManager.App.Helpers.DataHolder;

namespace ProjectManager.App.Pages;

public partial class ProfilePage : Page
{
    private readonly IUserService _userService;

    public ProfilePage()
    {
        _userService = AppContainer.Resolve<IUserService>();
        InitializeComponent();
    }

    private void ProfilePage_OnLoaded(object sender, RoutedEventArgs e)
    {
        PlaceUserInfoToTextBoxes();
    }

    private void ReturnButton_OnClick(object sender, RoutedEventArgs e)
    {
        NavigationService.GoBack();
    }

    private async void SubmitButton_OnClick(object sender, RoutedEventArgs e)
    {
        var updateUserData = new
        {
            CurrentUser.IdUser,
            Login = LoginTextBox.Text,
            FirstName = FirstNameTextBox.Text,
            LastName = LastNameTextBox.Text
        };

        SubmitButton.IsEnabled = false;
        var updatedUser = await _userService.UpdateUserAsync(CurrentUser.IdUser, LoginTextBox.Text,
            FirstNameTextBox.Text, LastNameTextBox.Text);
        SubmitButton.IsEnabled = true;

        if (updatedUser == null)
            return;

        CurrentUser = updatedUser;


        LoginTextBox.Clear();
        FirstNameTextBox.Clear();
        LastNameTextBox.Clear();

        PlaceUserInfoToTextBoxes();

        MessageBox.Show("Изменения сохранены");
    }

    private void PlaceUserInfoToTextBoxes()
    {
        if (CurrentUser.FirstName is not null)
            FirstNameTextBox.Text = CurrentUser.FirstName;

        if (CurrentUser.LastName is not null)
            LastNameTextBox.Text = CurrentUser.LastName;
    }

    private void ExitButton_OnClick(object sender, RoutedEventArgs e)
    {
        NavigationService.Navigate(new AuthPage());
    }

    private async void DeleteProfileButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (MessageBoxHelper.QuestionMessageBoxShow("Вы точно хотите удалить свой аккаунт?", "Удаление"))
            return;

        DeleteProfileButton.IsEnabled = false;
        var isDeleted = await _userService.DeleteUserAsync(CurrentUser.IdUser);
        DeleteProfileButton.IsEnabled = true;

        if (!isDeleted)
        {
            MessageBox.Show("Ошибка удаления профиля");
            return;
        }

        NavigationService.Navigate(new AuthPage());
    }
}