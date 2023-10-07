using System.Windows;
using System.Windows.Controls;
using ProjectManager.App.Helpers;
using ProjectManager.App.Models;
using ProjectManager.App.Services.Interfaces;
using static ProjectManager.App.Helpers.DataHolder;

namespace ProjectManager.App.Pages;

public partial class RegisterPage : Page
{
    private readonly IUserService _userService;

    public RegisterPage()
    {
        _userService = AppContainer.Resolve<IUserService>();
        InitializeComponent();
    }

    private void ReturnButton_OnClick(object sender, RoutedEventArgs e)
    {
        NavigationService.GoBack();
    }

    private void RolesComboBox_Loaded(object sender, RoutedEventArgs e)
    {
        RolesComboBox.ItemsSource = Roles;
        RolesComboBox.DisplayMemberPath = "Name";
    }

    private async void SubmitRegisterButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrEmpty(LoginTextBox.Text) ||
            string.IsNullOrEmpty(TryPasswordBox.Password) ||
            string.IsNullOrEmpty(ConfirmPasswordBox.Password))
        {
            MessageBox.Show("Не все поля заполнены");
            return;
        }

        if (RolesComboBox.SelectedItem == null)
        {
            MessageBox.Show("Пожалуйста выберите роль");
            return;
        }

        if (TryPasswordBox.Password != ConfirmPasswordBox.Password)
        {
            MessageBox.Show("Неверное подтверждение пароля");
            return;
        }

        DoRegisterButton.IsEnabled = false;

        var idRole = ((Role)RolesComboBox.SelectedItem).IdRole;

        if (await _userService.RegisterUserAsync(LoginTextBox.Text, ConfirmPasswordBox.Password, idRole))
        {
            NavigationService.Navigate(new AuthPage());
            MessageBox.Show("Вы успешно зарегистрированы");
        }

        DoRegisterButton.IsEnabled = true;
    }
}