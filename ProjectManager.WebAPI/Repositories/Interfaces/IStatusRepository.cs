using ProjectManager.WebAPI.Models;

namespace ProjectManager.WebAPI.Repositories.Interfaces;

public interface IStatusRepository
{
    Task<List<Status?>> GetStatusesAsync();
    Task<Status?> GetStatusById(int idStatus);
    Task<Status?> EditStatusAsync(Status status);
}