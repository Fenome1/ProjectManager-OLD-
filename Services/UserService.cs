using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;
using ProjectManager.App.Models;
using ProjectManager.App.Services.Interfaces;
using static ProjectManager.App.Helpers.DataHolder;

namespace ProjectManager.App.Services;

public class UserService : IUserService
{
    private readonly HttpClient _httpClient;

    public UserService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<bool> RegisterUserAsync(string login, string password, int idRole)
    {
        var regData = new
        {
            login,
            password,
            idRole
        };

        try
        {
            var response = await _httpClient.PostAsJsonAsync($"{BaseUrl}/api/user/reg", regData);

            if (response.IsSuccessStatusCode) return true;

            var responseBody = await response.Content.ReadAsStringAsync();
            var errorMessage = JsonConvert.DeserializeObject<ErrorMessage>(responseBody);
            MessageBox.Show(errorMessage?.Message);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }

        return false;
    }

    public async Task<User?> AuthUserAsync(string login, string password)
    {
        var authData = new
        {
            login,
            password
        };

        try
        {
            var response = await _httpClient.PostAsJsonAsync($"{BaseUrl}/api/user/auth", authData);
            var responseBody = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode) return JsonConvert.DeserializeObject<User>(responseBody);

            MessageBox.Show("Неверный логин или пароль.");
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }

        return null;
    }

    public async Task<User?> UpdateUserAsync(int idUser, string login, string firstName, string lastName)
    {
        var updateUserData = new
        {
            idUser,
            login,
            firstName,
            lastName
        };

        try
        {
            var response = await _httpClient.PutAsJsonAsync($"{BaseUrl}/api/user/edit", updateUserData);
            var responseBody = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode) return JsonConvert.DeserializeObject<User>(responseBody);

            var errorMessage = JsonConvert.DeserializeObject<ErrorMessage>(responseBody);
            MessageBox.Show(errorMessage?.Message);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }

        return null;
    }

    public async Task<bool> DeleteUserAsync(int idUser)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/api/user/delete/{idUser}");
            if (response.IsSuccessStatusCode)
                return true;

            var responseBody = await response.Content.ReadAsStringAsync();
            var errorMessage = JsonConvert.DeserializeObject<ErrorMessage>(responseBody);
            MessageBox.Show(errorMessage?.Message);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }

        return false;
    }
}