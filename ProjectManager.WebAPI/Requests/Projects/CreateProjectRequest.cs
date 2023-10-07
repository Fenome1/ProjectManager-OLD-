namespace ProjectManager.WebAPI.Requests.Projects;

public class CreateProjectRequest
{
    public string Title { get; set; } = null!;
    public int? Status { get; set; }
    public string? Description { get; set; }
    public DateTime? DeadlineDate { get; set; }
    public int? IdUser { get; set; }
}