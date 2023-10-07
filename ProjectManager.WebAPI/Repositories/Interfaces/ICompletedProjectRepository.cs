using ProjectManager.WebAPI.Models;

namespace ProjectManager.WebAPI.Repositories.Interfaces
{
    public interface ICompletedProjectRepository
    {
        Task<List<CompletedProject?>> GetCompletedProjectsAsync();
    }
}
