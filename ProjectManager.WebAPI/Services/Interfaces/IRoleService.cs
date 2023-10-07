using ProjectManager.WebAPI.Models;
using ProjectManager.WebAPI.Requests.Roles;

namespace ProjectManager.WebAPI.Services.Interfaces;

public interface IRoleService
{
    Task<List<Role>> GetAllRolesAsync();
    Task<Role> GetRoleByIdAsync(int idRole);
    Task<Role> EditRoleAsync(EditRoleRequest request);
}