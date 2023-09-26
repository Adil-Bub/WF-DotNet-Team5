using backendAPIs.Models;
using backendAPIs.Models.Request;
using backendAPIs.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace backendAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly Serilog.ILogger _logger;

        public AuthorizationController(IAuthService authService)
        {
            _authService = authService;
            _logger = Log.ForContext<AuthorizationController>();
        }

        [HttpPost("login")]
        public IActionResult Login(LoginRequest login)
        {
            EmployeeMaster? user = _authService.AuthenticateUser(login);
            if (user != null)
            {
                try
                {
                    var loginResponse = _authService.GenerateJSONWebToken(user);
                    return Ok(loginResponse);
                }catch (Exception ex)
                {
                    _logger.Error(ex.Message);
                    return StatusCode(500, "Something went wrong! Please try again later!");
                }
            }
            return StatusCode(401, "Plese enter valid credentials!");
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterRequest registerRequest)
        {
            IActionResult response = StatusCode(500, "Something went wrong! Please try again later!");
            EmployeeMaster? user = _authService.RegisterUser(registerRequest);

            if (user != null)
            {
                var registerResponse = _authService.GenerateJSONWebToken(user);
                return Ok(registerResponse);
            }

            return response;
        }
    }
}
