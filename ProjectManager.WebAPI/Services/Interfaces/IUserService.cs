using ProjectManager.WebAPI.Models;
using ProjectManager.WebAPI.Requests.Users;

namespace ProjectManager.WebAPI.Services.Interfaces;

public interface IUserService
{
    Task<List<User>> GetAllUsersAsync();
    Task<User> GetUserByIdAsync(int idUser);
    Task<User> RegisterUserAsync(RegisterUserRequest request);
    Task<User> AuthenticateUserAsync(AuthenticateUserRequest request);
    Task<User> EditUserAsync(EditUserRequest request);
    Task<bool> DeleteUserAsync(int idUser);
}