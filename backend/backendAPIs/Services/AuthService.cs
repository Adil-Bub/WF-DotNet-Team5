using backendAPIs.Repository.Interfaces;
using backendAPIs.Models;
using backendAPIs.Models.Request;
using backendAPIs.Models.Response;
using backendAPIs.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using backendAPIs.Util;

namespace backendAPIs.Services
{
    public class AuthService : IAuthService
    {
        private readonly IEmployeeRepo _employeeRepo;
        private readonly IConfiguration _configuration;
        public AuthService(IEmployeeRepo employeeRepo, IConfiguration configuration)
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

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            List<Claim> claims = new List<Claim>();
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
              expires: DateTime.Now.AddMinutes(20),
              signingCredentials: credentials);
            
            var key = new JwtSecurityTokenHandler().WriteToken(token);

            return new LoginResponse(key, userInfo.EmployeeId, userInfo.Designation, userInfo.EmployeeName);
        }

        EmployeeMaster? IAuthService.AuthenticateUser(LoginRequest loginRequest)
        {
            EmployeeMaster? employee = _employeeRepo.GetEmployeeById(loginRequest.EmployeeId);
            if (employee == null || !PasswordHelper.VerifyPassword(loginRequest.Password, employee.PasswordHash, employee.Salt))
            {
                return null;
            }
            return employee;
        }

        EmployeeMaster? IAuthService.RegisterUser(RegisterRequest registerRequest)
        {
            var employeeId = UIDGenerator.GenerateUniqueVarcharId("EMP");

            var (hashedPassword, salt) = PasswordHelper.HashPassword(registerRequest.Password);

            var employee = new EmployeeMaster(employeeId, hashedPassword, salt, registerRequest);
            try
            {
                bool status = _employeeRepo.AddEmployee(employee);
                if(status)
                {
                    return _employeeRepo.GetEmployeeById(employee.EmployeeId) ;
                }
            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }
    }
}
