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
        public async Task<ActionResult<IEnumerable<EmployeeResponse>>> GetEmployees()
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
            //pass the fields not being edited as null or same value (in UpdateEmployeeRequest)
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

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> DeleteEmployee(string id)
        {
            if (id==null)
            {
                return BadRequest("Please enter an employee id");
            }

            var deletedEmployee = _employeeService.DeleteEmployee(id);
            if (deletedEmployee == null)
            {
                return NotFound();
            }
            return Ok(deletedEmployee);
           
        }

    }
}
