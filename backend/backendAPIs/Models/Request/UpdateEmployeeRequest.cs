namespace backend.Models.Request
{
    public class UpdateEmployeeRequest
    {
        public string EmployeeId { get; set; } = null!;
        public string EmployeeName { get; set; } = null!;
        public string Designation { get; set; } = null!;
        public string Department { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public DateTime? DateOfBirth { get; set; }
        public DateTime? DateOfJoining { get; set; }

    }
}
