using System;
using System.Collections.Generic;

namespace backendAPIs.Models;

public partial class EmployeeRequestDetail
{
    public string RequestId { get; set; } = null!;

    public string? EmployeeId { get; set; }

    public string? ItemId { get; set; }

    public DateTime? RequestDate { get; set; }

    public string? RequestStatus { get; set; }

    public DateTime? ReturnDate { get; set; }

    public virtual EmployeeMaster? Employee { get; set; }

    public virtual ICollection<EmployeeLoanCardDetail> EmployeeLoanCardDetails { get; set; } = new List<EmployeeLoanCardDetail>();

    public virtual ItemMaster? Item { get; set; }
}
