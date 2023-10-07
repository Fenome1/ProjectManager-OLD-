namespace ProjectManager.WebAPI.Requests.Users;

public class AuthenticateUserRequest
{
    public string Login { get; set; } = null!;
    public string Password { get; set; } = null!;
}