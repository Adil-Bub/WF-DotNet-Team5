using backend.Models;
using backend.Models.Request;
using backend.Models.Response;
using backend.Repository.Interfaces;
using backend.Services.Interfaces;

namespace backend.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepo _employeeRepo;

        public EmployeeService(IEmployeeRepo employeeRepo)
        {
            _employeeRepo = employeeRepo;
        }
        public List<EmployeeResponse>? GetAllEmployees()
        {
            return _employeeRepo.GetAllEmployees();
        }

        public EmployeeResponse? GetEmployeeById(string id)
        {
            var employee = _employeeRepo.GetEmployeeById(id);
            return (employee!=null) ? new EmployeeResponse(employee) : null ;
        }

        public bool UpdateEmployee(UpdateEmployeeRequest employee)
        {
            return _employeeRepo.UpdateEmployee(employee);
        }
    }
}
