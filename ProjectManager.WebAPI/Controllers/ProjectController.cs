using Microsoft.AspNetCore.Mvc;
using ProjectManager.WebAPI.Requests.Projects;
using ProjectManager.WebAPI.Services.Interfaces;

namespace ProjectManager.WebAPI.Controllers;

[ApiController]
[Route("api/project")]
public class ProjectController : ControllerBase
{
    private readonly IProjectService _projectService;

    public ProjectController(IProjectService projectService)
    {
        _projectService = projectService;
    }

    private IActionResult HandleException(ArgumentException ex)
    {
        var error = new { message = ex.Message };
        return new JsonResult(error) { StatusCode = 400 };
    }

    [HttpGet("projects/")]
    public async Task<IActionResult> GetAllProjectAsync()
    {
        try
        {
            return Ok(await _projectService.GetAllProjectsAsync());
        }
        catch (ArgumentException ex)
        {
            return HandleException(ex);
        }
    }

    [HttpGet("{idProject}")]
    public async Task<IActionResult> GetAllProjectAsync(int idProject)
    {
        try
        {
            return Ok(await _projectService.GetProjectByIdAsync(idProject));
        }
        catch (ArgumentException ex)
        {
            return HandleException(ex);
        }
    }

    [HttpGet("user/{idUser}")]
    public async Task<IActionResult> GetAllUserProject(int idUser)
    {
        try
        {
            return Ok(await _projectService.GetProjectsByUserIdAsync(idUser));
        }
        catch (ArgumentException ex)
        {
            return HandleException(ex);
        }
    }

    [HttpGet("status/{idStatus}")]
    public async Task<IActionResult> GetAllProjectsByStatus(int idStatus)
    {
        try
        {
            return Ok(await _projectService.GetProjectsByIdStatusAsync(idStatus));
        }
        catch (ArgumentException ex)
        {
            return HandleException(ex);
        }
    }

    [HttpPost("create/")]
    public async Task<IActionResult> CreateProjectAsync(CreateProjectRequest request)
    {
        try
        {
            return Ok(await _projectService.CreateProjectAsync(request));
        }
        catch (ArgumentException ex)
        {
            return HandleException(ex);
        }
    }

    [HttpPut("update/")]
    public async Task<IActionResult> EditProjectAsync(EditProjectRequest request)
    {
        try
        {
            return Ok(await _projectService.EditProjectAsync(request));
        }
        catch (ArgumentException ex)
        {
            return HandleException(ex);
        }
    }

    [HttpPut("update/assign/add/")]
    public async Task<IActionResult> UpdateProjectAssignToUser(AssignProjectToUserRequest request)
    {
        try
        {
            return Ok(await _projectService.UpdateUserAssignAsync(request));
        }
        catch (ArgumentException ex)
        {
            return HandleException(ex);
        }
    }

    [HttpPut("update/assign/reset/")]
    public async Task<IActionResult> ResetProjectAssignToUser(ResetUserAssignToProject request)
    {
        try
        {
            return Ok(await _projectService.RemoveUserAssignAsync(request));
        }
        catch (ArgumentException ex)
        {
            return HandleException(ex);
        }
    }

    [HttpDelete("delete/{idProject}")]
    public async Task<IActionResult> DeleteProjectAsync(int idProject)
    {
        try
        {
            return Ok(await _projectService.DeleteProjectAsync(idProject));
        }
        catch (ArgumentException ex)
        {
            return HandleException(ex);
        }
    }
}
