using ProjectManager.WebAPI.Models;
using ProjectManager.WebAPI.Repositories.Interfaces;
using ProjectManager.WebAPI.Requests.Roles;
using ProjectManager.WebAPI.Services.Interfaces;

namespace ProjectManager.WebAPI.Services;

public class RoleService : IRoleService
{
    private readonly IRoleRepository _roleRepository;
    public RoleService(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task<List<Role>> GetAllRolesAsync()
    {
        var roles = await _roleRepository.GetRolesAsync();

        if (!roles.Any())
        {
            throw new ArgumentException("Роли не найдены");
        }

        return roles;
    }

    public async Task<Role> GetRoleByIdAsync(int idRole)
    {
        return await GetRoleByIdAsync(idRole, "Роль не найдена");
    }

    public async Task<Role> EditRoleAsync(EditRoleRequest request)
    {
        var updatingRole = await GetRoleByIdAsync(request.IdRole, "Роль не найдена");

        if(!string.IsNullOrWhiteSpace(request.Name))
            updatingRole.Name = request.Name;

        var updatedRole = await _roleRepository.EditRoleAsync(updatingRole);

        if (updatedRole is null)
        {
            throw new ArgumentException("Ошибка обновления роли");
        }

        return updatedRole;
    }

    private async Task<Role> GetRoleByIdAsync(int idRole, string errorMessage)
    {
        var role = await _roleRepository.GetRoleById(idRole);

        if (role is null)
        {
            throw new ArgumentException(errorMessage);
        }

        return role;
    }
}