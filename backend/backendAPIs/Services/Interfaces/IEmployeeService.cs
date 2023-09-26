using backendAPIs.Models;
using backendAPIs.Models.Request;
using backendAPIs.Models.Response;

namespace backendAPIs.Services.Interfaces
{
    public interface IEmployeeService
    {
        public List<EmployeeResponse>? GetAllEmployees();
        public EmployeeResponse? GetEmployeeById(string id);
        public bool UpdateEmployee(UpdateEmployeeRequest employee);
        public EmployeeResponse? DeleteEmployee(string employeeId);
    }
}
