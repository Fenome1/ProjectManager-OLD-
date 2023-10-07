using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;
using ProjectManager.App.Models;
using ProjectManager.App.Services.Interfaces;
using static ProjectManager.App.Helpers.DataHolder;

namespace ProjectManager.App.Services;

public class ProjectService : IProjectService
{
    private readonly HttpClient _httpClient;

    public ProjectService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Project>?> GetAllProjectsAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/api/project/projects");

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Project>>(responseBody);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }

        return null;
    }

    public async Task<List<Project>?> GetProjectsByStatusId(int idStatus)
    {
        try
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/api/project/status/{idStatus}");

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Project>>(responseBody);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }

        return null;
    }

    public async Task<List<Project>?> GetUserProjectsAsync(int idUser)
    {
        try
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/api/project/user/{idUser}");

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Project>>(responseBody);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }

        return null;
    }

    public async Task<bool> CreateProjectAsync(string title, string? description, DateTime? deadlineDate)
    {
        var creationProject = new
        {
            title,
            description,
            deadlineDate
        };

        try
        {
            var response = await _httpClient.PostAsJsonAsync($"{BaseUrl}/api/project/create", creationProject);
            if (response.IsSuccessStatusCode)
                return true;
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }

        return false;
    }


    public async Task<bool> UpdateProjectStatusAsync(int idProject, int operationStatus)
    {
        var updateStatusProject = new
        {
            idProject,
            idStatus = operationStatus
        };

        try
        {
            var response =
                await _httpClient.PutAsJsonAsync($"{BaseUrl}/api/project/update/assign/reset", updateStatusProject);
            if (response.IsSuccessStatusCode)
                return true;
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }

        return false;
    }

    public async Task<bool> AssignProjectToUserAsync(int idProject, int idUser)
    {
        var assignToUserData = new
        {
            idProject,
            idUser
        };

        try
        {
            var response =
                await _httpClient.PutAsJsonAsync($"{BaseUrl}/api/project/update/assign/add", assignToUserData);

            if (response.IsSuccessStatusCode)
                return true;

            var responseBody = await response.Content.ReadAsStringAsync();
            MessageBox.Show(responseBody);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }

        return false;
    }

    public async Task<bool> UpdateProjectAsync(int idProject, string title, string? description, DateTime? deadlineDate)
    {
        var updateProjectData = new
        {
            idProject,
            title,
            description,
            deadlineDate
        };

        try
        {
            if (updateProjectData.deadlineDate is null)
                await _httpClient.PutAsync($"{BaseUrl}/api/project/update/deadlineDate/reset/{idProject}", null);

            var response = await _httpClient.PutAsJsonAsync($"{BaseUrl}/api/project/update", updateProjectData);

            if (response.IsSuccessStatusCode)
                return true;

            var responseBody = await response.Content.ReadAsStringAsync();
            MessageBox.Show(responseBody);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }

        return false;
    }

    public async Task<bool> DeleteProjectAsync(int idProject)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/api/project/delete/{idProject}");

            if (response.IsSuccessStatusCode)
                return true;

            var responseBody = await response.Content.ReadAsStringAsync();
            MessageBox.Show(responseBody);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }

        return false;
    }
}