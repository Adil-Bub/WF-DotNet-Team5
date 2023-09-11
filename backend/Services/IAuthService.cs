using backend.Models;
using backend.Models.Request;

namespace backend.Services
{
    public interface IAuthService
    {
        public EmployeeMaster? GetEmployeeDetails(EmployeeLoginViewModel login);
    }
}
