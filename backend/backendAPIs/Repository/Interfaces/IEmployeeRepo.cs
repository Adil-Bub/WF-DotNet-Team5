using backend.Models;
using backend.Models.Response;
using backend.Models.Request;

namespace backend.Repository.Interfaces
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
