using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class LoanCardMaster
{
    public string LoanId { get; set; } = null!;

    public string LoanType { get; set; } = null!;

    public int DurationInYears { get; set; }
}
