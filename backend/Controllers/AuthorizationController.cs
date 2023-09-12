using backend.Models;
using backend.Models.Request;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthorizationController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login(LoginRequest login)
        {
            IActionResult response = Unauthorized();
            EmployeeMaster? user = _authService.AuthenticateUser(login);

            if (user != null)
            {
                var loginResponse = _authService.GenerateJSONWebToken(user);
                response = Ok(loginResponse);
            }

            return response;
        }


    }
}
