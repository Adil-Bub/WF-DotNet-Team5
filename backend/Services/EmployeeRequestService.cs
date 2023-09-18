using backend.Models;
using backend.Repository.Interfaces;
using backend.Models.Request;
using backend.Models.Response;
using backend.Services.Interfaces;
using Azure.Core;
using backend.Util;

namespace backend.Services
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
    }
}
