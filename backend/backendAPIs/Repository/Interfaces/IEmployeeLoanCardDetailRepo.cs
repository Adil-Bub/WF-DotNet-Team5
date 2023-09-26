using backendAPIs.Models;

namespace backendAPIs.Repository.Interfaces
{
    public interface IEmployeeLoanCardDetailRepo 
    {
        public string AddEmployeeLoanCard(EmployeeLoanCardDetail employeeLoanCardDetail);

        public List<EmployeeLoanCardDetail> GetAllApprovedLoansByEmployeeId(string employeeId);
        public List<EmployeeLoanCardDetail> GetAllApprovedLoans();
    }
}
