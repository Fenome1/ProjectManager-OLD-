using Microsoft.AspNetCore.Mvc;
using ProjectManager.WebAPI.Requests.Users;
using ProjectManager.WebAPI.Services.Interfaces;

namespace ProjectManager.WebAPI.Controllers;

[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    private IActionResult HandleException(ArgumentException ex)
    {
        var error = new { message = ex.Message };
        return new JsonResult(error) { StatusCode = 400 };
    }

    [HttpGet("users/")]
    public async Task<IActionResult> GetAllUsersAsync()
    {
        try
        {
            return Ok(await _userService.GetAllUsersAsync());
        }
        catch (ArgumentException ex)
        {
            return HandleException(ex);
        }
    }

    [HttpGet("{idUser:int}")]
    public async Task<IActionResult> GetUserByIdAsync(int idUser)
    {
        try
        {
            return Ok(await _userService.GetUserByIdAsync(idUser));
        }
        catch (ArgumentException ex)
        {
            return HandleException(ex);
        }
    }

    [HttpPost("auth")]
    public async Task<IActionResult> AuthenticateUserAsync([FromBody] AuthenticateUserRequest request)
    {
        try
        {
            return Ok(await _userService.AuthenticateUserAsync(request));
        }
        catch (ArgumentException ex)
        {
            return HandleException(ex);
        }
    }

    [HttpPost("reg")]
    public async Task<IActionResult> RegisterUserAsync([FromBody] RegisterUserRequest request)
    {
        try
        {
            return Ok(await _userService.RegisterUserAsync(request));
        }
        catch (ArgumentException ex)
        {
            return HandleException(ex);
        }
    }

    [HttpPut("edit")]
    public async Task<IActionResult> EditUserNameAsync([FromBody] EditUserRequest request)
    {
        try
        {
            return Ok(await _userService.EditUserAsync(request));
        }
        catch (ArgumentException ex)
        {
            return HandleException(ex);
        }
    }

    [HttpDelete("delete/{idUser}")]
    public async Task<IActionResult> DeleteUserAsync(int idUser)
    {
        try
        {
            return Ok(await _userService.DeleteUserAsync(idUser));
        }
        catch (ArgumentException ex)
        {
            return HandleException(ex);
        }
    }
}