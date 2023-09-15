using backend.Models;
using backend.Models.Response;
using backend.Models.Request;
using backend.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using backend.Services;

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

        public bool UpdateEmployee(UpdateEmployeeRequest employee)
        {
            var existingEmployee = _db.EmployeeMasters.FirstOrDefault(empl => empl.EmployeeId == employee.EmployeeId);
            Console.WriteLine("employee is " + employee);
            if (existingEmployee != null) 
            {
                existingEmployee.EmployeeName = employee.EmployeeName ?? existingEmployee.EmployeeName;
                existingEmployee.Designation = employee.Designation ?? existingEmployee.Designation;
                existingEmployee.Department = employee.Department ?? existingEmployee.Department;
                existingEmployee.Gender = employee.Gender ?? existingEmployee.Gender;
                existingEmployee.DateOfBirth = employee.DateOfBirth ?? existingEmployee.DateOfBirth;

                if(employee.Password!=null)
                {
                    (existingEmployee.PasswordHash, existingEmployee.Salt) = PasswordHelper.HashPassword(employee.Password);
                }

                try
                {
                    _db.Entry(existingEmployee).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    _db.SaveChangesAsync();
                    return true;
                }
                catch(Exception e)
                {
                    return false;
                }
            }

            return false;
        }

    }
}
