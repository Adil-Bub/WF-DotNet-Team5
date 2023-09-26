using backendAPIs.Models;
using backendAPIs.Repository.Interfaces;
using backendAPIs.Models.Request;
using backendAPIs.Models.Response;
using backendAPIs.Services.Interfaces;
using Azure.Core;
using backendAPIs.Util;

namespace backendAPIs.Services
{
    public class EmployeeRequestService : IEmployeeRequestService
    {
        private readonly IEmployeeRequestDetailRepo _employeeRequestDetailRepo;

        public EmployeeRequestService(IEmployeeRequestDetailRepo employeeRequestDetailRepo)
        {
            _employeeRequestDetailRepo = employeeRequestDetailRepo;
        }
        public string AddEmployeeRequest(EmployeeLoanRequest employeeLoanRequest)
        {
            var employeeRequestDetail = new EmployeeRequestDetail
            {
                RequestId = UIDGenerator.GenerateUniqueVarcharId("REQ"),
                EmployeeId = employeeLoanRequest.EmployeeId,
                ItemId = employeeLoanRequest.ItemId,
                RequestDate = DateTime.Now.Date
            };

            return _employeeRequestDetailRepo.AddEmployeeRequest(employeeRequestDetail);
        }

        public EmployeeRequestDetail DeleteEmployeeRequest(string id)
        {
            return _employeeRequestDetailRepo.DeleteEmployeeRequest(id);
        }

        public bool UpdateEmployeeRequest(UpdateEmployeeLoanRequest employeeRequestDetail)
        {
            return _employeeRequestDetailRepo.UpdateEmployeeRequest(employeeRequestDetail);
        }

        public List<EmployeeLoanRequestResponse> GetAllEmployeeRequests()
        {
            var employeeRequestDetails = _employeeRequestDetailRepo.GetAllEmployeeRequests();
            var employeeLoanRequests = employeeRequestDetails.Select(employeeRequestDetail => new EmployeeLoanRequestResponse
            {
                RequestId = employeeRequestDetail.RequestId,
                EmployeeId = employeeRequestDetail.EmployeeId,
                ItemId = employeeRequestDetail.ItemId,
                RequestDate = employeeRequestDetail.RequestDate,
                RequestStatus = employeeRequestDetail.RequestStatus,
                ReturnDate = employeeRequestDetail.ReturnDate,
            }).ToList();
            return employeeLoanRequests;
        }

        public EmployeeLoanRequestResponse GetEmployeeRequestDetailByRequestId(string requestId)
        {
            var employeeRequestDetails = _employeeRequestDetailRepo.GetEmployeeRequestDetailByRequestId(requestId);
            if(employeeRequestDetails == null)
            {
                return null;
            }

            var employeeLoanRequestResponse = new EmployeeLoanRequestResponse
            {
                RequestId = employeeRequestDetails.RequestId,
                EmployeeId= employeeRequestDetails.EmployeeId,
                ItemId= employeeRequestDetails.ItemId,
                RequestDate= employeeRequestDetails.RequestDate,
                RequestStatus = employeeRequestDetails.RequestStatus,
                ReturnDate= employeeRequestDetails.ReturnDate,
            };

            return employeeLoanRequestResponse;
        }

        public List<EmployeeLoanRequestResponse> GetAllEmployeeRequestsByEmployeeId(string employeeId)
        {
            var employeeRequestDetails = _employeeRequestDetailRepo.GetAllEmployeeRequestsByEmployeeId(employeeId);
            var employeeLoanRequests = employeeRequestDetails.Select(employeeRequestDetail => new EmployeeLoanRequestResponse
            {
                RequestId = employeeRequestDetail.RequestId,
                EmployeeId = employeeRequestDetail.EmployeeId,
                ItemId = employeeRequestDetail.ItemId,
                RequestDate = employeeRequestDetail.RequestDate,
                RequestStatus = employeeRequestDetail.RequestStatus,
                ReturnDate = employeeRequestDetail.ReturnDate,
            }).ToList();
            return employeeLoanRequests;
        }

        public List<EmployeeLoanRequestResponse> GetAllEmployeeRequestsByStatus(string status)
        {
            var employeeRequestDetails = _employeeRequestDetailRepo.GetAllEmployeeRequestsByStatus(status);
            var employeeLoanRequests = employeeRequestDetails.Select(employeeRequestDetail => new EmployeeLoanRequestResponse
            {
                RequestId = employeeRequestDetail.RequestId,
                EmployeeId = employeeRequestDetail.EmployeeId,
                ItemId = employeeRequestDetail.ItemId,
                RequestDate = employeeRequestDetail.RequestDate,
                RequestStatus = employeeRequestDetail.RequestStatus,
                ReturnDate = employeeRequestDetail.ReturnDate,
            }).ToList();
            return employeeLoanRequests;
        }

        public List<EmployeeLoanRequestResponse> GetAllEmployeeRequestsByItemId(string itemId)
        {
            var employeeRequestDetails = _employeeRequestDetailRepo.GetAllEmployeeRequestsByItemId(itemId);
            var employeeLoanRequests = employeeRequestDetails.Select(employeeRequestDetail => new EmployeeLoanRequestResponse
            {
                RequestId = employeeRequestDetail.RequestId,
                EmployeeId = employeeRequestDetail.EmployeeId,
                ItemId = employeeRequestDetail.ItemId,
                RequestDate = employeeRequestDetail.RequestDate,
                RequestStatus = employeeRequestDetail.RequestStatus,
                ReturnDate = employeeRequestDetail.ReturnDate,
            }).ToList();
            return employeeLoanRequests;
        }

        public List<LoanDetailsResponse> GetAllLoanDetailsByEmployeeId(string employeeId)
        {
            return _employeeRequestDetailRepo.GetAllLoanDetailsByEmployeeId(employeeId);   
        }

        public List<LoanDetailsAdminResponse> GetAllRequestDetails()
        {
            return _employeeRequestDetailRepo.GetAllRequestDetails();
        }

        public List<LoanDetailsResponse> GetAllLoanDetailsByRequestId(string requestId)
        {
            throw new NotImplementedException();
        }
    }
}
