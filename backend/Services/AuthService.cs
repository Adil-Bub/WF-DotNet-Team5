using backend.Data;
using backend.Models;
using backend.Models.Request;

namespace backend.Services
{
    public class AuthService: IAuthService
    {
        private readonly EmployeeDataProvider _employeeDataProvider;
        public AuthService(EmployeeDataProvider employeeDataProvider)
        {
            _employeeDataProvider = employeeDataProvider;
        }
        public EmployeeMaster? GetEmployeeDetails(EmployeeLoginViewModel login)
        {
            EmployeeMaster? employee = _employeeDataProvider.GetEmployeeDetail(login);
            return employee;
        }
    }
}
