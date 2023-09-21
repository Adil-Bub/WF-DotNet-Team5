using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class ItemMaster
{
    public string ItemId { get; set; } = null!;

    public string ItemDescription { get; set; } = null!;

    public string IssueStatus { get; set; } = null!;

    public string ItemMake { get; set; } = null!;

    public string? ItemCategory { get; set; }

    public int ItemValuation { get; set; }

    public virtual ICollection<EmployeeRequestDetail> EmployeeRequestDetails { get; set; } = new List<EmployeeRequestDetail>();

    public virtual LoanCardMaster? ItemCategoryNavigation { get; set; }
}
