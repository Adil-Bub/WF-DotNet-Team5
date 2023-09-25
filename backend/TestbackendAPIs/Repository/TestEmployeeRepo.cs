using backend.Models;
using backend.Models.Request;
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
        
        private Mock<DbSet<EmployeeMaster>> _mockDbSetEmployeeMaster;
        private Mock<DbSet<EmployeeRequestDetail>> _mockDbSetEmployeeRequestDetail;
        private Mock<DbSet<EmployeeLoanCardDetail>> _mockDbSetEmployeeLoanCardDetail;
        
        private IQueryable<EmployeeMaster> employeeData;
        private IQueryable<EmployeeRequestDetail> employeeRequestData;
        private IQueryable<EmployeeLoanCardDetail> employeeLoanCardData;

        [SetUp]
        public void Setup()
        {
            
            _mockDbSetEmployeeMaster = new Mock<DbSet<EmployeeMaster>>();
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

            _mockDbSetEmployeeMaster.As<IQueryable<EmployeeMaster>>().Setup(m => m.Provider).Returns(employeeData.Provider);
            _mockDbSetEmployeeMaster.As<IQueryable<EmployeeMaster>>().Setup(m => m.Expression).Returns(employeeData.Expression);
            _mockDbSetEmployeeMaster.As<IQueryable<EmployeeMaster>>().Setup(m => m.ElementType).Returns(employeeData.ElementType);
            _mockDbSetEmployeeMaster.As<IQueryable<EmployeeMaster>>().Setup(m => m.GetEnumerator()).Returns(employeeData.GetEnumerator());

            _mockDbSetEmployeeRequestDetail = new Mock<DbSet<EmployeeRequestDetail>>();
            employeeRequestData = new List<EmployeeRequestDetail>()
            {
                new EmployeeRequestDetail
                {
                    RequestId = "REQ-1234",
                    EmployeeId = "EMP-1235",
                    ItemId = "ITEM-1234",
                    RequestDate = DateTime.Parse("2023-10-25"),
                    RequestStatus = "Approved",
                    ReturnDate = DateTime.Parse("2023-10-10")
                },
                new EmployeeRequestDetail
                {
                    RequestId = "REQ-1235",
                    EmployeeId = "EMP-1234",
                    ItemId = "ITEM-1232",
                    RequestDate = DateTime.Parse("2023-10-25"),
                    RequestStatus = "Approved",
                    ReturnDate = DateTime.Parse("2023-10-10")
                }
            }.AsQueryable();

            _mockDbSetEmployeeRequestDetail.As<IQueryable<EmployeeRequestDetail>>().Setup(m => m.Provider).Returns(employeeRequestData.Provider);
            _mockDbSetEmployeeRequestDetail.As<IQueryable<EmployeeRequestDetail>>().Setup(m => m.Expression).Returns(employeeRequestData.Expression);
            _mockDbSetEmployeeRequestDetail.As<IQueryable<EmployeeRequestDetail>>().Setup(m => m.ElementType).Returns(employeeRequestData.ElementType);
            _mockDbSetEmployeeMaster.As<IQueryable<EmployeeRequestDetail>>().Setup(m => m.GetEnumerator()).Returns(employeeRequestData.GetEnumerator());

            _mockDbSetEmployeeLoanCardDetail = new Mock<DbSet<EmployeeLoanCardDetail>>();
            employeeLoanCardData = new List<EmployeeLoanCardDetail>()
            {
                new EmployeeLoanCardDetail
                {
                    RequestId = "REQ-1234",
                    EmployeeId = "EMP-1235",
                    CardId = "CARD-1234",
                    LoanId = "LOAN-1234"
                },
                new EmployeeLoanCardDetail
                {
                    RequestId = "REQ-1235",
                    EmployeeId = "EMP-1234",
                    CardId = "CARD-1234",
                    LoanId = "LOAN-1234"
                }
            }.AsQueryable();

            _mockDbSetEmployeeLoanCardDetail.As<IQueryable<EmployeeLoanCardDetail>>().Setup(m => m.Provider).Returns(employeeLoanCardData.Provider);
            _mockDbSetEmployeeLoanCardDetail.As<IQueryable<EmployeeLoanCardDetail>>().Setup(m => m.Expression).Returns(employeeLoanCardData.Expression);
            _mockDbSetEmployeeLoanCardDetail.As<IQueryable<EmployeeLoanCardDetail>>().Setup(m => m.ElementType).Returns(employeeLoanCardData.ElementType);
            _mockDbSetEmployeeLoanCardDetail.As<IQueryable<EmployeeLoanCardDetail>>().Setup(m => m.GetEnumerator()).Returns(employeeLoanCardData.GetEnumerator());

            var options = new DbContextOptions<LoansContext>();
            _mockDbContext = new Mock<LoansContext>(options);

            _mockDbContext.Setup(m => m.EmployeeMasters).Returns(_mockDbSetEmployeeMaster.Object);
            _mockDbContext.Setup(m => m.EmployeeRequestDetails).Returns(_mockDbSetEmployeeRequestDetail.Object);
            _mockDbContext.Setup(m => m.EmployeeLoanCardDetails).Returns(_mockDbSetEmployeeLoanCardDetail.Object);

            _employeeRepo = new EmployeeRepo(_mockDbContext.Object);
        }

        [Test] //fail
        public void TestAddEmployee()
        {
            var addEmployee = new EmployeeMaster
            {
                EmployeeId = "EMP-12366",
                PasswordHash = "1234",
                Salt = "123",
                EmployeeName = "Test",
                Designation = "admin",
                Department = "IT",
                Gender = "F",
                DateOfBirth = DateTime.Now.Date,
                DateOfJoining = DateTime.Now.Date
            };
            var result = _employeeRepo.AddEmployee(addEmployee);

            Assert.IsTrue(result);

        }
        [Test]
        public void TestGetAllEmployees()
        {
            var result = _employeeRepo.GetAllEmployees();

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<List<EmployeeResponse>>(result);
            Assert.AreEqual(2, result.Count);
        }

        [TestCase("EMP-1234")]
        public void TestGetEmployeeById(string id)
        {
            var result = _employeeRepo.GetEmployeeById(id);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<EmployeeMaster>(result);
            
        }

        [Test]
        public void TestUpdateEmployee()
        {
            var updateRequest = new UpdateEmployeeRequest
            {
                EmployeeId = "EMP-1234",
                EmployeeName = "Test1",
                Designation = null,
                Department = null,
                Gender = "F",
                DateOfBirth = null,
                DateOfJoining = null
            };

            var result = _employeeRepo.UpdateEmployee(updateRequest); 
            
            Assert.IsNotNull(result);
            Assert.IsTrue(result);
        }

        [TestCase("EMP-1235")]
        public void TestDeleteEmployee(string id)
        {
            var result = _employeeRepo.DeleteEmployee(id);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<EmployeeResponse>(result);

        }
    }
}
