using System;
using System.Collections.Generic;

namespace ProjectManager.WebAPI.Models;

public partial class User
{
    public int IdUser { get; set; }

    public int IdRole { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public virtual Role IdRoleNavigation { get; set; } = null!;

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
}
