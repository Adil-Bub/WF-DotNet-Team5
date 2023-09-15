using backend.Models;
using backend.Models.Request;
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
        [Authorize(Roles ="admin")]
        public async Task<ActionResult> GetEmployees()
        {
            var response = _employeeService.GetAllEmployees();
            return Ok(response);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "admin,employee")]
        public async Task<ActionResult<EmployeeResponse>> GetEmployeeById(string id)
        {
            var employee = _employeeService.GetEmployeeById(id);
            if(employee==null)
            {
                return NotFound();
            }
            return employee;
            
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin,employee")]
        public async Task<ActionResult> UpdateEmployee(string id, [FromBody] UpdateEmployeeRequest employee)
        {
            if(employee == null)
            {
                return BadRequest("Invalid employee data");
            }
            
            if(id!=employee.EmployeeId)
            {
                return BadRequest("ID mismatch");
            }

            bool updated = _employeeService.UpdateEmployee(employee);

            if(!updated)
            {
                return NotFound();
            }
            return NoContent();
        }

        //[HttpDelete]
        //public async Task<ActionResult> DeleteEmployee(string employeeId)
        //{
        //    if (employeeId==null)
        //    {
        //        return BadRequest("Please enter an employee id");
        //    }

        //    bool deleted = _employeeService.DeeleteEmployee(employeeId);
        //    if(!deleted)
        //    {
        //        return NotFound();
        //    }
        //    return Ok()
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
