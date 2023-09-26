namespace backendAPIs.Models.Request
{
    public class AddItemRequest
    {
        public string ItemDescription { get; set; } = null!;

        public string IssueStatus { get; set; } = null!;

        public string ItemMake { get; set; } = null!;

        public string? ItemCategory { get; set; }

        public int ItemValuation { get; set; }
    }
}
