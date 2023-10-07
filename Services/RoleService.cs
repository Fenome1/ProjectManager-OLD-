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

internal class RoleService : IRoleService
{
    private readonly HttpClient _httpClient;

    public RoleService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Role>?> GetRolesAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/api/role/roles");

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Role>>(responseBody);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }

        return null;
    }

    public async Task<bool> UpdateRoleAsync(int idRole, string name)
    {
        var updateRoleData = new
        {
            idRole,
            name
        };

        try
        {
            var response = await _httpClient.PutAsJsonAsync($"{BaseUrl}/api/role/edit", updateRoleData);
            if (response.IsSuccessStatusCode)
                return true;
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }

        return false;
    }
}