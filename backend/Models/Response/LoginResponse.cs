namespace backend.Models.Response
{
    public class LoginResponse
    {
        public string Token { get; set; } = null!;
        public string EmployeeId { get; set; } = null!;
        public string Designation { get; set; } = null!;
        public LoginResponse() { }
        public LoginResponse(string token, string employeeId, string designation)
        {
            Token = token;
            EmployeeId = employeeId;
            Designation = designation;
        }
    }
}
