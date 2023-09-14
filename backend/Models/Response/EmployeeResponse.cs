namespace backend.Models.Response
{
    public class EmployeeResponse
    {
        public string EmployeeId { get; set; } = null!;

        public string EmployeeName { get; set; } = null!;

        public string Designation { get; set; } = null!;

        public string Department { get; set; } = null!;

        public string Gender { get; set; } = null!;

        public DateTime? DateOfBirth { get; set; }

        public DateTime? DateOfJoining { get; set; }

        public EmployeeResponse() { }

        public EmployeeResponse(EmployeeMaster employee)
        {
            EmployeeId = employee.EmployeeId;
            EmployeeName = employee.EmployeeName;
            Department = employee.Department;
            Gender = employee.Gender;
            DateOfBirth = employee.DateOfBirth;
            DateOfJoining = employee.DateOfJoining;
        }
    }
}
