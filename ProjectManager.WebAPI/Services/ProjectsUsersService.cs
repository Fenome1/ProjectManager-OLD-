using ProjectManager.WebAPI.Models;
using ProjectManager.WebAPI.Repositories.Interfaces;
using ProjectManager.WebAPI.Services.Interfaces;

namespace ProjectManager.WebAPI.Services
{
    public class ProjectsUsersService : IProjectsUsersService
    {
        private readonly IProjectsUsersRepository _projectsUsersRepository;

        public ProjectsUsersService(IProjectsUsersRepository projectsUsersRepository)
        {
            _projectsUsersRepository = projectsUsersRepository;
        }

        public async Task<List<ProjectsUsersView>> GetProjectsAndUsersAsync()
        {
            return await _projectsUsersRepository.GetProjectsUsersViewAsync();
        }
    }
}
