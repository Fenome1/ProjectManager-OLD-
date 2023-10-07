using Microsoft.AspNetCore.Mvc;
using ProjectManager.WebAPI.Services.Interfaces;

namespace ProjectManager.WebAPI.Controllers
{
    [ApiController]
    [Route("api/userView")]
    public class UserViewController : ControllerBase
    {
        private readonly IUserViewService _userViewService;

        public UserViewController(IUserViewService userViewService)
        {
            _userViewService = userViewService;
        }

        private IActionResult HandleException(ArgumentException ex)
        {
            var error = new { message = ex.Message };
            return new JsonResult(error) { StatusCode = 400 };
        }

        [HttpGet("users/view/")]
        public async Task<IActionResult> GetAllUsersViewAsync()
        {
            try
            {
                return Ok(await _userViewService.GetUsersViewAsync());
            }
            catch (ArgumentException ex)
            {
                return HandleException(ex);
            }
        }
    }
}
