using backendAPIs.Models;
using backendAPIs.Models.Request;
using backendAPIs.Models.Response;

namespace backendAPIs.Services.Interfaces
{
    public interface IAuthService
    {
        public EmployeeMaster? AuthenticateUser(LoginRequest login);
        public LoginResponse GenerateJSONWebToken(EmployeeMaster userInfo);
        public EmployeeMaster RegisterUser(RegisterRequest registerRequest);
    }
}
