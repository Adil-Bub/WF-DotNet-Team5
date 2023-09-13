using backend.Models.Request;
using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class EmployeeMaster
{
    public string EmployeeId { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string Salt { get; set; } = null!;

    public string EmployeeName { get; set; } = null!;

    public string Designation { get; set; } = null!;

    public string Department { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public DateTime? DateOfBirth { get; set; }

    public DateTime? DateOfJoining { get; set; }

    public virtual ICollection<EmployeeIssueDetail> EmployeeIssueDetails { get; set; } = new List<EmployeeIssueDetail>();

    public EmployeeMaster() { }
    public EmployeeMaster(string hashedPassword, string salt, RegisterRequest registerRequest)
    {
        EmployeeId = registerRequest.EmployeeId;
        PasswordHash = hashedPassword;
        Salt = salt;
        EmployeeName = registerRequest.EmployeeName;
        Designation = registerRequest.Designation;
        Department = registerRequest.Department;
        Gender = registerRequest.Gender;
        DateOfBirth = registerRequest.DateOfBirth;
        DateOfJoining = registerRequest.DateOfJoining;
    }
}
