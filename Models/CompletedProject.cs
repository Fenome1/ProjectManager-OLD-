using System;

namespace ProjectManager.App.Models;

public class CompletedProject
{
    public int IdCompletedProject { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public DateTime? CompletionDate { get; set; }
}