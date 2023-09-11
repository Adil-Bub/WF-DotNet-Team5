using backend.Models;
using backend.Models.Request;

namespace backend.Data
{
    public interface IEmployeeDataProvider
    {
        public EmployeeMaster? GetEmployeeDetail(EmployeeLoginViewModel employeeLoginData);
    }
}
