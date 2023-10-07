using Microsoft.EntityFrameworkCore;
using ProjectManager.WebAPI.Data;
using ProjectManager.WebAPI.Models;
using ProjectManager.WebAPI.Repositories.Interfaces;

namespace ProjectManager.WebAPI.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly ProjectManagerDbContext _context;

    public RoleRepository(ProjectManagerDbContext context)
    {
        _context = context;
    }

    public async Task<List<Role?>> GetRolesAsync()
    {
        return await _context.Roles.ToListAsync();
    }

    public async Task<Role?> GetRoleById(int idRole)
    {
        return await _context.GetRoleById(idRole);
    }

    public async Task<Role?> EditRoleAsync(Role role)
    {
        await _context.UpdateRoleAsync(role.IdRole, role.Name);
        await _context.SaveChangesAsync();
        return role;
    }
}