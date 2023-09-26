using backendAPIs.Models;
using backendAPIs.Repository.Interfaces;

namespace backendAPIs.Repository
{
    public class EmployeeLoanCardDetailRepo : IEmployeeLoanCardDetailRepo
    {
        private readonly LoansContext _db;

        public EmployeeLoanCardDetailRepo(LoansContext db)
        {
            _db = db;
        }
        public string AddEmployeeLoanCard(EmployeeLoanCardDetail employeeLoanCardDetail)
        {
            try
            {
                _db.EmployeeLoanCardDetails.Add(employeeLoanCardDetail);
                _db.SaveChanges();
                return employeeLoanCardDetail.CardId;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public List<EmployeeLoanCardDetail> GetAllApprovedLoans()
        {
            return _db.EmployeeLoanCardDetails.ToList();
        }

        public List<EmployeeLoanCardDetail> GetAllApprovedLoansByEmployeeId(string employeeId)
        {
            return _db.EmployeeLoanCardDetails
                .Where(loanCard => loanCard.EmployeeId == employeeId)
                .ToList();
        }
    }
}
