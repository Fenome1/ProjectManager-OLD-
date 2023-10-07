using System;
using System.Collections.Generic;

namespace ProjectManager.WebAPI.Models;

public partial class Project
{
    public int IdProject { get; set; }

    public int IdStatus { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime AddDate { get; set; }

    public DateTime? DeadlineDate { get; set; }

    public int? IdUser { get; set; }

    public virtual Status IdStatusNavigation { get; set; } = null!;

    public virtual User? IdUserNavigation { get; set; }
}
