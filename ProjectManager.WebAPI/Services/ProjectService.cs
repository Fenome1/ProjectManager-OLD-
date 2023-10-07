using AutoMapper;
using ProjectManager.WebAPI.Helpers.Validators;
using ProjectManager.WebAPI.Models;
using ProjectManager.WebAPI.Repositories.Interfaces;
using ProjectManager.WebAPI.Requests.Projects;
using ProjectManager.WebAPI.Services.Interfaces;

namespace ProjectManager.WebAPI.Services;

public class ProjectService : IProjectService
{
    private readonly IProjectRepository _projectRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    public ProjectService(IProjectRepository projectRepository, IMapper mapper, IUserRepository userRepository)
    {
        _projectRepository = projectRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<Project> GetProjectByIdAsync(int idProject)
    {
        return await GetProjectByIdAsync(idProject, "Проект не найден");
    }

    public async Task<List<Project>> GetProjectsByUserIdAsync(int idUser)
    {
        var projects = await _projectRepository.GetProjectsByUserIdAsync(idUser);

        if (!projects.Any())
        {
            throw new ArgumentNullException(nameof(projects), "Проекты не найдены");
        }

        return projects;
    }

    public async Task<List<Project>> GetProjectsByIdStatusAsync(int idStatus)
    {
        var projects = await _projectRepository.GetProjectsByIdStatusAsync(idStatus);

        if (!projects.Any())
        {
            throw new ArgumentNullException(nameof(projects), "Проекты не найдены");
        }

        return projects;
    }

    public async Task<List<Project>> GetAllProjectsAsync()
    {
        var projects = await _projectRepository.GetProjectsAsync();

        if (!projects.Any())
        {
            throw new ArgumentNullException(nameof(projects), "Проекты не найдены");
        }

        return projects;
    }

    public async Task<Project> CreateProjectAsync(CreateProjectRequest request)
    {
        ProjectValidator.Validate(request);

        var creationProject = _mapper.Map<Project>(request);
        var createdProject = await _projectRepository.CreateProjectAsync(creationProject);

        if (createdProject == null)
        {
            throw new ArgumentException("Ошибка создания проекта");
        }

        return createdProject;
    }

    public async Task<Project> EditProjectAsync(EditProjectRequest request)
    {
        var editingProject = await GetProjectByIdAsync(request.IdProject, "Проект не найден");

        editingProject.Title = request.Title;

        editingProject.Description = request.Description;

        editingProject.DeadlineDate = request.DeadlineDate;

        return await _projectRepository.UpdateProjectAsync(editingProject);
    }

    public async Task<bool> DeleteProjectAsync(int idProject)
    {
        var isDeleted = await _projectRepository.DeleteProjectAsync(idProject);

        if (!isDeleted)
        {
            throw new ArgumentException("Ошибка удаления проекта");
        }

        return isDeleted;
    }

    public async Task<Project> UpdateUserAssignAsync(AssignProjectToUserRequest request)
    {
        var updatingProject = await GetProjectByIdAsync(request.IdProject, "Проект не найден");

        if (!await _userRepository.IsUserExistAsync(request.IdUser))
        {
            throw new ArgumentException("Пользователь не найден");
        }


        updatingProject.IdUser = request.IdUser;
        updatingProject.IdStatus = 2;

        var updatedProject = await _projectRepository.UpdateProjectAsync(updatingProject);

        if (updatedProject is null)
        {
            throw new ArgumentException("Ошибка обновления проекта");
        }

        return updatedProject;
    }

    public async Task<Project> RemoveUserAssignAsync(ResetUserAssignToProject request)
    {
        ProjectValidator.Validate(request);

        var updatingProject = await GetProjectByIdAsync(request.idProject, "Проект не найден");

        updatingProject.IdUser = null;
        updatingProject.IdStatus = request.idStatus;

        var updatedProject = await _projectRepository.UpdateProjectAsync(updatingProject);

        if (updatedProject is null)
        {
            throw new ArgumentException("Ошибка обновления проекта");
        }

        return updatedProject;
    }

    private async Task<Project> GetProjectByIdAsync(int idProject, string errorMessage)
    {
        var project = await _projectRepository.GetProjectByIdAsync(idProject);

        if (project is null)
        {
            throw new ArgumentNullException(errorMessage);
        }

        return project;
    }
}