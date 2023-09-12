using backend.Data;
using backend.Models;
using backend.Models.Request;
using backend.Models.Response;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace backend.Services
{
    public class AuthService: IAuthService
    {
        private readonly EmployeeRepo _employeeRepo;
        private readonly IConfiguration _configuration;
        public AuthService(EmployeeRepo employeeRepo, IConfiguration configuration)
        {
            _employeeRepo = employeeRepo;
            _configuration = configuration;
        }      

        LoginResponse IAuthService.GenerateJSONWebToken(EmployeeMaster userInfo)
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
            var key = new JwtSecurityTokenHandler().WriteToken(token);

            return new LoginResponse(key, userInfo.EmployeeId, userInfo.Designation); 
        }

      
       

        EmployeeMaster? IAuthService.AuthenticateUser(LoginRequest login)
        {
            EmployeeMaster? employee = _employeeRepo.GetEmployeeDetail(login);
            return employee;
        }

       
    }
}
