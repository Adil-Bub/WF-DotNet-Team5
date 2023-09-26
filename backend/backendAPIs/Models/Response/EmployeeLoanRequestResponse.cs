namespace backendAPIs.Models.Response
{
    public class EmployeeLoanRequestResponse
    {
        public string RequestId { get; set; } = null!;

        public string? EmployeeId { get; set; }

        public string? ItemId { get; set; }

        public DateTime? RequestDate { get; set; }

        public string? RequestStatus { get; set; }

        public DateTime? ReturnDate { get; set; }
    }
}
