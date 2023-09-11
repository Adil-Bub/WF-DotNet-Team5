using backend.Data;
using backend.Models;
using backend.Models.Request;
using backend.Models.Response;
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
        private readonly EmployeeDataProvider _employeeDataProvider;
        private readonly IConfiguration _configuration;

        public AuthorizationController(EmployeeDataProvider employeeDataProvider, IConfiguration config)
        {
            _employeeDataProvider = employeeDataProvider;
            _configuration = config;
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(EmployeeLoginViewModel login)
        {
            IActionResult response = Unauthorized();
            EmployeeMaster? user = AuthenticateUser(login);

            if (user != null)
            {
                var tokenString = GenerateJSONWebToken(user);
                response = Ok(new LoginResponse{ token = tokenString, EmployeeId = login.EmployeeId, Designation = user.Designation });
            }

            return response;
        }

        private string GenerateJSONWebToken(EmployeeMaster userInfo)
        {

            if (userInfo is null)
            {
                throw new ArgumentNullException(nameof(userInfo));
            }
            List<Claim> claims = new List<Claim>();
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            claims.Add(new Claim("Username", userInfo.EmployeeName));
            if (userInfo.Designation == "admin")
            {
                claims.Add(new Claim("role", "admin"));
            }
            else
            {
                claims.Add(new Claim("role", "employee"));

            }
            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
              _configuration["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(2),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private EmployeeMaster? AuthenticateUser(EmployeeLoginViewModel login)
        {
            EmployeeMaster? employee = _employeeDataProvider.GetEmployeeDetail(login);
            return employee;
        }


    }
}
