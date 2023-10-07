using Microsoft.EntityFrameworkCore;
using ProjectManager.WebAPI.Data;
using ProjectManager.WebAPI.Models;
using ProjectManager.WebAPI.Repositories.Interfaces;

namespace ProjectManager.WebAPI.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ProjectManagerDbContext _context;
    public UserRepository(ProjectManagerDbContext context)
    {
        _context = context;
    }
    public async Task<User?> GetUserByIdAsync(int idUser)
    {
        return await _context.Users
            .Include(u => u.Projects)
            .FirstOrDefaultAsync(u => u.IdUser == idUser);
    }

    public async Task<List<User?>> GetUsersAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<User?> GetUserByLoginAsync(string login)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Login == login);
    }

    public async Task<User?> CreateUserAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User?> UpdateUserAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<bool> DeleteUserAsync(int idUser)
    {
        var deletingUser = await _context.Users.FindAsync(idUser);

        if (deletingUser is null)
            throw new Exception("Пользователь не найден");

        _context.Users.Remove(deletingUser);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> IsUserLoginExistAsync(string login)
    {
        return await _context.Users.AnyAsync(u => u.Login == login);
    }

    public async Task<bool> IsUserExistAsync(int idUser)
    {
        return await _context.Users.AnyAsync(u => u.IdUser == idUser);
    }


}