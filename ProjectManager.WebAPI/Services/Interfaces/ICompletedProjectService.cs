using ProjectManager.WebAPI.Models;

namespace ProjectManager.WebAPI.Services.Interfaces
{
    public interface ICompletedProjectService
    {
        Task<List<CompletedProject>> GetCompletedProjectsAsync();
    }
}
