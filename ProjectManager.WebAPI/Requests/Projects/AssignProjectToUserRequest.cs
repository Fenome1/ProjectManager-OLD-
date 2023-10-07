namespace ProjectManager.WebAPI.Requests.Projects;

public class AssignProjectToUserRequest
{
    public int IdProject { get; set; }
    public int IdUser { get; set; }
}