using System;
using System.Collections.Generic;

namespace ProjectManager.WebAPI.Models;

public partial class Status
{
    public int IdStatus { get; set; }

    public string Title { get; set; } = null!;

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
}
