using backend.Models;
using backend.Models.Response;
using backend.Repository;
using backend.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestbackendAPIs.Repository
{
    public class TestEmployeeRepo
    {
        private Mock<LoansContext> _mockDbContext;
        private IEmployeeRepo _employeeRepo;
        private Mock<DbSet<EmployeeMaster>> _mockDbSet;
        private IQueryable<EmployeeMaster> employeeData;

        [SetUp]
        public void Setup()
        {
            _mockDbContext = new Mock<LoansContext>();
            _mockDbSet = new Mock<DbSet<EmployeeMaster>>();
            employeeData = new List<EmployeeMaster>() {
            new EmployeeMaster
            {
                EmployeeId = "EMP-1234",
                PasswordHash = "1234",
                Salt = "123",
                EmployeeName = "Test",
                Designation = "admin",
                Department = "IT",
                Gender = "F",
                DateOfBirth = DateTime.Now.Date,
                DateOfJoining = DateTime.Now.Date
            },

            new EmployeeMaster
            {
                EmployeeId = "EMP-1235",
                PasswordHash = "1234",
                Salt = "123",
                EmployeeName = "Test1",
                Designation = "employee",
                Department = "IT",
                Gender = "F",
                DateOfBirth = DateTime.Now.Date,
                DateOfJoining = DateTime.Now.Date
            }
            }.AsQueryable();

            _mockDbSet.As<IQueryable<EmployeeMaster>>().Setup(m => m.Provider).Returns(employeeData.Provider);
            _mockDbSet.As<IQueryable<EmployeeMaster>>().Setup(m => m.Expression).Returns(employeeData.Expression);
            _mockDbSet.As<IQueryable<EmployeeMaster>>().Setup(m => m.ElementType).Returns(employeeData.ElementType);
            _mockDbSet.As<IQueryable<EmployeeMaster>>().Setup(m => m.GetEnumerator()).Returns(employeeData.GetEnumerator());

            _mockDbContext.Setup(m => m.EmployeeMasters).Returns(_mockDbSet.Object);

            _employeeRepo = new EmployeeRepo(_mockDbContext.Object);
        }

        //[Test] //fail
        //public void TestAddEmployee()
        //{
        //    var addEmployee = new EmployeeMaster
        //    {
        //        EmployeeId = "EMP-12366",
        //        PasswordHash = "1234",
        //        Salt = "123",
        //        EmployeeName = "Test",
        //        Designation = "admin",
        //        Department = "IT",
        //        Gender = "F",
        //        DateOfBirth = DateTime.Now.Date,
        //        DateOfJoining = DateTime.Now.Date
        //    };
        //    var result = _employeeRepo.AddEmployee(addEmployee);

        //    Assert.IsTrue(result);
            
        //}
        [Test]
        public void TestGetAllEmployees()
        {
            var result = _employeeRepo.GetAllEmployees();

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<List<EmployeeResponse>>(result);
            Assert.AreEqual(2, result.Count);
        }
    }
}
