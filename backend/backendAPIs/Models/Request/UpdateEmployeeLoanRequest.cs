﻿namespace backendAPIs.Models.Request
{
    public class UpdateEmployeeLoanRequest
    {
        public string RequestId { get; set; } = null!;
        public string? RequestStatus { get; set; }
    }
}
