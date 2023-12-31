﻿using System;
using System.Collections.Generic;

namespace backendAPIs.Models;

public partial class EmployeeLoanCardDetail
{
    public string CardId { get; set; } = null!;

    public string? RequestId { get; set; }

    public string? EmployeeId { get; set; }

    public string? LoanId { get; set; }

    public DateTime? CardIssueDate { get; set; }

    public virtual EmployeeMaster? Employee { get; set; }

    public virtual LoanCardMaster? Loan { get; set; }

    public virtual EmployeeRequestDetail? Request { get; set; }
}
