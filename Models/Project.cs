using System;

namespace ProjectManager.App.Models;

public class Project
{
    public int IdProject { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public DateTime AddDate { get; set; }
    public DateTime? DeadlineDate { get; set; }
    public int IdStatus { get; set; }
    public int? IdUser { get; set; }
}