using backend.Data;
using backend.Models;
using backend.Models.Request;
using backend.Models.Response;
using backend.Services;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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

        [HttpPost]
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
