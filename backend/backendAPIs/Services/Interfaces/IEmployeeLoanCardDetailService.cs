using backend.Models;
using backend.Models.Response;

namespace backend.Services.Interfaces
{
    public interface IEmployeeLoanCardDetailService
    {
        public List<ApprovedLoansResponse> GetAllApprovedLoansByEmployeeId(string employeeId);
        public List<ApprovedLoansResponse> GetAllApprovedLoans();
    }
}
