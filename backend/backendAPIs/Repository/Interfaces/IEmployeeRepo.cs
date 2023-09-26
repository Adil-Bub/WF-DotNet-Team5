using backendAPIs.Models;
using backendAPIs.Models.Response;
using backendAPIs.Models.Request;

namespace backendAPIs.Repository.Interfaces
{
    public interface IEmployeeRepo
    {
        public EmployeeMaster? GetEmployeeById(string employeeId);
        public bool AddEmployee(EmployeeMaster employee);
        public List<EmployeeResponse> GetAllEmployees();
        public bool UpdateEmployee(UpdateEmployeeRequest employee);
        public EmployeeResponse DeleteEmployee(string employeeId);
    }
}
