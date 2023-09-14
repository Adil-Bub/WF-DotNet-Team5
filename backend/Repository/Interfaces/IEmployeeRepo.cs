using backend.Models;
using backend.Models.Response;

namespace backend.Repository.Interfaces
{
    public interface IEmployeeRepo
    {
        public EmployeeMaster? GetEmployeeById(string id);
        public bool AddEmployee(EmployeeMaster employee);
        public List<EmployeeResponse> GetAllEmployees();
    }
}
