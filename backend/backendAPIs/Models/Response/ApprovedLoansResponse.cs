namespace backendAPIs.Models.Response
{
    public class ApprovedLoansResponse
    {
        public string CardId { get; set; } = null!;

        public string? RequestId { get; set; }

        public string? EmployeeId { get; set; }

        public string? LoanId { get; set; }

        public DateTime? CardIssueDate { get; set; }
    }
}
