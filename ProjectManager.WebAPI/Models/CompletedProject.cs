using System;
using System.Collections.Generic;

namespace ProjectManager.WebAPI.Models;

public partial class CompletedProject
{
    public int IdCompletedProject { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime? CompletionDate { get; set; }
}
