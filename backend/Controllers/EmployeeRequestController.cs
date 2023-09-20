using backend.Services.Interfaces;
using backend.Models.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using backend.Services;
using backend.Models;
using Microsoft.AspNetCore.Authorization;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeRequestController : ControllerBase
    {
        private readonly IEmployeeRequestService _employeeRequestService;

        public EmployeeRequestController(IEmployeeRequestService employeeRequestService)
        {
            _employeeRequestService = employeeRequestService;
        }
        [HttpGet("all")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> GetAllEmployeeRequests()
        {
            var employeeRequests = _employeeRequestService.GetAllEmployeeRequests();

            if (employeeRequests == null || employeeRequests.Count==0)
            {
                return NoContent();
            }
            return Ok(employeeRequests);
        }

        [HttpGet("/request-id/{request-id}")]
        [Authorize(Roles = "admin,employee")]
        public async Task<ActionResult> GetEmployeeRequestDetailByRequestId([FromRoute(Name = "request-id")] string requestId)
        {
            if (requestId == null)
            {
                return BadRequest("Please enter a valid request ID");
            }
            var employeeRequest = _employeeRequestService.GetEmployeeRequestDetailByRequestId(requestId);

            if (employeeRequest == null)
            {
                return NotFound("No request with given request ID");
            }
            return Ok(employeeRequest);
        }

        [HttpGet("/employee-id/{employee-id}")]
        [Authorize(Roles = "admin,employee")]
        public async Task<ActionResult> GetEmployeeRequestDetailByEmployeeId([FromRoute(Name = "employee-id")] string employeeId)
        {
            if (employeeId == null)
            {
                return BadRequest("Please enter a valid Employee ID");
            }
            var employeeRequest = _employeeRequestService.GetAllEmployeeRequestsByEmployeeId(employeeId);

            if (employeeRequest == null || employeeRequest.Count == 0)
            {
                return NotFound("No requests for given employee ID");
            }
            return Ok(employeeRequest);
        }

        [HttpGet("/status/{status}")]
        [Authorize(Roles = "admin,employee")]
        public async Task<ActionResult> GetEmployeeRequestDetailByStatus([FromRoute(Name = "status")] string status)
        {
            if (status == null)
            {
                return BadRequest("Please enter a valid status");
            }
            var employeeRequest = _employeeRequestService.GetAllEmployeeRequestsByStatus(status);

            if (employeeRequest == null || employeeRequest.Count == 0)
            {
                return NotFound("No requests for given status");
            }
            return Ok(employeeRequest);
        }

        [HttpGet("/item-id/{item-id}")]
        [Authorize(Roles = "admin,employee")]
        public async Task<ActionResult> GetEmployeeRequestDetailByItemId([FromRoute(Name = "item-id")] string itemId)
        {
            if (itemId == null)
            {
                return BadRequest("Please enter a valid Item ID");
            }
            var employeeRequest = _employeeRequestService.GetAllEmployeeRequestsByItemId(itemId);

            if (employeeRequest == null || employeeRequest.Count == 0)
            {
                return NotFound("No requests for given item ID");
            }
            return Ok(employeeRequest);
        }

        [HttpGet("/my-loans/{employee-id}")]
        [Authorize(Roles = "admin,employee")]
        public async Task<ActionResult> GetAllLoanDetailsByEmployeeId([FromRoute(Name = "employee-id")] string employeeId)
        {
            if (employeeId == null)
            {
                return BadRequest("Enter employee id");
            }

            var loanDetails = _employeeRequestService.GetAllLoanDetailsByEmployeeId(employeeId);
            if (loanDetails==null)
            {
                return NoContent();
            }
            return Ok(loanDetails);
        }
        [HttpPost("add")]
        [Authorize(Roles = "admin,employee")]
        public async Task<ActionResult> AddEmployeeRequest(EmployeeLoanRequest employeeLoanRequest)
        {
            if (employeeLoanRequest == null)
            {
                return BadRequest("Invalid loan request");
            }
            var requestId = _employeeRequestService.AddEmployeeRequest(employeeLoanRequest);
            if (requestId== null)
            {
                return BadRequest("Failed to add new loan request");
            }
            var response = new { requestId = $"{requestId}" };
            return Ok(response);
        }

        [HttpPut("{request-id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> UpdateEmployeeRequest([FromRoute(Name = "request-id")] string requestId, [FromBody] UpdateEmployeeLoanRequest employeeRequestDetail)
        {
            if (employeeRequestDetail == null)
            {
                return BadRequest("Invalid Employee Request details");
            }

            if (employeeRequestDetail.RequestId != requestId)
            {
                return BadRequest("ID mismatch");
            }
            var isUpdated = _employeeRequestService.UpdateEmployeeRequest(employeeRequestDetail);

            if (!isUpdated)
            {
                return NotFound("Request  not found");
            }

            return Ok("Updated Employee loan card request details successfully!");
        }

        [HttpDelete("{request-id}")]
        [Authorize(Roles = "admin,employee")]
        public async Task<ActionResult> DeleteEmployeeRequestByRequestId([FromRoute(Name = "request-id")] string requestId)
        {
            if (requestId==null)
            {
                return BadRequest("Please enter an employee id");
            }

            var deletedEmployeeRequest = _employeeRequestService.DeleteEmployeeRequest(requestId);
            if (deletedEmployeeRequest == null)
            {
                return NotFound();
            }
            return Ok(deletedEmployeeRequest);

        }
    }
}
