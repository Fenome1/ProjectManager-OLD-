
using ProjectManager.WebAPI.Requests.Users;

namespace ProjectManager.WebAPI.Helpers.Validators;

public static class UserValidator
{
    public static void ValidateLogin(string login)
    {
        if (string.IsNullOrWhiteSpace(login))
            throw new ArgumentException("Логин должен содержать хотя бы 1 символ");
    }

    public static void ValidatePassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password) || password.Length < 6)
            throw new ArgumentException("Пароль должен состоять минимум из 6 символов");
    }

}