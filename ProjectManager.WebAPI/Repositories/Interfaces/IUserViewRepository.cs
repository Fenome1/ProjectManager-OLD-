using ProjectManager.WebAPI.Models;

namespace ProjectManager.WebAPI.Repositories.Interfaces
{
    public interface IUserViewRepository
    {
        Task<List<UserView?>> GetUserViewAsync();
    }
}
