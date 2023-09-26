using backend.Models;
using backend.Models.Request;
using backend.Models.Response;

namespace backendAPIs.Repository
{
    public class ItemRepo: IItemRepo
    {
        private readonly LoansContext _db;

        public ItemRepo(LoansContext db) { 
            _db= db;
        }

        public bool AddItem(ItemMaster item)
        {
            try
            {
                _db.Set<ItemMaster>().Add(item);
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

        public List<ItemResponse> GetAllItems()
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
                existingEmployee.DateOfJoining = employee.DateOfJoining ?? existingEmployee.DateOfJoining;

                /*
                 * Functionality for admin only
                 * if (employee.Password!=null)
                {
                    (existingEmployee.PasswordHash, existingEmployee.Salt) = PasswordHelper.HashPassword(employee.Password);
                }*/
                try
                {
                    _db.Entry(existingEmployee).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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

        public EmployeeResponse? DeleteEmployee(string employeeId)
        {
            EmployeeMaster? employee = _db.EmployeeMasters.Find(employeeId);
            if (employee == null)
            {
                return null;
            }
            else
            {
                var employeeRequests = _db.EmployeeRequestDetails
                    .Where(request => request.EmployeeId == employeeId)
                    .ToList();

                //Deleting approved requests
                var employeeLoanCards = _db.EmployeeLoanCardDetails
                    .Where(loanCard => loanCard.EmployeeId == employeeId)
                    .ToList();
                _db.EmployeeLoanCardDetails.RemoveRange(employeeLoanCards);
                _db.SaveChanges();

                //deleting all requests
                _db.EmployeeRequestDetails.RemoveRange(employeeRequests);
                _db.SaveChanges();


                //deleting the employee
                _db.EmployeeMasters.Remove(employee);
                _db.SaveChanges();
                var deletedEmployee = new EmployeeResponse
                {
                    EmployeeId = employee.EmployeeId,
                    EmployeeName = employee.EmployeeName,
                    Designation = employee.Designation,
                    Department = employee.Department,
                    Gender = employee.Gender,
                    DateOfBirth = employee.DateOfBirth,
                    DateOfJoining = employee.DateOfJoining
                };
                return deletedEmployee;
            }
        }

    }
}
