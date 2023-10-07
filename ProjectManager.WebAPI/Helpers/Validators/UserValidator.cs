
using ProjectManager.WebAPI.Requests.Users;

namespace ProjectManager.WebAPI.Helpers.Validators;

public static class UserValidator
{
    public static void ValidateLogin(string login)
    {
        if (string.IsNullOrWhiteSpace(login))
            throw new ArgumentException("����� ������ ��������� ���� �� 1 ������");
    }

    public static void ValidatePassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password) || password.Length < 6)
            throw new ArgumentException("������ ������ �������� ������� �� 6 ��������");
    }

}