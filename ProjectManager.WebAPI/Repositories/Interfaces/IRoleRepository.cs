using ProjectManager.WebAPI.Models;

namespace ProjectManager.WebAPI.Repositories.Interfaces;

public interface IRoleRepository
{
    Task<List<Role?>> GetRolesAsync();
    Task<Role?> GetRoleById(int idRole);
    Task<Role?> EditRoleAsync(Role role);
}