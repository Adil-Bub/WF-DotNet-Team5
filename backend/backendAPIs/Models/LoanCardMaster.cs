using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class LoanCardMaster
{
    public string LoanId { get; set; } = null!;

    public string LoanType { get; set; } = null!;

    public int DurationInYears { get; set; }

    public virtual ICollection<EmployeeLoanCardDetail> EmployeeLoanCardDetails { get; set; } = new List<EmployeeLoanCardDetail>();

    public virtual ICollection<ItemMaster> ItemMasters { get; set; } = new List<ItemMaster>();
}
