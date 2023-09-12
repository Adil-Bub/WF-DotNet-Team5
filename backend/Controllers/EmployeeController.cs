using backend.Models;
using backend.Models.Request;
using backend.services;
using backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly LoansContext _db;
       /// private readonly JwtTokenService _jwtTokenService;

        public EmployeeController(IConfiguration configuration, LoansContext db)
        {
            _configuration= configuration;
            _db = db;
            //_jwtTokenService = jwtTokenService;
        }


        [HttpGet]
        public async Task<ActionResult> GetEmployees()
        {
            return Ok(_db.EmployeeMasters);
        }

        [HttpGet]
        [Route("findById")]
        public async Task<ActionResult> GetEmployeeById(string id)
        {
            EmployeeMaster? employee = await _db.EmployeeMasters.FindAsync(id);
            if(employee == null)
            {
                return BadRequest("Employee not found!");
            }
            else
            {
                return Ok(employee);
            }
        }

        [HttpPost("login")]
        public IActionResult Login(LoginRequest e)
        {

            var employee = _db.EmployeeMasters.FirstOrDefault(emp => emp.EmployeeId == e.EmployeeId);

            //temp fix
            if (employee==null) return Unauthorized("Invalid");
            var (hashedPassword, salt) = PasswordHelper.HashPassword(employee.PasswordHash);

            if (employee == null || !PasswordHelper.VerifyPassword(e.Password, hashedPassword, salt))
            {
                return Unauthorized("Invalid credentials");
            }

            // Generate JWT token
            //var token = _jwtTokenService.GenerateToken(employee);
            JwtTokenService jwtTokenService = new JwtTokenService(this._configuration);
            var token = jwtTokenService.GenerateToken(employee);
            return Ok(new { Token = token });
        }
     
        [HttpPut]
        public async Task<ActionResult> UpdateEmployee(EmployeeMaster employee)
        {
            _db.EmployeeMasters.Update(employee);
            await _db.SaveChangesAsync();
            return Ok("Updated employee details successfully!");
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteEmployee(string eId)
        {
            EmployeeMaster? employee = await _db.EmployeeMasters.FindAsync(eId);
            if(employee == null)
            {
                return BadRequest("Employee does not exist!");
            }
            else
            {
                _db.EmployeeMasters.Remove(employee);
                await _db.SaveChangesAsync();
                return Ok("Employee details removed!");
            }
        }

    }
}
