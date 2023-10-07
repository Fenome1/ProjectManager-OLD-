using ProjectManager.WebAPI.Models;
using ProjectManager.WebAPI.Repositories.Interfaces;
using ProjectManager.WebAPI.Services.Interfaces;

namespace ProjectManager.WebAPI.Services;

public class UserViewService : IUserViewService
{
    private readonly IUserViewRepository _userViewRepository;

    public UserViewService(IUserViewRepository userViewRepository)
    {
        _userViewRepository = userViewRepository;
    }

    public async Task<List<UserView>> GetUsersViewAsync()
    {
        var usersView = await _userViewRepository.GetUserViewAsync();

        if (usersView is null || !usersView.Any())
        {
            throw new ArgumentException("Пользователи не найдены");
        }

        return usersView;
    }
}