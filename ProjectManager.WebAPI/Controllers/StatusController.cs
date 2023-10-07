using Microsoft.AspNetCore.Mvc;
using ProjectManager.WebAPI.Requests.Statuses;
using ProjectManager.WebAPI.Services.Interfaces;

namespace ProjectManager.WebAPI.Controllers;

[ApiController]
[Route("api/status")]
public class StatusController : ControllerBase
{
    private readonly IStatusService _statusService;

    public StatusController(IStatusService statusService)
    {
        _statusService = statusService;
    }

    private IActionResult HandleException(ArgumentException ex)
    {
        var error = new { message = ex.Message };
        return new JsonResult(error) { StatusCode = 400 };
    }

    [HttpGet("statuses/")]
    public async Task<IActionResult> GetAllStatusesAsync()
    {
        try
        {
            return Ok(await _statusService.GetAllStatusesAsync());
        }
        catch (ArgumentException ex)
        {
            return HandleException(ex);
        }
    }

    [HttpGet("{idStatus:int}")]
    public async Task<IActionResult> GetStatusByIdAsync(int idStatus)
    {
        try
        {
            return Ok(await _statusService.GetStatusByIdAsync(idStatus));
        }
        catch (ArgumentException ex)
        {
            return HandleException(ex);
        }
    }

    [HttpPut("edit")]
    public async Task<IActionResult> EditStatusAsync([FromBody] EditStatusRequest request)
    {
        try
        {
            return Ok(await _statusService.EditStatusAsync(request));
        }
        catch (ArgumentException ex)
        {
            return HandleException(ex);
        }
    }
}