using ProjectManager.WebAPI.Models;
using ProjectManager.WebAPI.Repositories.Interfaces;
using ProjectManager.WebAPI.Services.Interfaces;

namespace ProjectManager.WebAPI.Services
{
    public class CompletedProjectService : ICompletedProjectService
    {
        private readonly ICompletedProjectRepository _completedProjectRepository;

        public CompletedProjectService(ICompletedProjectRepository completedProjectRepository)
        {
            _completedProjectRepository = completedProjectRepository;
        }

        public async Task<List<CompletedProject>> GetCompletedProjectsAsync()
        {
            var projects = await _completedProjectRepository.GetCompletedProjectsAsync();

            if (!projects.Any())
            {
                throw new ArgumentNullException("Выполненные проекты не найдены");
            }

            return projects;
        }

    }
}
