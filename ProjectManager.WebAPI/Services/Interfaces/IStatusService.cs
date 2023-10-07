using ProjectManager.WebAPI.Models;
using ProjectManager.WebAPI.Requests.Statuses;

namespace ProjectManager.WebAPI.Services.Interfaces;

public interface IStatusService
{
    Task<List<Status>> GetAllStatusesAsync();
    Task<Status> GetStatusByIdAsync(int idRole);
    Task<Status> EditStatusAsync(EditStatusRequest request);
}