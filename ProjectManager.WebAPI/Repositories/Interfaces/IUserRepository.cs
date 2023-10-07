using ProjectManager.WebAPI.Models;

namespace ProjectManager.WebAPI.Repositories.Interfaces;

public interface IUserRepository
{
    Task<List<User?>> GetUsersAsync();
    Task<User?> GetUserByIdAsync(int idUser);
    Task<User?> GetUserByLoginAsync(string login);
    Task<User?> CreateUserAsync(User user);
    Task<User?> UpdateUserAsync(User user);
    Task<bool> DeleteUserAsync(int idUser);
    Task<bool> IsUserLoginExistAsync(string login);
    Task<bool> IsUserExistAsync(int idUser);
}