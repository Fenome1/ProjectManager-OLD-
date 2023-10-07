using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectManager.App.Models;

namespace ProjectManager.App.Services.Interfaces;

internal interface IStatusService
{
    Task<List<Status>?> GetStatusesAsync();
    Task<bool> UpdateStatusAsync(int idStatus, string title);
}