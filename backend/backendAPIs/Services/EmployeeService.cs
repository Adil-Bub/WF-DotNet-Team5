using backendAPIs.Models;
using backendAPIs.Models.Request;
using backendAPIs.Models.Response;
using backendAPIs.Repository.Interfaces;
using backendAPIs.Services.Interfaces;

namespace backendAPIs.Services
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

        public EmployeeResponse? DeleteEmployee(string employeeId)
        {
            return _employeeRepo.DeleteEmployee(employeeId);
        }
    }
}
