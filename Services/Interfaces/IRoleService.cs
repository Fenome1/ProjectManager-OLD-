using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectManager.App.Models;

namespace ProjectManager.App.Services.Interfaces;

internal interface IRoleService
{
    Task<List<Role>?> GetRolesAsync();
    Task<bool> UpdateRoleAsync(int idRole, string name);
}