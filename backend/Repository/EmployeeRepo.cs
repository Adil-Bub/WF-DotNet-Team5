using backend.Models;
using backend.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backend.Repository
{
    public class EmployeeRepo : IEmployeeRepo
    {
        private readonly LoansContext _db;

        public EmployeeRepo(LoansContext db)
        {
            _db = db;
        }

        public EmployeeMaster? GetEmployeeById(string id)
        {
            return _db.EmployeeMasters.SingleOrDefault(e => e.EmployeeId==id);
        }

        public bool AddEmployee(EmployeeMaster employee)
        {
            try
            {
                _db.Set<EmployeeMaster>().Add(employee);
                _db.SaveChanges();
                //_db.EmployeeMasters.Add(employee);
                // _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

    }
}
