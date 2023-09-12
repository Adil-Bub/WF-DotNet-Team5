using backend.Models;
using backend.Models.Request;
using backend.Repository.Interfaces;

namespace backend.Data
{
    public class EmployeeRepo : IEmployeeRepo<EmployeeMaster>
    {
        private readonly LoansContext _db;

        public EmployeeRepo(LoansContext db) {
            _db = db;
        }
        public EmployeeMaster? GetEmployeeDetail(LoginRequest employeeLoginData)
        {
            return _db.EmployeeMasters.SingleOrDefault(x => x.EmployeeId == employeeLoginData.EmployeeId && x.PasswordHash == employeeLoginData.Password);
        }
    }
}
