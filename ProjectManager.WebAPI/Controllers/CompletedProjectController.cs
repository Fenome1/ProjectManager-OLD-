using Microsoft.AspNetCore.Mvc;
using ProjectManager.WebAPI.Services.Interfaces;

namespace ProjectManager.WebAPI.Controllers
{
    [ApiController]
    [Route("api/completedProjects")]
    public class CompletedProjectController : ControllerBase
    {
        private readonly ICompletedProjectService _completedProjectService;

        public CompletedProjectController(ICompletedProjectService completedProjectService)
        {
            _completedProjectService = completedProjectService;
        }

        private IActionResult HandleException(ArgumentException ex)
        {
            var error = new { message = ex.Message };
            return new JsonResult(error) { StatusCode = 400 };
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCompletedProjects()
        {
            try
            {
                return Ok(await _completedProjectService.GetCompletedProjectsAsync());
            }
            catch (ArgumentException ex)
            {
                return HandleException(ex);
            }
        }
    }
}
