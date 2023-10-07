using ProjectManager.WebAPI.Models;

namespace ProjectManager.WebAPI.Services.Interfaces
{
    public interface IUserViewService
    {
        Task<List<UserView>> GetUsersViewAsync();
    }
}
