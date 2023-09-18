using backend.Models;
using backend.Models.Request;

namespace backend.Repository.Interfaces
{
    public interface IEmployeeRequestDetailRepo
    {
        public EmployeeRequestDetail GetEmployeeRequestDetailByRequestId(string requestId);
        public List<EmployeeRequestDetail> GetAllEmployeeRequests();
        public List<EmployeeRequestDetail> GetAllEmployeeRequestsByEmployeeId(string employeeId);
        public List<EmployeeRequestDetail> GetAllEmployeeRequestsByStatus(string status);
        public List<EmployeeRequestDetail> GetAllEmployeeRequestsByItemId(string itemId);
        public string AddEmployeeRequest(EmployeeRequestDetail employeeRequestDetail);
        public bool UpdateEmployeeRequest(UpdateEmployeeLoanRequest employeeRequestDetail);
        public EmployeeRequestDetail DeleteEmployeeRequest(string id);
    }
}
