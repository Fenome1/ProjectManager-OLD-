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

internal class StatusService : IStatusService
{
    private readonly HttpClient _httpClient;

    public StatusService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Status>?> GetStatusesAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/api/status/statuses");

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Status>>(responseBody);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }

        return null;
    }

    public async Task<bool> UpdateStatusAsync(int idStatus, string title)
    {
        var updateStatusData = new
        {
            idStatus,
            title
        };

        try
        {
            var response = await _httpClient.PutAsJsonAsync($"{BaseUrl}/api/status/edit", updateStatusData);
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