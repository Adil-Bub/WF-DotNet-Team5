using backendAPIs.Models;
using backendAPIs.Models.Request;
using backendAPIs.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace backendAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanCardController : ControllerBase
    {
        private readonly ILoanCardService _loanCardService;

        public LoanCardController(ILoanCardService loanCardService)
        {
            _loanCardService = loanCardService;
        }


        [HttpGet("all")]
        
        public async Task<ActionResult> GetAllLoanCards()
        {
            var loanCards =  _loanCardService.GetAllLoanCards();

            if(loanCards == null || loanCards.Count==0)
            {
                return NoContent();
            }
            return Ok(loanCards);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "admin,employee")]
        public async Task<ActionResult> GetLoanCardById(string id)
        {
            var loanCardResponse = _loanCardService.GetLoanCardById(id);

            if(loanCardResponse == null)
            {
                return NotFound();
            }

            return Ok(loanCardResponse);
        }

        [HttpPost("add")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> AddLoanCard([FromBody] LoanCardRequest loanCard)
        {

            var loanId = _loanCardService.AddLoanCard(loanCard);

            if (loanId == null)
            {
                return BadRequest("Invalid Loan Card Data");
            }
            var response = new { loanId = $"{loanId}"};
            return Ok(response);
        }

        [HttpPut("{id}")]
        //[Authorize(Roles = "admin")]
        public async Task<ActionResult> UpdateLoanCard(string id, [FromBody] UpdateLoanCardRequest loanCard)
        {
            if(loanCard == null)
            {
                return BadRequest("Invalid Loan Card data");
            }

            if(loanCard.LoanId != id)
            {
                return BadRequest("ID mismatch");
            }
            var isUpdated = _loanCardService.UpdateLoanCard(loanCard);

            if(!isUpdated)
            {
                return NotFound("Loan Card not found");
            }
            
            return Ok("Updated loan card details successfully!");
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> DeleteLoanCard(string id)
        {
            var isDeleted = _loanCardService.DeleteLoanCard(id);

            if(!isDeleted)
            {
                return BadRequest("Delete failed");
            }
            return Ok("Deleted loan card successfully");
        }
    }
}
