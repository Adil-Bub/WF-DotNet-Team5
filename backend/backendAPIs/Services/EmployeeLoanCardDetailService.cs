using backend.Models;
using backend.Services.Interfaces;
using backend.Repository.Interfaces;
using backend.Models.Response;
using Azure.Core;

namespace backend.Services
{
    public class EmployeeLoanCardDetailService : IEmployeeLoanCardDetailService
    {
        private readonly IEmployeeLoanCardDetailRepo _employeeLoanCardDetailRepo;

        public EmployeeLoanCardDetailService(IEmployeeLoanCardDetailRepo employeeLoanCardDetailRepo)
        {
            _employeeLoanCardDetailRepo=employeeLoanCardDetailRepo;
        }

        public List<ApprovedLoansResponse> GetAllApprovedLoans()
        {
            var employeeLoans = _employeeLoanCardDetailRepo.GetAllApprovedLoans();

            var approvedLoans = employeeLoans.Select(employee => new ApprovedLoansResponse
            {
                CardId = employee.CardId,
                RequestId = employee.RequestId,
                EmployeeId = employee.EmployeeId,
                LoanId = employee.LoanId,
                CardIssueDate = employee.CardIssueDate
            })
            .ToList();

            return approvedLoans;
        }

        public List<ApprovedLoansResponse> GetAllApprovedLoansByEmployeeId(string employeeId)
        {
            var employeeLoans = _employeeLoanCardDetailRepo.GetAllApprovedLoansByEmployeeId(employeeId);

            var approvedLoans = employeeLoans.Select(employee => new ApprovedLoansResponse
            {
                CardId = employee.CardId,
                RequestId = employee.RequestId,
                EmployeeId = employee.EmployeeId,
                LoanId = employee.LoanId,
                CardIssueDate = employee.CardIssueDate
            })
            .ToList();

            return approvedLoans;
        }
    }
}
