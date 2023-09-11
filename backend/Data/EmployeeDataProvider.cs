﻿using backend.Models;
using backend.Models.Request;

namespace backend.Data
{
    public class EmployeeDataProvider : IEmployeeDataProvider
    {
        private readonly LoansContext _db;

        public EmployeeDataProvider(LoansContext db) {
            _db = db;
        }
        public EmployeeMaster? GetEmployeeDetail(EmployeeLoginViewModel employeeLoginData)
        {
            return _db.EmployeeMasters.SingleOrDefault(x => x.EmployeeId == employeeLoginData.EmployeeId && x.Password == employeeLoginData.Password);
        }
    }
}
