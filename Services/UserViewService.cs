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

internal class UserViewService : IUserViewService
{
    private readonly HttpClient _httpClient;

    public UserViewService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<UserView>?> GetUsersViewAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/api/userView/users/view");
            var responseBody = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode) return JsonConvert.DeserializeObject<List<UserView>>(responseBody);

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