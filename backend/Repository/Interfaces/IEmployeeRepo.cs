﻿using backend.Models;

namespace backend.Repository.Interfaces
{
    public interface IEmployeeRepo
    {
        public EmployeeMaster? GetEmployeeById(string id);
    }
}
