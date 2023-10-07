using System.Threading.Tasks;
using ProjectManager.App.Models;

namespace ProjectManager.App.Services.Interfaces;

internal interface IUserService
{
    Task<bool> RegisterUserAsync(string login, string password, int idRole);
    Task<User?> AuthUserAsync(string login, string password);
    Task<User?> UpdateUserAsync(int idUser, string login, string firstName, string lastName);
    Task<bool> DeleteUserAsync(int idUser);
}