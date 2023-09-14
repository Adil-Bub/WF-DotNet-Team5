using backend.Models;
using backend.Models.Response;

namespace backend.Services.Interfaces
{
    public interface IEmployeeService
    {
        public List<EmployeeResponse>? GetAllEmployees();
        public EmployeeResponse? GetEmployeeById(string id);
    }
}
