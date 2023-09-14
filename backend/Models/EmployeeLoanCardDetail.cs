using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class EmployeeLoanCardDetail
{
    public string CardId { get; set; } = null!;

    public string? EmployeeId { get; set; }

    public string? LoanId { get; set; }

    public DateTime? CardIssueDate { get; set; }

    public virtual EmployeeMaster? Employee { get; set; }

    public virtual LoanCardMaster? Loan { get; set; }
}
