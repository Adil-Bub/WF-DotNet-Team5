using backend.Models;

namespace backend.Repository.Interfaces
{
    public interface ILoanCardRepo
    {
        public List<LoanCardMaster> GetAllLoanCards();
        public LoanCardMaster GetLoanCardById(string id);
        public string AddLoanCard(LoanCardMaster loanCard);
        public bool UpdateLoanCard(LoanCardMaster loanCard);
        public bool DeleteLoanCard(string id);  
    }
}
