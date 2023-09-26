using backendAPIs.Models;

namespace backendAPIs.Models.Response
{
    public class ItemResponse
    {
        public string ItemId { get; set; } = null!;

        public string ItemDescription { get; set; } = null!;

        public string IssueStatus { get; set; } = null!;

        public string ItemMake { get; set; } = null!;

        public string? ItemCategory { get; set; }

        public int ItemValuation { get; set; }

        public ItemResponse() { }

        public ItemResponse(ItemMaster item)
        {
            ItemId = item.ItemId;
            ItemDescription = item.ItemDescription;
            IssueStatus = item.IssueStatus;
            ItemMake = item.ItemMake;
            ItemCategory = item.ItemCategory;
            ItemValuation = item.ItemValuation;
        }
    }
}
