using backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanCardController : ControllerBase
    {
        private readonly LoansContext _db;

        public LoanCardController(LoansContext db)
        {
            _db = db;
        }


        [HttpGet]
        public async Task<ActionResult> GetLoanCards()
        {
            return Ok(_db.LoanCardMasters);
        }

        [HttpGet]
        [Route("findById")]
        public async Task<ActionResult> GetLoanCardById(string id)
        {
            LoanCardMaster? loanCard = await _db.LoanCardMasters.FindAsync(id);
            if (loanCard == null)
            {
                return BadRequest("Loan card not found!");
            }
            else
            {
                return Ok(loanCard);
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddLoanCard(LoanCardMaster loanCard)
        {
            _db.LoanCardMasters.Add(loanCard);
            await _db.SaveChangesAsync();
            return Ok("Successfully added Loan card!");
        }

        [HttpPut]
        public async Task<ActionResult> UpdateEmployee(LoanCardMaster loanCard)
        {
            _db.LoanCardMasters.Update(loanCard);
            await _db.SaveChangesAsync();
            return Ok("Updated loan card details successfully!");
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteEmployee(string eId)
        {
            LoanCardMaster? loanCard = await _db.LoanCardMasters.FindAsync(eId);
            if (loanCard == null)
            {
                return BadRequest("Loan card does not exist!");
            }
            else
            {
                _db.LoanCardMasters.Remove(loanCard);
                await _db.SaveChangesAsync();
                return Ok("Loan card details removed!");
            }
        }
    }
}
