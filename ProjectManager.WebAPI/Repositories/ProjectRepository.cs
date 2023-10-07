using Microsoft.EntityFrameworkCore;
using ProjectManager.WebAPI.Data;
using ProjectManager.WebAPI.Models;
using ProjectManager.WebAPI.Repositories.Interfaces;

namespace ProjectManager.WebAPI.Repositories;

public class ProjectRepository : IProjectRepository
{
    private readonly ProjectManagerDbContext _context;

    public ProjectRepository(ProjectManagerDbContext context)
    {
        _context = context;
    }

    public async Task<Project?> GetProjectByIdAsync(int idProject)
    {
        return await _context.Projects.FirstOrDefaultAsync(p => p.IdProject == idProject);
    }

    public async Task<List<Project?>> GetProjectsAsync()
    {
        return await _context.Projects.ToListAsync();
    }
    public async Task<List<Project?>> GetProjectsByUserIdAsync(int idUser)
    {
        return await _context.Projects.Where(p => p.IdUser == idUser).ToListAsync();
    }

    public async Task<List<Project?>> GetProjectsByIdStatusAsync(int idStatus)
    {
        return await _context.GetProjectsByStatus(idStatus);
    }

    public async Task<Project?> CreateProjectAsync(Project project)
    {
        await _context.Projects.AddAsync(project);
        await _context.SaveChangesAsync();
        return project;
    }

    public async Task<Project?> UpdateProjectAsync(Project project)
    {
        _context.Projects.Update(project);
        await _context.SaveChangesAsync();
        return project;
    }

    public async Task<bool> DeleteProjectAsync(int idProject)
    {
        var project = await GetProjectByIdAsync(idProject);

        if (project is null)
            throw new ArgumentNullException("Проект не найден");

        _context.Projects.Remove(project);
        return await _context.SaveChangesAsync() > 0;
    }

}
