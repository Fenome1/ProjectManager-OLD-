using System.Collections.Generic;
using ProjectManager.App.Models;

namespace ProjectManager.App.Helpers;

public class DataHolder
{
    public static List<Role>? Roles { get; set; }
    public static List<Status>? Statuses { get; set; }
    public static User? CurrentUser { get; set; }
    public static string BaseUrl => "https://localhost:7169";
}