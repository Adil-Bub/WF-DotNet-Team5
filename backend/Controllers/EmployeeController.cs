using backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly LoansContext _db;

        public EmployeeController(LoansContext db)
        {
            _db = db;
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

        [HttpPost]
        public async Task<ActionResult> AddEmployee(EmployeeMaster employee)
        {
            _db.EmployeeMasters.Add(employee);
            await _db.SaveChangesAsync();
            return Ok("Successfully added employee!");
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
