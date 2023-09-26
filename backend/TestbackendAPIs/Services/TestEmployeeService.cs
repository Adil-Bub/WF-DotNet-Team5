using backendAPIs.Services;
using backendAPIs.Models.Response;
using backendAPIs.Models;
using backendAPIs.Repository.Interfaces;
using backendAPIs.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using backendAPIs.Models.Request;
using backendAPIs.Controllers;

namespace TestbackendAPIs.Services
{
    public class TestEmployeeService
    {
        private IEmployeeService _employeeService;
        private Mock<IEmployeeRepo> _mockEmployeeRepo;

        private List<EmployeeResponse> employeeResponseList;
        private EmployeeMaster employeeMaster;
        private EmployeeResponse employeeResponse;
        [SetUp]
        public void Setup()
        {
            _mockEmployeeRepo = new Mock<IEmployeeRepo>();
            _employeeService = new EmployeeService(_mockEmployeeRepo.Object);

            employeeResponseList = new List<EmployeeResponse>() {
            new EmployeeResponse
            {
                EmployeeId = "EMP-1234",
                EmployeeName = "Test",
                Designation = "admin",
                Department = "IT",
                Gender = "F",
                DateOfBirth = DateTime.Now.Date,
                DateOfJoining = DateTime.Now.Date
            },

            new EmployeeResponse
            {
                EmployeeId = "EMP-1235",
                EmployeeName = "Test1",
                Designation = "employee",
                Department = "IT",
                Gender = "F",
                DateOfBirth = DateTime.Now.Date,
                DateOfJoining = DateTime.Now.Date
            }
        };
            employeeMaster = new EmployeeMaster
            {
                EmployeeId = "EMP-1234",
                PasswordHash = "1234", 
                Salt = "1234",
                EmployeeName = "Test",
                Designation = "admin",
                Department = "IT",
                Gender = "F",
                DateOfBirth = DateTime.Now.Date,
                DateOfJoining = DateTime.Now.Date
            };
            employeeResponse = new EmployeeResponse
            {
                EmployeeId = "EMP-1234",
                EmployeeName = "Test",
                Designation = "admin",
                Department = "IT",
                Gender = "F",
                DateOfBirth = DateTime.Now.Date,
                DateOfJoining = DateTime.Now.Date
            };
        }

        [Test]
        public void TestGetAllEmployees()
        {
            _mockEmployeeRepo.Setup(repo => repo.GetAllEmployees()).Returns(employeeResponseList);

            var result = _employeeService.GetAllEmployees();

            Assert.That(result, Is.InstanceOf<List<EmployeeResponse>>());
        }

        [TestCase("EMP-1234")]
        public void TestGetEmployeeById(string id)
        {
            _mockEmployeeRepo.Setup(repo => repo.GetEmployeeById(id)).Returns(employeeMaster);

            var result = _employeeService.GetEmployeeById(id);

            Assert.IsInstanceOf<EmployeeResponse>(result);
        }

        [TestCase("EMP-1234")]
        public void TestUpdateEmployee(string id)
        {
            var updateEmployeeRequest = new UpdateEmployeeRequest
            {
                EmployeeId = "EMP-2134",
                EmployeeName = "Test",
                Designation = "admin",
                Department = "IT",
                Gender = "F",
                DateOfBirth = DateTime.Now.Date,
                DateOfJoining = DateTime.Now.Date
            };

            _mockEmployeeRepo.Setup(repo => repo.UpdateEmployee(updateEmployeeRequest)).Returns(true);

            var result = _employeeService.UpdateEmployee(updateEmployeeRequest);

            Assert.IsTrue(result);
        }

        [TestCase("EMP-1234")]
        public void TestDeleteEmployee(string id)
        {

            _mockEmployeeRepo.Setup(repo => repo.DeleteEmployee(id)).Returns(employeeResponse);

            var result = _employeeService.DeleteEmployee(id);

            Assert.IsInstanceOf<EmployeeResponse>(result);
        }
    }
}
