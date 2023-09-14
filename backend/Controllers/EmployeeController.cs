using backend.Models;
using backend.Models.Response;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }


        [HttpGet("all")]
        //[Authorize(Roles ="admin")]
        public async Task<ActionResult> GetEmployees()
        {
            var response = _employeeService.GetAllEmployees();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeResponse>> GetEmployeeById(string id)
        {
            var employee = _employeeService.GetEmployeeById(id);
            if(employee==null)
            {
                return NotFound();
            }
            return employee;
            
        }

        //[HttpPut]
        //public async Task<ActionResult> UpdateEmployee(EmployeeMaster employee)
        //{
        //    _db.EmployeeMasters.Update(employee);
        //    await _db.SaveChangesAsync();
        //    return Ok("Updated employee details successfully!");
        //}

        //[HttpDelete]
        //public async Task<ActionResult> DeleteEmployee(string eId)
        //{
        //    EmployeeMaster? employee = await _db.EmployeeMasters.FindAsync(eId);
        //    if (employee == null)
        //    {
        //        return BadRequest("Employee does not exist!");
        //    }
        //    else
        //    {
        //        _db.EmployeeMasters.Remove(employee);
        //        await _db.SaveChangesAsync();
        //        return Ok("Employee details removed!");
        //    }
        //}

    }
}
