using backend.Models;
using backend.Models.Request;
using backend.Repository.Interfaces;
using backend.Services;

namespace backend.Repository
{
    public class EmployeeRequestDetailRepo : IEmployeeRequestDetailRepo
    {
        private readonly LoansContext _db;

        public EmployeeRequestDetailRepo(LoansContext db)
        {
            _db = db;
        }
        public string AddEmployeeRequest(EmployeeRequestDetail employeeRequestDetail)
        {
            try
            {
                var employeeRequestExists = _db.EmployeeRequestDetails
                    .FirstOrDefault(employeeRequest => employeeRequest.EmployeeId == employeeRequestDetail.EmployeeId &&
                    employeeRequest.ItemId == employeeRequestDetail.ItemId);
                    
                if(employeeRequestExists !=null)
                {
                    return null;
                }
                _db.EmployeeRequestDetails.Add(employeeRequestDetail);
                _db.SaveChanges();
                return employeeRequestDetail.RequestId;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public EmployeeRequestDetail DeleteEmployeeRequest(string id)
        {
            EmployeeRequestDetail? employeeRequestDetail = _db.EmployeeRequestDetails.Find(id);
            if (employeeRequestDetail == null)
            {
                return null;
            }
            else
            {
                try
                {
                    _db.EmployeeRequestDetails.Remove(employeeRequestDetail);

                    _db.SaveChanges();
                    return employeeRequestDetail;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }
            }
        }

        public bool UpdateEmployeeRequest(UpdateEmployeeLoanRequest employeeRequestDetail)
        {
            var existingEmployeeRequest = _db.EmployeeRequestDetails.FirstOrDefault(emplReq => emplReq.RequestId == employeeRequestDetail.RequestId);
            Console.WriteLine("employee is " + existingEmployeeRequest);
            if (existingEmployeeRequest != null)
            {
                existingEmployeeRequest.RequestStatus = employeeRequestDetail.RequestStatus ?? existingEmployeeRequest.RequestStatus;
                existingEmployeeRequest.ReturnDate = employeeRequestDetail.ReturnDate ?? existingEmployeeRequest.ReturnDate;

                if(existingEmployeeRequest.RequestStatus == "Approved")
                {
                    //implement add to employeeLoanCardDetails
                }
                try
                {
                    _db.Entry(existingEmployeeRequest).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    _db.SaveChangesAsync();
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }

            return false;
        }

        public List<EmployeeRequestDetail> GetAllEmployeeRequests()
        {
            return _db.EmployeeRequestDetails.ToList();
        }

        public List<EmployeeRequestDetail> GetAllEmployeeRequestsByEmployeeId(string employeeId)
        {
            return _db.EmployeeRequestDetails
                .Where(employeeRequest => employeeRequest.EmployeeId == employeeId)
                .ToList();
        }

        public EmployeeRequestDetail GetEmployeeRequestDetailByRequestId(string requestId)
        {
            return _db.EmployeeRequestDetails.Find(requestId);
        }

        public List<EmployeeRequestDetail> GetAllEmployeeRequestsByStatus(string status)
        {
            return _db.EmployeeRequestDetails
               .Where(employeeRequest => employeeRequest.RequestStatus == status)
               .ToList();
        }

        public List<EmployeeRequestDetail> GetAllEmployeeRequestsByItemId(string itemId)
        {
            return _db.EmployeeRequestDetails
               .Where(employeeRequest => employeeRequest.ItemId == itemId)
               .ToList();
        }
    }
}
