using Microsoft.AspNetCore.Mvc;
using ProjectManager.WebAPI.Services.Interfaces;

namespace ProjectManager.WebAPI.Controllers;

[ApiController]
[Route("api/projectsUsers")]
public class ProjectsUsersController : ControllerBase
{
    private readonly IProjectsUsersService _projectsUsersService;

    public ProjectsUsersController(IProjectsUsersService projectsUsersService)
    {
        _projectsUsersService = projectsUsersService;
    }

    private IActionResult HandleException(ArgumentException ex)
    {
        var error = new { message = ex.Message };
        return new JsonResult(error) { StatusCode = 400 };
    }

    [HttpGet]
    public async Task<IActionResult> GetProjectsAndUsersAsync()
    {
        try
        {
            return Ok(await _projectsUsersService.GetProjectsAndUsersAsync());
        }
        catch (ArgumentException ex)
        {
            return HandleException(ex);
        }
    }
}