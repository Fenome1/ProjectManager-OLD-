namespace ProjectManager.WebAPI.Requests.Users
{
    public class RegisterUserRequest
    {
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int IdRole { get; set; }
    }
}
