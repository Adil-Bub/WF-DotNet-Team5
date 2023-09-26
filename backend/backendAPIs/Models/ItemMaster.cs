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

    public ItemMaster() { }

    public ItemMaster(string itemId,string itemDescription,string issueStatus, string itemMake, string itemCategory,int itemValuation )
    {
        ItemId = itemId;
        ItemDescription =itemDescription;
        IssueStatus = issueStatus;
        ItemMake = itemMake;
        ItemCategory = itemCategory;
        ItemValuation = itemValuation;
    }
}


