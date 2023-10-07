using ProjectManager.WebAPI.Models;

namespace ProjectManager.WebAPI.Services.Interfaces
{
    public interface IProjectsUsersService
    {
        Task<List<ProjectsUsersView>> GetProjectsAndUsersAsync();
    }
}
