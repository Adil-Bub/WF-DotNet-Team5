namespace backend.Models.Request
{
    public class LoanCardRequest
    {
        public string? LoanType { get; set; }

        public int DurationInYears { get; set; }
    }
}
