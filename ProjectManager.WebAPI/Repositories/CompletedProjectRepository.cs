using Microsoft.EntityFrameworkCore;
using ProjectManager.WebAPI.Data;
using ProjectManager.WebAPI.Models;
using ProjectManager.WebAPI.Repositories.Interfaces;

namespace ProjectManager.WebAPI.Repositories
{
    public class CompletedProjectRepository : ICompletedProjectRepository
    {
        private readonly ProjectManagerDbContext _context;
        public CompletedProjectRepository(ProjectManagerDbContext context)
        {
            _context = context;
        }
        public async Task<List<CompletedProject?>> GetCompletedProjectsAsync()
        {
            return await _context.CompletedProjects.ToListAsync();
        }
    }
}
