namespace backendAPIs.Models.Response
{
    public class LoanCardResponse
    {
        public string LoanId { get; set; } = null!;

        public string? LoanType { get; set; }

        public int DurationInYears { get; set; }
    }
}
