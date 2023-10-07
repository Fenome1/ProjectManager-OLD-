using Microsoft.EntityFrameworkCore;
using ProjectManager.WebAPI.Data;
using ProjectManager.WebAPI.Models;
using ProjectManager.WebAPI.Repositories.Interfaces;
using System.Data;

namespace ProjectManager.WebAPI.Repositories;

public class StatusRepository : IStatusRepository
{
    private readonly ProjectManagerDbContext _context;

    public StatusRepository(ProjectManagerDbContext context)
    {
        _context = context;
    }

    public async Task<List<Status?>> GetStatusesAsync()
    {
        return await _context.Statuses.ToListAsync();
    }

    public async Task<Status?> GetStatusById(int idStatus)
    {
        return await _context.Statuses.FindAsync(idStatus);
    }

    public async Task<Status?> EditStatusAsync(Status status)
    {
        await _context.UpdateStatusAsync(status.IdStatus, status.Title);
        await _context.SaveChangesAsync();
        return status;
    }
}