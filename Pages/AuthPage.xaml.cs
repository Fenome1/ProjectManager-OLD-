using System.Windows;
using System.Windows.Controls;
using ProjectManager.App.Helpers;
using ProjectManager.App.Pages.Manager;
using ProjectManager.App.Pages.User;
using ProjectManager.App.Services.Interfaces;
using static ProjectManager.App.Helpers.DataHolder;

namespace ProjectManager.App.Pages;

public partial class AuthPage : Page
{
    private readonly IUserService _userService;

    public AuthPage()
    {
        InitializeComponent();
        _userService = AppContainer.Resolve<IUserService>();
    }

    private void RegisterButton_OnClick(object sender, RoutedEventArgs e)
    {
        NavigationService.Navigate(new RegisterPage());
    }

    private async void AuthButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrEmpty(LoginTextBox.Text) ||
            string.IsNullOrEmpty(PasswordBox.Password)) //валидация полей ввода
        {
            MessageBox.Show("Все поля должны быть заполнены", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        SwitchButtons(); //блокирование кнопок
        CurrentUser = await _userService.AuthUserAsync(LoginTextBox.Text, PasswordBox.Password); //обращение к методу авторизации из сервиса по работе с пользователями
        SwitchButtons(); //разблокирование кнопок

        if (CurrentUser is null) //проверка на результат авторизации авторизации
            return;

        NavigationService.Navigate(CurrentUser.IdRole == 1 ? new ManagerProjectsPage() : new UserMyProjectsPage()); //осуществляется переход к странице в зависимости от роли пользователя
    }

    private void SwitchButtons()
    {
        AuthButton.IsEnabled = !AuthButton.IsEnabled;
        RegisterButton.IsEnabled = !RegisterButton.IsEnabled;
    }
}