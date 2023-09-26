namespace backendAPIs.Models.Response
{
    public class LoanDetailsAdminResponse
    {
        //DTO that returns Item details, request details and loan card details for a given employee id
        //Item details
        public string ItemId { get; set; } = null!;

        public string ItemDescription { get; set; } = null!;

        public string IssueStatus { get; set; } = null!;

        public string ItemMake { get; set; } = null!;

        public string? ItemCategory { get; set; }

        public int ItemValuation { get; set; }

        //Request details
        public string RequestId { get; set; } = null!;

        public DateTime? RequestDate { get; set; }

        public string? RequestStatus { get; set; }

        public DateTime? ReturnDate { get; set; }

        //Loan Card Details
        public string LoanId { get; set; } = null!;

        public int DurationInYears { get; set; }
    }
}
