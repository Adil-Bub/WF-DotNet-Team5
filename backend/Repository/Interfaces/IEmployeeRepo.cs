using backend.Models;
using backend.Models.Request;

namespace backend.Repository.Interfaces
{
    public interface IEmployeeRepo<EmployeeMaster>
    {
        public EmployeeMaster? GetEmployeeDetail(LoginRequest employeeLoginData);
    }
}
