using backend.Models;
using backend.Models.Request;

namespace backend.Data
{
    public interface IEmployeeDataProvider<EmployeeMaster>
    {
        public EmployeeMaster? GetEmployeeDetail(EmployeeLoginViewModel employeeLoginData);
    }
}
