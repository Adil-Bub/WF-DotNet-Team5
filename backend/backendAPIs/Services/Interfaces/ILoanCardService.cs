using backendAPIs.Models;
using backendAPIs.Models.Request;
using backendAPIs.Models.Response;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace backendAPIs.Services.Interfaces
{
    public interface ILoanCardService
    {
        public List<LoanCardResponse> GetAllLoanCards();
        public LoanCardResponse GetLoanCardById(string id);
        public string AddLoanCard(LoanCardRequest loanCardRequest);
        public bool UpdateLoanCard(UpdateLoanCardRequest loanCard);
        public bool DeleteLoanCard(string id);
    }
}
