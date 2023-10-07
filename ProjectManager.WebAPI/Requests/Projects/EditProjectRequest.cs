namespace ProjectManager.WebAPI.Requests.Projects;

public class EditProjectRequest
{
    public int IdProject { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTime? DeadlineDate { get; set; }
}