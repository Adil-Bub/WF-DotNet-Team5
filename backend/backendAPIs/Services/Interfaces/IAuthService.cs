using backend.Models;
using backend.Models.Request;
using backend.Models.Response;

namespace backend.Services.Interfaces
{
    public interface IAuthService
    {
        public EmployeeMaster? AuthenticateUser(LoginRequest login);
        public LoginResponse GenerateJSONWebToken(EmployeeMaster userInfo);
        public EmployeeMaster RegisterUser(RegisterRequest registerRequest);
    }
}
