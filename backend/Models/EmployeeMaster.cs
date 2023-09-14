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

    public virtual ICollection<EmployeeLoanCardDetail> EmployeeLoanCardDetails { get; set; } = new List<EmployeeLoanCardDetail>();

    public virtual ICollection<EmployeeRequestDetail> EmployeeRequestDetails { get; set; } = new List<EmployeeRequestDetail>();
}
