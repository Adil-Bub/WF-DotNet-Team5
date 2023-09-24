using backend.Models;
using backend.Models.Request;
using backend.Models.Response;

namespace backend.Services.Interfaces
{
    public interface IEmployeeRequestService
    {
        public EmployeeLoanRequestResponse GetEmployeeRequestDetailByRequestId(string requestId);
        public List<EmployeeLoanRequestResponse> GetAllEmployeeRequests();
        public List<EmployeeLoanRequestResponse> GetAllEmployeeRequestsByEmployeeId(string employeeId);
        public List<EmployeeLoanRequestResponse> GetAllEmployeeRequestsByStatus(string status);
        public List<EmployeeLoanRequestResponse> GetAllEmployeeRequestsByItemId(string itemId);
        public string AddEmployeeRequest(EmployeeLoanRequest employeeLoanRequest);
        public bool UpdateEmployeeRequest(UpdateEmployeeLoanRequest employeeRequestDetail);
        public EmployeeRequestDetail DeleteEmployeeRequest(string id);

        public List<LoanDetailsResponse> GetAllLoanDetailsByEmployeeId(string employeeId);
        public List<LoanDetailsResponse> GetAllLoanDetailsByRequestId(string requestId);

        public List<LoanDetailsAdminResponse> GetAllRequestDetails();

    }
}
