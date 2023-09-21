using backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeLoanCardController : ControllerBase
    {
        private readonly IEmployeeLoanCardDetailService _employeeLoanCardDetailService;

        public EmployeeLoanCardController(IEmployeeLoanCardDetailService employeeLoanCardDetailService)
        {
            _employeeLoanCardDetailService=employeeLoanCardDetailService;
        }

        [HttpGet("/approved/all")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> GetAllApprovedLoans()
        {
            var approvedLoans = _employeeLoanCardDetailService.GetAllApprovedLoans();

            if(!approvedLoans.Any())
            {
                return Ok("No approved loans yet");
            }
            return Ok(approvedLoans);
        }

        [HttpGet("/approved/{employee-id}")]
        [Authorize(Roles = "admin,employee")]
        public async Task<ActionResult> GetAllApprovedLoansByEmployeeId([FromRoute(Name = "employee-id")] string employeeId)
        {
            var approvedLoans = _employeeLoanCardDetailService.GetAllApprovedLoansByEmployeeId(employeeId);

            if(!approvedLoans.Any())
            {
                return Ok("No approved loans for this ID yet");
            }
            return Ok(approvedLoans);
        }
           
    }
}
