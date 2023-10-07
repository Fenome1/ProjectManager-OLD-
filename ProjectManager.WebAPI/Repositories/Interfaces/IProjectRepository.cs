using ProjectManager.WebAPI.Models;

namespace ProjectManager.WebAPI.Repositories.Interfaces;

public interface IProjectRepository
{
    Task<List<Project?>> GetProjectsAsync();
    Task<Project?> GetProjectByIdAsync(int idProject);
    Task<List<Project?>> GetProjectsByUserIdAsync(int idUser);
    Task<List<Project?>> GetProjectsByIdStatusAsync(int idStatus);
    Task<Project?> CreateProjectAsync(Project project);
    Task<Project?> UpdateProjectAsync(Project project);
    Task<bool> DeleteProjectAsync(int idProject);
}
