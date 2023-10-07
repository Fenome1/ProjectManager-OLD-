using Microsoft.EntityFrameworkCore;
using ProjectManager.WebAPI.Data;
using ProjectManager.WebAPI.Models;
using ProjectManager.WebAPI.Repositories.Interfaces;

namespace ProjectManager.WebAPI.Repositories
{
    public class UserViewRepository : IUserViewRepository
    {
        private readonly ProjectManagerDbContext _context;
        public UserViewRepository(ProjectManagerDbContext context)
        {
            _context = context;
        }
        public async Task<List<UserView?>> GetUserViewAsync()
        {
            return await _context.UserViews.ToListAsync();
        }
    }
}
