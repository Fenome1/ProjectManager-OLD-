using ProjectManager.WebAPI.Models;
using ProjectManager.WebAPI.Repositories.Interfaces;
using ProjectManager.WebAPI.Requests.Statuses;
using ProjectManager.WebAPI.Services.Interfaces;

namespace ProjectManager.WebAPI.Services;

public class StatusService : IStatusService
{
    private readonly IStatusRepository _statusRepository;

    public StatusService(IStatusRepository statusRepository)
    {
        _statusRepository = statusRepository;
    }

    public async Task<List<Status>> GetAllStatusesAsync()
    {
        var statuses = await _statusRepository.GetStatusesAsync();

        if (!statuses.Any())
        {
            throw new ArgumentException("Статусы не найдены");
        }

        return statuses;
    }

    public async Task<Status> GetStatusByIdAsync(int idStatus)
    {
        var status = await _statusRepository.GetStatusById(idStatus);

        if (status is null)
        {
            throw new ArgumentException("Статус не найден");
        }

        return status;
    }

    public async Task<Status> EditStatusAsync(EditStatusRequest request)
    {
        var updatingStatus = await GetStatusByIdAsync(request.IdStatus);

        if(!string.IsNullOrWhiteSpace(updatingStatus.Title))
            updatingStatus.Title = request.Title;

        var updatedStatus = await _statusRepository.EditStatusAsync(updatingStatus);

        if (updatedStatus is null)
        {
            throw new ArgumentException("Ошибка обновления статуса");
        }

        return updatedStatus;
    }
}