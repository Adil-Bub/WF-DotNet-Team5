namespace backendAPIs.Models.Request
{
    public class UpdateLoanCardRequest
    {
        public string LoanId { get; set; } = null!;

        public string LoanType { get; set; } = null!;

        public int DurationInYears { get; set; }
    }
}
