using Microsoft.EntityFrameworkCore;
using ProjectManager.WebAPI.Data;
using ProjectManager.WebAPI.Models;
using ProjectManager.WebAPI.Repositories.Interfaces;

namespace ProjectManager.WebAPI.Repositories;

public class ProjectsUsersRepository : IProjectsUsersRepository
{
    private readonly ProjectManagerDbContext _context;

    public ProjectsUsersRepository(ProjectManagerDbContext context)
    {
        _context = context;
    }

    public async Task<List<ProjectsUsersView?>> GetProjectsUsersViewAsync()
    {
        return await _context.ProjectsUsersViews.ToListAsync();
    }
}