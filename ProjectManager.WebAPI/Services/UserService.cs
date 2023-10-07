using AutoMapper;
using ProjectManager.WebAPI.Helpers;
using ProjectManager.WebAPI.Helpers.Validators;
using ProjectManager.WebAPI.Models;
using ProjectManager.WebAPI.Repositories.Interfaces;
using ProjectManager.WebAPI.Requests.Users;
using ProjectManager.WebAPI.Services.Interfaces;

namespace ProjectManager.WebAPI.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IProjectRepository _projectRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IMapper mapper, IProjectRepository projectRepository)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _projectRepository = projectRepository;
    }

    public async Task<List<User>> GetAllUsersAsync()
    {
        var users = await _userRepository.GetUsersAsync();

        if (users is null || !users.Any())
        {
            throw new ArgumentNullException("Пользователи не найдены");
        }

        return users;
    }

    public async Task<User> GetUserByIdAsync(int idUser)
    {
        var user = await _userRepository.GetUserByIdAsync(idUser);

        if (user is null)
        {
            throw new ArgumentNullException("Пользователь не найден");
        }

        return user;
    }

    public async Task<User> RegisterUserAsync(RegisterUserRequest request)
    {
        UserValidator.ValidateLogin(request.Login);

        if (await _userRepository.IsUserLoginExistAsync(request.Login))
        {
            throw new ArgumentException("Пользователь с таким логином уже существует");
        }

        UserValidator.ValidatePassword(request.Password);

        var hashedPassword = HashHelper.HashPassword(request.Password);

        var creationUser = _mapper.Map<User>(request);
        creationUser.Password = hashedPassword;

        var createdUser = await _userRepository.CreateUserAsync(creationUser);

        if (createdUser is null)
        {
            throw new ArgumentException("Не удалось зарегистрировать пользователя");
        }

        return createdUser;
    }

    public async Task<User> AuthenticateUserAsync(AuthenticateUserRequest request)
    {
        var authUser = await _userRepository.GetUserByLoginAsync(request.Login);

        if (authUser is null)
        {
            throw new ArgumentException("Пользователь не найден");
        }

        if (!HashHelper.VerifyPassword(request.Password, authUser.Password))
        {
            throw new ArgumentException("Неверный пароль");
        }

        return authUser;
    }

    public async Task<User> EditUserAsync(EditUserRequest request)
    {
        var editingUser = await _userRepository.GetUserByIdAsync(request.IdUser);

        if (editingUser is null)
        {
            throw new ArgumentException("Пользователь не найден");
        }

        if (!string.IsNullOrWhiteSpace(request.Login))
        {
            if (await _userRepository.IsUserLoginExistAsync(request.Login))
            {
                throw new ArgumentException("Пользователь с таким логином уже существует");
            }

            editingUser.Login = request.Login;
        }

        if (!string.IsNullOrWhiteSpace(request.FirstName))
        {
            editingUser.FirstName = request.FirstName;
        }

        if (!string.IsNullOrWhiteSpace(request.LastName))
        {
            editingUser.LastName = request.LastName;
        }

        if (request.IdRole is not null)
        {
            editingUser.IdRole = (int)request.IdRole;
        }

        if (request.Password is not null)
        {
            editingUser.Password = request.Password;
        }

        var updatedUser = await _userRepository.UpdateUserAsync(editingUser);

        if (updatedUser is null)
        {
            throw new ArgumentException("Не удалось отредактировать пользователя");
        }

        return updatedUser;
    }

    public async Task<bool> DeleteUserAsync(int idUser)
    {
        var deletingUser = await _userRepository.GetUserByIdAsync(idUser);

        if (deletingUser is null)
        {
            throw new ArgumentException("Пользователь не найден");
        }

        var isUserDeleted = await _userRepository.DeleteUserAsync(idUser);

        if (!isUserDeleted)
        {
            throw new ArgumentException("Ошибка удаления пользователя");
        }

        var projects = deletingUser.Projects;

        if (projects.Any())
        {
            foreach (var project in projects)
            {
                project.IdStatus = 1;
                if (await _projectRepository.UpdateProjectAsync(project) is null)
                {
                    throw new ArgumentException(nameof(project), "Ошибка обновления проекта");
                }
            }
        }

        return isUserDeleted;
    }

}