namespace backend.Models.Response
{
    public class LoginResponse
    {
        public string token { get; set; } = null!;
        public string EmployeeId { get; set; } = null!;
        public string Designation { get; set; } = null!;
        }
}
