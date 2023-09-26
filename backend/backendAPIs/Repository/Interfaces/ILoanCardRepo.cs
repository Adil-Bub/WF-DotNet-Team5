using backendAPIs.Models;
using backendAPIs.Models.Request;
namespace backendAPIs.Repository.Interfaces
{
    public interface ILoanCardRepo
    {
        public List<LoanCardMaster> GetAllLoanCards();
        public LoanCardMaster GetLoanCardById(string id);
        public string AddLoanCard(LoanCardMaster loanCard);
        public bool UpdateLoanCard(UpdateLoanCardRequest loanCard);
        public bool DeleteLoanCard(string id);  
    }
}
