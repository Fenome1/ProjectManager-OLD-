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

internal class UsersProjectsViewService : IUsersProjectsViewService
{
    private readonly HttpClient _httpClient;

    public UsersProjectsViewService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<UsersProjectsView>?> GetUsersProjectsViewAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/api/projectsUsers");
            var responseBody = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<List<UsersProjectsView>>(responseBody);

            var errorMessage = JsonConvert.DeserializeObject<ErrorMessage>(responseBody);
            MessageBox.Show(errorMessage?.Message);
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message);
        }

        return null;
    }
}