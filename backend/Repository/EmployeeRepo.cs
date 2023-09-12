using backend.Models;
using backend.Repository.Interfaces;

namespace backend.Repository
{
    public class EmployeeRepo : IEmployeeRepo
    {
        private readonly LoansContext _db;

        public EmployeeRepo(LoansContext db) {
            _db = db;
        }

        public EmployeeMaster? GetEmployeeById(string id)
        {
            return _db.EmployeeMasters.SingleOrDefault(e => e.EmployeeId==id);
        }

         
    }
}
