namespace ProjectManager.WebAPI.Requests.Users;

public class EditUserRequest
{
    public int IdUser { get; set; }

    public int? IdRole { get; set; }

    public string? Login { get; set; }

    public string? Password { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }
}