using backend.Models;
using backend.Models.Request;
using backend.Models.Response;

namespace backend.Services.Interfaces
{
    public interface IEmployeeService
    {
        public List<EmployeeResponse>? GetAllEmployees();
        public EmployeeResponse? GetEmployeeById(string id);
        public bool UpdateEmployee(UpdateEmployeeRequest employee);
    }
}
