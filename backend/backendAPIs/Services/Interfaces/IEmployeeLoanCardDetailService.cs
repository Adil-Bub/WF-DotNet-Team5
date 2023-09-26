using backendAPIs.Models;
using backendAPIs.Models.Response;

namespace backendAPIs.Services.Interfaces
{
    public interface IEmployeeLoanCardDetailService
    {
        public List<ApprovedLoansResponse> GetAllApprovedLoansByEmployeeId(string employeeId);
        public List<ApprovedLoansResponse> GetAllApprovedLoans();
    }
}
