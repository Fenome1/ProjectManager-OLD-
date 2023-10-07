using Microsoft.AspNetCore.Mvc;
using ProjectManager.WebAPI.Requests.Roles;
using ProjectManager.WebAPI.Services.Interfaces;

namespace ProjectManager.WebAPI.Controllers;

[ApiController]
[Route("api/role")]
public class RoleController : ControllerBase
{
    private readonly IRoleService _roleService;

    public RoleController(IRoleService roleService)
    {
        _roleService = roleService;
    }

    private IActionResult HandleException(ArgumentException ex)
    {
        var error = new { message = ex.Message };
        return new JsonResult(error) { StatusCode = 400 };
    }

    [HttpGet("roles/")]
    public async Task<IActionResult> GetAllRolesAsync()
    {
        try
        {
            return Ok(await _roleService.GetAllRolesAsync());
        }
        catch (ArgumentException ex)
        {
            return HandleException(ex);
        }
    }

    [HttpGet("{idRole:int}")]
    public async Task<IActionResult> GetRoleByIdAsync(int idRole)
    {
        try
        {
            return Ok(await _roleService.GetRoleByIdAsync(idRole));
        }
        catch (ArgumentException ex)
        {
            return HandleException(ex);
        }
    }

    [HttpPut("edit")]
    public async Task<IActionResult> EditRoleAsync([FromBody] EditRoleRequest request)
    {
        try
        {
            return Ok(await _roleService.EditRoleAsync(request));
        }
        catch (ArgumentException ex)
        {
            return HandleException(ex);
        }
    }
}