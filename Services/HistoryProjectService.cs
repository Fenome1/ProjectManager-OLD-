using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;
using ProjectManager.App.Models;
using ProjectManager.App.Services.Interfaces;
using static ProjectManager.App.Helpers.DataHolder;

namespace ProjectManager.App.Services;

internal class CompletedProjectService : ICompletedProjectService
{
    private readonly HttpClient _httpClient;

    public CompletedProjectService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<CompletedProject>?> GetCompletedProjectsAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/api/completedProjects");

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<CompletedProject>>(responseBody);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }

        return null;
    }
}