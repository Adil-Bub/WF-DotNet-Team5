using backend.Models;
using backend.Models.Request;
using backend.Models.Response;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace backend.Services.Interfaces
{
    public interface ILoanCardService
    {
        public List<LoanCardResponse> GetAllLoanCards();
        public LoanCardResponse GetLoanCardById(string id);
        public string AddLoanCard(LoanCardRequest loanCardRequest);
        public bool UpdateLoanCard(LoanCardMaster loanCard);
        public bool DeleteLoanCard(string id);
    }
}
