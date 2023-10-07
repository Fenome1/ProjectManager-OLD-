using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectManager.App.Models;

namespace ProjectManager.App.Services.Interfaces;

internal interface ICompletedProjectService
{
    Task<List<CompletedProject>?> GetCompletedProjectsAsync();
}