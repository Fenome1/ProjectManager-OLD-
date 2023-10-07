using System;
using System.Collections.Generic;

namespace ProjectManager.WebAPI.Models;

public partial class UserView
{
    public string Login { get; set; } = null!;

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string Post { get; set; } = null!;
}
