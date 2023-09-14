using backend.Models;
using backend.Models.Response;
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

        public List<EmployeeResponse> GetAllEmployees()
        {
            var response = _db.EmployeeMasters
                .Select(employee => new EmployeeResponse
                {
                    EmployeeId = employee.EmployeeId,
                    EmployeeName = employee.EmployeeName,
                    Designation = employee.Designation,
                    Department = employee.Department,
                    Gender = employee.Gender,
                    DateOfBirth = employee.DateOfBirth,
                    DateOfJoining = employee.DateOfJoining
                })
                .ToList();
            return response;
        }

        public EmployeeMaster? GetEmployeeById(string employeeId)
        {
            var employee = _db.EmployeeMasters.FirstOrDefault(employee => employee.EmployeeId == employeeId);
            return employee;
        }

    }
}
