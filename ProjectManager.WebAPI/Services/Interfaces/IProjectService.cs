using ProjectManager.WebAPI.Models;
using ProjectManager.WebAPI.Requests.Projects;

namespace ProjectManager.WebAPI.Services.Interfaces;

public interface IProjectService
{
    Task<List<Project>> GetAllProjectsAsync();
    Task<Project> GetProjectByIdAsync(int idProject);
    Task<List<Project>> GetProjectsByUserIdAsync(int idUser);
    Task<List<Project>> GetProjectsByIdStatusAsync(int idStatus);
    Task<Project> CreateProjectAsync(CreateProjectRequest request);
    Task<Project> EditProjectAsync(EditProjectRequest request);
    Task<bool> DeleteProjectAsync(int idProject);
    Task<Project> UpdateUserAssignAsync(AssignProjectToUserRequest request);
    Task<Project> RemoveUserAssignAsync(ResetUserAssignToProject request);
    
}