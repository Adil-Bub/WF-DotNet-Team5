namespace backend.Models.Request
{
    public class UpdateEmployeeLoanRequest
    {
        public string RequestId { get; set; } = null!;
        public string? RequestStatus { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}
