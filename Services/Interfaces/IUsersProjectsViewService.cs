using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectManager.App.Models;

namespace ProjectManager.App.Services.Interfaces;

public interface IUsersProjectsViewService
{
    Task<List<UsersProjectsView>?> GetUsersProjectsViewAsync();
}