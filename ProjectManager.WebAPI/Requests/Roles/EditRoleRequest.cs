namespace ProjectManager.WebAPI.Requests.Roles
{
    public class EditRoleRequest
    {
        public int IdRole { get; set; }
        public string Name { get; set; } = null!;
    }
}
