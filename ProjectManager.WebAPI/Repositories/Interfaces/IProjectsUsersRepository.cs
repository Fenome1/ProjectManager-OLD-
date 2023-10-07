using ProjectManager.WebAPI.Models;

namespace ProjectManager.WebAPI.Repositories.Interfaces;

public interface IProjectsUsersRepository
{
    Task<List<ProjectsUsersView?>> GetProjectsUsersViewAsync();
}