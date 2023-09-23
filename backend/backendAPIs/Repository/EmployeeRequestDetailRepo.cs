using backend.Models;
using backend.Models.Request;
using backend.Models.Response;
using backend.Repository.Interfaces;
using backend.Services;
using backend.Util;

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

                //Adding approveed loans to employee loan card details
                if (existingEmployeeRequest.RequestStatus == "Approved")
                {
                    var loanCategory = _db.ItemMasters
                        .Where(item => existingEmployeeRequest.ItemId == item.ItemId)
                        .Select(item => item.ItemCategory)
                        .FirstOrDefault();

                    var loanId = _db.LoanCardMasters
                        .Where(loan => loan.LoanType == loanCategory)
                        .Select(loan => loan.LoanId)
                        .FirstOrDefault();

                    
                    var employeeLoanCardDetail = new EmployeeLoanCardDetail
                    {
                        CardId = UIDGenerator.GenerateUniqueVarcharId("LOAN_CARD"),
                        RequestId = existingEmployeeRequest.RequestId,
                        EmployeeId = existingEmployeeRequest.EmployeeId,
                        LoanId = loanId,
                        CardIssueDate = DateTime.Now.Date
                    };
                    try
                    {
                        var loanCard = _db.EmployeeLoanCardDetails
                            .Where(lc => lc.RequestId ==  existingEmployeeRequest.RequestId)
                            .FirstOrDefault();
                        if(loanCard == null)
                        {
                            _db.EmployeeLoanCardDetails.Add(employeeLoanCardDetail);
                            _db.SaveChanges();
                        }
                        
                        //return employeeLoanCardDetail.CardId;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                        return false;
                    }
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

        public List<LoanDetailsResponse> GetAllLoanDetailsByEmployeeId(string employeeId)
        {
            var loanDetails = _db.EmployeeRequestDetails
                .Where(employeeRequest => employeeRequest.EmployeeId == employeeId)
                .Join(
                    _db.ItemMasters,
                    employeeRequest => employeeRequest.ItemId,
                    item => item.ItemId,
                    (employeeRequest, item) => new { EmployeeRequestDetail = employeeRequest, ItemMaster = item }
                )
                .Join(
                    _db.LoanCardMasters,
                    joined => joined.ItemMaster.ItemCategory,
                    loan => loan.LoanType,
                    (joined, loan) => new { joined.EmployeeRequestDetail, joined.ItemMaster, LoanCardMaster = loan }
                )
                .Select(
                    joined => new LoanDetailsResponse
                    {
                        ItemId = joined.ItemMaster.ItemId,
                        ItemDescription = joined.ItemMaster.ItemDescription,
                        IssueStatus = joined.ItemMaster.IssueStatus,
                        ItemMake = joined.ItemMaster.ItemMake,
                        ItemCategory = joined.ItemMaster.ItemCategory,
                        ItemValuation = joined.ItemMaster.ItemValuation,

                        RequestId = joined.EmployeeRequestDetail.RequestId,
                        EmployeeId = joined.EmployeeRequestDetail.EmployeeId,
                        RequestDate = joined.EmployeeRequestDetail.RequestDate,
                        RequestStatus = joined.EmployeeRequestDetail.RequestStatus,
                        ReturnDate = joined.EmployeeRequestDetail.ReturnDate,

                        LoanId = joined.LoanCardMaster.LoanId,
                        DurationInYears = joined.LoanCardMaster.DurationInYears

                    }
                )
                .ToList();

            return loanDetails;
        }
    }
}
