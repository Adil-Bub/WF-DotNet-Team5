using backend.Controllers;
using backend.Models.Response;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace TestbackendAPIs.Controllers
{
    public class TestEmployeeController
    {
        private EmployeeController _employeeController;
        private Mock<IEmployeeService> _mockEmployeeService;

        List<EmployeeResponse> employeeResponse = new List<EmployeeResponse>() {
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
        
        [SetUp]
        public void Setup()
        {
            _mockEmployeeService = new Mock<IEmployeeService>();
            _employeeController = new EmployeeController(_mockEmployeeService.Object);
        }

        [Test]
        public void GetEmployees_Valid()
        {
            _mockEmployeeService.Setup(service => service.GetAllEmployees()).Returns(employeeResponse);

            var result = _employeeController.GetEmployees();

            Assert.That(result, Is.InstanceOf<Task<ActionResult<IEnumerable<EmployeeResponse>>>>());
        }
    }
}
