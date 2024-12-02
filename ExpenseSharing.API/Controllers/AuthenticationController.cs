using ExpenseSharing.Application.Common.Interfaces.Service;
using ExpenseSharing.Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ExpenseSharing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthService _userService;

        public AuthenticationController(IAuthService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginRequestModel model)
        {
            try
            {
                var login = await _userService.LoginAsync(model);
                return Ok(new { acccessToken = login });
            }
            catch(UnauthorizedAccessException exception)
            {
                return Unauthorized(new {message = exception.Message});
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { message = "An error occurred during login." });
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegistrationRequestModel model)
        {
            try
            {
                await _userService.RegisterAsync(model);
                return Ok(new { message = "Registration successfull" });
            }
            catch(RoleNotFoundException exception)
            {
                return BadRequest(exception.Message);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { message = "An error occurred during registration." });
            }
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await _userService.LogoutAsync();
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { message = "An error occurred while logging out." });
            }
        }
    }
}
