namespace backendAPIs.Models.Response
{
    public class LoginResponse
    {
        public string Token { get; set; } = null!;
        public string EmployeeId { get; set; } = null!;
        public string Designation { get; set; } = null!;
        public string EmployeeName { get; set; } = null!;
        public LoginResponse() { }
        public LoginResponse(string token, string employeeId, string designation, string employeeName)
        {
            Token = token;
            EmployeeId = employeeId;
            Designation = designation;
            EmployeeName = employeeName;
        }
    }
}
