namespace ProjectManager.WebAPI.Requests.Statuses;

public class EditStatusRequest
{
    public int IdStatus { get; set; }
    public string Title { get; set; } = null!;
}