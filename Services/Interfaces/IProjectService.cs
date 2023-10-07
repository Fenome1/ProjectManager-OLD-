using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectManager.App.Models;

namespace ProjectManager.App.Services.Interfaces;

public interface IProjectService
{
    Task<List<Project>?> GetAllProjectsAsync();
    Task<List<Project>?> GetProjectsByStatusId(int idStatus);
    Task<List<Project>?> GetUserProjectsAsync(int idUser);
    Task<bool> CreateProjectAsync(string title, string? description, DateTime? deadlineDate);
    Task<bool> UpdateProjectStatusAsync(int idProject, int operationStatus);
    Task<bool> AssignProjectToUserAsync(int idProject, int idUser);
    Task<bool> UpdateProjectAsync(int idProject, string title, string? description, DateTime? deadlineDate);
    Task<bool> DeleteProjectAsync(int idProject);
}